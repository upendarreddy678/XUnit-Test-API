using ApplicationSrv.iRepo;
using DomainSrv.User;
using Microsoft.AspNetCore.Mvc;

namespace Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _iUserRepo;
        public UserController(IUserRepository UserRepo)
        {
            _iUserRepo = UserRepo;
        }

        [HttpPost]
        [Produces("Application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(string))]
        public IActionResult Create([FromBody] CreateUpdateUserDto InP)
        {
            string Id = _iUserRepo.Create(InP);
            return Created($"api/User/{Id}", Id);
        }
        [HttpPut("{Id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status304NotModified)]
        public IActionResult Update([FromRoute] string Id, [FromBody] CreateUpdateUserDto InP)
        {
            if (!_iUserRepo.IsUserIdExist(Id))
            {
                return NotFound();
            }
            var Resp = _iUserRepo.Update(Id, InP);
            return Resp == true ? NoContent() : StatusCode(StatusCodes.Status304NotModified);
        }
        [HttpGet("{Id}")]
        [Produces("Application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
        public IActionResult GetById([FromRoute] string Id)
        {
            var Response = _iUserRepo.GetById(Id);
            return Response == null ? NotFound() : Ok(Response);
        }
        [HttpGet]
        [Produces("Application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<UserDto>))]
        public IActionResult GetList()
        {
            var Response = _iUserRepo.GetList();
            return Ok(Response);
        }

        [HttpDelete("{Id}")]
        [Produces("Application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
        public IActionResult Delete([FromRoute] string Id)
        {
            if (!_iUserRepo.IsUserIdExist(Id))
            {
                return NotFound();
            }
            _iUserRepo.Delete(Id);
            return NoContent();
        }
    }
}
