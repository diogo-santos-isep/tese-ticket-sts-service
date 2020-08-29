namespace DAL.Repositories.Implementations
{
    using DAL.Repositories.Interfaces;
    using Models.Domain;
    using MongoDB.Driver;
    using System.Collections.Generic;

    public class UserRepository : IUserRepository
    {
        private readonly string COLLECTIONNAME = "Users";
        private readonly IMongoCollection<User> _collection;

        public UserRepository(IMongoDatabase database)
        {
            _collection = database.GetCollection<User>(COLLECTIONNAME);
        }
        public User Create(User model)
        {
            _collection.InsertOne(model);
            return model;
        }

        public void Delete(string id) =>
            _collection.DeleteOne(book => book.Id == id);

        public List<User> Get() =>
            _collection.Find(book => true).ToList();

        public User Get(string id) =>
            _collection.Find<User>(book => book.Id == id).FirstOrDefault();

        public User GetByEmail(string email) =>
            _collection.Find<User>(book => book.Email == email).FirstOrDefault();

        public void Update(string id, User model) =>
            _collection.ReplaceOne(book => book.Id == id, model);
    }
}
