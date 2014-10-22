using System.Collections.Generic;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using ANTLR_HomeTask1;
using Calculator;

namespace Tests
{
    [TestFixture]
    public class TestCalculator
    {
        private IDataExchange _dataExchange;
        private IConsoleReadWrite _consoleReadWrite;


        [SetUp]
        public void SetUp()
        {
            _consoleReadWrite = Substitute.For<IConsoleReadWrite>();
            _dataExchange = new DataExchange(_consoleReadWrite);
        }

        [Test]
        public void SimpleAdd()
        {
            // Arrange
            _consoleReadWrite.ReadLine().Returns("2+2");
            var stream = _dataExchange.ReadData();
            // Act
            var lexer = new Lexer();
            List<string> dividedString = lexer.DivStringFroTokens(stream);
            var parser = new Parser();
            var result = parser.Calculate(dividedString);
            //Assert
            result.Should().Be(4);
        }

        [Test]
        public void TestForMulPriority()
        {
            // Arrange
            _consoleReadWrite.ReadLine().Returns("2+2*2");
            var stream = _dataExchange.ReadData();
            // Act
            var lexer = new Lexer();
            List<string> dividedString = lexer.DivStringFroTokens(stream);
            var parser = new Parser();
            var result = parser.Calculate(dividedString);
            //Assert
            result.Should().Be(6);
        }

        [Test]
        public void TestForMulPriorityArgumentsOrder()
        {
            // Arrange
            _consoleReadWrite.ReadLine().Returns("2-2*2");
            var stream = _dataExchange.ReadData();
            // Act
            var lexer = new Lexer();
            List<string> dividedString = lexer.DivStringFroTokens(stream);
            var parser = new Parser();
            var result = parser.Calculate(dividedString);
            //Assert
            result.Should().Be(-2);
        }

        [Test]
        public void TestJustBrackets()
        {
            // Arrange
            _consoleReadWrite.ReadLine().Returns("(2+2)");
            var stream = _dataExchange.ReadData();
            // Act
            var lexer = new Lexer();
            List<string> dividedString = lexer.DivStringFroTokens(stream);
            var parser = new Parser();
            var result = parser.Calculate(dividedString);
            //Assert
            result.Should().Be(4);
        }

        [Test]
        public void TestSimpleAddWithBrackets()
        {
            // Arrange
            _consoleReadWrite.ReadLine().Returns("2+(2+2)");
            var stream = _dataExchange.ReadData();
            // Act
            var lexer = new Lexer();
            List<string> dividedString = lexer.DivStringFroTokens(stream);
            var parser = new Parser();
            var result = parser.Calculate(dividedString);
            //Assert
            result.Should().Be(6);
        }

        [Test]
        public void TestWithBracketsAndMul()
        {
            // Arrange
            _consoleReadWrite.ReadLine().Returns("(2+2)*2");
            var stream = _dataExchange.ReadData();
            // Act
            var lexer = new Lexer();
            List<string> dividedString = lexer.DivStringFroTokens(stream);
            var parser = new Parser();
            var result = parser.Calculate(dividedString);
            //Assert
            result.Should().Be(8);
        }

        [Test]
        public void TestABitHarder()
        {
            // Arrange
            _consoleReadWrite.ReadLine().Returns("7+2-(12-3-7)*2");
            var stream = _dataExchange.ReadData();
            // Act
            var lexer = new Lexer();
            List<string> dividedString = lexer.DivStringFroTokens(stream);
            var parser = new Parser();
            var result = parser.Calculate(dividedString);
            //Assert
            result.Should().Be(5);
        }

        [Test]
        public void JustTest1()
        {
            // Arrange
            _consoleReadWrite.ReadLine().Returns("(12-7+3*2-(2+1)*2+7)/4");
            var stream = _dataExchange.ReadData();
            // Act
            var lexer = new Lexer();
            List<string> dividedString = lexer.DivStringFroTokens(stream);
            var parser = new Parser();
            var result = parser.Calculate(dividedString);
            //Assert
            result.Should().Be(3);
        }

        [Test]
        public void JustTest2()
        {
            // Arrange
            _consoleReadWrite.ReadLine().Returns("(2+2)+(2+1)*3");
            var stream = _dataExchange.ReadData();
            // Act
            var lexer = new Lexer();
            List<string> dividedString = lexer.DivStringFroTokens(stream);
            var parser = new Parser();
            var result = parser.Calculate(dividedString);
            //Assert
            result.Should().Be(13);
        }

        [Test]
        public void JustTest3()
        {
            // Arrange
            _consoleReadWrite.ReadLine().Returns("(12-7+3*2-(2+1)*2+7+(1+1)*2)/4");
            var stream = _dataExchange.ReadData();
            // Act
            var lexer = new Lexer();
            List<string> dividedString = lexer.DivStringFroTokens(stream);
            var parser = new Parser();
            var result = parser.Calculate(dividedString);
            //Assert
            result.Should().Be(4);
        }
    }
}
