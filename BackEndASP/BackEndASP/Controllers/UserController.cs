using BackEndASP.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

        [HttpGet("detailsById")]
        public async Task<ActionResult<dynamic>> FindUserById()
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                return Ok(await _unitOfWork.UserRepository.FindUserById(userId));
            }
            catch (ArgumentException e)
            {
                return Ok(null);
            }
        }
    }
}
