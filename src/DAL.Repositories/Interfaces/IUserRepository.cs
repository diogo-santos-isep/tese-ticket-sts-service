namespace DAL.Repositories.Interfaces
{
    using Models.Domain;
    public interface IUserRepository : IRepository<User>
    {
        User GetByEmail(string username);
    }
}
