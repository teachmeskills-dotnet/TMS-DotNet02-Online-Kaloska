using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TMS_DotNet02_Online_Kaloska.TmTracker.Data.Models;
using TMS_DotNet02_Online_Kaloska.TmTracker.Logic.Interfaces;
using TMS_DotNet02_Online_Kaloska.TmTracker.Web.Models.ChartModels;
using TMS_DotNet02_Online_Kaloska.TmTracker.Web.ViewModels;

namespace TMS_DotNet02_Online_Kaloska.TmTracker.Web.Controllers
{
    /// <summary>
    /// Chart controller.
    /// </summary>
    [Authorize]
    public class ChartController : Controller
    {
        UserManager<User> _userManager;
        private IProjectManager _projectManager;
        private IRecordManager _recordManager;
        private IGoalManager _goalManager;
        /// <summary>
        /// Constructor with params.
        /// </summary>
        /// <param name="projectManager">Project manager.</param>
        /// <param name="userManager">Project manager.</param>
        /// <param name="recordManager">Project manager.</param>
        public ChartController(IProjectManager projectManager,
            UserManager<User> userManager,
            IGoalManager goalManager,
            IRecordManager recordManager)

        {
            _projectManager = projectManager ?? throw new ArgumentNullException(nameof(projectManager));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _recordManager = recordManager ?? throw new ArgumentNullException(nameof(recordManager));
            _goalManager = goalManager ?? throw new ArgumentNullException(nameof(goalManager));
        }
        /// <summary>
        /// Show chart (Get.)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="nameProject"></param>
        /// <returns></returns>

        [HttpGet]
        public async Task<IActionResult> ShowChart(int id, string nameProject)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var records = await _recordManager.GetAllByProjectAndUserIdAsync(id, userId);
            var atimelis = new List<DateTime>();
            var usersRecord = _userManager.Users;
            var companiesList = records.GroupBy(p => p.GoalId);
            var chartLists = new List<ChartListViewModel>();
            foreach (var item in companiesList)
            {
                var chartListViewModel = new ChartListViewModel()
                {
                    ChartViewModes = new List<ChartViewModel>(),
                };
                if (item.Key != 0)
                {
                    var nameproject = await _goalManager.GetById(item.Key);
                    chartListViewModel.GoalName = nameproject.Text;
                }
                else
                {
                    chartListViewModel.GoalName = "Работа без задачи";

                }
                var asda = item.GroupBy(r => r.UserId).Select(k => new
                {
                    nameUser = usersRecord.FirstOrDefault(u => u.Id == k.Key).FullName,
                    timeuser = k.Sum(p => ConvertDateTimeToSeconds(p.End)),

                }).ToList();
                foreach (var itemUser in asda)
                {
                    var chartViewModel = new ChartViewModel()
                    {
                        UserName = itemUser.nameUser,
                        Time = formatTime(itemUser.timeuser),
                    };
                    chartListViewModel.ChartViewModes.Add(chartViewModel);
                }
                chartLists.Add(chartListViewModel);
            };
            ViewBag.nameProject = nameProject;
            ViewBag.projectId = id;
            return View(chartLists);
        }
        /// <summary>
        /// ShowChartResult (Get.)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> ShowChartResult(int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var records = await _recordManager.GetAllByProjectAndUserIdAsync(id, userId);
            var usersRecord = _userManager.Users;
            var recordList = new List<string[]>();

            //var companies = records.GroupBy(p => p.UserId).Select(g => new
            //{
            //    id = g.Key,
            //    Count = g.Count(),
            //    atimelisss = g.Select(p => p),
            //});
            //foreach (var item in companies)
            //{
            //    var time = 0;
            //    string NameUser = usersRecord.FirstOrDefault(u => u.Id == item.id).FullName;

            //    foreach (var recordlists in item.atimelisss)
            //    {
            //        time += ConvertDateTimeToSeconds(recordlists.End);
            //    }

            //    string[] asdasd = { @NameUser, Convert.ToString(time) };
            //    recordList.Add(asdasd);
            //}

            var nameUserAndTime = records.GroupBy(r => r.UserId).Select(k => new
            {
                nameUser = usersRecord.FirstOrDefault(u => u.Id == k.Key).FullName,
                timeuser = k.Sum(p => ConvertDateTimeToSeconds(p.End)),
            }).ToList();
            
            foreach (var item in nameUserAndTime)
            {
                string[] record = { item.nameUser, Convert.ToString(item.timeuser) };
                recordList.Add(record);
            }
            var recordJson = new Root()
            {
                chart = new Charts()
                {
                    type = "pie",
                    data = recordList,
                }
            };
            return Json(recordJson);
        }
        /// <summary>
        /// Convert datetime to seconds
        /// </summary>
        /// <param name="asd"></param>
        /// <returns></returns>
        private int ConvertDateTimeToSeconds(DateTime asd)
        {
            var second = asd.Second;
            var minute = asd.Minute;
            var hours = asd.Hour;
            var seconds = second + (minute * 60) + (hours * 3600);
            return seconds;
        }
        /// <summary>
        /// Output format
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        private string formatTime(double time)
        {
            var hours = Math.Floor((time / 3600));
            var minutes = Math.Floor(((time / 3600) - hours) * 60);
            var seconds = time % 60;
            var secondsResult = $"{ seconds}";
            var minutesResult = $"{ minutes}";
            if (seconds < 10)
            {
                secondsResult = $"0{ seconds}";
            }
            if (minutes < 10)
            {
                minutesResult = $"0{ minutes}";
            }
            return $"{hours}: {minutesResult}: {secondsResult}";
        }
    }
}
