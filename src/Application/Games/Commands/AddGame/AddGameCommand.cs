using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BerkeGaming.Application.Common.Interfaces;
using BerkeGaming.Application.Common.Models;
using BerkeGaming.Domain.Entities.Games;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BerkeGaming.Application.Games.Commands.AddGame
{
    public class AddGameCommand : IRequest<bool>
    {
        public GameDto Game { get; set; }
    }

    public class AddGameCommandHandler : IRequestHandler<AddGameCommand, bool>
    {
        private readonly IApplicationDbRepository _repository;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly ILogger<AddGameCommandHandler> _logger;

        public AddGameCommandHandler(IApplicationDbRepository repository, IMapper mapper,
            ICurrentUserService currentUserService, ILogger<AddGameCommandHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _currentUserService = currentUserService;
            _logger = logger;
        }

        public async Task<bool> Handle(AddGameCommand request, CancellationToken cancellationToken)
        {
            var game = _mapper.Map<Game>(request.Game);
            var result = await _repository.AddGame(_currentUserService.UserName, game);
            return result.Succeeded;
        }
    }
}
