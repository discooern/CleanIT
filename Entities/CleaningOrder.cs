using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class CleaningOrder
    {
        public int OrderId { get; set; }
        public DateTime CleaningDate { get; set; }
        public int Hours { get; set; }
        public bool Completion { get; set; }
        public bool Company { get; set; }
     }
}
