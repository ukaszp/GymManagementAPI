﻿using FluentValidation;
using GymApi.Entities;

namespace GymApi.Models.Validators
{
    public class CreateUserValidator:AbstractValidator<CreateUserDto>
    {
        public CreateUserValidator(GymDbContext db) 
        { 
            RuleFor(x=>x.Name)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(25);

            RuleFor(x=>x.LastName)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(25);

            RuleFor(x=>x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Password)
                .MinimumLength(6);

            RuleFor(x => x.ConfirmPassword)
                .Equal(p => p.Password);

            RuleFor(x => x.Email)
                .Custom((value, context) =>
                {
                    var emailInUse = db.Users.Any(u => u.Email == value);
                    if(emailInUse)
                    {
                        context.AddFailure("Email", "Account with this Email already exist");
                    }
                });
            RuleFor(x => x.Password)
               .Custom((value, context) =>
               {
                   var email = context.InstanceToValidate.Email;
                   if (value == email)
                   {
                       context.AddFailure("Password", "Password cannot be the same as the email");
                   }
               });
        }
    }
}
