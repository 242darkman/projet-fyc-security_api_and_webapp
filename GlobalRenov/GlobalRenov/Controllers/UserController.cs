using GlobalRenov.Datas;
using GlobalRenov.DTO;
using GlobalRenov.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GlobalRenov.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DefaultContext _context;

        public UserController(DefaultContext context)
        {
            _context = context;
        }

        // GET: api/<UserController>
        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = _context.Users
                .Include(u => u.UserRoles)
                .ToList();

            var usersDTO = users.Select(u => new UserDTO
            {
                Id = u.Id,
                LastName = u.LastName,
                FirstName = u.FirstName,
                //Roles = u.UserRoles.Select(ur => ur.Role).ToList()
            }).ToList();

            return Ok(usersDTO);
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UserController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
