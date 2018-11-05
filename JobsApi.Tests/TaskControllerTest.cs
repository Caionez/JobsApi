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
        readonly ITaskRepository _taskRepository;
        readonly TasksController _controller;        

        public TasksControllerTest()
        {
            _taskRepository = new TaskRepositoryFake();
            _controller = new TasksController(_taskRepository);
        }

        [Fact]
        public void Get_WhenCalled_ReturnsListOfTasks()
        {
            var result = _controller.Get(DateTime.Now);
            Assert.IsAssignableFrom<IEnumerable<Task>>(result);
        }
                
        [Fact]
        public void Get_WhenCalled_ReturnsAllItems()
        {
            var result = _controller.Get(null);
            Assert.Equal(3, (result as List<Task>).Count);
        }

        [Fact]
        public void Post_WhenCalled_CreatesTask()
        {
            var task4 = new Task() { Id = 4, Name = "Task 4", Completed = false, CreatedAt = DateTime.Now, Weight = 1 };

            var result = _controller.Post(task4);
            Assert.IsType<CreatedAtRouteResult>(result);
        }

        [Fact]
        public void Post_WhenCalled_FailCreatingTask()
        {
            var task5 = new Task() { Id = 5, Name = "Task 5", Completed = false, CreatedAt = DateTime.Now, Weight = 1 };
            _controller.ModelState.AddModelError("fakeModelError", "fakeModelError");

            var result = _controller.Post(task5);
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void GetById_WhenCalled_ReturnsSpecificTask()
        {
            var result = _controller.GetById(1);
            Assert.NotNull(result);
        }

        [Fact]
        public void GetById_WhenCalled_ReturnsNull()
        {
            var result = _controller.GetById(4);
            Assert.Null(result);
        }
    }
}
