using Microsoft.CodeAnalysis;
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
                UserId = Guid.NewGuid().ToString(),
                Username = _user.Username,
                FirstName = _user.FirstName,
                LastName = _user.LastName,
                EmailAddress = _user.EmailAddress,
                Password = _user.Password,
                EnrollmentDate = _user.EnrollmentDate,
                Role = "User",
                Institutions = _user.Institutions,
                Themes = _user.Themes,
                ResidencePlace = _user.ResidencePlace
            };


            await _context.Users.InsertOneAsync(user);
            return user;
        }

        public async Task<string> DeleteUser(string userId)
        {
            await _context.Users.DeleteOneAsync(x => x.UserId == userId);
            return userId;
        }

        public async Task<UserDTO> GetUserByID(string userId)
        {
            var user = await _context.Users.Find(x => x.UserId == userId).FirstOrDefaultAsync();
            if (user == null)
            {
                throw new Exception("User does not exist");
            }
            return new UserDTO()
            {
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                EmailAddress = user.EmailAddress,
                Password = user.Password,
                EnrollmentDate = user.EnrollmentDate,
                Role = user.Role,
                Institutions = user.Institutions,
                Themes = user.Themes,
                ResidencePlace = user.ResidencePlace
            };
        }

        public async Task<UserDTO> UpdateUser(UserDTO _user, string _userId)
        {
            var user = new User
            {
                UserId = _userId,
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
            await _context.Users.ReplaceOneAsync(x => x.UserId == _userId, user);
            return new UserDTO()
            {
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                EmailAddress = user.EmailAddress,
                Password = user.Password,
                EnrollmentDate = user.EnrollmentDate,
                Role = user.Role,
                Institutions = user.Institutions,
                Themes = user.Themes,
                ResidencePlace = user.ResidencePlace
            };
        }
    }
}
