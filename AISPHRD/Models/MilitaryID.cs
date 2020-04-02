using System;
using System.Collections.Generic;
using System.Text;

namespace AISPHRD.Models
{
    public class MilitaryID
    {
        public int MilitaryIDId { get; set; }

        public int WorkerId { get; set; }

        public Worker Worker { get; set; }

        public byte[] MilitaryIDPhoto { get; set; }
    }
}
