using FluentValidation;
using GymApi.Entities;

namespace GymApi.Models.Validators
{
    public class LoginUserValidator:AbstractValidator<LoginDto>
    {
        public LoginUserValidator(GymDbContext db)
        {
            RuleFor(x => x.Email)
                .Custom((value, context) =>
                {
                    var emailInUse = db.Users.Any(u => u.Email == value);
                    if (!emailInUse)
                    {
                        context.AddFailure("Email", "Account with this Email does not exist");
                    }
                });
        }
    }
}
