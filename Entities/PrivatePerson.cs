using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{

    //Initialization of PrivatePerson object
    public class PrivatePerson
    {
        public int PrivatePersonId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Address { get; set; }
        public int Postalcode { get; set; }
        public int Phone { get; set; }
    }
}
