using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TMS_DotNet02_Online_Kaloska.TmTracker.Web.ViewModels
{
    public class ProjectViewModel
    {
        /// <summary>
        /// Identifier.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Name.
        /// </summary>
        [Required]
        [Display(Name = "Название проекта")]
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
        [Display(Name = "Добавить в избранные?")]
        public bool IsFavourite { get; set; }
    }
}
