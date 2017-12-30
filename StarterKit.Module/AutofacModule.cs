namespace StarterKit.Module
{
    using Autofac;

    public class AutofacModuleService : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            #region Service Bus Stuff
            // Should Only ever register one Service
            builder.RegisterType<MyService>();

            // Scan for Consumers
            builder.RegisterAssemblyTypes(typeof(AutofacModuleService).Assembly)
                .Where(t => t.Name.EndsWith("Consumer"))
                .AsImplementedInterfaces()
                .AsSelf();
            #endregion
        }
    }
}
