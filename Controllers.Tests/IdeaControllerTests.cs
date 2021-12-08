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
        //skal laves om hvis vi vælger at bruge actionresults?
        [Fact]
        public async Task GetById_given_non_existing_id_returns_null()
        {
            #region Arrange
            var id = 666;
            IdeaDetailsDTO idea = null;

            var logMock = new Mock<ILogger<IdeaController>>();
            var repoMock = new Mock<IIdeaRepository>();
            repoMock.Setup(m => m.FindIdeaDetailsAsync(id)).ReturnsAsync(idea);
            var controller = new IdeaController(logMock.Object, repoMock.Object);
            #endregion

            #region Act
            var result = await controller.GetById(id);
            #endregion

            #region Assert
            Assert.Null(result);
            #endregion
        }

        [Fact]
        public async Task GetById_given_existing_id_returns_detailsDTO()
        {
            #region Arrange
            var id = 2;

            var IdeaDetailsDTO = new IdeaDetailsDTO
            {
                CreatorId = 1,
                Id = 2,
                Title = "Data Intelligence is good",
                Subject = "Data Intelligence",
                Posted = DateTime.Now,
                Description = "Data Intelligence gives value",
                AmountOfCollaborators = 1,
                Open = true,
                TimeToComplete = DateTime.Today - DateTime.Today,
                StartDate = DateTime.Now,
                Type = IdeaType.PhD
            };

            var logMock = new Mock<ILogger<IdeaController>>();
            var repoMock = new Mock<IIdeaRepository>();
            repoMock.Setup(m => m.FindIdeaDetailsAsync(id)).ReturnsAsync(IdeaDetailsDTO);
            var controller = new IdeaController(logMock.Object, repoMock.Object);
            #endregion

            #region Act
            var result = await controller.GetById(id);
            #endregion

            #region Assert
            Assert.NotNull(result);
            Assert.Equal(IdeaDetailsDTO, result);
            #endregion
        }
    }
}