namespace ApplicationService.Abstractions
{
    public interface ICurrencyService
    {
        int CalculateRate(string filePath);
    }
}
