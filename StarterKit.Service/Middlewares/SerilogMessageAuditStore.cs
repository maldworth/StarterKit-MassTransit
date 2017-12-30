namespace StarterKit.Service.Middlewares
{
    using System.Threading.Tasks;
    using MassTransit.Audit;
    using Serilog;

    public class SerilogMessageAuditStore : IMessageAuditStore
    {
        private static readonly ILogger Log = Serilog.Log.ForContext<SerilogMessageAuditStore>();

        public Task StoreMessage<T>(T message, MessageAuditMetadata metadata) where T : class
        {
            // persist details of the call to durable logs DB
            Log.ForContext("MessageMetadata", metadata, true).Debug("Logged Message Metadata.");
            Log.ForContext("MessagePayload", message, true).Debug("Logged Message Payload.");

            return Task.FromResult(0);
        }
    }
}
