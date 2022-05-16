using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMS_DotNet02_Online_Kaloska.TmTracker.Web.Models;

namespace TMS_DotNet02_Online_Kaloska.TmTracker.Web.Services.MessageService
{
    interface IMessageService
    {
        public Task<List<MessageModel>> GetMessage(int projectId);
        public Task AddMessage(MessageModel message);
    }
}
