using System;

namespace Domain
{
    public class Change
    {
        
        public string SourceCurrency { get; private set; }

        public string TargetCurrency { get; private set; }

        private readonly decimal Rate;

        public Change(string source, string target, decimal rate)
        {
            SourceCurrency = source;
            TargetCurrency = target;
            Rate = rate;
        }

        public decimal Convert(decimal amount)
        {
            return Math.Round(amount * Rate, 4);
        }

        public Change Invert()
        {
            return new Change(TargetCurrency, SourceCurrency, Math.Round(1 / Rate, 4));
        }

        public override bool Equals(object obj)
        {
            return
                obj != null &&
                obj is Change change &&
                string.Equals(SourceCurrency, change.SourceCurrency) &&
                string.Equals(TargetCurrency, change.TargetCurrency) &&
                Rate == change.Rate;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + (SourceCurrency?.GetHashCode() ?? 0);
                hash = hash * 23 + (TargetCurrency?.GetHashCode() ?? 0);
                hash = hash * 23 + Rate.GetHashCode();
                return hash;
            }
        }
    }
}
