namespace Domain
{
    public class Change
    {
        public string SourceCurrency { get; private set; }

        public string TargetCurrency { get; private set; }

        public decimal Rate { get; private set; }

        public Change(string source, string target, decimal rate)
        {
            SourceCurrency = source;
            TargetCurrency = target;
            Rate = rate;
        }
    }
}
