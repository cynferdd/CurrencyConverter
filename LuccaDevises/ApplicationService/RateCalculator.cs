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
        
        public RateCalculator(IPathCalculator pathCalculator)
        {
            changes = pathCalculator.Rates();
            
        }
        public int CalculateChangeRate(int amount)
        {
            decimal convertedRate = amount;
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
