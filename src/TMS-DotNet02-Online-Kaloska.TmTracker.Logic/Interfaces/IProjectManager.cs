using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS_DotNet02_Online_Kaloska.TmTracker.Data.Models;
using TMS_DotNet02_Online_Kaloska.TmTracker.Logic.ModelsDto;

namespace TMS_DotNet02_Online_Kaloska.TmTracker.Logic.Interfaces
{
    /// <summary>
    /// Project manager.
    /// </summary>
    public interface IProjectManager
    {
        /// <summary>
        /// Create project.
        /// </summary>
        /// <param name="model">Project data transfer object.</param>
        /// <param name="user">User data transfer object.</param>
        Task CreateAsync(ProjectDto model, User user);

        /// <summary>
        /// Get projects by user identifier.
        /// </summary>
        /// <param name="userId">User identifier.</param>
        /// <returns>Project data transfer objects.</returns>
        Task<IEnumerable<ProjectDto>> GetAllByUserIdAsync(string userId);
        /// <summary>
        /// AddUserstoProject
        /// </summary>
        /// <param name="model"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        Task AddUsertoProject(ProjectDto model, User user);

        /// <summary>
        /// Get project by id.
        /// </summary>
        /// <param name="id">Project identifier.</param>
        /// <returns>Project data transfer object.</returns>
        Task<ProjectDto> GetById(int id);
        /// <summary>
        /// Get Users by project id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Users data transfer object.</returns>
        Task<IEnumerable<UserDto>> GetUsersByProjectIdAsync(int id);

        /// <summary>
        /// Update project by identifier.
        /// </summary>
        /// <param name="model">Project data transfer object.</param>
        Task UpdateAsync(ProjectDto model);

        /// <summary>
        /// Delete project by identifier.
        /// </summary>
        /// <param name="id">Project identifier.</param>
        /// <param name="id">User identifier.</param>
        Task DeleteAsync(int id, string userId);
    }
}
