using ApplicationService.Abstractions;
using Domain;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ApplicationService.Test
{
    public class RateCalculatorTest
    {
        [Fact]
        public void ShouldGet59033yens_WhenGiven550Euros()
        {
            List<Change> listeChanges = new List<Change>();
            listeChanges.Add(new Change("EUR", "CHF", (decimal)1.2053));
            listeChanges.Add(new Change("CHF", "AUD", (decimal)1.0351));
            listeChanges.Add(new Change("AUD", "JPY", (decimal)86.0305));
            var mock = new Mock<IPathCalculator>();
            mock.Setup(pc => pc.Rates())
                .Returns(listeChanges);
            RateCalculator rc = new RateCalculator(mock.Object, 550);

            int calculatedAmount = rc.CalculateChangeRate();

            Assert.Equal(59033, calculatedAmount);

        }
    }
}
