using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cricket.ReportData
{
   public class InduvidualExport
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string Team1{ get; set; }
        public string Team2 { get; set; }
        public string Venue { get; set; }
        public string Umpire1 { get; set; }
        public string Umpire2 { get; set; }
        public string Scorer { get; set; }
        public string Division { get; set; }
        public string MatchId { get; set; }
        public DateTime Date { get; set; }
        public string Season { get; set; }
       
    }
}
