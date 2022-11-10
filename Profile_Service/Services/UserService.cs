using AutoMapper;
using EventBus.Messages.Events;
using Microsoft.CodeAnalysis;
using MongoDB.Driver;
using Profile_Service.DTO;
using Profile_Service.Entities;



namespace Profile_Service.Services
{
    public class UserService
    {
        private readonly DBContext _context;
        private readonly IMapper _mapper;



        public UserService(DBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }



        public async Task<UserDTO> CreateUser(UserDTO _user)
        {
            var user = _mapper.Map<User>(_user);
            user.UserId = Guid.NewGuid().ToString();
            user.Role = "User";



            await _context.Users.InsertOneAsync(user);

            return _mapper.Map<UserDTO>(user);
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



            return _mapper.Map<UserDTO>(user);
        }



        public async Task<UserDTO> UpdateUser(UserDTO _user, string _userId)
        {
            var user = _mapper.Map<User>(_user);
            user.UserId = _userId;



            await _context.Users.ReplaceOneAsync(x => x.UserId == _userId, user);

            return _mapper.Map<UserDTO>(user);
        }
    }
}