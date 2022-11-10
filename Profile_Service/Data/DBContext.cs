using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using Profile_Service.Entities;


namespace Profile_Service.Entities
{
    public class DBContext
    {
        public DBContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("MongoDb:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("MongoDb:DatabaseName"));

            Users = database.GetCollection<User>("Users");
            Education = database.GetCollection<Education>("Education");
            Themes = database.GetCollection<Themes>("Themes");

        }

        public IMongoCollection<User> Users { get; }
        public IMongoCollection<Education> Education { get; }
        public IMongoCollection<Themes> Themes { get; }
    }
}