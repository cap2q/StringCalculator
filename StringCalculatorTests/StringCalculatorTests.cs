using StringCalculator;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StringCalculatorTests
{
    [TestClass]
    public class StringCalculatorTests
    {
        // Requirements:
        // 1. Adds all valid numbers together.
        // 2. Comma delimited
        // 3. Support max of 2 numbers
        // 4. Invalid numbers are converted to zeroes

        [TestMethod]
        public void TestParseInput()
        {
            var input = "1,20";
            var total = Program.ParseInput(input);
            Assert.AreEqual(total, 21);
        }

        [TestMethod]
        public void TestParseInputMax()
        {
            var input = "1,20,50";
            Assert.ThrowsException<ArgumentException>(() => Program.ParseInput(input));
        }

        [TestMethod]
        public void TestParseInputInvalidNumberConvertsToZero()
        {
            var input = "5,tytyt";
            var total = Program.ParseInput(input);
            Assert.AreEqual(total, 5);
        }
    }
}