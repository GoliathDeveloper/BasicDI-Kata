using DRI.BasicDI.Exceptions;
using DRI.BasicDI.UnitTests.TestClasses;
using DRI.BasicDI.UnitTests.TestFixtures;
using Microsoft.Extensions.DependencyModel;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace DRI.BasicDI.UnitTests
{
    /// <summary>
    /// Inherits from ContainerFixture
    /// </summary>
    public class GetInstanceTests : ContrainerFixture
    {
        /// <summary>
        /// TestClassC is used as it is a type with no dependencies
        /// </summary>
        [Fact]
        public void Should_instantiate_concrete_type_with_no_dependencies()
        {
            // Arrange & Act
            container.Register<TestClassC>();
            var result = container.GetInstance<TestClassC>();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<TestClassC>(result);
            Assert.Equal("Test", result._message);
        }

        /// <summary>
        /// TestClassB is used as it is a concrete type and instantiates a new instance of TestClassC which is a concrete dependency
        /// </summary>
        [Fact]
        public void Should_instantiate_concrete_type_with_concrete_dependency()
        {
            // Arrange & Act
            container.Register<TestClassC>();
            container.Register<TestClassB>();
            var result = container.GetInstance<TestClassB>();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<TestClassB>(result);
            Assert.NotNull(result._testClassC);
        }

        /// <summary>
        /// TestClassA is used as it is a concrete type that instanciates multiple concrete dependencies
        /// </summary>
        [Fact]
        public void Should_instantiate_concrete_type_with_multiple_concrete_dependencies()
        {
            // Arrange & Act
            container.Register<TestClassC>();
            container.Register<TestClassB>();
            container.Register<TestClassA>();
            var result = container.GetInstance<TestClassA>();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<TestClassA>(result);
            Assert.Equal("Test", result._testClassC._message);
        }

        [Fact]
        public void Should_throw_UnregisteredDependencyException_when_instantiating_unregistered_type()
        {
            // Arrange, Act & Assert
            Assert.Throws<UnregisteredDependencyException>(() => container.GetInstance<TestClassA>());
        }

        /// <summary>
        /// In the test I create a new container instance, register TestClassB not but TestClassA or TestClassC
        /// </summary>
        [Fact]
        public void Should_throw_UnregisteredDependencyException_when_instantiating_type_with_unregistered_dependency()
        {
            // Arrange
            // register TestClassB, but not TestClassC
            var expectedExceptionMessage = "Type DRI.BasicDI.UnitTests.TestClasses.TestClassC has not been registered.";

            // Act
            var exception = Assert.Throws<UnregisteredDependencyException>(() => container.Register<TestClassB>());

            // Assert
            Assert.Equal(expectedExceptionMessage, exception.Message);
        }

        [Fact]
        public void Should_throw_CircularDependencyException_when_instantiating_type_with_circular_dependency()
        {
            //Arrange, Act & Assert
            Assert.Throws<CircularDependencyException>(() => container.Register<CircularClassC>());
        }

        [Fact]
        public void Should_not_throw_CircularDependencyException_when_instantiating_type_with_concrete_dependency()
        {
            //Arrange & Act
            container.Register<TestClassC>();

            //Assert
            var exception = Record.Exception(() => container.Register<TestClassB>());
            Assert.Null(exception);
        }

        [Fact]
        public void Get_instance_with_unregistered_type_throws_InvalidOperationException()
        {
            // Arrange, Act & Assert
            Assert.Throws<UnregisteredDependencyException>(() => container.GetInstance<TestClassC>());
        }

        [Fact]
        public void GetInstance_with_registered_type_returns_created_instance()
        {
            // Arrange & Act
            container.Register<TestClassC>();
            var instance = container.GetInstance<TestClassC>();

            // Assert
            Assert.IsType<TestClassC>(instance);
        }

        [Fact]
        public void TestConstructorWithTestClassCParameter()
        {
            // Arrange
            TestClassC expectedTestClassC = new TestClassC();

            // Act
            TestClassB testClassB = new TestClassB(expectedTestClassC);

            // Assert
            Assert.Equal(expectedTestClassC, testClassB._testClassC);
        }

        [Fact]
        public void TestConstructorWithParameters()
        {
            // Arrange
            var expectedMessage = "Test Message";

            container.Register<TestClassC>(new TestClassC { _message = expectedMessage });
            container.Register<TestClassB>(new TestClassB(container.GetInstance<TestClassC>()));
            container.Register<TestClassA>(new TestClassA(container.GetInstance<TestClassB>(), container.GetInstance<TestClassC>()));

            // Act
            var testClassA = container.GetInstance<TestClassA>();

            // Assert
            Assert.NotNull(testClassA);
            Assert.NotNull(testClassA._testClassB);
            Assert.Equal(expectedMessage, testClassA._testClassB._testClassC._message);
        }
    }
}