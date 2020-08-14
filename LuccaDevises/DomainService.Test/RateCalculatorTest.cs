using DomainService.Abstractions;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Logger.Abstraction;
using Moq;

namespace DomainService.Test
{
    public class RateCalculatorTest
    {
        [Fact]
        public void ShouldGet59033yens_WhenGiven550Euros()
        {
            var loggerMock = new Mock<ILogger>();
            List<Change> listeChanges = new List<Change>();
            listeChanges.Add(new Change("EUR", "CHF", 1.2053m));
            listeChanges.Add(new Change("CHF", "AUD", 1.0351m));
            listeChanges.Add(new Change("AUD", "JPY", 86.0305m));
            RateCalculator rc = new RateCalculator(loggerMock.Object);

            int calculatedAmount = rc.CalculateChangeRate(550, listeChanges);

            Assert.Equal(59033, calculatedAmount);

        }

        [Fact]
        public void ShouldGetSameAmount_WhenGivenNoExchangeRate()
        {
            var loggerMock = new Mock<ILogger>();
            RateCalculator rc = new RateCalculator(loggerMock.Object);

            int calculatedAmount = rc.CalculateChangeRate(550, new List<Change>());

            Assert.Equal(550, calculatedAmount);

        }

        [Fact]
        public void ShouldGetSameAmount_WhenGivenNullExchangeRate()
        {
            var loggerMock = new Mock<ILogger>();
            RateCalculator rc = new RateCalculator(loggerMock.Object);

            int calculatedAmount = rc.CalculateChangeRate(550, null);

            Assert.Equal(550, calculatedAmount);

        }
    }
}
