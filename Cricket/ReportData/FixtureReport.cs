using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cricket.ReportData
{
   public class FixtureReport
    {
       public int SlNo { get; set; }
       public string Group { get; set; }
       public string Versus { get; set; }
       public string Day { get; set; }
       public DateTime Date{get;set;}
       public string Venue { get; set; }
       public string Umpire1 { get; set; }
       public string Umpire2 { get; set; }
       public string Scorer { get; set; }

    }
}
