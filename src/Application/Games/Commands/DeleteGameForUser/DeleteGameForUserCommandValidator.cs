using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace BerkeGaming.Application.Games.Commands.DeleteGameForUser
{
    public class DeleteGameForUserCommandValidator : AbstractValidator<DeleteGameForUserCommand>
    {
        public DeleteGameForUserCommandValidator()
        {
            RuleFor(v => v.GameId)
                .NotEmpty();
        }
    }
}
