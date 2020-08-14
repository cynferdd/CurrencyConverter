using Infrastructure.Abstraction;
using Logger.Abstraction;
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
        private readonly ILogger logger;
        public FileValidator(ILogger logger)
        {
            this.logger = logger;
        }

        public bool Validate(IList<string> fileLines)
        {

            return CheckHasAtLeast3Lines(fileLines) &&
                CheckFirstLineContains3Fields(fileLines) &&
                CheckFirstLineFieldsFormat(fileLines) &&
                CheckSecondLineIsPositiveInt(fileLines) &&
                CheckGoodAmountOfLines(fileLines) &&
                CheckLastLinesFormat(fileLines);
        }

        

        public bool CheckHasAtLeast3Lines(IList<string> lines)
        {
            bool isValid = (lines != null && lines.Count >= 3);
            if(!isValid)
            {
                logger.InsufficiantAmountOfLines();
            }
            return isValid;
        }

        public bool CheckFirstLineContains3Fields(IList<string> lines)
        {
            bool isValid = CheckLineContains3Fields(lines[0]);
            if (!isValid)
            {
                logger.FirstLineWrongAmountOfFields();
            }
            return isValid;
        }

        private bool CheckLineContains3Fields(string line)
        {
            return line.Count(str => string.Equals(str, ';')) == 2;
        }

        public bool CheckFirstLineFieldsFormat(IList<string> lines)
        {
             
            var fields = lines[0].Split(";");

            bool isValid = fields[0].Length == 3 &&
                Int32.TryParse(fields[1], out _) &&
                fields[2].Length == 3;

            if (!isValid)
            {
                logger.FirstLineWrongFieldsFormat();
            }

            return isValid;
        }

        public bool CheckSecondLineIsPositiveInt(IList<string> lines)
        {
            bool isValid = Int32.TryParse(lines[1], out int resultedInt) && resultedInt > 0;
            if (!isValid)
            {
                logger.SecondLineWrongNotPositiveInt();
            }
            return isValid;
        }

        public bool CheckGoodAmountOfLines(IList<string> lines)
        {
            // we must have the first two lines + the amount defined in line 2
            int nbLines = Convert.ToInt32(lines[1]);
            bool isValid = (lines.Count == nbLines + 2);
            if (!isValid)
            {
                logger.WrongAmountOfLines();
            }
            return isValid;
        }

        public bool CheckLastLinesFormat(IList<string> lines)
        {
            bool isFormatOk = true;
            var exceptList = new List<string>();
            exceptList.Add(lines[0]);
            exceptList.Add(lines[1]);
            foreach (var line in lines.Except(exceptList))
            {
                isFormatOk = isFormatOk && CheckChangeFormat(line);
            }
            if (!isFormatOk)
            {
                logger.WrongChangeDataFormat();
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

            if (!isFormatOk)
            {
                logger.WrongDataLineFormat(line);
            }
            return isFormatOk;
        }
    }
}
