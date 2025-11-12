using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using InterviewApp.Services;
using Microsoft.Extensions.Http;
using MediatR;
using System.Reflection;
using InterviewApp.Requests;

class Program
{
    static async Task Main(string[] args)
    {
        using IHost host = Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, config) =>
            {
                config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            })
            .ConfigureServices((_, services) =>
            {
                services.AddTransient<IGreetingService, GreetingService>();
                services.AddTransient<ITimeGreetingService, TimeGreetingService>();
                services.AddHttpClient<ITranslationService, DeepLTranslationService>();

                services.AddMediatR(cfg =>
                {
                    cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                });
            })
            .Build();

       
        var mediator = host.Services.GetRequiredService<IMediator>();
        await mediator.Send(new InterviewApp.Requests.GreetUserCommand());
        await host.RunAsync();
    }
}