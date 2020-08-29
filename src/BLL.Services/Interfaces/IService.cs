namespace BLL.Services.Interfaces
{
    using Models.Domain;
    using System.Collections.Generic;
    public interface IService<T> where T : IMongoEntity
    {
        T Create(T model);

        T Update(string id, T model);

        List<T> Get();

        T Get(string id);
        void Delete(string id);
    }
}
