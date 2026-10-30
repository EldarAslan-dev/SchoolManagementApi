namespace SchoolManagementApi.Entities;

public class Student
{
    public int Id { get; set; }
    public string FullName { get; set; } = null!;
    public int Age { get; set; }
    public int ClassroomId { get; set; }
    public Classroom? Classroom { get; set; } = null!;
    public ICollection<StudentCourse> Courses { get; set; } = new List<StudentCourse>();
}