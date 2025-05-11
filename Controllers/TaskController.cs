using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using System.Security.Claims;
using TaskManagementSystem.Data;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // valid JWT token to access any endpoint
    public class TaskController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public TaskController(AppDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // 📌 GET: /api/tasks
        // Returns all tasks that belong to the logged-in user
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskItem>>> GetTasks()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            var tasks = await _context.TaskItems
            .Where(t=> t.UserId == userId)
            .ToListAsync();


            return Ok();
        }

          // 📌 GET: /api/tasks/{id}
        // Get a specific task by ID (only if it belongs to the user)
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskItem>> GetTask(int id)
        {
             string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            var task = await _context.TaskItems.FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

            if(task == null) return NotFound();

        return Ok(task);
        }
        

        // 📌 POST: /api/tasks
        // Create a new task for the logged-in user
        [HttpPost]
        public async Task<ActionResult> CreateTask(TaskDto dto)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var task = new TaskItem
            {
                Title = dto.Title,
                Description = dto.Description,
                IsCompleted = dto.IsCompleted,
                UserId = userId
            };

            _context.TaskItems.Add(task);
            await _context.SaveChangesAsync();


            return CreatedAtAction(nameof(GetTask), new {id = task.Id}, task);

        }

        // 📌 PUT: /api/tasks/{id}
        // Update an existing task if it belongs to the user
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, TaskDto dto)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            var task = await _context.TaskItems.FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);
            if (task == null) return NotFound();

            task.Title = dto.Title;
            task.Description = dto.Description;
            task.IsCompleted = dto.IsCompleted;

            await _context.SaveChangesAsync();
            return NoContent(); // 204 Success
        }

        // 📌 DELETE: /api/tasks/{id}
        // Delete a task (only if it belongs to the user)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            var task = await _context.TaskItems.FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);
            if (task == null) return NotFound();

            _context.TaskItems.Remove(task);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }




}