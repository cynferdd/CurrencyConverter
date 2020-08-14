using System.Collections.Generic;

namespace Infrastructure.Abstraction
{
    public interface IFileValidator
    {
        bool Validate(IList<string> fileLines);
    }
}
