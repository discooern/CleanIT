using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
 
    //Initialization of CleaningAppointment object
    public class CleaningAppointment
    {
        public int CleaningAppointmentId { get; set; }
        public int CustomerId { get; set; }
        public int CompanyId { get; set; }
        public bool Company { get; set; }
        public int HourPrice { get; set; }
        public string AppointmentDesc { get; set; }
    }
}
