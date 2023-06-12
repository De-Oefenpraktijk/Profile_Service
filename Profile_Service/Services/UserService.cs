using AutoMapper;
using EventBus.Messages.Events;
using MassTransit;
using Microsoft.AspNetCore.Identity;
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
            user.CreatedAt = DateTime.UtcNow;

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

        public async Task<List<UserActivityStatusDTO>> GetAllActivityStatuses()
        {
            List<User> users = await _context.Users.Find(_ => true).ToListAsync();

            List<UserActivityStatusDTO> activityStatusDTOs= new List<UserActivityStatusDTO>();
            foreach (User user in users)
            {
                var activityStatusDTO = new UserActivityStatusDTO();
                activityStatusDTO.Username = user.Username;
                TimeSpan timeDifference = (TimeSpan)(DateTime.UtcNow - user.LastOnline);
                activityStatusDTO.IsOnline = timeDifference.TotalSeconds < 60;
                activityStatusDTOs.Add(activityStatusDTO);
            }

            return activityStatusDTOs;
        }

        public async Task<OutputUserDTO> UpdateUser(InputUpdateUserDTO _user, string Id)
        {
            ObjectId objectId = ObjectId.Parse(Id);
            User existingUser = await _context.Users.Find(x => x.Id == Id).FirstOrDefaultAsync();
            if (existingUser == null)
            {
                throw new Exception("User does not exist");
            }
            User user = _mapper.Map(_user, existingUser);

            await _context.Users.ReplaceOneAsync(x => x.Id == Id, user);

            OutputUserDTO result = _mapper.Map<User, OutputUserDTO>(user);

            ProfileUpdatedEvent message = _mapper.Map<OutputUserDTO, ProfileUpdatedEvent>(result);
            await _publish.Publish(message);

            return result;
        }

        public async Task<OutputUserDTO> UpdateUserByEmail(InputUpdateUserDTO _user, string Email)
        {
            User existingUser = await _context.Users.Find(x => x.Email == Email).FirstOrDefaultAsync();
            if (existingUser == null)
            {
                throw new Exception("User does not exist");
            }
            User user = _mapper.Map(_user, existingUser);

            await _context.Users.ReplaceOneAsync(x => x.Email == Email, user);

            OutputUserDTO result = _mapper.Map<User, OutputUserDTO>(user);

            ProfileUpdatedEvent message = _mapper.Map<OutputUserDTO, ProfileUpdatedEvent>(result);
            await _publish.Publish(message);

            return result;
        }

        public async Task<bool> UpdateActivityStatus(string email)
        {
            User existingUser = await _context.Users.Find(x => x.Email == email).FirstOrDefaultAsync();
            if (existingUser == null)
            {
                throw new Exception("User does not exist");
            }
            existingUser.LastOnline = DateTime.UtcNow;

            await _context.Users.ReplaceOneAsync(x => x.Email == email, existingUser);

            return true;
        }

        public async Task<List<OutputUserDTO>> GetAllUsersStartsWith(string searchPattern)
        {
            List<User> users = new List<User>();
            // users = await _context.Users.Find(_ => true).ToListAsync();
            if (searchPattern == "*")
            {
                // 10 users limit
                int limit = 10;
                users = await _context.Users.Find(_ => true).Limit(limit).ToListAsync();
            }
            else
            {
                users = await _context.Users.Find(user => user.Email != null && user.Email.StartsWith(searchPattern)).ToListAsync();
            }

            if (users == null)
            {
                throw new Exception("No users found!");
            }
            List<OutputUserDTO> result = _mapper.Map<List<User>, List<OutputUserDTO>>(users);
            return result;
        }
    }
}