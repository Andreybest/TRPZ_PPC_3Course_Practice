using AISPHRD.Models;
using System.Collections.Generic;

namespace AISPHRD.Repositories
{
    public interface IStudentRepository
    {
        List<Student> GetAll();

        List<Student> GetAllBySearchString(string searchString);

        Student GetStudent(int studentId);

        List<string> GetUniqueSpecialities();

        List<string> GetUniqueFaculties();

        List<string> GetUniqueSexes();

        void Insert(Student student);

        void Update(Student student);

        void Delete(int studentId);

        void Delete(Student student);
    }
}
