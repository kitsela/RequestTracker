using RequestTracker.Consumer.Extensions;
using RequestTracker.Consumer.Services;

var builder = Host.CreateDefaultBuilder(args);

builder.ConfigureServices((hostContext, services) =>
{
    var config = hostContext.Configuration;
    services.AddMassTransitPublisher(config);
    services.AddSingleton<IRequestInfoProcessor, MultiThreadLogsWriter>();
    services.AddSingleton<IDateTimeService, DateTimeService>();
});

var app = builder.Build();
app.Run();
