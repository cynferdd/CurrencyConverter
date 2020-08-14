using ApplicationService.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace LuccaDevises
{
    public class App
    {
        private readonly ICurrencyService service;
        public App(ICurrencyService currencyService)
        {
            service = currencyService;
        }
        public void Run(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("File Path Needed");
                return;
            }
            int calculatedAmount = service.CalculateRate(args[0]);

            Console.WriteLine(calculatedAmount);
        }
    }
}
