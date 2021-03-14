using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BerkeGaming.Application.Common.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BerkeGaming.Application.Games.Commands.DeleteGameForUser
{
    public class DeleteGameForUserCommand : IRequest<bool>
    {
        [Required]
        public int GameId { get; set; }
    }

    public class DeleteGameForUserCommandHandler : IRequestHandler<DeleteGameForUserCommand, bool>
    {
        private readonly IApplicationDbRepository _repository;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly ILogger<DeleteGameForUserCommand> _logger;

        public DeleteGameForUserCommandHandler(IApplicationDbRepository repository, IMapper mapper,
            ICurrentUserService currentUserService, ILogger<DeleteGameForUserCommand> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _currentUserService = currentUserService;
            _logger = logger;
        }

        public async Task<bool> Handle(DeleteGameForUserCommand request, CancellationToken cancellationToken)
        {
            var result = await _repository.DeleteGameForUser(request.GameId, _currentUserService.UserName);

            return result.Succeeded;
        }
    }
}
