using FluentValidation;

namespace Models.Validators
{
    public class AccountValidator : AbstractValidator<Account>
    {
        public AccountValidator()
        {
            RuleFor(a => a.Currency).NotEmpty();
        }
    }
}
