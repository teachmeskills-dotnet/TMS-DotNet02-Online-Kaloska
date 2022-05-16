using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TMS_DotNet02_Online_Kaloska.TmTracker.Web.ViewModels
{
    /// <summary>
    /// Project list view model.
    /// </summary>
    public class ProjectListViewModel
    {
        /// <summary>
        /// Projects.
        /// </summary>
        public IEnumerable<ProjectViewModel> Projects { get; set; }
    }
}
