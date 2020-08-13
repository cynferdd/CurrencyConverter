﻿using Domain;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Infrastructure.Test
{
    public class FileParserTest
    {
        [Fact]
        public void ShouldGetBaseDataWithTwoChangeRatesAndCorrectData_WhenGiven4Lines()
        {
            List<string> lines = new List<string>() 
            {
                "eur;2;usd",
                "2",
                "eur;jpy;2.1234",
                "usd;jpy;3.1234",
            };
            FileParser fileParser = new FileParser(lines);

            BaseData data = fileParser.Parse();

            Assert.NotNull(data);
            Assert.Equal("eur", data.InitialCurrency);
            Assert.Equal(2, data.Amount);
            Assert.Equal("usd", data.TargetCurrency);
            Assert.Equal(2, data?.ChangeRates.Count);
        }
    }
}
