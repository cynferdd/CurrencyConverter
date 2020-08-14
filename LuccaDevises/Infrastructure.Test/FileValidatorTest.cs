using Infrastructure.Abstraction;
using System;
using System.Collections.Generic;
using Xunit;

namespace Infrastructure.Test
{
    public class FileValidatorTest
    {
        [Fact]
        public void ShouldNotBeValid_WhenGivenNullFileContent()
        {
            
            FileValidator fileValidator = new FileValidator();

            bool isValid = fileValidator.CheckHasAtLeast3Lines(null);

            Assert.False(isValid);
        }

        [Fact]
        public void ShouldNotBeValid_WhenGivenEmptyFileContent()
        {

            FileValidator fileValidator = new FileValidator();

            bool isValid = fileValidator.CheckHasAtLeast3Lines(new List<string>());

            Assert.False(isValid);
        }

        [Fact]
        public void ShouldNotBeValid_WhenGivenOneLine()
        {
            List<string> lines = new List<string>
            {
                ""
            };
            FileValidator fileValidator = new FileValidator();

            bool isValid = fileValidator.CheckHasAtLeast3Lines(lines);

            Assert.False(isValid);
        }

        [Fact]
        public void ShouldNotBeValid_WhenGivenTwoLines()
        {
            List<string> lines = new List<string>
            {
                "",
                ""
            };
            FileValidator fileValidator = new FileValidator();

            bool isValid = fileValidator.CheckHasAtLeast3Lines(lines);

            Assert.False(isValid);
        }

        [Fact]
        public void ShouldBeValid_WhenGivenAtLeastThreeLines()
        {
            List<string> lines = new List<string>
            {
                "",
                "",
                ""
            };
            FileValidator fileValidator = new FileValidator();

            bool isValid = fileValidator.CheckHasAtLeast3Lines(lines);

            Assert.True(isValid);
        }

        [Fact]
        public void ShouldNotBeValid_WhenFirstLineHasLessThan3Fields()
        {
            List<string> lines = new List<string>
            {
                ""
            };
            FileValidator fileValidator = new FileValidator();

            bool isValid = fileValidator.CheckFirstLineContains3Fields(lines);

            Assert.False(isValid);
        }

        [Fact]
        public void ShouldNotBeValid_WhenFirstLineHasMoreThan3Fields()
        {
            List<string> lines = new List<string>
            {
                "a;a;a;a"
            };
            FileValidator fileValidator = new FileValidator();

            bool isValid = fileValidator.CheckFirstLineContains3Fields(lines);

            Assert.False(isValid);
        }

        [Fact]
        public void ShouldBeValid_WhenFirstLineHasExactly3Fields()
        {
            List<string> lines = new List<string>
            {
                "a;a;a"
            };
            FileValidator fileValidator = new FileValidator();

            bool isValid = fileValidator.CheckFirstLineContains3Fields(lines);

            Assert.True(isValid);
        }

        [Fact]
        public void ShouldNotBeValid_WhenFirstFieldLessThan3Characters()
        {
            List<string> lines = new List<string>
            {
                "a;1;a"
            };
            FileValidator fileValidator = new FileValidator();

            bool isValid = fileValidator.CheckFirstLineFieldsFormat(lines);

            Assert.False(isValid);
        }

        [Fact]
        public void ShouldNotBeValid_WhenFirstFieldMoreThan3Characters()
        {
            List<string> lines = new List<string>
            {
                "abcd;1;a"
            };
            FileValidator fileValidator = new FileValidator();

            bool isValid = fileValidator.CheckFirstLineFieldsFormat(lines);

            Assert.False(isValid);
        }

        [Fact]
        public void ShouldNotBeValid_WhenSecondFieldNotAnInt()
        {
            List<string> lines = new List<string>
            {
                "abc;Z;a"
            };
            FileValidator fileValidator = new FileValidator();

            bool isValid = fileValidator.CheckFirstLineFieldsFormat(lines);

            Assert.False(isValid);
        }

