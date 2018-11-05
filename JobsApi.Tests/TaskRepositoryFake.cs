using System;
using System.Collections.Generic;
using System.Linq;
using Unisys_JobsApi.Models;

namespace Unisys_JobsApi.Tests
{
    public class TaskRepositoryFake : ITaskRepository
    {
        private readonly List<Task> _tasks;
        public TaskRepositoryFake()
        {
            _tasks = new List<Task>()
            {
                new Task() { Id = 1, Name = "Task 1", Completed = false, CreatedAt = System.DateTime.Now, Weight = 1 },
                new Task() { Id = 2, Name = "Task 2", Completed = false, CreatedAt = System.DateTime.Now, Weight = 3 },
                new Task() { Id = 3, Name = "Task 3", Completed = false, CreatedAt = System.DateTime.Now, Weight = 2 }
            };
        }

        public IEnumerable<Task> GetAll(DateTime? createdAt)
        {
            if (createdAt.HasValue)
                return (from task in _tasks
                        where task.CreatedAt >= createdAt
                        select task).ToList();
            else
                return _tasks.ToList();
        }

        public void Add(Task item)
        {
            _tasks.Add(item);
        }

        public Task Find(int key)
        {
            return _tasks.FirstOrDefault(t => t.Id == key);
        }

        public void Remove(int key)
        {
            var entity = _tasks.First(t => t.Id == key);
            _tasks.Remove(entity);
        }

        public void Update(Task item)
        {
            var entity = Find(item.Id);
            _tasks.Remove(entity);
            entity = item;
            _tasks.Add(entity);
        }
    }
}