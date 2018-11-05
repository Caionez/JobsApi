using System;
using System.Collections.Generic;
using System.Linq;

namespace Unisys_JobsApi.Models
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TaskContext _context;
        public TaskRepository(TaskContext context)
        {
            _context = context;
            Add(new Task { Id = 1, Name = "Task 1", Completed = false, CreatedAt = DateTime.Now, Weight = 1 });
        }

        public IEnumerable<Task> GetAll(DateTime? createdAt)
        {
            if (createdAt.HasValue)
                return (from task in _context.Tasks
                       where task.CreatedAt >= createdAt
                       select task).ToList();
            else
                return _context.Tasks.ToList();
        }

        public void Add(Task item)
        {
            _context.Tasks.Add(item);
            _context.SaveChanges();
        }

        public Task Find(int key)
        {
            return _context.Tasks.FirstOrDefault(t => t.Id == key);
        }

        public void Remove(int key)
        {
            var entity = _context.Tasks.First(t => t.Id == key);
            _context.Tasks.Remove(entity);
            _context.SaveChanges();
        }

        public void Update(Task item)
        {
            _context.Tasks.Update(item);
            _context.SaveChanges();
        }
    }
}