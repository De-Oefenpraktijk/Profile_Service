using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using MongoDB.Driver;
using Profile_Service.DTO;
using Profile_Service.Entities;

namespace Profile_Service.Services
{
    public class UserService
    {
        private readonly DBContext _context;
        
        public UserService(DBContext context)
        {
            _context = context;
        }

        public async Task<User> CreateUser(UserDTO _user)
        {
            var user = new User
            {
                Id = new Guid(),
                Username = _user.Username,
                FirstName = _user.FirstName,
                LastName = _user.LastName,
                EmailAddress = _user.EmailAddress,
                Password = _user.Password,
                EnrollmentDate = _user.EnrollmentDate,
                Role = _user.Role,
                Institutions = _user.Institutions,
                Themes = _user.Themes,
                ResidencePlace = _user.ResidencePlace
            };


            await _context.Users.InsertOneAsync(user);
            return user;
        }

        //public async Task<string> DeleteUser(string userId)
        //{
        //    await _context.Users.DeleteOneAsync(x => x.Id == userId);
        //    return userId;
        //}

        //public async Task<UserDTO> GetUserByID(string userId)
        //{
        //    var user = await _context.Users.Find(x => x.Id == userId).FirstOrDefaultAsync();
        //    if (user == null)
        //    {
        //        throw new Exception("User does not exist");
        //    }
        //    return new UserDTO(user);
        //}

        //public async Task<IEnumerable<UserDTO>> GetUsers()
        //{
        //    var result = await _context.Users.Find(_ => true).ToListAsync();
        //    return result.Select(x => new UserDTO(x)).ToList();
        //}

        public async Task<User> UpdateUser(UserDTO _user)
        {
            var user = new User
            {
                Username = _user.Username,
                FirstName = _user.FirstName,
                LastName = _user.LastName,
                EmailAddress = _user.EmailAddress,
                Password = _user.Password,
                EnrollmentDate = _user.EnrollmentDate,
                Role = _user.Role,
                Institutions = _user.Institutions,
                Themes = _user.Themes,
                ResidencePlace = _user.ResidencePlace
            };
            await _context.Users.ReplaceOneAsync(x => x.Id == user.Id, user);
            return user;
        }
    }
}
