using DomainService.Abstractions;
using Domain;
using System;
using System.Collections.Generic;
using Logger.Abstraction;

namespace DomainService
{
    public class RateCalculator : IRateCalculator
    {
        private readonly ILogger _logger;
        public RateCalculator(ILogger logger)
        {
            _logger = logger;
        }

        public int ConvertAmount(int amount, IList<Change> changes)
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
                _logger.NoConversionPathFound();
            }
            return Convert.ToInt32(Math.Round(convertedRate, 0));
        }

    }
}
