using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS_DotNet02_Online_Kaloska.TmTracker.Logic.ModelsDto;

namespace TMS_DotNet02_Online_Kaloska.TmTracker.Logic.Interfaces
{
    /// <summary>
    /// Goal manager.
    /// </summary>
    public interface IGoalManager
    {
        /// <summary>
        /// Create goal.
        /// </summary>
        /// <param name="model">Goal data transfer object.</param>
        /// <param name="userId">User identifier.</param>
        Task CreateAsync(GoalDto model, string userId);

        /// <summary>
        /// Get goal by id.
        /// </summary>
        /// <param name="id">Goal identifier.</param>
        /// <returns>Project data transfer object.</returns>
        Task<GoalDto> GetById(int id);
        /// <summary>
        /// Get goals by project identifier.
        /// </summary>
        /// <param name="projectId">=Project identifier.</param>
        /// <param name="userId">User identifier.</param>
        /// <returns>Goal data transfer objects.</returns>
        Task<IEnumerable<GoalDto>> GetAllByProjectAndUserIdAsync(int projectId, string userId);

        /// <summary>
        /// Update goal by identifier.
        /// </summary>
        /// <param name="model">Goal data transfer object.</param>
        Task UpdateAsync(GoalDto model, string userId);

        /// <summary>
        /// Delete goal by identifier.
        /// </summary>
        /// <param name="id">Goal identifier.</param>
        /// <param name="userId">User identifier.</param>
        Task DeleteAsync(int id, string userId);
    }
}
