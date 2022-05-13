using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TMS_DotNet02_Online_Kaloska.TmTracker.Data.Models;
using TMS_DotNet02_Online_Kaloska.TmTracker.Logic.Interfaces;
using TMS_DotNet02_Online_Kaloska.TmTracker.Logic.ModelsDto;
using TMS_DotNet02_Online_Kaloska.TmTracker.Web.ViewModels;

namespace TMS_DotNet02_Online_Kaloska.TmTracker.Web.Controllers
{
    public class RecordController : Controller
    {
        UserManager<User> _userManager;
        private IProjectManager _projectManager;
        private IRecordManager _recordManager;
        private IGoalManager _goalManager;
        /// <summary>
        /// Constructor with params.
        /// </summary>
        /// <param name="userManager">Project manager.</param>
        /// <param name="recordManager">Project manager.</param>
        /// <param name="projectManager">Project manager.</param>
        /// <param name="goalManager">Project manager.</param>
        public RecordController(IProjectManager projectManager,
            UserManager<User> userManager,
            IGoalManager goalManager,
            IRecordManager recordManager)

        {
            _projectManager = projectManager ?? throw new ArgumentNullException(nameof(projectManager));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _recordManager = recordManager ?? throw new ArgumentNullException(nameof(recordManager));
            _goalManager = goalManager ?? throw new ArgumentNullException(nameof(goalManager));
        }
        [HttpGet]
        public ActionResult StartTimer()
        {
            return View();
        }
        /// <summary>
        /// ShowRecord (Get).
        /// </summary>
        /// <param name="id"></param>
        /// <param name="nameProject"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> ShowRecord(int id, string nameProject)
        {
            //Todo change to Mvc
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var users = await _projectManager.GetUsersByProjectIdAsync(id);
            var records = await _recordManager.GetAllByProjectAndUserIdAsync(id, userId);
            var goals = await _goalManager.GetAllByProjectAndUserIdAsync(id, userId);
            var usersrec = _userManager.Users;

            var recordsView = records.Select(r => new RecordViewModel
            {
                NameUser = usersrec.FirstOrDefault(u => u.Id == r.UserId).FullName,
                DateCreate = r.Start.ToString("d"),
                Timer = r.End.ToString("T"),
                GoalName = goals.FirstOrDefault(g => g.Id == r.GoalId)?.Text ?? "Работа без задачи",
            }).ToList();

            SelectList selectListItems = new SelectList(goals.Where(g => g.IsComplete == false), "Id", "Text");

            ViewBag.goals = selectListItems;
            ViewBag.users = users;
            ViewBag.nameProject = nameProject;
            ViewBag.projectId = id;
            ViewBag.records = recordsView;
            return View();
        }
        /// <summary>
        /// AddTimer (Get).
        /// </summary>
        /// <param name="time"></param>
        /// <param name="projectId"></param>
        /// <param name="goal"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> AddTimerAsync(int time, int projectId, int goal)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var timer = DateTime.Today.AddSeconds(time);
            var recordDto = new RecordDto()
            {
                ProjectId = projectId,
                UserId = userId,
                Start = DateTime.Now,
                End = timer,
                GoalId = goal,
            };
            await _recordManager.CreateAsync(recordDto, userId);
            return Ok(true);
        }
    }
}
