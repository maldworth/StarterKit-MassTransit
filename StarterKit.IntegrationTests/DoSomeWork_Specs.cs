namespace StarterKit.IntegrationTests
{
    using MassTransit;
    using MassTransit.AzureServiceBusTransport;
    using StarterKit.Contracts;
    using System;
    using System.Threading.Tasks;
    using Xunit;

    public class DoSomeWork_Specs : IAsyncLifetime
    {
        private IPublishEndpoint _publishEndpoint;
        private BusHandle _busHandle;

        public async Task DisposeAsync()
        {
            await _busHandle?.StopAsync();
        }

        public async Task InitializeAsync()
        {
            var bus = Bus.Factory.CreateUsingAzureServiceBus(cfg =>
            {
                var azureSbConnString = "put in your key here";
                var host = cfg.Host(azureSbConnString, h =>
                {
                });
            });

            _busHandle = await bus.StartAsync();

            _publishEndpoint = bus;
        }

        [Fact]
        public async Task Should_publish_and_consumer_faults()
        {
            await _publishEndpoint.Publish<DoSomeWork>(new { Data1 = "First Bit of Data", Data2 = 4563, Data3 = new[] { "Apple", "Pear", "Orange" } });
        }

        [Fact]
        public async Task Should_publish_and_consumer_succeeds()
        {
            await _publishEndpoint.Publish<DoSomeWork>(new { Data1 = "Second Bit of Data", Data2 = 87, Data3 = new[] { "Apple", "Pear", "Orange" } });
        }
    }
}
