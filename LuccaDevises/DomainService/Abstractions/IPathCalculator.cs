using Domain;
using System.Collections.Generic;

namespace DomainService.Abstractions
{
    public interface IPathCalculator
    {
        IList<Change> GetRatesPathes(IList<Change> currencyPathes, string source, string target);
    }
}
