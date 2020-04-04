using AISPHRD.Models;
using System.Collections.Generic;

namespace AISPHRD.Repositories
{
    public interface IConscriptRepository
    {
        List<Conscript> GetAll();

        List<Conscript> GetAllBySearchString(string searchString);

        Conscript GetConscript(int conscriptId);

        List<string> GetUniqueCommissariats();

        void Insert(Conscript conscript);

        void Update(Conscript conscript);

        void Delete(int conscriptId);

        void Delete(Conscript conscript);
    }
}
