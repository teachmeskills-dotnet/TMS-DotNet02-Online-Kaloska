using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS_DotNet02_Online_Kaloska.TmTracker.Data.Constants
{
    /// <summary>
    /// SQL configuration contants.
    /// </summary>
    public static class SqlConfiguration
    {
        /// <summary>
        /// Custom date format.
        /// </summary>
        public const string SqlDateFormat = "date";

        /// <summary>
        /// Custom small date format.
        /// </summary>
        public const string SqlSmallDateFormat = "smalldatetime";

        /// <summary>
        /// Custom date time format.
        /// </summary>
        public const string SqlDateTimeFormat = "datetime";

        /// <summary>
        /// Custom decimal format.
        /// </summary>
        public const string SqlDecimalFormat = "decimal(18,4)";

        /// <summary>
        /// Min lenght for string field.
        /// </summary>
        public const int SqlMaxLengthShort = 63;

        /// <summary>
        /// Standart lenght for string field.
        /// </summary>
        public const int SqlMaxLengthMedium = 127;

        /// <summary>
        /// Max lenght for string field.
        /// </summary>
        public const int SqlMaxLengthLong = 255;
    }
}
