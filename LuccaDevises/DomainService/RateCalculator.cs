using DomainService.Abstractions;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;
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
                    convertedRate = Math.Round(convertedRate * item.Rate, 4);
                }
            }
            else
            {
                logger.NoChangesPathFound();
            }
            return Convert.ToInt32(Math.Round(convertedRate, 0));
        }
    }
}
