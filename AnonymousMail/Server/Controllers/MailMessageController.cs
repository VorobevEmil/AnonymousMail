using AnonymousMail.Server.Services;
using AnonymousMail.Shared.Command.Request;
using AnonymousMail.Shared.Command.Response.Mail;
using AnonymousMail.Shared.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AnonymousMail.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MailMessageController : ControllerBase
    {
        private readonly MailService _service;
        private readonly IMapper _mapper;

        public MailMessageController(MailService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("SearchUsers/{username}")]
        public async Task<ActionResult<IEnumerable<User>>> SearchUsersAsync(string username)
        {
            try
            {
                return Ok(await _service.SearchUsersAsync(username));
            }
            catch
            {
                return BadRequest("Не удалось осуществить поиск");
            }
        }

        [HttpGet("GetUsers")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsersAsync()
        {
            try
            {
                return Ok(await _service.GetUsersAsync());
            }
            catch
            {
                return BadRequest("Не удалось получить пользователей");
            }
        }

        [HttpGet("GetUserDetails/{userId}")]
        public async Task<ActionResult<User>> GetUserDetailsAsync(string userId)
        {
            try
            {
                var user = await _service.GetUserDetailsAsync(userId);

                if (user == null)
                    return NotFound("Пользователь не найден");

                return Ok();
            }
            catch
            {
                return BadRequest("Не удалось получить пользователя");
            }
        }

        [HttpPost("Save")]
        public async Task<ActionResult<MailMessageResponse>> SaveMessageAsync(MailMessageRequest message)
        {
            try
            {
                var messageResult = await _service.SaveMessageAsync(_mapper.Map<MailMessage>(message));
                return Ok(_mapper.Map<MailMessageResponse>(messageResult));
            }
            catch
            {
                return BadRequest("Не удалось сохранить письмо");
            }
        }

        [HttpGet("GetAllInputMessages")]
        public async Task<ActionResult<List<MailMessageResponse>>> GetAllInputMessagesAsync()
        {
            try
            {
                var messages = await _service.GetAllInputMessagesAsync();

                return Ok(_mapper.Map<List<MailMessageResponse>>(messages));
            }
            catch
            {
                return BadRequest("Не удалось получить входящие сообщения");
            }
        }

        [HttpGet("GetAllOutputMessages")]
        public async Task<ActionResult<List<MailMessageResponse>>> GetAllOutputMessagesAsync()
        {
            try
            {
                var messages = await _service.GetAllOutputMessagesAsync();

                return Ok(_mapper.Map<List<MailMessageResponse>>(messages));
            }
            catch
            {
                return BadRequest("Не удалось получить отправленные сообщения");
            }
        }
    }
}
