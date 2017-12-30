namespace StarterKit.Service
{
    using Autofac;
    using StarterKit.Module;
    using StarterKit.Service.Configuration;
    using System;
    using System.Diagnostics;
    using Topshelf;
    using Topshelf.HostConfigurators;

    internal static class Program
    {
        static void Configure(HostConfigurator hostConfigurator)
        {
            hostConfigurator.Service<MyService>(s =>
            {
                s.ConstructUsing(() =>
                {
                    var builder = new ContainerBuilder();
                    builder.RegisterModule(new AutofacModule());

                    builder.RegisterType<TopshelfSerilogBootstrapper>()
                        .AutoActivate();

                    var container = builder.Build();

                    return container.Resolve<MyService>();
                });

                s.WhenStarted((service, control) => service.Start());
                s.WhenStopped((service, control) => service.Stop());
            });

            hostConfigurator.SetDisplayName("StarterKit.MassTransit.Service");
            hostConfigurator.SetServiceName("StarterKit.MassTransit.Service");
            hostConfigurator.SetDescription("StarterKit Masstransit");
        }

        static int Main()
        {
            try
            {
                return (int)HostFactory.Run(Configure);
            }
            catch (Exception e)
            {
                Trace.TraceError(e.ToString());
                throw;
            }
        }
    }
}
