using System.Collections.Generic;
using System.Threading.Tasks;
using API.Helpers;
using API.Models;
using API.Repository;
using AutoMapper;
namespace API.BusinessLogic
{
    public class MessagesLogic
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IUserRepoLayer _userRepoLayer;
        private readonly IMapper _mapper;
        public MessagesLogic(IMessageRepository messageRepository, IUserRepoLayer userRepoLayer, IMapper mapper)
        {
            _messageRepository = messageRepository;
            _userRepoLayer = userRepoLayer;
            _mapper = mapper;
        }

        public async Task<MessageDto> CreateMessage(CreateMessageDto createMessageDto, string username)
        {
            var sender = await _userRepoLayer.GetUserByUsernameAsync(username);
            var recipent = await _userRepoLayer.GetUserByUsernameAsync(createMessageDto.RecipientUsername);

            if(recipent == null) return null;

            var message = new Message
            {
                Sender = sender,
                Recipient = recipent,
                SenderUsername = sender.UserName,
                RecipientUsername = recipent.UserName,
                Content = createMessageDto.Content
            };

            _messageRepository.AddMessage(message);

            if(await _messageRepository.SaveAllAsync()) return _mapper.Map<MessageDto>(message);

            return null;
        }

        public async Task<PagedList<MessageDto>> GetMessagesForUser(MessageParams messageParams)
        {
            var messages = await _messageRepository.GetMessagesForUser(messageParams);

            return messages;
        }

        public async Task<IEnumerable<MessageDto>> GetMessageThread(string currentUsername, string username)
        {
            var messages = await _messageRepository.GetMessageThread(currentUsername, username);

            return messages;
        }

        public async Task<Message> GetMessage(int id)
        {
            return await _messageRepository.GetMessage(id);
        }

        public async Task<bool> DeleteMessage(Message message, string username)
        {
            if (message.Sender.UserName == username) message.SenderDeleted = true;

            if (message.Recipient.UserName == username) message.RecipientDeleted = true;

            if (message.SenderDeleted && message.RecipientDeleted) _messageRepository.DeleteMessage(message);

            if (await _messageRepository.SaveAllAsync()) return true;

            return false;
        }


    }
}