using System.Collections.Generic;
using System.Threading.Tasks;
using API.Helpers;
using API.Interfaces;
using API.Models;
using API.Repository;
using AutoMapper;
namespace API.BusinessLogic
{
    public class MessagesLogic
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public MessagesLogic(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<MessageDto> CreateMessage(CreateMessageDto createMessageDto, string username)
        {
            var sender = await _unitOfWork.UserRepository.GetUserByUsernameAsync(username);
            var recipent = await _unitOfWork.UserRepository.GetUserByUsernameAsync(createMessageDto.RecipientUsername);

            if (recipent == null) return null;

            var message = new Message
            {
                Sender = sender,
                Recipient = recipent,
                SenderUsername = sender.UserName,
                RecipientUsername = recipent.UserName,
                Content = createMessageDto.Content
            };

            _unitOfWork.MessageRepository.AddMessage(message);

            if (await _unitOfWork.Complete()) return _mapper.Map<MessageDto>(message);

            return null;
        }

        public async Task<PagedList<MessageDto>> GetMessagesForUser(MessageParams messageParams)
        {
            var messages = await _unitOfWork.MessageRepository.GetMessagesForUser(messageParams);

            return messages;
        }

        public async Task<IEnumerable<MessageDto>> GetMessageThread(string currentUsername, string username)
        {
            var messages = await _unitOfWork.MessageRepository.GetMessageThread(currentUsername, username);

            return messages;
        }

        public async Task<Message> GetMessage(int id)
        {
            return await _unitOfWork.MessageRepository.GetMessage(id);
        }

        public async Task<bool> DeleteMessage(Message message, string username)
        {
            if (message.Sender.UserName == username) message.SenderDeleted = true;

            if (message.Recipient.UserName == username) message.RecipientDeleted = true;

            if (message.SenderDeleted && message.RecipientDeleted) _unitOfWork.MessageRepository.DeleteMessage(message);

            if (await _unitOfWork.Complete()) return true;

            return false;
        }


    }
}