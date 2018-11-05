using System.Collections.Generic;

namespace Unisys_JobsApi.Models
{
    public interface ITaskRepository
    {
        void Add(Models.Task item);
        IEnumerable<Models.Task> GetAll(System.DateTime? createdAt);
        Models.Task Find(int key);
        void Remove(int key);
        void Update(Models.Task item);
    }
}