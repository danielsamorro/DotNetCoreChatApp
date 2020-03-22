using ChatApp.Dtos;
using ChatApp.Models;
using ChatApp.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Repositories.Interfaces
{
    public interface IMessageRepository
    {
        Task<int> Add(Message entity);
        Task<int> Delete(int id);
        Task<Message> Get(int id);
        Task<IEnumerable<MessageDto>> GetAll(ChatUser currentUser, ChatUser targetUser);
        Task<int> Update(Message message);
    }
}
