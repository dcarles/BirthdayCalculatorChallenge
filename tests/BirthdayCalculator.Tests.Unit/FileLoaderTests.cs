using BirthdayCalculator.Console;
using Moq;
using System.IO;
using System.Text;
using Xunit;

namespace BirthdayCalculator.Tests.Unit
{
    public class FileLoaderTests
    {
        [Theory]
        [InlineData(@"[[""Doe"", ""John"", ""1982/10/08""],
                [""Wayne"", ""Bruce"", ""1965/01/30""],
                [""Gaga"", ""Lady"", ""1986/03/19""],
                [""Curry"", ""Mark"", ""1988/02/29""]]")]
        [InlineData(@"[{""firstName"":""John"",""lastName"":""Doe"",""birthDate"":""1982-10-08""},
                {""firstName"":""Bruce"",""lastName"":""Wayne"",""birthDate"":""1965-01-30""},
                {""firstName"":""Lady"",""lastName"":""Gaga"",""birthDate"":""1986-03-19""},
                {""firstName"":""Mark"",""lastName"":""Curry"",""birthDate"":""1988-02-29""}]")]
        public void Load_ReturnsCorrectBirthdayRequest(string input)
        {           
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(input));
            using var reader = new StreamReader(stream);

            // Act
            var birthdayRequest = PeopleFileLoader.Load(reader);

            // Assert
            Assert.Collection(birthdayRequest.People,
                person => Assert.Equal("John", person.FirstName),
                person => Assert.Equal("Bruce", person.FirstName),
                person => Assert.Equal("Lady", person.FirstName),
                person => Assert.Equal("Mark", person.FirstName)
            );
        }
    }
}
