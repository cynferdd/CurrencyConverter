using Domain;
using Infrastructure.Abstraction;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Infrastructure
{
    public class FileParser : IFileParser
    {

        public FileParser()
        {

        }

        public BaseData Parse(IList<string> lines)
        {
            var firstFields = lines[0].Split(';');
            var nbDataLines = Convert.ToInt32(lines[1]);
            List<Change> data = new List<Change>();
            // we exclude the first 2 lines for the data parse
            for (int i = 2; i < nbDataLines + 2; i++)
            {
                var newFields = lines[i].Split(';');
                data.Add(
                    new Change(
                        newFields[0], 
                        newFields[1], 
                        Convert.ToDecimal(newFields[2], new CultureInfo("en-EN")
                        )));
            }

            return new BaseData(
                firstFields[0], 
                Convert.ToInt32(firstFields[1]), 
                firstFields[2], 
                data);
        }
    }
}
