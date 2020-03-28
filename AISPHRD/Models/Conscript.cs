namespace AISPHRD.Models
{
    public class Conscript
    {
        public int ConscriptId { get; set; }

        public int StudentId { get; set; }

        public Student Student { get; set; }

        public string Commissariat { get; set; }
    }
}