        [Fact]
        public void ShouldNotBeValid_WhenThirdFieldLessThan3Characters()
        {
            List<string> lines = new List<string>
            {
                "abc;2;a"
            };
            FileValidator fileValidator = new FileValidator();

            bool isValid = fileValidator.CheckFirstLineFieldsFormat(lines);

            Assert.False(isValid);
        }

        [Fact]
        public void ShouldNotBeValid_WhenThirdFieldLessMoreThan3Characters()
        {
            List<string> lines = new List<string>
            {
                "abc;2;abcd"
            };
            FileValidator fileValidator = new FileValidator();

            bool isValid = fileValidator.CheckFirstLineFieldsFormat(lines);

            Assert.False(isValid);
        }

        [Fact]
        public void ShouldBeValid_WhenThirdLineFormatIsOk()
        {
            List<string> lines = new List<string>
            {
                "abc;2;abc"
            };
            FileValidator fileValidator = new FileValidator();

            bool isValid = fileValidator.CheckFirstLineFieldsFormat(lines);

            Assert.True(isValid);
        }

        [Fact]
        public void ShouldNotBeValid_WhenSecondLineIsNotAnInteger()
        {
            List<string> lines = new List<string>
            {
                "abc;2;abcd",
                "t"
            };
            FileValidator fileValidator = new FileValidator();

            bool isValid = fileValidator.CheckSecondLineIsPositiveInt(lines);

            Assert.False(isValid);
        }

        [Fact]
        public void ShouldNotBeValid_WhenSecondLineIsANegativeInteger()
        {
            List<string> lines = new List<string>
            {
                "abc;2;abcd",
                "-12"
            };
            FileValidator fileValidator = new FileValidator();

            bool isValid = fileValidator.CheckSecondLineIsPositiveInt(lines);

            Assert.False(isValid);
        }

        [Fact]
        public void ShouldBeValid_WhenSecondLineIsZero()
        {
            List<string> lines = new List<string>
            {
                "abc;2;abcd",
                "0"
            };
            FileValidator fileValidator = new FileValidator();

            bool isValid = fileValidator.CheckSecondLineIsPositiveInt(lines);

            Assert.False(isValid);
        }

        [Fact]
        public void ShouldBeValid_WhenSecondLineIsAPositiveInteger()
        {
            List<string> lines = new List<string>
            {
                "abc;2;abcd",
                "210"
            };
            FileValidator fileValidator = new FileValidator();

            bool isValid = fileValidator.CheckSecondLineIsPositiveInt(lines);

            Assert.True(isValid);
        }

        [Fact]
        public void ShouldNotBeValid_WhenTotalAmounOfLinesUnderExpected()
        {
            List<string> lines = new List<string>
            {
                "abc;2;abcd",
                "2",
                ""
            };
            FileValidator fileValidator = new FileValidator();

            bool isValid = fileValidator.CheckGoodAmountOfLines(lines);

            Assert.False(isValid);
        }


        [Fact]
        public void ShouldNotBeValid_WhenTotalAmounOfLinesAboveExpected()
        {
            List<string> lines = new List<string>
            {
                "abc;2;abcd",
                "2",
                "",
                "",
                ""
            };
            FileValidator fileValidator = new FileValidator();

            bool isValid = fileValidator.CheckGoodAmountOfLines(lines);

            Assert.False(isValid);
        }


        [Fact]
        public void ShouldBeValid_WhenTotalAmounOfLinesEqualToExpected()
        {
            List<string> lines = new List<string>
            {
                "abc;2;abcd",
                "2",
                "",
                ""
            };
            FileValidator fileValidator = new FileValidator();

            bool isValid = fileValidator.CheckGoodAmountOfLines(lines);

            Assert.True(isValid);
        }

        [Fact]
        public void ShouldNotBeValid_WhenLessThan3FieldsInDataLine()
        {
            string line = "";

            bool isValid = new FileValidator().CheckChangeFormat(line);

            Assert.False(isValid);
        }

        [Fact]
        public void ShouldNotBeValid_WhenMoreThan3FieldsInDataLine()
        {
            string line = "eur;usd;3.0000;d";

            bool isValid = new FileValidator().CheckChangeFormat(line);

            Assert.False(isValid);
        }

