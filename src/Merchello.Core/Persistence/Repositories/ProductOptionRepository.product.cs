namespace Merchello.Core.Persistence.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Merchello.Core.Models;
    using Merchello.Core.Models.Rdbms;

    /// <inheritdoc/>
    internal partial class ProductOptionRepository : IProductOptionRepository
    {
        /// <inheritdoc/>
        public IEnumerable<Guid> SaveForProduct(IProduct product)
        {
            // Ensures the sort order with respect to this product
            if (!product.ProductOptions.Any())
                EnsureProductOptionsSortOrder(product.ProductOptions);

            // Reset the Product Options Collection so that updated values are ordered and cached correctly
            product.ProductOptions = SaveForProduct(product.ProductOptions.AsEnumerable(), product.Key);

            return product.ProductOptions.Where(x => x.Shared).Select(x => x.Key);
        }

        /// <inheritdoc/>
        public ProductOptionCollection GetProductOptionCollection(Guid productKey)
        {
            var options = GetByProductKey(productKey);

            // Build the collection
            var collection = new ProductOptionCollection();

            foreach (var o in options)
            {
                collection.Add(o);
            }

            return collection;
        }

        /// <inheritdoc/>
        public IEnumerable<Guid> DeleteAllProductOptions(IProduct product)
        {
            var sharedOptionKeys = product.ProductOptions.Where(x => x.Shared).Select(x => x.Key).ToArray();

            var statements = GetRemoveAllProductOptionsFromProductSql(product);

            foreach (var sql in statements)
            {
                Database.Execute(sql);
            }

            return sharedOptionKeys;
        }


        /// <summary>
        /// Saves a collection of product options for a given product.
        /// </summary>
        /// <param name="options">
        /// The options.
        /// </param>
        /// <param name="productKey">
        /// The product key.
        /// </param>
        /// <returns>
        /// The saved collection <see cref="IEnumerable{IProductOption}"/>.
        /// </returns>
        internal ProductOptionCollection SaveForProduct(IEnumerable<IProductOption> options, Guid productKey)
        {
            // Order the options by sort order
            var savers = options.OrderBy(x => x.SortOrder);

            // Find the product options to find any that need to be removed 
            var existing = GetByProductKey(productKey).ToArray();

            if (existing.Any())
            {
                // Remove any options that previously existed in the product option collection that are not present in the new collection
                this.SafeRemoveSharedOptionsFromProduct(savers, existing, productKey);
            }

            foreach (var o in savers)
            {
                this.SafeAddOrUpdateProductWithProductOption(o, existing.Any(x => x.Key == o.Key), productKey);
            }

            return GetProductOptionCollection(productKey);
        }

        /// <summary>
        /// Adds or updates a product option, respecting shared option rules.
        /// </summary>
        /// <param name="option">
        /// The option.
        /// </param>
        /// <param name="exists">
        /// The exists.
        /// </param>
        /// <param name="productKey">
        /// The product key.
        /// </param>
        private void SafeAddOrUpdateProductWithProductOption(IProductOption option, bool exists, Guid productKey)
        {
            var makeAssociation = false;

            if (!option.HasIdentity)
            {
                // this option is being added through the product UI
                option.Shared = false;
                PersistNewItem(option);
                makeAssociation = true;
            }
            else
            {
                if (!exists) makeAssociation = true;

                if (!option.Shared) PersistUpdatedItem(option);
            }

            if (makeAssociation)
            {
                var dto = new Product2ProductOptionDto
                {
                    OptionKey = option.Key,
                    ProductKey = productKey,
                    SortOrder = option.SortOrder,
                    UseName = option.UseName,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now
                };

                Database.Insert(dto);

                if (!option.Shared) return;

                foreach (
                    var mapDto in
                        option.Choices.Select(
                            choice =>
                            new ProductOptionAttributeShareDto
                            {
                                ProductKey = productKey,
                                OptionKey = choice.OptionKey,
                                AttributeKey = choice.Key,
                                IsDefaultChoice = choice.IsDefaultChoice,
                                CreateDate = DateTime.Now,
                                UpdateDate = DateTime.Now
                            }))
                {
                    this.Database.Insert(mapDto);
                }
            }
            else
            {
                Database.Update<Product2ProductOptionDto>(
                    "SET sortOrder = @so, useName = @un, updateDate = @ud WHERE productKey = @pk AND optionKey = @ok",
                    new
                    {
                        @so = option.SortOrder,
                        @un = option.UseName,
                        @ud = DateTime.Now,
                        @pk = productKey,
                        @ok = option.Key
                    });

                if (option.Shared)
                {
                    foreach (var choice in option.Choices)
                    {
                        Database.Update<ProductOptionAttributeShareDto>(
                            "SET isDefaultChoice = @dfc WHERE productKey = @pk AND attributeKey = @ak AND optionKey = @ok",
                            new
                            {
                                @dfc = choice.IsDefaultChoice,
                                @pk = productKey,
                                @ak = choice.Key,
                                @ok = option.Key
                            });
                    }
                }
            }
        }


        /// <summary>
        /// Safely removes old shared options from a product.
        /// </summary>
        /// <param name="savers">
        /// The savers.
        /// </param>
        /// <param name="existing">
        /// The existing.
        /// </param>
        /// <param name="productKey">
        /// The product key.
        /// </param>
        private void SafeRemoveSharedOptionsFromProduct(IEnumerable<IProductOption> savers, IEnumerable<IProductOption> existing, Guid productKey)
        {
            var existingOptions = existing as IProductOption[] ?? existing.ToArray();
            var removers = existingOptions.Where(ex => savers.All(sv => sv.Key != ex.Key)).ToArray();
            if (removers.Any())
            {
                foreach (var rm in removers)
                {
                    if (rm.Shared)
                    {
                        //// shared options cannot be deleted from a product.  Instead useCount will be decremented
                        this.DeleteSharedProductOptionFromProduct(rm, productKey);
                    }
                    else
                    {
                        PersistDeletedItem(rm);
                    }
                }
            }

            // now check the selected choices for each of the savers
            foreach (var o in savers)
            {
                var current = existingOptions.FirstOrDefault(x => x.Key == o.Key);
                if (current == null) continue;

                var removeChoices = current.Choices.Where(x => o.Choices.All(oc => oc.Key != x.Key));
                foreach (var rm in removeChoices)
                {
                    foreach (var clause in GetRemoveAttributeFromSharedProductOptionSql(rm, productKey))
                    {
                        Database.Execute(clause);
                    }
                }

                if (!o.Shared) return;
                var newChoices = o.Choices.Where(x => current.Choices.All(cc => cc.Key != x.Key));
                var dtos = newChoices.Select(nc => new ProductOptionAttributeShareDto
                {
                    ProductKey = productKey,
                    AttributeKey = nc.Key,
                    OptionKey = o.Key,
                    IsDefaultChoice = nc.IsDefaultChoice,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now
                });
                foreach (var dto in dtos)
                {
                    Database.Insert(dto);
                }
            }
        }

        /// <summary>
        /// Removes attribute association from IProduct for a shared option.
        /// </summary>
        /// <param name="option">
        /// The option.
        /// </param>
        /// <param name="productKey">
        /// The product key.
        /// </param>
        /// <remarks>
        /// This affectively deletes the shared option from the product
        /// </remarks>
        private void DeleteSharedProductOptionFromProduct(IProductOption option, Guid productKey)
        {
            foreach (var clause in this.GetRemoveShareProductOptionFromProductSql(option, productKey))
            {
                Database.Execute(clause);
            }
        }

        /// <summary>
        /// Ensures the sort order of product options.
        /// </summary>
        /// <param name="options">
        /// The options.
        /// </param>
        private void EnsureProductOptionsSortOrder(ProductOptionCollection options)
        {
            if (!options.Any()) return;

            var sort = 1;
            var sorted = options.OrderBy(x => x.SortOrder);
            foreach (var option in sorted)
            {
                if (option.SortOrder != sort) option.SortOrder = sort;
                sort++;
            }
        }
    }
}
