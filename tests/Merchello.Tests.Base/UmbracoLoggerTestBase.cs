﻿namespace Merchello.Tests.Base
{
    using System.IO;

    using global::Umbraco.Core.Logging;

    using Merchello.Tests.Base.Adapter;
    using Merchello.Umbraco.Adapters.Logging;

    using NUnit.Framework;

    public abstract class UmbracoLoggerTestBase
    {

        protected ILogger Logger { get; private set; }

        protected ProfilingLogger ProfileLogger { get; private set; }

        [OneTimeSetUp]
        public virtual void Initialize()
        {
            this.Logger = new Logger(new FileInfo(TestHelper.MapPathForTest("~/Config/log4net.config")));

            // Goofy way to get around internals
            var profiler = new Core.Acquired.Logging.LogProfiler(new LoggerAdapter(this.Logger));
            this.ProfileLogger = new ProfilingLogger(this.Logger, new LogProfilerAdapter(profiler));
        }

        [OneTimeTearDown]
        public virtual void TearDown()
        {
        }
    }
}