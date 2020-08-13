using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationService.Abstractions
{
    public interface IRateCalculator
    {
        int CalculateChangeRate();
    }
}
