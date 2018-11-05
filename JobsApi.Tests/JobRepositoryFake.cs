using System.Collections.Generic;
using System.Linq;
using Unisys_JobsApi.Models;

namespace Unisys_JobsApi.Tests
{
    public class JobRepositoryFake : IJobRepository
    {
        private readonly List<Job> _jobs;
        public JobRepositoryFake()
        {
            var task1 = new Task() { Id = 1, Name = "Task 1", Completed = false, CreatedAt = System.DateTime.Now, Weight = 1 };
            var task2 = new Task() { Id = 2, Name = "Task 2", Completed = false, CreatedAt = System.DateTime.Now, Weight = 3 };
            var task3 = new Task() { Id = 3, Name = "Task 3", Completed = false, CreatedAt = System.DateTime.Now, Weight = 2 };

            _jobs = new List<Job>()
            {
                new Job { Id = 1, Name = "Job 1", Active = false, ParentJob = null, Tasks = new List<Task>() {task1} },
                new Job { Id = 2, Name = "Job 2", Active = false, ParentJob = null, Tasks = new List<Task>() {task2, task3} }
            };
        }
        public IEnumerable<Job> GetAll(bool? sortByWeight)
        {
            if (sortByWeight.HasValue && sortByWeight.Value)
                return (from job in _jobs
                       orderby job.Tasks.Sum(t => t.Weight) descending
                       select job).ToList();
            else
                return _jobs.ToList();
        }

        public void Add(Job item)
        {
            _jobs.Add(item);
        }

        public Job Find(int key)
        {
            return _jobs.FirstOrDefault(t => t.Id == key);
        }

        public void Remove(int key)
        {
            var entity = _jobs.First(t => t.Id == key);
            _jobs.Remove(entity);
        }

        public void Update(Job item)
        {
            var entity = Find(item.Id);
            _jobs.Remove(entity);
            entity = item;
            _jobs.Add(entity);
        }
    }
}