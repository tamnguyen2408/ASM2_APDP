using Microsoft.AspNetCore.Mvc;
using SIMS_IT0602.Models;
using System.Text.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
namespace SIMS_IT0602.Controllers
{
    public class StudentController : Controller
    {
        public class EmailValidationAttribute : ValidationAttribute
        {
            private const string Pattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";

            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                if (value == null || !Regex.IsMatch(value.ToString(), Pattern))
                {
                    return new ValidationResult("Invalid email format.");
                }

                return ValidationResult.Success;
            }
        }
        static List<Student> students = new List<Student>();
        public IActionResult Delete(int Id)
        {
            var students = LoadStudentFromFile("student.json");

            //find teacher in an array
            var searchStudent = students.FirstOrDefault(t => t.Id == Id);
            students.Remove(searchStudent);

            //save to file
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(students, options);
            //Save file
            using (StreamWriter writer = new StreamWriter("student.json"))
            {
                writer.Write(jsonString);
            }
            return RedirectToAction("ManageStudent");

        }
        public List<Student>? LoadStudentFromFile(string fileName)
        {
            string readText = System.IO.File.ReadAllText("student.json");
            return JsonSerializer.Deserialize<List<Student>>(readText);
        }
        public IActionResult ManageStudent()
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            ViewBag.Role = HttpContext.Session.GetString("Role");
            // Read a file
            students = LoadStudentFromFile("student.json");
            return View(students);
            // Trả về view Managestudent.cshtml
            // return View("Managestudent");//
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Student student)
        {
            if (ModelState.IsValid)
            {
                var existingStudent = students.FirstOrDefault(t => t.Id == student.Id);
                if (existingStudent == null)
                {
                    return NotFound(); // Trả về lỗi 404 nếu không tìm thấy sinh viên
                }

                // Kiểm tra xem email có đúng định dạng hay không
                if (!IsValidEmail(student.Email))
                {
                    ModelState.AddModelError("Email", "Email không hợp lệ.");
                    return View(student);
                }

                existingStudent.Name = student.Name;
                existingStudent.DoB = student.DoB;
                existingStudent.Email = student.Email;
                existingStudent.Address = student.Address;
                existingStudent.Major = student.Major;

                var options = new JsonSerializerOptions { WriteIndented = true };
                string jsonString = JsonSerializer.Serialize(students, options);
                // Lưu thông tin mới vào file
                System.IO.File.WriteAllText("student.json", jsonString);
                // Chuyển hướng về trang quản lý sinh viên
                return RedirectToAction("ManageStudent", new { students = jsonString });
            }
            else
            {
                // Nếu trường bắt buộc bị bỏ trống hoặc email không hợp lệ, trả về view với thông báo lỗi
                return View(student);
            }
        }

        // Hàm kiểm tra email có đúng định dạng hay không
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        [HttpGet] //click hyperlink
        public IActionResult EditStudent(int id)
        {
            var student = students.FirstOrDefault(s => s.Id == id);
            if (student == null)
            {
                return NotFound(); // Trả về lỗi 404 nếu không tìm thấy sinh viên
            }

            return View(student);

        }
        [HttpPost]
        public IActionResult Save(Student student)
        {
            if (ModelState.IsValid)
            {
                var existingStudent = students.FirstOrDefault(t => t.Id == student.Id);
                if (existingStudent == null)
                {
                    return NotFound(); // Trả về lỗi 404 nếu không tìm thấy sinh viên
                }

                // Kiểm tra xem email có đúng định dạng hay không
                if (!IsValidEmail(student.Email))
                {
                    ModelState.AddModelError("Email", "Email không hợp lệ.");
                    return View("EditStudent", student); // Trả về view chỉnh sửa với thông báo lỗi
                }

                // Cập nhật thông tin sinh viên
                existingStudent.Name = student.Name;
                existingStudent.DoB = student.DoB;
                existingStudent.Email = student.Email;
                existingStudent.Address = student.Address;
                existingStudent.Major = student.Major;

                var options = new JsonSerializerOptions { WriteIndented = true };
                string jsonString = JsonSerializer.Serialize(students, options);

                // Lưu thông tin mới vào file
                System.IO.File.WriteAllText("student.json", jsonString);

                // Chuyển hướng về trang quản lý sinh viên
                return RedirectToAction("ManageStudent");
            }
            else
            {
                // Nếu trường bắt buộc bị bỏ trống hoặc email không hợp lệ, trả về view chỉnh sửa với thông báo lỗi
                return View("EditStudent", student);
            }
        }
        [HttpPost] //submit new Student
        [ValidateAntiForgeryToken] // Protect against Cross-Site Request Forgery (CSRF) attacks
        public IActionResult NewStudent(Student student)
        {
            if (ModelState.IsValid)
            {
                // Add the student to the collection
                students.Add(student);

                // Serialize the students collection to JSON
                var options = new JsonSerializerOptions { WriteIndented = true };
                string jsonString = JsonSerializer.Serialize(students, options);

                // Save the JSON data to the file
                System.IO.File.WriteAllText("student.json", jsonString);

                // Redirect to the action to manage students
                return RedirectToAction("ManageStudent", new { students = jsonString });
            }
            else
            {
                // If model state is not valid, return to the view with validation errors
                return View(student);
            }
        }

        [HttpGet] // Display the form to save students
        public IActionResult Save()
        {
            return View();
        }

       
        [HttpPost]
        public ActionResult Cancel(string returnUrl)
        {
            // Redirect to the ManageCourse action method
            return RedirectToAction("ManageStudent");
        }
        [HttpGet] //click hyperlink
        public IActionResult NewStudent()
        {
            return View();
        }

        public IActionResult ViewClass()
        {
            // Xử lý logic để hiển thị thông tin lớp học
            return View();
        }
        public IActionResult Index()
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            ViewBag.Role = HttpContext.Session.GetString("Role");
            // Read a file
            students = LoadStudentFromFile("student.json");
            return View(students);
        }



    }
}
