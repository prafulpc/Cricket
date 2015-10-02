using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cricket.View;
using System.Data;
using System.Windows;
using System.Collections.ObjectModel;

using CricketSol;
using CricketSol.Base;
using CricketSol.DAL;
using CricketSol.Database;
using CricketSol.System;
using System.Data.OleDb;
using System.Configuration;

 
namespace Cricket.BLL
{

    public class ScoreCardBLL
    {
        private BattingDetails _battingdetails;
       

        int totalruns1 = 0;
        int totalwides1 = 0;
        int totalnoball1 =0;
        int totalwickets1 = 0;
        int totalbowlerruns1 = 0;
        double totalovers1 = 0;
        
        int a1 = 0;
        int b1 = 0;
        int ol1 = 0;
        int or1 = 0;

        int totalruns2 = 0;
        int totalwides2 = 0;
        int totalnoball2 = 0;
        int totalwickets2 = 0;
        int totalbowlerruns2 = 0;
        double totalovers2 = 0;

        int a2 = 0;
        int b2 = 0;
        int ol2 = 0;
        int or2 = 0;


        public ScoreCardBLL()
        {
            _battingdetails = new BattingDetails(); 
        }

        string captain1 = captainteam1.captain1[captainteam1.captaincount1];
        string captain2 = captainteam2.captain2[captainteam2.captaincount2];
        string keeper1 = keeperteam1.keeper1[keeperteam1.keepercount1];
        string keeper2 = keeperteam2.keeper2[keeperteam2.keepercount2];

        public DataTable batting1(ObservableCollection<string> team1)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("SLNO", Type.GetType("System.String"));
            dt.Columns.Add("Name Of Players", Type.GetType("System.String"));
            dt.Columns.Add("KSCA UID", Type.GetType("System.String"));
            dt.Columns.Add("Dismissal", Type.GetType("System.String"));
            dt.Columns.Add("Runs", Type.GetType("System.String"));
            dt.Columns.Add("Mins", Type.GetType("System.String"));
            dt.Columns.Add("Balls", Type.GetType("System.String"));
            dt.Columns.Add("Fours", Type.GetType("System.String"));
            dt.Columns.Add("Sixes", Type.GetType("System.String"));
                      

            int count = 1;
            for (int i = 0; i <= (team1.Count - 1); i++)
            {
                if (captain1 == keeper1)
                {
                    if (team1[i] == captain1)
                    {
                        DataRow dr = dt.NewRow();
                        dr.BeginEdit();
                        dr["SLNO"] = count;
                        count++;
                        dr["Name Of Players"] = team1[i] + " (C)(W)";
                        dr["KSCA UID"] = KSCA.team1[i];
                        dr.EndEdit();
                        dt.Rows.Add(dr);
                        dt.AcceptChanges();
                        
                    }
                    else
                    {
                        DataRow dr = dt.NewRow();
                        dr.BeginEdit();
                        dr["SLNO"] = count;
                        count++;
                        dr["Name Of Players"] = team1[i];
                        dr["KSCA UID"] = KSCA.team1[i];

                        dr.EndEdit();
                        dt.Rows.Add(dr);
                        dt.AcceptChanges();
                    }

                }
                else
                {
                    if (team1[i] == captain1)
                    {
                        DataRow dr = dt.NewRow();
                        dr.BeginEdit();
                        dr["SLNO"] = count;
                        count++;
                        dr["Name Of Players"] = team1[i] + " (C)";
                        dr["KSCA UID"] = KSCA.team1[i];
                        dr.EndEdit();
                        dt.Rows.Add(dr);
                        dt.AcceptChanges();
                    }
                    else if (team1[i] == keeper1)
                    {
                        DataRow dr = dt.NewRow();
                        dr.BeginEdit();
                        dr["SLNO"] = count;
                        count++;
                        dr["Name Of Players"] = team1[i] + " (W)";
                        dr["KSCA UID"] = KSCA.team1[i];
                        dr.EndEdit();
                        dt.Rows.Add(dr);
                        dt.AcceptChanges();
                    }
                    else
                    {

                        DataRow dr = dt.NewRow();
                        dr.BeginEdit();
                        dr["SLNO"] = count;
                        count++;
                        dr["Name Of Players"] = team1[i];
                        dr["KSCA UID"] = KSCA.team1[i];
                        dr.EndEdit();
                        dt.Rows.Add(dr);
                        dt.AcceptChanges();
                    }
                }
            }
            
                return (dt);
        }

