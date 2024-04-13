using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NPOI.SS.UserModel;
using WebApp.Models;

namespace WebApp.Controllers;
public class StudentController : BaseController{
    public IActionResult Index(){
        //return View(Provider.Student.GetStudents());
        ViewBag.Average = Provider.Student.Average();
        return View(Provider.Student.GetStudentViews());

    }
    public IActionResult Add(){
        ViewBag.Classes = new SelectList(Provider.Class.GetClasses(), "ClassId", "ClassName");
        return View();
    }
    [HttpPost]
    public IActionResult Add(StudentView obj){
        int ret = Provider.Student.Add(obj);
        if(ret > 0){
            return Redirect("/student");
        }
        ViewBag.Classes = new SelectList(Provider.Class.GetClasses(), "ClassId", "ClassName", obj.ClassId);
        return View(obj);
    }
    public IActionResult Import(){
        ViewBag.Classes = new SelectList(Provider.Class.GetClasses(), "ClassId", "ClassName");
        return View();
    }
    [HttpPost]
    public IActionResult Import(IFormFile f, string classId){
        if(f != null){
            using IWorkbook workbok = WorkbookFactory.Create(f.OpenReadStream());
            ISheet sheet = workbok.GetSheetAt(0);
            List<StudentView> list = new List<StudentView>();
            for (int i = 1; i <= sheet.LastRowNum; i++){
                IRow row = sheet.GetRow(i);
                list.Add(new StudentView{
                    StudentId = row.GetCell(0).StringCellValue,
                    ClassId = classId,
                    FirstName = row.GetCell(2).StringCellValue,
                    LastName = row.GetCell(1).StringCellValue,
                    Email = row.GetCell(3).StringCellValue,
                    Gender = row.GetCell(4).BooleanCellValue ? Gender.Female : Gender.Male,
                    Score = Convert.ToDecimal(row.GetCell(5).NumericCellValue)
                });
            }
            int ret = Provider.Student.Add(list);
            if(ret > 0){
                return Redirect($"/student/class/{classId}");
            }
            ModelState.AddModelError("Error", "Import Failed");
        }
        return View();
    }
    public IActionResult Details(string id){
        // return View(Provider.Student.GetStudent(id));
        return View(Provider.Student.GetStudentView(id));
    }
    public IActionResult Edit(string id){
        ViewBag.Classes = new SelectList(Provider.Class.GetClasses(), "ClassId", "ClassName");
        return Details(id);
    }
    [HttpPost]
    public IActionResult Edit(StudentView obj){
        int ret = Provider.Student.Edit(obj);
        if (ret > 0){
            return Redirect("/student");
        }
        ViewBag.Classes = new SelectList(Provider.Class.GetClasses(), "ClassId", "ClassName", obj.ClassId);
        return View(obj);
    }
}