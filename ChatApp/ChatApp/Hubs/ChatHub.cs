using ChatApp.Mappings;
using ChatApp.Models;
using ChatApp.Repositories.Interfaces;
using ChatApp.Requests;
using ChatApp.Response;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace ChatApp.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly IBus _bus;
        private readonly UserManager<ChatUser> _userManager;
        private readonly IMessageRepository _messageRepository;
        private readonly IRequestClient<StockRequest> _requestClient;

        public ChatHub(UserManager<ChatUser> userManager, IMessageRepository messageRepository, IBus bus, IRequestClient<StockRequest> requestClient)
        {
            _userManager = userManager;
            _messageRepository = messageRepository;
            _bus = bus;
            _requestClient = requestClient;
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

        public async Task SendMessage(string message)
        {
            try
            {
                var currentUser = await _userManager.GetUserAsync(Context.User);
                var msg = new Message
                {
                    Text = message,
                    ChatUserID = currentUser.Id,
                    ChatUser = currentUser,
                    SentFrom = ""
                };

                await _messageRepository.Add(msg);
                await Clients.All.SendAsync("ReceiveMessage", currentUser.UserName, msg.Text);

                if (message.StartsWith("/stock="))
                {
                    {
                    
                        var command = message.Substring(message.IndexOf("=") + 1);

                        var request = _requestClient.Create(new { StockValue = command });

                        var response = await request.GetResponse<StockResponse>();

                        var botMsg = new Message
                        {
                            Text = response.Message.Message,
                            ChatUserID = currentUser.Id,
                            SentFrom = "Bot",
                        };

                        await _messageRepository.Add(botMsg);

                        await Clients.All.SendAsync("ReceiveMessage", "Bot", response.Message.Message);                    
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }
    }
}
