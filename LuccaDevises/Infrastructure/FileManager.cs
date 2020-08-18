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
        private readonly IFileValidator _validator;
        private readonly IFileParser _parser;
        private readonly ILogger _logger;
        public FileManager(IFileValidator fileValidator, IFileParser fileParser, ILogger logger)
        {
            _validator = fileValidator;
            _parser = fileParser;
            _logger = logger;
        }

        public BaseData GetData(string filePath)
        {
            BaseData baseData = null;
            IList<string> lines = Open(filePath);
            if (_validator.Validate(lines))
            {
                baseData = _parser.Parse(lines);
            }
            return baseData;
        }

        private IList<string> Open(string filePath)
        {
            if (!File.Exists(filePath))
            {
                _logger.FileNotFound();
                return null;
            }
            string[] lines = File.ReadAllLines(filePath);
            return lines?.ToList() ?? new List<string>();
        }
    }
}
