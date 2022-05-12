using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS_DotNet02_Online_Kaloska.TmTracker.Logic.ModelsDto
{
    /// <summary>
    /// Record.
    /// </summary>
    class RecordDto
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
        /// Start.
        /// </summary>
        public DateTime Start { get; set; }
        /// <summary>
        /// End.
        /// </summary>
        public DateTime End { get; set; }
    }
}
