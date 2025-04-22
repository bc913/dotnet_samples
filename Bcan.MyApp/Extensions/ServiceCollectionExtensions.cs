using Bcan.MyApp.Contracts;
using Bcan.MyApp.Services;
using Bcan.MyApp.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace Bcan.MyApp.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddServices(this IServiceCollection collection)
    {
        collection.AddSingleton<ILoggerService, LoggerService>();
        collection.AddSingleton<LogViewModel>();
        collection.AddSingleton<SampleViewModel>();

        collection.AddSingleton<MainViewModel>();
    }
}
