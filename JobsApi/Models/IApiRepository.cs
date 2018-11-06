using System.Collections.Generic;

namespace Unisys_JobsApi.Models
{
    public interface IApiRepository
    {
        void AddJob(Job item);
        IEnumerable<Job> GetAllJobs(bool? sortByWeight);
        Job FindJob(int key);
        void RemoveJob(int key);
        void UpdateJob(Job item);

        void AddTask(Task item);
        IEnumerable<Task> GetAllTasks(System.DateTime? createdAt);
        Task FindTask(int key);
        void RemoveTask(int key);
        void UpdateTask(Task item);
    }
}