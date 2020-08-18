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
        private readonly IFileManager _fileManager;
        private readonly IPathCalculator _pathCalculator;
        private readonly IRateCalculator _rateCalculator;
        private readonly ILogger _logger;
        public CurrencyService(IFileManager manager, IPathCalculator pathCalculator, IRateCalculator rateCalculator, ILogger logger)
        {
            _fileManager = manager;
            _pathCalculator = pathCalculator;
            _rateCalculator = rateCalculator;
            _logger = logger;
        }
        public int ProcessConversion(string filePath)
        {
            BaseData data = _fileManager.GetData(filePath);
            if (data == null)
            {
                _logger.NoDataRetrievedFromFile();
                return 0;
            }

            IList<Change> conversionPath = _pathCalculator.GetConversionRatePath(data.ChangeRates, data.InitialCurrency, data.TargetCurrency);
            if (conversionPath == null)
            {
                _logger.NoConversionPathFound();
                return 0;
                
            }

            return _rateCalculator.ConvertAmount(data.Amount, conversionPath);
        }
    }
}
