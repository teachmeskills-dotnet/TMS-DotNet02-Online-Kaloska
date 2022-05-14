using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS_DotNet02_Online_Kaloska.TmTracker.Data.Models
{
    /// <summary>
    /// Project.
    /// </summary>
    public class Project
    {
        /// <summary>
        /// Identifier.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Name.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// User identifier.
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// Creation time.
        /// </summary>
        public DateTime CreationTime { get; set; }
        /// <summary>
        /// Is favourite.
        /// </summary>
        public bool IsFavourite { get; set; }
        /// <summary>
        /// Navigation property for goal.
        /// </summary>
        public ICollection<Goal> Goals { get; set; }
        /// <summary>
        /// Navigation property for user.
        /// </summary>
        public ICollection<User> Users { get; set; }
        /// <summary>
        /// Navigation property for record.
        /// </summary>
        public ICollection<Record> Records { get; set; }
    }
}
