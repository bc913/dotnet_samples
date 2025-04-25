using Bcan.MyApp.Contracts;
using Bcan.MyApp.Services;
using Bcan.MyApp.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace Bcan.MyApp.Extensions;

public static class ServiceCollectionExtensions
{
    //https://github.com/AvaloniaUI/Avalonia/issues/10628
    // TODO: Better file location within the appdata
    public static void AddServices(this IServiceCollection collection)
    {
        collection.AddSingleton<ILoggerService>(new LoggerService());
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
