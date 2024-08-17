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
        // GET: Teacher/New
        public ActionResult New()
        {
          
            return View();
        }

        // GET: Teacher/Delete/{id}
        public ActionResult Delete(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher teacher = controller.FindTeacher(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        // POST: Teacher/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            controller.DeleteTeacher(id);
            return RedirectToAction("List");
        }
       
        namespace SchoolDatabase.Controllers
        {
        public class TeacherController : Controller
        {
            private readonly SchoolDbContext _context;

            public TeacherController(SchoolDbContext context)
            {
                _context = context;
            }
            // GET: Teacher/Update/{id}
            public ActionResult Update(int id)
            {
                Teacher teacher = _context.Teacher.Find(id);
                if (teacher == null)
                {
                    return HttpNotFound();
                }
                return View(teacher);
            }

            // POST: Teacher/Update/{id}
            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Update(Teacher teacher)
            {
                if (ModelState.IsValid)
                {
                    _context.Entry(teacher).State = EntityState.Modified;
                    _context.SaveChanges();
                    return RedirectToAction("List");
                }
                return View(teacher);
            }
        }

        internal class EntityState
        {
            internal static object Modified;
        }