using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class BaseData
    {
        public string InitialCurrency { get; private set; }

        public decimal Amount { get; private set; }

        public string TargetCurrency { get; private set; }

        public IList<Change> ChangeRates { get; private set; }

        public BaseData(string initialCurrency, decimal amount, string targetCurrency, IList<Change> changeRates)
        {
            InitialCurrency = initialCurrency;
            Amount = amount;
            TargetCurrency = targetCurrency;
            ChangeRates = changeRates;
        }

    }
}
