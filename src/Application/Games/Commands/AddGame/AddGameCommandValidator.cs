using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace BerkeGaming.Application.Games.Commands.AddGame
{
    public class AddGameCommandValidator : AbstractValidator<AddGameCommand>
    {
        public AddGameCommandValidator()
        {
            RuleFor(v => v.Name)
                .MaximumLength(256)
                .NotEmpty();

            RuleFor(v => v.Overview)
                .MaximumLength(4000)
                .NotEmpty();

            RuleFor(v => v.ReleaseDate)
                .NotEmpty();

            RuleFor(v => v.PublisherId)
                .NotEmpty();

            RuleFor(v => v.GenreIds)
                .NotEmpty();
        }
    }
}
