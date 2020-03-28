using System;

namespace AISPHRD.Models
{
    public class Student
    {
        public int StudentId { get; set; }

        public string FullName { get; set; }

        public string Speciality { get; set; }

        public string Faculty { get; set; }

        public string Sex { get; set; }

        public int Year { get; set; }

        public string Address { get; set; }

        public DateTime BirthDate { get; set; }

        public Conscript Conscript { get; set; }
    }
}