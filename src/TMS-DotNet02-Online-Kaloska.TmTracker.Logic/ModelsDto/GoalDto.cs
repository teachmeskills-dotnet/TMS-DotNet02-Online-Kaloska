﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS_DotNet02_Online_Kaloska.TmTracker.Logic.ModelsDto
{
    /// <summary>
    /// Goal.
    /// </summary>
    class GoalDto
    {
        /// <summary>
        /// Identifier.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Project identifier.
        /// </summary>
        public int ProjectId { get; set; }
        /// <summary>
        /// Title.
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Text.
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// Is complete
        /// </summary>
        public bool IsComplete { get; set; }
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
