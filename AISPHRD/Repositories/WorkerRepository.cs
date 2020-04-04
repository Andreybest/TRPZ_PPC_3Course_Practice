using System.Collections.Generic;
using System.Linq;
using AISPHRD.Models;
using AISPHRD.Data;
using AISPHRD.Repositories;

namespace AISPHRD
{
    public class WorkerRepository : IWorkerRepository
    {
        private ApplicationDbContext _context;
        public WorkerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Worker> GetAll()
        {
            return _context.Workers.ToList();
        }

        public List<Worker> GetAllBySearchString(string searchString)
        {
            return _context.Workers.Where(w => w.FullName.Contains(searchString)
                                                || w.Department.Contains(searchString)
                                                || w.Sex.Contains(searchString)
                                                || w.WorkerType.Contains(searchString)).ToList();
        }

        public Worker GetWorker(int workerId)
        {
            return _context.Workers.FirstOrDefault(w => w.WorkerId == workerId);
        }

        public List<string> GetUniqueDepartments()
        {
            return _context.Workers.Select(w => w.Department).Distinct().ToList();
        }

        public List<string> GetUniqueSexes()
        {
            return _context.Workers.Select(w => w.Sex).Distinct().ToList();
        }

        public List<string> GetUniqueWorkerTypes()
        {
            return _context.Workers.Select(w => w.WorkerType).Distinct().ToList();
        }

        public void Insert(Worker worker)
        {
            _context.Workers.Add(worker);
            _context.SaveChanges();
        }

        public void Update(Worker worker)
        {
            _context.Workers.Update(worker);
            _context.SaveChanges();
        }

        public void Delete(int workerId)
        {
            var worker = GetWorker(workerId);
            _context.Workers.Remove(worker);
            _context.SaveChanges();
        }

        public void Delete(Worker worker)
        {
            _context.Workers.Remove(worker);
            _context.SaveChanges();
        }
    }
}