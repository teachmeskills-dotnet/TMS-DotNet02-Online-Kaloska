using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TMS_DotNet02_Online_Kaloska.TmTracker.Web.Models.ChartModels
{
    public class Charts
    {
        public string type { get; set; }
        public List<string[]> data { get; set; } 
    }
}
