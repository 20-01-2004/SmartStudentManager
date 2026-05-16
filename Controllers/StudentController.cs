using Microsoft.AspNetCore.Mvc;
using CognizantSimpleApp.Models;
using System.Linq;

namespace CognizantSimpleApp.Controllers
{
    public class StudentController : Controller
    {
        private static List<Student> students = new List<Student>();

        // READ
        public IActionResult Index()
        {
            return View(students);
        }

        // CREATE GET
       public IActionResult Create()
{
    ViewBag.Students = students;

    return View();
}
        // CREATE POST
        [HttpPost]
        public IActionResult Create(Student student)
        {
            // Check duplicate registration number
            if (students.Any(s => s.Id == student.Id))
            {
                ModelState.AddModelError("Id",
                    $"Student with ID '{student.Id}' already exists!");

                return View(student);
            }

            students.Add(student);

            return RedirectToAction("Index");
        }

        // EDIT GET
        public IActionResult Edit(string id)
        {
            var student = students.FirstOrDefault(s => s.Id == id);

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // EDIT POST
        [HttpPost]
        public IActionResult Edit(Student updatedStudent)
        {
            var student = students.FirstOrDefault(s => s.Id == updatedStudent.Id);

            if (student != null)
            {
                student.Name = updatedStudent.Name;
                student.Department = updatedStudent.Department;
            }

            return RedirectToAction("Index");
        }

        // DELETE
        public IActionResult Delete(string id)
        {
            var student = students.FirstOrDefault(s => s.Id == id);

            if (student != null)
            {
                students.Remove(student);
            }

            return RedirectToAction("Index");
        }
    }
}