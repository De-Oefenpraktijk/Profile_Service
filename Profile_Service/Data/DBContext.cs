using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
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



            MongoCollectionSettings collectionSettings = new MongoCollectionSettings()
            {
                GuidRepresentation = GuidRepresentation.Standard,
            };



            Users = database.GetCollection<User>("Users", collectionSettings);
            Institutions = database.GetCollection<Institutions>("Institutions");
            Themes = database.GetCollection<Themes>("Themes");



        }



        public IMongoCollection<User> Users { get; }
        public IMongoCollection<Institutions> Institutions { get; }
        public IMongoCollection<Themes> Themes { get; }
    }
}