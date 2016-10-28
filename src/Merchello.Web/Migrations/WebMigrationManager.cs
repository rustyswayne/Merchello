namespace Merchello.Web.Migrations
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;

    using LightInject;

    using Merchello.Core;
    using Merchello.Core.Boot;
    using Merchello.Core.Configuration;
    using Merchello.Core.Logging;
    using Merchello.Core.Persistence;
    using Merchello.Core.Persistence.Migrations;
    using Merchello.Core.Persistence.Migrations.Analytics;
    using Merchello.Core.Persistence.Migrations.Initial;

    using Newtonsoft.Json;

    using MigrationRecord = Merchello.Core.Models.Migrations.MigrationRecord;

    /// <summary>
    /// Represents a Migration Manager
    /// </summary>
    internal class WebMigrationManager : MigrationManagerBase
    {
        /// <summary>
        /// The post URL.
        /// </summary>
        private const string PostUrl = "http://instance.merchello.com/api/migration/Post";

        /// <summary>
        /// The record domain URL.
        /// </summary>
        private const string RecordDomainUrl = "http://instance.merchello.com/api/migration/RecordDomain";

        /// <summary>
        /// Initializes a new instance of the <see cref="WebMigrationManager"/> class.
        /// </summary>
        /// <param name="container">
        /// The <see cref="IServiceContainer"/>.
        /// </param>
        /// <param name="dbFactory">
        /// The <see cref="IDatabaseFactory"/>.
        /// </param>
        /// <param name="logger">
        /// The <see cref="ILogger"/>.
        /// </param>
        public WebMigrationManager(IServiceContainer container, IDatabaseFactory dbFactory, ILogger logger)
            : base(container, dbFactory, logger)
        {
        }


        /// <summary>
        /// Posts the migration analytic record.
        /// </summary>
        /// <param name="record">
        /// The record.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<HttpResponseMessage> PostAnalyticInfo(MigrationRecord record)
        {
            if (!MerchelloConfig.For.MerchelloSettings().EnableInstallTracking)
                return new HttpResponseMessage(HttpStatusCode.OK);

            // reset the domain analytic
            var storeSettingService = MerchelloContext.Current.Services.StoreSettingService;

            var setting = storeSettingService.GetByKey(Constants.StoreSetting.HasDomainRecordKey);
            if (setting != null)
            {
                setting.Value = false.ToString();
            }

            storeSettingService.Save(setting);
            

            var data = JsonConvert.SerializeObject(record);

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage responseMessage = null;
                try
                {
                    responseMessage = await client.PostAsync(PostUrl, new StringContent(data, Encoding.UTF8, "application/json"));
                }
                catch (Exception ex)
                {
                    if (responseMessage == null)
                    {
                        responseMessage = new HttpResponseMessage();
                    }

                    responseMessage.StatusCode = HttpStatusCode.InternalServerError;
                    responseMessage.ReasonPhrase = $"PostAnalyticInfo failed: {ex}";
                }

                return responseMessage;
            }
        }

        /// <summary>
        /// Posts a record of the domain.
        /// </summary>
        /// <param name="record">
        /// The record.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<HttpResponseMessage> PostDomainRecord(MigrationDomain record)
        {
            if (!MerchelloConfig.For.MerchelloSettings().EnableInstallTracking)
                return new HttpResponseMessage(HttpStatusCode.OK);

            var data = JsonConvert.SerializeObject(record);

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage responseMessage = null;
                try
                {
                    responseMessage = await client.PostAsync(RecordDomainUrl, new StringContent(data, Encoding.UTF8, "application/json"));
                }
                catch (Exception ex)
                {
                    if (responseMessage == null)
                    {
                        responseMessage = new HttpResponseMessage();
                    }

                    responseMessage.StatusCode = HttpStatusCode.InternalServerError;
                    responseMessage.ReasonPhrase = $"PostDomainRecord failed: {ex}";
                }

                return responseMessage;
            }
        }

        /// <inheritdoc/>
        protected override void ProcessMigrations()
        {
        }
    }
}