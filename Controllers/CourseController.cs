using Microsoft.AspNetCore.Mvc;
namespace SchoolManagementApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CourseController : ControllerBase
{
    private readonly AppDbContext _context;
    public CourseController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> CreateCourse([FromBody] Course course)
    {
        _context.Courses.Add(course);
        await _context.SaveChangesAsync();
        return StatusCode(201, new
        {
            course.Id,
            course.Name
        });
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCourses()
    {
             var courses = await _context.Courses
            .Select(c => new
            {
                c.Id,
                c.Name
            })
            .ToListAsync();
             return Ok(courses);
    }
}