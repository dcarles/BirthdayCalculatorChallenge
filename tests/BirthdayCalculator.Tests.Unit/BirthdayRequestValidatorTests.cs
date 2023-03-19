using AutoFixture;
using BirthdayCalculator.ViewModels;
using BirthdayCalculator.ViewModels.Validation;
using FluentValidation.TestHelper;
using System;
using Xunit;


namespace BirthdayCalculator.Tests.Unit
{
    public class BirthdayRequestValidatorTests
    {
        private readonly PersonValidator _validator;
        private readonly IFixture _fixture;
        private readonly PersonDTO _person;
        private const string PostRuleSetName = "Post";

        public BirthdayRequestValidatorTests()
        {
            _fixture = new Fixture();
            _person = _fixture.Create<PersonDTO>();
            _validator = new PersonValidator();
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void FirstName_IsNullOrEmpty_ShouldHaveError(string name)
        {
            _person.FirstName = name;
            var result = _validator.TestValidate(_person, options => options.IncludeRuleSets(PostRuleSetName));
            result.ShouldHaveValidationErrorFor(request => request.FirstName);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void LastName_IsNullOrEmpty_ShouldHaveError(string name)
        {
            _person.LastName = name;
            var result = _validator.TestValidate(_person, options => options.IncludeRuleSets(PostRuleSetName));
            result.ShouldHaveValidationErrorFor(request => request.LastName);
        }

        [Theory]
        [InlineData(null)]
        public void Birthdate_IsNull_ShouldHaveError(DateTime? date)
        {
            _person.BirthDate = date;
            var result = _validator.TestValidate(_person, options => options.IncludeRuleSets(PostRuleSetName));
            result.ShouldHaveValidationErrorFor(request => request.BirthDate);
        }
    }
}
