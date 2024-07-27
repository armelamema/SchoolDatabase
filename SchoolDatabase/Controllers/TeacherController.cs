using System.Collections.Generic;
using System.Web.Configuration;
using System.Web.Mvc;
using SchoolDatabase.Models;
using System.Web;
using System;
using System.Linq;

namespace SchoolDatabase.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher
        public ActionResult Index()
        {
            return View();
        }

        // GET: Teacher/List
        public ActionResult List()
        {
            TeacherDataController controller = new TeacherDataController();
            IEnumerable<Teacher> Teacher = controller.ListTeachers();
            return View(Teacher);
        }

        // GET: Teacher/Show/{id}
        public ActionResult Show(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.FindTeacher(id);
            return View(NewTeacher);
        }
    }
}
