using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BerkeGaming.Application.Common.Interfaces;
using BerkeGaming.Domain.Entities.Games;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BerkeGaming.Application.Games.Commands.AddGame
{
    public class AddGameCommand : IRequest<bool>
    {
        public string Name { get; set; }

        public DateTimeOffset ReleaseDate { get; set; }

        public string Overview { get; set; }

        public int PublisherId { get; set; }

        public int[] GenreIds { get; set; }
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
            // Get genres
            var genres = await _repository.GetGenresById(request.GenreIds);
            
            // get publisher
            var publisher = await _repository.GetPublisherById(request.PublisherId);

            // Map to domain object Game
            var game = new Game {
                Name = request.Name,
                ReleaseDate = request.ReleaseDate,
                Overview = request.Overview,
                Publisher = publisher,
                Genres = genres,
                CreatedUserId = _currentUserService.UserName
            };

            // Add game
            var result = await _repository.AddGame(_currentUserService.UserName, game);
            
            return result.Succeeded;
        }
    }
}
