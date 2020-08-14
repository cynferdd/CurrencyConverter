using Domain;
using Infrastructure.Abstraction;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Infrastructure
{
    public class FileManager : IFileManager
    {
        private readonly IFileValidator validator;
        private readonly IFileParser parser;
        public FileManager(IFileValidator fileValidator, IFileParser fileParser)
        {
            validator = fileValidator;
            parser = fileParser;
        }

        public BaseData GetData(string filePath)
        {
            BaseData baseData = null;
            IList<string> lines = Open(filePath);
            if (validator.Validate(lines))
            {
                baseData = parser.Parse(lines);
            }
            return baseData;
        }

        private IList<string> Open(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("file not found : " + filePath);
            }
            string[] lines = File.ReadAllLines(filePath);
            return lines?.ToList() ?? new List<string>();
        }
    }
}
