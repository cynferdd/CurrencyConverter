using ApplicationService;
using ApplicationService.Abstractions;
using DomainService;
using DomainService.Abstractions;
using Infrastructure;
using Infrastructure.Abstraction;
using Logger.Abstraction;
using Microsoft.Extensions.DependencyInjection;

namespace LuccaDevises
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = ConfigureServices();

            var serviceProvider = services.BuildServiceProvider();

            // calls the Run method in App, which is replacing Main
            serviceProvider.GetService<App>().Run(args);
        }

        private static IServiceCollection ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddScoped<ICurrencyService, CurrencyService>();
            services.AddScoped<IPathCalculator, PathCalculator>();
            services.AddScoped<IRateCalculator, RateCalculator>();
            services.AddScoped<IFileManager, FileManager>();
            services.AddScoped<IFileParser, FileParser>();
            services.AddScoped<IFileValidator, FileValidator>();
            services.AddScoped<ILogger, Logger.Logger>();

            // required to run the application
            services.AddTransient<App>();

            return services;
        }
    }
}
