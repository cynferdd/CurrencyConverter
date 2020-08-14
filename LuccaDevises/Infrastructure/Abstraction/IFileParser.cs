using Domain;
using System.Collections.Generic;

namespace Infrastructure.Abstraction
{
    public interface IFileParser
    {
        BaseData Parse(IList<string> lines);
    }
}
