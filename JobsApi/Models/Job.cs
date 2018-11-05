using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public List<Models.Task> Tasks { get; set; }
        
        [ForeignKey("ParentJobId")]
        public virtual Job ParentJob { get; set; }
        public int? ParentJobId { get; set; }
    }
}