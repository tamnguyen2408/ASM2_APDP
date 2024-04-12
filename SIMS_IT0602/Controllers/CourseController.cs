using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SIMS_IT0602.Models;
using System.Text.Json;

namespace SIMS_IT0602.Controllers
{
    public class CourseController : Controller
    {

        static List<Course> courses = new List<Course>();
        public IActionResult Delete(int Id)
        {
            var courses = LoadCourseFromFile("course.json");

            //find teacher in an array
            var searchCourse = courses.FirstOrDefault(t => t.Id == Id);
            courses.Remove(searchCourse);

            //save to file
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(courses, options);
            //Save file
            using (StreamWriter writer = new StreamWriter("course.json"))
            {
                writer.Write(jsonString);
            }
            return RedirectToAction("ManageCourse");

        }
        [HttpPost]
        public IActionResult CreateCourse(Course course, List<Class> classes)
        {
            courses.Add(course);
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(courses, options);

            // Populate ViewBag.SelectClass with the list of classes
            ViewBag.SelectClass = new SelectList(classes, "ClassName", "ClassName");

            // Populate ViewBag.SelectClass with the list of classes
            ViewBag.SelectClass = classes;
            //Save file
            using (StreamWriter writer = new StreamWriter("course.json"))
            {
                writer.Write(jsonString);
            }

            return RedirectToAction("ManageCourse", new { courses = jsonString });
        }

        [HttpGet]
        public IActionResult CreateCourse()
        {
            // Load the list of teachers from the file
            List<Teacher> teachers = LoadTeacherFromFile("teacher.json");
            List<Class> classes = LoadClassFromFile("class.json");

            // Populate ViewBag.SelectTeacher with the list of teachers
            ViewBag.SelectTeacher = teachers;
            ViewBag.SelectClass = classes;
            return View();
        }

        public List<Teacher>? LoadTeacherFromFile(string fileName)
        {
            string readText = System.IO.File.ReadAllText(fileName);
            return JsonSerializer.Deserialize<List<Teacher>>(readText);
        }
        public List<Class>? LoadClassFromFile(string fileName)
        {
            string readText = System.IO.File.ReadAllText(fileName);
            return JsonSerializer.Deserialize<List<Class>>(readText);
        }

        [HttpPost]
        public IActionResult Save(Course course)
        {
            var existingCourse = courses.FirstOrDefault(t => t.Id == course.Id);
            if (existingCourse == null)
            {
                return NotFound();
            }

            // Cập nhật thông tin khoa hoc
            existingCourse.Name = course.Name;
            existingCourse.Class = course.Class;
            existingCourse.Major = course.Major;
            existingCourse.Lecturer = course.Lecturer;

            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(courses, options);

            // Lưu thông tin mới vào file
            System.IO.File.WriteAllText("course.json", jsonString);

            // Chuyển hướng về trang quản lý giáo viên
            return RedirectToAction("ManageCourse");
        }
        [HttpPost]
        public ActionResult Cancel(string returnUrl)
        {
            // Redirect to the ManageCourse action method
            return RedirectToAction("ManageCourse");
        }
        [HttpGet] //click hyperlink
        public IActionResult Save()
        {
            return View();
        }
        [HttpGet]
        public IActionResult EditCourse(int Id)
        {
            var course = courses.FirstOrDefault(s => s.Id == Id);
            if (course == null)
            {
                return NotFound(); // Return 404 error if course is not found
            }

            // Load the list of teachers from the file
            List<Teacher> teachers = LoadTeacherFromFile("teacher.json");
            List<Class> classes = LoadClassFromFile("class.json");

            // Populate ViewBag.SelectTeacher with the list of teachers
            ViewBag.SelectTeacher = teachers;
            ViewBag.SelectClass = classes;

            // Populate ViewBag.StatusOptions with the status options for the dropdown list
            ViewBag.StatusOptions = new SelectList(new[]
            {
        new SelectListItem { Text = "Active", Value = "Active" },
        new SelectListItem { Text = "Inactive", Value = "Inactive" },
        // Add more status options as needed
    }, "Value", "Text", course.Status); // Set the selected value to the current status of the course

            return View("EditCourse", course); // Pass the course object to the Edit view
        }

        [HttpPost]
        public IActionResult EditCourse(Course course, List<Class> classes)
        {
            var existingCourse = courses.FirstOrDefault(t => t.Id == course.Id);
            if (existingCourse == null)
            {
                return NotFound(); // Return 404 error if course is not found
            }

            // Update the existing course with the new values
            existingCourse.Name = course.Name;
            existingCourse.Class = course.Class;
            existingCourse.Major = course.Major;
            existingCourse.Lecturer = course.Lecturer;
            existingCourse.Status = course.Status; // Update the status property

            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(courses, options);

            // Populate ViewBag.SelectClass with the list of classes
            ViewBag.SelectClass = classes;

            // Save the updated list of courses to the file
            System.IO.File.WriteAllText("course.json", jsonString);

            // Redirect to the ManageCourse action method
            return RedirectToAction("ManageCourse");
        }

        public List<Course>? LoadCourseFromFile(string fileName)
        {
            string readText = System.IO.File.ReadAllText("course.json");
            return JsonSerializer.Deserialize<List<Course>>(readText);
        }
        public IActionResult ManageCourse()
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            ViewBag.Role = HttpContext.Session.GetString("Role");
            // Read a file
            courses = LoadCourseFromFile("course.json");
            return View(courses);
            // Trả về view Managestudent.cshtml
            // return View("Managestudent");//
        }
        [HttpGet]
        public IActionResult ViewCourse()
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            ViewBag.Role = HttpContext.Session.GetString("Role");
            // Read a file
            courses = LoadCourseFromFile("course.json");
            return View(courses);
        }

        // GET: /<controller>/

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
    }
}

