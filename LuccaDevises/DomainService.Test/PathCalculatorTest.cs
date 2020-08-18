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
            Change expectedFirstChange = new Change("EUR", "CHF", 1.2053m);
            Change expectedSecondChange = new Change("CHF", "AUD", 1.0351m);
            Change expectedThirdChange = new Change("AUD", "JPY", 86.0305m);
            List<Change> currencyList = new List<Change>();
            currencyList.Add(new Change("AUD", "CHF", 0.9661m));
            currencyList.Add(new Change("JPY", "KWU", 13.1151m));
            currencyList.Add(new Change("EUR", "CHF", 1.2053m));
            currencyList.Add(new Change("AUD", "JPY", 86.0305m));
            currencyList.Add(new Change("EUR", "USD", 1.2989m));
            currencyList.Add(new Change("JPY", "INR", 0.6571m));
            PathCalculator pc = new PathCalculator();

            IList<Change> rates = pc.GetConversionRatePath(currencyList, "EUR", "JPY");

            Assert.Equal(3, rates.Count);
            Assert.Equal(expectedFirstChange, rates[0]);
            Assert.Equal(expectedSecondChange, rates[1]);
            Assert.Equal(expectedThirdChange, rates[2]);

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
            Change expectedFirstChange = new Change("EUR", "KWU", 1m);
            Change expectedSecondChange = new Change("KWU", "JPY", 0.0762m);
            List<Change> currencyList = new List<Change>();
            currencyList.Add(new Change("AUD", "CHF", 0.9661m));
            currencyList.Add(new Change("JPY", "KWU", 13.1151m));
            currencyList.Add(new Change("EUR", "CHF", 1.2053m));
            currencyList.Add(new Change("AUD", "JPY", 86.0305m));
            currencyList.Add(new Change("EUR", "USD", 1.2989m));
            currencyList.Add(new Change("JPY", "INR", 0.6571m));
            currencyList.Add(new Change("EUR", "KWU", 1m));
            PathCalculator pc = new PathCalculator();

            IList<Change> rates = pc.GetConversionRatePath(currencyList, "EUR", "JPY");

            Assert.Equal(2, rates.Count);
            Assert.Equal(expectedFirstChange, rates[0]);
            Assert.Equal(expectedSecondChange, rates[1]);


        }

        [Fact]
        public void ShouldRetrieveNothing_WhenCurrencyListEmpty()
        {
            
            PathCalculator pc = new PathCalculator();

            IList<Change> rates = pc.GetConversionRatePath(new List<Change>(), "EUR", "JPY");

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

            IList<Change> rates = pc.GetConversionRatePath(currencyList, "EUR", "RUB");

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

            IList<Change> rates = pc.GetConversionRatePath(currencyList, "RUB", "EUR");

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

            IList<Change> rates = pc.GetConversionRatePath(currencyList, "AUD", "USD");

            Assert.Null(rates);

        }
    }
}
