﻿using HandlebarsDotNet;
using Quartz;
using Quartzmon.Helpers;

namespace Quartzmon
{
    public class Services
    {
        internal const string ContextKey = "quartzmon.services";

        public QuartzmonOptions Options { get; set; }

        public ViewEngine ViewEngine { get; set; }

        public IHandlebars Handlebars { get; set; }

        public TypeHandlerService TypeHandlers { get; set; }

        public IScheduler Scheduler { get; set; }

        internal Cache Cache { get; private set; }

        public static Services Create(QuartzmonOptions options)
        {
            var handlebarsConfiguration = new HandlebarsConfiguration()
            {
                FileSystem = ViewFileSystemFactory.Create(options),
                ThrowOnUnresolvedBindingExpression = true,
            };

            var services = new Services()
            {
                Options = options,
                Scheduler = options.Scheduler,
                Handlebars = HandlebarsDotNet.Handlebars.Create(handlebarsConfiguration),
            };

            HandlebarsHelpers.Register(services);

            services.ViewEngine = new ViewEngine(services);
            services.TypeHandlers = new TypeHandlerService(services);
            services.Cache = new Cache(services);

            return services;
        }
    }
}
