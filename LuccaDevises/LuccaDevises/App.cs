using ApplicationService.Abstractions;
using Logger.Abstraction;
using System;

namespace LuccaDevises
{
    public class App
    {
        private readonly ICurrencyService service;
        private readonly ILogger logger;
        public App(ICurrencyService currencyService, ILogger logger)
        {
            service = currencyService;
            this.logger = logger;
        }

        public void Run(string[] args)
        {
            if (args.Length == 0)
            {
                logger.FilePathNeeded();
                return;
            }
            int calculatedAmount = service.CalculateRate(args[0]);

            Console.WriteLine(calculatedAmount);
        }
    }
}
