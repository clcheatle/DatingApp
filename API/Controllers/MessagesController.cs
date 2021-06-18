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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MessageDto>>> GetMessagesForUser([FromQuery] MessageParams messageParams)
        {
            messageParams.Username = User.GetUsername();

            var messages = await _messagesLogic.GetMessagesForUser(messageParams);

            Response.AddPaginationHeader(messages.CurrentPage, messages.PageSize, messages.TotalCount, messages.TotalPages);

            return messages;

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