using DomainService.Abstractions;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainService
{
    public class RateCalculator : IRateCalculator
    {

        public RateCalculator()
        {
            
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
            return Convert.ToInt32(Math.Round(convertedRate, 0));
        }
    }
}
