using Domain;
using Infrastructure.Abstraction;
using Logger.Abstraction;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Infrastructure.Test
{
    public class FileManagerTest
    {
        [Fact]
        public void ShouldGetNull_WhenUnableToValidateFile()
        {
            var fileValidatorMock = new Mock<IFileValidator>();
            var fileParserMock = new Mock<IFileParser>();
            var loggerMock = new Mock<ILogger>();
            fileValidatorMock
                .Setup(fv => fv.Validate(It.IsAny<IList<string>>()))
                .Returns(false);
            FileManager manager = new FileManager(fileValidatorMock.Object, fileParserMock.Object, loggerMock.Object);

            var dataRetrievedFromFile = manager.GetData("testPath");

            Assert.Null(dataRetrievedFromFile);
        }

        [Fact]
        public void ShouldGetParsedData_WhenFileValidationIsOk()
        {
            BaseData expectedData = new BaseData("EUR", 2, "USD", new List<Change>());
            var fileValidatorMock = new Mock<IFileValidator>();
            var fileParserMock = new Mock<IFileParser>();
            var loggerMock = new Mock<ILogger>();
            fileValidatorMock
                .Setup(fv => fv.Validate(It.IsAny<IList<string>>()))
                .Returns(true);
            fileParserMock
                .Setup(fp => fp.Parse(It.IsAny<IList<string>>()))
                .Returns(expectedData);
            FileManager manager = new FileManager(fileValidatorMock.Object, fileParserMock.Object, loggerMock.Object);

            var dataRetrievedFromFile = manager.GetData("testPath");

            Assert.Equal(expectedData, dataRetrievedFromFile);
        }
    }
}
