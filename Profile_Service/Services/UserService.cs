using AutoMapper;
using EventBus.Messages.Events;
using MassTransit;
using Microsoft.CodeAnalysis;
using MongoDB.Bson;
using MongoDB.Driver;
using Profile_Service.DTO;
using Profile_Service.Entities;



namespace Profile_Service.Services
{
    public class UserService
    {
        private readonly DBContext _context;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publish;

        public UserService(DBContext context, IMapper mapper, IPublishEndpoint publish)
        {
            _context = context;
            _mapper = mapper;
            _publish = publish;
        }

        public async Task<UserDTO> CreateUser(UserDTO _user)
        {
            var user = _mapper.Map<User>(_user);
            user.Role = "User";

            await _context.Users.InsertOneAsync(user);

            var result = _mapper.Map<UserDTO>(user);

            var message = _mapper.Map<ProfileUpdatedEvent>(result);
            await _publish.Publish(message);// Hier zit de error, of dit is de reden voor het lange wachten

            return result;
        }

        public async Task<string> DeleteUser(string userId)
        {
            var objectId = ObjectId.Parse(userId);
            
            await _context.Users.DeleteOneAsync(x => x.Id == userId);
            
            return userId;
        }

        public async Task<UserDTO> GetUserByID(string userId)
        {
            var objectId = ObjectId.Parse(userId);
            
            var user = await _context.Users.Find(x => x.Id == userId).FirstOrDefaultAsync();
            if (user == null)
            {
                throw new Exception("User does not exist");
            }

            return _mapper.Map<UserDTO>(user);
        }

        public async Task<UserDTO> UpdateUser(UserDTO _user, string Id)
        {
            var objectId = ObjectId.Parse(Id);
            var user = _mapper.Map<User>(_user);
            user.Id = Id;

            await _context.Users.ReplaceOneAsync(x => x.Id == Id, user);

            var result = _mapper.Map<UserDTO>(user);

            var message = _mapper.Map<ProfileUpdatedEvent>(result);
            await _publish.Publish(message);

            return result;
        }
    }
}