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
    public class FixtureGeneration
    {
       public List<string> list1 = new List<string>(15);
       public List<string> list2 = new List<string>(15);
       public List<string> list3 = new List<string>(15);
       public List<string> list4 = new List<string>(15);

       public List<string> listteamA = new List<string>(15);
       public List<string> listteamB = new List<string>(15);

       private Fixture _fixture;
       int count = 1;
       //int matchno0 = 0;
       //int matchno1 = 1;
       int sf1 = 1;


        public FixtureGeneration()
        {
            _fixture = new Fixture();
        }


        public DataTable fixture(ObservableCollection<string> octeam1, ObservableCollection<string> octeam2, ObservableCollection<string> octeam3, ObservableCollection<string> octeam4, string zonename, string seasonname, int NoOfGroups, string divletter )
        {
            int k = 0;

            ObservableCollection<string> collect_A = new ObservableCollection<string>();
            collect_A.Add(octeam1.ToString());

            ObservableCollection<string> collect_B = new ObservableCollection<string>();
            collect_B.Add(octeam2.ToString());

            DataTable dt = new DataTable();
           
            dt.Columns.Add("SerialNo", Type.GetType("System.String"));
            dt.Columns.Add("Day", Type.GetType("System.String"));
            dt.Columns.Add("MatchId", Type.GetType("System.String"));
            dt.Columns.Add("Group", Type.GetType("System.String"));
            dt.Columns.Add("Teams", Type.GetType("System.String"));
            dt.Columns.Add("Venue", Type.GetType("System.String"));
            dt.Columns.Add("Umpire", Type.GetType("System.String"));
            dt.Columns.Add("Umpiree", Type.GetType("System.String"));
            dt.Columns.Add("Scorer", Type.GetType("System.String"));

            // Group A Only
            if (NoOfGroups == 1)
            {
                int matchno0 = 0;
                int matchno1 = 1;
  
                int i;
                
                for (i = 0; i < octeam1.Count; i++)
                {
                    list1.Insert(i, octeam1[i]);
                }

                for (i = 1; i <= list1.Count - 1; i++)
                {
                    
                    rotationA();
                    for (int j = 0; j <= ((list1.Count - 1) / 2); j++)
                    {
                        
                        //fixturegenerator(j, i);
                        int n = list1.Count - 1;
                        if (!(list1[j].ToString() == "Dummy A" || list1[n - j].ToString() == "Dummy A"))
                        {
                            

                            DataRow dr = dt.NewRow();
                            dr.BeginEdit();
                            dr["Teams"] = list1[j].ToString() + " v/s " + list1[n - j].ToString();
                            dr["SerialNo"] = "Match No " + count.ToString();
                            dr["Day"] = "Day - " + i.ToString();
                            dr["MatchId"] = zonename + divletter + "M" + seasonname  + "A" + matchno0 + matchno1;
                            
                            if(matchno1!=9)
                            {
                                matchno1++;
                            }
                            else
                            {
                                matchno0++;
                                matchno1 = 0;
                            }
                            
                            dr["Group"] = "Group A";
                            count = count + 1;
                            dr.EndEdit();
                            dt.Rows.Add(dr); 
                            dt.AcceptChanges();

                            listteamA.Insert(k, list1[j].ToString());
                            listteamB.Insert(k, list1[n - j].ToString());
                            k++;
                                                            
                        }
                        
                    }
                }
                list1.Remove("Dummy A");

                int c = 1;
                int m;
                int a = 1;
                for (m = i + 1; m <= i + 2; m++)
                {
                    
                    DataRow dr1 = dt.NewRow();
                    dr1.BeginEdit();
                    dr1["Teams"] = "TBC" + c + " v/s " + "TBC" + (c+1) + "  (SemiFinal-" + a + ")";
                    dr1["SerialNo"] = "Match No " + count.ToString();
                    dr1["Day"] = "Day - " + (m-1).ToString();
                    dr1["MatchId"] = zonename + divletter + "M" + seasonname  + "SF" + sf1;
                    sf1++;
                    dr1["Group"] = "Semi-Final";
                    dr1.EndEdit();
                    dt.Rows.Add(dr1);
                    dt.AcceptChanges();
                    count++;
                    c = c + 2; 
                    a++;
                    //i++;
                }

                for(int n=m+1;n<=m+1;n++)
                {
                    DataRow dr2 = dt.NewRow();
                    dr2.BeginEdit();
                    dr2["Teams"] = "TBC" + c + " v/s " + "TBC" + (c + 1) + "  (Final)";
                    dr2["SerialNo"] = "Match No " + count.ToString();
                    dr2["Day"] = "Day - " + (m - 1).ToString();
                    dr2["MatchId"] = zonename + divletter + "M" + seasonname  + "FIN";
                    dr2["Group"] = "Final";
                    dr2.EndEdit();
                    dt.Rows.Add(dr2);
                    dt.AcceptChanges();
                }
            }

            // Group A & Group B Only
            else if (NoOfGroups == 2)
            {
                int matchno0 = 0;
                int matchno1 = 1;
                int i;

                for (i = 0; i < octeam1.Count; i++)
                {
                    list1.Insert(i, octeam1[i]);
                }

                for (int h = 0; h < octeam2.Count; h++)
                {
                    list2.Insert(h, octeam2[h]);
                }

                for (int p = 1; p <= list1.Count - 1; p++)
                {
                    rotationA();
                    

                    for (int j = 0; j <= ((list1.Count - 1) / 2); j++)
                    {
                        //fixturegenerator(j, i);
                        int n = list1.Count - 1;
                        if (!(list1[j].ToString() == "Dummy A" || list1[n - j].ToString() == "Dummy A"))
                        {
                            
                            DataRow dr = dt.NewRow();
                            dr.BeginEdit();
                            dr["Teams"] = list1[j].ToString() + " v/s " + list1[n - j].ToString();
                            dr["SerialNo"] = "Match No " + count.ToString();
                            dr["Day"] = "Day - " + i.ToString();

                            dr["MatchId"] = zonename + divletter + "M" + seasonname  + "A" + matchno0 + matchno1;

                            if (matchno1 != 9)
                            {
                                matchno1++;
                            }
                            else
                            {
                                matchno0++;
                                matchno1 = 0;
                            }

                            dr["Group"] = "Group A";
                            count = count + 1;
                            dr.EndEdit();
                            dt.Rows.Add(dr);
                            dt.AcceptChanges();

                            listteamA.Insert(k, list1[j].ToString());
                            listteamB.Insert(k, list1[n - j].ToString());
                            k++;
                        }
                    }
                    
                }

                matchno0 = 0;
                matchno1 = 1;
                for (int h = 1; h <= list2.Count - 1; h++)
                {
                    rotationB();
                    

                    for (int j = 0; j <= ((list2.Count - 1) / 2); j++)
                    {
                        int n = list2.Count - 1;
                        if (!(list2[j].ToString() == "Dummy B" || list2[n - j].ToString() == "Dummy B"))
                        {
                            
                            DataRow dr = dt.NewRow();
                            dr.BeginEdit();
                            dr["Teams"] = list2[j].ToString() + " v/s " + list2[n - j].ToString();
                            dr["SerialNo"] = "Match No " + count.ToString();
                            dr["Day"] = "Day - " + i.ToString();

                            dr["MatchId"] = zonename + divletter + "M" + seasonname +   "B" + matchno0 + matchno1;

                            if (matchno1 != 9)
                            {
                                matchno1++;
                            }
                            else
                            {
                                matchno0++;
                                matchno1 = 0;
                            }

                            dr["Group"] = "Group B";
                            count = count + 1;
                            dr.EndEdit();
                            dt.Rows.Add(dr);
                            dt.AcceptChanges();

                            listteamA.Insert(k, list2[j].ToString());
                            listteamB.Insert(k, list2[n - j].ToString());
                            k++;
                        }
                    }
                }

                int q = 1;
                int w;
                int b = 1;
                for (w = i + 1; w <= i + 2; w++)
                {
                    
                    DataRow dr1 = dt.NewRow();
                    dr1.BeginEdit();
                    dr1["Teams"] = "TBC" + q + " v/s " + "TBC" + (q + 1) + "  (SemiFinal-" + b + ")";
                    dr1["SerialNo"] = "Match No " + count.ToString();
                    dr1["Day"] = "Day - " + (w - 1).ToString();
                    dr1["MatchId"] = zonename + divletter + "M" + seasonname +   "SF" + sf1;
                    sf1++;
                    dr1["Group"] = "Semi-Final";
                    dr1.EndEdit();
                    dt.Rows.Add(dr1);
                    dt.AcceptChanges();
                    count++;
                    q = q + 2;
                    b++;

                }
                for (int n = w + 1; n <= w + 1; n++)
                {
                    DataRow dr2 = dt.NewRow();
                    dr2.BeginEdit();
                    dr2["Teams"] = "TBC" + q + " v/s " + "TBC" + (q + 1) + "  (Final)";
                    dr2["SerialNo"] = "Match No " + count.ToString();
                    dr2["Day"] = "Day - " + (w - 1).ToString();
                    dr2["MatchId"] = zonename + divletter + "M" + seasonname +   "FIN";
                    dr2["Group"] = "Final";
                    dr2.EndEdit();
                    dt.Rows.Add(dr2);
                    dt.AcceptChanges();
                }
            }

            // Groups A, B, C
            else if (NoOfGroups == 3)
            {
               int matchno0 = 0;
               int matchno1 = 1;

                int i;
                for (i = 0; i < octeam1.Count; i++)
                {
                    list1.Insert(i, octeam1[i]);
                }

                for (int h = 0; h < octeam2.Count; h++)
                {
                    list2.Insert(h, octeam2[h]);
                }
                for (int p = 0; p < octeam3.Count; p++)
                {
                    list3.Insert(p, octeam3[p]);
                }

                for (int q = 1; q <= list1.Count - 1; q++)
                {
                    rotationA();

                    for (int j = 0; j <= ((list1.Count - 1) / 2); j++)
                    {

                        int n = list1.Count - 1;
                        if (!(list1[j].ToString() == "Dummy A" || list1[n - j].ToString() == "Dummy A"))
                        {
                           
                            DataRow dr = dt.NewRow();
                            dr.BeginEdit();
                            dr["Teams"] = list1[j].ToString() + " v/s " + list1[n - j].ToString();
                            dr["SerialNo"] = "Match No " + count.ToString();
                            dr["Day"] = "Day - " + i.ToString();

                            dr["MatchId"] = zonename + divletter + "M" + seasonname +   "A" + matchno0 + matchno1;

                            if (matchno1 != 9)
                            {
                                matchno1++;
                            }
                            else
                            {
                                matchno0++;
                                matchno1 = 0;
                            }

                            dr["Group"] = "Group A";
                            count = count + 1;
                            dr.EndEdit();
                            dt.Rows.Add(dr);
                            dt.AcceptChanges();

                            listteamA.Insert(k, list1[j].ToString());
                            listteamB.Insert(k, list1[n - j].ToString());
                            k++;
                        }
                    }
                }


                matchno0 = 0;
                matchno1 = 1;
                for (int s = 1; s <= list2.Count - 1; s++)
                {
                    rotationB();

                    for (int j = 0; j <= ((list2.Count - 1) / 2); j++)
                    {

                        int n = list2.Count - 1;
                        if (!(list2[j].ToString() == "Dummy B" || list2[n - j].ToString() == "Dummy B"))
                        {
                            
                            DataRow dr = dt.NewRow();
                            dr.BeginEdit();
                            dr["Teams"] = list2[j].ToString() + " v/s " + list2[n - j].ToString();
                            dr["SerialNo"] = "Match No " + count.ToString();
                            dr["Day"] = "Day - " + i.ToString();

                            dr["MatchId"] = zonename + divletter + "M" + seasonname +   "B" + matchno0 + matchno1;

                            if (matchno1 != 9)
                            {
                                matchno1++;
                            }
                            else
                            {
                                matchno0++;
                                matchno1 = 0;
                            }

                            dr["Group"] = "Group B";
                            count = count + 1;
                            dr.EndEdit();
                            dt.Rows.Add(dr);
                            dt.AcceptChanges();

                            listteamA.Insert(k, list2[j].ToString());
                            listteamB.Insert(k, list2[n - j].ToString());
                            k++;
                        }
                    }
                }

                matchno0 = 0;
                matchno1 = 1;
                for (int q = 1; q <= list3.Count - 1; q++)
                {
                    rotationC();

                    for (int j = 0; j <= ((list3.Count - 1) / 2); j++)
                    {

                        int n = list3.Count - 1;
                        if (!(list3[j].ToString() == "Dummy C" || list3[n - j].ToString() == "Dummy C"))
                        {
                            
                            DataRow dr = dt.NewRow();
                            dr.BeginEdit();
                            dr["Teams"] = list3[j].ToString() + " v/s " + list3[n - j].ToString();
                            dr["SerialNo"] = "Match No " + count.ToString();
                            dr["Day"] = "Day - " + i.ToString();

                            dr["MatchId"] = zonename + divletter + "M" + seasonname +   "C" + matchno0 + matchno1;

                            if (matchno1 != 9)
                            {
                                matchno1++;
                            }
                            else
                            {
                                matchno0++;
                                matchno1 = 0;
                            }

                            dr["Group"] = "Group C";
                            count = count + 1;
                            dr.EndEdit();
                            dt.Rows.Add(dr);
                            dt.AcceptChanges();

                            listteamA.Insert(k, list3[j].ToString());
                            listteamB.Insert(k, list3[n - j].ToString());
                            k++;
                        }
                    }
                }

                int qq = 1;
                int ww;
                int bb = 1;
                for (ww = i + 1; ww <= i + 2; ww++)
                {

                    DataRow dr1 = dt.NewRow();
                    dr1.BeginEdit();
                    dr1["Teams"] = "TBC" + qq + " v/s " + "TBC" + (qq + 1) + "  (SemiFinal-" + bb + ")";
                    dr1["SerialNo"] = "Match No " + count.ToString();
                    dr1["Day"] = "Day - " + (ww - 1).ToString();
                    dr1["MatchId"] = zonename + divletter + "M" + seasonname +   "SF" + sf1;
                    sf1++;
                    dr1["Group"] = "Semi-Final";
                    dr1.EndEdit();
                    dt.Rows.Add(dr1);
                    dt.AcceptChanges();
                    count++;
                    qq = qq + 2;
                    bb++;

                }

                for (int n = ww + 1; n <= ww + 1; n++)
                {
                    DataRow dr2 = dt.NewRow();
                    dr2.BeginEdit();
                    dr2["Teams"] = "TBC" + qq + " v/s " + "TBC" + (qq + 1) + "  (Final)";
                    dr2["SerialNo"] = "Match No " + count.ToString();
                    dr2["Day"] = "Day - " + (ww - 1).ToString();
                    dr2["MatchId"] = zonename + divletter + "M" + seasonname +   "FIN";
                    dr2["Group"] = "Final";
                    dr2.EndEdit();
                    dt.Rows.Add(dr2);
                    dt.AcceptChanges();
                }
            }


            // Groups A, B, C, D
            else if (NoOfGroups == 4)
            {
               int matchno0 = 0;
               int matchno1 = 1;

                int i;
                for ( i = 0; i < octeam1.Count; i++)
                {
                    list1.Insert(i, octeam1[i]);
                }

                for (int h = 0; h < octeam2.Count; h++)
                {
                    list2.Insert(h, octeam2[h]);
                }
                for (int p = 0; p < octeam3.Count; p++)
                {
                    list3.Insert(p, octeam3[p]);
                }
                for (int q = 0; q < octeam4.Count; q++)
                {
                    list4.Insert(q, octeam4[q]);
                }

                for (int e = 1; e <= list1.Count - 1; e++)
                {
                    rotationA();

                    for (int j = 0; j <= ((list1.Count - 1) / 2); j++)
                    {

                        int n = list1.Count - 1;
                        if (!(list1[j].ToString() == "Dummy A" || list1[n - j].ToString() == "Dummy A"))
                        {

                            DataRow dr = dt.NewRow();
                            dr.BeginEdit();
                            dr["Teams"] = list1[j].ToString() + " v/s " + list1[n - j].ToString();
                            dr["SerialNo"] = "Match No " + count.ToString();
                            dr["Day"] = "Day - " + i.ToString();

                            dr["MatchId"] = zonename + divletter + "M" + seasonname +   "A" + matchno0 + matchno1;

                            if (matchno1 != 9)
                            {
                                matchno1++;
                            }
                            else
                            {
                                matchno0++;
                                matchno1 = 0;
                            }

                            dr["Group"] = "Group A";
                            count = count + 1;
                            dr.EndEdit();
                            dt.Rows.Add(dr);
                            dt.AcceptChanges();

                            listteamA.Insert(k, list1[j].ToString());
                            listteamB.Insert(k, list1[n - j].ToString());
                            k++;
                        }
                    }
                }

                matchno0 = 0;
                matchno1 = 1;
                
                for (int ii = 1; ii <= list2.Count - 1; ii++)
                {
                    rotationB();

                    for (int j = 0; j <= ((list2.Count - 1) / 2); j++)
                    {

                        int n = list2.Count - 1;
                        if (!(list2[j].ToString() == "Dummy B" || list2[n - j].ToString() == "Dummy B"))
                        {
                            
                            DataRow dr = dt.NewRow();
                            dr.BeginEdit();
                            dr["Teams"] = list2[j].ToString() + " v/s " + list2[n - j].ToString();
                            dr["SerialNo"] = "Match No " + count.ToString();
                            dr["Day"] = "Day - " + i.ToString();

                            dr["MatchId"] = zonename + divletter + "M" + seasonname +   "B" + matchno0 + matchno1;

                            if (matchno1 != 9)
                            {
                                matchno1++;
                            }
                            else
                            {
                                matchno0++;
                                matchno1 = 0;
                            }

                            dr["Group"] = "Group B";
                            count = count + 1;
                            dr.EndEdit();
                            dt.Rows.Add(dr);
                            dt.AcceptChanges();

                            listteamA.Insert(k, list2[j].ToString());
                            listteamB.Insert(k, list2[n - j].ToString());
                            k++;
                        }
                    }
                }


                matchno0 = 0;
                matchno1 = 1;
                for (int iii = 1; iii <= list3.Count - 1; iii++)
                {
                    rotationC();

                    for (int j = 0; j <= ((list3.Count - 1) / 2); j++)
                    {

                        int n = list3.Count - 1;
                        if (!(list3[j].ToString() == "Dummy C" || list3[n - j].ToString() == "Dummy C"))
                        {
                            
                            DataRow dr = dt.NewRow();
                            dr.BeginEdit();
                            dr["Teams"] = list3[j].ToString() + " v/s " + list3[n - j].ToString();
                            dr["SerialNo"] = "Match No " + count.ToString();
                            dr["Day"] = "Day - " + i.ToString();

                            dr["MatchId"] = zonename + divletter + "M" + seasonname +   "C" + matchno0 + matchno1;

                            if (matchno1 != 9)
                            {
                                matchno1++;
                            }
                            else
                            {
                                matchno0++;
                                matchno1 = 0;
                            }

                            dr["Group"] = "Group C";
                            count = count + 1;
                            dr.EndEdit();
                            dt.Rows.Add(dr);
                            dt.AcceptChanges();

                            listteamA.Insert(k, list3[j].ToString());
                            listteamB.Insert(k, list3[n - j].ToString());
                            k++;
                        }
                    }
                }

                matchno0 = 0;
                matchno1 = 1;
                for (int iiii = 1; iiii <= list4.Count - 1; iiii++)
                {
                    rotationD();

                    for (int j = 0; j <= ((list4.Count - 1) / 2); j++)
                    {

                        int n = list4.Count - 1;
                        if (!(list4[j].ToString() == "Dummy D" || list4[n - j].ToString() == "Dummy D"))
                        {
                            
                            DataRow dr = dt.NewRow();
                            dr.BeginEdit();
                            dr["Teams"] = list4[j].ToString() + " v/s " + list4[n - j].ToString();
                            dr["SerialNo"] = "Match No " + count.ToString();
                            dr["Day"] = "Day - " + i.ToString();

                            dr["MatchId"] = zonename + divletter + "M" + seasonname +   "D" + matchno0 + matchno1;

                            if (matchno1 != 9)
                            {
                                matchno1++;
                            }
                            else
                            {
                                matchno0++;
                                matchno1 = 0;
                            }

                            dr["Group"] = "Group D";
                            count = count + 1;
                            dr.EndEdit();
                            dt.Rows.Add(dr);
                            dt.AcceptChanges();

                            listteamA.Insert(k, list4[j].ToString());
                            listteamB.Insert(k, list4[n - j].ToString());
                            k++;
                        }
                    }
                }

                int qqq = 1;
                int www;
                int bbb = 1;
                for (www = i + 1; www <= i + 2; www++)
                {

                    DataRow dr1 = dt.NewRow();
                    dr1.BeginEdit();
                    dr1["Teams"] = "TBC" + qqq + " v/s " + "TBC" + (qqq + 1) + "  (SemiFinal-" + bbb + ")";
                    dr1["SerialNo"] = "Match No " + count.ToString();
                    dr1["Day"] = "Day - " + (www - 1).ToString();
                    dr1["MatchId"] = zonename + divletter + "M" + seasonname +   "SF" + sf1;
                    sf1++; 
                    dr1["Group"] = "Semi-Final";
                    dr1.EndEdit();
                    dt.Rows.Add(dr1);
                    dt.AcceptChanges();
                    count++;
                    qqq = qqq + 2;
                    bbb++;


                }

                for (int n = www + 1; n <= www + 1; n++)
                {
                    DataRow dr2 = dt.NewRow();
                    dr2.BeginEdit();
                    dr2["Teams"] = "TBC" + qqq + " v/s " + "TBC" + (qqq + 1) + "  (Final)";
                    dr2["SerialNo"] = "Match No " + count.ToString();
                    dr2["Day"] = "Day - " + (www - 1).ToString();
                    dr2["MatchId"] = zonename + divletter + "M" + seasonname +   "FIN";
                    dr2["Group"] = "Final";
                    dr2.EndEdit();
                    dt.Rows.Add(dr2);
                    dt.AcceptChanges();
                }
            }

            return (dt);

        }

        public void rotationA()
        {
            string temp = list1[list1.Count - 1];
            for (int i = list1.Count - 1; i >= 2; i--)
            {
                list1[i] = list1[i - 1];
            }
            list1[1] = temp;
        }

        public void rotationB()
        {
            string temp = list2[list2.Count - 1];
            for (int i = list2.Count - 1; i >= 2; i--)
            {
                list2[i] = list2[i - 1];
            }
            list2[1] = temp;
        }

        public void rotationC()
        {
            string temp = list3[list3.Count - 1];
            for (int i = list3.Count - 1; i >= 2; i--)
            {
                list3[i] = list3[i - 1];
            }
            list3[1] = temp;
        }

        public void rotationD()
        {
            string temp = list4[list4.Count - 1];
            for (int i = list4.Count - 1; i >= 2; i--)
            {
                list4[i] = list4[i - 1];
            }
            list4[1] = temp;
        }
    }
}