using FluentValidation;

namespace Models.Validators
{
    public class PersonValidator : AbstractValidator<Person>
    {
        public PersonValidator()
        {
            RuleFor(p => p.Name).NotEmpty();
        }
    }
}
