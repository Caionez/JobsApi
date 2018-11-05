using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Unisys_JobsApi.Models;

namespace Unisys_JobsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private readonly IJobRepository _jobRepository;

        public JobsController(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        // GET api/jobs
        [HttpGet]
        public IEnumerable<Job> Get(bool? sortByWeight)
        {
            return _jobRepository.GetAll(sortByWeight);
        }

        // POST api/jobs
        [HttpPost]
        public IActionResult Post(Job value)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            _jobRepository.Add(value);

            return CreatedAtRoute("jobs", value);
        }

        // GET api/jobs/5
        [HttpGet("{id}", Name = "GetJob")]
        public Job GetById(int id)
        {
            var item = _jobRepository.Find(id);

            return item;
        }

        // DELETE api/jobs/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var job = _jobRepository.Find(id);

            if (job == null)
                return NotFound();

            _jobRepository.Remove(id);
            return NoContent();
        }

        // PUT api/jobs/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, Job value)
        {
            if (value == null || value.Id != id || !ModelState.IsValid)
                return BadRequest();

            var job = _jobRepository.Find(id);

            if (job == null)
                return NotFound();

            job.Name = value.Name;
            job.Active = value.Active;
            job.Tasks = value.Tasks;
            job.ParentJob = value.ParentJob;

            _jobRepository.Update(job);
            return NoContent();
        }
    }
}
