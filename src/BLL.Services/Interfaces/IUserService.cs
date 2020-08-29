namespace BLL.Services.Interfaces
{
    using Models.Domain;

    public interface IUserService : IService<User>
    {
        User GetByEmail(string email);
    }
}
