using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS_DotNet02_Online_Kaloska.TmTracker.Data.Models;
using TMS_DotNet02_Online_Kaloska.TmTracker.Logic.Interfaces;
using TMS_DotNet02_Online_Kaloska.TmTracker.Logic.ModelsDto;

namespace TMS_DotNet02_Online_Kaloska.TmTracker.Logic.Managers
{
    /// <inheritdoc cref="IProjectManager"/>
    public class ProjectManager : IProjectManager
    {
        private readonly IRepositoryManager<Project> _projectRepository;
        public ProjectManager(IRepositoryManager<Project> projectRepository)
        {

            _projectRepository = projectRepository ?? throw new ArgumentNullException(nameof(projectRepository));
        }
        public async Task CreateAsync(ProjectDto model, User user)
        {
            if (string.IsNullOrEmpty(model.Name))
            {
                throw new Exception($"'{nameof(model.Name)})");
            }

            var project = new Project
            {
                UserId = model.UserId,
                Name = model.Name,
                CreationTime = DateTime.Now,
                IsFavourite = model.IsFavourite,
                Users = new List<User>(),
            };
            project.Users.Add(user);
            await _projectRepository.CreateAsync(project);//ADD user
            await _projectRepository.SaveChangesAsync();
        }
        public async Task AddUsertoProject(ProjectDto model, User user)
        {
            if (string.IsNullOrEmpty(model.Name))
            {
                throw new Exception($"'{nameof(model.Name)})");
            }

            var project = await _projectRepository.GetAllAsTracking().Include(p => p.Users).FirstOrDefaultAsync(p => p.Id == model.Id);

            var projectUser = project.Users.FirstOrDefault(u => u.Id == user.Id);
            if (projectUser is null)
            {
                project.Users.Add(user);
                await _projectRepository.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id, string userId)
        {
            var project = await _projectRepository.GetEntityAsync(p => p.Id == id && p.UserId == userId);

            if (project is null)
            {
                throw new NullReferenceException($"'{nameof(id)}' project not found."); //TODO
            }
            _projectRepository.Delete(project);
            await _projectRepository.SaveChangesAsync();
        }

        public async Task<ProjectDto> GetById(int id)
        {
            var projectlist = await _projectRepository.GetAll().ToListAsync();
            var project = projectlist.FirstOrDefault(p => p.Id == id);

            return new ProjectDto
            {
                Id = project.Id,
                UserId = project.UserId,
                CreationTime = project.CreationTime,
                IsFavourite = project.IsFavourite,
                Name = project.Name
            };
        }
        public async Task<IEnumerable<UserDto>> GetUsersByProjectIdAsync(int id)
        {
            var projectlist = await _projectRepository.GetAll().Include(p => p.Users).ToListAsync();
            var project = projectlist.FirstOrDefault(p => p.Id == id);

            return project.Users.Select(p => new UserDto
            {
                Email = p.Email,
                FullName = p.FullName,
                Id = p.Id,
            });
        }
        public async Task<IEnumerable<ProjectDto>> GetAllByUserIdAsync(string userId)
        {
            var projects = await _projectRepository.GetAll()
                .Include(p => p.Users)
                .Where(p => p.Users.Any(c => c.Id == userId))
                .ToListAsync();

            var projectsDto = new List<ProjectDto>();

            if (!projects.Any())
            {
                return new List<ProjectDto>();
            }

            foreach (var item in projects)
            {
                projectsDto.Add(new ProjectDto
                {
                    Id = item.Id,
                    UserId = item.UserId,
                    Name = item.Name,
                    CreationTime = item.CreationTime,
                    IsFavourite = item.IsFavourite,
                }
                );
            }
            return projectsDto;
        }

        public async Task UpdateAsync(ProjectDto model)
        {
            model = model ?? throw new ArgumentNullException(nameof(model));

            var project = await _projectRepository.GetEntityAsync(p => p.Id == model.Id && p.UserId == model.UserId);

            if (project is null)
            {
                throw new NullReferenceException($"'{nameof(model.Id)}' project not found.");
            }

            if (project.Name != model.Name)
            {
                project.Name = model.Name;
            }

            if (project.IsFavourite != model.IsFavourite)
            {
                project.IsFavourite = model.IsFavourite;
            }

            await _projectRepository.SaveChangesAsync();
        }
    }
}
