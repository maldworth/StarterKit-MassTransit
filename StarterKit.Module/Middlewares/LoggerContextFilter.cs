namespace StarterKit.Module.Middlewares
{
    using System;
    using System.Threading.Tasks;
    using GreenPipes;
    using Logging;
    using MassTransit;

    public class LoggerContextFilter<T> :
        IFilter<T>
        where T : class, MessageContext
    {
        private static readonly ILog Log = LogProvider.GetLogger(typeof(LoggerContextFilter<T>));

        public void Probe(ProbeContext context)
        {
            context.CreateFilterScope("loggerContext");
        }

        public async Task Send(T context, IPipe<T> next)
        {
            using (LogProvider.OpenMappedContext(nameof(context.ConversationId), context.ConversationId.GetValueOrDefault().ToString()))
            using (LogProvider.OpenMappedContext(nameof(context.CorrelationId), context.CorrelationId.GetValueOrDefault().ToString()))
            using (LogProvider.OpenMappedContext(nameof(context.InitiatorId), context.InitiatorId.GetValueOrDefault().ToString()))
            using (LogProvider.OpenMappedContext(nameof(context.MessageId), context.MessageId.GetValueOrDefault().ToString()))
            using (LogProvider.OpenMappedContext(nameof(context.RequestId), context.RequestId.GetValueOrDefault().ToString()))
            {
                try
                {
                    await next.Send(context).ConfigureAwait(false);
                }
                catch (Exception ex) when (LogExceptionMessage(ex)) // This ensures we don't unwind the stack to log the exception
                {
                    // This should never be entered
                    throw;
                }
            }
        }

        private static bool LogExceptionMessage(Exception e)
        {
            if (Log.IsDebugEnabled())
                Log.DebugFormat("{0}", e.ToString());
            return false;
        }
    }
}
