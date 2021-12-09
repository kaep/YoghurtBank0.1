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

        private readonly Mock<ICollaborationRequestRepository> _repoMock;
        private readonly CollaborationRequestController _controller;

        public CollaborationRequestControllerTests()
        {
            var logMock = new Mock<ILogger<CollaborationRequestController>>();
            _repoMock = new Mock<ICollaborationRequestRepository>();
            _controller = new CollaborationRequestController(logMock.Object, _repoMock.Object);
        }

        [Fact]
        public async Task Get_returns_two_requests()
        {
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
            var result = await _controller.Get();

            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task FindByStudent_returns_students_requests_from_repo()
        {
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
            var requests = new List<CollaborationRequestDetailsDTO>{cb1, cb2}.AsReadOnly();
            _repoMock.Setup(m => m.FindRequestsByStudentAsync(1)).ReturnsAsync(requests);

            var result = await _controller.GetRequestsByUser(false, 1);

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(cb1, result.ElementAt(0));
            Assert.Equal(cb2, result.ElementAt(1));
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
            var toCreate = new CollaborationRequestCreateDTO();
            _repoMock.Setup(m => m.CreateAsync(toCreate)).ReturnsAsync(cb1);


            var result = await _controller.Post(toCreate) as CreatedAtActionResult;

            Assert.Equal(cb1, result?.Value);
            Assert.Equal("Get", result?.ActionName);
            //denne her har application og science fordi Get inde i controlleren bruger application -> det skal laves om til id 
            Assert.Equal(KeyValuePair.Create("Application", (object?)"Science"), result?.RouteValues?.Single());
        }
    }
} 