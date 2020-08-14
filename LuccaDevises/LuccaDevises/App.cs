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
            int calculatedAmount = 0;

            if (args.Length == 0)
            {
                logger.FilePathNeeded();
                return;
            }
            
            try
            {
                service.CalculateRate(args[0]);
            }
            catch(Exception e)
            {
                logger.Write(e.Message);
                return;
            }

            logger.Write(calculatedAmount.ToString());
        }
    }
}
