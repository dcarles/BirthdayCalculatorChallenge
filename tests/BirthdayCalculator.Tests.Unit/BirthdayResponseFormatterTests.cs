using BirthdayCalculator.Console;
using BirthdayCalculator.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BirthdayCalculator.Tests.Unit
{
    public class BirthdayResponseFormatterTests
    {
        [Fact]
        public void Format_WithNullResponse_ReturnsEmptyString()
        {
            // Arrange
            var formatter = new BirthdayResponseFormatter();

            // Act
            var result = formatter.Format(null);

            // Assert
            Assert.Equal("No birthdays found for today.", result);
        }

        [Fact]
        public void Format_WithEmptyResponse_ReturnsEmptyString()
        {
            // Arrange
            var response = new BirthdayResponse();
            var formatter = new BirthdayResponseFormatter();

            // Act
            var result = formatter.Format(response);

            // Assert
            Assert.Equal("No birthdays found for today.", result);
        }

        [Fact]
        public void Format_WithSinglePerson_ReturnsFormattedString()
        {
            // Arrange
            var response = new BirthdayResponse
            {
                BirthdayCelebrants = new[]
                {
                     new PersonDTO
                    {
                        FirstName = "John",
                        LastName = "Doe",
                        BirthDate = new DateTime(1982,10,8)
                    }
                }
            };
            var formatter = new BirthdayResponseFormatter();

            // Act
            var result = formatter.Format(response);

            // Assert
            Assert.Equal("Happy Birthday John Doe! - Year 1982", result);
        }

        [Fact]
        public void Format_WithMultipleBirthdayCelebrants_ReturnsFormattedString()
        {
            // Arrange
            var response = new BirthdayResponse
            {
                BirthdayCelebrants = new[]
                {
                    new PersonDTO
                    {
                        FirstName = "John",
                        LastName = "Doe",
                        BirthDate = new DateTime(1982,10,8)
                    },
                    new PersonDTO
                    {
                        FirstName = "Bruce",
                        LastName = "Wayne",
                        BirthDate = new DateTime(1965,1,30)
                    }
                }
            };
            var formatter = new BirthdayResponseFormatter();

            // Act
            var result = formatter.Format(response);

            // Assert
            Assert.Equal("Happy Birthday John Doe! - Year 1982\nHappy Birthday Bruce Wayne! - Year 1965", result);
        }
    }
}
