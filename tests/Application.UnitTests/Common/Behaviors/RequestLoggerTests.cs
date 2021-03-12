using BerkeGaming.Application.Common.Interfaces;
using Moq;

namespace BerkeGaming.Application.UnitTests.Common.Behaviors
{
    public class RequestLoggerTests
    {
        private readonly Mock<ICurrentUserService> _currentUserService;
        private readonly Mock<IIdentityService> _identityService;


        public RequestLoggerTests()
        {

            _currentUserService = new Mock<ICurrentUserService>();

            _identityService = new Mock<IIdentityService>();
        }

        //[Test]
        //public async Task ShouldCallGetUserNameAsyncOnceIfAuthenticated()
        //{
        //    _currentUserService.Setup(x => x.UserId).Returns("Administrator");

        //    var requestLogger = new LoggingBehaviour<CreateTodoItemCommand>(_logger.Object, _currentUserService.Object, _identityService.Object);

        //    await requestLogger.Process(new CreateTodoItemCommand { ListId = 1, Title = "title" }, new CancellationToken());

        //    _identityService.Verify(i => i.GetUserNameAsync(It.IsAny<string>()), Times.Once);
        //}

        //[Test]
        //public async Task ShouldNotCallGetUserNameAsyncOnceIfUnauthenticated()
        //{
        //    var requestLogger = new LoggingBehaviour<CreateTodoItemCommand>(_logger.Object, _currentUserService.Object, _identityService.Object);

        //    await requestLogger.Process(new CreateTodoItemCommand { ListId = 1, Title = "title" }, new CancellationToken());

        //    _identityService.Verify(i => i.GetUserNameAsync(null), Times.Never);
        //}
    }
}
