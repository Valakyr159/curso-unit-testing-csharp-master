using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using Castle.Core.Logging;

namespace StringManipulation.Tests
{
    public class StringOperationTest
    {
        [Fact(Skip = "Esta prueba no es válida en este momento, TICKET-001")]
        public void ContactenateStrings()
        {
            // Arrange
            var strOperations = new StringOperations();
            // Act
            var result = strOperations.ConcatenateStrings("Hello", "Platzi");
            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal("Hello Platzi", result);
        }

        [Fact]
        public void IsPalindrome()
        {
            // Arrange
            var strOperations = new StringOperations();

            // Act 
            var result = strOperations.IsPalindrome("ama");

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsPalindrome_False()
        {
            // Arrange
            var strOperations = new StringOperations();

            // Act 
            var result = strOperations.IsPalindrome("Hello");

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void RemoveWhitespace()
        {
            // Arrange
            var strOperations = new StringOperations();

            // Act 
            var result = strOperations.RemoveWhitespace("Hello World");

            // Assert
            Assert.Equal("HelloWorld", result);
        }


        [Fact]
        public void QuantityInWords()
        {
            // Arrange
            var strOperations = new StringOperations();

            // Act 
            var result = strOperations.QuantintyInWords("cat", 10);

            // Assert
            Assert.StartsWith("diez", result);
            Assert.Equal("diez cats", result);
            Assert.Contains("cat", result);
        }

        [Fact]
        public void GetStringLength_Exception()
        {
            // Arrange
            var strOperations = new StringOperations();

            // Assert 
            Assert.ThrowsAny<ArgumentNullException>
                (
                () => strOperations.GetStringLength(null)
                );
        }

        [Fact]
        public void GetStringLength()
        {
            // Arrange
            var strOperations = new StringOperations();

            // Act
            var result = strOperations.GetStringLength("something");

            // Assert 
            Assert.Equal(9, result);
        }

        [Theory]
        [InlineData("V", 5)]
        [InlineData("III", 3)]
        [InlineData("X", 10)]
        public void FromRomanToNumber(string romanNumber, int expected)
        {
            // Arrange
            var strOperations = new StringOperations();

            // Act
            var result = strOperations.FromRomanToNumber(romanNumber);

            // Assert 
            Assert.Equal(expected, result);
        }

        [Fact]
        public void CountOccurrencies()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<StringOperations>>();
            var strOperations = new StringOperations(mockLogger.Object);

            // Act
            var result = strOperations.CountOccurrences("Hello platzi", 'l');

            // Assert 
            Assert.Equal(3, result);
        }

        [Fact]
        public void ReadFile()
        {
            // Arrange
            var strOperations = new StringOperations();
            var mockFileReader = new Mock<IFileReaderConector>();
            mockFileReader.Setup(p => p.ReadString("file.txt")).Returns("Reading file");
            // Another option omits specific param for .ReadFile(mockFileReader.Object, ("this"))
            // mockFileReader.Setup(p => p.ReadString(It.IsAny<string>())).Returns("Reading file");
            // Act
            var result = strOperations.ReadFile(mockFileReader.Object, "file.txt");

            // Assert 
            Assert.Equal("Reading file", result);
        }
    }
}