        [Fact]
        public void ShouldNotBeValid_WhenFirstFieldIsLessThan3CharactersInDataLine()
        {
            string line = "eu;usd;3.0000";

            bool isValid = new FileValidator().CheckChangeFormat(line);

            Assert.False(isValid);
        }

        [Fact]
        public void ShouldNotBeValid_WhenFirstFieldIsMoreThan3CharactersInDataLine()
        {
            string line = "euro;usd;3.0000";

            bool isValid = new FileValidator().CheckChangeFormat(line);

            Assert.False(isValid);
        }


        [Fact]
        public void ShouldNotBeValid_WhenSecondFieldIsLessThan3CharactersInDataLine()
        {
            string line = "eur;us;3.0000";

            bool isValid = new FileValidator().CheckChangeFormat(line);

            Assert.False(isValid);
        }

        [Fact]
        public void ShouldNotBeValid_WhenSecondFieldIsMoreThan3CharactersInDataLine()
        {
            string line = "eur;usdollar;3.0000";

            bool isValid = new FileValidator().CheckChangeFormat(line);

            Assert.False(isValid);
        }

        [Fact]
        public void ShouldNotBeValid_WhenThirdFieldIsNotADecimalInDataLine()
        {
            string line = "eur;usdollar;A";

            bool isValid = new FileValidator().CheckChangeFormat(line);

            Assert.False(isValid);
        }

        [Fact]
        public void ShouldNotBeValid_WhenThirdFieldDoesNotHaveCorrectSeparatorInDataLine()
        {
            string line = "eur;usdollar;3,4567";

            bool isValid = new FileValidator().CheckChangeFormat(line);

            Assert.False(isValid);
        }

        [Fact]
        public void ShouldNotBeValid_WhenThirdFieldDoesNotHave4DecimalsInDataLine()
        {
            string line = "eur;usdollar;3.1";

            bool isValid = new FileValidator().CheckChangeFormat(line);

            Assert.False(isValid);
        }

        [Fact]
        public void ShouldBeValid_WhenDataLineFormatIsOk()
        {
            string line = "eur;usd;3.1210";

            bool isValid = new FileValidator().CheckChangeFormat(line);

            Assert.True(isValid);
        }

        [Fact]
        public void ShouldNotBeValid_WhenAtLeastOneDataLineFormatIsKo()
        {
            List<string> lines = new List<string>
            {
                "abc;2;abcd",
                "3",
                "eur;usd;3.1234",
                "jpy;rub;44.2210",
                "false"
            };
            FileValidator fileValidator = new FileValidator();

            bool isValid = fileValidator.CheckLastLinesFormat(lines);

            Assert.False(isValid);
        }


        [Fact]
        public void ShouldBeValid_WhenAllDataLineFormatIsOk()
        {
            List<string> lines = new List<string>
            {
                "abc;2;abcd",
                "2",
                "eur;usd;3.1234",
                "jpy;rub;44.2210"
            };
            FileValidator fileValidator = new FileValidator();

            bool isValid = fileValidator.CheckLastLinesFormat(lines);

            Assert.True(isValid);
        }

        [Fact]
        public void ShouldNotBeValid_WhenAtLeastOneRuleIsKo()
        {
            List<string> lines = new List<string>
            {
                "abc;2;abcd",
                "3",
                "eur;usd;3.1234",
                "jpy;rub;44.2210"
            };
            FileValidator fileValidator = new FileValidator();

            bool isValid = fileValidator.Validate(lines);

            Assert.False(isValid);
        }


        [Fact]
        public void ShouldBeValid_WhenAllRulesAreOk()
        {
            List<string> lines = new List<string>
            {
                "abc;2;abc",
                "2",
                "eur;usd;3.1234",
                "jpy;rub;44.2210"
            };
            FileValidator fileValidator = new FileValidator();

            bool isValid = fileValidator.Validate(lines);

            Assert.True(isValid);
        }
    }
}

