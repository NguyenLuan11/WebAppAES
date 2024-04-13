using System.Data;
using Dapper;
using NPOI.Util;

namespace WebApp.Models;
public class StudentRepository : BaseRepository{
    public StudentRepository(IDbConnection connection) : base(connection){ }

    public decimal Average(){
        var list = GetStudentViews();
        decimal sum = 0;
        foreach(var studentView in list){
            sum += studentView.Score;
        }
        return sum / list.Count();
    }
    
    public IEnumerable<Student> GetStudents(){
        return connection.Query<Student>("SELECT Student.*, ClassName FROM Student JOIN Class ON Student.ClassId = Class.ClassId");
    }
    public IEnumerable<ClassView> GetClassViews(){
        return GetStudents().GroupBy(p => new Class{
            ClassId = p.ClassId,
            ClassName = p.ClassName
        }).Select(p => new ClassView{
            ClassId = p.Key.ClassId,
            ClassName = p.Key.ClassName,
            Average = p.Average(o => Convert.ToDecimal(Helper.Descrypt(o.StudentId, o.ClassId, o.Score)))
        });
    }
    
    public IEnumerable<ClassView> GetClassViewWithStudents(){
        return GetStudents().GroupBy(p => new Class{
            ClassId = p.ClassId,
            ClassName = p.ClassName
        }).Select(p => new ClassView{
            ClassId = p.Key.ClassId,
            ClassName = p.Key.ClassName,
            StudentViews = p.Select(o => new StudentView{
              StudentId = o.StudentId,
              FirstName = o.FirstName,
              LastName = o.LastName,
              Gender = o.Gender,
              Email = o.Email,
              Score = Convert.ToDecimal(Helper.Descrypt(o.StudentId, o.ClassId, o.Score))
            }),
            Average = p.Average(o => Convert.ToDecimal(Helper.Descrypt(o.StudentId, o.ClassId, o.Score)))
        });
    }
    
    public IEnumerable<StudentView> GetStudentViews(){
        return GetStudents().Select(p => new StudentView{
            StudentId = p.StudentId,
            ClassId = p.ClassId,
            ClassName = p.ClassName,
            FirstName = p.FirstName,
            LastName = p.LastName,
            Email = p.Email,
            Gender = p.Gender,
            Score = Convert.ToDecimal(Helper.Descrypt(p.StudentId, p.ClassId, p.Score))
        });
    }
    public IEnumerable<Student> Search(string q){
        return connection.Query<Student>("SELECT Student.*, ClassName FROM Student JOIN Class ON Class.ClassId = Student.ClassId WHERE FirstName LIKE @Q", new {Q = '%' + q + '%'});
    }
    public IEnumerable<StudentView> SearchStudentViews(string id){
        return Search(id).Select(p => new StudentView{
            StudentId = p.StudentId,
            FirstName = p.FirstName,
            LastName = p.LastName,
            ClassId = p.ClassId,
            Email = p.Email,
            Gender = p.Gender,
            Score = Convert.ToDecimal(Helper.Descrypt(p.StudentId, p.ClassId, p.Score))
        });
    }
    
    public int Add(Student obj){
        string sql = "INSERT INTO Student(StudentId, ClassId, FirstName, LastName, Email, Gender, Score) VALUES (@StudentId, @ClassId, @FirstName, @LastName, @Email, @Gender, @Score)";
        return connection.Execute(sql, obj);
    }
    public int Add(StudentView obj){
        return Add(new Student{
            StudentId = obj.StudentId,
            FirstName = obj.FirstName,
            LastName = obj.LastName,
            Gender = obj.Gender,
            Email = obj.Email,
            ClassId = obj.ClassId,
            Score = Helper.Encrypt(obj.StudentId, obj.ClassId, obj.Score.ToString())
        });
    }
    public int Add(IEnumerable<Student> list){
        string sql = "INSERT INTO Student(StudentId, ClassId, FirstName, LastName, Email, Gender, Score) VALUES (@StudentId, @ClassId, @FirstName, @LastName, @Email, @Gender, @Score)";
        return connection.Execute(sql, list);
    }
    public int Add(IEnumerable<StudentView> list){
        //Encrypt
        return Add(list.Select(p => new Student{
            StudentId = p.StudentId,
            FirstName = p.FirstName,
            LastName = p.LastName,
            Gender = p.Gender,
            Email = p.Email,
            ClassId = p.ClassId,
            Score = Helper.Encrypt(p.StudentId, p.ClassId, p.Score.ToString())
        }));
    }
    public Student GetStudent(string id){
        return connection.QuerySingle<Student>("SELECT Student.*, ClassName FROM Student JOIN Class ON Student.ClassId = Class.ClassId WHERE StudentId = @Id", new {Id = id});
    }
    public StudentView GetStudentView(string id){
        Student student = GetStudent(id);
        return new StudentView {
            StudentId = student.StudentId,
            FirstName = student.FirstName,
            LastName = student.LastName,
            ClassId = student.ClassId,
            ClassName = student.ClassName,
            Email = student.Email,
            Gender = student.Gender,
            Score = Convert.ToDecimal(Helper.Descrypt(student.StudentId, student.ClassId, student.Score))
        };
    }
    public IEnumerable<Student> GetStudentsByClass(string id){
       return connection.Query<Student>("SELECT * FROM Student WHERE ClassId = @Id", new {Id = id});
    }
    public IEnumerable<StudentView> GetStudentViewsByClass(string id){
        return GetStudentsByClass(id).Select(o => new StudentView{
              StudentId = o.StudentId,
              FirstName = o.FirstName,
              LastName = o.LastName,
              Gender = o.Gender,
              Email = o.Email,
              ClassId = o.ClassId,
              ClassName = o.ClassName,
              Score = Convert.ToDecimal(Helper.Descrypt(o.StudentId, o.ClassId, o.Score))
            });
    }
    
    public int Edit(Student obj){
        string sql = "UPDATE Student SET ClassId = @ClassId, FirstName = @FirstName, LastName = @LastName, Email = @Email, Gender = @Gender, Score = @Score WHERE StudentId = @StudentId";
        return connection.Execute(sql, obj);
    }
    public int Edit(StudentView obj){
        return Edit(new Student{
            StudentId = obj.StudentId,
            FirstName = obj.FirstName,
            LastName = obj.LastName,
            Gender = obj.Gender,
            Email = obj.Email,
            ClassId = obj.ClassId,
            Score = Helper.Encrypt(obj.StudentId, obj.ClassId, obj.Score.ToString())
        });
    }
}