using Microsoft.AspNetCore.Mvc;

namespace SchoolManagementApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClassroomController : ControllerBase
{
    private readonly AppDbContext _context;
    public ClassroomController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> CreateClassroom([FromBody] Classroom classroom)
    {
        _context.Classrooms.Add(classroom);
        await _context.SaveChangesAsync();
        return StatusCode(201, classroom);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllClassrooms()
    {
             var classrooms = await _context.Classrooms
            .Select(c => new
            {
                c.Id,
                c.Name,
                Students = c.Students.Select(s => new { s.Id, s.FullName, s.Age })
            })
            .ToListAsync();
             return Ok(classrooms);
    }
}