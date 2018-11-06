using JobsApi.Helpers;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

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

        [DataType(DataType.Date)]
        [JsonConverter(typeof(JsonDateConverter))]
        public DateTime CreatedAt { get; set; }
    }
}