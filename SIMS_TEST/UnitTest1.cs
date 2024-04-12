//using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SIMS_IT0602.Controllers;
using SIMS_IT0602.Models;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Xunit;

namespace SIMS_TEST
{

    public class StudentControllerTests
    {
        [Fact]
        public void TestDeleteStudent()
        {
            // Arrange
            var controller = new StudentController();
            var idToDelete = 1;

            // Act
            var result = controller.Delete(idToDelete) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("ManageStudent", result.ActionName);
        }
        [Fact]
        public void TestLoadStudentFromFile()
        {
            // Arrange
            var controller = new StudentController();
            var fileName = "student.json";
            var studentsJson = "[{\"Id\":1,\"Name\":\"hoan\",\"DoB\":\"2004-09-25T00:00:00\",\"Email\":\"Hoandp@fpt.edu.vn\",\"Address\":\"Bac Giang\",\"Major\":\"IT\"},{\"Id\":3,\"Name\":\"Nguyen Hai Nam\",\"DoB\":\"2004-09-02T00:00:00\",\"Email\":\"namnh05@gmail.com\",\"Address\":\"Ha Noi\",\"Major\":\"Graphic Design\"}]";
            System.IO.File.WriteAllText(fileName, studentsJson);

            // Act
            var result = controller.LoadStudentFromFile(fileName);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("hoan", result[0].Name);
            Assert.Equal("Nguyen Hai Nam", result[1].Name);
            // Adjust the assertions according to the actual properties and values you want to test
        }

        public class AuthenticationControllerTests
        {
            [Fact]
            public void TestLogin_InvalidUser_ReturnsErrorView()
            {
                // Arrange
                var controller = new AuthenticationController();
                var user = new User { UserName = "invalid", Pass = "invalid" };

                // Act
                var result = controller.Login(user) as ViewResult;

                // Assert
                Assert.NotNull(result);
                Assert.Equal("Login", result.ViewName);
                Assert.Equal("Invalid user!", result.ViewData["error"]);
            }

        }
        public class ClassControllerTests
        {
            [Fact]
            public void TestDeleteClass()
            {
                // Arrange
                var controller = new ClassController();
                var idToDelete = 1;

                // Act
                var result = controller.Delete(idToDelete) as RedirectToActionResult;

                // Assert
                Assert.NotNull(result);
                Assert.Equal("ManageClass", result.ActionName);
            }

            [Fact]
            public void TestLoadClassFromFile()
            {
                // Arrange
                var controller = new ClassController();
                var fileName = "class.json";
                var classesJson = "[{\"Id\":1,\"ClassName\":\"IT0601\",\"Major\":\"IT\",\"Lecturer\":\"Nguyen Van Toan\"},{\"Id\":2,\"ClassName\":\"IT0601\",\"Major\":\"IT\",\"Lecturer\":\"Nguyen Thanh Trieu\"},{\"Id\":3,\"ClassName\":\"IT0602\",\"Major\":\"Graphic Design\",\"Lecturer\":\"Nguyen Thuy Trang\"}]";
                System.IO.File.WriteAllText(fileName, classesJson);

                // Act
                var result = controller.LoadClassFromFile(fileName);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(3, result.Count);
                Assert.Equal("IT0601", result[0].ClassName);
                Assert.Equal("IT0601", result[1].ClassName);
                Assert.Equal("IT0602", result[2].ClassName);
            }
        }
    }
}






    