using AISPHRD.Models;
using System.Collections.Generic;

namespace AISPHRD.Repositories
{
    public interface IConscriptRepository
    {
        List<Conscript> GetAll();

        Conscript GetConscript(int conscriptId);

        void Insert(Conscript conscript);

        void Update(Conscript conscript);

        void Delete(int conscriptId);

        void Delete(Conscript conscript);
    }
}
