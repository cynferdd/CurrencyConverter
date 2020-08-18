using System;

namespace Domain
{
    public class Change
    {
        
        public string SourceCurrency { get; private set; }

        public string TargetCurrency { get; private set; }

        private readonly decimal _rate;

        public Change(string sourceCurrency, string targetCurrency, decimal conversionRate)
        {
            SourceCurrency = sourceCurrency;
            TargetCurrency = targetCurrency;
            _rate = conversionRate;
        }

        public decimal Convert(decimal amount)
        {
            return Math.Round(amount * _rate, 4);
        }

        public Change Invert()
        {
            return new Change(TargetCurrency, SourceCurrency, Math.Round(1 / _rate, 4));
        }

        public override bool Equals(object obj)
        {
            return
                obj != null &&
                obj is Change change &&
                string.Equals(SourceCurrency, change.SourceCurrency) &&
                string.Equals(TargetCurrency, change.TargetCurrency) &&
                _rate == change._rate;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + (SourceCurrency?.GetHashCode() ?? 0);
                hash = hash * 23 + (TargetCurrency?.GetHashCode() ?? 0);
                hash = hash * 23 + _rate.GetHashCode();
                return hash;
            }
        }
    }
}
