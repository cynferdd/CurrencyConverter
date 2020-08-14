using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainService.Abstractions
{
    public interface IPathCalculator
    {
        IList<Change> Rates(List<Change> currencyPathes, string source, string target);
    }
}
