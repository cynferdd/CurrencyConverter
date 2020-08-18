using Logger.Abstraction;
using System;

namespace Logger
{
    public class Logger : ILogger
    {
        public void FilePathNeeded()
        {
            Console.WriteLine("File path needed.");
        }

        public void FileNotFound()
        {
            Console.WriteLine("File not found.");
        }

        
        public void FirstLineWrongAmountOfFields()
        {
            Console.WriteLine("The first line needs to contain exactly 3 fields.");
        }

        public void FirstLineWrongFieldsFormat()
        {
            Console.WriteLine("Format is incorrect for the first line of the provided file.");
        }

        public void MinimumAmountOfLinesNotReached()
        {
            Console.WriteLine("The file must contain at least 3 lines.");
        }

        public void NoConversionPathFound()
        {
            Console.WriteLine("No path found from source currency to target currency.");
        }

        public void NoDataRetrievedFromFile()
        {
            Console.WriteLine("No data retrieved from file.");
        }

        public void SecondLineWrongNotPositiveInt()
        {
            Console.WriteLine("The second line of the file is not a positive integer.");
        }

        public void WrongAmountOfLines()
        {
            Console.WriteLine("The total amount of lines in the file is incorrect.");
            Console.WriteLine("Please check the number provided on line 2 or update the file accordingly.");
        }

        public void WrongChangeDataFormat()
        {
            Console.WriteLine("Some Change rate data is not formatted corectly.");
        }

        public void WrongDataLineFormat(string line)
        {
            Console.WriteLine("Wrong format on : " + line);
        }

        public void Write(string message)
        {
            Console.WriteLine(message);
        }
    }
}
