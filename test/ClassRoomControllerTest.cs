using Xunit;
using ClassroomApp.Controllers;
using ClassroomApp.Services;
using ClassroomApp;

namespace ClassroomApp.Tests;

public class ClassRoomControllerTest
{
    [Fact]
    public void AddStudent()
    {
        var student = new Student() { Name = "STD1", Id = 1, Grade = "B"};
        var service = new ClassroomService();
        var obj = new ClassRoomController(service); 
        var result = obj.AddStudent(student).Value;
        Assert.True(result);
    }
}