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
    /// <inheritdoc cref="IGoalManager"/>
    class GoalManager : IGoalManager
    {
        private readonly IRepositoryManager<Goal> _goalRepository;
        private readonly IRepositoryManager<Project> _projectRepository;

        public GoalManager(
            IRepositoryManager<Goal> goalRepository,
            IRepositoryManager<Project> projectRepository)
        {
            _goalRepository = goalRepository ?? throw new ArgumentNullException(nameof(goalRepository));
            _projectRepository = projectRepository ?? throw new ArgumentNullException(nameof(projectRepository));
        }

        public async Task CreateAsync(GoalDto model, string userId)
        {
            model = model ?? throw new ArgumentNullException(nameof(model));
            userId = userId ?? throw new ArgumentNullException($"{nameof(userId)} id is null");

            var isUserProjectExist = await _projectRepository
                .GetAll()
                .AnyAsync(p => p.Id == model.ProjectId && p.Users.Any(p => p.Id == userId));

            if (!isUserProjectExist)
            {
                throw new Exception($"'{nameof(model.ProjectId)}' forbidden.");
            }

            var project = await _projectRepository.GetAll().FirstOrDefaultAsync(p => p.Id == model.ProjectId);
            //TODO add userId in Goal
            var goal = new Goal
            {
                ProjectId = model.ProjectId,
                Text = model.Text,
                IsComplete = model.IsComplete,
                Start = DateTime.Now,
                End = DateTime.Now,
            };

            await _goalRepository.CreateAsync(goal);
            await _goalRepository.SaveChangesAsync();
        }
        public async Task<GoalDto> GetById(int id)
        {
            var goalList = await _goalRepository.GetAll().ToListAsync();
            var goal = goalList.FirstOrDefault(p => p.Id == id);

            return new GoalDto
            {
                Id = goal.Id,
                ProjectId = goal.ProjectId,
                IsComplete = goal.IsComplete,
                Text = goal.Text,
                Start = goal.Start,
                End = goal.End,
            };
        }
        public async Task<IEnumerable<GoalDto>> GetAllByProjectAndUserIdAsync(int projectId, string userId)
        {

            var goals = await _goalRepository
                .GetAll()
                .Include(r => r.Project)
                .ThenInclude(r => r.Users)
                .Where(r => r.ProjectId == projectId && r.Project.Users.Any(p => p.Id == userId))
                .ToListAsync();

            if (!goals.Any())
            {
                var goalList = new List<GoalDto>();
                goalList.Add(new GoalDto() { Id = 0, Text = "Работы без задачи" });
                return goalList;
            }
            //TODO 
            goals.Add(new Goal() { Id = 0, Text = "Работы без задачи" });
            return (goals.Select(goal => new GoalDto
            {
                Id = goal.Id,
                ProjectId = goal.ProjectId,
                Text = goal.Text,
                IsComplete = goal.IsComplete,
                Start = goal.Start,
                End = goal.End,
            }))
            .ToList();
        }

        public async Task UpdateAsync(GoalDto model, string userId)
        {
            model = model ?? throw new ArgumentNullException(nameof(model));

            var goal = await _goalRepository
                .GetAllAsTracking()
                .Include(r => r.Project)
                .ThenInclude(r => r.Users)
                .SingleOrDefaultAsync(r => r.Id == model.Id && r.Project.Users.Any(p => p.Id == userId));

            if (goal is null)
            {
                throw new Exception($"'{nameof(model.Id)}' record not found.");
            }

            if (goal.IsComplete != model.IsComplete)
            {
                goal.IsComplete = model.IsComplete;
            }

            if (goal.Text != model.Text)
            {
                goal.Text = model.Text;
            }

            if (goal.End != model.End)
            {
                goal.End = model.End;
            }

            await _goalRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id, string userId)
        {
            var goal = await _goalRepository
            .GetAll()
            .Include(r => r.Project)
            .ThenInclude(r => r.Users)
            .SingleOrDefaultAsync(r => r.Id == id && r.Project.Users.Any(p => p.Id == userId));

            if (goal is null)
            {
                throw new Exception($"'{nameof(id)}' record not found.");
            }

            _goalRepository.Delete(goal);
            await _goalRepository.SaveChangesAsync();
        }
    }
}
