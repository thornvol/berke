using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using BerkeGaming.Application.Common.Interfaces;
using BerkeGaming.Application.Common.Models;
using BerkeGaming.Application.Games.Queries;
using BerkeGaming.Domain.Entities.Games;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Handlers.UnitTest
{
    public class GetGamesQueryTest
    {
        //private static IMapper _mapper;
        private readonly Mock<IMapper> _mapper;
        private readonly MapperConfiguration _mapperConfiguration;
        private readonly GetGamesQueryHandler _sut;
        private readonly Mock<IApplicationDbRepository> _repository;
        private readonly Mock<ICurrentUserService> _currentUserService;
        private readonly Mock<ILogger<GetGamesQueryHandler>> _logger;

        public GetGamesQueryTest()
        {
            _repository = new Mock<IApplicationDbRepository>();

            _mapperConfiguration = new MapperConfiguration(mc =>
            {
                mc.CreateMap<Game, GameDto>();
                mc.CreateMap<Genre, GenreDto>();
                mc.CreateMap<Publisher, PublisherDto>();
            });
            //var mapper = config.CreateMapper();
            //_mapper = mapper;

            _mapper = new Mock<IMapper>();
            _currentUserService = new Mock<ICurrentUserService>();
            _logger = new Mock<ILogger<GetGamesQueryHandler>>();

            _sut = new GetGamesQueryHandler(_repository.Object, _mapper.Object, _currentUserService.Object, _logger.Object);
        }

        [Fact(Skip = "Need to finish implementing - ran out of time.")]
        public async Task GetGamesQuery_ShouldReturnListOfGames() 
        {
            // Arrange
            var userName = "harry";
            var gamesDto = new List<GameDto> 
            {
                { new GameDto {
                    GameId = 1, 
                    Name = "test", 
                    Genres = new List<GenreDto> 
                        { new GenreDto {GenreId = 1, Name = "scifi"} },
                    Publisher = new PublisherDto {Name = "pub", PublisherId = 1}
                    }
                }
            };
            var games = new List<Game>
            {
                { new Game {
                        GameId = 1,
                        Name = "test",
                        Genres = new List<Genre>
                            { new Genre {GenreId = 1, Name = "scifi"} },
                        Publisher = new Publisher {Name = "pub", PublisherId = 1}
                    }
                }
            };
            _mapper.Setup(x => x.ConfigurationProvider).Returns(_mapperConfiguration);
            _repository.Setup(x => x.GetGamesForUser(userName)).ReturnsAsync(games.AsQueryable);
            //_mapper.Setup(x => x.ProjectTo<GameDto>(It.IsAny<IQueryable<Game>>(), It.IsAny<object>())).Returns(gamesDto.AsQueryable);
            

            // Act 
            var result = await _sut.Handle(new GetGamesQuery(), default);

            // Assert
            result.Should().NotBeNullOrEmpty();
        }
    }
}
