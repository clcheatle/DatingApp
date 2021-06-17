using System.Collections.Generic;
using System.Threading.Tasks;
using API.Extensions;
using API.BusinessLogic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using API.Models;
using API.Helpers;

namespace API.Controllers
{
    [Authorize]
    public class MessagesController : BaseApiController
    {
        private readonly MessagesLogic _messagesLogic;
        public MessagesController(MessagesLogic messagesLogic)
        {
            _messagesLogic = messagesLogic;
        }

        // [HttpPost]
        // public async Task<ActionResult<MessageDto>> CreateMessage(CreateMessageDto createMessageDto)
        // {
        //     var username = User.GetUsername();

        //     if(username == createMessageDto.RecipientUsername.ToLower()) return BadRequest("You cannot send messages to yourself");

        //     var result = await _messagesLogic.CreateMessage(createMessageDto, username);

        //     if(result == null) return BadRequest("Either recipient not found or failure occured while sending");

        //     return Ok(result);

        // }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MessageDto>>> GetMessagesForUser([FromQuery] MessageParams messageParams)
        {
            messageParams.Username = User.GetUsername();

            var messages = await _messagesLogic.GetMessagesForUser(messageParams);

            Response.AddPaginationHeader(messages.CurrentPage, messages.PageSize, messages.TotalCount, messages.TotalPages);

            return messages;

        }

        [HttpGet("thread/{username}")]
        public async Task<ActionResult<IEnumerable<MessageDto>>> GetMessageThread(string username)
        {
            var currentUsername = User.GetUsername();

            return Ok(await _messagesLogic.GetMessageThread(currentUsername, username));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMessage(int id)
        {
            var username = User.GetUsername();
            var message = await _messagesLogic.GetMessage(id);

            if (message.Sender.UserName != username && message.Recipient.UserName != username) return Unauthorized();

            bool deleteSuccess = await _messagesLogic.DeleteMessage(message, username);

            if(deleteSuccess) return Ok();

            return BadRequest("Problem deleting the message");
        }
    }
}