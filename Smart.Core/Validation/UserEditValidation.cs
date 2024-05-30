﻿using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Smart.Core.Entities;
using Smart.Shared.DTOs.UserDTO;

namespace Smart.Core.Validation
{
    public class UserEditValidation : AbstractValidator<UserEditDTO>
    {
        protected readonly UserManager<User> _userManager;
        public UserEditValidation(UserManager<User> manager)
        {
            _userManager = manager;

            RuleFor(user => user.Firstname)
                .NotEmpty()
                .Length(3, 50);


            RuleFor(user => user.Lastname)
                .NotEmpty()
                .Length(3, 50);


            /*RuleFor(user => user.Email)
                .NotEmpty()
                .Must(IsUniqueEmail).WithMessage("{PropertyName} already exists.");*/

            RuleFor(user => user.CardNumber)
              .NotEmpty()
              .Matches("^[0-9]+$").When(user => !string.IsNullOrWhiteSpace(user.CardNumber))
              .Matches(@"^\d{16}$").When(user => !string.IsNullOrWhiteSpace(user.CardNumber))
              .Must(IsValidLuhnAlgorithm).WithMessage("{PropertyName} is not a valid credit card number.")
              .When(user => !string.IsNullOrWhiteSpace(user.CardNumber));
        }

        /*private bool IsUniqueEmail(string email)
        {
            var userObject = _userManager.FindByNameAsync(email).Result;
            return userObject == null;
        }*/


        private bool IsValidLuhnAlgorithm(string cardNumber)
        {
            if (string.IsNullOrWhiteSpace(cardNumber) || !cardNumber.All(char.IsDigit))
            {
                return true;
            }
            int sum = 0;
            bool doubleDigit = false;

            // Luhn Algorithm
            for (int i = cardNumber.Length - 1; i >= 0; i--)
            {
                int digit = int.Parse(cardNumber[i].ToString());

                if (doubleDigit)
                {
                    digit *= 2;
                    if (digit > 9)
                    {
                        digit -= 9;
                    }
                }

                sum += digit;
                doubleDigit = !doubleDigit;
            }

            return sum % 10 == 0;
        }

    }
}
