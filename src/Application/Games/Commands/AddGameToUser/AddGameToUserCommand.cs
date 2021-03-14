using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BerkeGaming.Application.Common.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BerkeGaming.Application.Games.Commands.AddGameToUser
{
    public class AddGameToUserCommand : IRequest<bool>
    {
        public int GameId { get; set; }
    }

    public class AddGameToUserCommandHandler : IRequestHandler<AddGameToUserCommand, bool>
    {
        private readonly IApplicationDbRepository _repository;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly ILogger<AddGameToUserCommand> _logger;

        public AddGameToUserCommandHandler(IApplicationDbRepository repository, IMapper mapper,
            ICurrentUserService currentUserService, ILogger<AddGameToUserCommand> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _currentUserService = currentUserService;
            _logger = logger;
        }

        public async Task<bool> Handle(AddGameToUserCommand request, CancellationToken cancellationToken)
        {
            var result = await _repository.AddGameToUser(request.GameId, _currentUserService.UserName);

            return result.Succeeded;
        }
    }
}
