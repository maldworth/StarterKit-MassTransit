namespace StarterKit.Module.Middlewares
{
    using System.Collections.Generic;
    using GreenPipes;
    using MassTransit;

    public class LoggerContextSpecification<T> :
        IPipeSpecification<T>
        where T : class, MessageContext
    {
        public IEnumerable<ValidationResult> Validate()
        {
            yield break;
        }

        public void Apply(IPipeBuilder<T> builder)
        {
            builder.AddFilter(new LoggerContextFilter<T>());
        }
    }
}
