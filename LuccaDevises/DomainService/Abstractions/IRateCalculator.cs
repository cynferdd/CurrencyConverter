using Domain;
using System.Collections.Generic;

namespace DomainService.Abstractions
{
    public interface IRateCalculator
    {
        int CalculateChangeRate(int amount, IList<Change> changes);
    }
}
