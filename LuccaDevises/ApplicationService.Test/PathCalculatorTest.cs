using Domain;
using System;
using System.Collections.Generic;
using Xunit;

namespace ApplicationService.Test
{
    public class PathCalculatorTest
    {
        /// <summary>
        /// example graph : 
        /// 
        /// AUD - CHF - EUR - USD
        ///  |
        /// JPY - KWU
        ///  |
        /// INR
        /// 
        /// </summary>
        [Fact]
        public void ShouldRetrieveEURCHFAUDJPY_WhenGivenExampleArray()
        {
            List<Change> currencyList = new List<Change>();
            currencyList.Add(new Change("AUD", "CHF", (decimal)0.9661));
            currencyList.Add(new Change("JPY", "KWU", (decimal)13.1151));
            currencyList.Add(new Change("EUR", "CHF", (decimal)1.2053));
            currencyList.Add(new Change("AUD", "JPY", (decimal)86.0305));
            currencyList.Add(new Change("EUR", "USD", (decimal)1.2989));
            currencyList.Add(new Change("JPY", "INR", (decimal)0.6571));
            PathCalculator pc = new PathCalculator(currencyList, "EUR", "JPY");

            IList<Change> rates = pc.Rates();

            Assert.Equal(3, rates.Count);
            Assert.Equal("EUR", rates[0].SourceCurrency);
            Assert.Equal("CHF", rates[0].TargetCurrency);
            Assert.Equal("CHF", rates[1].SourceCurrency);
            Assert.Equal("AUD", rates[1].TargetCurrency);
            Assert.Equal("AUD", rates[2].SourceCurrency);
            Assert.Equal("JPY", rates[2].TargetCurrency);

        }

        /// <summary>
        /// shortcut added : 
        /// 
        /// AUD - CHF - EUR - USD
        ///  |           |
        /// JPY - KWU ----
        ///  |
        /// INR
        /// 
        /// </summary>
        [Fact]
        public void ShouldRetrieveEURKWUJPY_WhenKWUShortcutAdded()
        {
            List<Change> currencyList = new List<Change>();
            currencyList.Add(new Change("AUD", "CHF", (decimal)0.9661));
            currencyList.Add(new Change("JPY", "KWU", (decimal)13.1151));
            currencyList.Add(new Change("EUR", "CHF", (decimal)1.2053));
            currencyList.Add(new Change("AUD", "JPY", (decimal)86.0305));
            currencyList.Add(new Change("EUR", "USD", (decimal)1.2989));
            currencyList.Add(new Change("JPY", "INR", (decimal)0.6571));
            currencyList.Add(new Change("EUR", "KWU", (decimal)1));
            PathCalculator pc = new PathCalculator(currencyList, "EUR", "JPY");

            IList<Change> rates = pc.Rates();

            Assert.Equal(2, rates.Count);
            Assert.Equal("EUR", rates[0].SourceCurrency);
            Assert.Equal("KWU", rates[0].TargetCurrency);
            Assert.Equal("KWU", rates[1].SourceCurrency);
            Assert.Equal("JPY", rates[1].TargetCurrency);
            

        }

        [Fact]
        public void ShouldRetrieveNothing_WhenCurrencyListEmpty()
        {
            
            PathCalculator pc = new PathCalculator(new List<Change>(), "EUR", "JPY");

            IList<Change> rates = pc.Rates();

            Assert.Null(rates);
            
        }

        [Fact]
        public void ShouldRetrieveNothing_WhenInexistantTarget()
        {
            List<Change> currencyList = new List<Change>();
            currencyList.Add(new Change("AUD", "CHF", (decimal)0.9661));
            currencyList.Add(new Change("JPY", "KWU", (decimal)13.1151));
            currencyList.Add(new Change("EUR", "CHF", (decimal)1.2053));
            currencyList.Add(new Change("AUD", "JPY", (decimal)86.0305));
            currencyList.Add(new Change("EUR", "USD", (decimal)1.2989));
            currencyList.Add(new Change("JPY", "INR", (decimal)0.6571));
            PathCalculator pc = new PathCalculator(currencyList, "EUR", "RUB");

            IList<Change> rates = pc.Rates();

            Assert.Null(rates);

        }

        [Fact]
        public void ShouldRetrieveNothing_WhenInexistantSource()
        {
            List<Change> currencyList = new List<Change>();
            currencyList.Add(new Change("AUD", "CHF", (decimal)0.9661));
            currencyList.Add(new Change("JPY", "KWU", (decimal)13.1151));
            currencyList.Add(new Change("EUR", "CHF", (decimal)1.2053));
            currencyList.Add(new Change("AUD", "JPY", (decimal)86.0305));
            currencyList.Add(new Change("EUR", "USD", (decimal)1.2989));
            currencyList.Add(new Change("JPY", "INR", (decimal)0.6571));
            PathCalculator pc = new PathCalculator(currencyList, "RUB", "EUR");

            IList<Change> rates = pc.Rates();

            Assert.Null(rates);

        }

        /// <summary>
        /// graph with a cut path : 
        /// 
        /// AUD - CHF X EUR - USD
        ///  |
        /// JPY - KWU
        ///  |
        /// INR
        /// 
        /// </summary>
        [Fact]
        public void ShouldRetrieveNothing_WhenInexistantPath()
        {
            List<Change> currencyList = new List<Change>();
            currencyList.Add(new Change("AUD", "CHF", (decimal)0.9661));
            currencyList.Add(new Change("JPY", "KWU", (decimal)13.1151));
            currencyList.Add(new Change("AUD", "JPY", (decimal)86.0305));
            currencyList.Add(new Change("EUR", "USD", (decimal)1.2989));
            currencyList.Add(new Change("JPY", "INR", (decimal)0.6571));
            PathCalculator pc = new PathCalculator(currencyList, "AUD", "USD");

            IList<Change> rates = pc.Rates();

            Assert.Null(rates);

        }
    }
}
