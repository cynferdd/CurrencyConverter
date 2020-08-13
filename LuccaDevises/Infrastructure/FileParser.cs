using Domain;
using Infrastructure.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
    public class FileParser : IFileParser
    {
        private readonly IList<string> lines;

        public BaseData Parse()
        {
            throw new NotImplementedException();
        }
    }
}
