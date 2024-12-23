using API.Controllers;
using API.Models;
using API.Repository;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace TaskTracker.Tests;

public class TasksControllerTests
{
    private readonly Mock<ITaskRepository> _mockTaskRepository;
    private readonly TasksController _controller;

    public TasksControllerTests()
    {
        _mockTaskRepository = new Mock<ITaskRepository>();
        _controller = new TasksController(_mockTaskRepository.Object);
    }

    [Fact]
    public async Task GetAllTasks_ReturnsOkResult_WithTasks()
    {
        // Arrange
        var tasks = new List<TaskEntity>
            {
                new TaskEntity { Id = 1, Title = "Task 1" },
                new TaskEntity { Id = 2, Title = "Task 2" }
            };

        _mockTaskRepository.Setup(repo => repo.GetAllTasksAsync()).ReturnsAsync(tasks);

        // Act
        var result = await _controller.GetAllTasks();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnTasks = Assert.IsAssignableFrom<IEnumerable<TaskEntity>>(okResult.Value);
        Assert.Equal(2, ((List<TaskEntity>)returnTasks).Count);
    }

    [Fact]
    public async Task GetTask_ReturnsOkResult_WithTask()
    {
        // Arrange
        var task = new TaskEntity { Id = 1, Title = "Task 1" };
        _mockTaskRepository.Setup(repo => repo.GetTaskByIdAsync(1)).ReturnsAsync(task);

        // Act
        var result = await _controller.GetTask(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnTask = Assert.IsAssignableFrom<TaskEntity>(okResult.Value);
        Assert.Equal(1, returnTask.Id);
    }

    [Fact]
    public async Task GetTask_ReturnsNotFound_WhenTaskDoesNotExist()
    {
        // Arrange
        _mockTaskRepository.Setup(repo => repo.GetTaskByIdAsync(1)).ReturnsAsync((TaskEntity)null);

        // Act
        var result = await _controller.GetTask(1);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task CreateTask_ReturnsCreatedAtActionResult_WithCreatedTask()
    {
        // Arrange
        var task = new TaskEntity { Id = 1, Title = "Task 1" };
        _mockTaskRepository.Setup(repo => repo.CreateTaskAsync(task)).ReturnsAsync(task);

        // Act
        var result = await _controller.CreateTask(task);

        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
        var returnTask = Assert.IsAssignableFrom<TaskEntity>(createdAtActionResult.Value);
        Assert.Equal(1, returnTask.Id);
    }

    [Fact]
    public async Task UpdateTask_ReturnsNoContent_WhenTaskUpdated()
    {
        // Arrange
        var task = new TaskEntity { Id = 1, Title = "Task 1" };
        _mockTaskRepository.Setup(repo => repo.UpdateTaskAsync(task)).ReturnsAsync(true);

        // Act
        var result = await _controller.UpdateTask(1, task);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task UpdateTask_ReturnsBadRequest_WhenIdsDoNotMatch()
    {
        // Arrange
        var task = new TaskEntity { Id = 1, Title = "Task 1" };

        // Act
        var result = await _controller.UpdateTask(2, task);

        // Assert
        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async Task DeleteTask_ReturnsNoContent_WhenTaskDeleted()
    {
        // Arrange
        _mockTaskRepository.Setup(repo => repo.DeleteTaskAsync(1)).ReturnsAsync(true);

        // Act
        var result = await _controller.DeleteTask(1);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task DeleteTask_ReturnsNotFound_WhenTaskDoesNotExist()
    {
        // Arrange
        _mockTaskRepository.Setup(repo => repo.DeleteTaskAsync(1)).ReturnsAsync(false);

        // Act
        var result = await _controller.DeleteTask(1);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }
}