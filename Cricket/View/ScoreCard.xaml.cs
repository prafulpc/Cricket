using Cricket.BLL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Cricket.View;

using CricketSol;
using CricketSol.Base;
using CricketSol.DAL;
using CricketSol.Database;
using CricketSol.System;
using System.Configuration;
using System.Windows.Controls.Primitives;
using System.Data.OleDb;
using System.Threading;
using System.Windows.Threading;
using System.Text.RegularExpressions;

namespace Cricket.View
{
    /// <summary>
    /// Interaction logic for ScoreCard.xaml
    /// </summary>
    /// 


    public partial class ScoreCard : UserControl
    {
        decimal team1overs;
        decimal team2overs;

        string id;

        string starttimeA;
        string endtimeA;

        string starttimeB;
        string endtimeB;

        int runs1 = 0;
        int wides1 = 0;
        int noballs1 = 0;
        int wickts1 = 0;
        int bruns1 = 0;
        double overs1 = 0;

        int R1 = 0;
        int W1 = 0;
        int N1 = 0;
        int Wkts1 = 0;
        int BR1 = 0;
        double O1 = 0;

        int runs2 = 0;
        int wides2 = 0;
        int noballs2 = 0;
        int wickts2 = 0;
        int bruns2 = 0;
        double overs2 = 0;

        int R2 = 0;
        int W2 = 0;
        int N2 = 0;
        int Wkts2 = 0;
        int BR2 = 0;
        double O2 = 0;

        bool ismatchnotplayed;

        int firstinngcount1 = 0;
        int firstinngcount2 = 0;

        int secondinngcount1 = 0;
        int secondinngcount2 = 0;

        bool _isResultclick;
        bool _isSaveClick;
        bool _IsClickedOnce;

        DataRowView drvbat1;
        DataRowView drvbowl1;
        DataRowView drvbat2;
        DataRowView drvbowl2;
     
        bool ismanualeditcommit = false;

        ObservableCollection<string> teamA = new ObservableCollection<string>();
        ObservableCollection<string> teamB = new ObservableCollection<string>();

        OleDbConnection oleconn = Database.getConnection();

        public ScoreCard()
        {
            InitializeComponent();
        }

