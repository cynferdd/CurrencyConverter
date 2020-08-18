using Infrastructure.Abstraction;
using Logger.Abstraction;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Infrastructure
{
    public class FileValidator : IFileValidator
    {
        private readonly ILogger _logger;
        public FileValidator(ILogger logger)
        {
            _logger = logger;
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

        
        /// <summary>
        /// file must have at least 3 lines to be valid. 
        /// First two are mandatory, third is the first data line.
        /// </summary>
        /// <param name="lines">text file lines</param>
        /// <returns>is it valid ?</returns>
        public bool CheckHasAtLeast3Lines(IList<string> lines)
        {
            bool isValid = (lines != null && lines.Count >= 3);
            if(!isValid)
            {
                _logger.MinimumAmountOfLinesNotReached();
            }
            return isValid;
        }

        /// <summary>
        /// First line must contain 3 fields
        /// </summary>
        /// <param name="lines">text file lines</param>
        /// <returns>is it valid ?</returns>
        public bool CheckFirstLineContains3Fields(IList<string> lines)
        {
            bool isValid = CheckLineContains3Fields(lines[0]);
            if (!isValid)
            {
                _logger.FirstLineWrongAmountOfFields();
            }
            return isValid;
        }

        /// <summary>
        /// generic method when lines must contain 3 fields
        /// </summary>
        /// <param name="line"></param>
        /// <returns>is it valid ?</returns>
        private bool CheckLineContains3Fields(string line)
        {
            return line.Count(str => string.Equals(str, ';')) == 2;
        }

        /// <summary>
        /// First line format must be : 
        /// CUR;amount(integer);CUR
        /// </summary>
        /// <param name="lines">text file lines</param>
        /// <returns>is it valid ?</returns>
        public bool CheckFirstLineFieldsFormat(IList<string> lines)
        {
             
            var fields = lines[0].Split(";");

            bool isValid = 
                fields[0].Length == 3 &&
                Int32.TryParse(fields[1], out _) &&
                fields[2].Length == 3;

            if (!isValid)
            {
                _logger.FirstLineWrongFieldsFormat();
            }

            return isValid;
        }

        /// <summary>
        /// Second line must be a positive integer
        /// </summary>
        /// <param name="lines">text file lines</param>
        /// <returns>is it valid ?</returns>
        public bool CheckSecondLineIsPositiveInt(IList<string> lines)
        {
            bool isValid = Int32.TryParse(lines[1], out int resultedInt) && resultedInt > 0;
            if (!isValid)
            {
                _logger.SecondLineWrongNotPositiveInt();
            }
            return isValid;
        }

        /// <summary>
        /// the total amount of lines must the amount defined in line 2 + the First two mandatory lines
        /// </summary>
        /// <param name="lines">text file lines</param>
        /// <returns>is it valid ?</returns>
        public bool CheckGoodAmountOfLines(IList<string> lines)
        {
            int nbLines = Convert.ToInt32(lines[1]);
            bool isValid = (lines.Count == nbLines + 2);
            if (!isValid)
            {
                _logger.WrongAmountOfLines();
            }
            return isValid;
        }

        /// <summary>
        /// global method verifying if all data lines are correctly formatted
        /// line 1 and 2 are excluded from this check.
        /// </summary>
        /// <param name="lines">text file lines</param>
        /// <returns>is it valid ?</returns>
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
                _logger.WrongChangeDataFormat();
            }
            return isFormatOk;
        }

        /// <summary>
        /// A data line must be formatted as such : 
        /// CUR;CUR;decimal(formatted like 0.0000)
        /// </summary>
        /// <param name="line"></param>
        /// <returns>is it valid ?</returns>
        public bool CheckChangeFormat(string line)
        {
            bool isFormatOk = CheckLineContains3Fields(line);
            if (isFormatOk)
            {
                var fields = line.Split(";");
                isFormatOk = 
                    fields[0].Length == 3 &&
                    fields[1].Length == 3 &&
                    fields[2].Count(str => string.Equals(str, '.')) == 1 &&
                    fields[2].Split(".")[1].Length == 4 &&
                    decimal.TryParse(fields[2], NumberStyles.Any, new CultureInfo("en-EN"), out _)
                ;
            }

            if (!isFormatOk)
            {
                _logger.WrongDataLineFormat(line);
            }
            return isFormatOk;
        }
    }
}
