using AISPHRD.Models;
using System.Collections.Generic;

namespace AISPHRD.Repositories
{
    public interface IWorkerRepository
    {
        List<Worker> GetAll();

        Worker GetWorker(int workerId);

        void Insert(Worker worker);

        void Update(Worker worker);

        void Delete(int workerId);

        void Delete(Worker worker);
    }
}
