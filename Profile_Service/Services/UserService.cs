using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Options;
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

        public async Task<UserDTO> CreateUser(User user)
        {
            await _context.Users.InsertOneAsync(user);
            return new UserDTO(user);
        }

        public async Task<string> DeleteUser(string userId)
        {
            await _context.Users.DeleteOneAsync(x => x.Id == userId);
            return userId;
        }

        public async Task<UserDTO> GetUserByID(string userId)
        {
            var user = await _context.Users.Find(x => x.Id == userId).FirstOrDefaultAsync();
            if (user == null)
            {
                throw new Exception("User does not exist");
            }
            return new UserDTO(user);
        }

        public async Task<IEnumerable<UserDTO>> GetUsers()
        {
            var result = await _context.Users.Find(_ => true).ToListAsync();
            return result.Select(x => new UserDTO(x)).ToList();
        }

        public async Task<UserDTO> UpdateUser(User user)
        {
            await _context.Users.ReplaceOneAsync(x => x.Id == user.Id, user);
            return new UserDTO(user);
        }
    }
}