        public DataTable bowling1(ObservableCollection<string> team2)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("SLNO", Type.GetType("System.String"));
            dt.Columns.Add("Name Of Players", Type.GetType("System.String"));
            dt.Columns.Add("KSCA UID", Type.GetType("System.String"));
            dt.Columns.Add("Overs", Type.GetType("System.String"));
            dt.Columns.Add("Maidens", Type.GetType("System.String"));
            dt.Columns.Add("Runs", Type.GetType("System.String"));
            dt.Columns.Add("Wickets", Type.GetType("System.String"));
            dt.Columns.Add("No Ball", Type.GetType("System.String"));
            dt.Columns.Add("Wide", Type.GetType("System.String"));
            dt.Columns.Add("Avg", Type.GetType("System.String"));

            int count = 1;
            for (int i = 0; i <= team2.Count - 1; i++)
            {
                
                if (team2[i] == captain2)
                {
                    DataRow dr = dt.NewRow();
                    dr.BeginEdit();
                    dr["SLNO"] = count; 
                    count++;
                    dr["Name Of Players"] = team2[i] + " (C)";
                    dr["KSCA UID"] = KSCA.team2[i];

                    dr.EndEdit();
                    dt.Rows.Add(dr);
                    dt.AcceptChanges();
                }
                else
                {
                    DataRow dr = dt.NewRow();
                    dr.BeginEdit();
                    dr["SLNO"] = count;
                    count++;
                    dr["Name Of Players"] = team2[i];
                    dr["KSCA UID"] = KSCA.team2[i];
                    dr.EndEdit();
                    dt.Rows.Add(dr);
                    dt.AcceptChanges();
                }


            }
            return (dt);
        }

        public DataTable batting2(ObservableCollection<string> team3)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("SLNO", Type.GetType("System.String"));
            dt.Columns.Add("Name Of Players", Type.GetType("System.String"));
            dt.Columns.Add("KSCA UID", Type.GetType("System.String"));
            dt.Columns.Add("Dismissal", Type.GetType("System.String"));
            dt.Columns.Add("Runs", Type.GetType("System.String"));
            dt.Columns.Add("Mins", Type.GetType("System.String"));
            dt.Columns.Add("Balls", Type.GetType("System.String"));
            dt.Columns.Add("Fours", Type.GetType("System.String"));
            dt.Columns.Add("Sixes", Type.GetType("System.String"));

            int count = 1;
            for (int i = 0; i <= team3.Count - 1; i++)
            {

                if (captain2 == keeper2)
                {
                    if (team3[i] == captain2)
                    {
                        DataRow dr = dt.NewRow();
                        dr.BeginEdit();
                        dr["SLNO"] = count;
                        count++;
                        dr["Name Of Players"] = team3[i] + " (C)(W)";
                        dr["KSCA UID"] = KSCA.team2[i];
                        dr.EndEdit();
                        dt.Rows.Add(dr);
                        dt.AcceptChanges();
                        
                    }
                    else
                    {
                        DataRow dr = dt.NewRow();
                        dr.BeginEdit();
                        dr["SLNO"] = count;
                        count++;

                        dr["Name Of Players"] = team3[i];
                        dr["KSCA UID"] = KSCA.team2[i];
                        
                        dr.EndEdit();
                        dt.Rows.Add(dr);
                        dt.AcceptChanges();
                    }

                }
                else
                {
                    if (team3[i] == captain2)
                    {
                        DataRow dr = dt.NewRow();
                        dr.BeginEdit();
                        dr["SLNO"] = count;
                        count++;
                        dr["Name Of Players"] = team3[i] + " (C)";
                        dr["KSCA UID"] = KSCA.team2[i];
                        dr.EndEdit();
                        dt.Rows.Add(dr);
                        dt.AcceptChanges();
                    }
                    else if (team3[i] == keeper2)
                    {
                        DataRow dr = dt.NewRow();
                        dr.BeginEdit();
                        dr["SLNO"] = count;
                        count++;
                        dr["Name Of Players"] = team3[i] + " (W)";
                        dr["KSCA UID"] = KSCA.team2[i];
                        dr.EndEdit();
                        dt.Rows.Add(dr);
                        dt.AcceptChanges();
                    }
                    else
                    {

                        DataRow dr = dt.NewRow();
                        dr.BeginEdit();
                        dr["SLNO"] = count;
                        count++;
                        dr["Name Of Players"] = team3[i];
                        dr["KSCA UID"] = KSCA.team2[i];
                        dr.EndEdit();
                        dt.Rows.Add(dr);
                        dt.AcceptChanges();
                    }
                }
                
            }
            return (dt); 
        }

        public DataTable bowling2(ObservableCollection<string> team4)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("SLNO", Type.GetType("System.String"));
            dt.Columns.Add("Name Of Players", Type.GetType("System.String"));
            dt.Columns.Add("KSCA UID", Type.GetType("System.String"));
            dt.Columns.Add("Overs", Type.GetType("System.String"));
            dt.Columns.Add("Maidens", Type.GetType("System.String"));
            dt.Columns.Add("Runs", Type.GetType("System.String"));
            dt.Columns.Add("Wickets", Type.GetType("System.String"));
            dt.Columns.Add("No Ball", Type.GetType("System.String"));
            dt.Columns.Add("Wide", Type.GetType("System.String"));
            dt.Columns.Add("Avg", Type.GetType("System.String"));

            int count = 1;
            for (int i = 0; i <= team4.Count - 1; i++)
            {                
                if (team4[i] == captain1)
                {

                    DataRow dr = dt.NewRow();
                    dr.BeginEdit();
                    dr["SLNO"] = count;
                    count++;
                    dr["Name Of Players"] = team4[i] + " (C)";
                    dr["KSCA UID"] = KSCA.team1[i];
                    dr.EndEdit();
                    dt.Rows.Add(dr);
                    dt.AcceptChanges();
                }
                else
                {
                    
                    DataRow dr = dt.NewRow();
                    dr.BeginEdit();
                    dr["SLNO"] = count;
                    count++;
                    dr["Name Of Players"] = team4[i];
                    dr["KSCA UID"] = KSCA.team1[i];
                    dr.EndEdit();
                    dt.Rows.Add(dr);
                    dt.AcceptChanges();
                }
            }
            return (dt);
        }


        public int runs1(int run)
        {
            totalruns1 = totalruns1 + run;
            return totalruns1;
        }

        public int wides1(int wide)
        {
            totalwides1 = totalwides1 + wide;
            return totalwides1;
        }

        public int noball1 (int noball)
        {
            totalnoball1 =totalnoball1+noball;
            return totalnoball1;
        }

        public int wickets1(int wicket)
        {
            totalwickets1 = totalwickets1 + wicket;
            return totalwickets1;
        }
        
        public int bowlerruns1(int bruns)
        {
            totalbowlerruns1 = totalbowlerruns1 + bruns;
            return totalbowlerruns1;
        }

        public double overs1(double over1)
        {
             a1 =Convert.ToInt16(Math.Truncate(over1));
            string str1 = Convert.ToString(Math.Round(over1 - a1,1));
            if(str1 != "0")
            {
                b1 = Convert.ToInt16(str1.Split('.')[1].Trim());
            }
            else
            {
                b1 = 0;
            }

            if((or1+b1)>=6)
            {
                ol1++;
                ol1 = ol1 + a1;
                or1 = (or1 + b1) % 6;
            }
            else
            {
                ol1 = ol1 + a1;
                or1 = or1 + b1;
            }

             totalovers1 = Convert.ToDouble(Convert.ToString(ol1) + "." + Convert.ToString(or1));
            return totalovers1; 
        }


        ///////////

        public int runs2(int run)
        {
            totalruns2 = totalruns2 + run;
            return totalruns2;
        }

        public int wides2(int wide)
        {
            totalwides2 = totalwides2 + wide;
            return totalwides2;
        }

        public int noball2(int noball)
        {
            totalnoball2 = totalnoball2 + noball;
            return totalnoball2;
        }

        public int wickets2(int wicket)
        {
            totalwickets2 = totalwickets2 + wicket;
            return totalwickets2;
        }

        public int bowlerruns2(int bruns)
        {
            totalbowlerruns2 = totalbowlerruns2 + bruns;
            return totalbowlerruns2;
        }

        public double overs2(double over2)
        {
            a2 = Convert.ToInt16(Math.Truncate(over2));
            string str2 = Convert.ToString(Math.Round(over2 - a2,1));
            if (str2 != "0")
            {
                b2 = Convert.ToInt16(str2.Split('.')[1].Trim());
            }
            else
            {
                b2 = 0;
            }

            if ((or2 + b2) >= 6)
            {
                ol2++;
                ol2 = ol2 + a2;
                or2 = (or2 + b2) % 6;
            }
            else
            {
                ol2 = ol2 + a2;
                or2 = or2 + b2;
            }

            totalovers2 = Convert.ToDouble(Convert.ToString(ol2) + "." + Convert.ToString(or2));
            return totalovers2;
        }
    }
}
