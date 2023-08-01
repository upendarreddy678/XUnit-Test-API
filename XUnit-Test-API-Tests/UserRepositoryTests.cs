using ApplicationSrv.Repo;
using DataSrv.Entities;
using DomainSrv.User;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace XUnit_Test_API_Tests
{
    public class UserRepositoryTests
    {

        private readonly Mock<UserDbContext> _mockDB;
        private UserRepository _repo;

        public UserRepositoryTests()
        {
            var data = new List<UserDetails>
            {
                new UserDetails{Id="GuId1",Name="Test1"},
                new UserDetails{Id="GuId2",Name="Test2"},
                new UserDetails{Id="GuId3",Name="Test3"}
            }.AsQueryable();
            var mockSet = new Mock<DbSet<UserDetails>>();
            mockSet.As<IQueryable<UserDetails>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<UserDetails>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<UserDetails>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<UserDetails>>().Setup(x => x.GetEnumerator()).Returns(() => data.GetEnumerator());

            _mockDB = new Mock<UserDbContext>();
            _mockDB.Setup(y=>y.userDetails).Returns(mockSet.Object);


            _repo = new UserRepository(_mockDB.Object);
        }


        [Fact]
        public void IsUserIdExistTest()
        {
            var result = _repo.IsUserIdExist("GuId1");
            Assert.IsType<bool>(result);
            Assert.True(result);
        }
        [Fact]
        public void IsUserIdNotExistTest()
        {
            var result = _repo.IsUserIdExist("test15");
            Assert.IsType<bool>(result);
            Assert.False(result);
        }
        [Fact]
        public void CreateSuccess()
        {
            var InP = new CreateUpdateUserDto()
            {
                Name = "Test"
            };
            var result = _repo.Create(InP);
            Assert.IsType<string>(result);
            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }
        [Fact]
        public void UpdateSuccess()
        {
            var InP = new CreateUpdateUserDto()
            {
                Name = "Test"
            };
            var result = _repo.Update("GuId1", InP);
            Assert.IsType<bool>(result);
            Assert.True(result);
        }
    }
}
