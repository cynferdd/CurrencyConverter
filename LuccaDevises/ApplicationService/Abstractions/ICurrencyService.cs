using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationService.Abstractions
{
    public interface ICurrencyService
    {
        int CalculateRate(string filePath);
    }
}
