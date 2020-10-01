using BusinessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public interface IStudentServices
    {
        IEnumerable<Student> GetAllStudent();
        IEnumerable<Student> GetStudentById(int id);
      void AddNewStudent(Student student);
        void UpdateStudent(int id, Student student);
        void DeleteStudent(int id);

    }
}
