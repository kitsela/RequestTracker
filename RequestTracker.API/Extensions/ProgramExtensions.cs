using MassTransit;

namespace RequestTracker.API.Extensions
{
    public static class ProgramExtensions
    {
        public static IServiceCollection AddMassTransitPublisher(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(x =>
            {
                x.SetKebabCaseEndpointNameFormatter();
                x.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(configuration.GetValue<string>("RabbitMq"));
                    cfg.UseMessageRetry(retry => retry.Interval(3, TimeSpan.FromSeconds(5)));
                    cfg.ConfigureEndpoints(ctx, new KebabCaseEndpointNameFormatter("dev", false));
                });
            });
            return services;
        }
    }
}
