using Domain;
using System.Collections.Generic;
using Xunit;

namespace DomainService.Test
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
            currencyList.Add(new Change("AUD", "CHF", 0.9661m));
            currencyList.Add(new Change("JPY", "KWU", 13.1151m));
            currencyList.Add(new Change("EUR", "CHF", 1.2053m));
            currencyList.Add(new Change("AUD", "JPY", 86.0305m));
            currencyList.Add(new Change("EUR", "USD", 1.2989m));
            currencyList.Add(new Change("JPY", "INR", 0.6571m));
            PathCalculator pc = new PathCalculator();

            IList<Change> rates = pc.Rates(currencyList, "EUR", "JPY");

            Assert.Equal(3, rates.Count);
            Assert.Equal("EUR", rates[0].SourceCurrency);
            Assert.Equal("CHF", rates[0].TargetCurrency);
            Assert.Equal(1.2053m, rates[0].Rate);
            Assert.Equal("CHF", rates[1].SourceCurrency);
            Assert.Equal("AUD", rates[1].TargetCurrency);
            Assert.Equal(1.0351m, rates[1].Rate);
            Assert.Equal("AUD", rates[2].SourceCurrency);
            Assert.Equal("JPY", rates[2].TargetCurrency);
            Assert.Equal(86.0305m, rates[2].Rate);

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
            currencyList.Add(new Change("AUD", "CHF", 0.9661m));
            currencyList.Add(new Change("JPY", "KWU", 13.1151m));
            currencyList.Add(new Change("EUR", "CHF", 1.2053m));
            currencyList.Add(new Change("AUD", "JPY", 86.0305m));
            currencyList.Add(new Change("EUR", "USD", 1.2989m));
            currencyList.Add(new Change("JPY", "INR", 0.6571m));
            currencyList.Add(new Change("EUR", "KWU", 1m));
            PathCalculator pc = new PathCalculator();

            IList<Change> rates = pc.Rates(currencyList, "EUR", "JPY");

            Assert.Equal(2, rates.Count);
            Assert.Equal("EUR", rates[0].SourceCurrency);
            Assert.Equal("KWU", rates[0].TargetCurrency);
            Assert.Equal("KWU", rates[1].SourceCurrency);
            Assert.Equal("JPY", rates[1].TargetCurrency);
            

        }

        [Fact]
        public void ShouldRetrieveNothing_WhenCurrencyListEmpty()
        {
            
            PathCalculator pc = new PathCalculator();

            IList<Change> rates = pc.Rates(new List<Change>(), "EUR", "JPY");

            Assert.Null(rates);
            
        }

        [Fact]
        public void ShouldRetrieveNothing_WhenInexistantTarget()
        {
            List<Change> currencyList = new List<Change>();
            currencyList.Add(new Change("AUD", "CHF", 0.9661m));
            currencyList.Add(new Change("JPY", "KWU", 13.1151m));
            currencyList.Add(new Change("EUR", "CHF", 1.2053m));
            currencyList.Add(new Change("AUD", "JPY", 86.0305m));
            currencyList.Add(new Change("EUR", "USD", 1.2989m));
            currencyList.Add(new Change("JPY", "INR", 0.6571m));
            PathCalculator pc = new PathCalculator();

            IList<Change> rates = pc.Rates(currencyList, "EUR", "RUB");

            Assert.Null(rates);

        }

        [Fact]
        public void ShouldRetrieveNothing_WhenInexistantSource()
        {
            List<Change> currencyList = new List<Change>();
            currencyList.Add(new Change("AUD", "CHF", 0.9661m));
            currencyList.Add(new Change("JPY", "KWU", 13.1151m));
            currencyList.Add(new Change("EUR", "CHF", 1.2053m));
            currencyList.Add(new Change("AUD", "JPY", 86.0305m));
            currencyList.Add(new Change("EUR", "USD", 1.2989m));
            currencyList.Add(new Change("JPY", "INR", 0.6571m));
            PathCalculator pc = new PathCalculator();

            IList<Change> rates = pc.Rates(currencyList, "RUB", "EUR");

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
            currencyList.Add(new Change("AUD", "CHF", 0.9661m));
            currencyList.Add(new Change("JPY", "KWU", 13.1151m));
            currencyList.Add(new Change("AUD", "JPY", 86.0305m));
            currencyList.Add(new Change("EUR", "USD", 1.2989m));
            currencyList.Add(new Change("JPY", "INR", 0.6571m));
            PathCalculator pc = new PathCalculator();

            IList<Change> rates = pc.Rates(currencyList, "AUD", "USD");

            Assert.Null(rates);

        }
    }
}
