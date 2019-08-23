using StringCalculator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace StringCalculatorTests
{
    [TestClass]
    public class StringCalculatorTests
    {
        // Requirements:
        // 1. Adds all valid numbers together
        // 2. Comma delimited
        // 3. Supports 1 or more numbers
        // 4. Invalid numbers are converted to zeroes
        // 5. Supports \n as delimiter
        // 6. Negative numbers throw an exception
        // 7. Numbers greater than 1000 are returned as 0
        // 8. Supports a single custom delimiter
        // 9. Supports delimiters of any length
        // 10. Supports multiple delimiters

        [TestMethod]
        public void TestParseInput()
        {
            var input = "1,20";
            var total = Program.ParseInput(input);
            Assert.AreEqual(21, total);
        }

        [TestMethod]
        public void TestParseInputInvalidNumberConvertsToZero()
        {
            var input = "5,tytyt";
            var total = Program.ParseInput(input);
            Assert.AreEqual(5, total);
        }

        [TestMethod]
        public void TestParseInputMoreThanTwoNumbers()
        {
            var input = "2,3,5,7,11,13";
            var total = Program.ParseInput(input);
            Assert.AreEqual(41, total);
        }

        [TestMethod]
        public void TestNewLineAsDelimiter()
        {
            var input = "1\\n2,3";
            var total = Program.ParseInput(input);
            Assert.AreEqual(6, total);
        }

        [TestMethod]
        public void TestNegativeNumbersThrowsException()
        {
            var input = "-1000,5,-500,3,-600";
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Program.ParseInput(input));
        }

        [TestMethod]
        public void TestNumbersGreaterThan1000()
        {
            var input = "2,1001,6";
            var total = Program.ParseInput(input);
            Assert.AreEqual(8, total);
        }

        [TestMethod]
        public void TestSingleCustomDelimiter()
        {
            var input = "//;\\n2;5";
            var total = Program.ParseInput(input);
            Assert.AreEqual(7, total);
        }

        [TestMethod]
        public void TestDelimiterOfAnyLength()
        {
            var input = "//[***]\\n11***22***33";
            var total = Program.ParseInput(input);
            Assert.AreEqual(66, total);
        }

        [TestMethod]
        public void TestMultipleDelimiters()
        {
            var input = "//[*][!!][rrr]\\n11rrr22*33!!44";
            var total = Program.ParseInput(input);
            Assert.AreEqual(110, total);
        }
    }
}