using BackEndASP.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackEndASP.Controllers
{
    [Route("user/details")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUnitOfWorkRepository _unitOfWork;

        public UserController(IUnitOfWorkRepository unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("{email}")]
        public async Task<ActionResult<dynamic>> FindUserByEmail(string email)
        {
            try
            {
                return Ok(await _unitOfWork.UserRepository.FindUserByEmail(email));
            }
            catch (ArgumentException e)
            {
                return BadRequest("Email não encontrado");
            }
        }
    }
}
