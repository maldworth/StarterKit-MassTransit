namespace StarterKit.Service
{
    using Autofac;
    using Configuration;
    using Middlewares;
    using StarterKit.Module;

    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Should Only ever register one Emailer
            builder.RegisterModule(new AutofacModuleService());

            builder.RegisterType<SerilogMessageAuditStore>()
                .AsImplementedInterfaces();

            // Bus Transport Specific Factory Registration
            builder.RegisterType<AzureServiceBusBusFactory>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
