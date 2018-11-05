using System.Collections.Generic;

namespace Unisys_JobsApi.Models
{
    public interface IJobRepository
    {
        void Add(Job item);
        IEnumerable<Job> GetAll(bool? sortByWeight);
        Job Find(int key);
        void Remove(int key);
        void Update(Job item);        
    }
}