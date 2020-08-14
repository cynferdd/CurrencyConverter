using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Abstraction
{
    public interface IFileValidator
    {
        bool Validate(IList<string> fileLines);
    }
}
