using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationService.Abstractions
{
    public interface IPathCalculator
    {
        IList<Change> Rates();
    }
}
