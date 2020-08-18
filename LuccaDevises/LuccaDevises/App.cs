using ApplicationService.Abstractions;
using Logger.Abstraction;
using System;

namespace LuccaDevises
{
    public class App
    {
        private readonly ICurrencyService _service;
        private readonly ILogger _logger;
        public App(ICurrencyService currencyService, ILogger logger)
        {
            _service = currencyService;
            _logger = logger;
        }

        public void Run(string[] args)
        {
            if (args.Length == 0)
            {
                _logger.FilePathNeeded();
                return;
            }

            int calculatedAmount;
            try
            {
                calculatedAmount = _service.ProcessConversion(args[0]);
            }
            catch (Exception e)
            {
                _logger.Write(e.Message);
                return;
            }

            _logger.Write(calculatedAmount.ToString());
        }
    }
}
