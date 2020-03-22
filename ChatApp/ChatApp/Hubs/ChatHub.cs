using ChatApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Hubs
{
    public class ChatHub : Hub
    {
        private readonly UserManager<ChatUser> _userManager;

        public ChatHub(UserManager<ChatUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task SendMessage(string user, string message)
        {
            var currentUser = await _userManager.FindByNameAsync(user);

            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
