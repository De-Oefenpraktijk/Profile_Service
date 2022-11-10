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
            Institutions = database.GetCollection<Institutions>("Institutions");
            Themes = database.GetCollection<Specialization>("Themes");

        }

        public IMongoCollection<User> Users { get; }
        public IMongoCollection<Institutions> Institutions { get; }
        public IMongoCollection<Specialization> Themes { get; }
    }
}