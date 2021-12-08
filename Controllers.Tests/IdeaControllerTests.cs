using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using YoghurtBank.Controllers;
using YoghurtBank.Data.Model;
using YoghurtBank.Services;

namespace YoghurtBank.ControllerTests
{
    public class IdeaControllerTests
    {

        [Fact]
        public async Task GetById_given_non_existing_id_returns_notfound()
        {
            #region Arrange
            IdeaDetailsDTO id = null;

            var logMock = new Mock<ILogger<IdeaController>>();
            var repoMock = new Mock<IIdeaRepository>();
            repoMock.Setup(m => m.FindIdeaDetailsAsync(420)).ReturnsAsync(id);
            var controller = new IdeaController(logMock.Object, repoMock.Object);
            #endregion

            #region Act
            var result = await controller.GetById(420);
            #endregion

            #region Assert
            Assert.NotNull(result);
            Assert.Equal(NotFoundResult, result);
            #endregion
        }

    }
} 