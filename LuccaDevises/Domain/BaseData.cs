using System.Collections.Generic;

namespace Domain
{
    public class BaseData
    {
        public string InitialCurrency { get; private set; }

        public int Amount { get; private set; }

        public string TargetCurrency { get; private set; }

        public IList<Change> ChangeRates { get; private set; }

        public BaseData(string initialCurrency, int amount, string targetCurrency, IList<Change> changeRates)
        {
            InitialCurrency = initialCurrency;
            Amount = amount;
            TargetCurrency = targetCurrency;
            ChangeRates = changeRates;
        }

    }
}
