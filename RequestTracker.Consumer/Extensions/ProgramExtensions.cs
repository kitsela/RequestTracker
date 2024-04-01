using MassTransit;
using System.Reflection;

namespace RequestTracker.Consumer.Extensions
{
    public static class ProgramExtensions
    {
        public static IServiceCollection AddMassTransitPublisher(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(x =>
            {
                var entryAssembly = Assembly.GetExecutingAssembly();
                var host = configuration.GetValue<string>("RabbitMq");
                x.AddConsumers(entryAssembly);
                x.SetKebabCaseEndpointNameFormatter();
                x.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(host);
                    cfg.UseMessageRetry(retry => retry.Interval(3, TimeSpan.FromSeconds(5)));
                    cfg.ConfigureEndpoints(ctx, new KebabCaseEndpointNameFormatter("dev", false));
                });
            });
            return services;
        }
    }
}
