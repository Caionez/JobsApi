using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Unisys_JobsApi.Models
{
    public class Task
    {
        [Key, Required]        
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }
        [Required]
        public int Weight { get; set; }
        public bool Completed { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}