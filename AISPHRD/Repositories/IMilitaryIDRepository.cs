using AISPHRD.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AISPHRD.Repositories
{
    public interface IMilitaryIDRepository
    {
        List<MilitaryID> GetAll();

        List<MilitaryID> GetAllBySearchString(string searchString);

        MilitaryID GetMilitaryID(int militaryIDId);

        void Insert(MilitaryID militaryID);

        void Update(MilitaryID militaryID);

        void Delete(int militaryIDId);

        void Delete(MilitaryID militaryID);
    }
}
