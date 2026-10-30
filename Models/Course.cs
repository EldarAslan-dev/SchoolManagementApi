namespace SchoolManagementApi.Entities;

public class Course
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public ICollection<StudentCourse> Students { get; set; } = new List<StudentCourse>();
}