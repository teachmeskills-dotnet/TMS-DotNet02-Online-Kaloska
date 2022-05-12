using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS_DotNet02_Online_Kaloska.TmTracker.Logic.ModelsDto
{
    /// <summary>
    /// Project.
    /// </summary>
    class ProjectDto
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
    }
}
