using Microsoft.AspNetCore.Mvc;
using System.Net;
using Profile_Service.Entities;
using Profile_Service.DTO;
using Profile_Service.Models;
using Microsoft.EntityFrameworkCore;

namespace Profile_Service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly DBContext _DbContext;

        public UserController(DBContext DBContext)
        {
            _DbContext = DBContext;
        }


        [HttpGet("GetUsers")]
        public async Task<ActionResult<List<UserDTO>>> Get()
        {
            var List = await _DbContext.Users.Select(
                s => new UserDTO
                {
                    Id = s.Id,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    Username = s.Username,
                    Password = s.Password,
                    EnrollmentDate = s.EnrollmentDate
                }
            ).ToListAsync();

            if (List.Count < 0)
            {
                return NotFound();
            }
            else
            {
                return List;
            }
        }

        [HttpGet("GetUserById")]
        public async Task<ActionResult<UserDTO>> GetUserById(int Id)
        {
            UserDTO User = await _DbContext.Users.Select(
                    s => new UserDTO
                    {
                        Id = s.Id,
                        FirstName = s.FirstName,
                        LastName = s.LastName,
                        Username = s.Username,
                        Password = s.Password,
                        EnrollmentDate = s.EnrollmentDate
                    })
                .FirstOrDefaultAsync(s => s.Id == Id);

            if (User == null)
            {
                return NotFound();
            }
            else
            {
                return User;
            }
        }

        [HttpPost("InsertUser")]
        public async Task<HttpStatusCode> InsertUser(UserDTO User)
        {
            var entity = new User()
            {
                Id = User.Id,
                FirstName = User.FirstName,
                LastName = User.LastName,
                Username = User.Username,
                EmailAddress = User.EmailAddress,
                Role = User.Role,
                Password = User.Password,
                EnrollmentDate = User.EnrollmentDate
            };

            _DbContext.Users.Add(entity);
            await _DbContext.SaveChangesAsync();

            return HttpStatusCode.Created;
        }

        [HttpPut("UpdateUser")]
        public async Task<HttpStatusCode> UpdateUser(UserDTO User)
        {
            var entity = await _DbContext.Users.FirstOrDefaultAsync(s => s.Id == User.Id);

            entity.FirstName = User.FirstName;
            entity.LastName = User.LastName;
            entity.Username = User.Username;
            entity.EmailAddress = User.EmailAddress;
            entity.Password = User.Password;
            entity.Role = User.Role;
            entity.EnrollmentDate = User.EnrollmentDate;

            await _DbContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }

        [HttpDelete("DeleteUser/{Id}")]
        public async Task<HttpStatusCode> DeleteUser(int Id)
        {
            var entity = new User()
            {
                Id = Id
            };
            _DbContext.Users.Attach(entity);
            _DbContext.Users.Remove(entity);
            await _DbContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
    }
}

