namespace StarterKit.Service.Configuration
{
    using Autofac;
    using GreenPipes;
    using MassTransit;
    using MassTransit.AzureServiceBusTransport;
    using Module.Consumers;
    using System;
    using System.Collections.Generic;

    public class AzureServiceBusBusFactory : IBusFactory
    {
        private readonly IComponentContext _componentContext;

        public AzureServiceBusBusFactory(IComponentContext componentContext)
        {
            _componentContext = componentContext;
        }

        public IBusControl CreateBus()
        {
            return Bus.Factory.CreateUsingAzureServiceBus(cfg =>
            {
                var azureSbConnString = Environment.GetEnvironmentVariable("AzureSbConnectionString");
                var host = cfg.Host(azureSbConnString, h =>
                {
                });
                
                cfg.UseServiceBusMessageScheduler();

                // Add Receive Endpoint
                cfg.ReceiveEndpoint(host, "queue-dosomeworkservice", e =>
                {
                    e.Consumer<DoSomeWorkConsumer>(_componentContext);
                    e.PrefetchCount = 4 * Environment.ProcessorCount;
                });
            });
        }

        public IEnumerable<ValidationResult> Validate()
        {
            throw new NotImplementedException();
        }
    }
}
