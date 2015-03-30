namespace Merchello.Tests.Base.TestHelpers
{
    using System;
    using System.Collections.Generic;
    using System.Configuration.Provider;
    using System.Linq;
    using System.Web.Configuration;
    using System.Xml.Linq;

    using global::Examine;
    using global::Examine.Config;
    using global::Examine.Providers;
    using global::Examine.SearchCriteria;

    ///<summary>
    /// Exposes searchers and indexers
    ///</summary>
    public class ExamineManagerTest : ISearcher, IIndexer
    {

        private ExamineManagerTest()
        {
            this.LoadProviders();
        }

        /// <summary>
        /// Singleton
        /// </summary>
        public static ExamineManagerTest Instance
        {
            get
            {
                return ManagerTest;
            }
        }

        private static readonly ExamineManagerTest ManagerTest = new ExamineManagerTest();

        private readonly object _lock = new object();

        ///<summary>
        /// Returns the default search provider
        ///</summary>
        public BaseSearchProvider DefaultSearchProvider { get; private set; }

        /// <summary>
        /// Returns the collection of searchers
        /// </summary>
        public SearchProviderCollection SearchProviderCollection { get; private set; }

        /// <summary>
        /// Return the colleciton of indexers
        /// </summary>
        public IndexProviderCollection IndexProviderCollection { get; private set; }

        private volatile bool _providersInit = false;

        private void LoadProviders()
        {
            if (!this._providersInit)
            {
                lock (this._lock)
                {
                    // Do this again to make sure _provider is still null
                    if (!this._providersInit)
                    {
                        // Load registered providers and point _provider to the default provider	

                        this.IndexProviderCollection = new IndexProviderCollection();
                        ProvidersHelper.InstantiateProviders(ExamineSettings.Instance.IndexProviders.Providers, this.IndexProviderCollection, typeof(BaseIndexProvider));

                        this.SearchProviderCollection = new SearchProviderCollection();
                        ProvidersHelper.InstantiateProviders(ExamineSettings.Instance.SearchProviders.Providers, this.SearchProviderCollection, typeof(BaseSearchProvider));

                        //set the default
                        if (!string.IsNullOrEmpty(ExamineSettings.Instance.SearchProviders.DefaultProvider))
                            this.DefaultSearchProvider = this.SearchProviderCollection[ExamineSettings.Instance.SearchProviders.DefaultProvider];

                        if (this.DefaultSearchProvider == null)
                            throw new ProviderException("Unable to load default search provider");

                        this._providersInit = true;


                        //check if we need to rebuild on startup
                        if (ExamineSettings.Instance.RebuildOnAppStart)
                        {
                            foreach (var index in this.IndexProviderCollection.Cast<IIndexer>())
                            {
                                if (!index.IndexExists())
                                {
                                    index.RebuildIndex();
                                }
                            }
                        }

                    }
                }
            }
        }


        #region ISearcher Members

        /// <summary>
        /// Uses the default provider specified to search
        /// </summary>
        /// <param name="searchParameters"></param>
        /// <returns></returns>
        /// <remarks>This is just a wrapper for the default provider</remarks>
        public ISearchResults Search(ISearchCriteria searchParameters)
        {
            return this.DefaultSearchProvider.Search(searchParameters);
        }

        /// <summary>
        /// Uses the default provider specified to search
        /// </summary>
        /// <param name="searchText"></param>
        /// <param name="maxResults"></param>
        /// <param name="useWildcards"></param>
        /// <returns></returns>
        public ISearchResults Search(string searchText, bool useWildcards)
        {
            return this.DefaultSearchProvider.Search(searchText, useWildcards);
        }


        #endregion

        /// <summary>
        /// Reindex nodes for the providers specified
        /// </summary>
        /// <param name="node"></param>
        /// <param name="type"></param>
        /// <param name="providers"></param>
        public void ReIndexNode(XElement node, string type, IEnumerable<BaseIndexProvider> providers)
        {
            this._ReIndexNode(node, type, providers);
        }

        /// <summary>
        /// Deletes index for node for the specified providers
        /// </summary>
        /// <param name="nodeId"></param>
        /// <param name="providers"></param>
        public void DeleteFromIndex(string nodeId, IEnumerable<BaseIndexProvider> providers)
        {
            this._DeleteFromIndex(nodeId, providers);
        }

        #region IIndexer Members

        /// <summary>
        /// Reindex nodes for all providers
        /// </summary>
        /// <param name="node"></param>
        /// <param name="type"></param>
        public void ReIndexNode(XElement node, string type)
        {
            this._ReIndexNode(node, type, this.IndexProviderCollection);
        }
        private void _ReIndexNode(XElement node, string type, IEnumerable<BaseIndexProvider> providers)
        {
            foreach (var provider in providers)
            {
                provider.ReIndexNode(node, type);
            }
        }

        /// <summary>
        /// Deletes index for node for all providers
        /// </summary>
        /// <param name="nodeId"></param>
        public void DeleteFromIndex(string nodeId)
        {
            this._DeleteFromIndex(nodeId, this.IndexProviderCollection);
        }
        private void _DeleteFromIndex(string nodeId, IEnumerable<BaseIndexProvider> providers)
        {
            foreach (var provider in providers)
            {
                provider.DeleteFromIndex(nodeId);
            }
        }

        public void IndexAll(string type)
        {
            this._IndexAll(type);
        }
        private void _IndexAll(string type)
        {
            foreach (BaseIndexProvider provider in this.IndexProviderCollection)
            {
                provider.IndexAll(type);
            }
        }

        public void RebuildIndex()
        {
            this._RebuildIndex();
        }
        private void _RebuildIndex()
        {
            foreach (BaseIndexProvider provider in this.IndexProviderCollection)
            {
                provider.RebuildIndex();
            }
        }

        public IIndexCriteria IndexerData
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public bool IndexExists()
        {
            throw new NotImplementedException();
        }

        #endregion


        #region ISearcher Members

        /// <summary>
        /// Creates search criteria that defaults to IndexType.Any and BooleanOperation.And
        /// </summary>
        /// <returns></returns>
        public ISearchCriteria CreateSearchCriteria()
        {
            return this.CreateSearchCriteria(string.Empty, BooleanOperation.And);
        }

        public ISearchCriteria CreateSearchCriteria(string type)
        {
            return this.CreateSearchCriteria(type, BooleanOperation.And);
        }

        public ISearchCriteria CreateSearchCriteria(BooleanOperation defaultOperation)
        {
            return this.CreateSearchCriteria(string.Empty, defaultOperation);
        }

        public ISearchCriteria CreateSearchCriteria(string type, BooleanOperation defaultOperation)
        {
            return this.DefaultSearchProvider.CreateSearchCriteria(type, defaultOperation);
        }

        #endregion
    }
}
