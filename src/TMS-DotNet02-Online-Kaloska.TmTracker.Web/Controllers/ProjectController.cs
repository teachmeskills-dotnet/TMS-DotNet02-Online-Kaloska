using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TMS_DotNet02_Online_Kaloska.TmTracker.Data.Models;
using TMS_DotNet02_Online_Kaloska.TmTracker.Logic.Interfaces;
using TMS_DotNet02_Online_Kaloska.TmTracker.Logic.ModelsDto;
using TMS_DotNet02_Online_Kaloska.TmTracker.Web.Models;
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
        /// <summary>
        /// ShowUsers (Get.)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="nameProject"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> ShowUsers(int id, string nameProject)
        {
            //Todo change to Mvc
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var users = await _projectManager.GetUsersByProjectIdAsync(id);
            ViewBag.users = users;
            ViewBag.nameProject = nameProject;
            ViewBag.projectId = id;
            return View();
        }
        /// <summary>
        /// AddUserToProject
        /// </summary>
        /// <param name="email"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> AddUserToProject(string email, int projectId)
        {
            var users = _userManager.Users;
            var user = users.FirstOrDefault(u => u.Email == email);
            if (user is not null)
            {
                var project = await _projectManager.GetById(projectId);
                await _projectManager.AddUsertoProject(project, user);
                return Ok();
            }
            return BadRequest();
        }
        /// <summary>
        /// Delete project (Post).
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> DeleteProject(int projectId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var project=await _projectManager.GetById(projectId);
            if (project.UserId == userId)
            {
                await _projectManager.DeleteAsync(projectId, userId);
                return Ok();
            }
            return BadRequest();
        }
        /// <summary>
        /// Show Chat (Get.)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="nameProject"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ShowChat(int id, string nameProject)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var users = _userManager.Users;
            var user = users.FirstOrDefault(u => u.Id == userId).FullName.ToString();
            var userModel = new UserModel()
            {
                FullName = user,
            };
            ViewBag.nameProject = nameProject;
            ViewBag.projectId = id;
            return View(userModel);
        }
        /// <summary>
        /// SearchProjectByNameAsync (Get.)
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> SearchProjectByNameAsync(string name)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var projects = await _projectManager.GetAllByUserIdAsync(userId);
            ProjectListViewModel projectsView = new ProjectListViewModel();
            string pattern = @"\d{4}(-)\d{2}(-)\d{2}";

            if (!Regex.IsMatch(name, pattern))
            {
                IEnumerable<ProjectViewModel> SearchToDate()
                {
                    return projects.Where(x => x.Name.Contains(name)).Select(p => new ProjectViewModel
                    {
                        Id = p.Id,
                        UserId = p.UserId,
                        Name = p.Name,
                        CreationTime = p.CreationTime,
                        IsFavourite = p.IsFavourite,
                    });
                }
                projectsView.Projects = SearchToDate();

                return View(projectsView);
            }
            else
            {
                IEnumerable<ProjectViewModel> SearchToName()
                {
                    return projects.Where(x => x.CreationTime.ToString("yyyy-MM-dd").Equals(name)).Select(p => new ProjectViewModel
                    {
                        Id = p.Id,
                        UserId = p.UserId,
                        Name = p.Name,
                        CreationTime = p.CreationTime,
                        IsFavourite = p.IsFavourite,
                    });
                }
                projectsView.Projects = SearchToName();

                return View(projectsView);
            }
        }
    }
}
