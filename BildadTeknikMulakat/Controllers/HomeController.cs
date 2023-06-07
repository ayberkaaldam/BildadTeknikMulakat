using BildadTeknikMulakat.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BildadTeknikMulakat.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private  AppDbContext _appDbContext;

        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _appDbContext = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            StudentModelView studentModelView = new ();
            List<StudentModel> students = _appDbContext.Students.ToList();
            studentModelView.List = students;
            return View(studentModelView);
        }

        public string AddStudent(string fullName ,string hobby, string section,string teacher ,string guide)
        {
            try
            {
                StudentModel studentModel = new StudentModel();
                studentModel.FullName = fullName;
                studentModel.Section = section;
                studentModel.SchoolTeacher = teacher;
                studentModel.GuidanceTeacher = guide;
                studentModel.Hobby = hobby;
                _appDbContext.Students.Add(studentModel);
                _appDbContext.SaveChanges();
                return "1";
            }
            catch(Exception ex)
            {
                return "0";
            }
            
        }

        public string DeleteStudent(int studentId)
        {
            try
            {
                var student = _appDbContext.Students.FirstOrDefault(x => x.Id == studentId);
                _appDbContext.Students.Remove(student);
                _appDbContext.SaveChanges();
                return "1";
            }
            catch(Exception ex)
            {
                return "0";
            }
        }

        public JsonResult GetStudentById(int studentId)
        {

            StudentModel student =(StudentModel)_appDbContext.Students.FirstOrDefault(x=>x.Id ==studentId);
            return Json(student);
        }
        public IActionResult Privacy()
        {
            return View();
        }
    }
}