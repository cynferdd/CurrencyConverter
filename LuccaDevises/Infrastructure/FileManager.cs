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
        public FileManager(string path)
        {
            filePath = path;
        }

        public IList<string> Open()
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
