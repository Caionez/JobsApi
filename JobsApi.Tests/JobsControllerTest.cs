using Xunit;
using System.Collections.Generic;
using Unisys_JobsApi.Models;
using Unisys_JobsApi.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Unisys_JobsApi.Tests
{
    public class JobsControllerTest
    {
        readonly IJobRepository _jobRepository;
        readonly JobsController _controller;        

        public JobsControllerTest()
        {
            _jobRepository = new JobRepositoryFake();
            _controller = new JobsController(_jobRepository);
        }

        [Fact]
        public void Get_WhenCalled_ReturnsListOfJobs()
        {
            var result = _controller.Get(true);
            Assert.IsAssignableFrom<IEnumerable<Job>>(result);
        }
                
        [Fact]
        public void Get_WhenCalled_ReturnsAllItems()
        {
            var result = _controller.Get(true);
            Assert.Equal(2, (result as List<Job>).Count);
        }

        [Fact]
        public void Post_WhenCalled_CreatesJob()
        {
            var taskA = new Task() { Id = 1, Name = "Task 4", Completed = false, CreatedAt = System.DateTime.Now, Weight = 1 };
            var jobA = new Job { Id = 3, Name = "Job 3", Active = true, ParentJob = null, Tasks = new List<Task>() { taskA } };

            var result = _controller.Post(jobA);
            Assert.IsType<CreatedAtRouteResult>(result);
        }

        [Fact]
        public void Post_WhenCalled_FailCreatingJob()
        {
            var taskA = new Task() { Id = 1, Name = "Task 4", Completed = false, CreatedAt = System.DateTime.Now, Weight = 1 };
            var jobA = new Job { Id = 1, Name = "Job 3", Active = true, ParentJob = null, Tasks = new List<Task>() { taskA } };
            _controller.ModelState.AddModelError("fakeModelError", "fakeModelError");

            var result = _controller.Post(jobA);
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void GetById_WhenCalled_ReturnsSpecificJob()
        {
            var result = _controller.GetById(1);
            Assert.NotNull(result);
        }

        [Fact]
        public void GetById_WhenCalled_ReturnsNull()
        {
            var result = _controller.GetById(3);
            Assert.Null(result);
        }

        [Fact]
        public void Delete_WhenCalled_DeletesJob()
        {
            var result = _controller.Delete(1);
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void Delete_WhenCalled_FailDeletingJobNotFound()
        {
            var result = _controller.Delete(3);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Put_WhenCalled_UpdatesJob()
        {
            var taskA = new Task() { Id = 1, Name = "Task 1 Edited", Completed = true, CreatedAt = System.DateTime.Now, Weight = 1 };
            var jobA = new Job { Id = 1, Name = "Job 1 Edited", Active = true, ParentJob = null, Tasks = new List<Task>() { taskA } };

            var result = _controller.Put(jobA.Id, jobA);
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void Put_WhenCalled_FailUpdatingJobNotFound()
        {
            var taskA = new Task() { Id = 1, Name = "Task 4 Edited", Completed = false, CreatedAt = System.DateTime.Now, Weight = 1 };
            var jobA = new Job { Id = 3, Name = "Job 3 Edited", Active = true, ParentJob = null, Tasks = new List<Task>() { taskA } };

            var result = _controller.Put(jobA.Id, jobA);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Put_WhenCalled_FailUpdatingJobInvalidModel()
        {
            var taskA = new Task() { Id = 1, Name = "Task 1", Completed = false, CreatedAt = System.DateTime.Now, Weight = 1 };
            var jobA = new Job { Id = 1, Name = "Job A", Active = true, ParentJob = null, Tasks = new List<Task>() { taskA } };
            _controller.ModelState.AddModelError("fakeModelError", "fakeModelError");

            var result = _controller.Put(jobA.Id, jobA);
            Assert.IsType<BadRequestResult>(result);
        }
    }
}
