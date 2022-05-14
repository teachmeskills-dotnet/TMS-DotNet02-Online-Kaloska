using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TMS_DotNet02_Online_Kaloska.TmTracker.Web.Models
{
    public class ApplicationContextSingalR : DbContext
    {
        public ApplicationContextSingalR(DbContextOptions<ApplicationContextSingalR> options)
            : base(options)
        {

        }
        public DbSet<MessageModel> Messages { get; set; }
    }
}
