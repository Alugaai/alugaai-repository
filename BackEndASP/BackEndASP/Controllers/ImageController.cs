using BackEndASP.DTOs.ImageDTOs;
using BackEndASP.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BackEndASP.Controllers
{
    [Route("images")]
    [ApiController]
    public class ImageController : ControllerBase
    {

        private readonly IUnitOfWorkRepository _unitOfWorkRepository;

        public ImageController(IUnitOfWorkRepository unitOfWorkRepository)
        {
            _unitOfWorkRepository = unitOfWorkRepository;
        }


        [HttpPost("user")]
        [Authorize(Policy = "StudentOrOwner")]
        public async Task<ActionResult<dynamic>> InsertImageForAUser([FromForm] IFormFileCollection file)
        {

            try
            {
                if (file.Count > 1)
                {
                    return BadRequest("O usuário deve conter apenas uma foto");
                }
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                await _unitOfWorkRepository.ImageRepository.InsertImageForAUser(file, userId);
                await _unitOfWorkRepository.CommitAsync();
                return Ok("Image saved successfuly");

            } catch (ArgumentException ex) 
            { 
                return BadRequest(ex.Message);
            }

        }


        [HttpPost("property/{propertyId}")]
        [Authorize(Policy = "OwnerOnly")]
        public async Task<ActionResult<dynamic>> InsertImageForProperty([FromForm] IFormFileCollection files ,int propertyId)
        {
            try
            {

                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                await _unitOfWorkRepository.ImageRepository.InsertImageForProperty(files, userId, propertyId);
                await _unitOfWorkRepository.CommitAsync();
                return Ok("Image saved successfuly");

            } catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("college/{collegeId}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<dynamic>> InsertImageForCollege([FromForm] IFormFileCollection files, int collegeId)
        {
            try
            {

                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                await _unitOfWorkRepository.ImageRepository.InsertImageForCollege(files, collegeId);
                await _unitOfWorkRepository.CommitAsync();
                return Ok("Image saved successfuly");

            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
