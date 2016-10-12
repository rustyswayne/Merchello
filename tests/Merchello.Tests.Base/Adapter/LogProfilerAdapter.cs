namespace Merchello.Tests.Base.Adapter
{
    using System;

    using global::Umbraco.Core.Logging;

    using Merchello.Core;


    internal class LogProfilerAdapter : IProfiler
    {
        private readonly Core.Logging.IProfiler _profiler;


        public LogProfilerAdapter(Core.Logging.IProfiler profiler)
        {
            Ensure.ParameterNotNull(profiler, nameof(profiler));
            this._profiler = profiler;
        }

        public string Render()
        {
            return this._profiler.Render();
        }

        public IDisposable Step(string name)
        {
            return this._profiler.Step(name);
        }

        public void Start()
        {
            this._profiler.Start();
        }

        public void Stop(bool discardResults = false)
        {
            this._profiler.Stop(discardResults);
        }
    }
}