using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMS_DotNet02_Online_Kaloska.TmTracker.Web;
using TMS_DotNet02_Online_Kaloska.TmTracker.Web.Models;
using TMS_DotNet02_Online_Kaloska.TmTracker.Web.Services.MessageService;

namespace SignalRApp
{
    public class ChatHub : Hub
    {
        IMessageService _MessageService { get; set; }

        public ChatHub()
        {

            _MessageService = (IMessageService)Startup._serviceProvider.GetRequiredService(typeof(IMessageService));

        }
        public async Task Send(string message, int projectId, string UserName)
        {
            var messageModel = new MessageModel()
            {
                Message = message,
                ProjectId = projectId,
                UserID = UserName,
                createDate = DateTime.Now,
            };
            await _MessageService.AddMessage(messageModel);
            await Clients.All.SendAsync("Send", messageModel);
        }
        public override async Task OnConnectedAsync()
        {
            await Clients.All.SendAsync("Notify");
            await base.OnConnectedAsync();
        }
        public async Task OnConnect(int projectid)
        {
            List<MessageModel> message = await _MessageService.GetMessage(projectid);
            await Clients.All.SendAsync("GetMessage", message);
        }
    }

}

