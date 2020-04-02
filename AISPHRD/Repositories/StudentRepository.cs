using System.Collections.Generic;
using System.Linq;
using AISPHRD.Models;
using AISPHRD.Data;
using AISPHRD.Repositories;

namespace AISPHRD
{
    public class StudentRepository : IStudentRepository
    {
        private ApplicationDbContext _context;
        public StudentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Student> GetAll()
        {
            return _context.Students.ToList();
        }

        public List<Student> GetAllBySearchString(string searchString)
        {
            return _context.Students.Where(s => s.FullName.Contains(searchString)
                                                || s.Speciality.Contains(searchString)
                                                || s.Faculty.Contains(searchString)
                                                || s.Sex.Contains(searchString)
                                                || s.Address.Contains(searchString)).ToList();
        }

        public Student GetStudent(int studentId)
        {
            return _context.Students.FirstOrDefault(s => s.StudentId == studentId);
        }

        public void Insert(Student student)
        {
            _context.Students.Add(student);
            _context.SaveChanges();
        }

        public void Update(Student student)
        {
            _context.Students.Update(student);
            _context.SaveChanges();
        }

        public void Delete(int studentId)
        {
            var student = GetStudent(studentId);
            _context.Students.Remove(student);
            _context.SaveChanges();
        }

        public void Delete(Student student)
        {
            _context.Students.Remove(student);
            _context.SaveChanges();
        }
    }
}