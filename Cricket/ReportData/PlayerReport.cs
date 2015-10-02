using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Cricket.ReportData
{
   public  class PlayerReport
    {
       public string ClubId { get; set; }
       public string PlayerName { get; set; }
       public string PhoneNumber { get; set; }
       public string EmailId { get; set; }
       public string Address { get; set; }
       public DateTime DOB { get; set; }
       public string Gender { get; set; }
       public string KSCAUID { get; set; }
       public byte[] PlayerPhoto { get; set; }

      

    }
}
