using ChatApp.Mappings;
using ChatApp.Models;
using ChatApp.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly UserManager<ChatUser> _userManager;
        private readonly IMessageRepository _messageRepository;

        public ChatHub(UserManager<ChatUser> userManager, IMessageRepository messageRepository)
        {
            _userManager = userManager;
            _messageRepository = messageRepository;
        } 
         
        public override async Task OnConnectedAsync()
        {
            string name = Context.User.Identity.Name;

            ConnectionMapping<string>.Add(name, Context.ConnectionId);

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            string name = Context.User.Identity.Name;

            ConnectionMapping<string>.Remove(name, Context.ConnectionId);

            await base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(string destinyUser, string message)
        {
            var userToSend = await _userManager.FindByNameAsync(destinyUser);
            var currentUser = await _userManager.GetUserAsync(Context.User);

            var msg = new Message
            {
                Text = message,
                ChatUserID = currentUser.Id,
                ChatUser = currentUser,
                SentTo = userToSend.Id
            };

            await _messageRepository.Add(msg);

            var userList = ConnectionMapping<string>.GetConnections(destinyUser).ToList();
            userList.AddRange(ConnectionMapping<string>.GetConnections(currentUser.UserName));

            foreach (var connectionId in userList)
            {
                await Clients.Client(connectionId).SendAsync("ReceiveMessage", currentUser.UserName, msg.Text);
            }
        }
    }
}
