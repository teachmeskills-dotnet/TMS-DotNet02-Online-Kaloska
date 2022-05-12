using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS_DotNet02_Online_Kaloska.TmTracker.Logic.ModelsDto
{
    /// <summary>
    /// User.
    /// </summary>
    class UserDto
    {
        /// <summary>
        /// Identifier.
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Fullname.
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// Email.
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Password.
        /// </summary>
        public string Password { get; set; }
    }
}
