using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMS_DotNet02_Online_Kaloska.TmTracker.Web.Models;

namespace TMS_DotNet02_Online_Kaloska.TmTracker.Web.Services.MessageService
{
    public class MessageService: IMessageService
    {
        private readonly ApplicationContextSingalR _context;
        public MessageService(ApplicationContextSingalR applicationDbContext)
        {
            _context = applicationDbContext;
        }
        public async Task<List<MessageModel>> GetMessage(int projectId)
        {
            var messages = await _context.Messages.Where(m=>m.ProjectId==projectId).ToListAsync();
            return messages;
        }
        public async Task AddMessage(MessageModel message)
        {
            await _context.Messages.AddAsync(message);
            await _context.SaveChangesAsync();
        }
    }
}
