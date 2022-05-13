using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TMS_DotNet02_Online_Kaloska.TmTracker.Data.Models;
using TMS_DotNet02_Online_Kaloska.TmTracker.Logic.Interfaces;
using TMS_DotNet02_Online_Kaloska.TmTracker.Logic.ModelsDto;

namespace TMS_DotNet02_Online_Kaloska.TmTracker.Web.Controllers
{
    [Authorize]
    public class GoalController : Controller
    {
        UserManager<User> _userManager;
        private IProjectManager _projectManager;
        private IGoalManager _goalManager;
        /// <summary>
        /// Constructor with params.
        /// </summary>
        /// <param name="projectManager">Project manager.</param>
        /// <param name="userManager">Project manager.</param>
        /// <param name="recordManager">Project manager.</param>
        public GoalController(IProjectManager projectManager,
            UserManager<User> userManager,
            IGoalManager goalManager,
            IRecordManager recordManager)

        {
            _projectManager = projectManager ?? throw new ArgumentNullException(nameof(projectManager));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _goalManager = goalManager ?? throw new ArgumentNullException(nameof(goalManager));
        }
        /// <summary>
        /// ShowGoal (Get.)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="nameProject"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> ShowGoal(int id, string nameProject)
        {
            //Todo change to Mvc
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var goals = await _goalManager.GetAllByProjectAndUserIdAsync(id, userId);

            ViewBag.nameProject = nameProject;
            ViewBag.projectId = id;
            ViewBag.goals = goals;
            return View();
        }
        /// <summary>
        /// AddGoal (Post.)
        /// </summary>
        /// <param name="text"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddGoal(string text, int projectId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var goalDto = new GoalDto()
            {
                Text = text,
                ProjectId = projectId,
                IsComplete = false,
            };
            await _goalManager.CreateAsync(goalDto, userId);
            return Ok(true);
        }
        /// <summary>
        /// DeleteGoal (Get.)
        /// </summary>
        /// <param name="goalId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task DeleteGoal(int goalId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            await _goalManager.DeleteAsync(goalId, userId);
        }
        /// <summary>
        /// GoalAsCompleted (Get.)
        /// </summary>
        /// <param name="goalId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task GoalAsCompleted(int goalId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var goal = await _goalManager.GetById(goalId);
            goal.IsComplete = true;
            await _goalManager.UpdateAsync(goal, userId);
        }
    }
}
