namespace Merchello.Web.Discounts.Coupons.Constraints
{
    using System;

    using Merchello.Core;
    using Merchello.Core.Configuration;
    using Merchello.Core.Marketing.Offer;
    using Merchello.Core.Models;

    using Umbraco.Core;

    /// <summary>
    /// The specific customer constraint.
    /// </summary>
    [OfferComponent("C2BD8FB2-68F5-4DAB-A51E-F5C4461A5369", "Specific customer", "Limit this coupon to a specific customer.",
        "~/App_Plugins/Merchello/Backoffice/Merchello/Dialogs/marketing.offerconstraint.specificcustomer.html", typeof(Coupon))]
    public class SpecificCustomerConstraint : CouponConstraintBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpecificCustomerConstraint"/> class.
        /// </summary>
        /// <param name="definition">
        /// The definition.
        /// </param>
        public SpecificCustomerConstraint(OfferComponentDefinition definition)
            : base(definition)
        {
        }

        public override string DisplayConfigurationFormat
        {
            get
            {
                var customerKey = string.IsNullOrEmpty(GetConfigurationValue("customerKey")) ? Guid.Empty : new Guid(GetConfigurationValue("customerKey"));

                if (customerKey != Guid.Empty)
                {
                var customerService = MerchelloContext.Current.Services.CustomerService;
                var customer = customerService.GetByKey(customerKey);
                return string.Format("'Customer is {0} '", customer.FullName);  
                }

                return base.DisplayConfigurationFormat;
            }
        }

        /// <summary>
        /// Validates the constraint against the <see cref="ILineItemContainer"/>
        /// </summary>
        /// <param name="value">
        /// The value to object to which the constraint is to be applied.
        /// </param>
        /// <param name="customer">
        /// The <see cref="ICustomerBase"/>.
        /// </param>
        /// <returns>
        /// The <see cref="Attempt{ILineItemContainer}"/> indicating whether or not the constraint can be enforced.
        /// </returns>
        public override Attempt<ILineItemContainer> TryApply(ILineItemContainer value, ICustomerBase customer)
        {
            throw new System.NotImplementedException();
        }
    }
}