namespace StarterKit.Module
{
    using System;
    using Logging;
    using MassTransit;
    using MassTransit.Audit;

    public class MyService
    {
        private static readonly ILog Log = LogProvider.GetLogger(typeof(MyService));

        private readonly IBusFactory _busFactory;
        private readonly IMessageAuditStore _auditStore;

        private IBusControl _busControl;

        public MyService(IBusFactory busFactory
            , IMessageAuditStore auditStore
        )
        {
            _busFactory = busFactory ?? throw new ArgumentNullException(nameof(busFactory));
            _auditStore = auditStore;
        }

        public bool Start()
        {
            Log.Info("Creating bus...");

            _busControl = _busFactory.CreateBus();

            _busControl.ConnectSendAuditObservers(_auditStore);
            _busControl.ConnectConsumeAuditObserver(_auditStore);

            Log.Info("Starting bus...");

            _busControl.Start();

            return true;
        }

        public bool Stop()
        {
            Log.Info("Stopping bus...");

            _busControl?.Stop();

            return true;
        }
    }
}
