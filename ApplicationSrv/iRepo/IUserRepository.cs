using DomainSrv.User;

namespace ApplicationSrv.iRepo
{
    public interface IUserRepository
    {
        bool IsUserIdExist(string Id);
        string Create(CreateUpdateUserDto InP);
        bool Update(string Id, CreateUpdateUserDto InP);
        UserDto? GetById(string Id);
        List<UserDto> GetList();
        bool Delete(string Id);
    }
}
