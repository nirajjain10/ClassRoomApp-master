using System.Collections.Generic;

namespace ClassroomApp.Services
{
    public interface IClassroomService
    {
        bool AddStudent(Student student);

        IEnumerable<Student> GetAllStudents();
    }
}