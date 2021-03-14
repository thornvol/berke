using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BerkeGaming.Application.Common.Interfaces;
using BerkeGaming.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BerkeGaming.Application.Games.Queries
{
    public class GetGamesQuery : IRequest<IList<GameDto>>
    {
    }

    public class GetGamesQueryHandler : IRequestHandler<GetGamesQuery, IList<GameDto>>
    {
        private readonly IApplicationDbRepository _repository;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly ILogger<GetGamesQueryHandler> _logger;

        public GetGamesQueryHandler(IApplicationDbRepository repository, IMapper mapper,
            ICurrentUserService currentUserService, ILogger<GetGamesQueryHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _currentUserService = currentUserService;
            _logger = logger;
        }

        public async Task<IList<GameDto>> Handle(GetGamesQuery request, CancellationToken cancellationToken)
        {
            var games = await _repository.GetGamesForUser(_currentUserService.UserName);

            return await games.ProjectTo<GameDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
        }
    }
}