        public void loadplayer()
        {
            try
            {
                if (_isResultclick == false)
                {
                    if (_IsClickedOnce == false)
                    {


                        ScoreCardBLL objscorecardplay = new ScoreCardBLL();
                        // SelectPlayers obselectjplayer = new SelectPlayers();

                        lblbat1.Content = tempvalues.teamA;
                        lblbowl1.Content = tempvalues.teamB;
                        lblbat2.Content = tempvalues.teamB;
                        lblbowl2.Content = tempvalues.teamA;

                        foreach (var abc in (TempValuesTeam1.team1))
                        {
                            if (abc != null)
                            {
                                teamA.Add(abc.ToString());
                            }
                        }

                        foreach (var xyz in TempValuesTeam2.team2)
                        {
                            if (xyz != null)
                            {
                                teamB.Add(xyz.ToString());
                            }
                        }



                        //teamA (batting)  teamB (bowling)
                        if ((comboboxvalue.value[0] == "0" && comboboxvalue.value[1] == "0") || (comboboxvalue.value[0] == "1" && comboboxvalue.value[1] == "1"))
                        {
                            for (int i = 0; i <= teamA.Count - 1; i++)
                            {
                                KSCA.team1[i] = KSCAUID.teamA[i];
                            }

                            for (int j = 0; j <= teamB.Count - 1; j++)
                            {
                                KSCA.team2[j] = KSCAUID.teamB[j];
                            }

                            //dgvbat1.DataContext = objscorecardplay.batting1(teamA);
                            //dgvbat2.DataContext = objscorecardplay.batting2(teamB);
                            //dgvbowl1.DataContext = objscorecardplay.bowling1(teamB);
                            //dgvbowl2.DataContext = objscorecardplay.bowling2(teamA);

                            dgvbat1.ItemsSource = (objscorecardplay.batting1(teamA)).DefaultView;
                            dgvbat2.ItemsSource = (objscorecardplay.batting2(teamB)).DefaultView;
                            dgvbowl1.ItemsSource = (objscorecardplay.bowling1(teamB)).DefaultView;
                            dgvbowl2.ItemsSource = (objscorecardplay.bowling2(teamA)).DefaultView;
                        }





                        //teamA (bowling)  teamB (batting)
                        if ((comboboxvalue.value[0] == "0" && comboboxvalue.value[1] == "1") || (comboboxvalue.value[0] == "1" && comboboxvalue.value[1] == "0"))
                        {
                            for (int i = 0; i <= teamB.Count - 1; i++)
                            {
                                KSCA.team1[i] = KSCAUID.teamB[i];
                            }

                            for (int j = 0; j <= teamA.Count - 1; j++)
                            {
                                KSCA.team2[j] = KSCAUID.teamA[j];
                            }
                            //dgvbat1.DataContext = objscorecardplay.batting1(teamB);
                            //dgvbat2.DataContext = objscorecardplay.batting2(teamA);
                            //dgvbowl1.DataContext = objscorecardplay.bowling1(teamA);
                            //dgvbowl2.DataContext = objscorecardplay.bowling2(teamB);

                            dgvbat1.ItemsSource = (objscorecardplay.batting1(teamB)).DefaultView;
                            dgvbat2.ItemsSource = (objscorecardplay.batting2(teamA)).DefaultView;
                            dgvbowl1.ItemsSource = (objscorecardplay.bowling1(teamA)).DefaultView;
                            dgvbowl2.ItemsSource = (objscorecardplay.bowling2(teamB)).DefaultView;
                        }


                    }

                }
                _isResultclick = false;
                _IsClickedOnce = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //public void timeA()
        //{
        //    if (chkmatchnotplayed.IsChecked == false)
        //    {
        //        DateTime startTime = Convert.ToDateTime(start_time1.Value);
        //        DateTime endtime = Convert.ToDateTime(close_time1.Value);


        //        string s_time;
        //        if ((startTime.ToString()).Substring(startTime.ToString().IndexOf(" ") + 2, 1) == ":")
        //        {

        //            // 2:41:32 AM
        //            s_time = ((startTime.ToString()).Substring(startTime.ToString().IndexOf(" ") + 1, 10));
        //            starttimeA = ((s_time.Substring(0, 4))) + " " + (s_time.Substring(8, 2));
        //        }
        //        else
        //        {
        //            // 08-May-15 12:41:32 AM
        //            s_time = ((startTime.ToString()).Substring(startTime.ToString().IndexOf(" ") + 1, 11));
        //            starttimeA = ((s_time.Substring(0, 5))) + " " + (s_time.Substring(9, 2));
        //        }

        //        string e_time;
        //        if ((endtime.ToString()).Substring(endtime.ToString().IndexOf(" ") + 2, 1) == ":")
        //        {
        //            // 2:41:32 AM
        //            e_time = ((endtime.ToString()).Substring(endtime.ToString().IndexOf(" ") + 1, 10));
        //            endtimeA = ((e_time.Substring(0, 4))) + " " + (e_time.Substring(8, 2));
        //        }
        //        else
        //        {
        //            // 08-May-15 12:41:32 AM
        //            e_time = ((endtime.ToString()).Substring(endtime.ToString().IndexOf(" ") + 1, 11));
        //            endtimeA = ((e_time.Substring(0, 5))) + " " + (e_time.Substring(9, 2));
        //        }
        //    }

        //}


        //public void timeB()
        //{
        //    if (chkmatchnotplayed.IsChecked == false)
        //    {
        //        DateTime startTime = Convert.ToDateTime(start_time2.Value);
        //        DateTime endtime = Convert.ToDateTime(close_time2.Value);

        //        string s_time;
        //        if ((startTime.ToString()).Substring(startTime.ToString().IndexOf(" ") + 2, 1) == ":")
        //        {

        //            // 2:41:32 AM   06-May-15 12:00:00 AM
        //            s_time = ((startTime.ToString()).Substring(startTime.ToString().IndexOf(" ") + 1, 10));
        //            starttimeB = ((s_time.Substring(0, 4))) + " " + (s_time.Substring(8, 2));
        //        }
        //        else
        //        {
        //            // 08-May-15 12:41:32 AM
        //            s_time = ((startTime.ToString()).Substring(startTime.ToString().IndexOf(" ") + 1, 11));
        //            starttimeB = ((s_time.Substring(0, 5))) + " " + (s_time.Substring(9, 2));
        //        }

        //        string e_time;
        //        if ((endtime.ToString()).Substring(endtime.ToString().IndexOf(" ") + 2, 1) == ":")
        //        {
        //            // 2:41:32 AM
        //            e_time = ((endtime.ToString()).Substring(endtime.ToString().IndexOf(" ") + 1, 10));
        //            endtimeB = ((e_time.Substring(0, 4))) + " " + (e_time.Substring(8, 2));
        //        }
        //        else
        //        {
        //            // 08-May-15 12:41:32 AM
        //            e_time = ((endtime.ToString()).Substring(endtime.ToString().IndexOf(" ") + 1, 11));
        //            endtimeB = ((e_time.Substring(0, 5))) + " " + (e_time.Substring(9, 2));
        //        }
        //    }

        //}

        public void extras1_empty()
        {
            try
            {
                if (chkmatchnotplayed.IsChecked == false)
                {
                    if (txtbyes1.Text == "")
                    {
                        MessageBox.Show("Enter Byes");
                    }

                    else if (txtlegbyes1.Text == "")
                    {
                        MessageBox.Show("Enter Leg-Byes");
                    }

                    else if (txtpenalty1.Text == "")
                    {
                        MessageBox.Show("Enter Penalty Runs");
                    }
                    else if (txtrunout1.Text == "")
                    {
                        MessageBox.Show("Enter Run-Out");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void extras2_empty()
        {
            try
            {
                if (chkmatchnotplayed.IsChecked == false)
                {
                    if (txtbyes2.Text == "")
                    {
                        MessageBox.Show("Enter Byes");
                    }

                    else if (txtlegbyes2.Text == "")
                    {
                        MessageBox.Show("Enter Leg-Byes");
                    }

                    else if (txtpenalty2.Text == "")
                    {
                        MessageBox.Show("Enter Penalty Runs");
                    }
                    else if (txtrunout2.Text == "")
                    {
                        MessageBox.Show("Enter Run-Out");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public decimal runratecalculation(decimal runs, decimal overs)
        {
            if (chkmatchnotplayed.IsChecked == false)
            {
                string id = Convert.ToString(overs);

                decimal a1;
                decimal b1;

                a1 = Convert.ToDecimal(Math.Truncate(overs)); //a1 = 4

                string str1 = Convert.ToString(Math.Round(overs - a1, 1));
                if (str1 != "0")
                {
                    b1 = Convert.ToDecimal(str1.Split('.')[1].Trim()); //b1 = 3
                }
                else
                {
                    b1 = 0;
                }


                decimal runrate = (runs) / (a1 * 6 + b1);
                return runrate;
            }
            else
            {
                decimal runrate = 0;
                return runrate;
            }
        }

        public void btn1score_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((comboboxvalue.value[0] == "0" && comboboxvalue.value[1] == "0") || (comboboxvalue.value[0] == "1" && comboboxvalue.value[1] == "1"))
                {
                    firstinngcount1 = teamA.Count;
                    firstinngcount2 = teamB.Count;
                }
                else
                {
                    firstinngcount1 = teamB.Count;
                    firstinngcount2 = teamA.Count;
                }

                ScoreCardBLL objscorecardplay = new ScoreCardBLL();

                if (txtbyes1.Text == "" || txtlegbyes1.Text == "" || txtpenalty1.Text == "" || txtrunout1.Text == "")
                {
                    extras1_empty();
                }

                else
                {

                    for (int i = 0; i <= (firstinngcount1 - 1); i++)
                    {

                        if (!DBNull.Value.Equals((dgvbat1.Items[i] as DataRowView).Row.ItemArray[4]) && ((dgvbat1.Items[i] as DataRowView).Row.ItemArray[4]).ToString() != "")
                        {
                            R1 = Convert.ToInt16((dgvbat1.Items[i] as DataRowView).Row.ItemArray[4]);
                            runs1 = objscorecardplay.runs1(R1);

                        }
                    }

                    for (int j = 0; j <= (firstinngcount2 - 1); j++)
                    {
                        if (!DBNull.Value.Equals((dgvbowl1.Items[j] as DataRowView).Row.ItemArray[8]) && ((dgvbowl1.Items[j] as DataRowView).Row.ItemArray[8]).ToString() != "")
                        {
                            W1 = Convert.ToInt16((dgvbowl1.Items[j] as DataRowView).Row.ItemArray[8]);
                            wides1 = objscorecardplay.wides1(W1);
                        }
                    }

                    for (int k = 0; k <= (firstinngcount2 - 1); k++)
                    {
                        if (!DBNull.Value.Equals((dgvbowl1.Items[k] as DataRowView).Row.ItemArray[7]) && ((dgvbowl1.Items[k] as DataRowView).Row.ItemArray[7]).ToString() != "")
                        {
                            N1 = Convert.ToInt16((dgvbowl1.Items[k] as DataRowView).Row.ItemArray[7]);
                            noballs1 = objscorecardplay.noball1(N1);
                        }
                    }

                    for (int m = 0; m <= (firstinngcount2 - 1); m++)
                    {
                        if (!DBNull.Value.Equals((dgvbowl1.Items[m] as DataRowView).Row.ItemArray[6]) && ((dgvbowl1.Items[m] as DataRowView).Row.ItemArray[6]).ToString() != "")
                        {
                            Wkts1 = Convert.ToInt16((dgvbowl1.Items[m] as DataRowView).Row.ItemArray[6]);
                            wickts1 = objscorecardplay.wickets1(Wkts1);
                        }
                    }
                    int runout1 = Convert.ToInt16(txtrunout1.Text);
                    wickts1 = wickts1 + runout1;


                    for (int n = 0; n <= (firstinngcount2 - 1); n++)
                    {
                        if (!DBNull.Value.Equals((dgvbowl1.Items[n] as DataRowView).Row.ItemArray[5]) && ((dgvbowl1.Items[n] as DataRowView).Row.ItemArray[5]).ToString() != "")
                        {
                            BR1 = Convert.ToInt16((dgvbowl1.Items[n] as DataRowView).Row.ItemArray[5]);
                            bruns1 = objscorecardplay.bowlerruns1(BR1);
                        }
                    }

                    for (int ov = 0; ov <= (firstinngcount2 - 1); ov++)
                    {
                        if (!DBNull.Value.Equals((dgvbowl1.Items[ov] as DataRowView).Row.ItemArray[3]) && ((dgvbowl1.Items[ov] as DataRowView).Row.ItemArray[3]).ToString() != "")
                        {
                            O1 = Convert.ToDouble((dgvbowl1.Items[ov] as DataRowView).Row.ItemArray[3]);
                            overs1 = objscorecardplay.overs1(O1);
                        }
                    }


                    txttotalextras1.Text = (wides1 + noballs1 + Convert.ToInt16(txtpenalty1.Text) + Convert.ToInt16(txtbyes1.Text) + Convert.ToInt16(txtlegbyes1.Text)).ToString();
                    txtruns1.Text = (runs1 + wides1 + noballs1 + Convert.ToInt16(txtpenalty1.Text) + Convert.ToInt16(txtbyes1.Text) + Convert.ToInt16(txtlegbyes1.Text)).ToString();
                    txtwickets1.Text = wickts1.ToString();
                    txtovers1.Text = overs1.ToString();

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void btn2score_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((comboboxvalue.value[0] == "0" && comboboxvalue.value[1] == "0") || (comboboxvalue.value[0] == "1" && comboboxvalue.value[1] == "1"))
                {
                    secondinngcount1 = teamB.Count;
                    secondinngcount2 = teamA.Count;
                }
                else
                {
                    secondinngcount1 = teamA.Count;
                    secondinngcount2 = teamB.Count;
                }

                ScoreCardBLL objscorecardplay = new ScoreCardBLL();

                if (txtbyes2.Text == "" || txtlegbyes2.Text == "" || txtpenalty2.Text == "" || txtrunout2.Text == "")
                {
                    extras2_empty();
                }

                else
                {

                    for (int i = 0; i <= (secondinngcount1 - 1); i++)
                    {

                        if (!DBNull.Value.Equals((dgvbat2.Items[i] as DataRowView).Row.ItemArray[4]) && ((dgvbat2.Items[i] as DataRowView).Row.ItemArray[4]).ToString() != "")
                        {
                            R2 = Convert.ToInt16((dgvbat2.Items[i] as DataRowView).Row.ItemArray[4]);
                            runs2 = objscorecardplay.runs2(R2);
                        }
                    }


                    for (int j = 0; j <= (secondinngcount2 - 1); j++)
                    {
                        if (!DBNull.Value.Equals((dgvbowl2.Items[j] as DataRowView).Row.ItemArray[8]) && ((dgvbowl2.Items[j] as DataRowView).Row.ItemArray[8]).ToString() != "")
                        {
                            W2 = Convert.ToInt16((dgvbowl2.Items[j] as DataRowView).Row.ItemArray[8]);
                            wides2 = objscorecardplay.wides2(W2);
                        }
                    }

                    for (int k = 0; k <= (secondinngcount2 - 1); k++)
                    {
                        if (!DBNull.Value.Equals((dgvbowl2.Items[k] as DataRowView).Row.ItemArray[7]) && ((dgvbowl2.Items[k] as DataRowView).Row.ItemArray[7]).ToString() != "")
                        {
                            N2 = Convert.ToInt16((dgvbowl2.Items[k] as DataRowView).Row.ItemArray[7]);
                            noballs2 = objscorecardplay.noball2(N2);
                        }
                    }

                    for (int m = 0; m <= (secondinngcount2 - 1); m++)
                    {
                        if (!DBNull.Value.Equals((dgvbowl2.Items[m] as DataRowView).Row.ItemArray[6]) && ((dgvbowl2.Items[m] as DataRowView).Row.ItemArray[6]).ToString() != "")
                        {
                            Wkts2 = Convert.ToInt16((dgvbowl2.Items[m] as DataRowView).Row.ItemArray[6]);
                            wickts2 = objscorecardplay.wickets2(Wkts2);
                        }
                    }
                    int runout2 = Convert.ToInt16(txtrunout2.Text);
                    wickts2 = wickts2 + runout2;


                    for (int n = 0; n <= (secondinngcount2 - 1); n++)
                    {
                        if (!DBNull.Value.Equals((dgvbowl2.Items[n] as DataRowView).Row.ItemArray[5]) && ((dgvbowl2.Items[n] as DataRowView).Row.ItemArray[5]).ToString() != "")
                        {
                            BR2 = Convert.ToInt16((dgvbowl2.Items[n] as DataRowView).Row.ItemArray[5]);
                            bruns2 = objscorecardplay.bowlerruns2(BR2);
                        }
                    }

                    for (int ov = 0; ov <= (secondinngcount2 - 1); ov++)
                    {
                        if (!DBNull.Value.Equals((dgvbowl2.Items[ov] as DataRowView).Row.ItemArray[3]) && ((dgvbowl2.Items[ov] as DataRowView).Row.ItemArray[3]).ToString() != "")
                        {
                            O2 = Convert.ToDouble((dgvbowl2.Items[ov] as DataRowView).Row.ItemArray[3]);
                            overs2 = objscorecardplay.overs2(O2);
                        }
                    }

                    txttotalextras2.Text = (wides2 + noballs2 + Convert.ToInt16(txtpenalty2.Text) + Convert.ToInt16(txtbyes2.Text) + Convert.ToInt16(txtlegbyes2.Text)).ToString();
                    txtruns2.Text = (runs2 + wides2 + noballs2 + Convert.ToInt16(txtpenalty2.Text) + Convert.ToInt16(txtbyes2.Text) + Convert.ToInt16(txtlegbyes2.Text)).ToString();
                    txtwickets2.Text = wickts2.ToString();
                    txtovers2.Text = overs2.ToString();

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Result_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _isResultclick = true;
                string abc;

                if (ismatchnotplayed == true)
                {
                    txtruns1.Text = "0";
                    txtruns2.Text = "0";
                    Match.Result = "Match Draw";
                }

                else
                {
                    if (txtruns1.Text == "" || txtruns2.Text == "")
                    {
                        MessageBox.Show("Information is Incompleted. Enter complete Information");
                    }

                     if (Convert.ToInt16(txtruns1.Text) > Convert.ToInt16(txtruns2.Text))
                    {
                        //if ((comboboxvalue.value[0] == "0" && comboboxvalue.value[1] == "0") || (comboboxvalue.value[0] == "1" && comboboxvalue.value[1] == "1"))
                        //{
                            abc = tempvalues.teamA;
                            Match.Result = abc + " won the match";

                       // }
                     }
                     else if (Convert.ToInt16(txtruns2.Text) > Convert.ToInt16(txtruns1.Text))
                        {
                            abc = tempvalues.teamB;
                            Match.Result = abc + " won the match";

                        }
                    
                    //else if (Convert.ToInt16(txtruns2.Text) > Convert.ToInt16(txtruns1.Text))
                    //{
                    //    if ((comboboxvalue.value[0] == "0" && comboboxvalue.value[1] == "0") || (comboboxvalue.value[0] == "1" && comboboxvalue.value[1] == "1"))
                    //    {
                    //        abc = tempvalues.teamB;
                    //        Match.Result = abc + " won the match";

                    //    }
                    //    else
                    //    {
                    //        abc = tempvalues.teamA;
                    //        Match.Result = abc + " won the match";

                    //    }
                    //}
                    else 
                    {
                        Match.Result = "Match Tied";

                    }
                }
                Button btn = (Button)sender;
                btn.Command = NavigationCommands.GoToPage;
                btn.CommandParameter = "/View/MatchResult.xaml";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        bool team0exit = false;
        bool team1exit = false;

        public bool checkteam()
        {
            // OleDbConnection oleconn = oleconn;
            try
            {
                ObservableCollection<SecondDivPointsCricket> obcteam = Database.GetEntityList<SecondDivPointsCricket>(false, false, false, oleconn, "RecordStatus", "TeamName");
                foreach (var item in obcteam)
                {
                    if (item.TeamName.ToString() == tempvalues.teamA)
                    {
                        team0exit = true;

                    }

                    if (item.TeamName.ToString() == tempvalues.teamB)
                    {
                        team1exit = true;

                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public decimal overscalculation(decimal forovers, decimal txtovers1)
        {
            //OleDbConnection oleconn = oleconn;
            try
            {
                int a1 = 0;
                int b1 = 0;

                a1 = Convert.ToInt16(Math.Truncate(forovers));
                string str1 = Convert.ToString(Math.Round(forovers - a1, 1));
                if (str1 != "0")
                {
                    b1 = Convert.ToInt16(str1.Split('.')[1].Trim());
                }
                else
                {
                    b1 = 0;
                }

                int a2 = 0;
                int b2 = 0;

                a2 = Convert.ToInt16(Math.Truncate(txtovers1));
                string str2 = Convert.ToString(Math.Round(txtovers1 - a2, 1));
                if (str2 != "0")
                {
                    b2 = Convert.ToInt16(str2.Split('.')[1].Trim());
                }
                else
                {
                    b2 = 0;
                }

                if ((b1 + b2) >= 6)
                {
                    int asd = a1 + a2 + 1;
                    int qwe = (b1 + b2) % 6;
                    decimal var = Convert.ToDecimal(asd + "." + qwe);
                    return var;
                }
                else
                {
                    int asd = a1 + a2;
                    int qwe = (b1 + b2) % 6;
                    decimal var = Convert.ToDecimal(asd + "." + qwe);
                    return var;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public decimal oversconvert(decimal overs)
        {
            //  OleDbConnection oleconn = oleconn;
            try
            {
                int a1 = 0;
                int b1 = 0;

                a1 = Convert.ToInt16(Math.Truncate(overs));
                string str1 = Convert.ToString(Math.Round(overs - a1, 1));
                if (str1 != "0")
                {
                    b1 = Convert.ToInt16(str1.Split('.')[1].Trim());
                }
                else
                {
                    b1 = 0;
                }

                decimal over = Convert.ToDecimal(Convert.ToDecimal(a1) + (Convert.ToDecimal(b1) / 6));
                return (over);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btnsave1_Click(object sender, RoutedEventArgs e)
        {

            _isSaveClick = true;




            //save Season Id
            //  ObservableCollection<Season> objseason = Database.GetEntityList<Season>(false, true, true, oleconn, "RecordStatus='Added'", "SeasonType");
            //   ObservableCollection<Zone> objzone = Database.GetEntityList<Zone>(false, true, true, oleconn, "RecordStatus='Added'", "ZoneName");
            //   ObservableCollection<Division> objdivision = Database.GetEntityList<Division>(false, true, true, oleconn, "RecordStatus='Added'", "DivisionName");
            ObservableCollection<Team> objteams = Database.GetEntityList<Team>(false, true, true, oleconn, "RecordStatus='Added'", "TeamName");
            ObservableCollection<Location> objlocationnames = Database.GetEntityList<Location>(false, true, true, oleconn, "RecordStatus", "LocationName");
            ObservableCollection<SecondDivPointsCricket> objseconddiv = Database.GetEntityList<SecondDivPointsCricket>(false, true, true, oleconn, "RecordStatus", "teamname");
            ObservableCollection<Player> objplayer1 = Database.GetEntityList<Player>(false, false, false, oleconn, "RecordStatus", "FirstName");
            ObservableCollection<BestBatsman> objbat1 = Database.GetEntityList<BestBatsman>(false, false, false, oleconn, "RecordStatus", "PlayerName");
            ObservableCollection<BestBowler> objbowl1 = Database.GetEntityList<BestBowler>(false, false, false, oleconn, "RecordStatus", "PlayerName");


            //  try
            // {


            // Team Report Team 1
            TeamReportTable ObjTeamReport1 = Database.GetNewEntity<TeamReportTable>();


            foreach (Team objTeam in objteams)
            {
                Team obj1 = Database.GetEntity<Team>(Guid.Parse(objTeam.TeamId.ToString()), false, false, false, oleconn);

                if (obj1.TeamName.ToString() == tempvalues.teamA)
                {
                    ObjTeamReport1.TeamId = obj1;
                    ObjTeamReport1.TeamName = obj1.TeamName;
                    break;
                }
            }


            //save Season Id
            //foreach (Season obcseason in objseason)
            //{
            //    if (obcseason.SeasonType.ToString() == lblseason.Content.ToString())
            //    {
            ObjTeamReport1.SeasonId = selectedgridvalue._seasonid;
            //  break;

            //    }
            //}

            //save Division Id
            //  foreach (Division obcdivision in objdivision)
            //{
            //    if (obcdivision.DivisionName.ToString() == lbldivision.Content.ToString())
            //    {
            ObjTeamReport1.DivisionId = selectedgridvalue.LoadDivisionId;
            //break;
            //    }
            //}



            ObjTeamReport1.Opponent = lblbowl1.Content.ToString();

            if (comboboxvalue.value[0] == "0")
            {
                ObjTeamReport1.Toss = "WON";
            }
            else
            {
                ObjTeamReport1.Toss = "LOST";
            }
            ObjTeamReport1.Venue = NameOf.Venue;
            ObjTeamReport1.RunsScored = Convert.ToInt16(txtruns1.Text);
            ObjTeamReport1.OversFaced = Convert.ToDecimal(txtovers1.Text);

            ObjTeamReport1.RunsConceded = Convert.ToInt16(txtruns2.Text);
            ObjTeamReport1.OversBowled = Convert.ToDecimal(txtovers2.Text);

            ObjTeamReport1.Points = Convert.ToInt16(TeamA.Points);
            ObjTeamReport1.Result = Match.Result;
            ObjTeamReport1.Remarks = Remark.Remarks;




            // Team Report Team 2
            TeamReportTable ObjTeamReport2 = Database.GetNewEntity<TeamReportTable>();

            foreach (Team objTeam in objteams)
            {
                if (objTeam.TeamName.ToString() == tempvalues.teamB)
                {
                    ObjTeamReport2.TeamId = objTeam;
                    ObjTeamReport2.TeamName = objTeam.TeamName;
                    break;
                }
            }

            //save Season Id
            //foreach (Season obcseason in objseason)
            //{
            //    if (obcseason.SeasonType.ToString() == lblseason.Content.ToString())
            //    {
            ObjTeamReport2.SeasonId = selectedgridvalue._seasonid;
            //        break;
            //    }
            //}

            //save Division Id
            //foreach (Division obcdivision in objdivision)
            //{
            //    if (obcdivision.DivisionName.ToString() == lbldivision.Content.ToString())
            //    {
            ObjTeamReport2.DivisionId = selectedgridvalue.LoadDivisionId;
            //        break;
            //    }
            //}



            ObjTeamReport2.Opponent = lblbat1.Content.ToString();

            if (comboboxvalue.value[0] == "1")
            {
                ObjTeamReport2.Toss = "WON";
            }
            else
            {
                ObjTeamReport2.Toss = "LOST";
            }
            ObjTeamReport2.Venue = NameOf.Venue;
            ObjTeamReport2.RunsScored = Convert.ToInt16(txtruns2.Text);
            ObjTeamReport2.OversFaced = Math.Round(Convert.ToDecimal(txtovers2.Text), 1);

            ObjTeamReport2.RunsConceded = Convert.ToInt16(txtruns1.Text);
            ObjTeamReport2.OversBowled = Math.Round(Convert.ToDecimal(txtovers1.Text), 1);

            ObjTeamReport2.Points = Convert.ToInt16(TeamB.Points);
            ObjTeamReport2.Result = Match.Result;
            ObjTeamReport2.Remarks = Remark.Remarks;



            progressbar.Value = 25;
            ////oleconn.Close();

            //////////////////////////////////


            // Location Report


            LocationReportTable ObjLocationReport = Database.GetNewEntity<LocationReportTable>();


            foreach (Location objlocation in objlocationnames)
            {

                if (objlocation.StadiumName.ToString() == NameOf.Venue)
                {
                    ObjLocationReport.Location = objlocation;
                    ObjLocationReport.LocationName = objlocation.StadiumName;
                    ObjLocationReport.StartDate = temp.stratdate;
                    ObjLocationReport.EndDate = temp.enddate;
                    ObjLocationReport.Division = season_zone_div.name[2].ToString();
                    ObjLocationReport.Group = selectedgridvalue.group;
                    ObjLocationReport.Matchtype = temp.matchtype;
                    ObjLocationReport.MatchId = selectedgridvalue.matchid;
                    ObjLocationReport.TeamOne = team.name[0];
                    ObjLocationReport.TeamTwo = team.name[1];
                    ObjLocationReport.UmpireOne = NameOf.Umpire1;
                    ObjLocationReport.UmpireTwo = NameOf.Umpire2;
                    ObjLocationReport.Scorer = NameOf.Scorer;
                    ObjLocationReport.Result = Match.Result;
                    break;
                }

            }

            //save Season Id
            // ObservableCollection<Season> objseasonlocationreport = Database.GetEntityList<Season>(false, true, true, oleconn, "RecordStatus='Added'", "SeasonType");
            //foreach (Season obcseason in objseason)
            //{
            //    if (obcseason.SeasonType.ToString() == lblseason.Content.ToString())
            //    {
            //Location Report
            ObjLocationReport.SeasonId = selectedgridvalue._seasonid;
            //        break;
            //    }
            //}


            progressbar.Value = 25;
            ////////////////////



            DataTable dt = new DataTable();

            dt.Columns.Add("SLNOA");
            dt.Columns.Add("Name Of PlayersA");
            dt.Columns.Add("KSCA UIDA");
            dt.Columns.Add("DismissalA");
            dt.Columns.Add("RunsA");
            dt.Columns.Add("MinsA");
            dt.Columns.Add("BallsA");
            dt.Columns.Add("FoursA");
            dt.Columns.Add("SixesA");
            dt.Columns.Add("OversA");
            dt.Columns.Add("MaidensA");
            dt.Columns.Add("BRunsA");
            dt.Columns.Add("WicketsA");
            dt.Columns.Add("No BallA");
            dt.Columns.Add("WideA");
            dt.Columns.Add("AvgA");

            dt.Columns.Add("SLNOB");
            dt.Columns.Add("Name Of PlayersB");
            dt.Columns.Add("KSCA UIDB");
            dt.Columns.Add("DismissalB");
            dt.Columns.Add("RunsB");
            dt.Columns.Add("MinsB");
            dt.Columns.Add("BallsB");
            dt.Columns.Add("FoursB");
            dt.Columns.Add("SixesB");
            dt.Columns.Add("OversB");
            dt.Columns.Add("MaidensB");
            dt.Columns.Add("BRunsB");
            dt.Columns.Add("WicketsB");
            dt.Columns.Add("No BallB");
            dt.Columns.Add("WideB");
            dt.Columns.Add("AvgB");


            DataTable dt1 = new DataTable();
            dt1 = ((DataView)dgvbat1.ItemsSource).ToTable();

            // dgvbat1.DataContext as DataTable;

            DataTable dt2 = new DataTable();
            dt2 = ((DataView)dgvbowl2.ItemsSource).ToTable();

            //dgvbowl2.DataContext as DataTable;
            DataTable dt3 = new DataTable();
            dt3 = ((DataView)dgvbat2.ItemsSource).ToTable();

            //dgvbat2.DataContext as DataTable;
            DataTable dt4 = new DataTable();
            dt4 = ((DataView)dgvbowl1.ItemsSource).ToTable();

            //dgvbowl1.DataContext as DataTable;

            var JoinResult1 = (from BAT1 in dt1.AsEnumerable()
                               join BOWL2 in dt2.AsEnumerable() on BAT1.Field<string>("SLNO") equals BOWL2.Field<string>("SLNO")
                               join BAT2 in dt3.AsEnumerable() on BAT1.Field<string>("SLNO") equals BAT2.Field<string>("SLNO")
                               join d in dt4.AsEnumerable() on BAT1.Field<string>("SLNO") equals d.Field<string>("SLNO")


                               select new
                               {
                                   serialnumberA = BAT1.Field<string>("SLNO"),
                                   playernameA = BAT1.Field<string>("Name Of Players"),
                                   kscauidA = BAT1.Field<string>("KSCA UID"),

                                   dismissalA = BAT1.Field<string>("Dismissal"),
                                   battingrunsA = BAT1.Field<string>("Runs"),
                                   minsA = BAT1.Field<string>("Mins"),
                                   ballsA = BAT1.Field<string>("Balls"),
                                   foursA = BAT1.Field<string>("Fours"),
                                   sixesA = BAT1.Field<string>("Sixes"),

                                   oversA = BOWL2.Field<string>("Overs"),
                                   MaidensA = BOWL2.Field<string>("Maidens"),
                                   ballingrunsA = BOWL2.Field<string>("Runs"),
                                   wicketsA = BOWL2.Field<string>("Wickets"),
                                   noballsA = BOWL2.Field<string>("No Ball"),
                                   widesA = BOWL2.Field<string>("Wide"),
                                   averageA = BOWL2.Field<string>("Avg"),

                                   serialnumberB = BAT2.Field<string>("SLNO"),
                                   playernameB = BAT2.Field<string>("Name Of Players"),
                                   kscauidB = BAT2.Field<string>("KSCA UID"),

                                   dismissalB = BAT2.Field<string>("Dismissal"),
                                   battingrunsB = BAT2.Field<string>("Runs"),
                                   minsB = BAT2.Field<string>("Mins"),
                                   ballsB = BAT2.Field<string>("Balls"),
                                   foursB = BAT2.Field<string>("Fours"),
                                   sixesB = BAT2.Field<string>("Sixes"),

                                   oversB = d.Field<string>("Overs"),
                                   MaidensB = d.Field<string>("Maidens"),
                                   ballingrunsB = d.Field<string>("Runs"),
                                   wicketsB = d.Field<string>("Wickets"),
                                   noballsB = d.Field<string>("No Ball"),
                                   widesB = d.Field<string>("Wide"),
                                   averageB = d.Field<string>("Avg")


                               }).ToList();


            foreach (var data in JoinResult1)
            {

                DataRow dtr = dt.NewRow();
                dtr["SLNOA"] = data.serialnumberA;
                dtr["Name Of PlayersA"] = data.playernameA;
                dtr["KSCA UIDA"] = data.kscauidA;
                dtr["DismissalA"] = data.dismissalA;
                dtr["RunsA"] = data.battingrunsA;
                dtr["MinsA"] = data.minsA;
                dtr["BallsA"] = data.ballsA;
                dtr["FoursA"] = data.foursA;
                dtr["SixesA"] = data.sixesA;
                dtr["OversA"] = data.oversA;
                dtr["MaidensA"] = data.MaidensA;
                dtr["BrunsA"] = data.ballingrunsA;
                dtr["WicketsA"] = data.wicketsA;
                dtr["No BallA"] = data.noballsA;
                dtr["WideA"] = data.widesA;
                dtr["AvgA"] = data.averageA;


                dtr["SLNOB"] = data.serialnumberB;
                dtr["Name Of PlayersB"] = data.playernameB;
                dtr["KSCA UIDB"] = data.kscauidB;
                dtr["DismissalB"] = data.dismissalB;
                dtr["RunsB"] = data.battingrunsB;
                dtr["MinsB"] = data.minsB;
                dtr["BallsB"] = data.ballsB;
                dtr["FoursB"] = data.foursB;
                dtr["SixesB"] = data.sixesB;
                dtr["OversB"] = data.oversB;
                dtr["MaidensB"] = data.MaidensB;
                dtr["BrunsB"] = data.ballingrunsB;
                dtr["WicketsB"] = data.wicketsB;
                dtr["No BallB"] = data.noballsB;
                dtr["WideB"] = data.widesB;
                dtr["AvgB"] = data.averageB;


                dt.Rows.Add(dtr);

            }




            // MessageBox.Show(" ScoreCard Details Saved");


            //update team 1



            bool exist = checkteam();
            bool TeamFound = false;

            if (team0exit == true)
            {
                foreach (Team objTeam in objteams)
                {

                    if (objTeam.TeamName.ToString() == tempvalues.teamA)
                    {

                        foreach (SecondDivPointsCricket objpoint in objseconddiv)
                        {
                            //save Season Id
                            //foreach (Season obcseason in objseason)
                            //{
                            //    if (obcseason.SeasonType.ToString() == lblseason.Content.ToString())
                            //    {
                            objpoint.SeasonId = selectedgridvalue._seasonid;
                            //    }
                            //}

                            //save Division Id
                            //foreach (Division obcdivision in objdivision)
                            //{
                            //    if (obcdivision.DivisionName.ToString() == lbldivision.Content.ToString())
                            //    {
                            objpoint.DivisionId = selectedgridvalue.LoadDivisionId;
                            //    }
                            //}

                            if ((objpoint.TeamName.ToString() == tempvalues.teamA))
                            {
                                objpoint.Matches++;

                                //if (objpoint.TeamName.ToString() == tempvalues.teamA)
                                //{
                                objpoint.TeamId = objTeam;
                                objpoint.Points = Convert.ToInt16(objpoint.Points + Convert.ToInt16(TeamA.Points));
                                objpoint.ForRuns = Convert.ToInt16(objpoint.ForRuns + Convert.ToInt16(txtruns1.Text));
                                objpoint.AgainstRuns = Convert.ToInt16(objpoint.AgainstRuns + Convert.ToInt16(txtruns2.Text));

                                decimal forovers = Math.Round(objpoint.ForOvers, 1);
                                decimal againstovers = Math.Round(objpoint.AgainstOvers, 1);

                                if (txtwickets1.Text == "10")
                                {
                                    team1overs = 50;
                                }
                                else
                                {
                                    team1overs = Math.Round(Convert.ToDecimal(txtovers1.Text), 1);
                                }

                                if (txtwickets2.Text == "10")
                                {
                                    team2overs = 50;
                                }
                                else
                                {
                                    team2overs = Math.Round(Convert.ToDecimal(txtovers2.Text), 1);
                                }


                                objpoint.ForOvers = Math.Round(overscalculation(forovers, team1overs), 1);
                                objpoint.AgainstOvers = Math.Round(overscalculation(againstovers, team2overs), 1);

                                objpoint.For = objpoint.ForRuns + "/" + objpoint.ForOvers;
                                objpoint.Against = objpoint.AgainstRuns + "/" + objpoint.AgainstOvers;


                                if (TeamA.Result == "WON")
                                {
                                    objpoint.Won++;
                                }

                                else if (TeamA.Result == "LOST")
                                {
                                    objpoint.Lost++;

                                }
                                else if (TeamA.Result == "DRAW")
                                {
                                    objpoint.Tie++;

                                }

                                objpoint.RunRate = Math.Round(runratecalculation(objpoint.ForRuns, objpoint.ForOvers) - runratecalculation(objpoint.AgainstRuns, objpoint.AgainstOvers), 2);
                                //}

                                Database.SaveEntity<SecondDivPointsCricket>(objpoint, oleconn);
                                //  MessageBox.Show("Team 1 updated");
                                progressbar.Value = 25;
                                TeamFound = true;
                            }

                        }

                    }
                    else if (TeamFound == true)
                    {
                        break;
                    }
                }

            }


            //update team 2
            TeamFound = false;

            if (team1exit == true)
            {

                foreach (Team objTeam in objteams)
                {

                    if (objTeam.TeamName.ToString() == tempvalues.teamB)
                    {

                        foreach (SecondDivPointsCricket objpoint in objseconddiv)
                        {
                            //save Season Id
                            //foreach (Season obcseason in objseason)
                            //{
                            //    if (obcseason.SeasonType.ToString() == lblseason.Content.ToString())
                            //    {
                            objpoint.SeasonId = selectedgridvalue._seasonid;
                            //    }
                            //}

                            //save Division Id
                            //foreach (Division obcdivision in objdivision)
                            //{
                            //    if (obcdivision.DivisionName.ToString() == lbldivision.Content.ToString())
                            //    {
                            objpoint.DivisionId = selectedgridvalue.LoadDivisionId;
                            //    }
                            //}

                            if (objpoint.TeamName.ToString() == tempvalues.teamB)
                            {
                                objpoint.Matches++;


                                //if (objpoint.TeamName.ToString() == tempvalues.teamB)
                                //{
                                objpoint.TeamId = objTeam;
                                objpoint.Points = Convert.ToInt16(objpoint.Points + Convert.ToInt16(TeamB.Points));
                                objpoint.ForRuns = Convert.ToInt16(objpoint.ForRuns + Convert.ToInt16(txtruns2.Text));
                                objpoint.AgainstRuns = Convert.ToInt16(objpoint.AgainstRuns + Convert.ToInt16(txtruns1.Text));

                                decimal forovers = Math.Round(objpoint.ForOvers, 1);
                                decimal againstovers = Math.Round(objpoint.AgainstOvers, 1);

                                if (txtwickets1.Text == "10")
                                {
                                    team1overs = 50;
                                }
                                else
                                {
                                    team1overs = Math.Round(Convert.ToDecimal(txtovers1.Text), 1);
                                }

                                if (txtwickets2.Text == "10")
                                {
                                    team2overs = 50;
                                }
                                else
                                {
                                    team2overs = Math.Round(Convert.ToDecimal(txtovers2.Text), 1);
                                }

                                decimal txtover1 = Math.Round(Convert.ToDecimal(team1overs), 1);
                                decimal txtover2 = Math.Round(Convert.ToDecimal(team2overs), 1);


                                objpoint.ForOvers = Math.Round(overscalculation(forovers, txtover2), 1);
                                objpoint.AgainstOvers = Math.Round(overscalculation(againstovers, txtover1), 1);

                                objpoint.For = objpoint.ForRuns + "/" + objpoint.ForOvers;
                                objpoint.Against = objpoint.AgainstRuns + "/" + objpoint.AgainstOvers;


                                if (TeamB.Result == "WON")
                                {
                                    objpoint.Won++;

                                }
                                else if (TeamB.Result == "LOST")
                                {
                                    objpoint.Lost++;
                                }
                                else if (TeamB.Result == "DRAW")
                                {
                                    objpoint.Tie++;
                                }

                                objpoint.RunRate = Math.Round(runratecalculation(objpoint.ForRuns, objpoint.ForOvers) - runratecalculation(objpoint.AgainstRuns, objpoint.AgainstOvers), 2);
                                // }
                                Database.SaveEntity<SecondDivPointsCricket>(objpoint, oleconn);
                                //   MessageBox.Show("Team 2 updated");
                                progressbar.Value = 25;
                                TeamFound = true;

                            }
                        }
                    }
                    else if (TeamFound == true)
                    {
                        break;
                    }
                }

            }

            //add team 1
            TeamFound = false;
            if (team0exit == false)
            {
                foreach (Team objTeam in objteams)
                {


                    if (objTeam.TeamName.ToString() == tempvalues.teamA)
                    {

                        SecondDivPointsCricket objpc = Database.GetNewEntity<SecondDivPointsCricket>();

                        //save Season Id
                        // foreach (Season obcseason in objseason)
                        //{
                        //    if (obcseason.SeasonType.ToString() == lblseason.Content.ToString())
                        //    {
                        objpc.SeasonId = selectedgridvalue._seasonid;
                        //    }
                        //}

                        //save Division Id
                        //foreach (Division obcdivision in objdivision)
                        //{
                        //    if (obcdivision.DivisionName.ToString() == lbldivision.Content.ToString())
                        //    {
                        objpc.DivisionId = selectedgridvalue.LoadDivisionId;
                        //    }
                        //}


                        //if (objTeam.TeamName.ToString() == tempvalues.teamA)
                        //{
                        objpc.TeamId = objTeam;
                        objpc.TeamName = team.name[0];
                        objpc.Matches = 1;
                        objpc.Group = selectedgridvalue.group;

                        objpc.Points = Convert.ToInt16(TeamA.Points);
                        objpc.ForRuns = Convert.ToInt16(txtruns1.Text);
                        objpc.AgainstRuns = Convert.ToInt16(txtruns2.Text);

                        if (txtwickets1.Text == "10")
                        {
                            team1overs = 50;
                        }
                        else
                        {
                            team1overs = Math.Round(Convert.ToDecimal(txtovers1.Text), 1);
                        }

                        if (txtwickets2.Text == "10")
                        {
                            team2overs = 50;
                        }
                        else
                        {
                            team2overs = Math.Round(Convert.ToDecimal(txtovers2.Text), 1);
                        }

                        objpc.ForOvers = Math.Round(team1overs, 1);
                        objpc.AgainstOvers = Math.Round(team2overs, 1);
                        objpc.For = objpc.ForRuns + "/" + objpc.ForOvers;
                        objpc.Against = objpc.AgainstRuns + "/" + objpc.AgainstOvers;


                        if (TeamA.Result == "WON")
                        {
                            objpc.Won = 1;
                            objpc.Lost = 0;
                            objpc.Tie = 0;
                            objpc.NoResult = 0;


                        }
                        else if (TeamA.Result == "LOST")
                        {
                            objpc.Won = 0;
                            objpc.Lost = 1;
                            objpc.Tie = 0;
                            objpc.NoResult = 0;

                        }
                        else if (TeamA.Result == "DRAW")
                        {
                            objpc.Won = 0;
                            objpc.Lost = 0;
                            objpc.Tie = 1;
                            objpc.NoResult = 0;

                        }

                        decimal team1batting = objpc.ForRuns;
                        decimal team1bowling = Math.Round(team1overs, 1);

                        decimal team2batting = objpc.AgainstRuns;
                        decimal team2bowling = Math.Round(team2overs, 1);


                        objpc.RunRate = Math.Round(runratecalculation(team1batting, team1bowling) - runratecalculation(team2batting, team2bowling), 2);

                        // }
                        Database.SaveEntity<SecondDivPointsCricket>(objpc, oleconn);
                        // MessageBox.Show("Team 1 added");
                        progressbar.Value = 25;
                        TeamFound = true;
                    }
                    else if (TeamFound == true)
                    {
                        break;
                    }
                }

            }

            //add team 2
            TeamFound = false;
            if (team1exit == false)
            {

                foreach (Team objTeam in objteams)
                {
                    if (objTeam.TeamName.ToString() == tempvalues.teamB)
                    {

                        SecondDivPointsCricket objpc = Database.GetNewEntity<SecondDivPointsCricket>();

                        //save Season Id
                        //foreach (Season obcseason in objseason)
                        //{
                        //    if (obcseason.SeasonType.ToString() == lblseason.Content.ToString())
                        //    {
                        objpc.SeasonId = selectedgridvalue._seasonid;

                        //    }
                        //}

                        //save Division Id
                        //foreach (Division obcdivision in objdivision)
                        //{
                        //    if (obcdivision.DivisionName.ToString() == lbldivision.Content.ToString())
                        //    {
                        objpc.DivisionId = selectedgridvalue.LoadDivisionId;
                        //    }
                        //}


                        //if (objTeam.TeamName.ToString() == tempvalues.teamB)
                        //{
                        objpc.TeamId = objTeam;
                        objpc.TeamName = team.name[1];
                        objpc.Matches = 1;
                        objpc.Group = selectedgridvalue.group;

                        objpc.Points = Convert.ToInt16(TeamB.Points);
                        objpc.ForRuns = Convert.ToInt16(txtruns2.Text);
                        objpc.AgainstRuns = Convert.ToInt16(txtruns1.Text);

                        if (txtwickets1.Text == "10")
                        {
                            team1overs = 50;
                        }
                        else
                        {
                            team1overs = Math.Round(Convert.ToDecimal(txtovers1.Text), 1);
                        }

                        if (txtwickets2.Text == "10")
                        {
                            team2overs = 50;
                        }
                        else
                        {
                            team2overs = Math.Round(Convert.ToDecimal(txtovers2.Text), 1);
                        }

                        objpc.ForOvers = Math.Round(team2overs, 1);
                        objpc.AgainstOvers = Math.Round(team1overs, 1);

                        objpc.For = objpc.ForRuns + "/" + objpc.ForOvers;
                        objpc.Against = objpc.AgainstRuns + "/" + objpc.AgainstOvers;

                        if (TeamB.Result == "WON")
                        {
                            objpc.Won = 1;
                            objpc.Lost = 0;
                            objpc.Tie = 0;
                            objpc.NoResult = 0;

                        }
                        else if (TeamB.Result == "LOST")
                        {
                            objpc.Won = 0;
                            objpc.Lost = 1;
                            objpc.Tie = 0;
                            objpc.NoResult = 0;

                        }
                        else if (TeamB.Result == "DRAW")
                        {
                            objpc.Won = 0;
                            objpc.Lost = 0;
                            objpc.Tie = 1;
                            objpc.NoResult = 0;

                        }

                        decimal team1batting = objpc.AgainstRuns;
                        decimal team1bowling = Math.Round(team1overs, 1);

                        decimal team2batting = objpc.ForRuns;
                        decimal team2bowling = Math.Round(team2overs, 1);


                        objpc.RunRate = Math.Round((runratecalculation(team2batting, team2bowling) - runratecalculation(team1batting, team1bowling)), 2);
                        //  }

                        Database.SaveEntity<SecondDivPointsCricket>(objpc, oleconn);
                        //MessageBox.Show("Team 2 added");
                        progressbar.Value = 25;
                        TeamFound = true;
                    }
                    else if (TeamFound == true)
                    {
                        break;
                    }
                }

            }
            //////////////

            bool isaddedbt1 = false;
            bool isaddedbt2 = false;
            bool isaddedbl1 = false;
            bool isaddedbl2 = false;

            // add  Batting 1 


            for (int i = 0; i <= (firstinngcount1 - 1); i++)
            {
                string playerkscauid = Convert.ToString((dgvbat1.Items[i] as DataRowView).Row.ItemArray[2]);
                checkplayerbt1(playerkscauid);

                if (playerexitbt1 == false)
                {

                    if (!DBNull.Value.Equals((dgvbat1.Items[i] as DataRowView).Row.ItemArray[1]) && ((dgvbat1.Items[i] as DataRowView).Row.ItemArray[1]).ToString() != "")
                    {

                        string playername;

                        string id = Convert.ToString((dgvbat1.Items[i] as DataRowView).Row.ItemArray[1]);

                        if (id.Contains("("))
                        {
                            playername = id.Substring(0, id.IndexOf("(") - 1);
                        }
                        else
                        {
                            playername = id;
                        }

                        foreach (Player obj in objplayer1)
                        {

                            if (obj.KSCAUID == playerkscauid)
                            {


                                BestBatsman objbt1 = Database.GetNewEntity<BestBatsman>();

                                //save Season Id
                                //foreach (Season obcseason in objseason)
                                //{
                                //    if (obcseason.SeasonType.ToString() == lblseason.Content.ToString())
                                //    {
                                objbt1.SeasonId = selectedgridvalue._seasonid;

                                //    }
                                //}



                                //save Division Id
                                //foreach (Division obcdivision in objdivision)
                                //{
                                //    if (obcdivision.DivisionName.ToString() == lbldivision.Content.ToString())
                                //    {
                                objbt1.DivisionId = selectedgridvalue.LoadDivisionId;
                                //    }
                                //}


                                objbt1.Player = obj;
                                objbt1.PlayerName = playername;
                                objbt1.KSCAUID = obj.KSCAUID;

                                foreach (Team objteam in objteams)
                                {
                                    if (obj.TeamIdId.ToString() == objteam.TeamId.ToString())
                                    {
                                        objbt1.TeamName = objteam.TeamName.ToString();
                                    }
                                }

                                if (ismatchnotplayed == true)
                                {
                                    objbt1.Matches = 1;
                                    objbt1.Innings = 0;
                                }
                                else
                                {

                                    objbt1.Matches = 1;
                                    objbt1.Innings = 1;

                                    if (!DBNull.Value.Equals((dgvbat1.Items[i] as DataRowView).Row.ItemArray[3]) && ((dgvbat1.Items[i] as DataRowView).Row.ItemArray[3]).ToString() != "")
                                    {
                                        if (Convert.ToString((dgvbat1.Items[i] as DataRowView).Row.ItemArray[3]) == "NOT OUT")
                                        {
                                            objbt1.NotOut = 1;
                                        }
                                    }

                                    else
                                    {
                                        objbt1.NotOut = 0;
                                    }

                                    if (!DBNull.Value.Equals((dgvbat1.Items[i] as DataRowView).Row.ItemArray[4]) && ((dgvbat1.Items[i] as DataRowView).Row.ItemArray[4]).ToString() != "")
                                    {
                                        if ((Convert.ToString((dgvbat1.Items[i] as DataRowView).Row.ItemArray[6]) != "") && (Convert.ToInt16((dgvbat1.Items[i] as DataRowView).Row.ItemArray[6]) != 0))
                                        {
                                            objbt1.RunsScored = Convert.ToInt16((dgvbat1.Items[i] as DataRowView).Row.ItemArray[4]);
                                            objbt1.BallsFaced = Convert.ToInt16((dgvbat1.Items[i] as DataRowView).Row.ItemArray[6]);
                                            objbt1.HighestScore = objbt1.RunsScored;
                                            objbt1.StrikeRate = (Convert.ToDecimal(objbt1.RunsScored) / Convert.ToDecimal(objbt1.BallsFaced)) * 100;
                                        }
                                        else
                                        {
                                            objbt1.RunsScored = 0;
                                            objbt1.BallsFaced = 0;
                                            objbt1.StrikeRate = 0;
                                        }
                                        if (objbt1.RunsScored >= 50 && objbt1.RunsScored <= 99)
                                        {
                                            objbt1.Fifties = 1;
                                        }
                                        else
                                        {
                                            objbt1.Fifties = 0;
                                        }

                                        if (objbt1.RunsScored >= 100)
                                        {
                                            objbt1.Hundreds = 1;
                                        }
                                        else
                                        {
                                            objbt1.Hundreds = 0;
                                        }

                                        if (!DBNull.Value.Equals((dgvbat1.Items[i] as DataRowView).Row.ItemArray[7]) && ((dgvbat1.Items[i] as DataRowView).Row.ItemArray[7]).ToString() != "")
                                        {
                                            objbt1.Fours = Convert.ToInt16((dgvbat1.Items[i] as DataRowView).Row.ItemArray[7]);
                                        }
                                        else
                                        {
                                            objbt1.Fours = 0;
                                        }

                                        if (!DBNull.Value.Equals((dgvbat1.Items[i] as DataRowView).Row.ItemArray[8]) && ((dgvbat1.Items[i] as DataRowView).Row.ItemArray[8]).ToString() != "")
                                        {
                                            objbt1.Sixes = Convert.ToInt16((dgvbat1.Items[i] as DataRowView).Row.ItemArray[8]);
                                        }
                                        else
                                        {
                                            objbt1.Sixes = 0;
                                        }
                                    }
                                    else
                                    {
                                        objbt1.RunsScored = 0;
                                        objbt1.StrikeRate = 0;
                                        objbt1.Fifties = 0;
                                        objbt1.Hundreds = 0;
                                        objbt1.Fours = 0;
                                        objbt1.Sixes = 0;
                                    }
                                }

                                Database.SaveEntity<BestBatsman>(objbt1, oleconn);
                                isaddedbt1 = true;
                                //  MessageBox.Show("batting 1 added");
                                progressbar.Value = 50;
                            }
                        }
                    }
                }
            }

            // add              Batting 2 

            for (int i = 0; i <= (secondinngcount1 - 1); i++)
            {
                string playerkscauid = Convert.ToString((dgvbat2.Items[i] as DataRowView).Row.ItemArray[2]);
                checkplayerbt2(playerkscauid);


                if (playerexitbt2 == false)
                {

                    if (!DBNull.Value.Equals((dgvbat2.Items[i] as DataRowView).Row.ItemArray[1]) && ((dgvbat2.Items[i] as DataRowView).Row.ItemArray[1]).ToString() != "")
                    {

                        string playername;


                        string id = Convert.ToString((dgvbat2.Items[i] as DataRowView).Row.ItemArray[1]);

                        if (id.Contains("("))
                        {
                            playername = id.Substring(0, id.IndexOf("(") - 1);
                        }
                        else
                        {
                            playername = id;
                        }

                        foreach (Player obj in objplayer1)
                        {
                            if (obj.KSCAUID == playerkscauid)
                            {
                                BestBatsman objbt2 = Database.GetNewEntity<BestBatsman>();

                                //save Season Id
                                //foreach (Season obcseason in objseason)
                                //{
                                //    if (obcseason.SeasonType.ToString() == lblseason.Content.ToString())
                                //    {
                                objbt2.SeasonId = selectedgridvalue._seasonid;

                                //    }
                                //}

                                //save Division Id
                                //foreach (Division obcdivision in objdivision)
                                //{
                                //    if (obcdivision.DivisionName.ToString() == lbldivision.Content.ToString())
                                //    {
                                objbt2.DivisionId = selectedgridvalue.LoadDivisionId;
                                //    }
                                //}


                                objbt2.Player = obj;
                                objbt2.PlayerName = playername;
                                objbt2.KSCAUID = obj.KSCAUID;

                                foreach (Team objteam in objteams)
                                {
                                    if (obj.TeamIdId.ToString() == objteam.TeamId.ToString())
                                    {
                                        objbt2.TeamName = objteam.TeamName.ToString();
                                    }
                                }


                                if (ismatchnotplayed == true)
                                {
                                    objbt2.Matches = 1;
                                    objbt2.Innings = 0;
                                }
                                else
                                {
                                    objbt2.Matches = 1;
                                    objbt2.Innings = 1;

                                    if (!DBNull.Value.Equals((dgvbat2.Items[i] as DataRowView).Row.ItemArray[3]) && ((dgvbat2.Items[i] as DataRowView).Row.ItemArray[3]).ToString() != "")
                                    {
                                        if (Convert.ToString((dgvbat2.Items[i] as DataRowView).Row.ItemArray[3]) == "NOT OUT")
                                        {
                                            objbt2.NotOut = 1;
                                        }
                                    }
                                    else
                                    {
                                        objbt2.NotOut = 0;
                                    }

                                    if (!DBNull.Value.Equals((dgvbat2.Items[i] as DataRowView).Row.ItemArray[4]) && ((dgvbat2.Items[i] as DataRowView).Row.ItemArray[4]).ToString() != "")
                                    {
                                        if ((Convert.ToString((dgvbat2.Items[i] as DataRowView).Row.ItemArray[6]) != "") && (Convert.ToInt16((dgvbat2.Items[i] as DataRowView).Row.ItemArray[6]) != 0))
                                        {
                                            objbt2.RunsScored = Convert.ToInt16((dgvbat2.Items[i] as DataRowView).Row.ItemArray[4]);
                                            objbt2.BallsFaced = Convert.ToInt16((dgvbat2.Items[i] as DataRowView).Row.ItemArray[6]);
                                            objbt2.HighestScore = Convert.ToInt16(objbt2.RunsScored);
                                            objbt2.StrikeRate = (Convert.ToDecimal(objbt2.RunsScored) / Convert.ToDecimal(objbt2.BallsFaced)) * 100;
                                        }
                                        else
                                        {
                                            objbt2.RunsScored = 0;
                                            objbt2.BallsFaced = 0;
                                            objbt2.StrikeRate = 0;
                                        }
                                        if (objbt2.RunsScored >= 50 && objbt2.RunsScored <= 99)
                                        {
                                            objbt2.Fifties = 1;
                                        }
                                        else
                                        {
                                            objbt2.Fifties = 0;
                                        }

                                        if (objbt2.RunsScored >= 100)
                                        {
                                            objbt2.Hundreds = 1;
                                        }
                                        else
                                        {
                                            objbt2.Hundreds = 0;
                                        }

                                        if (!DBNull.Value.Equals((dgvbat2.Items[i] as DataRowView).Row.ItemArray[7]) && ((dgvbat2.Items[i] as DataRowView).Row.ItemArray[7]).ToString() != "")
                                        {
                                            objbt2.Fours = Convert.ToInt16((dgvbat2.Items[i] as DataRowView).Row.ItemArray[7]);
                                        }
                                        else
                                        {
                                            objbt2.Fours = 0;
                                        }

                                        if (!DBNull.Value.Equals((dgvbat2.Items[i] as DataRowView).Row.ItemArray[8]) && ((dgvbat2.Items[i] as DataRowView).Row.ItemArray[8]).ToString() != "")
                                        {
                                            objbt2.Sixes = Convert.ToInt16((dgvbat2.Items[i] as DataRowView).Row.ItemArray[8]);
                                        }
                                        else
                                        {
                                            objbt2.Sixes = 0;
                                        }
                                    }
                                    else
                                    {
                                        objbt2.RunsScored = 0;
                                        objbt2.StrikeRate = 0;
                                        objbt2.Fifties = 0;
                                        objbt2.Hundreds = 0;
                                        objbt2.Fours = 0;
                                        objbt2.Sixes = 0;
                                    }

                                }
                                Database.SaveEntity<BestBatsman>(objbt2, oleconn);
                                //  MessageBox.Show("batting 2 added");
                                progressbar.Value = 50;
                                isaddedbt2 = true;
                            }

                        }
                    }
                }
            }


            // add              Bowling 1 
            for (int i = 0; i <= (firstinngcount2 - 1); i++)
            {
                string playerkscauid = Convert.ToString((dgvbowl1.Items[i] as DataRowView).Row.ItemArray[2]);
                checkplayerbl1(playerkscauid);

                if (playerexitbl1 == false)
                {

                    if (!DBNull.Value.Equals((dgvbowl1.Items[i] as DataRowView).Row.ItemArray[1]) && ((dgvbowl1.Items[i] as DataRowView).Row.ItemArray[1]).ToString() != "")
                    {
                        string playername;
                        string id = Convert.ToString((dgvbowl1.Items[i] as DataRowView).Row.ItemArray[1]);
                        if (id.Contains("("))
                        {
                            playername = id.Substring(0, id.IndexOf("(") - 1);
                        }
                        else
                        {
                            playername = id;
                        }

                        foreach (Player obj in objplayer1)
                        {
                            if (obj.KSCAUID == playerkscauid)
                            {


                                BestBowler objbl1 = Database.GetNewEntity<BestBowler>();

                                //save Season Id
                                //foreach (Season obcseason in objseason)
                                //{
                                //    if (obcseason.SeasonType.ToString() == lblseason.Content.ToString())
                                //    {
                                objbl1.SeasonId = selectedgridvalue._seasonid;

                                //    }
                                //}



                                //save Division Id
                                //foreach (Division obcdivision in objdivision)
                                //{
                                //    if (obcdivision.DivisionName.ToString() == lbldivision.Content.ToString())
                                //    {
                                objbl1.DivisionId = selectedgridvalue.LoadDivisionId;
                                //    }
                                //}


                                objbl1.PlayerName = obj.FirstName;
                                objbl1.PlayerName = playername;
                                objbl1.KSCAUID = obj.KSCAUID;

                                foreach (Team objteam in objteams)
                                {
                                    if (obj.TeamIdId.ToString() == objteam.TeamId.ToString())
                                    {
                                        objbl1.TeamName = objteam.TeamName.ToString();
                                    }
                                }

                                if (ismatchnotplayed == true)
                                {
                                    objbl1.Matches = 1;
                                    objbl1.Innings = 0;
                                }
                                else
                                {
                                    objbl1.Matches = 1;
                                    objbl1.Innings = 1;
                                    if (!DBNull.Value.Equals((dgvbowl1.Items[i] as DataRowView).Row.ItemArray[3]) && ((dgvbowl1.Items[i] as DataRowView).Row.ItemArray[3]).ToString() != "")
                                    {

                                        objbl1.Overs = Math.Round(Convert.ToDecimal((dgvbowl1.Items[i] as DataRowView).Row.ItemArray[3]), 1);

                                        if (!DBNull.Value.Equals((dgvbowl1.Items[i] as DataRowView).Row.ItemArray[4]) && ((dgvbowl1.Items[i] as DataRowView).Row.ItemArray[4]).ToString() != "")
                                        {

                                            objbl1.Maidens = Convert.ToInt16((dgvbowl1.Items[i] as DataRowView).Row.ItemArray[4]); ;

                                        }


                                        if (!DBNull.Value.Equals((dgvbowl1.Items[i] as DataRowView).Row.ItemArray[5]) && ((dgvbowl1.Items[i] as DataRowView).Row.ItemArray[5]).ToString() != "")
                                        {
                                            int Runsgiven = Convert.ToInt16((dgvbowl1.Items[i] as DataRowView).Row.ItemArray[5]);
                                            objbl1.Runs = Convert.ToInt16((dgvbowl1.Items[i] as DataRowView).Row.ItemArray[5]);

                                            objbl1.Econ = Math.Round((Convert.ToDecimal(objbl1.Runs) / oversconvert(objbl1.Overs)), 1);

                                        }


                                        if (!DBNull.Value.Equals((dgvbowl1.Items[i] as DataRowView).Row.ItemArray[6]) && ((dgvbowl1.Items[i] as DataRowView).Row.ItemArray[6]).ToString() != "")
                                        {
                                            objbl1.Wickets = Convert.ToInt16((dgvbowl1.Items[i] as DataRowView).Row.ItemArray[6]);
                                        }


                                    }
                                    else
                                    {
                                        objbl1.Overs = 0;
                                        objbl1.Maidens = 0;
                                        objbl1.Runs = 0;
                                        objbl1.Econ = 0;
                                        objbl1.Wickets = 0;

                                    }


                                }

                                Database.SaveEntity<BestBowler>(objbl1, oleconn);
                                //  MessageBox.Show("bowling 1 added");
                                progressbar.Value = 50;
                                isaddedbl1 = true;

                            }

                        }
                    }
                }

            }

            // add              Bowling 2
            for (int i = 0; i <= (secondinngcount2 - 1); i++)
            {
                string playerkscauid = Convert.ToString((dgvbowl2.Items[i] as DataRowView).Row.ItemArray[2]);
                checkplayerbl2(playerkscauid);

                if (playerexitbl2 == false)
                {

                    if (!DBNull.Value.Equals((dgvbowl2.Items[i] as DataRowView).Row.ItemArray[1]) && ((dgvbowl2.Items[i] as DataRowView).Row.ItemArray[1]).ToString() != "")
                    {
                        string playername;
                        string id = Convert.ToString((dgvbowl2.Items[i] as DataRowView).Row.ItemArray[1]);
                        if (id.Contains("("))
                        {
                            playername = id.Substring(0, id.IndexOf("(") - 1);
                        }
                        else
                        {
                            playername = id;
                        }

                        foreach (Player obj in objplayer1)
                        {

                            if (obj.KSCAUID == playerkscauid)
                            {

                                BestBowler objbl2 = Database.GetNewEntity<BestBowler>();

                                //foreach (Season obcseason in objseason)
                                //{
                                //    if (obcseason.SeasonType.ToString() == lblseason.Content.ToString())
                                //    {
                                objbl2.SeasonId = selectedgridvalue._seasonid;

                                //    }
                                //}



                                //save Division Id
                                //foreach (Division obcdivision in objdivision)
                                //{
                                //    if (obcdivision.DivisionName.ToString() == lbldivision.Content.ToString())
                                //    {
                                objbl2.DivisionId = selectedgridvalue.LoadDivisionId;
                                //    }
                                //}


                                objbl2.PlayerName = obj.FirstName;
                                objbl2.PlayerName = playername;
                                objbl2.KSCAUID = obj.KSCAUID;

                                foreach (Team objteam in objteams)
                                {
                                    if (obj.TeamIdId.ToString() == objteam.TeamId.ToString())
                                    {
                                        objbl2.TeamName = objteam.TeamName.ToString();
                                    }
                                }

                                if (ismatchnotplayed == true)
                                {
                                    objbl2.Matches = 1;
                                    objbl2.Innings = 0;
                                }
                                else
                                {
                                    objbl2.Matches = 1;
                                    objbl2.Innings = 1;
                                    if (!DBNull.Value.Equals((dgvbowl2.Items[i] as DataRowView).Row.ItemArray[3]) && ((dgvbowl2.Items[i] as DataRowView).Row.ItemArray[3]).ToString() != "")
                                    {

                                        objbl2.Overs = Math.Round(Convert.ToDecimal((dgvbowl2.Items[i] as DataRowView).Row.ItemArray[3]), 1);

                                        if (!DBNull.Value.Equals((dgvbowl1.Items[i] as DataRowView).Row.ItemArray[4]) && ((dgvbowl2.Items[i] as DataRowView).Row.ItemArray[4]).ToString() != "")
                                        {

                                            objbl2.Maidens = Convert.ToInt16((dgvbowl2.Items[i] as DataRowView).Row.ItemArray[4]);

                                        }


                                        if (!DBNull.Value.Equals((dgvbowl2.Items[i] as DataRowView).Row.ItemArray[5]) && ((dgvbowl2.Items[i] as DataRowView).Row.ItemArray[5]).ToString() != "")
                                        {
                                            int Runsgiven = Convert.ToInt16((dgvbowl2.Items[i] as DataRowView).Row.ItemArray[5]);
                                            objbl2.Runs = Convert.ToInt16((dgvbowl2.Items[i] as DataRowView).Row.ItemArray[5]);


                                            objbl2.Econ = Math.Round((Convert.ToDecimal(objbl2.Runs) / oversconvert(objbl2.Overs)), 1);

                                        }


                                        if (!DBNull.Value.Equals((dgvbowl2.Items[i] as DataRowView).Row.ItemArray[6]) && ((dgvbowl2.Items[i] as DataRowView).Row.ItemArray[6]).ToString() != "")
                                        {
                                            objbl2.Wickets = Convert.ToInt16((dgvbowl2.Items[i] as DataRowView).Row.ItemArray[6]);
                                        }



                                    }
                                    else
                                    {
                                        objbl2.Overs = 0;
                                        objbl2.Maidens = 0;
                                        objbl2.Runs = 0;
                                        objbl2.Econ = 0;
                                        objbl2.Wickets = 0;
                                    }
                                }

                                Database.SaveEntity<BestBowler>(objbl2, oleconn);
                                //  MessageBox.Show("bowling 2 added");
                                progressbar.Value = 50;
                                isaddedbl2 = true;
                            }
                        }
                    }
                }

            }

            // update              Batting 1 .

            int batplayed11 = 0;
            if (isaddedbt1 == false)
            {
                for (int i = 0; i <= (firstinngcount1 - 1); i++)
                {

                    string playerkscauid = Convert.ToString((dgvbat1.Items[i] as DataRowView).Row.ItemArray[2]);
                    checkplayerbt1(playerkscauid);

                    if (playerexitbt1 == true)
                    {

                        if (!DBNull.Value.Equals((dgvbat1.Items[i] as DataRowView).Row.ItemArray[1]) && ((dgvbat1.Items[i] as DataRowView).Row.ItemArray[1]).ToString() != "")
                        {
                            string playername;
                            string id = Convert.ToString((dgvbat1.Items[i] as DataRowView).Row.ItemArray[1]);
                            if (id.Contains("("))
                            {
                                playername = id.Substring(0, id.IndexOf("(") - 1);
                            }
                            else
                            {
                                playername = id;
                            }

                            //foreach (BestBatsman obj in objplayer1)
                            //{
                            //   // string playerid = obj.PlayerId.ToString();



                            foreach (BestBatsman objbt1 in objbat1)
                            {
                                if (objbt1.KSCAUID == playerkscauid)
                                {

                                    if (ismatchnotplayed == true)
                                    {
                                        objbt1.Matches++;
                                        objbt1.Innings = objbt1.Innings;
                                    }
                                    else
                                    {

                                        objbt1.Matches++;
                                        objbt1.Innings++;
                                        if (!DBNull.Value.Equals((dgvbat1.Items[i] as DataRowView).Row.ItemArray[3]) && ((dgvbat1.Items[i] as DataRowView).Row.ItemArray[3]).ToString() != "")
                                        {
                                            if (Convert.ToString((dgvbat1.Items[i] as DataRowView).Row.ItemArray[3]) == "NOT OUT" || Convert.ToString((dgvbat1.Items[i] as DataRowView).Row.ItemArray[3]) == "not out" || Convert.ToString((dgvbat1.Items[i] as DataRowView).Row.ItemArray[3]) == "n.o" || Convert.ToString((dgvbat1.Items[i] as DataRowView).Row.ItemArray[3]) == "N.O" || Convert.ToString((dgvbat1.Items[i] as DataRowView).Row.ItemArray[3]) == "no" || Convert.ToString((dgvbat1.Items[i] as DataRowView).Row.ItemArray[3]) == "NO")
                                            {
                                                objbt1.NotOut++;
                                            }
                                        }

                                        //     if (!DBNull.Value.Equals((dgvbat2.Items[i] as DataRowView).Row.ItemArray[4]) && ((dgvbat2.Items[i] as DataRowView).Row.ItemArray[4]).ToString() != "")
                                        //{
                                        //    if ((Convert.ToString((dgvbat2.Items[i] as DataRowView).Row.ItemArray[6]) != "") && (Convert.ToInt16((dgvbat2.Items[i] as DataRowView).Row.ItemArray[6]) != 0))
                                        //    {



                                        if (!DBNull.Value.Equals((dgvbat1.Items[i] as DataRowView).Row.ItemArray[4]) && ((dgvbat1.Items[i] as DataRowView).Row.ItemArray[4]).ToString() != "")
                                        {
                                            if ((Convert.ToString((dgvbat1.Items[i] as DataRowView).Row.ItemArray[6]) != "") && (Convert.ToInt16((dgvbat1.Items[i] as DataRowView).Row.ItemArray[6]) != 0))
                                            {
                                                objbt1.RunsScored = Convert.ToInt16(objbt1.RunsScored + Convert.ToInt16((dgvbat1.Items[i] as DataRowView).Row.ItemArray[4]));
                                                objbt1.BallsFaced = Convert.ToInt16(objbt1.BallsFaced + Convert.ToInt16((dgvbat1.Items[i] as DataRowView).Row.ItemArray[6]));

                                                if (objbt1.HighestScore < (Convert.ToInt16((dgvbat1.Items[i] as DataRowView).Row.ItemArray[4])))
                                                {
                                                    objbt1.HighestScore = Convert.ToInt16((dgvbat1.Items[i] as DataRowView).Row.ItemArray[4]);
                                                }
                                                objbt1.StrikeRate = (Convert.ToDecimal(objbt1.RunsScored) / Convert.ToDecimal(objbt1.BallsFaced)) * 100;
                                            }

                                            if (objbt1.RunsScored >= 50 && objbt1.RunsScored <= 99)
                                            {
                                                objbt1.Fifties++;
                                            }

                                            if (objbt1.RunsScored >= 100)
                                            {
                                                objbt1.Hundreds++;
                                            }


                                            if (!DBNull.Value.Equals((dgvbat1.Items[i] as DataRowView).Row.ItemArray[7]) && ((dgvbat1.Items[i] as DataRowView).Row.ItemArray[7]).ToString() != "")
                                            {
                                                objbt1.Fours = Convert.ToInt16(Convert.ToInt16(objbt1.Fours) + Convert.ToInt16((dgvbat1.Items[i] as DataRowView).Row.ItemArray[7]));
                                            }


                                            if (!DBNull.Value.Equals((dgvbat1.Items[i] as DataRowView).Row.ItemArray[8]) && ((dgvbat1.Items[i] as DataRowView).Row.ItemArray[8]).ToString() != "")
                                            {
                                                objbt1.Sixes = Convert.ToInt16(Convert.ToInt16(objbt1.Sixes) + Convert.ToInt16((dgvbat1.Items[i] as DataRowView).Row.ItemArray[8]));
                                            }

                                        }
                                    }
                                    Database.SaveEntity<BestBatsman>(objbt1, oleconn);
                                    //   MessageBox.Show("batting 1 updated");
                                    progressbar.Value = 50;


                                }

                            }
                            //}
                        }
                    }
                }
            }
            // update              Batting 2
            int batplayed12 = 0;
            if (isaddedbt2 == false)
            {
                for (int i = 0; i <= (firstinngcount1 - 1); i++)
                {

                    string playerkscauid = Convert.ToString((dgvbat2.Items[i] as DataRowView).Row.ItemArray[2]);
                    checkplayerbt2(playerkscauid);

                    if (playerexitbt2 == true)
                    {

                        if (!DBNull.Value.Equals((dgvbat2.Items[i] as DataRowView).Row.ItemArray[1]) && ((dgvbat2.Items[i] as DataRowView).Row.ItemArray[1]).ToString() != "")
                        {
                            string playername;
                            string id = Convert.ToString((dgvbat2.Items[i] as DataRowView).Row.ItemArray[1]);
                            if (id.Contains("("))
                            {
                                playername = id.Substring(0, id.IndexOf("(") - 1);
                            }
                            else
                            {
                                playername = id;
                            }

                            //foreach (Player obj2 in objplayer1)
                            //{

                            //    string playerid = obj2.PlayerId.ToString();



                            foreach (BestBatsman objbt2 in objbat1)
                            {

                                if (objbt2.KSCAUID == playerkscauid)
                                {
                                    if (ismatchnotplayed == true)
                                    {
                                        objbt2.Matches++;
                                        objbt2.Innings = objbt2.Innings;
                                    }
                                    else
                                    {
                                        objbt2.Matches++;
                                        objbt2.Innings++;

                                        if (!DBNull.Value.Equals((dgvbat2.Items[i] as DataRowView).Row.ItemArray[3]) && ((dgvbat2.Items[i] as DataRowView).Row.ItemArray[3]).ToString() != "")
                                        {
                                            if (Convert.ToString((dgvbat2.Items[i] as DataRowView).Row.ItemArray[3]) == "NOT OUT" || Convert.ToString((dgvbat2.Items[i] as DataRowView).Row.ItemArray[3]) == "not out" || Convert.ToString((dgvbat2.Items[i] as DataRowView).Row.ItemArray[3]) == "n.o" || Convert.ToString((dgvbat2.Items[i] as DataRowView).Row.ItemArray[3]) == "N.O" || Convert.ToString((dgvbat2.Items[i] as DataRowView).Row.ItemArray[3]) == "no" || Convert.ToString((dgvbat2.Items[i] as DataRowView).Row.ItemArray[3]) == "NO")
                                            {
                                                objbt2.NotOut++;
                                            }
                                        }


                                        if (!DBNull.Value.Equals((dgvbat2.Items[i] as DataRowView).Row.ItemArray[4]) && ((dgvbat2.Items[i] as DataRowView).Row.ItemArray[4]).ToString() != "")
                                        {
                                            if ((Convert.ToString((dgvbat2.Items[i] as DataRowView).Row.ItemArray[6]) != "") && (Convert.ToInt16((dgvbat2.Items[i] as DataRowView).Row.ItemArray[6]) != 0))
                                            {
                                                objbt2.RunsScored = Convert.ToInt16(objbt2.RunsScored + Convert.ToInt16((dgvbat2.Items[i] as DataRowView).Row.ItemArray[4]));
                                                objbt2.BallsFaced = Convert.ToInt16(objbt2.BallsFaced + Convert.ToInt16((dgvbat2.Items[i] as DataRowView).Row.ItemArray[6]));

                                                if (objbt2.HighestScore < (Convert.ToInt16((dgvbat2.Items[i] as DataRowView).Row.ItemArray[4])))
                                                {
                                                    objbt2.HighestScore = Convert.ToInt16((dgvbat2.Items[i] as DataRowView).Row.ItemArray[4]);
                                                }

                                                objbt2.StrikeRate = (Convert.ToDecimal(objbt2.RunsScored) / Convert.ToDecimal(objbt2.BallsFaced)) * 100;
                                            }
                                            if (objbt2.RunsScored >= 50 && objbt2.RunsScored <= 99)
                                            {
                                                objbt2.Fifties++;
                                            }

                                            if (objbt2.RunsScored >= 100)
                                            {
                                                objbt2.Hundreds++;
                                            }


                                            if (!DBNull.Value.Equals((dgvbat2.Items[i] as DataRowView).Row.ItemArray[7]) && ((dgvbat2.Items[i] as DataRowView).Row.ItemArray[7]).ToString() != "")
                                            {
                                                objbt2.Fours = Convert.ToInt16(Convert.ToInt16(objbt2.Fours) + Convert.ToInt16((dgvbat2.Items[i] as DataRowView).Row.ItemArray[7]));
                                            }


                                            if (!DBNull.Value.Equals((dgvbat2.Items[i] as DataRowView).Row.ItemArray[8]) && ((dgvbat2.Items[i] as DataRowView).Row.ItemArray[8]).ToString() != "")
                                            {
                                                objbt2.Sixes = Convert.ToInt16(Convert.ToInt16(objbt2.Sixes) + Convert.ToInt16((dgvbat2.Items[i] as DataRowView).Row.ItemArray[8]));
                                            }

                                        }
                                    }
                                    Database.SaveEntity<BestBatsman>(objbt2, oleconn);
                                    //  MessageBox.Show("batting 2 updated");
                                    progressbar.Value = 50;


                                }
                            }
                            // }
                        }
                    }
                }
            }

            // update              Bowling 1

            TeamFound = false;
            if (isaddedbl1 == false)
            {
                for (int i = 0; i <= (firstinngcount2 - 1); i++)
                {

                    string playerkscauid = Convert.ToString((dgvbowl1.Items[i] as DataRowView).Row.ItemArray[2]);
                    checkplayerbl1(playerkscauid);

                    if (playerexitbl1 == true)
                    {

                        if (!DBNull.Value.Equals((dgvbowl1.Items[i] as DataRowView).Row.ItemArray[1]) && ((dgvbowl1.Items[i] as DataRowView).Row.ItemArray[1]).ToString() != "")
                        {
                            string playername;
                            string id = Convert.ToString((dgvbowl1.Items[i] as DataRowView).Row.ItemArray[1]);
                            if (id.Contains("("))
                            {
                                playername = id.Substring(0, id.IndexOf("(") - 1);
                            }
                            else
                            {
                                playername = id;
                            }

                            //foreach (Player obj2 in objplayer1)
                            //{
                            //    string playerid = obj2.PlayerId.ToString();


                            foreach (BestBowler objbl1 in objbowl1)
                            {
                                if (objbl1.KSCAUID == playerkscauid)
                                {
                                    if (ismatchnotplayed == true)
                                    {
                                        objbl1.Matches++;
                                        objbl1.Innings = objbl1.Innings;
                                    }
                                    else
                                    {
                                        objbl1.Matches++;
                                        objbl1.Innings++;

                                        if (!DBNull.Value.Equals((dgvbowl1.Items[i] as DataRowView).Row.ItemArray[3]) && ((dgvbowl1.Items[i] as DataRowView).Row.ItemArray[3]).ToString() != "")
                                        {
                                            objbl1.Overs = Math.Round(overscalculation(objbl1.Overs, Convert.ToDecimal((dgvbowl1.Items[i] as DataRowView).Row.ItemArray[3])), 1);

                                            if (!DBNull.Value.Equals((dgvbowl1.Items[i] as DataRowView).Row.ItemArray[4]) && ((dgvbowl1.Items[i] as DataRowView).Row.ItemArray[4]).ToString() != "")
                                            {
                                                objbl1.Maidens = Convert.ToInt16(objbl1.Maidens + Convert.ToInt16((dgvbowl1.Items[i] as DataRowView).Row.ItemArray[4]));
                                            }

                                        }

                                        if (!DBNull.Value.Equals((dgvbowl1.Items[i] as DataRowView).Row.ItemArray[5]) && ((dgvbowl1.Items[i] as DataRowView).Row.ItemArray[5]).ToString() != "")
                                        {

                                            objbl1.Runs = Convert.ToInt16(Convert.ToInt16(objbl1.Runs) + Convert.ToInt16((dgvbowl1.Items[i] as DataRowView).Row.ItemArray[5]));
                                        }

                                        if (!DBNull.Value.Equals((dgvbowl1.Items[i] as DataRowView).Row.ItemArray[6]) && ((dgvbowl1.Items[i] as DataRowView).Row.ItemArray[6]).ToString() != "")
                                        {
                                            objbl1.Wickets = Convert.ToInt16(Convert.ToInt16(objbl1.Wickets) + Convert.ToInt16((dgvbowl1.Items[i] as DataRowView).Row.ItemArray[6]));
                                        }

                                        if (!DBNull.Value.Equals((dgvbowl1.Items[i] as DataRowView).Row.ItemArray[3]) && ((dgvbowl1.Items[i] as DataRowView).Row.ItemArray[3]).ToString() != "")
                                        {

                                            objbl1.Econ = Math.Round((Convert.ToDecimal(objbl1.Runs) / oversconvert(objbl1.Overs)), 1);

                                        }
                                    }

                                    Database.SaveEntity<BestBowler>(objbl1, oleconn);
                                    //    MessageBox.Show("bowling 1 updated");
                                    progressbar.Value = 50;

                                }
                            }
                            // }
                        }
                    }
                }
            }
            // update              Bowling 2
            if (isaddedbl2 == false)
            {
                for (int i = 0; i <= (secondinngcount2 - 1); i++)
                {

                    string playerkscauid = Convert.ToString((dgvbowl2.Items[i] as DataRowView).Row.ItemArray[2]);
                    checkplayerbl2(playerkscauid);

                    if (playerexitbl2 == true)
                    {

                        if (!DBNull.Value.Equals((dgvbowl2.Items[i] as DataRowView).Row.ItemArray[1]) && ((dgvbowl2.Items[i] as DataRowView).Row.ItemArray[1]).ToString() != "")
                        {
                            string playername;
                            string id = Convert.ToString((dgvbowl2.Items[i] as DataRowView).Row.ItemArray[1]);
                            if (id.Contains("("))
                            {
                                playername = id.Substring(0, id.IndexOf("(") - 1);
                            }
                            else
                            {
                                playername = id;
                            }

                            //foreach (Player obj2 in objplayer1)
                            //{

                            //string playerid = obj2.PlayerId.ToString();


                            foreach (BestBowler objbl2 in objbowl1)
                            {
                                if (objbl2.KSCAUID == playerkscauid)
                                {
                                    if (ismatchnotplayed == true)
                                    {
                                        objbl2.Matches++;
                                        objbl2.Innings = objbl2.Innings;
                                    }
                                    else
                                    {
                                        objbl2.Matches++;
                                        objbl2.Innings++;
                                        int prevruns = objbl2.Runs;

                                        if (!DBNull.Value.Equals((dgvbowl2.Items[i] as DataRowView).Row.ItemArray[3]) && ((dgvbowl2.Items[i] as DataRowView).Row.ItemArray[3]).ToString() != "")
                                        {
                                            objbl2.Overs = Math.Round(overscalculation(objbl2.Overs, Convert.ToDecimal((dgvbowl2.Items[i] as DataRowView).Row.ItemArray[3])), 1);

                                            if (!DBNull.Value.Equals((dgvbowl2.Items[i] as DataRowView).Row.ItemArray[4]) && ((dgvbowl2.Items[i] as DataRowView).Row.ItemArray[4]).ToString() != "")
                                            {
                                                objbl2.Maidens = Convert.ToInt16(objbl2.Maidens + Convert.ToInt16((dgvbowl2.Items[i] as DataRowView).Row.ItemArray[4]));
                                            }

                                        }

                                        if (!DBNull.Value.Equals((dgvbowl2.Items[i] as DataRowView).Row.ItemArray[5]) && ((dgvbowl2.Items[i] as DataRowView).Row.ItemArray[5]).ToString() != "")
                                        {

                                            objbl2.Runs = Convert.ToInt16(Convert.ToInt16(objbl2.Runs) + Convert.ToInt16((dgvbowl2.Items[i] as DataRowView).Row.ItemArray[5]));
                                        }

                                        if (!DBNull.Value.Equals((dgvbowl2.Items[i] as DataRowView).Row.ItemArray[6]) && ((dgvbowl2.Items[i] as DataRowView).Row.ItemArray[6]).ToString() != "")
                                        {
                                            objbl2.Wickets = Convert.ToInt16(Convert.ToInt16(objbl2.Wickets) + Convert.ToInt16((dgvbowl2.Items[i] as DataRowView).Row.ItemArray[6]));
                                        }

                                        if (!DBNull.Value.Equals((dgvbowl2.Items[i] as DataRowView).Row.ItemArray[3]) && ((dgvbowl2.Items[i] as DataRowView).Row.ItemArray[3]).ToString() != "")
                                        {

                                            objbl2.Econ = Math.Round((Convert.ToDecimal(objbl2.Runs) / oversconvert(objbl2.Overs)), 1);
                                        }

                                    }
                                    Database.SaveEntity<BestBowler>(objbl2, oleconn);
                                    //  MessageBox.Show("bowling 2 updated");
                                    progressbar.Value = 50;

                                }
                            }
                            //}
                        }
                    }
                }
            }

            /////////////////

            ScoreCardDetails scorecardobj = Database.GetNewEntity<ScoreCardDetails>();

            scorecardobj.MatchId = selectedgridvalue.matchid;
            scorecardobj.Group = selectedgridvalue.group;

            scorecardobj.TossWon = tosswon.team;
            scorecardobj.ElectedTo = electedto.batball;

            scorecardobj.TeamACaptain = captainteam1.captain1[0];
            scorecardobj.TeamAKeeper = keeperteam1.keeper1[0];

            scorecardobj.TeamBCaptain = captainteam2.captain2[0];
            scorecardobj.TeamBKeeper = keeperteam2.keeper2[0];

            //timeA();
            //timeB();


            scorecardobj.CommenceTimeA = start_time1.Text;
            scorecardobj.EndTimeA = close_time1.Text;
            scorecardobj.DurationA = Convert.ToInt16(txtDuration1.Text);
            scorecardobj.ByesA = Convert.ToInt16(txtbyes1.Text);
            scorecardobj.LegByesA = Convert.ToInt16(txtlegbyes1.Text);
            scorecardobj.PenaltyA = Convert.ToInt16(txtpenalty1.Text);
            scorecardobj.TotalExtrasA = Convert.ToInt16(txttotalextras1.Text);
            scorecardobj.TotalRunsA = Convert.ToInt16(txtruns1.Text);
            scorecardobj.TotalWicketsA = Convert.ToInt16(txtwickets1.Text);
            scorecardobj.TotalOversA = Convert.ToDecimal(txtovers1.Text);
            scorecardobj.runoutA = Convert.ToInt16(txtrunout1.Text);


            scorecardobj.CommenceTimeB = start_time2.Text;
            scorecardobj.EndTimeB = close_time2.Text;
            scorecardobj.DurationB = Convert.ToInt16(txtDuration2.Text);
            scorecardobj.ByesB = Convert.ToInt16(txtbyes2.Text);
            scorecardobj.LegByesB = Convert.ToInt16(txtlegbyes2.Text);
            scorecardobj.PenaltyB = Convert.ToInt16(txtpenalty2.Text);
            scorecardobj.TotalExtrasB = Convert.ToInt16(txttotalextras2.Text);
            scorecardobj.TotalRunsB = Convert.ToInt16(txtruns2.Text);
            scorecardobj.TotalWicketsB = Convert.ToInt16(txtwickets2.Text);
            scorecardobj.TotalOversB = Convert.ToDecimal(txtovers2.Text);
            scorecardobj.runoutB = Convert.ToInt16(txtrunout2.Text);


            scorecardobj.TeamAResult = TeamA.Result;
            scorecardobj.TeamBResult = TeamB.Result;
            scorecardobj.TeamAPoints = Convert.ToInt16(TeamA.Points);
            scorecardobj.TeamBPoints = Convert.ToInt16(TeamB.Points);
            scorecardobj.Result = Match.Result;
            scorecardobj.Remarks = Remark.Remarks;
            scorecardobj.Umpireone = NameOf.Umpire1;
            scorecardobj.Umpiretwo = NameOf.Umpire2;
            scorecardobj.Scorer = NameOf.Scorer;


            //foreach (Season obcseason in objseason)
            //{

            //    if (obcseason.SeasonType.ToString() == lblseason.Content.ToString())
            //    {
            scorecardobj.SeasonId = selectedgridvalue._seasonid;

            //    }

            //}



            //save Zone Id
            //foreach (Zone obczone in objzone)
            //{
            //    if (obczone.ZoneName.ToString() == lblzone.Content.ToString())
            //    {
            scorecardobj.ZoneId = TempValues.LoadZoneId;

            //    }
            //}

            //save Division Id
            //foreach (Division obcdivision in objdivision)
            //{
            //    if (obcdivision.DivisionName.ToString() == lbldivision.Content.ToString())
            //    {
            scorecardobj.DivisionId = selectedgridvalue.LoadDivisionId;
            //    }
            //}

            //team names
            scorecardobj.TeamA = lblbat1.Content.ToString();
            scorecardobj.TeamB = lblbowl1.Content.ToString();
            string abcd = scorecardobj.ScoreCardDetailsId.ToString();


            ///////////////////////
            foreach (DataRowView objmatch in dt.DefaultView)
            {
                MatchDetails obj1 = Database.GetNewEntity<MatchDetails>();

                Match._MatchScoreId = obj1.MatchDetailsId.ToString();
                obj1.ScoreCardID = abcd;

                obj1.SerialNoA = Convert.ToInt16(objmatch["SLNOA"].ToString());
                obj1.PlayersA = objmatch["Name Of PlayersA"].ToString();
                obj1.KSCAUIDA = objmatch["KSCA UIDA"].ToString();
                obj1.DismissalA = objmatch["DismissalA"].ToString();

                if (objmatch["RunsA"].ToString() == "")
                {
                    obj1.BattingRunsA = 0;
                }
                else
                {
                    obj1.BattingRunsA = Convert.ToInt16(objmatch["RunsA"].ToString());
                }

                if (objmatch["MinsA"].ToString() == "")
                {
                    obj1.MinsA = 0;
                }
                else
                {
                    obj1.MinsA = Convert.ToInt16(objmatch["MinsA"].ToString());
                }

                if (objmatch["BallsA"].ToString() == "")
                {
                    obj1.BallsA = 0;

                }
                else
                {
                    obj1.BallsA = Convert.ToInt16(objmatch["BallsA"].ToString());
                }

                if (objmatch["FoursA"].ToString() == "")
                {
                    obj1.FoursA = 0;
                }
                else
                {
                    obj1.FoursA = Convert.ToInt16(objmatch["FoursA"].ToString());
                }

                if (objmatch["SixesA"].ToString() == "")
                {
                    obj1.SixesA = 0;
                }
                else
                {
                    obj1.SixesA = Convert.ToInt16(objmatch["SixesA"].ToString());
                }


                if (objmatch["OversA"].ToString() == "")
                {
                    obj1.OversA = 0;
                }
                else
                {
                    obj1.OversA = Math.Round(Convert.ToDecimal(objmatch["OversA"].ToString()), 1);
                }

                if (objmatch["MaidensA"].ToString() == "")
                {
                    obj1.MaidensA = 0;
                }
                else
                {
                    obj1.MaidensA = Convert.ToInt16(objmatch["MaidensA"].ToString());
                }


                if (objmatch["BrunsA"].ToString() == "")
                {
                    obj1.BowlingRunsA = 0;
                }
                else
                {
                    obj1.BowlingRunsA = Convert.ToInt16(objmatch["BrunsA"].ToString());
                }

                if (objmatch["WicketsA"].ToString() == "")
                {
                    obj1.WicketsA = 0;
                }
                else
                {
                    obj1.WicketsA = Convert.ToInt16(objmatch["WicketsA"].ToString());
                }

                if (objmatch["No BallA"].ToString() == "")
                {
                    obj1.NoBallsA = 0;
                }
                else
                {
                    obj1.NoBallsA = Convert.ToInt16(objmatch["No BallA"].ToString());
                }

                if (objmatch["WideA"].ToString() == "")
                {
                    obj1.WidesA = 0;
                }
                else
                {
                    obj1.WidesA = Convert.ToInt16(objmatch["WideA"].ToString());
                }

                if (objmatch["AvgA"].ToString() == "")
                {
                    obj1.AverageA = 0;
                }
                else
                {
                    obj1.AverageA = Convert.ToInt16(objmatch["AvgA"].ToString());
                }


                obj1.SerialNoB = Convert.ToInt16(objmatch["SLNOB"].ToString());
                obj1.PlayersB = objmatch["Name Of PlayersB"].ToString();
                obj1.KSCAUIDB = objmatch["KSCA UIDB"].ToString();
                obj1.DismissalB = objmatch["DismissalB"].ToString();

                if (objmatch["RunsB"].ToString() == "")
                {
                    obj1.BattingRunsB = 0;
                }
                else
                {
                    obj1.BattingRunsB = Convert.ToInt16(objmatch["RunsB"].ToString());
                }

                if (objmatch["MinsB"].ToString() == "")
                {
                    obj1.MinsB = 0;
                }
                else
                {
                    obj1.MinsB = Convert.ToInt16(objmatch["MinsB"].ToString());
                }
                if (objmatch["BallsB"].ToString() == "")
                {
                    obj1.BallsB = 0;

                }
                else
                {
                    obj1.BallsB = Convert.ToInt16(objmatch["BallsB"].ToString());
                }

                if (objmatch["FoursB"].ToString() == "")
                {
                    obj1.FoursB = 0;
                }
                else
                {
                    obj1.FoursB = Convert.ToInt16(objmatch["FoursB"].ToString());
                }

                if (objmatch["SixesB"].ToString() == "")
                {
                    obj1.SixesB = 0;
                }
                else
                {
                    obj1.SixesB = Convert.ToInt16(objmatch["SixesB"].ToString());
                }


                if (objmatch["OversB"].ToString() == "")
                {
                    obj1.OversB = 0;
                }
                else
                {
                    obj1.OversB = Math.Round(Convert.ToDecimal(objmatch["OversB"].ToString()), 1);
                }

                if (objmatch["MaidensB"].ToString() == "")
                {
                    obj1.MaidensB = 0;
                }
                else
                {
                    obj1.MaidensB = Convert.ToInt16(objmatch["MaidensB"].ToString());
                }


                if (objmatch["BrunsB"].ToString() == "")
                {
                    obj1.BowlingRunsB = 0;
                }
                else
                {
                    obj1.BowlingRunsB = Convert.ToInt16(objmatch["BrunsB"].ToString());
                }

                if (objmatch["WicketsB"].ToString() == "")
                {
                    obj1.WicketsB = 0;
                }
                else
                {
                    obj1.WicketsB = Convert.ToInt16(objmatch["WicketsB"].ToString());
                }

                if (objmatch["No BallB"].ToString() == "")
                {
                    obj1.NoBallsB = 0;
                }
                else
                {
                    obj1.NoBallsB = Convert.ToInt16(objmatch["No BallB"].ToString());
                }

                if (objmatch["WideB"].ToString() == "")
                {
                    obj1.WidesB = 0;
                }
                else
                {
                    obj1.WidesB = Convert.ToInt16(objmatch["WideB"].ToString());
                }

                if (objmatch["AvgB"].ToString() == "")
                {
                    obj1.AverageB = 0;
                }
                else
                {
                    obj1.AverageB = Convert.ToInt16(objmatch["AvgB"].ToString());
                }

                obj1.MatchId = selectedgridvalue.matchid;
                Database.SaveEntity<MatchDetails>(obj1, oleconn);
                obj1 = null;
            }
            progressbar.Value = 75;

            Database.SaveEntity<ScoreCardDetails>(scorecardobj, oleconn);
            Database.SaveEntity<TeamReportTable>(ObjTeamReport1, oleconn);
            Database.SaveEntity<TeamReportTable>(ObjTeamReport2, oleconn);
            Database.SaveEntity<LocationReportTable>(ObjLocationReport, oleconn);

            MessageBox.Show("Saved");


            //clear
            dt.Rows.Clear();
            start_time1.Clear();
            close_time1.Clear();
            txtDuration1.Text = string.Empty;
            txtbyes1.Text = string.Empty;
            txtlegbyes1.Text = string.Empty;
            txtpenalty1.Text = string.Empty;
            txttotalextras1.Text = string.Empty;
            txtruns1.Text = string.Empty;
            txtwickets1.Text = string.Empty;
            txtovers1.Text = string.Empty;
            txtrunout1.Text = string.Empty;

            start_time2.Clear();
            close_time2.Clear();
            txtDuration2.Text = string.Empty;
            txtbyes2.Text = string.Empty;
            txtlegbyes2.Text = string.Empty;
            txtpenalty2.Text = string.Empty;
            txttotalextras2.Text = string.Empty;
            txtruns2.Text = string.Empty;
            txtwickets2.Text = string.Empty;
            txtovers2.Text = string.Empty;
            txtrunout2.Text = string.Empty;

            lblbat1.Content = null;
            lblbat2.Content = null;
            lblbowl1.Content = null;
            lblbowl2.Content = null;
            lbldivision.Content = null;
            lblseason.Content = null;
            lblzone.Content = null;


            Button btn = (Button)sender;
            btn.Command = NavigationCommands.GoToPage;
            btn.CommandParameter = "/View/formFixtures.xaml";

            _isResultclick = true;
            _isSaveClick = true;
            _IsClickedOnce = true;

            if (_isSaveClick == true)
            {

                _isResultclick = false;
                _isSaveClick = false;
                _IsClickedOnce = false;



                dgvbat1.DataContext = null;
                dgvbat2.DataContext = null;
                dgvbowl1.DataContext = null;
                dgvbowl2.DataContext = null;

                temp.enddate = string.Empty;
                temp.matchtype = string.Empty;
                temp.stratdate = string.Empty;
                tempvalues.teamA = null;
                tempvalues.teamB = null;
                TempValues.index = 0;
                TempValues.LoadSeason = string.Empty;
                TempValues.LoadSeasonId = string.Empty;
                TempValues.LoadZone = string.Empty;
                TempValues.SeasonName = string.Empty;
                TempValues.teamname = null;

                KSCA.team1 = new string[60];
                KSCA.team2 = new string[60];
                selectedgridvalue._fixtureid = string.Empty;
                selectedgridvalue._seasonid = string.Empty;
                selectedgridvalue.group = string.Empty;
                selectedgridvalue.LoadDivision = string.Empty;
                selectedgridvalue.LoadDivisionId = string.Empty;
                selectedgridvalue.matchid = string.Empty;
                selectedgridvalue.selectedstring = string.Empty;
                TempValuesTeam1.team1 = new string[60];
                TempValuesTeam2.team2 = new string[60];
                TempValuesTeam1.index1 = 0;

                comboboxvalue.value = new string[60];
                comboboxvalue.count = 0;
                TempValuesTeam2.index2 = 0;

                KSCAUID.teamA = new string[60];
                KSCAUID.teamB = new string[60];
                KSCAUID.countA = 0;
                KSCAUID.countB = 0;
                progressbar.Value = 100;
                progressbar.IsIndeterminate = false;
                btnsave1.IsEnabled = false;
                Result.IsEnabled = false;

                dgvbat1.ItemsSource = null;
                dgvbat2.ItemsSource = null;
                dgvbowl1.ItemsSource = null;
                dgvbowl2.ItemsSource = null;


                temp.enddate = string.Empty;
                temp.matchtype = string.Empty;
                temp.stratdate = string.Empty;
                tempvalues.teamA = null;
                tempvalues.teamB = null;
                TempValues.index = 0;
                TempValues.LoadSeason = string.Empty;
                TempValues.LoadSeasonId = string.Empty;
                TempValues.LoadZone = string.Empty;
                TempValues.SeasonName = string.Empty;
                TempValues.teamname = null;
                team.name = new string[60];
                team.count = 0;
                KSCA.count1 = 0;
                KSCA.count2 = 0;

                selectedgridvalue._fixtureid = string.Empty;
                selectedgridvalue._seasonid = string.Empty;
                selectedgridvalue.group = string.Empty;
                selectedgridvalue.LoadDivision = string.Empty;
                selectedgridvalue.LoadDivisionId = string.Empty;
                selectedgridvalue.matchid = string.Empty;
                selectedgridvalue.selectedstring = string.Empty;

                //objseason.Clear();
                //objzone.Clear();
                //objdivision.Clear();
                objteams.Clear();
                objlocationnames.Clear();
                objseconddiv.Clear();
                objplayer1.Clear();
                objbat1.Clear();
                objbowl1.Clear();



            }


        }

        bool playerexitbt1 = false;
        bool playerexitbl1 = false;
        bool playerexitbt2 = false;
        bool playerexitbl2 = false;

        public bool checkplayerbt1(string playerkscauid)
        {
            try
            {
                ObservableCollection<BestBatsman> batsman = Database.GetEntityList<BestBatsman>(false, false, false, oleconn, "RecordStatus", "KSCAUID");

                foreach (var item in batsman)
                {
                    if (item.KSCAUID == playerkscauid)
                    {
                        playerexitbt1 = true;
                        return true;
                    }

                    else
                    {
                        playerexitbt1 = false;
                    }
                }
                batsman.Clear();
                return false;


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool checkplayerbl1(string playerkscauid)
        {
            try
            {
                ObservableCollection<BestBowler> bowler = Database.GetEntityList<BestBowler>(false, false, false, oleconn, "RecordStatus", "KSCAUID");
                foreach (var item in bowler)
                {
                    if (item.KSCAUID == playerkscauid)
                    {
                        playerexitbl1 = true;
                        return true;
                    }

                    else
                    {
                        playerexitbl1 = false;
                    }
                }
                bowler.Clear();
                return false;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool checkplayerbt2(string playerkscauid)
        {
            try
            {
                ObservableCollection<BestBatsman> batsman = Database.GetEntityList<BestBatsman>(false, false, false, oleconn, "RecordStatus", "KSCAUID");
                foreach (var item in batsman)
                {
                    if (item.KSCAUID == playerkscauid)
                    {
                        playerexitbt2 = true;
                        return true;
                    }

                    else
                    {
                        playerexitbt2 = false;
                    }
                }
                batsman.Clear();
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public bool checkplayerbl2(string playerkscauid)
        {
            try
            {
                ObservableCollection<BestBowler> bowler = Database.GetEntityList<BestBowler>(false, false, false, oleconn, "RecordStatus", "KSCAUID");
                foreach (var item in bowler)
                {
                    if (item.KSCAUID == playerkscauid)
                    {
                        playerexitbl2 = true;
                        return true;
                    }

                    else
                    {
                        playerexitbl2 = false;
                    }
                }
                bowler.Clear();
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            progressbar.Visibility = Visibility.Visible;
            progressbar.Value = 0;
            ObservableCollection<ScoreCardDetails> lstscorecard = Database.GetEntityList<ScoreCardDetails>(false, false, false, oleconn, "RecordStatus", "ScoreCardDetailsId");
            ObservableCollection<MatchDetails> lstMatchDetails = Database.GetEntityList<MatchDetails>(false, false, false, oleconn, "RecordStatus", "MatchDetailsId");

            var objscorecard = from s in lstscorecard where s.MatchId == selectedgridvalue.matchid && s.DivisionId.ToString() == selectedgridvalue.LoadDivisionId && s.SeasonId.ToString() == TempValues.LoadSeasonId select s;
            List<ScoreCardDetails> lstscorecardfilter = objscorecard.ToList<ScoreCardDetails>();

            //var result = lstMatchDetails.Where(u => lstscorecardfilter.All(p => p.ScoreCardDetailsId.ToString() == u.ScoreCardID.ToString() && p.MatchId== selectedgridvalue.matchid));
            //List<MatchDetails> lstMatchDetailsSort = result.ToList<MatchDetails>();

            var result = from s in lstMatchDetails where s.MatchId == selectedgridvalue.matchid select s;
            List<MatchDetails> lstMatchDetailsSort = result.ToList<MatchDetails>();

            lblseason.Content = TempValues.LoadSeason;
            lblzone.Content = TempValues.LoadZone;
            lbldivision.Content = selectedgridvalue.LoadDivision;


            if (lstMatchDetailsSort.Count == 0)
            {
                if (_isResultclick == false)
                {
                    teamA.Clear();
                    teamB.Clear();
                    //////////////////////////
                    btnsave1.IsEnabled = true;
                    Result.IsEnabled = true;

                    btn1score.IsEnabled = true;
                    btn2score.IsEnabled = true;
                    start_time1.IsEnabled = true;
                    start_time2.IsEnabled = true;
                    close_time1.IsEnabled = true;
                    close_time2.IsEnabled = true;
                    txtbyes1.IsEnabled = true;
                    txtbyes2.IsEnabled = true;
                    txtDuration1.IsEnabled = true;
                    txtDuration2.IsEnabled = true;
                    txtlegbyes1.IsEnabled = true;
                    txtlegbyes2.IsEnabled = true;
                    txtovers1.IsEnabled = true;
                    txtovers2.IsEnabled = true;
                    txtpenalty1.IsEnabled = true;
                    txtpenalty2.IsEnabled = true;
                    txtruns1.IsEnabled = true;
                    txtruns2.IsEnabled = true;
                    txttotalextras1.IsEnabled = true;
                    txttotalextras2.IsEnabled = true;
                    txtwickets1.IsEnabled = true;
                    txtwickets2.IsEnabled = true;
                    dgvbat1.IsReadOnly = false;
                    dgvbat2.IsReadOnly = false;
                    dgvbowl1.IsReadOnly = false;
                    dgvbowl2.IsReadOnly = false;
                    txtrunout1.IsEnabled = true;
                    txtrunout2.IsEnabled = true;
                    loadplayer();
                 


                }
            }

            if (lstMatchDetailsSort.Count > 0)
            {



                string scorecardquery = "select * from ScoreCardDetails where matchid = '" + selectedgridvalue.matchid + "'";
                OleDbDataAdapter scoreadpt = new OleDbDataAdapter(scorecardquery, oleconn);

                string matchquery = "select * from matchdetails where matchid = '" + selectedgridvalue.matchid + "'";
                OleDbDataAdapter matchadpt = new OleDbDataAdapter(matchquery, oleconn);

                DataTable dtscore = new DataTable();
                DataTable dtmatch = new DataTable();

                scoreadpt.Fill(dtscore);
                matchadpt.Fill(dtmatch);

                DataTable dtbat1 = new DataTable();
                dtbat1.Columns.Add("SLNO");
                dtbat1.Columns.Add("Name Of Players");
                dtbat1.Columns.Add("KSCA UID");
                dtbat1.Columns.Add("Dismissal");
                dtbat1.Columns.Add("Runs");
                dtbat1.Columns.Add("Mins");
                dtbat1.Columns.Add("Balls");
                dtbat1.Columns.Add("Fours");
                dtbat1.Columns.Add("Sixes");

                DataTable dtbowl1 = new DataTable();
                dtbowl1.Columns.Add("SLNO");
                dtbowl1.Columns.Add("Name Of Players");
                dtbowl1.Columns.Add("KSCA UID");
                dtbowl1.Columns.Add("Overs");
                dtbowl1.Columns.Add("Maidens");
                dtbowl1.Columns.Add("Runs");
                dtbowl1.Columns.Add("Wickets");
                dtbowl1.Columns.Add("No Ball");
                dtbowl1.Columns.Add("Wide");
                dtbowl1.Columns.Add("Avg");

                DataTable dtbat2 = new DataTable();
                dtbat2.Columns.Add("SLNO");
                dtbat2.Columns.Add("Name Of Players");
                dtbat2.Columns.Add("KSCA UID");
                dtbat2.Columns.Add("Dismissal");
                dtbat2.Columns.Add("Runs");
                dtbat2.Columns.Add("Mins");
                dtbat2.Columns.Add("Balls");
                dtbat2.Columns.Add("Fours");
                dtbat2.Columns.Add("Sixes");

                DataTable dtbowl2 = new DataTable();
                dtbowl2.Columns.Add("SLNO");
                dtbowl2.Columns.Add("Name Of Players");
                dtbowl2.Columns.Add("KSCA UID");
                dtbowl2.Columns.Add("Overs");
                dtbowl2.Columns.Add("Maidens");
                dtbowl2.Columns.Add("Runs");
                dtbowl2.Columns.Add("Wickets");
                dtbowl2.Columns.Add("No Ball");
                dtbowl2.Columns.Add("Wide");
                dtbowl2.Columns.Add("Avg");



                int Slno = 1;
                int Slno1 = 1;
                int Slno2 = 1;
                int Slno3 = 1;

                foreach (DataRowView objscore in dtscore.DefaultView)
                {
                    string scorecardid = objscore["ScoreCardDetailsId"].ToString();

                    if (objscore["DivisionId"].ToString() == selectedgridvalue.LoadDivisionId && objscore["SeasonId"].ToString() == TempValues.LoadSeasonId)
                    {
                        foreach (DataRowView obj in dtmatch.DefaultView)
                        {
                            if (objscore["ScoreCardDetailsId"].ToString() == obj["ScoreCardID"].ToString())
                            {

                                lblbat1.Content = objscore["TeamA"].ToString();
                                lblbowl1.Content = objscore["TeamB"].ToString();
                                lblbat2.Content = objscore["TeamB"].ToString();
                                lblbowl2.Content = objscore["TeamA"].ToString();
                                //bat1

                                DataRow dr = dtbat1.NewRow();

                                dr["SLNO"] = Slno;
                                dr["Name Of Players"] = obj["PlayersA"].ToString();
                                dr["KSCA UID"] = obj["KSCAUIDA"].ToString();
                                dr["Dismissal"] = obj["DismissalA"].ToString();
                                dr["Runs"] = obj["BattingRunsA"].ToString();
                                dr["Mins"] = obj["MinsA"].ToString();
                                dr["Balls"] = obj["BallsA"].ToString();
                                dr["Fours"] = obj["FoursA"].ToString();
                                dr["Sixes"] = obj["SixesA"].ToString();



                                dtbat1.Rows.Add(dr);
                                Slno++;



                                //bowl2

                                DataRow dr1 = dtbowl2.NewRow();

                                dr1["SLNO"] = Slno1;
                                dr1["Name Of Players"] = obj["PlayersB"].ToString();
                                dr1["KSCA UID"] = obj["KSCAUIDA"].ToString();
                                dr1["Overs"] = Math.Round(double.Parse(obj["OversB"].ToString()), 1);
                                dr1["Maidens"] = obj["MaidensB"].ToString();
                                dr1["Runs"] =obj["BowlingRunsB"].ToString();
                                dr1["Wickets"] = obj["WicketsB"].ToString();
                                dr1["No Ball"] = obj["NoBallsB"].ToString();
                                dr1["Wide"] = obj["WidesB"].ToString();
                                dr1["Avg"] = Math.Round(double.Parse(obj["AverageB"].ToString()),1);



                                dtbowl2.Rows.Add(dr1);
                                Slno1++;



                                //bat2
                                DataRow dr2 = dtbat2.NewRow();

                                dr2["SLNO"] = Slno2;
                                dr2["Name Of Players"] = obj["PlayersB"].ToString();
                                dr2["KSCA UID"] = obj["KSCAUIDB"].ToString();
                                dr2["Dismissal"] = obj["DismissalB"].ToString();
                                dr2["Runs"] = obj["BattingRunsB"].ToString();
                                dr2["Mins"] = obj["MinsB"].ToString();
                                dr2["Balls"] = obj["BallsB"].ToString();
                                dr2["Fours"] = obj["FoursB"].ToString();
                                dr2["Sixes"] = obj["SixesB"].ToString();



                                dtbat2.Rows.Add(dr2);
                                Slno2++;


                                //bowl1
                                DataRow dr3 = dtbowl1.NewRow();

                                dr3["SLNO"] = Slno3;
                                dr3["Name Of Players"] = obj["PlayersA"].ToString();
                                dr3["KSCA UID"] = obj["KSCAUIDA"].ToString();
                                dr3["Overs"] =Math.Round(double.Parse( obj["OversA"].ToString()),1);
                                dr3["Maidens"] = obj["MaidensA"].ToString();
                                dr3["Runs"] = obj["BowlingRunsA"].ToString();
                                dr3["Wickets"] = obj["WicketsA"].ToString();
                                dr3["No Ball"] = obj["NoBallsA"].ToString();
                                dr3["Wide"] = obj["WidesA"].ToString();
                                dr3["Avg"] =Math.Round(double.Parse( obj["AverageA"].ToString()),1);

                                dtbowl1.Rows.Add(dr3);
                                Slno3++;


                                //clear
                                //start_time1.Value = null;
                                //close_time1.Value = null;
                                txtDuration1.Text = string.Empty;
                                txtbyes1.Text = string.Empty;
                                txtlegbyes1.Text = string.Empty;
                                txtpenalty1.Text = string.Empty;
                                txttotalextras1.Text = string.Empty;
                                txtruns1.Text = string.Empty;
                                txtwickets1.Text = string.Empty;
                                txtovers1.Text = string.Empty;
                                txtrunout1.Text = string.Empty;

                                //start_time2.Value = null;
                                //close_time2.Value = null;
                                txtDuration2.Text = string.Empty;
                                txtbyes2.Text = string.Empty;
                                txtlegbyes2.Text = string.Empty;
                                txtpenalty2.Text = string.Empty;
                                txttotalextras2.Text = string.Empty;
                                txtruns2.Text = string.Empty;
                                txtwickets2.Text = string.Empty;
                                txtovers2.Text = string.Empty;
                                txtrunout2.Text = string.Empty;

                                ////Read Only
                                //Readonly
                                btnsave1.IsEnabled = false;
                                Result.IsEnabled = false;
                                dgvbat1.IsReadOnly = true;
                                dgvbat2.IsReadOnly = true;
                                dgvbowl1.IsReadOnly = true;
                                dgvbowl2.IsReadOnly = true;
                                btn1score.IsEnabled = false;
                                btn2score.IsEnabled = false;
                                start_time1.IsEnabled = false;
                                start_time2.IsEnabled = false;
                                close_time1.IsEnabled = false;
                                close_time2.IsEnabled = false;
                                txtbyes1.IsEnabled = false;
                                txtbyes2.IsEnabled = false;
                                txtDuration1.IsEnabled = false;
                                txtDuration2.IsEnabled = false;
                                txtlegbyes1.IsEnabled = false;
                                txtlegbyes2.IsEnabled = false;
                                txtovers1.IsEnabled = false;
                                txtovers2.IsEnabled = false;
                                txtpenalty1.IsEnabled = false;
                                txtpenalty2.IsEnabled = false;
                                txtruns1.IsEnabled = false;
                                txtruns2.IsEnabled = false;
                                txttotalextras1.IsEnabled = false;
                                txttotalextras2.IsEnabled = false;
                                txtwickets1.IsEnabled = false;
                                txtwickets2.IsEnabled = false;
                                txtrunout1.IsEnabled = false;
                                txtrunout2.IsEnabled = false;
                                chkmatchnotplayed.IsEnabled = false;
                                //Assign Values

                                start_time1.Text = (objscore["CommenceTimeA"].ToString());
                                close_time1.Text = (objscore["EndTimeA"].ToString());
                                txtDuration1.Text = objscore["DurationA"].ToString();
                                txtbyes1.Text = objscore["ByesA"].ToString();
                                txtlegbyes1.Text = objscore["LegByesA"].ToString();
                                txtpenalty1.Text = objscore["PenaltyA"].ToString();
                                txttotalextras1.Text = objscore["TotalExtrasA"].ToString();
                                txtruns1.Text = objscore["TotalRunsA"].ToString();
                                txtwickets1.Text = objscore["TotalWicketsA"].ToString();
                                txtovers1.Text =Math.Round(decimal.Parse( objscore["TotalOversA"].ToString()),1).ToString();
                                txtrunout1.Text = objscore["runoutA"].ToString();

                                start_time2.Text = objscore["CommenceTimeB"].ToString();
                                close_time2.Text = objscore["EndTimeB"].ToString();
                                txtDuration2.Text = objscore["DurationB"].ToString();
                                txtbyes2.Text = objscore["ByesB"].ToString();
                                txtlegbyes2.Text = objscore["LegByesB"].ToString();
                                txtpenalty2.Text = objscore["PenaltyB"].ToString();
                                txttotalextras2.Text = objscore["TotalExtrasB"].ToString();
                                txtruns2.Text = objscore["TotalRunsB"].ToString();
                                txtwickets2.Text = objscore["TotalWicketsA"].ToString();
                                txtovers2.Text = Math.Round(decimal.Parse(objscore["TotalOversB"].ToString()),1).ToString();
                                txtrunout2.Text = objscore["runoutB"].ToString();

                            }

                            dgvbat1.ItemsSource = null;
                            dgvbat1.DataContext = null;
                            dgvbat1.DataContext = dtbat1;
                            dgvbat1.ItemsSource = dtbat1.DefaultView;

                            dgvbowl2.ItemsSource = null;
                            dgvbowl2.DataContext = null;
                            dgvbowl2.DataContext = dtbowl1;
                            dgvbowl2.ItemsSource = dtbowl1.DefaultView;

                            dgvbat2.ItemsSource = null;
                            dgvbat2.DataContext = null;
                            dgvbat2.DataContext = dtbat2;
                            dgvbat2.ItemsSource = dtbat2.DefaultView;

                            dgvbowl1.ItemsSource = null;
                            dgvbowl1.DataContext = null;
                            dgvbowl1.DataContext = dtbowl2;
                            dgvbowl1.ItemsSource = dtbowl2.DefaultView;


                        }


                    }

                }

            }
            lstMatchDetailsSort.Clear();
        }

        private void btnback_Click(object sender, RoutedEventArgs e)
        {

            teamA.Clear();
            teamB.Clear();
            //clear
            start_time1.Clear();
            close_time1.Clear();
            txtDuration1.Text = string.Empty;
            txtbyes1.Text = string.Empty;
            txtlegbyes1.Text = string.Empty;
            txtpenalty1.Text = string.Empty;
            txttotalextras1.Text = string.Empty;
            txtruns1.Text = string.Empty;
            txtwickets1.Text = string.Empty;
            txtovers1.Text = string.Empty;
            txtrunout1.Text = string.Empty;

            start_time2.Clear();
            close_time2.Clear();
            txtDuration2.Text = string.Empty;
            txtbyes2.Text = string.Empty;
            txtlegbyes2.Text = string.Empty;
            txtpenalty2.Text = string.Empty;
            txttotalextras2.Text = string.Empty;
            txtruns2.Text = string.Empty;
            txtwickets2.Text = string.Empty;
            txtovers2.Text = string.Empty;
            txtrunout2.Text = string.Empty;

            lblbat1.Content = null;
            lblbat2.Content = null;
            lblbowl1.Content = null;
            lblbowl2.Content = null;
            lbldivision.Content = null;
            lblseason.Content = null;
            lblzone.Content = null;

            KSCAUID.teamA = new string[60];
            KSCAUID.teamB = new string[60];
            KSCAUID.countA = 0;
            KSCAUID.countB = 0;
            KSCA.team1 = new string[60];
            KSCA.team2 = new string[60];
            comboboxvalue.value = new string[60];
            comboboxvalue.count = 0;
            selectedgridvalue.matchid = null;


            dgvbat1.ItemsSource = null;
            dgvbat2.ItemsSource = null;
            dgvbowl1.ItemsSource = null;
            dgvbowl2.ItemsSource = null;


            Button btn = (Button)sender;
            btn.Command = NavigationCommands.GoToPage;
            btn.CommandParameter = "/View/SelectPlayers.xaml";

        }

        private void chkmatchnotplayed_Checked(object sender, RoutedEventArgs e)
        {
            ismatchnotplayed = true;

            start_time1.Text = "";
            close_time1.Text = "";
            txtDuration1.Text = "0";
            txtbyes1.Text = "0";
            txtlegbyes1.Text = "0";
            txtpenalty1.Text = "0";
            txttotalextras1.Text = "0";
            txtruns1.Text = "0";
            txtwickets1.Text = "0";
            txtovers1.Text = "0";
            txtrunout1.Text = "0";

            start_time2.Text = "";
            close_time2.Text = "";
            txtDuration2.Text = "0";
            txtbyes2.Text = "0";
            txtlegbyes2.Text = "0";
            txtpenalty2.Text = "0";
            txttotalextras2.Text = "0";
            txtruns2.Text = "0";
            txtwickets2.Text = "0";
            txtovers2.Text = "0";
            txtrunout2.Text = "0";
            dgvbat1.IsReadOnly = true;
            dgvbowl1.IsReadOnly = true;

            /////////
            start_time1.IsEnabled = false;
            close_time1.IsEnabled = false;
            txtDuration1.IsEnabled = false;
            txtbyes1.IsEnabled = false;
            txtlegbyes1.IsEnabled = false;
            txtpenalty1.IsEnabled = false;
            txttotalextras1.IsEnabled = false;
            txtruns1.IsEnabled = false;
            txtwickets1.IsEnabled = false;
            txtovers1.IsEnabled = false;
            txtrunout1.IsEnabled = false;

            start_time2.IsEnabled = false;
            close_time2.IsEnabled = false;
            txtDuration2.IsEnabled = false;
            txtbyes2.IsEnabled = false;
            txtlegbyes2.IsEnabled = false;
            txtpenalty2.IsEnabled = false;
            txttotalextras2.IsEnabled = false;
            txtruns2.IsEnabled = false;
            txtwickets2.IsEnabled = false;
            txtovers2.IsEnabled = false;
            txtrunout2.IsEnabled = false;
            dgvbat2.IsReadOnly = true;
            dgvbowl2.IsReadOnly = true;

            btn1score.IsEnabled = false;
            btn2score.IsEnabled = false;


        }

        private void chkmatchnotplayed_Unchecked(object sender, RoutedEventArgs e)
        {
            ismatchnotplayed = false;

            start_time1.IsEnabled = true;
            close_time1.IsEnabled = true;
            txtDuration1.IsEnabled = true;
            txtbyes1.IsEnabled = true;
            txtlegbyes1.IsEnabled = true;
            txtpenalty1.IsEnabled = true;
            txttotalextras1.IsEnabled = true;
            txtruns1.IsEnabled = true;
            txtwickets1.IsEnabled = true;
            txtovers1.IsEnabled = true;
            txtrunout1.IsEnabled = true;
            dgvbat1.IsReadOnly = false;
            dgvbowl1.IsReadOnly = false;
            chkmatchnotplayed.IsEnabled = false;
            start_time2.IsEnabled = true;
            close_time2.IsEnabled = true;
            txtDuration2.IsEnabled = true;
            txtbyes2.IsEnabled = true;
            txtlegbyes2.IsEnabled = true;
            txtpenalty2.IsEnabled = true;
            txttotalextras2.IsEnabled = true;
            txtruns2.IsEnabled = true;
            txtwickets2.IsEnabled = true;
            txtovers2.IsEnabled = true;
            txtrunout2.IsEnabled = true;
            dgvbat2.IsReadOnly = false;
            dgvbowl2.IsReadOnly = false;

            btn1score.IsEnabled = true;
            btn2score.IsEnabled = true;
        }

        private void dgvbat1_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {

            if (e.Column.Header.ToString() == "Dismissal" || e.Column.Header.ToString() == "Runs" || e.Column.Header.ToString() == "Mins" || e.Column.Header.ToString() == "Balls" || e.Column.Header.ToString() == "Fours" || e.Column.Header.ToString() == "Sixes")
            {

                if (!ismanualeditcommit)
                {
                    ismanualeditcommit = true;

                    DataGrid dggrid = (DataGrid)sender;
                    dggrid.CommitEdit(DataGridEditingUnit.Row, true);

                    drvbat1 = (DataRowView)e.Row.Item;
                    ismanualeditcommit = false;
                }
            }
        }

        private void dgvbat1_CurrentCellChanged(object sender, EventArgs e)
        {
            if (drvbat1 != null)
            {
                drvbat1.EndEdit();

                Regex nameEx = new Regex(@"^[A-Za-z ]+$");
                Regex numex = new Regex(@"^[0-9]+$");

                if (drvbat1["Dismissal"].ToString() != "")
                {

                    if (nameEx.Match(drvbat1["Dismissal"].ToString()).Success)
                    {

                    }
                    else if (!nameEx.Match(drvbat1["Dismissal"].ToString()).Success)
                    {
                        drvbat1["Dismissal"] = "";
                        MessageBox.Show("Invalid Dismissal Value");

                        
                    }
                }

                if (drvbat1["Runs"].ToString() != "")
                {

                    if (numex.Match(drvbat1["Runs"].ToString()).Success)
                    {

                    }
                    else if (!numex.Match(drvbat1["Runs"].ToString()).Success)
                    {
                        drvbat1["Runs"] = "";
                        MessageBox.Show("Invalid Runs Format");
                    }
                }
                if (drvbat1["Mins"].ToString() != "")
                {
                    if (numex.Match(drvbat1["Mins"].ToString()).Success)
                    {

                    }
                    else if (!numex.Match(drvbat1["Mins"].ToString()).Success)
                    {
                        drvbat1["Mins"] = "";
                        MessageBox.Show("Invalid Minutes Format");
                    }
                }
                if (drvbat1["Fours"].ToString() != "")
                {
                    if (numex.Match(drvbat1["Fours"].ToString()).Success)
                    {

                    }
                    else if (!numex.Match(drvbat1["Fours"].ToString()).Success)
                    {
                        drvbat1["Fours"] = "";
                        MessageBox.Show("Invalid 4's Format");
                    }
                }
                if (drvbat1["Sixes"].ToString() != "")
                {
                    if (numex.Match(drvbat1["Sixes"].ToString()).Success)
                    {

                    }
                    else if (!numex.Match(drvbat1["Sixes"].ToString()).Success)
                    {
                        drvbat1["Sixes"] = "";
                        MessageBox.Show("Invalid 6's Format");
                    }
                }
                if (drvbat1["Balls"].ToString() != "")
                {
                    if (numex.Match(drvbat1["Balls"].ToString()).Success)
                    {

                    }
                    else if (!numex.Match(drvbat1["Balls"].ToString()).Success)
                    {
                        drvbat1["Balls"] = "";
                        MessageBox.Show("Invalid Balls Faced Format");
                    }
                }
                drvbat1.EndEdit();
                drvbat1 = null;
            }
        }

        private void dgvbowl1_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.Column.Header.ToString() == "Overs" || e.Column.Header.ToString() == "Maidens" || e.Column.Header.ToString() == "Runs" || e.Column.Header.ToString() == "Wickets" || e.Column.Header.ToString() == "No Ball" || e.Column.Header.ToString() == "Wide" || e.Column.Header.ToString() == "Avg")
            {

                if (!ismanualeditcommit)
                {
                    ismanualeditcommit = true;

                    DataGrid dggrid = (DataGrid)sender;
                    dggrid.CommitEdit(DataGridEditingUnit.Row, true);

                    drvbowl1 = (DataRowView)e.Row.Item;
                    ismanualeditcommit = false;
                }
            }
        }

        private void dgvbowl1_CurrentCellChanged(object sender, EventArgs e)
        {
            if (drvbowl1 != null)
            {
                Regex numex = new Regex(@"^[0-9]+$");
                Regex numexOver = new Regex(@"^[0-9]+(\.[0-9]+)?$");
                drvbowl1.EndEdit();
                if (drvbowl1["Overs"].ToString() != "")
                {

                    if (numexOver.Match(drvbowl1["Overs"].ToString()).Success)
                    {

                    }
                    else if (!numexOver.Match(drvbowl1["Overs"].ToString()).Success)
                    {
                        drvbowl1["Overs"] = "";
                        MessageBox.Show("Invalid Overs Format");
                    }
                }
                if (drvbowl1["Maidens"].ToString() != "")
                {
                    if (numex.Match(drvbowl1["Maidens"].ToString()).Success)
                    {

                    }
                    else if (!numex.Match(drvbowl1["Maidens"].ToString()).Success)
                    {
                        drvbowl1["Maidens"] = "";
                        MessageBox.Show("Invalid Maidens Format");
                    }
                }
                if (drvbowl1["Runs"].ToString() != "")
                {
                    if (numex.Match(drvbowl1["Runs"].ToString()).Success)
                    {

                    }
                    else if (!numex.Match(drvbowl1["Runs"].ToString()).Success)
                    {
                        drvbowl1["Runs"] = "";
                        MessageBox.Show("Invalid Runs Format");
                    }
                }
                if (drvbowl1["Wickets"].ToString() != "")
                {
                    if (numex.Match(drvbowl1["Wickets"].ToString()).Success)
                    {

                    }
                    else if (!numex.Match(drvbowl1["Wickets"].ToString()).Success)
                    {
                        drvbowl1["Wickets"] = "";
                        MessageBox.Show("Invalid Wickets Format");
                    }
                }
                if (drvbowl1["No Ball"].ToString() != "")
                {
                    if (numex.Match(drvbowl1["No Ball"].ToString()).Success)
                    {

                    }
                    else if (!numex.Match(drvbowl1["No Ball"].ToString()).Success)
                    {
                        drvbowl1["No Ball"] = "";
                        MessageBox.Show("Invalid No Ball's Format");
                    }
                }

                if (drvbowl1["Wide"].ToString() != "")
                {
                    if (numex.Match(drvbowl1["Wide"].ToString()).Success)
                    {

                    }
                    else if (!numex.Match(drvbowl1["Wide"].ToString()).Success)
                    {
                        drvbowl1["Wide"] = "";
                        MessageBox.Show("Invalid Wide's Format");
                    }
                }
                if (drvbowl1["Avg"].ToString() != "")
                {
                    if (numex.Match(drvbowl1["Avg"].ToString()).Success)
                    {

                    }
                    else if (!numex.Match(drvbowl1["Avg"].ToString()).Success)
                    {
                        drvbowl1["Avg"] = "";
                        MessageBox.Show("Invalid Average Format");
                    }
                }
                drvbowl1.EndEdit();
                drvbowl1 = null;
            }
        }

        private void dgvbat2_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.Column.Header.ToString() == "Dismissal" || e.Column.Header.ToString() == "Runs" || e.Column.Header.ToString() == "Mins" || e.Column.Header.ToString() == "Balls" || e.Column.Header.ToString() == "Fours" || e.Column.Header.ToString() == "Sixes")
            {

                if (!ismanualeditcommit)
                {
                    ismanualeditcommit = true;

                    DataGrid dggrid = (DataGrid)sender;
                    dggrid.CommitEdit(DataGridEditingUnit.Row, true);

                    drvbat2 = (DataRowView)e.Row.Item;
                    ismanualeditcommit = false;
                }
            }
        }

        private void dgvbat2_CurrentCellChanged(object sender, EventArgs e)
        {
            if (drvbat2 != null)
            {
                drvbat2.EndEdit();

                Regex nameEx = new Regex(@"^[A-Za-z ]+$");
                Regex numex = new Regex(@"^[0-9]+$");

                if (drvbat2["Dismissal"].ToString() != "")
                {

                    if (nameEx.Match(drvbat2["Dismissal"].ToString()).Success)
                    {

                    }
                    else if (!nameEx.Match(drvbat2["Dismissal"].ToString()).Success)
                    {
                        drvbat2["Dismissal"] = "";
                        MessageBox.Show("Invalid Dismissal Format");


                    }
                }

                if (drvbat2["Runs"].ToString() != "")
                {

                    if (numex.Match(drvbat2["Runs"].ToString()).Success)
                    {

                    }
                    else if (!numex.Match(drvbat2["Runs"].ToString()).Success)
                    {
                        drvbat2["Runs"] = "";
                        MessageBox.Show("Invalid Runs Format");
                    }
                }
                if (drvbat2["Mins"].ToString() != "")
                {
                    if (numex.Match(drvbat2["Mins"].ToString()).Success)
                    {

                    }
                    else if (!numex.Match(drvbat2["Mins"].ToString()).Success)
                    {
                        drvbat2["Mins"] = "";
                        MessageBox.Show("Invalid Mins Format");
                    }
                }
                if (drvbat2["Fours"].ToString() != "")
                {
                    if (numex.Match(drvbat2["Fours"].ToString()).Success)
                    {

                    }
                    else if (!numex.Match(drvbat2["Fours"].ToString()).Success)
                    {
                        drvbat2["Fours"] = "";
                        MessageBox.Show("Invalid 4's Format");
                    }
                }
                if (drvbat2["Sixes"].ToString() != "")
                {
                    if (numex.Match(drvbat2["Sixes"].ToString()).Success)
                    {

                    }
                    else if (!numex.Match(drvbat2["Sixes"].ToString()).Success)
                    {
                        drvbat2["Sixes"] = "";
                        MessageBox.Show("Invalid 6's Format");
                    }
                }
                if (drvbat2["Balls"].ToString() != "")
                {
                    if (numex.Match(drvbat2["Balls"].ToString()).Success)
                    {

                    }
                    else if (!numex.Match(drvbat2["Balls"].ToString()).Success)
                    {
                        drvbat2["Balls"] = "";
                        MessageBox.Show("Invalid Ball's Faced Format");
                    }
                }
                drvbat2.EndEdit();
                drvbat2 = null;
            }
        }

        private void dgvbowl2_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.Column.Header.ToString() == "Overs" || e.Column.Header.ToString() == "Maidens" || e.Column.Header.ToString() == "Runs" || e.Column.Header.ToString() == "Wickets" || e.Column.Header.ToString() == "No Ball" || e.Column.Header.ToString() == "Wide" || e.Column.Header.ToString() == "Avg")
            {

                if (!ismanualeditcommit)
                {
                    ismanualeditcommit = true;

                    DataGrid dggrid = (DataGrid)sender;
                    dggrid.CommitEdit(DataGridEditingUnit.Row, true);

                    drvbowl2 = (DataRowView)e.Row.Item;
                    ismanualeditcommit = false;
                }
            }
        }

        private void dgvbowl2_CurrentCellChanged(object sender, EventArgs e)
        {
            if (drvbowl2 != null)
            {
                Regex numex = new Regex(@"^[0-9]+$");
                Regex numexOver = new Regex(@"^[0-9]+(\.[0-9]+)?$");

                drvbowl2.EndEdit();
                if (drvbowl2["Overs"].ToString() != "")
                {

                    if (numexOver.Match(drvbowl2["Overs"].ToString()).Success)
                    {

                    }
                    else if (!numexOver.Match(drvbowl2["Overs"].ToString()).Success)
                    {
                        drvbowl2["Overs"] = "";
                        MessageBox.Show("Invalid Overs Format");
                    }
                }
                if (drvbowl2["Maidens"].ToString() != "")
                {
                    if (numex.Match(drvbowl2["Maidens"].ToString()).Success)
                    {

                    }
                    else if (!numex.Match(drvbowl2["Maidens"].ToString()).Success)
                    {
                        drvbowl2["Maidens"] = "";
                        MessageBox.Show("Invalid Maidens Format");
                    }
                }
                if (drvbowl2["Runs"].ToString() != "")
                {
                    if (numex.Match(drvbowl2["Runs"].ToString()).Success)
                    {

                    }
                    else if (!numex.Match(drvbowl2["Runs"].ToString()).Success)
                    {
                        drvbowl2["Runs"] = "";
                        MessageBox.Show("Invalid Runs Format");
                    }
                }
                if (drvbowl2["Wickets"].ToString() != "")
                {
                    if (numex.Match(drvbowl2["Wickets"].ToString()).Success)
                    {

                    }
                    else if (!numex.Match(drvbowl2["Wickets"].ToString()).Success)
                    {
                        drvbowl2["Wickets"] = "";
                        MessageBox.Show("Invalid Wickets Format");
                    }
                }
                if (drvbowl2["No Ball"].ToString() != "")
                {
                    if (numex.Match(drvbowl2["No Ball"].ToString()).Success)
                    {

                    }
                    else if (!numex.Match(drvbowl2["No Ball"].ToString()).Success)
                    {
                        drvbowl2["No Ball"] = "";
                        MessageBox.Show("Invalid No Ball's Format");
                    }
                }

                if (drvbowl2["Wide"].ToString() != "")
                {
                    if (numex.Match(drvbowl2["Wide"].ToString()).Success)
                    {

                    }
                    else if (!numex.Match(drvbowl2["Wide"].ToString()).Success)
                    {
                        drvbowl2["Wide"] = "";
                        MessageBox.Show("Invalid Wide Format");
                    }
                }
                if (drvbowl2["Avg"].ToString() != "")
                {
                    if (numex.Match(drvbowl2["Avg"].ToString()).Success)
                    {

                    }
                    else if (!numex.Match(drvbowl2["Avg"].ToString()).Success)
                    {
                        drvbowl2["Avg"] = "";
                        MessageBox.Show("Invalid Wickets Format");
                    }
                }
                drvbowl2.EndEdit();
                drvbowl2 = null;
            }
        }

        

      

        private void start_time1_LostFocus(object sender, RoutedEventArgs e)
        {
            System.Text.RegularExpressions.Regex rtime = new System.Text.RegularExpressions.Regex(@"[0-2][0-9]\:[0-6][0-9]");


            if (!rtime.IsMatch(start_time1.Text))
            {

                MessageBox.Show("Please provide the time in hh:mm format");
                start_time1.Text = "";
            }
        }

        private void close_time1_LostFocus(object sender, RoutedEventArgs e)
        {
            System.Text.RegularExpressions.Regex rtime = new System.Text.RegularExpressions.Regex(@"[0-2][0-9]\:[0-6][0-9]");


            if (!rtime.IsMatch(close_time1.Text))
            {

                MessageBox.Show("Please provide the time in hh:mm format");
                close_time1.Text = "";
            }
        }

        private void start_time2_LostFocus(object sender, RoutedEventArgs e)
        {
            System.Text.RegularExpressions.Regex rtime = new System.Text.RegularExpressions.Regex(@"[0-2][0-9]\:[0-6][0-9]");


            if (!rtime.IsMatch(start_time2.Text))
            {

                MessageBox.Show("Please provide the time in hh:mm format");
                start_time2.Text = "";
            }
        }

        private void close_time2_LostFocus(object sender, RoutedEventArgs e)
        {
            System.Text.RegularExpressions.Regex rtime = new System.Text.RegularExpressions.Regex(@"[0-2][0-9]\:[0-6][0-9]");


            if (!rtime.IsMatch(close_time2.Text))
            {

                MessageBox.Show("Please provide the time in hh:mm format");
                close_time2.Text = "";
            }
        }
    }
        public static class Match
        {
            public static string Result;
            public static string _MatchScoreId;

        }

        public static class KSCA
        {
            public static string[] team1 = new string[50];
            public static int count1 = 0;


            public static string[] team2 = new string[50];
            public static int count2 = 0;
        }

       

    }




