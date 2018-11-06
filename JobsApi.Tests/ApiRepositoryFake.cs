using System.Collections.Generic;
using System.Linq;
using Unisys_JobsApi.Models;

namespace Unisys_JobsApi.Tests
{
    public class ApiRepositoryFake : IApiRepository
    {
        private readonly List<Job> _jobs;
        private readonly List<Task> _tasks;

        public ApiRepositoryFake()
        {
            _tasks = new List<Task>()
            {
                new Task() { Id = 1, Name = "Task 1", Completed = false, CreatedAt = System.DateTime.Now, Weight = 1 },
                new Task() { Id = 2, Name = "Task 2", Completed = false, CreatedAt = System.DateTime.Now, Weight = 3 },
                new Task() { Id = 3, Name = "Task 3", Completed = false, CreatedAt = System.DateTime.Now, Weight = 2 }
            };

            _jobs = new List<Job>()
            {
                new Job { Id = 1, Name = "Job 1", Active = false, Tasks = new List<Task>() { _tasks[0] } },
                new Job { Id = 2, Name = "Job 2", Active = false, Tasks = new List<Task>() { _tasks[1], _tasks[2] } }
            };
        }
        public IEnumerable<Job> GetAllJobs(bool? sortByWeight)
        {
            if (sortByWeight.HasValue && sortByWeight.Value)
                return (from job in _jobs
                       orderby job.Tasks.Sum(t => t.Weight) descending
                       select job).ToList();
            else
                return _jobs.ToList();
        }

        public void AddJob(Job item)
        {
            _jobs.Add(item);
        }

        public Job FindJob(int key)
        {
            return _jobs.FirstOrDefault(t => t.Id == key);
        }

        public void RemoveJob(int key)
        {
            var entity = _jobs.First(t => t.Id == key);
            _jobs.Remove(entity);
        }

        public void UpdateJob(Job item)
        {
            var entity = FindJob(item.Id);
            _jobs.Remove(entity);
            entity = item;
            _jobs.Add(entity);
        }

        public IEnumerable<Task> GetAllTasks(System.DateTime? createdAt)
        {
            if (createdAt.HasValue)
                return (from task in _tasks
                        where task.CreatedAt >= createdAt
                        select task).ToList();
            else
                return _tasks.ToList();
        }

        public void AddTask(Task item)
        {
            _tasks.Add(item);
        }

        public Task FindTask(int key)
        {
            return _tasks.FirstOrDefault(t => t.Id == key);
        }

        public void RemoveTask(int key)
        {
            var entity = _tasks.First(t => t.Id == key);
            _tasks.Remove(entity);
        }

        public void UpdateTask(Task item)
        {
            var entity = FindTask(item.Id);
            _tasks.Remove(entity);
            entity = item;
            _tasks.Add(entity);
        }
    }
}