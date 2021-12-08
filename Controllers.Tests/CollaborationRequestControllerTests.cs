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
    public class CollaborationRequestControllerTests
    {

        [Fact]
        public async Task Get_returns_two_requests()
        {
            var logger = new Mock<ILogger<CollaborationRequestController>>();
            var repository = new Mock<ICollaborationRequestRepository>();

            var cb1 = new CollaborationRequestDetailsDTO
            {
                StudentId = 1,
                SupervisorId = 2,
                Application = "Science",
                Status = CollaborationRequestStatus.Waiting
            };
            var cb2 = new CollaborationRequestDetailsDTO
            {
                StudentId = 3,
                SupervisorId = 4,
                Application = "Not Science",
                Status = CollaborationRequestStatus.Waiting
            };
            var expected = new List<CollaborationRequestDetailsDTO> { cb1, cb2};
            var controller = new CollaborationRequestController(logger.Object, repository.Object);

            var result = await controller.Get();

            Assert.Equal(expected, result);
        }


        [Fact]
        public async Task Create_creates_request()
        {
            var cb1 = new CollaborationRequestDetailsDTO
            {
                StudentId = 1,
                SupervisorId = 2,
                Application = "Science",
                Status = CollaborationRequestStatus.Waiting
            };
            var logger = new Mock<ILogger<CollaborationRequestController>>();
            var repository = new Mock<ICollaborationRequestRepository>();
            var toCreate = new CollaborationRequestCreateDTO();
            repository.Setup(m => m.CreateAsync(toCreate)).ReturnsAsync(cb1);

            var controller = new CollaborationRequestController(logger.Object, repository.Object);

            var result = await controller.Post(toCreate) as CreatedAtActionResult;

            Assert.Equal(cb1, result?.Value);
            Assert.Equal("Get", result?.ActionName);
            //denne her har application og science fordi Get inde i controlleren bruger application -> det skal laves om til id 
            Assert.Equal(KeyValuePair.Create("Application", (object?)"Science"), result?.RouteValues?.Single());
        }


    }
} 