using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatApp.Dtos;
using ChatApp.Models;
using ChatApp.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Controllers
{  
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly UserManager<ChatUser> _userManager;
        private IMessageRepository _messageRepository;

        public MessageController(UserManager<ChatUser> userManager, IMessageRepository messageRepository)
        {
            _userManager = userManager;
            _messageRepository = messageRepository;
        }

        [Authorize]
        [HttpGet]
        public async Task<IEnumerable<MessageDto>> GetAsync(string userName)
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            var targetUser = await _userManager.FindByNameAsync(userName);


            return await _messageRepository.GetAll(currentUser, targetUser);
                
        }
    }
}