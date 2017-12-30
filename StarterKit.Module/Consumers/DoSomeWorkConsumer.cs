namespace StarterKit.Module.Consumers
{
    using System;
    using System.Threading.Tasks;
    using Logging;
    using MassTransit;
    using StarterKit.Contracts;

    public class DoSomeWorkConsumer : IConsumer<DoSomeWork>
    {
        private static readonly ILog Log = LogProvider.GetLogger(typeof(DoSomeWorkConsumer));

        public Task Consume(ConsumeContext<DoSomeWork> context)
        {
            //Log.Info($"AppSettings Queue Name: '{_appSettings.QueueName}'");

            // Do your work!!
            Log.Info($"Doing Work with Data1: '{context.Message.Data1}'");

            if (context.Message.Data2 > 100)
            {
                throw new ArgumentException("Too many reports!");
            }

            return Task.FromResult(0);
        }
    }
}
