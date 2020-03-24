using ChatApp.Data;
using ChatApp.Dtos;
using ChatApp.Models;
using ChatApp.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly ApplicationDbContext _context;

        public MessageRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Add(Message entity)
        {
            await _context.Messages.AddAsync(entity);

            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(int id)
        {
            var message = await Get(id);

            _context.Messages.Remove(message);

            return await _context.SaveChangesAsync();
        }

        public async Task<Message> Get(int id)
        {
            return await _context.Messages.FindAsync(id);
        }

        public async Task<IEnumerable<MessageDto>> GetAll(int count)
        {
            var messages = await _context.Messages
                .OrderBy(m => m.CreatedOn)
                .Take(count)
                .Select(m => new MessageDto
                {
                    CreatedOn = m.CreatedOn,
                    MessageID = m.MessageID,
                    SentFrom = string.Equals(m.SentFrom, "bot", StringComparison.OrdinalIgnoreCase) ? m.SentFrom : m.ChatUser.UserName,
                    Text = m.Text
                })
                .ToListAsync();

            return messages;
        }

        public async Task<int> Update(Message message)
        {
            _context.Update(message);

            return await _context.SaveChangesAsync();
        }
    }
}
