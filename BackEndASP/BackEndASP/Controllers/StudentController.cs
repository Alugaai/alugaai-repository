﻿using ApiCatalogo.Pagination;
using BackEndASP.DTOs.StudentDTOs;
using BackEndASP.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using BackEndASP.DTOs;
using BackEndASP.Utils;

namespace BackEndASP.Controllers
{
    [Route("students")]
    [ApiController]
    public class StudentController : ControllerBase
    {

        private readonly IUnitOfWorkRepository _unitOfWorkRepository;
        private readonly UserManager<User> _userManager;

        public StudentController(IUnitOfWorkRepository unitOfWorkRepository, UserManager<User> userManager)
        {
            _unitOfWorkRepository = unitOfWorkRepository;
            _userManager = userManager;
        }



        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentFindAllFilterDTO>>> FindAllStudentsAsync([FromQuery] PageStudentQueryParams pageQueryParams)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var response = await _unitOfWorkRepository.StudentRepository.FindAllStudentsAsync(pageQueryParams, userId);
                Response.AddPaginationHeader(new PaginationHeader(response.CurrentPage,
                response.PageSize, response.TotalCount, response.TotalPages));
                return Ok(response);
            }
            catch (Exception e)
            {
                return NotFound("Resource not found");
            }
        }


        [HttpGet("/myconnections")]
        [Authorize(Policy = "StudentOnly")]
        public async Task<ActionResult<StudentsConnectionsDTO>> FindAllMyStudentsConnections()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Ok(_unitOfWorkRepository.StudentRepository.FindAllMyStudentsConnections(userId));
        }

        [HttpGet("/myinvitationsforconnection")]
        [Authorize(Policy = "StudentOnly")]
        public async Task<ActionResult<IEnumerable<StudentResponseNotification>>> FindMyAllStudentsWhoInvitationsConnections()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Ok(_unitOfWorkRepository.StudentRepository.FindMyAllStudentsWhoInvitationsConnections(userId));
        }


        // adicionando o ID de um estudante como pedido de conexão
        [HttpPut("{studentForConnectionId}")]
        [Authorize(Policy = "StudentOnly")]
        public async Task<ActionResult<dynamic>> GiveConnectionOrder(string studentForConnectionId)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                await _unitOfWorkRepository.StudentRepository.GiveConnectionOrder(userId, studentForConnectionId);
                await _unitOfWorkRepository.CommitAsync();

                // QUANDO TIVER O GETBYID REFATORAR AQUI PARA CREATEDATACTION
                return Ok(new ResponseForStringMessagesInControllerDTO()
                {
                    Message = "Order sent successfully"
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { ErrorMessage = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(400, "An error occurred while processing the request.");
            }
        }



        // estudante aceitando ou recusando o pedido de conexão
        [HttpPost("{notificationId}")]
        [Authorize(Policy = "StudentOnly")]
        public async Task<ActionResult<dynamic>> GiveConnectionOrder([FromBody] StudentConnectionInsertDTO dto, int notificationId)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var result = await _unitOfWorkRepository.StudentRepository.HandleConnection(userId, dto, notificationId);
                await _unitOfWorkRepository.CommitAsync();
                if (!result)
                {
                    return BadRequest(new ResponseForStringMessagesInControllerDTO() {Message = "Conexão recusada!"});
                }
                return Ok(new ResponseForStringMessagesInControllerDTO() {Message = "Conexão aceita com sucesso!"});
                
            } catch (ArgumentException ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpPost("completeProfile")]
        [Authorize(Policy = "StudentOnly")]
        public async Task<ActionResult<dynamic>> CompleteProfile([FromBody] StudentCompleteProfileDTO dto)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var result = await _unitOfWorkRepository.StudentRepository.CompleteProfileStudent(userId, dto);
                await _unitOfWorkRepository.CommitAsync();
                return Ok(new ResponseForStringMessagesInControllerDTO() { Message = "Perfil do estudante atualizado com sucesso" });

            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("completeProfile/hobbies")]
        [Authorize(Policy = "StudentOnly")]
        public async Task<ActionResult<dynamic>> CompleteProfileHobbies([FromBody] StudentCompleteProfileHobbies dto)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var result = await _unitOfWorkRepository.StudentRepository.CompleteProfileStudentHobbies(userId, dto);
                await _unitOfWorkRepository.CommitAsync();
                return Ok(new ResponseForStringMessagesInControllerDTO() { Message = "Hobbies adicionados com sucesso" });

            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("completeProfile/personalities")]
        [Authorize(Policy = "StudentOnly")]
        public async Task<ActionResult<dynamic>> CompleteProfilePersonalities([FromBody] StudentCompleteProfilePersonalities dto)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var result = await _unitOfWorkRepository.StudentRepository.CompleteProfileStudentPersonalities(userId, dto);
                await _unitOfWorkRepository.CommitAsync();
                return Ok(new ResponseForStringMessagesInControllerDTO() { Message = "Personalidades adicionadas com sucesso"});

            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex);
            }
        }


    }
}
