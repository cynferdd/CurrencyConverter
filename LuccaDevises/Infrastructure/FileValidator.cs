using Infrastructure.Abstraction;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Infrastructure
{
    public class FileValidator : IFileValidator
    {
        private readonly IList<string> lines;

        public FileValidator(IList<string> fileLines)
        {
            lines = fileLines;
        }

        public bool Validate()
        {
            return CheckHasAtLeast3Lines() &&
                CheckFirstLineContains3Fields() &&
                CheckFirstLineFieldsFormat() &&
                CheckSecondLineIsInt() &&
                CheckGoodAmountOfLines() &&
                CheckLastLinesFormat();
        }

        

        public bool CheckHasAtLeast3Lines()
        {
            return lines != null && lines.Count >= 3;
        }

        public bool CheckFirstLineContains3Fields()
        {
            return CheckLineContains3Fields(lines[0]);
        }

        private bool CheckLineContains3Fields(string line)
        {
            return line.Count(str => string.Equals(str, ';')) == 2;
        }
        public bool CheckFirstLineFieldsFormat()
        {
            var fields = lines[0].Split(";");
            return fields[0].Length == 3 &&
                Int32.TryParse(fields[1], out _) &&
                fields[2].Length == 3;
        }

        public bool CheckSecondLineIsInt()
        {
            return Int32.TryParse(lines[1], out _);
        }

        public bool CheckGoodAmountOfLines()
        {
            // we must have the first two lines + the amount defined in line 2
            int nbLines = Convert.ToInt32(lines[1]);
            return lines.Count == nbLines + 2;
        }

        public bool CheckLastLinesFormat()
        {
            bool isFormatOk = true;
            var exceptList = new List<string>();
            exceptList.Add(lines[0]);
            exceptList.Add(lines[1]);
            foreach (var line in lines.Except(exceptList))
            {
                isFormatOk = isFormatOk && CheckChangeFormat(line);
            }
            return isFormatOk;
        }

        public bool CheckChangeFormat(string line)
        {
            bool isFormatOk = CheckLineContains3Fields(line);
            if (isFormatOk)
            {
                var fields = line.Split(";");
                isFormatOk = fields[0].Length == 3 &&
                    fields[1].Length == 3 &&
                    fields[2].Count(str => string.Equals(str, '.')) == 1 &&
                    fields[2].Split(".")[1].Length == 4 &&
                    decimal.TryParse(fields[2], NumberStyles.Any, new CultureInfo("en-EN"), out _)
                ;
            }
            return isFormatOk;
        }
    }
}
