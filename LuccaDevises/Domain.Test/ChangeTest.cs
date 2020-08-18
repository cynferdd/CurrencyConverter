using System;
using Xunit;

namespace Domain.Test
{
    public class ChangeTest
    {
        [Fact]
        public void ShouldGetCorrectResult_WhenGivenStandardBaseAmountAndChangeRate()
        {
            decimal expectedResult = 7.5m;
            Change change = new Change("EUR", "USD", 2.5m);

            decimal convertedAmount = change.Convert(3m);

            Assert.Equal(expectedResult, convertedAmount);
            Assert.Equal(expectedResult.GetHashCode(), convertedAmount.GetHashCode());
        }

        [Fact]
        public void ShouldGetZero_WhenGivenZeroBaseAmount()
        {
            decimal expectedResult = 0m;
            Change change = new Change("EUR", "USD", 2.5m);

            decimal convertedAmount = change.Convert(0m);

            Assert.Equal(expectedResult, convertedAmount);
            Assert.Equal(expectedResult.GetHashCode(), convertedAmount.GetHashCode());
        }

        [Fact]
        public void ShouldGetZero_WhenGivenZeroChangeRate()
        {
            decimal expectedResult = 0m;
            Change change = new Change("EUR", "USD", 0m);

            decimal convertedAmount = change.Convert(3m);

            Assert.Equal(expectedResult, convertedAmount);
            Assert.Equal(expectedResult.GetHashCode(), convertedAmount.GetHashCode());
        }

        [Fact]
        public void ShouldNotBeEqual_WhenAtLeastOnePropertyIsDifferent()
        {
            Change firstChange = new Change("EUR", "USD", 2.5m);
            Change secondChange = new Change("JPY", "USD", 4m);

            Assert.NotEqual(firstChange, secondChange);
        }

        [Fact]
        public void ShouldGetSameHashcode_WhenChangesValueObjectsHaveEqualProperties()
        {
            Change firstChange = new Change("EUR", "USD", 2.5m);
            Change secondChange = new Change("EUR", "USD", 2.5m);

            int firstHashCode = firstChange.GetHashCode();
            int SecondHashCode = secondChange.GetHashCode();

            Assert.Equal(firstHashCode, SecondHashCode);
        }

        [Fact]
        public void ShouldGetHashcodeAndNoException_WhenNoPropertiesGiven()
        {
            Change change = new Change(null, null, 0);
            
            int hashCode = change.GetHashCode();
            
            Assert.True(hashCode > -1);
        }

        [Fact]
        public void ShouldGetInvertedRate_WhenInvertingChange()
        {
            Change change = new Change("EUR", "USD", 2.5m);
            Change expectedInvertedChange = new Change("USD", "EUR", Math.Round((1/2.5m), 4));

            Change invertedChange = change.Invert();

            Assert.Equal(expectedInvertedChange, invertedChange);
            
        }
    }
}
