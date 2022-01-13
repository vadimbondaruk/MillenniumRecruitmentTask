using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MillenniumRecruitmentTask.Api.Controllers;
using MillenniumRecruitmentTask.Api.Data.Entities;
using MillenniumRecruitmentTask.Api.Data.Interfaces;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace MillenniumRecruitmentTask.Api.Tests
{
    public class UsersControllerTests
    {
        private Mock<IUserRepository> InjectedUserRepository { get; set; }

        private Mock<ILogger<UsersController>> InjectedLogger { get; set; }

        private readonly UsersController usersController;

        public UsersControllerTests()
        {
            InjectedUserRepository = new Mock<IUserRepository>();

            InjectedLogger = new Mock<ILogger<UsersController>>();

            usersController = new UsersController(InjectedLogger.Object, InjectedUserRepository.Object);
        }

        [Fact]
        public async Task CreateAsync_should_returns_bad_request_when_name_is_empty()
        {
            var result = await usersController.CreateAsync(string.Empty, CancellationToken.None);

            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task CreateAsync_should_returns_ok_when_name_is_correct()
        {
            var result = await usersController.CreateAsync("some name", CancellationToken.None);

            result.Should().BeOfType<OkResult>();
        }

        [Fact]
        public async Task UpdateAsync_should_returns_bad_request_when_name_is_empty()
        {
            var result = await usersController.UpdateAsync(new Data.Entities.User { Id = 1, Name = string.Empty}, CancellationToken.None);

            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task UpdateAsync_should_returns_not_found_when_user_not_found()
        {
            InjectedUserRepository.Setup(x => x.FindByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult((User)null));

            var result = await usersController.UpdateAsync(new Data.Entities.User { Id = 1, Name = "some name" }, CancellationToken.None);

            result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async Task UpdateAsync_should_returns_ok_when_user_exists_and_input_data_is_valid()
        {
            InjectedUserRepository.Setup(x => x.FindByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(new User()));

            var result = await usersController.UpdateAsync(new Data.Entities.User { Id = 1, Name = "some name" }, CancellationToken.None);

            result.Should().BeOfType<OkResult>();
        }
    }
}
