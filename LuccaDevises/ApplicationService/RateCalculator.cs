using ApplicationService.Abstractions;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationService
{
    public class RateCalculator : IRateCalculator
    {

        private readonly IList<Change> changes;
        private readonly int baseAmount;
        public RateCalculator(IPathCalculator pathCalculator, int amount)
        {
            changes = pathCalculator.Rates();
            baseAmount = amount;
        }
        public int CalculateChangeRate()
        {
            decimal convertedRate = baseAmount;
            if (changes != null)
            {
                foreach (var item in changes)
                {
                    convertedRate = Math.Round(convertedRate * item.Rate, 4);
                }
            }
            return Convert.ToInt32(Math.Round(convertedRate, 0));
        }
    }
}
