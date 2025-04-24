using Bcan.MyApp.Contracts;
using Bcan.MyApp.Services;
using Bcan.MyApp.ViewModels;
using Bcan.MyApp.Views;
using Microsoft.Extensions.DependencyInjection;

namespace Bcan.MyApp.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddServices(this IServiceCollection collection)
    {
        collection.AddSingleton<ILoggerService, LoggerService>();
        collection.AddSingleton<LogViewModel>();
        collection.AddSingleton<SampleViewModel>();
        
        collection.AddSingleton<SensorViewModel>(provider =>
        {
            var logService = provider.GetRequiredService<ILoggerService>();
            return new SensorViewModel(logService, "OsmanSensor");
        });
        
        collection.AddSingleton<MainViewModel>(provider => new MainViewModel(
            provider.GetRequiredService<LogViewModel>(), 
            provider.GetRequiredService<SampleViewModel>(),
            provider.GetRequiredService<SensorViewModel>()));
    }
}
