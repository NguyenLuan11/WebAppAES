using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Models;

namespace WebApp.Controllers;
public class HomeController : BaseController{
    public IActionResult Index(){
        //return View(Provider.Class.GetClasses());
        ViewBag.Average = Provider.Student.Average();
        return View(Provider.Student.GetClassViews());
    }
    public IActionResult Classes(){
        return View(Provider.Student.GetClassViewWithStudents());
    }
    public IActionResult Class(string id){
        // Class? @class = Provider.Class.GetClass(id);
        //You code here
        // return View(@class);
        var students = Provider.Student.GetStudentViewsByClass(id);
        return View(students);
    }

    public IActionResult Search(string q){
        if(string.IsNullOrEmpty(q)){
           return Redirect("/"); 
        }
        // return View(Provider.Student.Search(q));
        return View(Provider.Student.SearchStudentViews(q));
    }
}