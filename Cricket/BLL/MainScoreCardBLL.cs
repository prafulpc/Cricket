using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CricketSol;
using CricketSol.Base;
using CricketSol.DAL;
using CricketSol.Database;
using CricketSol.System;
using System.Data.OleDb;
using System.Configuration;
using System.Data;
using System.Collections.ObjectModel;
using Cricket.View;

namespace Cricket.BLL
{
   public class MainScoreCardBLL
    {

       private BattingDetails _battingdetails;

       public MainScoreCardBLL()
        {
            _battingdetails = new BattingDetails(); 
        }

        string captain1 = captainteam1.captain1[captainteam1.captaincount1];
        string captain2 = captainteam2.captain2[captainteam2.captaincount2];
        string keeper1 = keeperteam1.keeper1[keeperteam1.keepercount1];
        string keeper2 = keeperteam2.keeper2[keeperteam2.keepercount2];

        public DataTable batting(ObservableCollection<string> team1)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("SLNO", Type.GetType("System.String"));
            dt.Columns.Add("Batsman", Type.GetType("System.String"));
            dt.Columns.Add("Dismissal", Type.GetType("System.String"));
            dt.Columns.Add("Bowler", Type.GetType("System.String"));
            dt.Columns.Add("Runs", Type.GetType("System.String"));
            dt.Columns.Add("Balls", Type.GetType("System.String"));
            dt.Columns.Add("Mins", Type.GetType("System.String"));
            dt.Columns.Add("Fours", Type.GetType("System.String"));
            dt.Columns.Add("Sixes", Type.GetType("System.String"));
            dt.Columns.Add("SR", Type.GetType("System.String"));


            int count = 1;
            for (int i = 0; i <= (team1.Count - 1); i++)
            {
                //if (captain1 == keeper1)
                {
                   // if (team1[i] == captain1)
                    {
                        DataRow dr = dt.NewRow();
                        dr.BeginEdit();
                        dr["SLNO"] = count;
                        count++;
                        dr["Batsman"] = team1[i];
                        //dr["KSCA UID"] = KSCA.team1[i];
                        dr.EndEdit();
                        dt.Rows.Add(dr);
                        dt.AcceptChanges();

                    }
                    //else
                    //{
                    //    DataRow dr = dt.NewRow();
                    //    dr.BeginEdit();
                    //    dr["SLNO"] = count;
                    //    count++;
                    //    dr["Batsman"] = team1[i];
                    //   // dr["KSCA UID"] = KSCA.team1[i];

                    //    dr.EndEdit();
                    //    dt.Rows.Add(dr);
                    //    dt.AcceptChanges();
                    //}

                }
                //else
                //{
                //    if (team1[i] == captain1)
                //    {
                //        DataRow dr = dt.NewRow();
                //        dr.BeginEdit();
                //        dr["SLNO"] = count;
                //        count++;
                //        dr["Batsman"] = team1[i];
                //       // dr["KSCA UID"] = KSCA.team1[i];
                //        dr.EndEdit();
                //        dt.Rows.Add(dr);
                //        dt.AcceptChanges();
                //    }
                //    else if (team1[i] == keeper1)
                //    {
                //        DataRow dr = dt.NewRow();
                //        dr.BeginEdit();
                //        dr["SLNO"] = count;
                //        count++;
                //        dr["Batsman"] = team1[i];
                //        //dr["KSCA UID"] = KSCA.team1[i];
                //        dr.EndEdit();
                //        dt.Rows.Add(dr);
                //        dt.AcceptChanges();
                //    }
                //    else
                //    {

                //        DataRow dr = dt.NewRow();
                //        dr.BeginEdit();
                //        dr["SLNO"] = count;
                //        count++;
                //        dr["Batsman"] = team1[i];
                //       // dr["KSCA UID"] = KSCA.team1[i];
                //        dr.EndEdit();
                //        dt.Rows.Add(dr);
                //        dt.AcceptChanges();
                //    }
                //}
            }

            return (dt);

        }

        public DataTable bowling(ObservableCollection<string> team2)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("SLNO", Type.GetType("System.String"));
            dt.Columns.Add("Bowler", Type.GetType("System.String"));
            dt.Columns.Add("Overs", Type.GetType("System.String"));
            dt.Columns.Add("Dots", Type.GetType("System.String"));
            dt.Columns.Add("Maidens", Type.GetType("System.String"));
            dt.Columns.Add("Runs", Type.GetType("System.String"));
            dt.Columns.Add("Wickets", Type.GetType("System.String"));
            dt.Columns.Add("Wides", Type.GetType("System.String"));
            dt.Columns.Add("NoBalls", Type.GetType("System.String"));
            dt.Columns.Add("Econ", Type.GetType("System.String"));

            int count = 1;
            for (int i = 0; i <= team2.Count - 1; i++)
            {

                if (team2[i] == captain2)
                {
                    DataRow dr = dt.NewRow();
                    dr.BeginEdit();
                    dr["SLNO"] = count;
                    count++;
                    dr["Bowler"] = team2[i];
                   // dr["KSCA UID"] = KSCA.team2[i];

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
                    dr["Bowler"] = team2[i];
                    //dr["KSCA UID"] = KSCA.team2[i];
                    dr.EndEdit();
                    dt.Rows.Add(dr);
                    dt.AcceptChanges();
                }


            }
            return (dt);
        }
    }


    
        
}
