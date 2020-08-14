using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainService.Abstractions
{
    public interface IRateCalculator
    {
        int CalculateChangeRate(int amount, IList<Change> changes);
    }
}
