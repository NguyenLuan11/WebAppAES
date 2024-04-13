namespace WebApp.Models;
public abstract class StudentBase{
    public string StudentId { get; set; } = null!;
    public string ClassId { get; set; } = null!;
    public string ClassName { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public Gender Gender { get; set; }
}