using System;

namespace AISPHRD.Models
{
    public class Worker
    {
        public int WorkerId { get; set; }

        public string FullName { get; set; }

        public string Department { get; set; }

        public string Sex { get; set; }

        public DateTime BirthDate { get; set; }

        public string WorkerType { get; set; }

        public MilitaryID MilitaryID { get; set; }

        public override string ToString()
        {
            return FullName;
        }
    }
}