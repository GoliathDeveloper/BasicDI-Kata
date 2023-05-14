using DRI.BasicDI.UnitTests.TestClasses;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;

namespace DRI.BasicDI.UnitTests
{
    public class CircularClassTests
    {
        [Fact]
        public void CircularClassA_Constructor_NullArgument_ThrowsArgumentNullException()
        {
            // Arrange
            CircularClassB circularClassB = null;

            // Act + Assert
            Assert.Throws<ArgumentNullException>(() => new CircularClassA(circularClassB));
        }

        [Fact]
        public void CircularClassB_Constructor_NullArgument_ThrowsArgumentNullException()
        {
            // Arrange
            CircularClassC circularClassC = null;

            // Act + Assert
            Assert.Throws<ArgumentNullException>(() => new CircularClassB(circularClassC));
        }

        [Fact]
        public void CircularClassC_Constructor_NullArgument_ThrowsArgumentNullException()
        {
            // Arrange
            CircularClassA circularClassA = null;

            // Act + Assert
            Assert.Throws<ArgumentNullException>(() => new CircularClassC(circularClassA));
        }
    }
}