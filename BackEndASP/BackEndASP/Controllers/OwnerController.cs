using BackEndASP.DTOs.StudentDTOs;
using BackEndASP.DTOs.UserDTOs;
using BackEndASP.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BackEndASP.Controllers
{
    [Route("owners")]
    [ApiController]
    public class OwnerController : ControllerBase
    {

        private readonly IUnitOfWorkRepository _unitOfWorkRepository;

        public OwnerController(IUnitOfWorkRepository unitOfWorkRepository)
        {
            _unitOfWorkRepository = unitOfWorkRepository;
        }

        [HttpPost("completeProfile")]
        [Authorize(Policy = "OwnerOnly")]
        public async Task<ActionResult<dynamic>> CompleteProfile([FromBody] UserCompleteProfileDTO dto)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var result = await _unitOfWorkRepository.OwnerRepository.CompleteProfileOwner(userId, dto);
                await _unitOfWorkRepository.CommitAsync();
                return Ok("Owner profile completed successfully");

            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex);
            }
        }


    }
}
