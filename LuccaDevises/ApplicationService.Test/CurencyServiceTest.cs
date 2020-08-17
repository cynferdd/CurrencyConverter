using Domain;
using DomainService.Abstractions;
using Infrastructure.Abstraction;
using Logger.Abstraction;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace ApplicationService.Test
{
    public class CurencyServiceTest
    {
        [Fact]
        public void ShouldGet0_WhenFileManagerDoesNotRetrieveData()
        {
            var fileManagerMock = new Mock<IFileManager>();
            var pathCalculatorMock = new Mock<IPathCalculator>();
            var rateCalculatorMock = new Mock<IRateCalculator>();
            var loggerMock = new Mock<ILogger>();
            fileManagerMock
                .Setup(fm => fm.GetData(It.IsAny<string>()))
                .Returns((BaseData)null);
            CurrencyService service = new CurrencyService(fileManagerMock.Object, pathCalculatorMock.Object, rateCalculatorMock.Object, loggerMock.Object);

            int calculatedAmount = service.CalculateRate("randomPath");

            Assert.Equal(0, calculatedAmount);
        }

        [Fact]
        public void ShouldGet0_WhenPathCalculatorDoesNotRetrieveData()
        {
            var fileManagerMock = new Mock<IFileManager>();
            var pathCalculatorMock = new Mock<IPathCalculator>();
            var rateCalculatorMock = new Mock<IRateCalculator>();
            var loggerMock = new Mock<ILogger>();
            fileManagerMock
                .Setup(fm => fm.GetData(It.IsAny<string>()))
                .Returns(new BaseData(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(),It.IsAny<IList<Change>>()));
            pathCalculatorMock
                .Setup(pc => pc.GetRatesPathes(It.IsAny<IList<Change>>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns((IList<Change>)null);
            CurrencyService service = new CurrencyService(fileManagerMock.Object, pathCalculatorMock.Object, rateCalculatorMock.Object, loggerMock.Object);

            int calculatedAmount = service.CalculateRate("randomPath");

            Assert.Equal(0, calculatedAmount);
        }

        [Fact]
        public void ShouldGetCalculatedAmount_WhenDataCouldBeRetrievedAndCalculated()
        {
            int resultedAmount = 530;
            var fileManagerMock = new Mock<IFileManager>();
            var pathCalculatorMock = new Mock<IPathCalculator>();
            var rateCalculatorMock = new Mock<IRateCalculator>();
            var loggerMock = new Mock<ILogger>();
            fileManagerMock
                .Setup(fm => fm.GetData(It.IsAny<string>()))
                .Returns(new BaseData(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<IList<Change>>()));
            pathCalculatorMock
                .Setup(pc => pc.GetRatesPathes(It.IsAny<IList<Change>>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new List<Change>());
            rateCalculatorMock
                .Setup(rc => rc.CalculateChangeRate(It.IsAny<int>(), It.IsAny<IList<Change>>()))
                .Returns(resultedAmount);
            CurrencyService service = new CurrencyService(fileManagerMock.Object, pathCalculatorMock.Object, rateCalculatorMock.Object, loggerMock.Object);

            int calculatedAmount = service.CalculateRate("randomPath");

            Assert.Equal(resultedAmount, calculatedAmount);
        }
    }

}
