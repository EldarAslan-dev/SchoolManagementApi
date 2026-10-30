using Microsoft.AspNetCore.Mvc;
namespace SchoolManagementApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentController : ControllerBase
{
    private readonly AppDbContext _context;

    public StudentController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> CreateStudent([FromBody] Student student)
    {
        bool classroomExists = await _context.Classrooms
            .AnyAsync(c => c.Id == student.ClassroomId);

          if (!classroomExists)
        {
          return NotFound($"Id-si {student.ClassroomId} olan sinif tapılmadı!");
        }

        _context.Students.Add(student);
        await _context.SaveChangesAsync();
        return StatusCode(201, new
        {
            student.Id,
            student.FullName,
            student.Age,
            student.ClassroomId
        });
    }

    [HttpGet]
    public async Task<IActionResult> GetAllStudents()
    {
             var students = await _context.Students
            .Select(s => new
            {
                s.Id,
                s.FullName,
                s.Age,
                s.ClassroomId,
                ClassroomName = s.Classroom != null ? s.Classroom.Name : null
            })
            .ToListAsync();
             return Ok(students);
    }
}