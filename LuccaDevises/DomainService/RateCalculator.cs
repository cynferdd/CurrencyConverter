using DomainService.Abstractions;
using Domain;
using System;
using System.Collections.Generic;
using Logger.Abstraction;

namespace DomainService
{
    public class RateCalculator : IRateCalculator
    {
        private readonly ILogger logger;
        public RateCalculator(ILogger logger)
        {
            this.logger = logger;
        }

        public int CalculateChangeRate(int amount, IList<Change> changes)
        {
            decimal convertedRate = amount;
            if (changes != null)
            {
                foreach (var item in changes)
                {
                    convertedRate = item?.Convert(convertedRate) ?? convertedRate;
                }
            }
            else
            {
                logger.NoConversionPathFound();
            }
            return Convert.ToInt32(Math.Round(convertedRate, 0));
        }
    }
}
