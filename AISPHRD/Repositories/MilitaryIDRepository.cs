﻿using AISPHRD.Data;
using AISPHRD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AISPHRD.Repositories
{
    public class MilitaryIDRepository : IMilitaryIDRepository
    {
        private ApplicationDbContext _context;
        public MilitaryIDRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<MilitaryID> GetAll()
        {
            return _context.MilitaryIDs.ToList();
        }

        public MilitaryID GetMilitaryID(int militaryIDId)
        {
            return _context.MilitaryIDs.FirstOrDefault(m => m.MilitaryIDId == militaryIDId);
        }

        public void Insert(MilitaryID militaryID)
        {
            _context.MilitaryIDs.Add(militaryID);
            _context.SaveChanges();
        }

        public void Update(MilitaryID militaryID)
        {
            _context.MilitaryIDs.Update(militaryID);
            _context.SaveChanges();
        }

        public void Delete(int militaryIDId)
        {
            var militaryID = GetMilitaryID(militaryIDId);
            _context.MilitaryIDs.Remove(militaryID);
            _context.SaveChanges();
        }

        public void Delete(MilitaryID militaryID)
        {
            _context.MilitaryIDs.Remove(militaryID);
            _context.SaveChanges();
        }
    }
}