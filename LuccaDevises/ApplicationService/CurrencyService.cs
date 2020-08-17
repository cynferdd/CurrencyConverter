using ApplicationService.Abstractions;
using Domain;
using DomainService.Abstractions;
using Infrastructure.Abstraction;
using Logger.Abstraction;
using System.Collections.Generic;

namespace ApplicationService
{
    public class CurrencyService : ICurrencyService
    {
        private readonly IFileManager fileManager;
        private readonly IPathCalculator pathCalculator;
        private readonly IRateCalculator rateCalculator;
        private readonly ILogger logger;
        public CurrencyService(IFileManager manager, IPathCalculator pCalculator, IRateCalculator rCalculator, ILogger logger)
        {
            fileManager = manager;
            pathCalculator = pCalculator;
            rateCalculator = rCalculator;
            this.logger = logger;
        }
        public int CalculateRate(string filePath)
        {
            int convertedAmount = 0;
            BaseData data = fileManager.GetData(filePath);
            if (data != null)
            {
                IList<Change> conversionPath = pathCalculator.GetRatesPathes(data.ChangeRates, data.InitialCurrency, data.TargetCurrency);
                if (conversionPath != null)
                {
                    convertedAmount = rateCalculator.CalculateChangeRate(data.Amount, conversionPath);
                }
                else
                {
                    logger.NoChangesPathFound();
                }
            }
            else
            {
                logger.NoDataRetrievedFromFile();
            }
            
            return convertedAmount;
        }
    }
}
