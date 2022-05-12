using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS_DotNet02_Online_Kaloska.TmTracker.Data.Models
{
    /// <summary>
    /// User.
    /// </summary>
    class User : IdentityUser
    {
        /// <summary>
        /// Name.
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// Navigation property for record.
        /// </summary>
        public ICollection<Record> Records { get; set; }
        /// <summary>
        /// Navigation property for user.
        /// </summary>
        public ICollection<Project> Projects { get; set; }
    }
}
