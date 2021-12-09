using Xunit;
using YoghurtBank.Services;
using YoghurtBank.Infrastructure;
using YoghurtBank.Data.Model;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Net;
using Moq;

namespace ServicesTests
{

    public class UserValidatorTests 
    {
        private readonly IUserValidator _validator; 
        private readonly Mock<IUserRepository> _repoMock;
        public UserValidatorTests()
        {
             _repoMock = new Mock<IUserRepository>();
             _validator = new UserValidator(_repoMock.Object);
        }

        [Fact]
        public async Task UserWithEmailExists_given_existing_user_returns_true()
        {
            var user = new UserDetailsDTO
            {
                Id = 1,
                Email = "omikron@omikron.dk", 
                UserName = "M-ette",
                UserType = "Supervisor"
            };
            _repoMock.Setup(r => r.FindUserByEmail("omikron@omikron.dk")).ReturnsAsync(user);
            var result = await _validator.UserWithEmailExists("omikron@omikron.dk");
            Assert.True(result);
        }

        [Fact]
        public async Task UserWithEmailExists_given_non_existing_user_returns_false()
        {
            UserDetailsDTO user = null; 
            _repoMock.Setup(r => r.FindUserByEmail("omikron@omikron.dk")).ReturnsAsync(user);
            var result = await _validator.UserWithEmailExists("omikron@omikron.dk");
            Assert.False(result);
        }
    }
}