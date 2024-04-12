using ApiCatalogo.Pagination;
using BackEndASP.DTOs.BuildingDTOs;
using BackEndASP.DTOs.PropertyDTOs;
using BackEndASP.Interfaces;
using Correios.NET.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BackEndASP.Controllers
{
    [Route("properties")]
    [ApiController]
    public class PropertyController : ControllerBase
    {

        private readonly IUnitOfWorkRepository _unitOfWorkRepository;
        private readonly UserManager<User> _userManager;

        public PropertyController(IUnitOfWorkRepository unitOfWorkRepository, UserManager<User> userManager)
        {
            _unitOfWorkRepository = unitOfWorkRepository;
            _userManager = userManager;
        }


        [HttpGet]
        public async Task<ActionResult<dynamic>> FindAllProperties([FromQuery] PagePropertyQueryParams pageQueryParams)
        {
            try
            {
                return Ok(await _unitOfWorkRepository.PropertyRepository.FindAllPropertiesAsync(pageQueryParams));
            }
            catch (Exception e)
            {
                return NotFound("Resource not found");
            }
        }

        [HttpGet("findPropertyDetailsById/{propertyId}")]
        public async Task<ActionResult<dynamic>> FindPropertyDetailsById(int propertyId)
        {

            try
            {
                return Ok(await _unitOfWorkRepository.PropertyRepository.FindPropertyDetailsById(propertyId));
            }
            catch (Exception e)
            {
                return NotFound("Resource not found");
            }

        }



        [HttpPost]
        [Authorize(Policy = "OwnerOnly")]
        public async Task<ActionResult<dynamic>> InsertProperty([FromBody] PropertyInsertDTO dto)
        {

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _userManager.FindByIdAsync(userId);
            //return CreatedAtAction(nameof(result.Id), new { id = result.Id }, result.Result);

            await _unitOfWorkRepository.PropertyRepository.InsertProperty(dto, user);
            await _unitOfWorkRepository.CommitAsync();

            return Ok("Property created successfully");
            

        }





    }
}
