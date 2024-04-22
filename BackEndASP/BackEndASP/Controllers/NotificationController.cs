using BackEndASP.DTOs;
using BackEndASP.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BackEndASP.Controllers
{
    [Route("notifications")]
    [ApiController]
    public class NotificationController : ControllerBase
    {

        private IUnitOfWorkRepository _unitOfWorkRepository;

        public NotificationController(IUnitOfWorkRepository unitOfWorkRepository)
        {
            _unitOfWorkRepository = unitOfWorkRepository;
        }


        [HttpGet]
        [Authorize(Policy = "StudentOrOwner")]
        public async Task<ActionResult<IEnumerable<NotificationDTO>>> GetNotifications()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Ok(await _unitOfWorkRepository.NotificationRepository.GetNotifications(userId));


        }

        [HttpPut("{notificationId}")]
        public async Task<dynamic> ReadNotification(int notificationId)
        {
            try
            {
                await _unitOfWorkRepository.NotificationRepository.ReadNotification(notificationId);
                await _unitOfWorkRepository.CommitAsync();
                return Ok("Notification has been read");
            } catch (ArgumentException ex) 
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
