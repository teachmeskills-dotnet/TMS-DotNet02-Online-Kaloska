using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TMS_DotNet02_Online_Kaloska.TmTracker.Web.ViewModels
{
    public class RecordViewModel
    {
        /// <summary>
        /// User name.
        /// </summary>
        public string NameUser { get; set; }
        /// <summary>
        /// Date create.
        /// </summary>
        public string DateCreate { get; set; }
        /// <summary>
        /// Time.
        /// </summary>
        public string Timer { get; set; }
        /// <summary>
        /// Goal name.
        /// </summary>
        public string GoalName { get; set; }
    }
}
