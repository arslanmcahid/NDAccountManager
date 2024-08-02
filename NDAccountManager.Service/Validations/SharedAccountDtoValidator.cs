using FluentValidation;
using NDAccountManager.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDAccountManager.Service.Validations
{
    public class SharedAccountDtoValidator : AbstractValidator<SharedAccountDto>
    {
        public SharedAccountDtoValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required.");
            RuleFor(x => x.AccountId).NotEmpty().WithMessage("AccountId is required.");

            RuleFor(x => x.IsUnlimited).NotNull().WithMessage("IsUnlimited is required.");

            RuleFor(x => x.ExpirationDate)
                .NotNull()
                .When(x => !x.IsUnlimited)
                .WithMessage("ExpirationDate is required when IsUnlimited is false.");
        }
    }
}
