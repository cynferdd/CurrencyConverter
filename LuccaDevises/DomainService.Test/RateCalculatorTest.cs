using DomainService.Abstractions;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DomainService.Test
{
    public class RateCalculatorTest
    {
        [Fact]
        public void ShouldGet59033yens_WhenGiven550Euros()
        {
            List<Change> listeChanges = new List<Change>();
            listeChanges.Add(new Change("EUR", "CHF", 1.2053m));
            listeChanges.Add(new Change("CHF", "AUD", 1.0351m));
            listeChanges.Add(new Change("AUD", "JPY", 86.0305m));
            RateCalculator rc = new RateCalculator();

            int calculatedAmount = rc.CalculateChangeRate(550, listeChanges);

            Assert.Equal(59033, calculatedAmount);

        }

        [Fact]
        public void ShouldGetSameAmount_WhenGivenNoExchangeRate()
        {
            RateCalculator rc = new RateCalculator();

            int calculatedAmount = rc.CalculateChangeRate(550, new List<Change>());

            Assert.Equal(550, calculatedAmount);

        }

        [Fact]
        public void ShouldGetSameAmount_WhenGivenNullExchangeRate()
        {
            RateCalculator rc = new RateCalculator();

            int calculatedAmount = rc.CalculateChangeRate(550, null);

            Assert.Equal(550, calculatedAmount);

        }
    }
}
