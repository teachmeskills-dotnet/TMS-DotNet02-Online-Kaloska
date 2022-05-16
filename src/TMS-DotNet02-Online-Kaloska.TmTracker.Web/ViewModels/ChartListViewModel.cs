using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TMS_DotNet02_Online_Kaloska.TmTracker.Web.ViewModels
{
    public class ChartListViewModel
    {
        /// <summary>
        /// Goal name.
        /// </summary>
        public string GoalName { get; set; }
        /// <summary>
        /// List chart view.
        /// </summary>
        public List<ChartViewModel> ChartViewModes { get; set; }
    }
}
