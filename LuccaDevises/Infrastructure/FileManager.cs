using Domain;
using Infrastructure.Abstraction;
using Logger.Abstraction;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Infrastructure
{
    public class FileManager : IFileManager
    {
        private readonly IFileValidator validator;
        private readonly IFileParser parser;
        private readonly ILogger logger;
        public FileManager(IFileValidator fileValidator, IFileParser fileParser, ILogger logger)
        {
            validator = fileValidator;
            parser = fileParser;
            this.logger = logger;
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
                logger.FileNotFound();
                return null;
            }
            string[] lines = File.ReadAllLines(filePath);
            return lines?.ToList() ?? new List<string>();
        }
    }
}
