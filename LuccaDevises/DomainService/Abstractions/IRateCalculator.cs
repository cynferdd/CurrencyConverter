using Domain;
using System.Collections.Generic;

namespace DomainService.Abstractions
{
    public interface IRateCalculator
    {
        int ConvertAmount(int amount, IList<Change> changes);
    }
}
