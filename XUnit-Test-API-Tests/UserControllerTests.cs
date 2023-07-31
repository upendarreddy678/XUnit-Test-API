using ApplicationSrv.iRepo;
using Controllers;
using DomainSrv.User;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace XUnit_Test_API_Tests
{
    public class UserControllerTests
    {
        private Mock<IUserRepository> _mockRepo;
        private UserController _controller;

        public UserControllerTests()
        {
            _mockRepo = new Mock<IUserRepository>();
            _controller = new UserController(_mockRepo.Object);
        }
        [Fact]
        public void CreateTest()
        {
            var ReqBody = new CreateUpdateUserDto
            {
                Name = "Upender"
            };
            _mockRepo.Setup(repo => repo.Create(ReqBody)).Returns("GuId");
            var result = _controller.Create(ReqBody);
            Assert.IsType<CreatedResult>(result);
        }

        [Fact]
        public void UpdateModifiedTest()
        {
            var ReqBody = new CreateUpdateUserDto
            {
                Name = "Upender"
            };
            string Id = "GuId";
            _mockRepo.Setup(repo => repo.IsUserIdExist(Id)).Returns(true);
            _mockRepo.Setup(repo => repo.Update(Id, ReqBody)).Returns(true);
            var result = _controller.Update(Id, ReqBody);
            Assert.IsType<NoContentResult>(result);
        }
        [Fact]
        public void UpdateNotFoundTest()
        {
            var ReqBody = new CreateUpdateUserDto
            {
                Name = "Upender"
            };
            string Id = "GuId";
            _mockRepo.Setup(repo => repo.IsUserIdExist(Id)).Returns(false);
            var result = _controller.Update(Id, ReqBody);
            Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public void GetByIdSuccessTest()
        {
            string Id = "GuId";

            var RespBody = new UserDto
            {
                Id = Id,
                Name = "Upender"
            };
            _mockRepo.Setup(repo => repo.GetById(Id)).Returns(RespBody);
            var result = _controller.GetById(Id);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public void GetByIdNotFoundTest()
        {
            string Id = "GuId";
            UserDto? RespBody = null;
            _mockRepo.Setup(repo => repo.GetById(Id)).Returns(RespBody);
            var result = _controller.GetById(Id);
            Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public void GetListSuccessTest()
        {
            _mockRepo.Setup(repo => repo.GetList()).Returns(new List<UserDto>());
            var result = _controller.GetList();
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public void DeleteNotFoundTest()
        {
            string Id = "GuId";
            _mockRepo.Setup(repo => repo.IsUserIdExist(Id)).Returns(false);
            var result = _controller.Delete(Id);
            Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public void DeleteSuccessTest()
        {
            string Id = "GuId";
            _mockRepo.Setup(repo => repo.IsUserIdExist(Id)).Returns(true);
            var result = _controller.Delete(Id);
            Assert.IsType<NoContentResult>(result);
        }
    }
}