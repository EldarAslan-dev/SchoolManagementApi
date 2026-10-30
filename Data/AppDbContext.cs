namespace SchoolManagementApi.Data;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    public DbSet<Student> Students { get; set; }
    public DbSet<Classroom> Classrooms { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<StudentCourse> StudentCourses { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
                   base.OnModelCreating(modelBuilder);
                   modelBuilder.Entity<Student>(entity =>
        {
                   entity.Property(s => s.FullName)
                  .HasMaxLength(100)
                  .IsRequired();

                   entity.Property(s => s.Age)
                  .HasColumnType("int");
        });
                   modelBuilder.Entity<Classroom>(entity =>
        {
                   entity.Property(c => c.Name)
                  .HasMaxLength(50)
                  .IsRequired();
        });
                   modelBuilder.Entity<Course>(entity =>
        {
                   entity.Property(c => c.Name)
                  .HasMaxLength(100)
                  .IsRequired();
        });
                   modelBuilder.Entity<Student>()
                  .HasOne(s => s.Classroom)
                  .WithMany(c => c.Students)
                  .HasForeignKey(s => s.ClassroomId);
                   modelBuilder.Entity<StudentCourse>()
                  .HasKey(sc => new { sc.StudentId, sc.CourseId });
                   modelBuilder.Entity<StudentCourse>()
                  .HasOne(sc => sc.Student)
                  .WithMany(s => s.Courses)
                  .HasForeignKey(sc => sc.StudentId);
                   modelBuilder.Entity<StudentCourse>()
                  .HasOne(sc => sc.Course)
                  .WithMany(c => c.Students)
                  .HasForeignKey(sc => sc.CourseId);
    }
}