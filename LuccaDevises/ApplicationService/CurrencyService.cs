using ApplicationService.Abstractions;
using Domain;
using DomainService.Abstractions;
using Infrastructure.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationService
{
    public class CurrencyService : ICurrencyService
    {
        private readonly IFileManager fileManager;
        private readonly IPathCalculator pathCalculator;
        private readonly IRateCalculator rateCalculator;
        public CurrencyService(IFileManager manager, IPathCalculator pCalculator, IRateCalculator rCalculator)
        {
            fileManager = manager;
            pathCalculator = pCalculator;
            rateCalculator = rCalculator;
        }
        public int CalculateRate(string filePath)
        {
            int convertedAmount = 0;
            BaseData data = fileManager.GetData(filePath);
            if (data != null)
            {
                IList<Change> conversionPath = pathCalculator.Rates(data.ChangeRates, data.InitialCurrency, data.TargetCurrency);
                if (conversionPath != null)
                {
                    rateCalculator.CalculateChangeRate(data.Amount, conversionPath);
                }
            }
            
            return convertedAmount;
        }
    }
}
