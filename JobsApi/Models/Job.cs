using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Unisys_JobsApi.Models
{
    public class Job
    {
        [Key, Required]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }
        [Required]
        public bool Active { get; set; }
        public virtual List<Task> Tasks { get; set; }
    }
}