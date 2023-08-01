using ApplicationSrv.iRepo;
using DataSrv.Entities;
using DomainSrv.User;

namespace ApplicationSrv.Repo
{
    public class UserRepository : IUserRepository
    {
        private UserDbContext _dbContext;
        public UserRepository(UserDbContext Dbcon)
        {
            _dbContext = Dbcon;
        }
        public bool IsUserIdExist(string Id) =>
            _dbContext.userDetails.Any(a => a.Id == Id);
        public string Create(CreateUpdateUserDto InP)
        {
            var User = new UserDetails
            {
                Id = Guid.NewGuid().ToString(),
                Name = InP.Name,
            };
            _dbContext.userDetails.Add(User);
            _dbContext.SaveChanges();
            return User.Id;
        }
        public bool Update(string Id, CreateUpdateUserDto InP)
        {
            var user = new UserDetails
            {
                Id = Id,
                Name = InP.Name
            };
            _dbContext.userDetails.Attach(user);
            _dbContext.Entry(user).Property(x => x.Name).IsModified = true;
            return _dbContext.SaveChanges() > 0;
        }
        public UserDto? GetById(string Id)
        {
            var Response = _dbContext.userDetails.Where(x => x.Id == Id).Select(y => new UserDto
            {
                Id = y.Id,
                Name = y.Name
            }).FirstOrDefault();
            return Response;
        }
        public List<UserDto> GetList()
        {
            var Response = _dbContext.userDetails.Select(y => new UserDto
            {
                Id = y.Id,
                Name = y.Name
            }).ToList();
            return Response;
        }
        public bool Delete(string Id)
        {
            var user = new UserDetails
            {
                Id = Id
            };
            _dbContext.userDetails.Remove(user);
            return _dbContext.SaveChanges() > 0;
        }
    }
}
