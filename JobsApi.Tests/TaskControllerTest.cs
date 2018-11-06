using Xunit;
using System.Collections.Generic;
using Unisys_JobsApi.Models;
using Unisys_JobsApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Unisys_JobsApi.Tests
{
    public class TasksControllerTest
    {
        readonly IApiRepository _taskRepository;
        readonly TasksController _controller;        

        public TasksControllerTest()
        {
            _taskRepository = new ApiRepositoryFake();
            _controller = new TasksController(_taskRepository);
        }

        [Fact]
        public void Get_ReturnsListOfTasks()
        {
            var result = _controller.Get(DateTime.Now);
            Assert.IsAssignableFrom<IEnumerable<Task>>(result);
        }
                
        [Fact]
        public void Get_ReturnsAllItems()
        {
            var result = _controller.Get(null);
            Assert.Equal(3, (result as List<Task>).Count);
        }

        [Fact]
        public void Post_CreatesTask()
        {
            var task4 = new Task() { Id = 4, Name = "Task 4", Completed = false, CreatedAt = DateTime.Now, Weight = 1 };

            var result = _controller.Post(task4);
            Assert.IsType<CreatedAtRouteResult>(result);
        }

        [Fact]
        public void Post_FailCreatingTask_InvalidModel()
        {
            var task5 = new Task() { Id = 5, Name = "Task 5", Completed = false, CreatedAt = DateTime.Now, Weight = 1 };
            _controller.ModelState.AddModelError("fakeModelError", "fakeModelError");

            var result = _controller.Post(task5);
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void GetById_ReturnsSpecificTask()
        {
            var result = _controller.GetById(1);
            Assert.NotNull(result);
        }

        [Fact]
        public void GetById_ReturnsNull()
        {
            var result = _controller.GetById(4);
            Assert.Null(result);
        }

        [Fact]
        public void Delete_DeletesTask()
        {
            var result = _controller.Delete(1);
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void Delete_FailDeletingTask_NotFound()
        {
            var result = _controller.Delete(4);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Put_UpdatesTask()
        {
            var taskA = new Task() { Id = 1, Name = "Task 1 Edited", Completed = true, CreatedAt = DateTime.Now, Weight = 1 };

            var result = _controller.Put(taskA.Id, taskA);
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void Put_FailUpdatingTask_NotFound()
        {
            var taskA = new Task() { Id = 4, Name = "Task 4 Edited", Completed = false, CreatedAt = DateTime.Now, Weight = 1 };
            
            var result = _controller.Put(taskA.Id, taskA);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Put_FailUpdatingTask_InvalidModel()
        {
            var taskA = new Task() { Id = 1, Name = "Task 1 Edited", Completed = false, CreatedAt = DateTime.Now, Weight = 1 };
            _controller.ModelState.AddModelError("fakeModelError", "fakeModelError");

            var result = _controller.Put(taskA.Id, taskA);
            Assert.IsType<BadRequestResult>(result);
        }
    }
}
