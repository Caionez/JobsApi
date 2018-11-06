using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Unisys_JobsApi.Models;

namespace Unisys_JobsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly IApiRepository _taskRepository;

        public TasksController(IApiRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        // GET api/tasks
        [HttpGet]
        public IEnumerable<Models.Task> Get(DateTime? createdAt)
        {
            return _taskRepository.GetAllTasks(createdAt);
        }

        // POST api/tasks
        [HttpPost]
        public IActionResult Post([FromBody] Models.Task value)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            _taskRepository.AddTask(value);

            return CreatedAtRoute("tasks", value);
        }

        // GET api/tasks/5
        [HttpGet("{id}", Name = "GetTask")]
        public Models.Task GetById(int id)
        {
            var item = _taskRepository.FindTask(id);

            return item;
        }

        // DELETE api/tasks/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var task = _taskRepository.FindTask(id);

            if (task == null)
                return NotFound();

            _taskRepository.RemoveTask(id);
            return NoContent();
        }

        // PUT api/tasks/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Models.Task value)
        {
            if (value == null || value.Id != id || !ModelState.IsValid)
                return BadRequest();

            var task = _taskRepository.FindTask(id);

            if (task == null)
                return NotFound();

             task.Name = value.Name;
             task.Completed = value.Completed;
             task.Weight = value.Weight;
             task.CreatedAt = value.CreatedAt;

            _taskRepository.UpdateTask(task);
            return NoContent();
        }
    }
}
