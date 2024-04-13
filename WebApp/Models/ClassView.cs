namespace WebApp.Models;
public class ClassView : Class{
    public IEnumerable<StudentView> StudentViews { get; set; } = null!;
    public decimal Average { get; set; }
}