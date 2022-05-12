using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS_DotNet02_Online_Kaloska.TmTracker.Data.Models
{
    /// <summary>
    /// Record.
    /// </summary>
    public class Record
    {
        /// <summary>
        /// Identifier.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// User identifier.
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// Project identifier.
        /// </summary>
        public int ProjectId { get; set; }
        /// <summary>
        /// Goal identifier.
        /// </summary>
        public int GoalId { get; set; }
        /// <summary>
        /// Navigation property for user.
        /// </summary>
        public User User { get; set; }
        /// <summary>
        /// Navigation property for project.
        /// </summary>
        public Project Project { get; set; }
        /// <summary>
        /// Start.
        /// </summary>
        public DateTime Start{ get; set; }
        /// <summary>
        /// End.
        /// </summary>
        public DateTime End { get; set; }
    }
}
