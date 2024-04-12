using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SIMS_IT0602.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SIMS_IT0602.Controllers
{
    public class TeacherController : Controller
    {
        static List<Teacher> teachers = new List<Teacher>();

        public IActionResult Delete(int Id)
        {
            var teachers = LoadTeacherFromFile("teacher.json");

            //find teacher in an array
            var searchTeacher = teachers.FirstOrDefault(t => t.Id == Id);
            teachers.Remove(searchTeacher);

            //save to file
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(teachers, options);
            //Save file
            using (StreamWriter writer = new StreamWriter("teacher.json"))
            {
                writer.Write(jsonString);
            }
            return RedirectToAction("ManageTeacher");

        }

        [HttpPost]
        public IActionResult Save(int Id, Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                var existingTeacher = teachers.FirstOrDefault(t => t.Id == teacher.Id);
                if (existingTeacher == null)
                {
                    return NotFound(); // Return a 404 error if the teacher is not found
                }

                // Update teacher's information
                existingTeacher.Name = teacher.Name;
                existingTeacher.DoB = teacher.DoB;
                existingTeacher.Course = teacher.Course;

                // Serialize the updated teachers collection to JSON
                var options = new JsonSerializerOptions { WriteIndented = true };
                string jsonString = JsonSerializer.Serialize(teachers, options);

                // Save the updated data to the file
                System.IO.File.WriteAllText("teacher.json", jsonString);

                // Redirect to the page to manage teachers
                return RedirectToAction("ManageTeacher");
            }
            else
            {
                // If model state is not valid, reload the list of teachers and return to the view with validation errors
                List<Teacher> allTeachers = LoadTeachersFromFile("teacher.json");
                ViewBag.Teachers = allTeachers;
                return View("EditTeacher", teacher);
            }
        }

        public List<Teacher> LoadTeachersFromFile(string fileName)
        {
            string readText = System.IO.File.ReadAllText(fileName);
            return JsonSerializer.Deserialize<List<Teacher>>(readText);
        }

        [HttpPost]
        public IActionResult NewTeacher(Teacher teacher, List<Course> courses)
        {
            if (ModelState.IsValid)
            {
                teachers.Add(teacher);
                var options = new JsonSerializerOptions { WriteIndented = true };
                string jsonString = JsonSerializer.Serialize(teachers, options);
                ViewBag.SelectCourse = new SelectList(courses, "Name", "Name");
                ViewBag.SelectCourse = courses;
                // Save file
                System.IO.File.WriteAllText("teacher.json", jsonString);

                // Redirect to the action to manage teachers
                return RedirectToAction("ManageTeacher", new { teachers = jsonString });
            }
            else
            {
                // If model state is not valid, reload the list of courses and return to the view with validation errors
                List<Course> allCourses = LoadCourseFromFile("course.json");
                ViewBag.SelectCourse = new SelectList(allCourses, "Name", "Name");
                return View("NewTeacher", teacher);
            }
        }


        [HttpGet]
        public IActionResult NewTeacher()
        {
            List<Course> courses = LoadCourseFromFile("course.json");
            ViewBag.SelectCourse = courses;
            return View();
        }//click hyperlink
        public List<Course>? LoadCourseFromFile(string fileName)
        {
            string readText = System.IO.File.ReadAllText("course.json");
            return JsonSerializer.Deserialize<List<Course>>(readText);
        }
        public IActionResult Save(int Id)
        {
            return View();
        }
        [HttpPost]
        public IActionResult Edit(int Id, Teacher teacher, List<Course>courses)
        {
            var existingTeacher = teachers.FirstOrDefault(t => t.Id == teacher.Id);
            if (existingTeacher == null)
            {
                return NotFound(); // Trả về lỗi 404 nếu không tìm thấy giáo viên
            }
            teacher.Id = existingTeacher.Id;
            existingTeacher.Name = teacher.Name;
            existingTeacher.DoB = teacher.DoB;
            existingTeacher.Course = teacher.Course;

            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(teachers, options);
            ViewBag.SelectCourse = courses;

            // Lưu thông tin mới vào file
            System.IO.File.WriteAllText("teacher.json", jsonString);


            // Chuyển hướng về trang quản lý giáo viên
            return RedirectToAction("ManageTeacher");
        }
        [HttpGet] //click hyperlink
        public IActionResult EditTeacher(int id)
        { 
            var teacher = teachers.FirstOrDefault(s => s.Id == id);
            if (teacher == null)
            {
                return NotFound(); // Trả về lỗi 404 nếu không tìm thấy sinh viên
            }
            List<Course> courses = LoadCourseFromFile("course.json");
            ViewBag.SelectCourse = courses;
            return View(teacher);
        }
    
        public List<Teacher>? LoadTeacherFromFile(string fileName)
        {
            string readText = System.IO.File.ReadAllText("teacher.json");
            return JsonSerializer.Deserialize<List<Teacher>>(readText);
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            ViewBag.Role = HttpContext.Session.GetString("Role");
            // Read a file
            teachers = LoadTeacherFromFile("teacher.json");
            return View(teachers);
        }
        
        public IActionResult ManageTeacher()
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            ViewBag.Role = HttpContext.Session.GetString("Role");
          //  Read a file
           teachers = LoadTeacherFromFile("teacher.json");
            return View(teachers);
           // Trả về view Manageteacher.cshtml
         return View("Manageteacher");
        }
        public IActionResult ViewTeacher()
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            ViewBag.Role = HttpContext.Session.GetString("Role");
            //  Read a file
            teachers = LoadTeacherFromFile("teacher.json");
            return View(teachers);
        }
        public IActionResult ViewClass()
        {
            // Xử lý logic để hiển thị thông tin lớp học
            return View();
        }

        [HttpPost]
        public ActionResult Cancel(string returnUrl)
        {
            // Redirect to the ManageCourse action method
            return RedirectToAction("ManageTeacher");
        }

        public IActionResult ViewCourse()
        {
            // Xử lý logic để hiển thị thông tin khóa học
            return View();
        }
        
    }
}

