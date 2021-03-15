using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace BerkeGaming.Application.Games.Commands.AddGameToUser
{
    public class AddGameToUserCommandValidator : AbstractValidator<AddGameToUserCommand>
    {
        public AddGameToUserCommandValidator()
        {
            RuleFor(v => v.GameId).NotNull();
        }
    }
}
