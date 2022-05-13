﻿using Microsoft.AspNetCore.Authorization;
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
using TMS_DotNet02_Online_Kaloska.TmTracker.Web.ViewModels;

namespace TMS_DotNet02_Online_Kaloska.TmTracker.Web.Controllers
{
    /// <summary>
    /// Project controller.
    /// </summary>
    [Authorize]
    public class ProjectController : Controller
    {
        UserManager<User> _userManager;
        private IProjectManager _projectManager;
        /// <summary>
        /// Constructor with params.
        /// </summary>
        /// <param name="projectManager">Project manager.</param>
        public ProjectController(IProjectManager projectManager,
            UserManager<User> userManager)
        {
            _projectManager = projectManager ?? throw new ArgumentNullException(nameof(projectManager));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }
        /// <summary>
        /// AddProject (Get).
        /// </summary>
        /// <returns>View.</returns>
        [HttpGet]
        public IActionResult AddProjectAsync()
        {
            return View();
        }
        /// <summary>
        /// AddProject (Post).
        /// </summary>
        /// <param name="projectViewModel"></param>
        /// <returns>Status.</returns>
        [HttpPost]
        public async Task<IActionResult> AddProjectAsync(ProjectViewModel projectViewModel)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var projectDto = new ProjectDto()
            {
                UserId = userId,
                Name = projectViewModel.Name,
                IsFavourite = projectViewModel.IsFavourite,
            };
            var user = await _userManager.FindByIdAsync(userId);

            await _projectManager.CreateAsync(projectDto, user);

            return View("Close");
        }
        /// <summary>
        /// GetProjects (Get).
        /// </summary>
        /// <returns>All user projects.</returns>
        [HttpGet]
        public async Task<IActionResult> GetProjectsAsync()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var projects = await _projectManager.GetAllByUserIdAsync(userId);

            IEnumerable<ProjectViewModel> ProjectViewModels()
            {
                return projects.Select(p => new ProjectViewModel
                {
                    Id = p.Id,
                    UserId = p.UserId,
                    Name = p.Name,
                    CreationTime = p.CreationTime,
                    IsFavourite = p.IsFavourite,
                });
            }

            ProjectListViewModel projectsListViewModel = new()
            {
                Projects = ProjectViewModels()
            };

            return View(projectsListViewModel);
        }
        /// <summary>
        /// GetProjectsIsFavAsync (Get).
        /// </summary>
        /// <returns>All user projects.</returns>
        [HttpGet]
        public async Task<IActionResult> GetProjectsIsFavAsync()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var projects = await _projectManager.GetAllByUserIdAsync(userId);

            IEnumerable<ProjectViewModel> ProjectViewModels()
            {
                return projects.Where(p => p.IsFavourite == true).Select(p => new ProjectViewModel
                {
                    Id = p.Id,
                    UserId = p.UserId,
                    Name = p.Name,
                    CreationTime = p.CreationTime,
                    IsFavourite = p.IsFavourite,
                });
            }

            ProjectListViewModel projectsListViewModelisFav = new()
            {
                Projects = ProjectViewModels()
            };

            return View(projectsListViewModelisFav);
        }
        /// <summary>
        /// Is favourite (Get).
        /// </summary>
        /// <param name="isFav"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> IsFav(bool isFav, int projectId)
        {
            var project = await _projectManager.GetById(projectId);
            project.IsFavourite = isFav;
            await _projectManager.UpdateAsync(project);
            return Ok(true);
        }
        /// <summary>
        /// CreateProjectModalWindow (Get).
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CreateProjectModalWindow()
        {
            return PartialView("CreateProjectModalWindow");
        }
    }
}