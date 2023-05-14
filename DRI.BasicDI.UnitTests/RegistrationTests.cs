using System;
using Xunit;
using DRI.BasicDI.UnitTests.TestClasses;
using DRI.BasicDI.Exceptions;
using DRI.BasicDI;
using System.Collections.Generic;
using DRI.BasicDI.UnitTests.TestFixtures;

namespace DRI.BasicDI.UnitTests
{
    public class RegistrationTests: ContrainerFixture
    {

        [Fact]
        public void Register_concrete_type_with_concrete_dependency()
        {
            // Arrange
            container.Register<TestClassC>();
            container.Register<TestClassB>();

            // Act & Assert
            Assert.Equal(2, container.Registrations());
        }

        [Fact]
        public void Register_creates_registration_for_concrete_type()
        {
            // Arrange
            container.Register<TestClassC>();

            // Act

            int registrations = container.Registrations();

            // Assert
            Assert.Equal(1, registrations);
        }

        [Fact]
        public void Register_ThrowsAlreadyRegisteredException_WhenRegisteringDependencyMoreThanOnce()
        {
            // Arrange
            container.Register<TestClassC>();
            container.Register<TestClassB>();

            // Act and assert
            Assert.Throws<AlreadyRegisteredException>(() => container.Register<TestClassB>());
        }
    }
}