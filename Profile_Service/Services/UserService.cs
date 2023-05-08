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

        public async Task<OutputUserDTO> CreateUser(InputUserDTO _user)
        {
            User user = _mapper.Map<InputUserDTO, User>(_user);
            user.Role = "User";

            await _context.Users.InsertOneAsync(user);

            OutputUserDTO result = _mapper.Map<User, OutputUserDTO>(user);

            ProfileUpdatedEvent message = _mapper.Map<OutputUserDTO, ProfileUpdatedEvent>(result);
            await _publish.Publish(message);// Hier zit de error, of dit is de reden voor het lange wachten

            return result;
        }

        public async Task<string> DeleteUser(string userId)
        {
            ObjectId objectId = ObjectId.Parse(userId);
            
            await _context.Users.DeleteOneAsync(x => x.Id == userId);
            
            return userId;
        }

        public async Task<OutputUserDTO> GetUserByID(string userId)
        {
            ObjectId objectId = ObjectId.Parse(userId);
            
            User user = await _context.Users.Find(x => x.Id == userId).FirstOrDefaultAsync();
            if (user == null)
            {
                throw new Exception("User does not exist");
            }

            return _mapper.Map<User, OutputUserDTO>(user);
        }

        public async Task<OutputUserDTO> GetUserByEmail(string email)
        {
            User user = await _context.Users.Find(x => x.Email == email).FirstOrDefaultAsync();
            if (user == null)
            {
                throw new Exception("User does not exist");
            }

            return _mapper.Map<User, OutputUserDTO>(user);
        }

        public async Task<OutputUserDTO> UpdateUser(InputUserDTO _user, string Id)
        {
            ObjectId objectId = ObjectId.Parse(Id);
            User user = _mapper.Map<InputUserDTO, User>(_user);
            user.Id = Id;

            await _context.Users.ReplaceOneAsync(x => x.Id == Id, user);

            OutputUserDTO result = _mapper.Map<User, OutputUserDTO>(user);

            ProfileUpdatedEvent message = _mapper.Map<OutputUserDTO, ProfileUpdatedEvent>(result);
            await _publish.Publish(message);

            return result;
        }
    }
}