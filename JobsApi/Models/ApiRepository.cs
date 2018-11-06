using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Unisys_JobsApi.Models
{
    public class ApiRepository : IApiRepository
    {
        private readonly ApiContext _context;
        public ApiRepository(ApiContext context)
        {
            _context = context;
        }
        public IEnumerable<Job> GetAllJobs(bool? sortByWeight)
        {
            if (sortByWeight.HasValue && sortByWeight.Value)
                return (from jobs in _context.Jobs
                                            .Include(j => j.Tasks)
                       orderby jobs.Tasks.Sum(t => t.Weight) descending
                       select jobs).ToList();
            else
                return _context.Jobs
                        .Include(j => j.Tasks).ToList();
        }

        public void AddJob(Job item)
        {
            _context.Jobs.Add(item);
            _context.SaveChanges();
        }

        public Job FindJob(int key)
        {
            return _context.Jobs.FirstOrDefault(t => t.Id == key);
        }

        public void RemoveJob(int key)
        {
            var entity = _context.Jobs.First(t => t.Id == key);
            _context.Jobs.Remove(entity);
            _context.SaveChanges();
        }

        public void UpdateJob(Job item)
        {
            _context.Jobs.Update(item);
            _context.Tasks.UpdateRange(item.Tasks);
            _context.SaveChanges();
        }

        public IEnumerable<Task> GetAllTasks(System.DateTime? createdAt)
        {
            if (createdAt.HasValue)
                return (from task in _context.Tasks
                        where task.CreatedAt >= createdAt
                        select task).ToList();
            else
                return _context.Tasks.ToList();
        }

        public void AddTask(Task item)
        {
            _context.Tasks.Add(item);
            _context.SaveChanges();
        }

        public Task FindTask(int key)
        {
            return _context.Tasks.FirstOrDefault(t => t.Id == key);
        }

        public void RemoveTask(int key)
        {
            var entity = _context.Tasks.First(t => t.Id == key);
            _context.Tasks.Remove(entity);
            _context.SaveChanges();
        }

        public void UpdateTask(Task item)
        {
            _context.Tasks.Update(item);
            _context.SaveChanges();
        }
    }
}