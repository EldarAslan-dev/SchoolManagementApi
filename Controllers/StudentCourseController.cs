
using Microsoft.AspNetCore.Mvc;

namespace SchoolManagementApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentCourseController : ControllerBase
{
    private readonly AppDbContext _context;

    public StudentCourseController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> EnrollStudentInCourse([FromBody] StudentCourse studentCourse)
    {
        bool studentExists = await _context.Students.AnyAsync(s => s.Id == studentCourse.StudentId);
        if (!studentExists)
            return NotFound($"Id-si {studentCourse.StudentId} olan tələbə tapılmadı!");

        bool courseExists = await _context.Courses.AnyAsync(c => c.Id == studentCourse.CourseId);
        if (!courseExists)
            return NotFound($"Id-si {studentCourse.CourseId} olan kurs tapılmadı!");

        bool alreadyEnrolled = await _context.StudentCourses.AnyAsync(sc =>
            sc.StudentId == studentCourse.StudentId && sc.CourseId == studentCourse.CourseId);

        if (alreadyEnrolled)
            return BadRequest("Bu tələbə artıq həmin kursa qeydiyyatdan keçib!");

        _context.StudentCourses.Add(studentCourse);
        await _context.SaveChangesAsync();

        return StatusCode(201, new
        {
            Message = "Tələbə kursa uğurla əlavə edildi!",
            studentCourse.StudentId,
            studentCourse.CourseId
        });
    }

    [HttpGet("{studentId}")]
    public async Task<IActionResult> GetStudentCourses(int studentId)
    {
        var student = await _context.Students
            .Where(s => s.Id == studentId)
            .Select(s => new
            {
                s.Id,
                s.FullName,
                Courses = s.Courses.Select(sc => new
                {
                    sc.CourseId,
                    CourseName = sc.Course != null ? sc.Course.Name : null
                })
            })
            .FirstOrDefaultAsync();

        if (student == null)
        {
            return NotFound($"Id-si {studentId} olan tələbə tapılmadı!");
        }

        return Ok(student);
    }
}