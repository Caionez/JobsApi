using System.Collections.Generic;
using System.Linq;

namespace Unisys_JobsApi.Models
{
    public class JobRepository : IJobRepository
    {
        private readonly JobContext _context;
        public JobRepository(JobContext context)
        {
            _context = context;
            Add(new Job { Id = 1, Name = "Job 1", Active = false, ParentJob = null, Tasks = null });
        }
        public IEnumerable<Job> GetAll(bool? sortByWeight)
        {
            if (sortByWeight.HasValue && sortByWeight.Value)
                return (from jobs in _context.Jobs
                       orderby jobs.Tasks.Sum(t => t.Weight) descending
                       select jobs).ToList();
            else
                return _context.Jobs.ToList();
        }

        public void Add(Job item)
        {
            _context.Jobs.Add(item);
            _context.SaveChanges();
        }

        public Job Find(int key)
        {
            return _context.Jobs.FirstOrDefault(t => t.Id == key);
        }

        public void Remove(int key)
        {
            var entity = _context.Jobs.First(t => t.Id == key);
            _context.Jobs.Remove(entity);
            _context.SaveChanges();
        }

        public void Update(Job item)
        {
            _context.Jobs.Update(item);
            _context.SaveChanges();
        }
    }
}