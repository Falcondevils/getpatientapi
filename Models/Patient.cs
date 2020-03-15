using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GetPatientAPI.Models
{
    public class Patient
    {
        public int IdPatient { get; set; }
        public string NameLast { get; set; }
        public string NameFirst { get; set; }
        public string NameMiddle { get; set; }
    }
}
