using System.Collections.Generic;
using System.Linq;
using AISPHRD.Models;
using AISPHRD.Data;
using AISPHRD.Repositories;

namespace AISPHRD
{
    public class ConscriptRepository : IConscriptRepository
    {
        private ApplicationDbContext _context;
        public ConscriptRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Conscript> GetAll()
        {
            return _context.Conscripts.ToList();
        }

        public List<Conscript> GetAllBySearchString(string searchString)
        {
            return _context.Conscripts.Where(c => c.Student.FullName.Contains(searchString)).ToList();
        }

        public Conscript GetConscript(int conscriptId)
        {
            return _context.Conscripts.FirstOrDefault(c => c.ConscriptId == conscriptId);
        }

        public void Insert(Conscript conscript)
        {
            _context.Conscripts.Add(conscript);
            _context.SaveChanges();
        }

        public void Update(Conscript conscript)
        {
            _context.Conscripts.Update(conscript);
            _context.SaveChanges();
        }

        public void Delete(int conscriptId)
        {
            var conscript = GetConscript(conscriptId);
            _context.Conscripts.Remove(conscript);
            _context.SaveChanges();
        }

        public void Delete(Conscript conscript)
        {
            _context.Conscripts.Remove(conscript);
            _context.SaveChanges();
        }
    }
}