using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS_DotNet02_Online_Kaloska.TmTracker.Data.Contexts;
using TMS_DotNet02_Online_Kaloska.TmTracker.Data.Models;
using TMS_DotNet02_Online_Kaloska.TmTracker.Logic.Interfaces;
using TMS_DotNet02_Online_Kaloska.TmTracker.Logic.ModelsDto;

namespace TMS_DotNet02_Online_Kaloska.TmTracker.Logic.Managers
{
    /// <inheritdoc cref="IRecordManager"/>
    public class RecordManager : IRecordManager
    {
        private readonly IRepositoryManager<Record> _recordRepository;
        private readonly IRepositoryManager<Project> _projectRepository;
        private readonly DbContext _context;

        public RecordManager(
            IRepositoryManager<Record> recordRepository,
            IRepositoryManager<Project> projectRepository, ApplicationContext context)
        {
            _recordRepository = recordRepository ?? throw new ArgumentNullException(nameof(recordRepository));
            _projectRepository = projectRepository ?? throw new ArgumentNullException(nameof(projectRepository));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task CreateAsync(RecordDto model, string userId)
        {
            if (model.ProjectId == 0)
            {
                throw new Exception($"'{nameof(model.ProjectId)}' cannot be zero identifier.");
            }

            var isUserProjectExist = await _projectRepository
                .GetAll()
                .AnyAsync(p => p.Id == model.ProjectId && p.Users.Any(p => p.Id == userId));

            if (!isUserProjectExist)
            {
                throw new Exception($"'{nameof(model.ProjectId)}' forbidden.");
            }

            var record = new Record
            {
                Start = model.Start,
                End = model.End,
                ProjectId = model.ProjectId,
                UserId = model.UserId,
                GoalId = model.GoalId,
            };

            await _recordRepository.CreateAsync(record);
            await _recordRepository.SaveChangesAsync();
        }
        public async Task<IEnumerable<RecordDto>> GetAllByProjectAndUserIdAsync(int projectId, string userId)
        {
            var project = await _projectRepository.GetAll().Include(p => p.Users).FirstOrDefaultAsync(p => p.Id == projectId);
            var isUser = project.Users.Any(u => u.Id == userId);
            var records = await _recordRepository
                .GetAll()
                .Include(r => r.Project)
                .Include(r => r.User)
                .Where(r => r.ProjectId == projectId && isUser)
                .ToListAsync();

            if (!records.Any())
            {
                return new List<RecordDto>();
            }

            var models = new List<RecordDto>();
            foreach (var record in records)
            {
                models.Add(new RecordDto
                {
                    Id = record.Id,
                    ProjectId = record.ProjectId,
                    Start = record.Start,
                    End = record.End,
                    UserId = record.UserId,
                    GoalId = record.GoalId,
                });
            }
            return models;
        }

        public async Task DeleteAsync(int id, string userId, int projectId)
        {
            var record = await _recordRepository
                .GetAll()
                .Include(r => r.Project)
                .Include(r => r.User)
                .SingleOrDefaultAsync(r => r.Id == id && r.Project.Id == projectId && r.User.Id == userId);

            if (record is null)
            {
                throw new Exception($"'{nameof(id)}' record not found.");
            }

            _recordRepository.Delete(record);
            await _recordRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(RecordDto model, string userId)
        {
            model = model ?? throw new ArgumentNullException(nameof(model));

            var record = await _recordRepository
                .GetAll()
                .Include(r => r.Project)
                .SingleOrDefaultAsync(r => r.Id == model.Id && r.Project.UserId == userId);

            if (record is null)
            {
                throw new Exception($"'{nameof(model.Id)}' record not found.");
            }

            if (model.Start >= model.End)
            {
                throw new Exception($"'{nameof(model.Start)}' >= '{nameof(model.End)}'.");
            }

            if (record.Start != model.Start)
            {
                record.Start = model.Start;
            }

            if (record.End != model.End)
            {
                record.End = model.End;
            }

            await _recordRepository.SaveChangesAsync();
        }
    }
}
