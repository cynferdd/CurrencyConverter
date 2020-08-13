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
        private readonly string filePath;
        private readonly IFileValidator validator;
        private readonly IFileParser parser;
        public FileManager(string path, IFileValidator fileValidator, IFileParser fileParser)
        {
            filePath = path;
            validator = fileValidator;
            parser = fileParser;
        }

        public BaseData GetData()
        {
            BaseData baseData = null;
            IList<string> lines = Open();
            if (validator.Validate())
            {
                baseData = parser.Parse();
            }
            return baseData;
        }

        private IList<string> Open()
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
