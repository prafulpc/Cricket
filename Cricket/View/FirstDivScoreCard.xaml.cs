using System;
using System.Collections.Generic;
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

using CricketSol;
using CricketSol.Base;
using CricketSol.DAL;
using CricketSol.Database;
using CricketSol.System;
using System.Configuration;
using System.Collections.ObjectModel;
using Cricket.BLL;
using System.Data;
using System.Data.OleDb;
using System.Text.RegularExpressions;



namespace Cricket.View
{
    /// <summary>
    /// Interaction logic for FirstDivScoreCard.xaml
    /// </summary>
    /// 

    public partial class FirstDivScoreCard : UserControl
    {


        OleDbConnection oleconn = Database.getConnection();

        bool ismatchnotpalyed;
        bool team1secondinningsbattingnotplayed;
        bool team2firstinningsbattingnotplayed;
        bool team2secondbattingnotplayed;
        bool team1firstinningsbowlingnotplayed;
        bool team1secondinningsbowlingnotplayed;
        bool team2secondinningsbowlingnotplayed;

        DataRowView drv;
        bool ismanualeditcommit = false;

        int team1runs1;
        int team1runs2;
        int team2runs1;
        int team2runs2;

        bool isaddedbt1;
        bool isaddedbt2;
        bool isaddedbl1;
        bool isaddedbl2;

        decimal team1overs1;
        decimal team1overs2;
        decimal team2overs1;
        decimal team2overs2;




        int firstinng1count1;
        int firstinng1count2;

        int firstinng2count1;
        int firstinng2count2;

        int secondinng1count1;
        int secondinng1count2;

        int secondinng2count1;
        int secondinng2count2;


        bool _isResultclick;
        bool _isSaveClick;
        bool _IsClickedOnce;


        ObservableCollection<string> teamA = new ObservableCollection<string>();
        ObservableCollection<string> teamB = new ObservableCollection<string>();
        ObservableCollection<string> KSCAUIDA = new ObservableCollection<string>();
        ObservableCollection<string> KSCAUIDB = new ObservableCollection<string>();



        public FirstDivScoreCard()
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


                        lblbat1firstinngs.Content = tempvalues.teamA;
                        lblbowl1firstinngs.Content = tempvalues.teamB;

                        lblbat1secondinngs.Content = tempvalues.teamA;
                        lblbowl1secondinngs.Content = tempvalues.teamB;

                        lblbat2firstinngs.Content = tempvalues.teamB;
                        lblbowl2firstinngs.Content = tempvalues.teamA;

                        lblbat2secondinngs.Content = tempvalues.teamB;
                        lblbowl2secondinngs.Content = tempvalues.teamA;




                        //teamA (batting)  teamB (bowling)
                        if ((comboboxvalue.value[0] == "0" && comboboxvalue.value[1] == "0") || (comboboxvalue.value[0] == "1" && comboboxvalue.value[1] == "1"))
                        {
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

                            for (int i = 0; i <= teamA.Count - 1; i++)
                            {
                                KSCA.team1[i] = KSCAUID.teamA[i];
                            }

                            for (int j = 0; j <= teamB.Count - 1; j++)
                            {
                                KSCA.team2[j] = KSCAUID.teamB[j];
                            }

                        }

                        //teamA (bowling)  teamB (batting)
                        if ((comboboxvalue.value[0] == "0" && comboboxvalue.value[1] == "1") || (comboboxvalue.value[0] == "1" && comboboxvalue.value[1] == "0"))
                        {

                            foreach (var abc in (TempValuesTeam1.team1))
                            {
                                if (abc != null)
                                {
                                    teamB.Add(abc.ToString());
                                }
                            }

                            foreach (var xyz in TempValuesTeam2.team2)
                            {
                                if (xyz != null)
                                {
                                    teamA.Add(xyz.ToString());
                                }
                            }


                            for (int i = 0; i <= teamB.Count - 1; i++)
                            {
                                KSCA.team1[i] = KSCAUID.teamB[i];
                            }

                            for (int j = 0; j <= teamA.Count - 1; j++)
                            {
                                KSCA.team2[j] = KSCAUID.teamA[j];
                            }



                        }

                        dgvbat1firstinngs.ItemsSource = (objscorecardplay.batting1(teamA)).DefaultView;
                        dgvbat1secondinngs.ItemsSource = (objscorecardplay.batting1(teamA)).DefaultView;

                        dgvbat2firstinngs.ItemsSource = (objscorecardplay.batting2(teamB)).DefaultView;
                        dgvbat2secondinngs.ItemsSource = (objscorecardplay.batting2(teamB)).DefaultView;

                        dgvbowl1firstinngs.ItemsSource = (objscorecardplay.bowling1(teamB)).DefaultView;
                        dgvbowl1secondinngs.ItemsSource = (objscorecardplay.bowling1(teamB)).DefaultView;

                        dgvbowl2firstinngs.ItemsSource = (objscorecardplay.bowling2(teamA)).DefaultView;
                        dgvbowl2secondinngs.ItemsSource = (objscorecardplay.bowling2(teamA)).DefaultView;

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

        //public void timeAfirstinngs()
        //{
        //    if (chkmatchnotplayed.IsChecked == false)
        //    {
        //        DateTime startTime = Convert.ToDateTime(start_time1firstinngs.Value);
        //        DateTime endtime = Convert.ToDateTime(close_time1firstinngs.Value);


        //        string s_time;
        //        if ((startTime.ToString()).Substring(startTime.ToString().IndexOf(" ") + 2, 1) == ":")
        //        {

        //            // 2:41:32 AM
        //            s_time = ((startTime.ToString()).Substring(startTime.ToString().IndexOf(" ") + 1, 10));
        //            starttimeAfirstinngs = ((s_time.Substring(0, 4))) + " " + (s_time.Substring(8, 2));
        //        }
        //        else
        //        {
        //            // 08-May-15 12:41:32 AM
        //            s_time = ((startTime.ToString()).Substring(startTime.ToString().IndexOf(" ") + 1, 11));
        //            starttimeAfirstinngs = ((s_time.Substring(0, 5))) + " " + (s_time.Substring(9, 2));
        //        }

        //        string e_time;
        //        if ((endtime.ToString()).Substring(endtime.ToString().IndexOf(" ") + 2, 1) == ":")
        //        {
        //            // 2:41:32 AM
        //            e_time = ((endtime.ToString()).Substring(endtime.ToString().IndexOf(" ") + 1, 10));
        //            endtimeAfirstinngs = ((e_time.Substring(0, 4))) + " " + (e_time.Substring(8, 2));
        //        }
        //        else
        //        {
        //            // 08-May-15 12:41:32 AM
        //            e_time = ((endtime.ToString()).Substring(endtime.ToString().IndexOf(" ") + 1, 11));
        //            endtimeAfirstinngs = ((e_time.Substring(0, 5))) + " " + (e_time.Substring(9, 2));
        //        }

        //    }

        //}

        //public void timeAsecondinngs()
        //{
        //    if (chkmatchnotplayed.IsChecked == false)
        //    {
        //        if (chkteam1secondinnings.IsChecked == true)
        //        {
        //            starttimeAsecondinngs = "";
        //        }
        //        else
        //        {
        //            DateTime startTime = Convert.ToDateTime(start_time1secondinngs.Value);
        //            DateTime endtime = Convert.ToDateTime(close_time1secondinngs.Value);

        //            string s_time;
        //            if ((startTime.ToString()).Substring(startTime.ToString().IndexOf(" ") + 2, 1) == ":")
        //            {

        //                // 2:41:32 AM
        //                s_time = ((startTime.ToString()).Substring(startTime.ToString().IndexOf(" ") + 1, 10));
        //                starttimeAsecondinngs = ((s_time.Substring(0, 4))) + " " + (s_time.Substring(8, 2));
        //            }
        //            else
        //            {
        //                // 08-May-15 12:41:32 AM
        //                s_time = ((startTime.ToString()).Substring(startTime.ToString().IndexOf(" ") + 1, 11));
        //                starttimeAsecondinngs = ((s_time.Substring(0, 5))) + " " + (s_time.Substring(9, 2));
        //            }

        //            string e_time;
        //            if ((endtime.ToString()).Substring(endtime.ToString().IndexOf(" ") + 2, 1) == ":")
        //            {
        //                // 2:41:32 AM
        //                e_time = ((endtime.ToString()).Substring(endtime.ToString().IndexOf(" ") + 1, 10));
        //                endtimeAsecondinngs = ((e_time.Substring(0, 4))) + " " + (e_time.Substring(8, 2));
        //            }
        //            else
        //            {
        //                // 08-May-15 12:41:32 AM
        //                e_time = ((endtime.ToString()).Substring(endtime.ToString().IndexOf(" ") + 1, 11));
        //                endtimeAsecondinngs = ((e_time.Substring(0, 5))) + " " + (e_time.Substring(9, 2));
        //            }
        //        }
        //    }
        //}

        //public void timeBfirstinngs()
        //{

        //    if (chkmatchnotplayed.IsChecked == false)
        //    {
        //        DateTime startTime = Convert.ToDateTime(start_time2firstinngs.Value);
        //        DateTime endtime = Convert.ToDateTime(close_time2firstinngs.Value);



        //        string s_time;
        //        if ((startTime.ToString()).Substring(startTime.ToString().IndexOf(" ") + 2, 1) == ":")
        //        {

        //            // 2:41:32 AM
        //            s_time = ((startTime.ToString()).Substring(startTime.ToString().IndexOf(" ") + 1, 10));
        //            starttimeBfirstinngs = ((s_time.Substring(0, 4))) + " " + (s_time.Substring(8, 2));
        //        }
        //        else
        //        {
        //            // 08-May-15 12:41:32 AM
        //            s_time = ((startTime.ToString()).Substring(startTime.ToString().IndexOf(" ") + 1, 11));
        //            starttimeBfirstinngs = ((s_time.Substring(0, 5))) + " " + (s_time.Substring(9, 2));
        //        }

        //        string e_time;
        //        if ((endtime.ToString()).Substring(endtime.ToString().IndexOf(" ") + 2, 1) == ":")
        //        {
        //            // 2:41:32 AM
        //            e_time = ((endtime.ToString()).Substring(endtime.ToString().IndexOf(" ") + 1, 10));
        //            endtimeBfirstinngs = ((e_time.Substring(0, 4))) + " " + (e_time.Substring(8, 2));
        //        }
        //        else
        //        {
        //            // 08-May-15 12:41:32 AM
        //            e_time = ((endtime.ToString()).Substring(endtime.ToString().IndexOf(" ") + 1, 11));
        //            endtimeBfirstinngs = ((e_time.Substring(0, 5))) + " " + (e_time.Substring(9, 2));
        //        }

        //    }

        //}

        //public void timeBsecondinngs()
        //{
        //    if (chkmatchnotplayed.IsChecked == false)
        //    {
        //        if (chkteam2secondinnings.IsChecked == true)
        //        {

        //            starttimeBsecondinngs = "";

        //        }
        //        else
        //        {
        //            DateTime startTime = Convert.ToDateTime(start_time2secondinngs.Value);
        //            DateTime endtime = Convert.ToDateTime(close_time2secondinngs.Value);


        //            string s_time;
        //            if ((startTime.ToString()).Substring(startTime.ToString().IndexOf(" ") + 2, 1) == ":")
        //            {

        //                // 2:41:32 AM
        //                s_time = ((startTime.ToString()).Substring(startTime.ToString().IndexOf(" ") + 1, 10));
        //                starttimeBsecondinngs = ((s_time.Substring(0, 4))) + " " + (s_time.Substring(8, 2));
        //            }
        //            else
        //            {
        //                // 08-May-15 12:41:32 AM
        //                s_time = ((startTime.ToString()).Substring(startTime.ToString().IndexOf(" ") + 1, 11));
        //                starttimeBsecondinngs = ((s_time.Substring(0, 5))) + " " + (s_time.Substring(9, 2));
        //            }

        //            string e_time;
        //            if ((endtime.ToString()).Substring(endtime.ToString().IndexOf(" ") + 2, 1) == ":")
        //            {
        //                // 2:41:32 AM
        //                e_time = ((endtime.ToString()).Substring(endtime.ToString().IndexOf(" ") + 1, 10));
        //                endtimeBsecondinngs = ((e_time.Substring(0, 4))) + " " + (e_time.Substring(8, 2));
        //            }
        //            else
        //            {
        //                // 08-May-15 12:41:32 AM
        //                e_time = ((endtime.ToString()).Substring(endtime.ToString().IndexOf(" ") + 1, 11));
        //                endtimeBsecondinngs = ((e_time.Substring(0, 5))) + " " + (e_time.Substring(9, 2));
        //            }
        //        }
        //    }
        //}

        public void extras1firstinngs_empty()
        {

            try
            {
                if (chkmatchnotplayed.IsChecked == false)
                {
                    if (txtbyes1firstinngs.Text == "")
                    {
                        MessageBox.Show("Enter Byes");
                    }

                    else if (txtlegbyes1firstinngs.Text == "")
                    {
                        MessageBox.Show("Enter Leg-Byes");
                    }

                    else if (txtpenalty1firstinngs.Text == "")
                    {
                        MessageBox.Show("Enter Penalty Runs");
                    }
                    else if (txtrunout1firstinnings.Text == "")
                    {
                        MessageBox.Show("Enter Number Of Run Outs");
                    }
                    else if (start_time1firstinngs.Text == "")
                    {
                        MessageBox.Show("Enter Commenced Time");
                    }
                    else if (close_time1firstinngs.Text == "")
                    {
                        MessageBox.Show("Enter Closed Time");
                    }
                    else if (txtDuration1firstinngs.Text == "")
                    {
                        MessageBox.Show("Enter FIrst Innings Duration Time");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void extras1secondinngs_empty()
        {
            try
            {
                if (chkmatchnotplayed.IsChecked == false)
                {
                    if (chkteam1secondinnings.IsChecked == false)
                    {

                        if (txtbyes1secondinngs.Text == "")
                        {
                            MessageBox.Show("Enter Byes");
                        }

                        else if (txtlegbyes1secondinngs.Text == "")
                        {
                            MessageBox.Show("Enter Leg-Byes");
                        }

                        else if (txtpenalty1secondinngs.Text == "")
                        {
                            MessageBox.Show("Enter Penalty Runs");
                        }
                        else if (txtrunout1seccondinnings.Text == "")
                        {
                            MessageBox.Show("Enter Number Of Run Outs");
                        }
                        else if (start_time1secondinngs.Text == "")
                        {
                            MessageBox.Show("Enter Commenced Time");
                        }
                        else if (close_time1secondinngs.Text == "")
                        {
                            MessageBox.Show("Enter Closed Time");
                        }
                        else if (txtDuration1secondinngs.Text == "")
                        {
                            MessageBox.Show("Enter Second Innings Duration Time");
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void extras2firstinngs_empty()
        {
            try
            {
                if (chkmatchnotplayed.IsChecked == false)
                {
                    if (txtbyes2firstinngs.Text == "")
                    {
                        MessageBox.Show("Enter Byes");
                    }

                    else if (txtlegbyes2firstinngs.Text == "")
                    {
                        MessageBox.Show("Enter Leg-Byes");
                    }

                    else if (txtpenalty2firstinngs.Text == "")
                    {
                        MessageBox.Show("Enter Penalty Runs");
                    }
                    else if (txtrunout2firstinngs.Text == "")
                    {
                        MessageBox.Show("Enter Number Of Run Outs");
                    }
                    else if (start_time2firstinngs.Text == "")
                    {
                        MessageBox.Show("Enter Commenced Time");
                    }
                    else if (close_time2firstinngs.Text == "")
                    {
                        MessageBox.Show("Enter Closed Time");
                    }
                    else if (txtDuration2firstinngs.Text == "")
                    {
                        MessageBox.Show("Enter First Innings Duration Time");
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void extras2secondinngs_empty()
        {
            try
            {
                if (chkmatchnotplayed.IsChecked == false)
                {
                    if (chkteam2secondinnings.IsChecked == false)
                    {

                        if (txtbyes2secondinngs.Text == "")
                        {
                            MessageBox.Show("Enter Byes");
                        }

                        else if (txtlegbyes2secondinngs.Text == "")
                        {
                            MessageBox.Show("Enter Leg-Byes");
                        }

                        else if (txtpenalty2secondinngs.Text == "")
                        {
                            MessageBox.Show("Enter Penalty Runs");
                        }
                        else if (txtrunout2secondinngs.Text == "")
                        {
                            MessageBox.Show("Enter Number Of Run Outs");
                        }
                        else if (start_time2secondinngs.Text == "")
                        {
                            MessageBox.Show("Enter Commenced Time");
                        }
                        else if (close_time2secondinngs.Text == "")
                        {
                            MessageBox.Show("Enter Closed Time");
                        }
                        else if (txtDuration2secondinngs.Text == "")
                        {
                            MessageBox.Show("Enter Second Innings Duration Time");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btn1scorefirstinngs_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (chkmatchnotplayed.IsChecked == false)
                {

                    int runs1firstinngs = 0;
                    int wides1firstinngs = 0;
                    int noballs1firstinngs = 0;
                    int wickts1firstinngs = 0;
                    int bruns1firstinngs = 0;
                    double overs1firstinngs = 0;


                    int R1firstinngs = 0;
                    int W1firstinngs = 0;
                    int N1firstinngs = 0;
                    int Wkts1firstinngs = 0;
                    int BR1firstinngs = 0;
                    double O1firstinngs = 0;

                    if ((comboboxvalue.value[0] == "0" && comboboxvalue.value[1] == "0") || (comboboxvalue.value[0] == "1" && comboboxvalue.value[1] == "1"))
                    {
                        firstinng1count1 = teamA.Count;
                        firstinng1count2 = teamB.Count;

                    }
                    else
                    {
                        firstinng1count1 = teamB.Count;
                        firstinng1count2 = teamA.Count;
                    }

                    ScoreCardBLL objscorecardplay = new ScoreCardBLL();

                    if (txtbyes1firstinngs.Text == "" || txtlegbyes1firstinngs.Text == "" || txtpenalty1firstinngs.Text == "" || txtrunout1firstinnings.Text == "")
                    {
                        extras1firstinngs_empty();
                    }

                    else
                    {


                        for (int i = 0; i <= (firstinng1count1 - 1); i++)
                        {

                            if (!DBNull.Value.Equals((dgvbat1firstinngs.Items[i] as DataRowView).Row.ItemArray[4]) && ((dgvbat1firstinngs.Items[i] as DataRowView).Row.ItemArray[4]).ToString() != "")
                            {
                                R1firstinngs = Convert.ToInt16((dgvbat1firstinngs.Items[i] as DataRowView).Row.ItemArray[4]);
                                runs1firstinngs = objscorecardplay.runs1(R1firstinngs);

                            }
                        }


                        for (int j = 0; j <= (firstinng1count2 - 1); j++)
                        {
                            if (!DBNull.Value.Equals((dgvbowl1firstinngs.Items[j] as DataRowView).Row.ItemArray[8]) && ((dgvbowl1firstinngs.Items[j] as DataRowView).Row.ItemArray[8]).ToString() != "")
                            {
                                W1firstinngs = Convert.ToInt16((dgvbowl1firstinngs.Items[j] as DataRowView).Row.ItemArray[8]);
                                wides1firstinngs = objscorecardplay.wides1(W1firstinngs);
                            }
                        }

                        for (int k = 0; k <= (firstinng1count2 - 1); k++)
                        {
                            if (!DBNull.Value.Equals((dgvbowl1firstinngs.Items[k] as DataRowView).Row.ItemArray[7]) && ((dgvbowl1firstinngs.Items[k] as DataRowView).Row.ItemArray[7]).ToString() != "")
                            {
                                N1firstinngs = Convert.ToInt16((dgvbowl1firstinngs.Items[k] as DataRowView).Row.ItemArray[7]);
                                noballs1firstinngs = objscorecardplay.noball1(N1firstinngs);
                            }
                        }

                        for (int m = 0; m <= (firstinng1count2 - 1); m++)
                        {
                            if (!DBNull.Value.Equals((dgvbowl1firstinngs.Items[m] as DataRowView).Row.ItemArray[6]) && ((dgvbowl1firstinngs.Items[m] as DataRowView).Row.ItemArray[6]).ToString() != "")
                            {
                                Wkts1firstinngs = Convert.ToInt16((dgvbowl1firstinngs.Items[m] as DataRowView).Row.ItemArray[6]);
                                wickts1firstinngs = objscorecardplay.wickets1(Wkts1firstinngs);
                            }
                        }

                        int runout1 = Convert.ToInt16(txtrunout1firstinnings.Text);
                        wickts1firstinngs = wickts1firstinngs + runout1;

                        for (int n = 0; n <= (firstinng1count2 - 1); n++)
                        {
                            if (!DBNull.Value.Equals((dgvbowl1firstinngs.Items[n] as DataRowView).Row.ItemArray[5]) && ((dgvbowl1firstinngs.Items[n] as DataRowView).Row.ItemArray[5]).ToString() != "")
                            {
                                BR1firstinngs = Convert.ToInt16((dgvbowl1firstinngs.Items[n] as DataRowView).Row.ItemArray[5]);
                                bruns1firstinngs = objscorecardplay.bowlerruns1(BR1firstinngs);
                            }
                        }

                        for (int ov = 0; ov <= (firstinng1count2 - 1); ov++)
                        {
                            if (!DBNull.Value.Equals((dgvbowl1firstinngs.Items[ov] as DataRowView).Row.ItemArray[3]) && ((dgvbowl1firstinngs.Items[ov] as DataRowView).Row.ItemArray[3]).ToString() != "")
                            {
                                O1firstinngs = Convert.ToDouble((dgvbowl1firstinngs.Items[ov] as DataRowView).Row.ItemArray[3]);
                                overs1firstinngs = objscorecardplay.overs1(O1firstinngs);
                            }
                        }


                        {
                            txttotalextras1firstinngs.Text = (wides1firstinngs + noballs1firstinngs + Convert.ToInt16(txtpenalty1firstinngs.Text) + Convert.ToInt16(txtbyes1firstinngs.Text) + Convert.ToInt16(txtlegbyes1firstinngs.Text)).ToString();
                            txtruns1firstinngs.Text = (runs1firstinngs + wides1firstinngs + noballs1firstinngs + Convert.ToInt16(txtpenalty1firstinngs.Text) + Convert.ToInt16(txtbyes1firstinngs.Text) + Convert.ToInt16(txtlegbyes1firstinngs.Text)).ToString();
                            txtwickets1firstinngs.Text = wickts1firstinngs.ToString();
                            txtovers1firstinngs.Text = overs1firstinngs.ToString();


                        }

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btn1scoresecondinngs_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (chkmatchnotplayed.IsChecked == false)
                {
                    int runs1secondinngs = 0;
                    int wides1secondinngs = 0;
                    int noballs1secondinngs = 0;
                    int wickts1secondinngs = 0;
                    int bruns1secondinngs = 0;
                    double overs1secondinngs = 0;

                    int R1secondinngs = 0;
                    int W1secondinngs = 0;
                    int N1secondinngs = 0;
                    int Wkts1secondinngs = 0;
                    int BR1secondinngs = 0;
                    double O1secondinngs = 0;

                    if ((comboboxvalue.value[0] == "0" && comboboxvalue.value[1] == "0") || (comboboxvalue.value[0] == "1" && comboboxvalue.value[1] == "1"))
                    {

                        secondinng1count1 = teamA.Count;
                        secondinng1count2 = teamB.Count;
                    }
                    else
                    {
                        secondinng1count1 = teamB.Count;
                        secondinng1count2 = teamA.Count;
                    }

                    ScoreCardBLL objscorecardplay = new ScoreCardBLL();

                    if (txtbyes1secondinngs.Text == "" || txtlegbyes1secondinngs.Text == "" || txtpenalty1secondinngs.Text == "" || txtrunout1seccondinnings.Text == "")
                    {
                        extras1secondinngs_empty();
                    }

                    else
                    {


                        for (int i = 0; i <= (secondinng1count1 - 1); i++)
                        {

                            if (!DBNull.Value.Equals((dgvbat1secondinngs.Items[i] as DataRowView).Row.ItemArray[4]) && ((dgvbat1secondinngs.Items[i] as DataRowView).Row.ItemArray[4]).ToString() != "")
                            {
                                R1secondinngs = Convert.ToInt16((dgvbat1secondinngs.Items[i] as DataRowView).Row.ItemArray[4]);
                                runs1secondinngs = objscorecardplay.runs1(R1secondinngs);

                            }
                        }

                        for (int j = 0; j <= (secondinng1count2 - 1); j++)
                        {
                            if (!DBNull.Value.Equals((dgvbowl1secondinngs.Items[j] as DataRowView).Row.ItemArray[8]) && ((dgvbowl1secondinngs.Items[j] as DataRowView).Row.ItemArray[8]).ToString() != "")
                            {
                                W1secondinngs = Convert.ToInt16((dgvbowl1secondinngs.Items[j] as DataRowView).Row.ItemArray[8]);
                                wides1secondinngs = objscorecardplay.wides1(W1secondinngs);
                            }
                        }

                        for (int k = 0; k <= (secondinng1count2 - 1); k++)
                        {
                            if (!DBNull.Value.Equals((dgvbowl1secondinngs.Items[k] as DataRowView).Row.ItemArray[7]) && ((dgvbowl1secondinngs.Items[k] as DataRowView).Row.ItemArray[7]).ToString() != "")
                            {
                                N1secondinngs = Convert.ToInt16((dgvbowl1secondinngs.Items[k] as DataRowView).Row.ItemArray[7]);
                                noballs1secondinngs = objscorecardplay.noball1(N1secondinngs);
                            }
                        }

                        for (int m = 0; m <= (secondinng1count2 - 1); m++)
                        {
                            if (!DBNull.Value.Equals((dgvbowl1secondinngs.Items[m] as DataRowView).Row.ItemArray[6]) && ((dgvbowl1secondinngs.Items[m] as DataRowView).Row.ItemArray[6]).ToString() != "")
                            {
                                Wkts1secondinngs = Convert.ToInt16((dgvbowl1secondinngs.Items[m] as DataRowView).Row.ItemArray[6]);
                                wickts1secondinngs = objscorecardplay.wickets1(Wkts1secondinngs);
                            }
                        }

                        int runout1 = Convert.ToInt16(txtrunout1seccondinnings.Text);
                        wickts1secondinngs = wickts1secondinngs + runout1;

                        for (int n = 0; n <= (secondinng1count2 - 1); n++)
                        {
                            if (!DBNull.Value.Equals((dgvbowl1secondinngs.Items[n] as DataRowView).Row.ItemArray[5]) && ((dgvbowl1secondinngs.Items[n] as DataRowView).Row.ItemArray[5]).ToString() != "")
                            {
                                BR1secondinngs = Convert.ToInt16((dgvbowl1secondinngs.Items[n] as DataRowView).Row.ItemArray[5]);
                                bruns1secondinngs = objscorecardplay.bowlerruns1(BR1secondinngs);
                            }
                        }

                        for (int ov = 0; ov <= (secondinng1count2 - 1); ov++)
                        {
                            if (!DBNull.Value.Equals((dgvbowl1secondinngs.Items[ov] as DataRowView).Row.ItemArray[3]) && ((dgvbowl1secondinngs.Items[ov] as DataRowView).Row.ItemArray[3]).ToString() != "")
                            {
                                O1secondinngs = Convert.ToDouble((dgvbowl1secondinngs.Items[ov] as DataRowView).Row.ItemArray[3]);
                                overs1secondinngs = objscorecardplay.overs1(O1secondinngs);
                            }
                        }


                        txttotalextras1secondinngs.Text = (wides1secondinngs + noballs1secondinngs + Convert.ToInt16(txtpenalty1secondinngs.Text) + Convert.ToInt16(txtbyes1secondinngs.Text) + Convert.ToInt16(txtlegbyes1secondinngs.Text)).ToString();
                        txtruns1secondinngs.Text = (runs1secondinngs + wides1secondinngs + noballs1secondinngs + Convert.ToInt16(txtpenalty1secondinngs.Text) + Convert.ToInt16(txtbyes1secondinngs.Text) + Convert.ToInt16(txtlegbyes1secondinngs.Text)).ToString();
                        txtwickets1secondinngs.Text = wickts1secondinngs.ToString();
                        txtovers1secondinngs.Text = overs1secondinngs.ToString();



                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btn2scorefirstinngs_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (chkmatchnotplayed.IsChecked == false)
                {


                    int runs2firstinngs = 0;
                    int wides2firstinngs = 0;
                    int noballs2firstinngs = 0;
                    int wickts2firstinngs = 0;
                    int bruns2firstinngs = 0;
                    double overs2firstinngs = 0;

                    int R2firstinngs = 0;
                    int W2firstinngs = 0;
                    int N2firstinngs = 0;
                    int Wkts2firstinngs = 0;
                    int BR2firstinngs = 0;
                    double O2firstinngs = 0;


                    if ((comboboxvalue.value[0] == "0" && comboboxvalue.value[1] == "0") || (comboboxvalue.value[0] == "1" && comboboxvalue.value[1] == "1"))
                    {
                        firstinng2count1 = teamB.Count;
                        firstinng2count2 = teamA.Count;
                    }
                    else
                    {
                        firstinng2count1 = teamA.Count;
                        firstinng2count2 = teamB.Count;
                    }

                    ScoreCardBLL objscorecardplay = new ScoreCardBLL();

                    if (txtbyes2firstinngs.Text == "" || txtlegbyes2firstinngs.Text == "" || txtpenalty2firstinngs.Text == "" || txtrunout2firstinngs.Text == "")
                    {
                        extras2firstinngs_empty();
                    }

                    else
                    {

                        for (int i = 0; i <= (firstinng2count1 - 1); i++)
                        {

                            if (!DBNull.Value.Equals((dgvbat2firstinngs.Items[i] as DataRowView).Row.ItemArray[4]) && ((dgvbat2firstinngs.Items[i] as DataRowView).Row.ItemArray[4]).ToString() != "")
                            {
                                R2firstinngs = Convert.ToInt16((dgvbat2firstinngs.Items[i] as DataRowView).Row.ItemArray[4]);
                                runs2firstinngs = objscorecardplay.runs2(R2firstinngs);
                            }
                        }


                        for (int j = 0; j <= (firstinng2count2 - 1); j++)
                        {
                            if (!DBNull.Value.Equals((dgvbowl2firstinngs.Items[j] as DataRowView).Row.ItemArray[8]) && ((dgvbowl2firstinngs.Items[j] as DataRowView).Row.ItemArray[8]).ToString() != "")
                            {
                                W2firstinngs = Convert.ToInt16((dgvbowl2firstinngs.Items[j] as DataRowView).Row.ItemArray[8]);
                                wides2firstinngs = objscorecardplay.wides2(W2firstinngs);
                            }
                        }

                        for (int k = 0; k <= (firstinng2count2 - 1); k++)
                        {
                            if (!DBNull.Value.Equals((dgvbowl2firstinngs.Items[k] as DataRowView).Row.ItemArray[7]) && ((dgvbowl2firstinngs.Items[k] as DataRowView).Row.ItemArray[7]).ToString() != "")
                            {
                                N2firstinngs = Convert.ToInt16((dgvbowl2firstinngs.Items[k] as DataRowView).Row.ItemArray[7]);
                                noballs2firstinngs = objscorecardplay.noball2(N2firstinngs);
                            }
                        }

                        for (int m = 0; m <= (firstinng2count2 - 1); m++)
                        {
                            if (!DBNull.Value.Equals((dgvbowl2firstinngs.Items[m] as DataRowView).Row.ItemArray[6]) && ((dgvbowl2firstinngs.Items[m] as DataRowView).Row.ItemArray[6]).ToString() != "")
                            {
                                Wkts2firstinngs = Convert.ToInt16((dgvbowl2firstinngs.Items[m] as DataRowView).Row.ItemArray[6]);
                                wickts2firstinngs = objscorecardplay.wickets2(Wkts2firstinngs);
                            }
                        }

                        int runout1 = Convert.ToInt16(txtrunout2firstinngs.Text);
                        wickts2firstinngs = wickts2firstinngs + runout1;

                        for (int n = 0; n <= (firstinng2count2 - 1); n++)
                        {
                            if (!DBNull.Value.Equals((dgvbowl2firstinngs.Items[n] as DataRowView).Row.ItemArray[5]) && ((dgvbowl2firstinngs.Items[n] as DataRowView).Row.ItemArray[5]).ToString() != "")
                            {
                                BR2firstinngs = Convert.ToInt16((dgvbowl2firstinngs.Items[n] as DataRowView).Row.ItemArray[5]);
                                bruns2firstinngs = objscorecardplay.bowlerruns2(BR2firstinngs);
                            }
                        }

                        for (int ov = 0; ov <= (firstinng2count2 - 1); ov++)
                        {
                            if (!DBNull.Value.Equals((dgvbowl2firstinngs.Items[ov] as DataRowView).Row.ItemArray[3]) && ((dgvbowl2firstinngs.Items[ov] as DataRowView).Row.ItemArray[3]).ToString() != "")
                            {
                                O2firstinngs = Convert.ToDouble((dgvbowl2firstinngs.Items[ov] as DataRowView).Row.ItemArray[3]);
                                overs2firstinngs = objscorecardplay.overs2(O2firstinngs);
                            }
                        }

                        txttotalextras2firstinngs.Text = (wides2firstinngs + noballs2firstinngs + Convert.ToInt16(txtpenalty2firstinngs.Text) + Convert.ToInt16(txtbyes2firstinngs.Text) + Convert.ToInt16(txtlegbyes2firstinngs.Text)).ToString();
                        txtruns2firstinngs.Text = (runs2firstinngs + wides2firstinngs + noballs2firstinngs + Convert.ToInt16(txtpenalty2firstinngs.Text) + Convert.ToInt16(txtbyes2firstinngs.Text) + Convert.ToInt16(txtlegbyes2firstinngs.Text)).ToString();
                        txtwickets2firstinngs.Text = wickts2firstinngs.ToString();
                        txtovers2firstinngs.Text = overs2firstinngs.ToString();


                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btn2scoresecondinngs_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (chkmatchnotplayed.IsChecked == false)
                {


                    int runs2secondinngs = 0;
                    int wides2secondinngs = 0;
                    int noballs2secondinngs = 0;
                    int wickts2secondinngs = 0;
                    int bruns2secondinngs = 0;
                    double overs2secondinngs = 0;



                    int R2secondinngs = 0;
                    int W2secondinngs = 0;
                    int N2secondinngs = 0;
                    int Wkts2secondinngs = 0;
                    int BR2secondinngs = 0;
                    double O2secondinngs = 0;



                    if ((comboboxvalue.value[0] == "0" && comboboxvalue.value[1] == "0") || (comboboxvalue.value[0] == "1" && comboboxvalue.value[1] == "1"))
                    {
                        secondinng2count1 = teamB.Count;
                        secondinng2count2 = teamA.Count;
                    }
                    else
                    {
                        secondinng2count1 = teamA.Count;
                        secondinng2count2 = teamB.Count;
                    }

                    ScoreCardBLL objscorecardplay = new ScoreCardBLL();

                    if (txtbyes2secondinngs.Text == "" || txtlegbyes2secondinngs.Text == "" || txtpenalty2secondinngs.Text == "" || txtrunout2secondinngs.Text == "")
                    {
                        extras2secondinngs_empty();
                    }

                    else
                    {

                        for (int i = 0; i <= (secondinng2count1 - 1); i++)
                        {

                            if (!DBNull.Value.Equals((dgvbat2secondinngs.Items[i] as DataRowView).Row.ItemArray[4]) && ((dgvbat2secondinngs.Items[i] as DataRowView).Row.ItemArray[4]).ToString() != "")
                            {
                                R2secondinngs = Convert.ToInt16((dgvbat2secondinngs.Items[i] as DataRowView).Row.ItemArray[4]);
                                runs2secondinngs = objscorecardplay.runs2(R2secondinngs);
                            }
                        }


                        for (int j = 0; j <= (secondinng2count2 - 1); j++)
                        {
                            if (!DBNull.Value.Equals((dgvbowl2secondinngs.Items[j] as DataRowView).Row.ItemArray[8]) && ((dgvbowl2secondinngs.Items[j] as DataRowView).Row.ItemArray[8]).ToString() != "")
                            {
                                W2secondinngs = Convert.ToInt16((dgvbowl2secondinngs.Items[j] as DataRowView).Row.ItemArray[8]);
                                wides2secondinngs = objscorecardplay.wides2(W2secondinngs);
                            }
                        }

                        for (int k = 0; k <= (secondinng2count2 - 1); k++)
                        {
                            if (!DBNull.Value.Equals((dgvbowl2secondinngs.Items[k] as DataRowView).Row.ItemArray[7]) && ((dgvbowl2secondinngs.Items[k] as DataRowView).Row.ItemArray[7]).ToString() != "")
                            {
                                N2secondinngs = Convert.ToInt16((dgvbowl2secondinngs.Items[k] as DataRowView).Row.ItemArray[7]);
                                noballs2secondinngs = objscorecardplay.noball2(N2secondinngs);
                            }
                        }

                        for (int m = 0; m <= (secondinng2count2 - 1); m++)
                        {
                            if (!DBNull.Value.Equals((dgvbowl2secondinngs.Items[m] as DataRowView).Row.ItemArray[6]) && ((dgvbowl2secondinngs.Items[m] as DataRowView).Row.ItemArray[6]).ToString() != "")
                            {
                                Wkts2secondinngs = Convert.ToInt16((dgvbowl2secondinngs.Items[m] as DataRowView).Row.ItemArray[6]);
                                wickts2secondinngs = objscorecardplay.wickets2(Wkts2secondinngs);
                            }
                        }

                        int runout1 = Convert.ToInt16(txtrunout2secondinngs.Text);
                        wickts2secondinngs = wickts2secondinngs + runout1;

                        for (int n = 0; n <= (secondinng2count2 - 1); n++)
                        {
                            if (!DBNull.Value.Equals((dgvbowl2secondinngs.Items[n] as DataRowView).Row.ItemArray[5]) && ((dgvbowl2secondinngs.Items[n] as DataRowView).Row.ItemArray[5]).ToString() != "")
                            {
                                BR2secondinngs = Convert.ToInt16((dgvbowl2secondinngs.Items[n] as DataRowView).Row.ItemArray[5]);
                                bruns2secondinngs = objscorecardplay.bowlerruns2(BR2secondinngs);
                            }
                        }

                        for (int ov = 0; ov <= (secondinng2count2 - 1); ov++)
                        {
                            if (!DBNull.Value.Equals((dgvbowl2secondinngs.Items[ov] as DataRowView).Row.ItemArray[3]) && ((dgvbowl2secondinngs.Items[ov] as DataRowView).Row.ItemArray[3]).ToString() != "")
                            {
                                O2secondinngs = Convert.ToDouble((dgvbowl2secondinngs.Items[ov] as DataRowView).Row.ItemArray[3]);
                                overs2secondinngs = objscorecardplay.overs2(O2secondinngs);
                            }
                        }

                        txttotalextras2secondinngs.Text = (wides2secondinngs + noballs2secondinngs + Convert.ToInt16(txtpenalty2secondinngs.Text) + Convert.ToInt16(txtbyes2secondinngs.Text) + Convert.ToInt16(txtlegbyes2secondinngs.Text)).ToString();
                        txtruns2secondinngs.Text = (runs2secondinngs + wides2secondinngs + noballs2secondinngs + Convert.ToInt16(txtpenalty2secondinngs.Text) + Convert.ToInt16(txtbyes2secondinngs.Text) + Convert.ToInt16(txtlegbyes2secondinngs.Text)).ToString();
                        txtwickets2secondinngs.Text = wickts2secondinngs.ToString();
                        txtovers2secondinngs.Text = overs2secondinngs.ToString();


                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Resultsecondinngs_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                _isResultclick = true;
                string abc;

                if (chkmatchnotplayed.IsChecked == true)
                {
                    txtruns1firstinngs.Text = "0";
                    txtruns1secondinngs.Text = "0";
                    txtruns2firstinngs.Text = "0";
                    txtruns2secondinngs.Text = "0";
                    Match.Result = "Match Abondend";
                }

                if (chkteam1secondinnings.IsChecked == true)
                {
                    int score;

                    score = ((Convert.ToInt16(txtruns1firstinngs.Text) + Convert.ToInt16(txtruns1secondinngs.Text)) - (Convert.ToInt16(txtruns2firstinngs.Text) + Convert.ToInt16(txtruns2secondinngs.Text)));

                    abc = tempvalues.teamA;
                    Match.Result = abc + " Lead The Innings By  " + score + " Runs  ";

                }

                if (chkteam1secondinnings.IsChecked == true && chkteam2secondinnings.IsChecked == true)
                {
                    //int score;
                    Match.Result = "Match Ended as Draw";

                }

                if (chkteam2secondinnings.IsChecked == true)
                {
                    int score;

                    score = ((Convert.ToInt16(txtruns2firstinngs.Text) + Convert.ToInt16(txtruns2secondinngs.Text)) - (Convert.ToInt16(txtruns1firstinngs.Text) + Convert.ToInt16(txtruns1secondinngs.Text)));

                    abc = tempvalues.teamB;
                    Match.Result = abc + " Won The Match by  " + score + " Runs";

                }

                if (chkteam1secondinnings.IsChecked == true && chkteam2firstinnings.IsChecked == true && chkteam2secondinnings.IsChecked == true)
                {

                    Match.Result = "Match Abondend";

                }


                if (chkteam1secondinnings.IsChecked == false && chkteam2secondinnings.IsChecked == false && chkmatchnotplayed.IsChecked == false && chkteam2firstinnings.IsChecked == false)
                {
                    if (txtruns1firstinngs.Text == "" || txtruns1secondinngs.Text == "" || txtruns2firstinngs.Text == "" || txtruns2secondinngs.Text == "")
                    {
                        MessageBox.Show("Information is Incomplete Enter complete Information");
                    }

                    if (((Convert.ToInt16(txtruns1firstinngs.Text)) + (Convert.ToInt16(txtruns1secondinngs.Text))) > ((Convert.ToInt16(txtruns2firstinngs.Text)) + (Convert.ToInt16(txtruns2secondinngs.Text))))
                    {

                        abc = tempvalues.teamA;
                        Match.Result = abc + " won the match";

                    }
                    else if (((Convert.ToInt16(txtruns2firstinngs.Text)) + (Convert.ToInt16(txtruns2secondinngs.Text))) > ((Convert.ToInt16(txtruns1firstinngs.Text)) + (Convert.ToInt16(txtruns1secondinngs.Text))))
                    {
                        abc = tempvalues.teamB;
                        Match.Result = abc + " won the match";

                    }

                    else
                    {
                        Match.Result = "Match Tied";
                    }

                }
                //if (((Convert.ToInt16(txtruns2firstinngs.Text)) + (Convert.ToInt16(txtruns2secondinngs.Text))) > ((Convert.ToInt16(txtruns1firstinngs.Text)) + (Convert.ToInt16(txtruns1secondinngs.Text))))
                //{

                //    abc = tempvalues.teamB;
                //    Match.Result = abc + " won the match";

                //}
                //else
                //{
                //    abc = tempvalues.teamA;
                //    Match.Result = abc + " won the match";

                //}


                //if (((Convert.ToInt16(txtruns1firstinngs.Text)) + (Convert.ToInt16(txtruns1secondinngs.Text))) == ((Convert.ToInt16(txtruns2firstinngs.Text)) + (Convert.ToInt16(txtruns2secondinngs.Text))))
                //{
                //    Match.Result = "Match Tied";

                //}


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
            try
            {
                ObservableCollection<FirstDivPointsCricket> obcteam = Database.GetEntityList<FirstDivPointsCricket>(false, false, false, oleconn, "RecordStatus", "TeamName");

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

        public decimal runratecalculation(decimal runs, decimal overs)
        {
            if (ismatchnotpalyed == false)
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


                if (runs != 0)
                {
                    decimal runrate = (runs) / (a1 * 6 + b1);
                    return runrate;
                }

                else
                {
                    decimal runrate = 0;
                    return runrate;
                }

            }
            else
            {
                decimal runrate = 0;
                return runrate;
            }

        }

        public decimal oversconvert(decimal overs)
        {
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

        public decimal addovers(decimal over1, decimal over2)
        {
            try
            {

                int a1 = 0;
                int b1 = 0;
                double o1 = Convert.ToDouble(over1);
                double o2 = Convert.ToDouble(over2);
                int or1 = 0;
                int ol1 = 0;
                decimal overs;

                a1 = Convert.ToInt16(Math.Truncate(o1));
                string str1 = Convert.ToString(Math.Round(o1 - a1, 1));
                if (str1 != "0")
                {
                    b1 = Convert.ToInt16(str1.Split('.')[1].Trim());
                }
                else
                {
                    b1 = 0;
                }

                ol1 = Convert.ToInt16(Math.Truncate(o2));
                string str2 = Convert.ToString(Math.Round(o2 - ol1, 1));
                if (str2 != "0")
                {
                    or1 = Convert.ToInt16(str2.Split('.')[1].Trim());
                }
                else
                {
                    or1 = 0;
                }


                if ((or1 + b1) >= 6)
                {
                    ol1++;
                    ol1 = ol1 + a1;
                    or1 = (or1 + b1) % 6;
                    string over = ol1 + "." + or1;
                    overs = Convert.ToDecimal(over);
                }
                else
                {
                    ol1 = ol1 + a1;
                    or1 = or1 + b1;
                    string over = ol1 + "." + or1;
                    overs = Convert.ToDecimal(over);
                }
                return overs;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void chkmatchnotplayed_Checked(object sender, RoutedEventArgs e)
        {
            ismatchnotpalyed = true;

            //Team1FirstInnings
            start_time1firstinngs.IsEnabled = false;
            close_time1firstinngs.IsEnabled = false;
            txtDuration1firstinngs.IsEnabled = false;
            txtbyes1firstinngs.IsEnabled = false;
            txtlegbyes1firstinngs.IsEnabled = false;
            txtpenalty1firstinngs.IsEnabled = false;
            txttotalextras1firstinngs.IsEnabled = false;
            txtruns1firstinngs.IsEnabled = false;
            txtwickets1firstinngs.IsEnabled = false;
            txtovers1firstinngs.IsEnabled = false;
            dgvbat1firstinngs.IsReadOnly = true;
            dgvbowl1firstinngs.IsReadOnly = true;
            txtrunout1firstinnings.IsEnabled = false;
            btn1scorefirstinngs.IsEnabled = false;



            //Team1secinngs
            start_time1secondinngs.IsEnabled = false;
            close_time1secondinngs.IsEnabled = false;
            txtbyes1secondinngs.IsEnabled = false;
            txtDuration1secondinngs.IsEnabled = false;
            txtlegbyes1secondinngs.IsEnabled = false;
            txtpenalty1secondinngs.IsEnabled = false;
            txttotalextras2secondinngs.IsEnabled = false;
            txtruns1secondinngs.IsEnabled = false;
            txtwickets1secondinngs.IsEnabled = false;
            txtovers1secondinngs.IsEnabled = false;
            dgvbat1secondinngs.IsReadOnly = true;
            dgvbowl1secondinngs.IsReadOnly = true;
            txtrunout1seccondinnings.IsEnabled = false;
            btn1scoresecondinngs.IsEnabled = false;



            //Team2FirstInnings
            start_time2firstinngs.IsEnabled = false;
            close_time2firstinngs.IsEnabled = false;

            txtDuration2firstinngs.IsEnabled = false;
            txtbyes2firstinngs.IsEnabled = false;
            txtlegbyes2firstinngs.IsEnabled = false;
            txtpenalty2firstinngs.IsEnabled = false;
            txttotalextras2firstinngs.IsEnabled = false;
            txtruns2firstinngs.IsEnabled = false;
            txtwickets2firstinngs.IsEnabled = false;
            txtovers2firstinngs.IsEnabled = false;
            dgvbat2firstinngs.IsReadOnly = true;
            dgvbat2secondinngs.IsReadOnly = true;
            txtrunout2firstinngs.IsEnabled = false;
            btn2scorefirstinngs.IsEnabled = false;

            //Team2SecondInnings
            start_time2secondinngs.IsEnabled = false;
            close_time2secondinngs.IsEnabled = false;
            txtDuration2secondinngs.IsEnabled = false;
            txtbyes2secondinngs.IsEnabled = false;
            txtlegbyes2secondinngs.IsEnabled = false;
            txtpenalty2secondinngs.IsEnabled = false;
            txttotalextras2secondinngs.IsEnabled = false;
            txtruns2secondinngs.IsEnabled = false;
            txtwickets2secondinngs.IsEnabled = false;
            txtovers2secondinngs.IsEnabled = false;
            txtrunout2secondinngs.IsEnabled = false;
            dgvbowl2firstinngs.IsReadOnly = true;
            dgvbowl2secondinngs.IsReadOnly = true;
            btn2scoresecondinngs.IsEnabled = false;

            start_time1firstinngs.Text = "";
            close_time1firstinngs.Text = "";
            txtDuration1firstinngs.Text = "0";
            txtbyes1firstinngs.Text = "0";
            txtlegbyes1firstinngs.Text = "0";
            txtpenalty1firstinngs.Text = "0";
            txtrunout1firstinnings.Text = "0";
            txttotalextras1firstinngs.Text = "0";
            txtruns1firstinngs.Text = "0";
            txtwickets1firstinngs.Text = "0";
            txtovers1firstinngs.Text = "0";


            start_time1secondinngs.Text = "";
            close_time1secondinngs.Text = "";
            txtDuration1secondinngs.Text = "0";
            txtbyes1secondinngs.Text = "0";
            txtlegbyes1secondinngs.Text = "0";
            txtpenalty1secondinngs.Text = "0";
            txtrunout1seccondinnings.Text = "0";
            txttotalextras1secondinngs.Text = "0";
            txtruns1secondinngs.Text = "0";
            txtwickets1secondinngs.Text = "0";
            txtovers1secondinngs.Text = "0";




            start_time2firstinngs.Text = "";
            close_time2firstinngs.Text = "";
            txtDuration2firstinngs.Text = "0";
            txtbyes2firstinngs.Text = "0";
            txtlegbyes2firstinngs.Text = "0";
            txtpenalty2firstinngs.Text = "0";
            txtrunout2firstinngs.Text = "0";
            txttotalextras2firstinngs.Text = "0";
            txtruns2firstinngs.Text = "0";
            txtwickets2firstinngs.Text = "0";
            txtovers2firstinngs.Text = "0";

            start_time2secondinngs.Text = "";
            close_time2secondinngs.Text = "";
            txtDuration2secondinngs.Text = "0";
            txtbyes2secondinngs.Text = "0";
            txtlegbyes2secondinngs.Text = "0";
            txtpenalty2secondinngs.Text = "0";
            txtrunout2secondinngs.Text = "0";
            txttotalextras2secondinngs.Text = "0";
            txtruns2secondinngs.Text = "0";
            txtwickets2secondinngs.Text = "0";
            txtovers2secondinngs.Text = "0";

        }

        private void chkmatchnotplayed_Unchecked(object sender, RoutedEventArgs e)
        {
            ismatchnotpalyed = false;

            //Team1FirstInnings
            start_time1firstinngs.IsEnabled = true;
            close_time1firstinngs.IsEnabled = true;
            txtDuration1firstinngs.IsEnabled = true;
            txtbyes1firstinngs.IsEnabled = true;
            txtlegbyes1firstinngs.IsEnabled = true;
            txtpenalty1firstinngs.IsEnabled = true;
            txttotalextras1firstinngs.IsEnabled = true;
            txtruns1firstinngs.IsEnabled = true;
            txtwickets1firstinngs.IsEnabled = true;
            txtovers1firstinngs.IsEnabled = true;
            dgvbat1firstinngs.IsReadOnly = false;
            dgvbowl1firstinngs.IsReadOnly = false;
            txtrunout1firstinnings.IsEnabled = true;
            btn1scorefirstinngs.IsEnabled = true;



            //Team1secinngs
            start_time1secondinngs.IsEnabled = true;
            close_time1secondinngs.IsEnabled = true;
            txtbyes1secondinngs.IsEnabled = true;
            txtDuration1secondinngs.IsEnabled = true;
            txtlegbyes1secondinngs.IsEnabled = true;
            txtpenalty1secondinngs.IsEnabled = true;
            txttotalextras2secondinngs.IsEnabled = true;
            txtruns1secondinngs.IsEnabled = true;
            txtwickets1secondinngs.IsEnabled = true;
            txtovers1secondinngs.IsEnabled = true;
            dgvbat1secondinngs.IsReadOnly = false;
            dgvbowl1secondinngs.IsReadOnly = false;
            txtrunout1seccondinnings.IsEnabled = true;
            btn1scoresecondinngs.IsEnabled = true;


            //Team2FirstInnings
            start_time2firstinngs.IsEnabled = true;
            close_time2firstinngs.IsEnabled = true;

            txtDuration2firstinngs.IsEnabled = true;
            txtbyes2firstinngs.IsEnabled = true;
            txtlegbyes2firstinngs.IsEnabled = true;
            txtpenalty2firstinngs.IsEnabled = true;
            txttotalextras2firstinngs.IsEnabled = true;
            txtruns2firstinngs.IsEnabled = true;
            txtwickets2firstinngs.IsEnabled = true;
            txtovers2firstinngs.IsEnabled = true;
            dgvbat2firstinngs.IsReadOnly = false;
            dgvbat2secondinngs.IsReadOnly = false;
            txtrunout2firstinngs.IsEnabled = true;
            btn2scorefirstinngs.IsEnabled = true;

            //Team2SecondInnings
            start_time2secondinngs.IsEnabled = true;
            close_time2secondinngs.IsEnabled = true;
            txtDuration2secondinngs.IsEnabled = true;
            txtbyes2secondinngs.IsEnabled = true;
            txtlegbyes2secondinngs.IsEnabled = true;
            txtpenalty2secondinngs.IsEnabled = true;
            txttotalextras2secondinngs.IsEnabled = true;
            txtruns2secondinngs.IsEnabled = true;
            txtwickets2secondinngs.IsEnabled = true;
            txtovers2secondinngs.IsEnabled = true;
            txtrunout2secondinngs.IsEnabled = true;
            dgvbowl2firstinngs.IsReadOnly = false;
            dgvbowl2secondinngs.IsReadOnly = false;
            btn2scoresecondinngs.IsEnabled = true;

        }

        private void chkteam1secondinnings_Checked(object sender, RoutedEventArgs e)
        {
            team1secondinningsbattingnotplayed = true;
            team2secondinningsbowlingnotplayed = true;

            //Team1secinngs
            start_time1secondinngs.IsEnabled = false;
            close_time1secondinngs.IsEnabled = false;
            txtbyes1secondinngs.IsEnabled = false;
            txtDuration1secondinngs.IsEnabled = false;
            txtlegbyes1secondinngs.IsEnabled = false;
            txtpenalty1secondinngs.IsEnabled = false;
            txttotalextras1secondinngs.IsEnabled = false;
            txtruns1secondinngs.IsEnabled = false;
            txtwickets1secondinngs.IsEnabled = false;
            txtovers1secondinngs.IsEnabled = false;
            dgvbat1secondinngs.IsReadOnly = true;
            dgvbowl1secondinngs.IsReadOnly = true;
            txtrunout1seccondinnings.IsEnabled = false;
            btn1scoresecondinngs.IsEnabled = false;

            start_time1secondinngs.Text = "";
            close_time1secondinngs.Text = "";
            txtDuration1secondinngs.Text = "0";
            txtbyes1secondinngs.Text = "0";
            txtlegbyes1secondinngs.Text = "0";
            txtpenalty1secondinngs.Text = "0";
            txtrunout1seccondinnings.Text = "0";
            txttotalextras1secondinngs.Text = "0";
            txtruns1secondinngs.Text = "0";
            txtwickets1secondinngs.Text = "0";
            txtovers1secondinngs.Text = "0";



        }

        private void chkteam1secondinnings_Unchecked(object sender, RoutedEventArgs e)
        {
            team1secondinningsbattingnotplayed = false;
            team2secondinningsbowlingnotplayed = false;

            //Team1secinngs
            start_time1secondinngs.IsEnabled = true;
            close_time1secondinngs.IsEnabled = true;
            txtbyes1secondinngs.IsEnabled = true;
            txtDuration1secondinngs.IsEnabled = true;
            txtlegbyes1secondinngs.IsEnabled = true;
            txtpenalty1secondinngs.IsEnabled = true;
            txttotalextras2secondinngs.IsEnabled = true;
            txtruns1secondinngs.IsEnabled = true;
            txtwickets1secondinngs.IsEnabled = true;
            txtovers1secondinngs.IsEnabled = true;
            dgvbat1secondinngs.IsReadOnly = false;
            dgvbowl1secondinngs.IsReadOnly = false;
            txtrunout1seccondinnings.IsEnabled = true;
            btn1scoresecondinngs.IsEnabled = true;
        }

        private void chkteam2firstinnings_Checked(object sender, RoutedEventArgs e)
        {
            team2firstinningsbattingnotplayed = true;
            team1firstinningsbowlingnotplayed = true;
            //Team2FirstInnings
            start_time2firstinngs.IsEnabled = false;
            close_time2firstinngs.IsEnabled = false;

            txtDuration2firstinngs.IsEnabled = false;
            txtbyes2firstinngs.IsEnabled = false;
            txtlegbyes2firstinngs.IsEnabled = false;
            txtpenalty2firstinngs.IsEnabled = false;
            txttotalextras2firstinngs.IsEnabled = false;
            txtruns2firstinngs.IsEnabled = false;
            txtwickets2firstinngs.IsEnabled = false;
            txtovers2firstinngs.IsEnabled = false;
            dgvbat2firstinngs.IsReadOnly = true;
            dgvbowl2firstinngs.IsReadOnly = true;
            txtrunout2firstinngs.IsEnabled = false;
            btn2scorefirstinngs.IsEnabled = false;

            start_time2firstinngs.Text = "";
            close_time2firstinngs.Text = "";
            txtDuration2firstinngs.Text = "0";
            txtbyes2firstinngs.Text = "0";
            txtlegbyes2firstinngs.Text = "0";
            txtpenalty2firstinngs.Text = "0";
            txtrunout2firstinngs.Text = "0";
            txttotalextras2firstinngs.Text = "0";
            txtruns2firstinngs.Text = "0";
            txtwickets2firstinngs.Text = "0";
            txtovers2firstinngs.Text = "0";
        }

        private void chkteam2firstinnings_Unchecked(object sender, RoutedEventArgs e)
        {
            team2firstinningsbattingnotplayed = false;
            team1firstinningsbowlingnotplayed = false;

            //Team2FirstInnings
            start_time2firstinngs.IsEnabled = true;
            close_time2firstinngs.IsEnabled = true;

            txtDuration2firstinngs.IsEnabled = true;
            txtbyes2firstinngs.IsEnabled = true;
            txtlegbyes2firstinngs.IsEnabled = true;
            txtpenalty2firstinngs.IsEnabled = true;
            txttotalextras2firstinngs.IsEnabled = true;
            txtruns2firstinngs.IsEnabled = true;
            txtwickets2firstinngs.IsEnabled = true;
            txtovers2firstinngs.IsEnabled = true;
            dgvbat2firstinngs.IsReadOnly = false;
            dgvbowl2firstinngs.IsReadOnly = false;
            txtrunout2firstinngs.IsEnabled = true;
            btn2scorefirstinngs.IsEnabled = true;
        }

        private void chkteam2secondinnings_Checked(object sender, RoutedEventArgs e)
        {
            team2secondbattingnotplayed = true;
            team1secondinningsbowlingnotplayed = true;

            //Team2SecondInnings
            start_time2secondinngs.IsEnabled = false;
            close_time2secondinngs.IsEnabled = false;
            txtDuration2secondinngs.IsEnabled = false;
            txtbyes2secondinngs.IsEnabled = false;
            txtlegbyes2secondinngs.IsEnabled = false;
            txtpenalty2secondinngs.IsEnabled = false;
            txttotalextras2secondinngs.IsEnabled = false;
            txtruns2secondinngs.IsEnabled = false;
            txtwickets2secondinngs.IsEnabled = false;
            txtovers2secondinngs.IsEnabled = false;
            txtrunout2secondinngs.IsEnabled = false;
            dgvbat2secondinngs.IsReadOnly = true;
            dgvbowl2secondinngs.IsReadOnly = true;
            btn2scoresecondinngs.IsEnabled = false;

            start_time2secondinngs.Text = "";
            close_time2secondinngs.Text = "";
            txtDuration2secondinngs.Text = "0";
            txtbyes2secondinngs.Text = "0";
            txtlegbyes2secondinngs.Text = "0";
            txtpenalty2secondinngs.Text = "0";
            txtrunout2secondinngs.Text = "0";
            txttotalextras2secondinngs.Text = "0";
            txtruns2secondinngs.Text = "0";
            txtwickets2secondinngs.Text = "0";
            txtovers2secondinngs.Text = "0";
        }

        private void chkteam2secondinnings_Unchecked(object sender, RoutedEventArgs e)
        {

            team2secondbattingnotplayed = false;
            team1secondinningsbowlingnotplayed = false;

            //Team2SecondInnings
            start_time2secondinngs.IsEnabled = true;
            close_time2secondinngs.IsEnabled = true;
            txtDuration2secondinngs.IsEnabled = true;
            txtbyes2secondinngs.IsEnabled = true;
            txtlegbyes2secondinngs.IsEnabled = true;
            txtpenalty2secondinngs.IsEnabled = true;
            txttotalextras2secondinngs.IsEnabled = true;
            txtruns2secondinngs.IsEnabled = true;
            txtwickets2secondinngs.IsEnabled = true;
            txtovers2secondinngs.IsEnabled = true;
            txtrunout2secondinngs.IsEnabled = true;
            dgvbat2secondinngs.IsReadOnly = false;
            dgvbowl2secondinngs.IsReadOnly = false;
            btn2scoresecondinngs.IsEnabled = true;
        }




        private void btnsave_Click(object sender, RoutedEventArgs e)
        {
            _isSaveClick = true;
            //observable collections
            ObservableCollection<Player> objplayer1 = Database.GetEntityList<Player>(false, false, false, oleconn, "RecordStatus", "FirstName");
            ObservableCollection<Team> objteams = Database.GetEntityList<Team>(false, true, true, oleconn, "RecordStatus", "TeamName");
            ObservableCollection<FirstDivPointsCricket> objFirstDivPointsCricket = Database.GetEntityList<FirstDivPointsCricket>(false, true, true, oleconn, "RecordStatus", "TeamName");
            ObservableCollection<Location> objlocationnames = Database.GetEntityList<Location>(false, true, true, oleconn, "RecordStatus", "LocationName");
            ObservableCollection<BestBatsman> objbat1 = Database.GetEntityList<BestBatsman>(false, false, false, oleconn, "RecordStatus", "PlayerName");
            ObservableCollection<BestBowler> objbowl1 = Database.GetEntityList<BestBowler>(false, false, false, oleconn, "RecordStatus", "PlayerName");



            progessbar.Visibility = Visibility.Visible;




            ///////////////////////////////////////////////

            // Team Report Team 1

            TeamReportTable ObjTeamReport1 = Database.GetNewEntity<TeamReportTable>();


            foreach (Team objTeam in objteams)
            {

                if (objTeam.TeamName.ToString() == tempvalues.teamA)
                {
                    ObjTeamReport1.TeamId = objTeam;
                    ObjTeamReport1.TeamName = objTeam.TeamName;
                    break;
                }
            }


            ObjTeamReport1.SeasonId = selectedgridvalue._seasonid;

            ObjTeamReport1.DivisionId = selectedgridvalue.LoadDivisionId;

            ObjTeamReport1.Opponent = lblbowl1firstinngs.Content.ToString();

            if (comboboxvalue.value[0] == "0")
            {
                ObjTeamReport1.Toss = "WON";
            }
            else
            {
                ObjTeamReport1.Toss = "LOST";
            }

            ObjTeamReport1.Venue = NameOf.Venue;

            ObjTeamReport1.RunsScored = Convert.ToInt16(Convert.ToInt16(txtruns1firstinngs.Text) + Convert.ToInt16(txtruns1secondinngs.Text));
            ObjTeamReport1.OversFaced = addovers(Convert.ToDecimal(txtovers1firstinngs.Text), Convert.ToDecimal(txtovers1secondinngs.Text));

            ObjTeamReport1.RunsConceded = Convert.ToInt16(Convert.ToInt16(txtruns2firstinngs.Text) + Convert.ToInt16(txtruns2secondinngs.Text));
            ObjTeamReport1.OversBowled = addovers(Convert.ToDecimal(txtovers2firstinngs.Text), Convert.ToDecimal(txtovers2secondinngs.Text));

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


            ObjTeamReport2.SeasonId = selectedgridvalue._seasonid;

            ObjTeamReport2.DivisionId = selectedgridvalue.LoadDivisionId;

            ObjTeamReport2.Opponent = lblbat1firstinngs.Content.ToString();

            if (comboboxvalue.value[0] == "1")
            {
                ObjTeamReport2.Toss = "WON";
            }
            else
            {
                ObjTeamReport2.Toss = "LOST";
            }


            ObjTeamReport2.Venue = NameOf.Venue;

            ObjTeamReport2.RunsScored = Convert.ToInt16(Convert.ToInt16(txtruns2firstinngs.Text) + Convert.ToInt16(txtruns2secondinngs.Text));
            ObjTeamReport2.OversFaced = addovers(Convert.ToDecimal(txtovers2firstinngs.Text), Convert.ToDecimal(txtovers2secondinngs.Text));

            ObjTeamReport2.RunsConceded = Convert.ToInt16(Convert.ToInt16(txtruns1firstinngs.Text) + Convert.ToInt16(txtruns1secondinngs.Text));
            ObjTeamReport2.OversBowled = addovers(Convert.ToDecimal(txtovers1firstinngs.Text), Convert.ToDecimal(txtovers1secondinngs.Text));

            ObjTeamReport2.Points = Convert.ToInt16(TeamB.Points);
            ObjTeamReport2.Result = Match.Result;
            ObjTeamReport2.Remarks = Remark.Remarks;




            /////////////////////////////////////


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


            ObjLocationReport.SeasonId = selectedgridvalue._seasonid;

            //////////////////////////////////////////////

            progessbar.Value = 25;

            ////////////////////////////////////////////
            DataTable dt = new DataTable();

            dt.Columns.Add("SLNOA");
            dt.Columns.Add("Name Of PlayersA");
            dt.Columns.Add("KSCA UIDA");

            dt.Columns.Add("DismissalBAT1FI");
            dt.Columns.Add("RunsBAT1FI");
            dt.Columns.Add("MinsBAT1FI");
            dt.Columns.Add("BallsBAT1FI");
            dt.Columns.Add("FoursBAT1FI");
            dt.Columns.Add("SixesBAT1FI");

            dt.Columns.Add("DismissalBAT1SI");
            dt.Columns.Add("RunsBAT1SI");
            dt.Columns.Add("MinsBAT1SI");
            dt.Columns.Add("BallsBAT1SI");
            dt.Columns.Add("FoursBAT1SI");
            dt.Columns.Add("SixesBAT1SI");

            dt.Columns.Add("OversBOWL2FI");
            dt.Columns.Add("MaidensBOWL2FI");
            dt.Columns.Add("BRunsBOWL2FI");
            dt.Columns.Add("WicketsBOWL2FI");
            dt.Columns.Add("No BallBOWL2FI");
            dt.Columns.Add("WideBOWL2FI");
            dt.Columns.Add("AvgBOWL2FI");

            dt.Columns.Add("OversBOWL2SI");
            dt.Columns.Add("MaidensBOWL2SI");
            dt.Columns.Add("BRunsBOWL2SI");
            dt.Columns.Add("WicketsBOWL2SI");
            dt.Columns.Add("No BallBOWL2SI");
            dt.Columns.Add("WideBOWL2SI");
            dt.Columns.Add("AvgBOWL2SI");

            //////////////////////////////////////////

            dt.Columns.Add("SLNOB");
            dt.Columns.Add("Name Of PlayersB");
            dt.Columns.Add("KSCA UIDB");

            dt.Columns.Add("DismissalBAT2FI");
            dt.Columns.Add("RunsBAT2FI");
            dt.Columns.Add("MinsBAT2FI");
            dt.Columns.Add("BallsBAT2FI");
            dt.Columns.Add("FoursBAT2FI");
            dt.Columns.Add("SixesBAT2FI");

            dt.Columns.Add("DismissalBAT2SI");
            dt.Columns.Add("RunsBAT2SI");
            dt.Columns.Add("MinsBAT2SI");
            dt.Columns.Add("BallsBAT2SI");
            dt.Columns.Add("FoursBAT2SI");
            dt.Columns.Add("SixesBAT2SI");

            dt.Columns.Add("OversBOWL1FI");
            dt.Columns.Add("MaidensBOWL1FI");
            dt.Columns.Add("BRunsBOWL1FI");
            dt.Columns.Add("WicketsBOWL1FI");
            dt.Columns.Add("No BallBOWL1FI");
            dt.Columns.Add("WideBOWL1FI");
            dt.Columns.Add("AvgBOWL1FI");

            dt.Columns.Add("OversBOWL1SI");
            dt.Columns.Add("MaidensBOWL1SI");
            dt.Columns.Add("BRunsBOWL1SI");
            dt.Columns.Add("WicketsBOWL1SI");
            dt.Columns.Add("No BallBOWL1SI");
            dt.Columns.Add("WideBOWL1SI");
            dt.Columns.Add("AvgBOWL1SI");

            DataTable dtBAT1FI = new DataTable();
            dtBAT1FI = ((DataView)dgvbat1firstinngs.ItemsSource).ToTable();

            //dgvbat1firstinngs.DataContext as DataTable;

            DataTable dtBOWL1FI = new DataTable();
            dtBOWL1FI = ((DataView)dgvbowl1firstinngs.ItemsSource).ToTable();

            // dgvbowl1firstinngs.DataContext as DataTable;

            DataTable dtBAT1SI = new DataTable();
            dtBAT1SI = ((DataView)dgvbat1secondinngs.ItemsSource).ToTable();
            //dgvbat1secondinngs.DataContext as DataTable;

            DataTable dtBOWL1SI = new DataTable();
            dtBOWL1SI = ((DataView)dgvbowl1secondinngs.ItemsSource).ToTable();
            //dgvbowl1secondinngs.DataContext as DataTable;
            DataTable dtBAT2FI = new DataTable();
            dtBAT2FI = ((DataView)dgvbat2firstinngs.ItemsSource).ToTable();
            //dgvbat2firstinngs.DataContext as DataTable;
            DataTable dtBOWL2FI = new DataTable();
            dtBOWL2FI = ((DataView)dgvbowl2firstinngs.ItemsSource).ToTable();
            //dgvbowl2firstinngs.DataContext as DataTable;
            DataTable dtBAT2SI = new DataTable();
            dtBAT2SI = ((DataView)dgvbat2secondinngs.ItemsSource).ToTable();
            // dgvbat2secondinngs.DataContext as DataTable;
            DataTable dtBOWL2SI = new DataTable();
            dtBOWL2SI = ((DataView)dgvbowl2secondinngs.ItemsSource).ToTable();
            //dgvbowl2secondinngs.DataContext as DataTable;

            var JoinResult1 = (from BAT1FI in dtBAT1FI.AsEnumerable()
                               join BAT1SI in dtBAT1SI.AsEnumerable() on BAT1FI.Field<string>("SLNO") equals BAT1SI.Field<string>("SLNO")
                               join BOWL2FI in dtBOWL2FI.AsEnumerable() on BAT1FI.Field<string>("SLNO") equals BOWL2FI.Field<string>("SLNO")
                               join BOWL2SI in dtBOWL2SI.AsEnumerable() on BAT1FI.Field<string>("SLNO") equals BOWL2SI.Field<string>("SLNO")

                               join BAT2FI in dtBAT2FI.AsEnumerable() on BAT1FI.Field<string>("SLNO") equals BAT2FI.Field<string>("SLNO")
                               join BAT2SI in dtBAT2SI.AsEnumerable() on BAT1FI.Field<string>("SLNO") equals BAT2SI.Field<string>("SLNO")
                               join BOWL1FI in dtBOWL1FI.AsEnumerable() on BAT1FI.Field<string>("SLNO") equals BOWL1FI.Field<string>("SLNO")
                               join BOWL1SI in dtBOWL1SI.AsEnumerable() on BAT1FI.Field<string>("SLNO") equals BOWL1SI.Field<string>("SLNO")

                               select new
                               {
                                   serialnumberA = BAT1FI.Field<string>("SLNO"),
                                   playernameA = BAT1FI.Field<string>("Name Of Players"),
                                   kscauidA = BAT1FI.Field<string>("KSCA UID"),

                                   dismissalBAT1FI = BAT1FI.Field<string>("Dismissal"),
                                   battingrunsBAT1FI = BAT1FI.Field<string>("Runs"),
                                   minsBAT1FI = BAT1FI.Field<string>("Mins"),
                                   ballsBAT1FI = BAT1FI.Field<string>("Balls"),
                                   foursBAT1FI = BAT1FI.Field<string>("Fours"),
                                   sixesBAT1FI = BAT1FI.Field<string>("Sixes"),

                                   dismissalBAT1SI = BAT1SI.Field<string>("Dismissal"),
                                   battingrunsBAT1SI = BAT1SI.Field<string>("Runs"),
                                   minsBAT1SI = BAT1SI.Field<string>("Mins"),
                                   ballsBAT1SI = BAT1SI.Field<string>("Balls"),
                                   foursBAT1SI = BAT1SI.Field<string>("Fours"),
                                   sixesBAT1SI = BAT1SI.Field<string>("Sixes"),

                                   oversBOWL2FI = BOWL2FI.Field<string>("Overs"),
                                   MaidensBOWL2FI = BOWL2FI.Field<string>("Maidens"),
                                   ballingrunsBOWL2FI = BOWL2FI.Field<string>("Runs"),
                                   wicketsBOWL2FI = BOWL2FI.Field<string>("Wickets"),
                                   noballsBOWL2FI = BOWL2FI.Field<string>("No Ball"),
                                   widesBOWL2FI = BOWL2FI.Field<string>("Wide"),
                                   averageBOWL2FI = BOWL2FI.Field<string>("Avg"),

                                   oversBOWL2SI = BOWL2SI.Field<string>("Overs"),
                                   MaidensBOWL2SI = BOWL2SI.Field<string>("Maidens"),
                                   ballingrunsBOWL2SI = BOWL2SI.Field<string>("Runs"),
                                   wicketsBOWL2SI = BOWL2SI.Field<string>("Wickets"),
                                   noballsBOWL2SI = BOWL2SI.Field<string>("No Ball"),
                                   widesBOWL2SI = BOWL2SI.Field<string>("Wide"),
                                   averageBOWL2SI = BOWL2SI.Field<string>("Avg"),

                                   //////////////////////////////////////////////////////////////


                                   serialnumberB = BAT2FI.Field<string>("SLNO"),
                                   playernameB = BAT2FI.Field<string>("Name Of Players"),
                                   kscauidB = BAT2FI.Field<string>("KSCA UID"),

                                   dismissalBAT2FI = BAT2FI.Field<string>("Dismissal"),
                                   battingrunsBAT2FI = BAT2FI.Field<string>("Runs"),
                                   minsBAT2FI = BAT2FI.Field<string>("Mins"),
                                   ballsBAT2FI = BAT2FI.Field<string>("Balls"),
                                   foursBAT2FI = BAT2FI.Field<string>("Fours"),
                                   sixesBAT2FI = BAT2FI.Field<string>("Sixes"),

                                   dismissalBAT2SI = BAT2FI.Field<string>("Dismissal"),
                                   battingrunsBAT2SI = BAT2SI.Field<string>("Runs"),
                                   minsBAT2SI = BAT2SI.Field<string>("Mins"),
                                   ballsBAT2SI = BAT2SI.Field<string>("Balls"),
                                   foursBAT2SI = BAT2SI.Field<string>("Fours"),
                                   sixesBAT2SI = BAT2SI.Field<string>("Sixes"),

                                   oversBOWL1FI = BOWL1FI.Field<string>("Overs"),
                                   MaidensBOWL1FI = BOWL1FI.Field<string>("Maidens"),
                                   ballingrunsBOWL1FI = BOWL1FI.Field<string>("Runs"),
                                   wicketsBOWL1FI = BOWL1FI.Field<string>("Wickets"),
                                   noballsBOWL1FI = BOWL1FI.Field<string>("No Ball"),
                                   widesBOWL1FI = BOWL1FI.Field<string>("Wide"),
                                   averageBOWL1FI = BOWL1FI.Field<string>("Avg"),

                                   oversBOWL1SI = BOWL1SI.Field<string>("Overs"),
                                   MaidensBOWL1SI = BOWL1SI.Field<string>("Maidens"),
                                   ballingrunsBOWL1SI = BOWL1SI.Field<string>("Runs"),
                                   wicketsBOWL1SI = BOWL1SI.Field<string>("Wickets"),
                                   noballsBOWL1SI = BOWL1SI.Field<string>("No Ball"),
                                   widesBOWL1SI = BOWL1SI.Field<string>("Wide"),
                                   averageBOWL1SI = BOWL1SI.Field<string>("Avg"),

                               }).ToList();



            foreach (var data in JoinResult1)
            {

                DataRow dtr = dt.NewRow();
                dtr["SLNOA"] = data.serialnumberA;
                dtr["Name Of PlayersA"] = data.playernameA;
                dtr["KSCA UIDA"] = data.kscauidA;

                dtr["DismissalBAT1FI"] = data.dismissalBAT1FI;
                dtr["RunsBAT1FI"] = data.battingrunsBAT1FI;
                dtr["MinsBAT1FI"] = data.minsBAT1FI;
                dtr["BallsBAT1FI"] = data.ballsBAT1FI;
                dtr["FoursBAT1FI"] = data.foursBAT1FI;
                dtr["SixesBAT1FI"] = data.sixesBAT1FI;

                dtr["DismissalBAT1SI"] = data.dismissalBAT1SI;
                dtr["RunsBAT1SI"] = data.battingrunsBAT1SI;
                dtr["MinsBAT1SI"] = data.minsBAT1SI;
                dtr["BallsBAT1SI"] = data.ballsBAT1SI;
                dtr["FoursBAT1SI"] = data.foursBAT1SI;
                dtr["SixesBAT1SI"] = data.sixesBAT1SI;

                dtr["OversBOWL2FI"] = data.oversBOWL2FI;
                dtr["MaidensBOWL2FI"] = data.MaidensBOWL2FI;
                dtr["BrunsBOWL2FI"] = data.ballingrunsBOWL2FI;
                dtr["WicketsBOWL2FI"] = data.wicketsBOWL2FI;
                dtr["No BallBOWL2FI"] = data.noballsBOWL2FI;
                dtr["WideBOWL2FI"] = data.widesBOWL2FI;
                dtr["AvgBOWL2FI"] = data.averageBOWL2FI;

                dtr["OversBOWL2SI"] = data.oversBOWL2SI;
                dtr["MaidensBOWL2SI"] = data.MaidensBOWL2SI;
                dtr["BrunsBOWL2SI"] = data.ballingrunsBOWL2SI;
                dtr["WicketsBOWL2SI"] = data.wicketsBOWL2SI;
                dtr["No BallBOWL2SI"] = data.noballsBOWL2SI;
                dtr["WideBOWL2SI"] = data.widesBOWL2SI;
                dtr["AvgBOWL2SI"] = data.averageBOWL2SI;

                ////////////////////////////////////////////////

                dtr["SLNOB"] = data.serialnumberB;
                dtr["Name Of PlayersB"] = data.playernameB;
                dtr["KSCA UIDB"] = data.kscauidB;

                dtr["DismissalBAT2FI"] = data.dismissalBAT2FI;
                dtr["RunsBAT2FI"] = data.battingrunsBAT2FI;
                dtr["MinsBAT2FI"] = data.minsBAT2FI;
                dtr["BallsBAT2FI"] = data.ballsBAT2FI;
                dtr["FoursBAT2FI"] = data.foursBAT2FI;
                dtr["SixesBAT2FI"] = data.sixesBAT2FI;

                dtr["DismissalBAT2SI"] = data.dismissalBAT2SI;
                dtr["RunsBAT2SI"] = data.battingrunsBAT2SI;
                dtr["MinsBAT2SI"] = data.minsBAT2SI;
                dtr["BallsBAT2SI"] = data.ballsBAT2SI;
                dtr["FoursBAT2SI"] = data.foursBAT2SI;
                dtr["SixesBAT2SI"] = data.sixesBAT2SI;

                dtr["OversBOWL1FI"] = data.oversBOWL1FI;
                dtr["MaidensBOWL1FI"] = data.MaidensBOWL1FI;
                dtr["BrunsBOWL1FI"] = data.ballingrunsBOWL1FI;
                dtr["WicketsBOWL1FI"] = data.wicketsBOWL1FI;
                dtr["No BallBOWL1FI"] = data.noballsBOWL1FI;
                dtr["WideBOWL1FI"] = data.widesBOWL1FI;
                dtr["AvgBOWL1FI"] = data.averageBOWL1FI;

                dtr["OversBOWL1SI"] = data.oversBOWL1SI;
                dtr["MaidensBOWL1SI"] = data.MaidensBOWL1SI;
                dtr["BrunsBOWL1SI"] = data.ballingrunsBOWL1SI;
                dtr["WicketsBOWL1SI"] = data.wicketsBOWL1SI;
                dtr["No BallBOWL1SI"] = data.noballsBOWL1SI;
                dtr["WideBOWL1SI"] = data.widesBOWL1SI;
                dtr["AvgBOWL1SI"] = data.averageBOWL1SI;



                dt.Rows.Add(dtr);

            }


            team1runs1 = Convert.ToInt16(txtruns1firstinngs.Text);
            team1runs2 = Convert.ToInt16(txtruns1secondinngs.Text);
            team2runs1 = Convert.ToInt16(txtruns2firstinngs.Text);
            team2runs2 = Convert.ToInt16(txtruns2secondinngs.Text);

            if (txtwickets1firstinngs.Text == "10")
            {
                team1overs1 = 90;
            }

            else
            {
                team1overs1 = Convert.ToDecimal(txtovers1firstinngs.Text);
            }

            if (txtwickets1secondinngs.Text == "10")
            {
                team1overs2 = 90;
            }
            else
            {
                team1overs2 = Convert.ToDecimal(txtovers1secondinngs.Text);
            }

            if (txtwickets2firstinngs.Text == "10")
            {
                team2overs1 = 90;
            }
            else
            {
                team2overs1 = Convert.ToDecimal(txtovers2firstinngs.Text);
            }


            if (txtwickets2secondinngs.Text == "10")
            {
                team2overs2 = 90;
            }
            else
            {
                team2overs2 = Convert.ToDecimal(txtovers2secondinngs.Text);
            }

            //  update team 1

            bool exist = checkteam();
            bool TeamFound = false;

            if (team0exit == true)
            {
                foreach (Team objTeam in objteams)
                {
                    if (objTeam.TeamName.ToString() == tempvalues.teamA)
                    {

                        foreach (FirstDivPointsCricket obcteam in objFirstDivPointsCricket)
                        {
                            obcteam.SeasonId = selectedgridvalue._seasonid;

                            obcteam.DivisionId = selectedgridvalue.LoadDivisionId;

                            if ((obcteam.TeamName.ToString() == tempvalues.teamA))
                            {
                                obcteam.Matches++;
                                obcteam.TeamId = objTeam;
                                obcteam.Points = Convert.ToInt16(obcteam.Points + Convert.ToInt16(TeamA.Points));
                                obcteam.ForRuns = Convert.ToInt16(obcteam.ForRuns + team1runs1 + team1runs2);
                                obcteam.AgainstRuns = Convert.ToInt16(obcteam.AgainstRuns + team2runs1 + team2runs2);

                                decimal forovers = obcteam.ForOvers;
                                decimal againstovers = obcteam.AgainstOvers;

                                decimal overs1 = addovers(team1overs1, team1overs2);
                                decimal overs2 = addovers(team2overs1, team2overs2);


                                obcteam.ForOvers = addovers(forovers, overs1);
                                obcteam.AgainstOvers = addovers(againstovers, overs2);

                                obcteam.For = obcteam.ForRuns + "/" + obcteam.ForOvers;
                                obcteam.Against = obcteam.AgainstRuns + "/" + obcteam.AgainstOvers;


                                if (TeamA.Result == "WON")
                                {
                                    obcteam.Won++;
                                }

                                else if (TeamA.Result == "LOST")
                                {
                                    obcteam.Lost++;

                                }
                                else if (TeamA.Result == "DRAW")
                                {
                                    obcteam.Tie++;

                                }


                                obcteam.RunRate = ((runratecalculation(obcteam.ForRuns, obcteam.ForOvers)) - (runratecalculation(obcteam.AgainstRuns, obcteam.AgainstOvers)));

                                Database.SaveEntity<FirstDivPointsCricket>(obcteam, oleconn);
                                progessbar.Value = 25;
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
            //  update team 2

            bool TeamFound2 = false;
            if (team1exit == true)
            {
                foreach (Team objTeam in objteams)
                {
                    if (objTeam.TeamName.ToString() == tempvalues.teamB)
                    {
                        foreach (FirstDivPointsCricket obcteam in objFirstDivPointsCricket)
                        {
                            obcteam.SeasonId = selectedgridvalue._seasonid;

                            obcteam.DivisionId = selectedgridvalue.LoadDivisionId;

                            if ((obcteam.TeamName.ToString() == tempvalues.teamB))
                            {
                                obcteam.Matches++;
                                obcteam.TeamId = objTeam;
                                obcteam.Points = Convert.ToInt16(obcteam.Points + Convert.ToInt16(TeamB.Points));
                                obcteam.ForRuns = Convert.ToInt16(obcteam.ForRuns + team2runs1 + team2runs2);
                                obcteam.AgainstRuns = Convert.ToInt16(obcteam.AgainstRuns + team1runs1 + team1runs2);

                                decimal forovers = obcteam.ForOvers;
                                decimal againstovers = obcteam.AgainstOvers;

                                decimal overs1 = addovers(team1overs1, team1overs2);
                                decimal overs2 = addovers(team2overs1, team2overs2);


                                obcteam.ForOvers = addovers(forovers, overs2);
                                obcteam.AgainstOvers = addovers(againstovers, overs1);

                                obcteam.For = obcteam.ForRuns + "/" + obcteam.ForOvers;
                                obcteam.Against = obcteam.AgainstRuns + "/" + obcteam.AgainstOvers;


                                if (TeamA.Result == "WON")
                                {
                                    obcteam.Won++;
                                }

                                else if (TeamA.Result == "LOST")
                                {
                                    obcteam.Lost++;

                                }
                                else if (TeamA.Result == "DRAW")
                                {
                                    obcteam.Tie++;

                                }


                                obcteam.RunRate = ((runratecalculation(obcteam.ForRuns, obcteam.ForOvers)) - (runratecalculation(obcteam.AgainstRuns, obcteam.AgainstOvers)));

                                Database.SaveEntity<FirstDivPointsCricket>(obcteam, oleconn);

                                TeamFound2 = true;
                                progessbar.Value = 25;
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

                        FirstDivPointsCricket objpc = Database.GetNewEntity<FirstDivPointsCricket>();

                        objpc.SeasonId = selectedgridvalue._seasonid;
                        objpc.DivisionId = selectedgridvalue.LoadDivisionId;
                        objpc.TeamId = objTeam;
                        objpc.TeamName = tempvalues.teamA;
                        objpc.Matches = 1;
                        objpc.Group = selectedgridvalue.group;
                        objpc.Points = Convert.ToInt16(TeamA.Points);

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


                        objpc.ForRuns = Convert.ToInt16(team1runs1 + team1runs2);
                        objpc.AgainstRuns = Convert.ToInt16(team2runs1 + team2runs2);

                        objpc.ForOvers = addovers(team1overs1, team1overs2);
                        objpc.AgainstOvers = addovers(team2overs1, team2overs2);

                        objpc.For = objpc.ForRuns + "/" + objpc.ForOvers;
                        objpc.Against = objpc.AgainstRuns + "/" + objpc.AgainstOvers;

                        decimal team1batting = team1runs1 + team1runs2;
                        decimal team1bowling = addovers(team1overs1, team1overs2);

                        decimal team2batting = team2runs1 + team2runs2;
                        decimal team2bowling = addovers(team2overs1, team2overs2);

                        objpc.RunRate = ((runratecalculation(team1batting, team1bowling)) - (runratecalculation(team2batting, team2bowling)));

                        Database.SaveEntity<FirstDivPointsCricket>(objpc, oleconn);
                        TeamFound = true;
                        progessbar.Value = 25;
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
                        FirstDivPointsCricket objpc = Database.GetNewEntity<FirstDivPointsCricket>();
                        objpc.SeasonId = selectedgridvalue._seasonid;
                        objpc.DivisionId = selectedgridvalue.LoadDivisionId;
                        objpc.TeamId = objTeam;
                        objpc.TeamName = tempvalues.teamB;
                        objpc.Matches = 1;
                        objpc.Group = selectedgridvalue.group;
                        objpc.Points = Convert.ToInt16(TeamB.Points);

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


                        objpc.ForRuns = Convert.ToInt16(team2runs1 + team2runs2);
                        objpc.AgainstRuns = Convert.ToInt16(team1runs1 + team1runs2);

                        objpc.ForOvers = addovers(team2overs1, team2overs2);
                        objpc.AgainstOvers = addovers(team1overs1, team1overs2);

                        objpc.For = objpc.ForRuns + "/" + objpc.ForOvers;
                        objpc.Against = objpc.AgainstRuns + "/" + objpc.AgainstOvers;

                        decimal team1batting = team1runs1 + team1runs2;
                        decimal team1bowling = addovers(team1overs1, team1overs2);

                        decimal team2batting = team2runs1 + team2runs2;
                        decimal team2bowling = addovers(team2overs1, team2overs2);

                        objpc.RunRate = ((runratecalculation(team2batting, team2bowling)) - (runratecalculation(team1batting, team1bowling)));
                        Database.SaveEntity<FirstDivPointsCricket>(objpc, oleconn);

                        TeamFound = true;
                        progessbar.Value = 25;

                    }

                    else if (TeamFound == true)
                    {
                        break;
                    }
                }
            }

            /////////////





            // add              Batting 1 

            for (int i = 0; i <= (firstinng1count1 - 1); i++)
            {
                string playerkscauid = Convert.ToString((dgvbat1firstinngs.Items[i] as DataRowView).Row.ItemArray[2]);
                checkplayerbt1(playerkscauid);

                if (playerexitbt1 == false)
                {

                    if (!DBNull.Value.Equals((dgvbat1firstinngs.Items[i] as DataRowView).Row.ItemArray[1]) && ((dgvbat1firstinngs.Items[i] as DataRowView).Row.ItemArray[1]).ToString() != "")
                    {
                        string playername;
                        string id = Convert.ToString((dgvbat1firstinngs.Items[i] as DataRowView).Row.ItemArray[1]);
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

                                objbt1.SeasonId = selectedgridvalue._seasonid;
                                objbt1.DivisionId = selectedgridvalue.LoadDivisionId;
                                objbt1.Player = obj;
                                objbt1.PlayerName = playername;
                                objbt1.KSCAUID = obj.KSCAUID;

                                objbt1.NotOut = 0;
                                objbt1.Fifties = 0;
                                objbt1.Hundreds = 0;
                                objbt1.RunsScored = 0;
                                objbt1.BallsFaced = 0;
                                objbt1.HighestScore = 0;
                                objbt1.StrikeRate = 0;
                                objbt1.Fours = 0;
                                objbt1.Sixes = 0;


                                foreach (Team objteam in objteams)
                                {
                                    if (obj.TeamIdId == objteam.TeamId)
                                    {
                                        objbt1.TeamName = objteam.TeamName;
                                    }
                                }


                                if (!(chkmatchnotplayed.IsChecked == true))
                                {

                                    objbt1.Matches = 1;
                                    objbt1.Innings = 1;



                                    if (!DBNull.Value.Equals((dgvbat1firstinngs.Items[i] as DataRowView).Row.ItemArray[3]) && ((dgvbat1firstinngs.Items[i] as DataRowView).Row.ItemArray[3]).ToString() != "")
                                    {
                                        if (Convert.ToString((dgvbat1firstinngs.Items[i] as DataRowView).Row.ItemArray[3]) == "NOT OUT" || Convert.ToString((dgvbat1firstinngs.Items[i] as DataRowView).Row.ItemArray[3]) == "not out" || Convert.ToString((dgvbat1firstinngs.Items[i] as DataRowView).Row.ItemArray[3]) == "n.o" || Convert.ToString((dgvbat1firstinngs.Items[i] as DataRowView).Row.ItemArray[3]) == "N.O" || Convert.ToString((dgvbat1firstinngs.Items[i] as DataRowView).Row.ItemArray[3]) == "no" || Convert.ToString((dgvbat1firstinngs.Items[i] as DataRowView).Row.ItemArray[3]) == "NO")
                                        {
                                            objbt1.NotOut++;
                                        }
                                    }

                                    int runs1 = 0;


                                    if (!DBNull.Value.Equals((dgvbat1firstinngs.Items[i] as DataRowView).Row.ItemArray[6]) && ((dgvbat1firstinngs.Items[i] as DataRowView).Row.ItemArray[6]).ToString() != "")
                                    {
                                        objbt1.BallsFaced = Convert.ToInt16(objbt1.BallsFaced + Convert.ToInt16((dgvbat1firstinngs.Items[i] as DataRowView).Row.ItemArray[6]));
                                    }


                                    if (!DBNull.Value.Equals((dgvbat1firstinngs.Items[i] as DataRowView).Row.ItemArray[4]) && ((dgvbat1firstinngs.Items[i] as DataRowView).Row.ItemArray[4]).ToString() != "")
                                    {
                                        if ((Convert.ToString((dgvbat1firstinngs.Items[i] as DataRowView).Row.ItemArray[6]) != "") && (Convert.ToInt16((dgvbat1firstinngs.Items[i] as DataRowView).Row.ItemArray[6]) != 0))
                                        {

                                            objbt1.RunsScored = Convert.ToInt16(objbt1.RunsScored + Convert.ToInt16((dgvbat1firstinngs.Items[i] as DataRowView).Row.ItemArray[4]));
                                            runs1 = Convert.ToInt16((dgvbat1firstinngs.Items[i] as DataRowView).Row.ItemArray[4]);
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
                                            objbt1.Fifties++;
                                        }

                                        if (objbt1.RunsScored >= 100)
                                        {
                                            objbt1.Hundreds++;
                                        }


                                        if (!DBNull.Value.Equals((dgvbat1firstinngs.Items[i] as DataRowView).Row.ItemArray[7]) && ((dgvbat1firstinngs.Items[i] as DataRowView).Row.ItemArray[7]).ToString() != "")
                                        {
                                            objbt1.Fours = Convert.ToInt16(objbt1.Fours + Convert.ToInt16((dgvbat1firstinngs.Items[i] as DataRowView).Row.ItemArray[7]));
                                        }


                                        if (!DBNull.Value.Equals((dgvbat1firstinngs.Items[i] as DataRowView).Row.ItemArray[8]) && ((dgvbat1firstinngs.Items[i] as DataRowView).Row.ItemArray[8]).ToString() != "")
                                        {
                                            objbt1.Sixes = Convert.ToInt16(objbt1.Sixes + Convert.ToInt16((dgvbat1firstinngs.Items[i] as DataRowView).Row.ItemArray[8]));
                                        }

                                    }


                                    ///////////////

                                    if (team1secondinningsbattingnotplayed == true)
                                    {

                                        objbt1.Matches = 1;
                                        objbt1.Innings = objbt1.Innings;
                                    }


                                    else
                                    {
                                        objbt1.Innings++;

                                        if (!DBNull.Value.Equals((dgvbat1secondinngs.Items[i] as DataRowView).Row.ItemArray[6]) && ((dgvbat1secondinngs.Items[i] as DataRowView).Row.ItemArray[6]).ToString() != "")
                                        {
                                            objbt1.BallsFaced = Convert.ToInt16(objbt1.BallsFaced + Convert.ToInt16((dgvbat1secondinngs.Items[i] as DataRowView).Row.ItemArray[6]));
                                        }

                                        if (!DBNull.Value.Equals((dgvbat1secondinngs.Items[i] as DataRowView).Row.ItemArray[3]) && ((dgvbat1secondinngs.Items[i] as DataRowView).Row.ItemArray[3]).ToString() != "")
                                        {
                                            if (Convert.ToString((dgvbat1secondinngs.Items[i] as DataRowView).Row.ItemArray[3]) == "NOT OUT" || Convert.ToString((dgvbat1secondinngs.Items[i] as DataRowView).Row.ItemArray[3]) == "not out" || Convert.ToString((dgvbat1secondinngs.Items[i] as DataRowView).Row.ItemArray[3]) == "n.o" || Convert.ToString((dgvbat1secondinngs.Items[i] as DataRowView).Row.ItemArray[3]) == "N.O" || Convert.ToString((dgvbat1secondinngs.Items[i] as DataRowView).Row.ItemArray[3]) == "no" || Convert.ToString((dgvbat1secondinngs.Items[i] as DataRowView).Row.ItemArray[3]) == "NO")
                                            {
                                                objbt1.NotOut++;
                                            }
                                        }


                                        if (!DBNull.Value.Equals((dgvbat1secondinngs.Items[i] as DataRowView).Row.ItemArray[4]) && ((dgvbat1secondinngs.Items[i] as DataRowView).Row.ItemArray[4]).ToString() != "")
                                        {
                                            if ((Convert.ToString((dgvbat1secondinngs.Items[i] as DataRowView).Row.ItemArray[6]) != "") || (Convert.ToString((dgvbat1secondinngs.Items[i] as DataRowView).Row.ItemArray[6]) != "0"))
                                            {
                                                objbt1.RunsScored = Convert.ToInt16(objbt1.RunsScored + Convert.ToInt16((dgvbat1secondinngs.Items[i] as DataRowView).Row.ItemArray[4]));
                                                objbt1.StrikeRate = (Convert.ToDecimal(objbt1.RunsScored) / Convert.ToDecimal(objbt1.BallsFaced)) * 100;
                                            }

                                            // runs1 = Convert.ToInt16((dgvbat1firstinngs.Items[i] as DataRowView).Row.ItemArray[4]);
                                            int runs2 = Convert.ToInt16((dgvbat1secondinngs.Items[i] as DataRowView).Row.ItemArray[4]);

                                            if (runs1 > runs2)
                                            {
                                                objbt1.HighestScore = Convert.ToInt16(runs1);
                                            }
                                            else
                                            {
                                                objbt1.HighestScore = Convert.ToInt16(runs2);
                                            }


                                            if (objbt1.RunsScored >= 50 && objbt1.RunsScored <= 99)
                                            {
                                                objbt1.Fifties++;
                                            }

                                            if (objbt1.RunsScored >= 100)
                                            {
                                                objbt1.Hundreds++;
                                            }


                                            if (!DBNull.Value.Equals((dgvbat1secondinngs.Items[i] as DataRowView).Row.ItemArray[7]) && ((dgvbat1secondinngs.Items[i] as DataRowView).Row.ItemArray[7]).ToString() != "")
                                            {
                                                objbt1.Fours = Convert.ToInt16(objbt1.Fours + Convert.ToInt16((dgvbat1secondinngs.Items[i] as DataRowView).Row.ItemArray[7]));
                                            }


                                            if (!DBNull.Value.Equals((dgvbat1secondinngs.Items[i] as DataRowView).Row.ItemArray[8]) && ((dgvbat1secondinngs.Items[i] as DataRowView).Row.ItemArray[8]).ToString() != "")
                                            {
                                                objbt1.Sixes = Convert.ToInt16(objbt1.Sixes + Convert.ToInt16((dgvbat1secondinngs.Items[i] as DataRowView).Row.ItemArray[8]));
                                            }

                                        }
                                    }
                                }

                                else
                                {
                                    objbt1.Matches = 1;
                                    objbt1.Innings = 0;

                                }


                                Database.SaveEntity<BestBatsman>(objbt1, oleconn);
                                //isaddedbt1 = true;
                                //  MessageBox.Show("batting 1 added");
                                progessbar.Value = 50;
                                //}
                            }
                        }
                    }
                }
            }


            // add              Batting 2 
            //int batplayed12 = 0;

            for (int i = 0; i <= (firstinng1count2 - 1); i++)
            {
                string playerkscauid = Convert.ToString((dgvbat2firstinngs.Items[i] as DataRowView).Row.ItemArray[2]);
                checkplayerbt2(playerkscauid);

                if (playerexitbt2 == false)
                {

                    if (!DBNull.Value.Equals((dgvbat2firstinngs.Items[i] as DataRowView).Row.ItemArray[1]) && ((dgvbat2firstinngs.Items[i] as DataRowView).Row.ItemArray[1]).ToString() != "")
                    {
                        string playername;
                        string id = Convert.ToString((dgvbat2firstinngs.Items[i] as DataRowView).Row.ItemArray[1]);
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

                                objbt2.SeasonId = selectedgridvalue._seasonid;
                                objbt2.DivisionId = selectedgridvalue.LoadDivisionId;
                                objbt2.Player = obj;
                                objbt2.PlayerName = playername;
                                objbt2.KSCAUID = obj.KSCAUID;

                                objbt2.NotOut = 0;
                                objbt2.Fifties = 0;
                                objbt2.Hundreds = 0;
                                objbt2.RunsScored = 0;
                                objbt2.BallsFaced = 0;
                                objbt2.HighestScore = 0;
                                objbt2.StrikeRate = 0;
                                objbt2.Fours = 0;
                                objbt2.Sixes = 0;
                                int runs1 = 0;

                                foreach (Team objteam in objteams)
                                {
                                    if (obj.TeamIdId == objteam.TeamId)
                                    {
                                        objbt2.TeamName = objteam.TeamName;
                                    }
                                }

                                if (!(chkmatchnotplayed.IsChecked == true))
                                {
                                    if (team2firstinningsbattingnotplayed == true)
                                    {

                                        objbt2.Matches = 1;
                                        objbt2.Innings = 0;
                                    }

                                    else
                                    {

                                        objbt2.Matches = 1;
                                        objbt2.Innings = 1;


                                        if (!DBNull.Value.Equals((dgvbat2firstinngs.Items[i] as DataRowView).Row.ItemArray[3]) && ((dgvbat2firstinngs.Items[i] as DataRowView).Row.ItemArray[3]).ToString() != "")
                                        {
                                            if (Convert.ToString((dgvbat2firstinngs.Items[i] as DataRowView).Row.ItemArray[3]) == "NOT OUT" || Convert.ToString((dgvbat2firstinngs.Items[i] as DataRowView).Row.ItemArray[3]) == "not out" || Convert.ToString((dgvbat2firstinngs.Items[i] as DataRowView).Row.ItemArray[3]) == "n.o" || Convert.ToString((dgvbat2firstinngs.Items[i] as DataRowView).Row.ItemArray[3]) == "N.O" || Convert.ToString((dgvbat2firstinngs.Items[i] as DataRowView).Row.ItemArray[3]) == "no" || Convert.ToString((dgvbat2firstinngs.Items[i] as DataRowView).Row.ItemArray[3]) == "NO")
                                            {
                                                objbt2.NotOut++;
                                            }
                                        }



                                        if (!DBNull.Value.Equals((dgvbat2firstinngs.Items[i] as DataRowView).Row.ItemArray[6]) && ((dgvbat2firstinngs.Items[i] as DataRowView).Row.ItemArray[6]).ToString() != "")
                                        {
                                            objbt2.BallsFaced = Convert.ToInt16(objbt2.BallsFaced + Convert.ToInt16((dgvbat2firstinngs.Items[i] as DataRowView).Row.ItemArray[6]));
                                        }

                                        if (!DBNull.Value.Equals((dgvbat2firstinngs.Items[i] as DataRowView).Row.ItemArray[4]) && ((dgvbat2firstinngs.Items[i] as DataRowView).Row.ItemArray[4]).ToString() != "")
                                        {
                                            if ((Convert.ToString((dgvbat2firstinngs.Items[i] as DataRowView).Row.ItemArray[6]) != "") || (Convert.ToString((dgvbat2firstinngs.Items[i] as DataRowView).Row.ItemArray[6]) != "0"))
                                            {
                                                runs1 = Convert.ToInt16((dgvbat2firstinngs.Items[i] as DataRowView).Row.ItemArray[4]);

                                                objbt2.RunsScored = Convert.ToInt16(objbt2.RunsScored + Convert.ToInt16((dgvbat2firstinngs.Items[i] as DataRowView).Row.ItemArray[4]));
                                                objbt2.HighestScore = objbt2.RunsScored;
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


                                            if (!DBNull.Value.Equals((dgvbat2firstinngs.Items[i] as DataRowView).Row.ItemArray[7]) && ((dgvbat2firstinngs.Items[i] as DataRowView).Row.ItemArray[7]).ToString() != "")
                                            {
                                                objbt2.Fours = Convert.ToInt16(objbt2.Fours + Convert.ToInt16((dgvbat2firstinngs.Items[i] as DataRowView).Row.ItemArray[7]));
                                            }


                                            if (!DBNull.Value.Equals((dgvbat2firstinngs.Items[i] as DataRowView).Row.ItemArray[8]) && ((dgvbat2firstinngs.Items[i] as DataRowView).Row.ItemArray[8]).ToString() != "")
                                            {
                                                objbt2.Sixes = Convert.ToInt16(objbt2.Sixes + Convert.ToInt16((dgvbat2firstinngs.Items[i] as DataRowView).Row.ItemArray[8]));
                                            }

                                        }
                                    }


                                    if (team2secondbattingnotplayed == true)
                                    {
                                        objbt2.Matches = 1;
                                        objbt2.Innings = objbt2.Innings;
                                    }

                                        ////////////////
                                    else
                                    {
                                        objbt2.Innings++;

                                        if (!DBNull.Value.Equals((dgvbat2secondinngs.Items[i] as DataRowView).Row.ItemArray[6]) && ((dgvbat2secondinngs.Items[i] as DataRowView).Row.ItemArray[6]).ToString() != "")
                                        {
                                            objbt2.BallsFaced = Convert.ToInt16(objbt2.BallsFaced + Convert.ToInt16((dgvbat2secondinngs.Items[i] as DataRowView).Row.ItemArray[6]));
                                        }

                                        if (!DBNull.Value.Equals((dgvbat2secondinngs.Items[i] as DataRowView).Row.ItemArray[3]) && ((dgvbat2secondinngs.Items[i] as DataRowView).Row.ItemArray[3]).ToString() != "")
                                        {
                                            if (Convert.ToString((dgvbat2secondinngs.Items[i] as DataRowView).Row.ItemArray[3]) == "NOT OUT" || Convert.ToString((dgvbat2secondinngs.Items[i] as DataRowView).Row.ItemArray[3]) == "not out" || Convert.ToString((dgvbat2secondinngs.Items[i] as DataRowView).Row.ItemArray[3]) == "n.o" || Convert.ToString((dgvbat2secondinngs.Items[i] as DataRowView).Row.ItemArray[3]) == "N.O" || Convert.ToString((dgvbat2secondinngs.Items[i] as DataRowView).Row.ItemArray[3]) == "no" || Convert.ToString((dgvbat2secondinngs.Items[i] as DataRowView).Row.ItemArray[3]) == "NO")
                                            {
                                                objbt2.NotOut++;
                                            }
                                        }

                                        if (!DBNull.Value.Equals((dgvbat2secondinngs.Items[i] as DataRowView).Row.ItemArray[4]) && ((dgvbat2secondinngs.Items[i] as DataRowView).Row.ItemArray[4]).ToString() != "")
                                        {


                                            if ((Convert.ToString((dgvbat2secondinngs.Items[i] as DataRowView).Row.ItemArray[6]) != "") || (Convert.ToString((dgvbat2secondinngs.Items[i] as DataRowView).Row.ItemArray[6]) != "0"))
                                            {
                                                objbt2.RunsScored = Convert.ToInt16(objbt2.RunsScored + Convert.ToInt16((dgvbat2secondinngs.Items[i] as DataRowView).Row.ItemArray[4]));
                                                objbt2.BallsFaced = Convert.ToInt16(objbt2.BallsFaced + Convert.ToInt16((dgvbat2secondinngs.Items[i] as DataRowView).Row.ItemArray[6]));
                                                objbt2.StrikeRate = (Convert.ToDecimal(objbt2.RunsScored) / Convert.ToDecimal(objbt2.BallsFaced)) * 100;
                                            }

                                            int runs2 = Convert.ToInt16((dgvbat2secondinngs.Items[i] as DataRowView).Row.ItemArray[4]);

                                            if (runs1 > runs2)
                                            {
                                                objbt2.HighestScore = Convert.ToInt16(runs1);
                                            }
                                            else
                                            {
                                                objbt2.HighestScore = Convert.ToInt16(runs2);
                                            }



                                            if (objbt2.RunsScored >= 50 && objbt2.RunsScored <= 99)
                                            {
                                                objbt2.Fifties++;
                                            }

                                            if (objbt2.RunsScored >= 100)
                                            {
                                                objbt2.Hundreds++;
                                            }


                                            if (!DBNull.Value.Equals((dgvbat2secondinngs.Items[i] as DataRowView).Row.ItemArray[7]) && ((dgvbat2secondinngs.Items[i] as DataRowView).Row.ItemArray[7]).ToString() != "")
                                            {
                                                objbt2.Fours = Convert.ToInt16(objbt2.Fours + Convert.ToInt16((dgvbat2secondinngs.Items[i] as DataRowView).Row.ItemArray[7]));
                                            }


                                            if (!DBNull.Value.Equals((dgvbat2secondinngs.Items[i] as DataRowView).Row.ItemArray[8]) && ((dgvbat2secondinngs.Items[i] as DataRowView).Row.ItemArray[8]).ToString() != "")
                                            {
                                                objbt2.Sixes = Convert.ToInt16(objbt2.Sixes + Convert.ToInt16((dgvbat2secondinngs.Items[i] as DataRowView).Row.ItemArray[8]));
                                            }

                                        }

                                    }
                                }

                                else
                                {
                                    objbt2.Matches = 1;
                                    objbt2.Innings = 0;

                                }
                                Database.SaveEntity<BestBatsman>(objbt2, oleconn);
                                // isaddedbt2 = true;
                                progessbar.Value = 50;
                            }
                        }
                    }

                }
            }

            // add              Bowling 1 

            for (int i = 0; i <= (firstinng1count1 - 1); i++)
            {
                string playerkscauid = Convert.ToString((dgvbowl1firstinngs.Items[i] as DataRowView).Row.ItemArray[2]);
                checkplayerbl1(playerkscauid);

                if (playerexitbl1 == false)
                {
                    if (!DBNull.Value.Equals((dgvbowl1firstinngs.Items[i] as DataRowView).Row.ItemArray[1]) && ((dgvbowl1firstinngs.Items[i] as DataRowView).Row.ItemArray[1]).ToString() != "")
                    {
                        string playername;
                        string id = Convert.ToString((dgvbowl1firstinngs.Items[i] as DataRowView).Row.ItemArray[1]);
                        if (id.Contains("("))
                        {
                            playername = id.Substring(0, id.IndexOf("(") - 1);
                        }
                        else
                        {
                            playername = id;
                        }

                        foreach (Player obj2 in objplayer1)
                        {
                            if (obj2.KSCAUID == playerkscauid)
                            {

                                BestBowler objbl1 = Database.GetNewEntity<BestBowler>();



                                objbl1.Overs = 0;
                                objbl1.Maidens = 0;
                                objbl1.Runs = 0;
                                objbl1.Econ = 0;
                                objbl1.Wickets = 0;

                                objbl1.SeasonId = selectedgridvalue._seasonid;


                                objbl1.DivisionId = selectedgridvalue.LoadDivisionId;

                                objbl1.PlayerName = obj2.FirstName;
                                objbl1.PlayerName = playername;
                                objbl1.KSCAUID = obj2.KSCAUID;

                                foreach (Team objteam in objteams)
                                {
                                    if (obj2.TeamIdId == objteam.TeamId)
                                    {
                                        objbl1.TeamName = objteam.TeamName;
                                    }
                                }


                                if (!(chkmatchnotplayed.IsChecked == true))
                                {

                                    objbl1.Matches = 1;
                                    objbl1.Innings = 1;


                                    if (!DBNull.Value.Equals((dgvbowl1firstinngs.Items[i] as DataRowView).Row.ItemArray[3]) && ((dgvbowl1firstinngs.Items[i] as DataRowView).Row.ItemArray[3]).ToString() != "")
                                    {
                                        decimal over = Convert.ToDecimal((dgvbowl1firstinngs.Items[i] as DataRowView).Row.ItemArray[3]);
                                        objbl1.Overs = addovers(objbl1.Overs, over);

                                    }

                                    if (!DBNull.Value.Equals((dgvbowl1firstinngs.Items[i] as DataRowView).Row.ItemArray[4]) && ((dgvbowl1firstinngs.Items[i] as DataRowView).Row.ItemArray[4]).ToString() != "")
                                    {
                                        objbl1.Maidens = Convert.ToInt16(objbl1.Maidens + Convert.ToInt16((dgvbowl1firstinngs.Items[i] as DataRowView).Row.ItemArray[4]));
                                    }


                                    if (!DBNull.Value.Equals((dgvbowl1firstinngs.Items[i] as DataRowView).Row.ItemArray[5]) && ((dgvbowl1firstinngs.Items[i] as DataRowView).Row.ItemArray[5]).ToString() != "")
                                    {
                                        objbl1.Runs = Convert.ToInt16(objbl1.Runs + Convert.ToInt16((dgvbowl1firstinngs.Items[i] as DataRowView).Row.ItemArray[5]));
                                        objbl1.Econ = (Convert.ToDecimal(objbl1.Runs) / oversconvert(objbl1.Overs));
                                    }


                                    if (!DBNull.Value.Equals((dgvbowl1firstinngs.Items[i] as DataRowView).Row.ItemArray[6]) && ((dgvbowl1firstinngs.Items[i] as DataRowView).Row.ItemArray[6]).ToString() != "")
                                    {
                                        objbl1.Wickets = Convert.ToInt16(objbl1.Wickets + Convert.ToInt16((dgvbowl1firstinngs.Items[i] as DataRowView).Row.ItemArray[6]));
                                    }


                                    /////////////////////////
                                    if (team2secondinningsbowlingnotplayed == true)
                                    {
                                        objbl1.Matches = 1;
                                        objbl1.Innings = objbl1.Innings;

                                    }

                                    else
                                    {

                                        objbl1.Innings++;
                                        if (!DBNull.Value.Equals((dgvbowl1secondinngs.Items[i] as DataRowView).Row.ItemArray[3]) && ((dgvbowl1secondinngs.Items[i] as DataRowView).Row.ItemArray[3]).ToString() != "")
                                        {
                                            decimal over = Convert.ToDecimal((dgvbowl1secondinngs.Items[i] as DataRowView).Row.ItemArray[3]);
                                            objbl1.Overs = addovers(objbl1.Overs, over);

                                        }

                                        if (!DBNull.Value.Equals((dgvbowl1secondinngs.Items[i] as DataRowView).Row.ItemArray[4]) && ((dgvbowl1secondinngs.Items[i] as DataRowView).Row.ItemArray[4]).ToString() != "")
                                        {
                                            objbl1.Maidens = Convert.ToInt16(objbl1.Maidens + Convert.ToInt16((dgvbowl1secondinngs.Items[i] as DataRowView).Row.ItemArray[4]));
                                        }


                                        if (!DBNull.Value.Equals((dgvbowl1secondinngs.Items[i] as DataRowView).Row.ItemArray[5]) && ((dgvbowl1secondinngs.Items[i] as DataRowView).Row.ItemArray[5]).ToString() != "")
                                        {
                                            objbl1.Runs = Convert.ToInt16(objbl1.Runs + Convert.ToInt16((dgvbowl1secondinngs.Items[i] as DataRowView).Row.ItemArray[5]));
                                            objbl1.Econ = (Convert.ToDecimal(objbl1.Runs) / oversconvert(objbl1.Overs));
                                        }


                                        if (!DBNull.Value.Equals((dgvbowl1secondinngs.Items[i] as DataRowView).Row.ItemArray[6]) && ((dgvbowl1secondinngs.Items[i] as DataRowView).Row.ItemArray[6]).ToString() != "")
                                        {
                                            objbl1.Wickets = Convert.ToInt16(objbl1.Wickets + Convert.ToInt16((dgvbowl1secondinngs.Items[i] as DataRowView).Row.ItemArray[6]));
                                        }
                                    }
                                }

                                else
                                {
                                    objbl1.Matches = 1;
                                    objbl1.Innings = 0;

                                }

                                Database.SaveEntity<BestBowler>(objbl1, oleconn);
                                //o  MessageBox.Show("bowling 1 added");
                                // isaddedbl1 = true;
                                progessbar.Value = 50;

                                // }
                            }

                        }
                    }
                }
            }

            // add              Bowling 2 

            for (int i = 0; i <= (firstinng1count2 - 1); i++)
            {
                string playerkscauid = Convert.ToString((dgvbowl2firstinngs.Items[i] as DataRowView).Row.ItemArray[2]);
                checkplayerbl2(playerkscauid);

                if (playerexitbl2 == false)
                {

                    if (!DBNull.Value.Equals((dgvbowl2firstinngs.Items[i] as DataRowView).Row.ItemArray[1]) && ((dgvbowl2firstinngs.Items[i] as DataRowView).Row.ItemArray[1]).ToString() != "")
                    {
                        string playername;
                        string id = Convert.ToString((dgvbowl2firstinngs.Items[i] as DataRowView).Row.ItemArray[1]);
                        if (id.Contains("("))
                        {
                            playername = id.Substring(0, id.IndexOf("(") - 1);
                        }
                        else
                        {
                            playername = id;
                        }

                        foreach (Player obj2 in objplayer1)
                        {
                            if (obj2.KSCAUID == playerkscauid)
                            {
                                //if (playername == obj2.FirstName)
                                //{
                                BestBowler objbl2 = Database.GetNewEntity<BestBowler>();


                                objbl2.Overs = 0;
                                objbl2.Maidens = 0;
                                objbl2.Runs = 0;
                                objbl2.Econ = 0;
                                objbl2.Wickets = 0;

                                objbl2.SeasonId = selectedgridvalue._seasonid;


                                objbl2.DivisionId = selectedgridvalue.LoadDivisionId;

                                objbl2.PlayerName = obj2.FirstName;
                                objbl2.PlayerName = playername;
                                objbl2.KSCAUID = obj2.KSCAUID;

                                foreach (Team objteam in objteams)
                                {
                                    if (obj2.TeamIdId == objteam.TeamId)
                                    {
                                        objbl2.TeamName = objteam.TeamName;
                                    }
                                }

                                if (!(chkmatchnotplayed.IsChecked == true))
                                {

                                    if (team1firstinningsbowlingnotplayed == true)
                                    {
                                        objbl2.Matches = 1;
                                        objbl2.Innings = objbl2.Innings;

                                    }
                                    else
                                    {
                                        objbl2.Matches = 1;
                                        objbl2.Innings = 1;

                                        if (!DBNull.Value.Equals((dgvbowl2firstinngs.Items[i] as DataRowView).Row.ItemArray[3]) && ((dgvbowl2firstinngs.Items[i] as DataRowView).Row.ItemArray[3]).ToString() != "")
                                        {
                                            decimal over = Convert.ToDecimal((dgvbowl2firstinngs.Items[i] as DataRowView).Row.ItemArray[3]);
                                            objbl2.Overs = addovers(objbl2.Overs, over);

                                        }

                                        if (!DBNull.Value.Equals((dgvbowl2firstinngs.Items[i] as DataRowView).Row.ItemArray[4]) && ((dgvbowl2firstinngs.Items[i] as DataRowView).Row.ItemArray[4]).ToString() != "")
                                        {

                                            objbl2.Maidens = Convert.ToInt16(objbl2.Maidens + Convert.ToInt16((dgvbowl2firstinngs.Items[i] as DataRowView).Row.ItemArray[4]));

                                        }


                                        if (!DBNull.Value.Equals((dgvbowl2firstinngs.Items[i] as DataRowView).Row.ItemArray[5]) && ((dgvbowl2firstinngs.Items[i] as DataRowView).Row.ItemArray[5]).ToString() != "")
                                        {

                                            objbl2.Runs = Convert.ToInt16(objbl2.Runs + Convert.ToInt16((dgvbowl2firstinngs.Items[i] as DataRowView).Row.ItemArray[5]));

                                            objbl2.Econ = (Convert.ToDecimal(objbl2.Runs) / oversconvert(objbl2.Overs));

                                        }


                                        if (!DBNull.Value.Equals((dgvbowl2firstinngs.Items[i] as DataRowView).Row.ItemArray[6]) && ((dgvbowl2firstinngs.Items[i] as DataRowView).Row.ItemArray[6]).ToString() != "")
                                        {
                                            objbl2.Wickets = Convert.ToInt16(objbl2.Wickets + Convert.ToInt16((dgvbowl2firstinngs.Items[i] as DataRowView).Row.ItemArray[6]));
                                        }


                                    }
                                    ////////////////////first///////////
                                    if (team1secondinningsbowlingnotplayed == true)
                                    {
                                        objbl2.Matches = 1;
                                        objbl2.Innings = objbl2.Innings;

                                    }
                                    else
                                    {

                                        objbl2.Innings++;
                                        if (!DBNull.Value.Equals((dgvbowl2secondinngs.Items[i] as DataRowView).Row.ItemArray[3]) && ((dgvbowl2secondinngs.Items[i] as DataRowView).Row.ItemArray[3]).ToString() != "")
                                        {
                                            decimal over = Convert.ToDecimal((dgvbowl2secondinngs.Items[i] as DataRowView).Row.ItemArray[3]);
                                            objbl2.Overs = addovers(objbl2.Overs, over);

                                        }

                                        if (!DBNull.Value.Equals((dgvbowl2secondinngs.Items[i] as DataRowView).Row.ItemArray[4]) && ((dgvbowl2secondinngs.Items[i] as DataRowView).Row.ItemArray[4]).ToString() != "")
                                        {
                                            objbl2.Maidens = Convert.ToInt16(objbl2.Maidens + Convert.ToInt16((dgvbowl2secondinngs.Items[i] as DataRowView).Row.ItemArray[4]));
                                        }


                                        if (!DBNull.Value.Equals((dgvbowl2secondinngs.Items[i] as DataRowView).Row.ItemArray[5]) && ((dgvbowl2secondinngs.Items[i] as DataRowView).Row.ItemArray[5]).ToString() != "")
                                        {
                                            objbl2.Runs = Convert.ToInt16(objbl2.Runs + Convert.ToInt16((dgvbowl2secondinngs.Items[i] as DataRowView).Row.ItemArray[5]));
                                            objbl2.Econ = (Convert.ToDecimal(objbl2.Runs) / oversconvert(objbl2.Overs));
                                        }


                                        if (!DBNull.Value.Equals((dgvbowl2secondinngs.Items[i] as DataRowView).Row.ItemArray[6]) && ((dgvbowl2secondinngs.Items[i] as DataRowView).Row.ItemArray[6]).ToString() != "")
                                        {
                                            objbl2.Wickets = Convert.ToInt16(objbl2.Wickets + Convert.ToInt16((dgvbowl2secondinngs.Items[i] as DataRowView).Row.ItemArray[6]));
                                        }
                                    }
                                }
                                else
                                {
                                    objbl2.Matches = 1;
                                    objbl2.Innings = 0;

                                }


                                Database.SaveEntity<BestBowler>(objbl2, oleconn);
                                //  MessageBox.Show("bowling 2 added");
                                //isaddedbl2 = true;
                                progessbar.Value = 50;
                                //}
                            }
                        }
                    }
                }
            }


            // update              Batting 1 
            int batplayed1 = 0;
            if (isaddedbt1 == false)
            {
                for (int i = 0; i <= (firstinng1count1 - 1); i++)
                {
                    string playerkscauid = Convert.ToString((dgvbat1firstinngs.Items[i] as DataRowView).Row.ItemArray[2]);
                    checkplayerbt1(playerkscauid);

                    if (playerexitbt1 == true)
                    {

                        if (!DBNull.Value.Equals((dgvbat1firstinngs.Items[i] as DataRowView).Row.ItemArray[1]) && ((dgvbat1firstinngs.Items[i] as DataRowView).Row.ItemArray[1]).ToString() != "")
                        {
                            string playername;
                            string id = Convert.ToString((dgvbat1firstinngs.Items[i] as DataRowView).Row.ItemArray[1]);
                            if (id.Contains("("))
                            {
                                playername = id.Substring(0, id.IndexOf("(") - 1);
                            }
                            else
                            {
                                playername = id;
                            }


                            foreach (BestBatsman objbt1 in objbat1)
                            {
                                if (objbt1.KSCAUID == playerkscauid)
                                {
                                    if (!(chkmatchnotplayed.IsChecked == true))
                                    {

                                        objbt1.Matches++;
                                        objbt1.Innings++;

                                        if (!DBNull.Value.Equals((dgvbat1firstinngs.Items[i] as DataRowView).Row.ItemArray[3]) && ((dgvbat1firstinngs.Items[i] as DataRowView).Row.ItemArray[3]).ToString() != "")
                                        {
                                            if (Convert.ToString((dgvbat1firstinngs.Items[i] as DataRowView).Row.ItemArray[3]) == "NOT OUT")
                                            {
                                                objbt1.NotOut++;
                                            }
                                        }


                                        if (!DBNull.Value.Equals((dgvbat1firstinngs.Items[i] as DataRowView).Row.ItemArray[4]) && ((dgvbat1firstinngs.Items[i] as DataRowView).Row.ItemArray[4]).ToString() != "")
                                        {
                                            if (Convert.ToString((dgvbat1firstinngs.Items[i] as DataRowView).Row.ItemArray[6]) != Convert.ToString(batplayed1))
                                            {
                                                objbt1.RunsScored = Convert.ToInt16(objbt1.RunsScored + Convert.ToInt16((dgvbat1firstinngs.Items[i] as DataRowView).Row.ItemArray[4]));
                                                objbt1.BallsFaced = Convert.ToInt16(objbt1.BallsFaced + Convert.ToInt16((dgvbat1firstinngs.Items[i] as DataRowView).Row.ItemArray[6]));

                                                if (objbt1.HighestScore < (Convert.ToInt16((dgvbat1firstinngs.Items[i] as DataRowView).Row.ItemArray[4])))
                                                {
                                                    objbt1.HighestScore = Convert.ToInt16((dgvbat1firstinngs.Items[i] as DataRowView).Row.ItemArray[4]);
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


                                            if (!DBNull.Value.Equals((dgvbat1firstinngs.Items[i] as DataRowView).Row.ItemArray[7]) && ((dgvbat1firstinngs.Items[i] as DataRowView).Row.ItemArray[7]).ToString() != "")
                                            {
                                                objbt1.Fours = Convert.ToInt16(Convert.ToInt16(objbt1.Fours) + Convert.ToInt16((dgvbat1firstinngs.Items[i] as DataRowView).Row.ItemArray[7]));
                                            }


                                            if (!DBNull.Value.Equals((dgvbat1firstinngs.Items[i] as DataRowView).Row.ItemArray[8]) && ((dgvbat1firstinngs.Items[i] as DataRowView).Row.ItemArray[8]).ToString() != "")
                                            {
                                                objbt1.Sixes = Convert.ToInt16(Convert.ToInt16(objbt1.Sixes) + Convert.ToInt16((dgvbat1firstinngs.Items[i] as DataRowView).Row.ItemArray[8]));
                                            }
                                        }

                                        ///////

                                        if (team1secondinningsbattingnotplayed == true)
                                        {
                                            // objbt1.Matches ++;
                                            objbt1.Innings = objbt1.Innings;


                                        }
                                        else
                                        {
                                            objbt1.Innings++;

                                            if (!DBNull.Value.Equals((dgvbat1secondinngs.Items[i] as DataRowView).Row.ItemArray[3]) && ((dgvbat1secondinngs.Items[i] as DataRowView).Row.ItemArray[3]).ToString() != "")
                                            {
                                                if (Convert.ToString((dgvbat1secondinngs.Items[i] as DataRowView).Row.ItemArray[3]) == "NOT OUT")
                                                {
                                                    objbt1.NotOut++;
                                                }
                                            }


                                            if (!DBNull.Value.Equals((dgvbat1secondinngs.Items[i] as DataRowView).Row.ItemArray[4]) && ((dgvbat1secondinngs.Items[i] as DataRowView).Row.ItemArray[4]).ToString() != "")
                                            {
                                                if (Convert.ToString((dgvbat1secondinngs.Items[i] as DataRowView).Row.ItemArray[6]) != Convert.ToString(batplayed1))
                                                {
                                                    objbt1.RunsScored = Convert.ToInt16(objbt1.RunsScored + Convert.ToInt16((dgvbat1secondinngs.Items[i] as DataRowView).Row.ItemArray[4]));
                                                    objbt1.BallsFaced = Convert.ToInt16(objbt1.BallsFaced + Convert.ToInt16((dgvbat1secondinngs.Items[i] as DataRowView).Row.ItemArray[6]));

                                                    if (objbt1.HighestScore < (Convert.ToInt16((dgvbat1secondinngs.Items[i] as DataRowView).Row.ItemArray[4])))
                                                    {
                                                        objbt1.HighestScore = Convert.ToInt16((dgvbat1secondinngs.Items[i] as DataRowView).Row.ItemArray[4]);
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


                                                if (!DBNull.Value.Equals((dgvbat1secondinngs.Items[i] as DataRowView).Row.ItemArray[7]) && ((dgvbat1secondinngs.Items[i] as DataRowView).Row.ItemArray[7]).ToString() != "")
                                                {
                                                    objbt1.Fours = Convert.ToInt16(Convert.ToInt16(objbt1.Fours) + Convert.ToInt16((dgvbat1secondinngs.Items[i] as DataRowView).Row.ItemArray[7]));
                                                }


                                                if (!DBNull.Value.Equals((dgvbat1secondinngs.Items[i] as DataRowView).Row.ItemArray[8]) && ((dgvbat1secondinngs.Items[i] as DataRowView).Row.ItemArray[8]).ToString() != "")
                                                {
                                                    objbt1.Sixes = Convert.ToInt16(Convert.ToInt16(objbt1.Sixes) + Convert.ToInt16((dgvbat1secondinngs.Items[i] as DataRowView).Row.ItemArray[8]));
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        objbt1.Matches++;
                                        objbt1.Innings = objbt1.Innings;
                                    }
                                    Database.SaveEntity<BestBatsman>(objbt1, oleconn);
                                    // MessageBox.Show("batting 1 updated");
                                    progessbar.Value = 50;

                                }
                            }
                        }
                    }
                }
            }

            // update              Batting 2 
            int batplayed2 = 0;
            if (isaddedbt2 == false)
            {
                for (int i = 0; i <= (firstinng1count2 - 1); i++)
                {


                    string playerkscauid = Convert.ToString((dgvbat2firstinngs.Items[i] as DataRowView).Row.ItemArray[2]);
                    checkplayerbt2(playerkscauid);

                    if (playerexitbt2 == true)
                    {

                        if (!DBNull.Value.Equals((dgvbat2firstinngs.Items[i] as DataRowView).Row.ItemArray[1]) && ((dgvbat2firstinngs.Items[i] as DataRowView).Row.ItemArray[1]).ToString() != "")
                        {
                            string playername;
                            string id = Convert.ToString((dgvbat2firstinngs.Items[i] as DataRowView).Row.ItemArray[1]);
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
                            //    if (obj2.KSCAUID == playerkscauid)
                            //    {
                            //        if (playername == obj2.FirstName)
                            //        {

                            foreach (BestBatsman objbt2 in objbat1)
                            {
                                if (objbt2.PlayerName == playername)
                                {
                                    if (!(chkmatchnotplayed.IsChecked == true))
                                    {
                                        objbt2.Matches++;

                                        if (team2firstinningsbattingnotplayed == true)
                                        {
                                            objbt2.Innings = objbt2.Innings;
                                        }

                                        else
                                        {
                                            //objbt2.Matches++;
                                            objbt2.Innings++;

                                            if (!DBNull.Value.Equals((dgvbat2firstinngs.Items[i] as DataRowView).Row.ItemArray[3]) && ((dgvbat2firstinngs.Items[i] as DataRowView).Row.ItemArray[3]).ToString() != "")
                                            {
                                                if (Convert.ToString((dgvbat2firstinngs.Items[i] as DataRowView).Row.ItemArray[3]) == "NOT OUT")
                                                {
                                                    objbt2.NotOut++;
                                                }
                                            }


                                            if (!DBNull.Value.Equals((dgvbat2firstinngs.Items[i] as DataRowView).Row.ItemArray[4]) && ((dgvbat2firstinngs.Items[i] as DataRowView).Row.ItemArray[4]).ToString() != "")
                                            {
                                                if (Convert.ToString((dgvbat2firstinngs.Items[i] as DataRowView).Row.ItemArray[6]) != Convert.ToString(batplayed2))
                                                {
                                                    objbt2.RunsScored = Convert.ToInt16(objbt2.RunsScored + Convert.ToInt16((dgvbat2firstinngs.Items[i] as DataRowView).Row.ItemArray[4]));
                                                    objbt2.BallsFaced = Convert.ToInt16(objbt2.BallsFaced + Convert.ToInt16((dgvbat2firstinngs.Items[i] as DataRowView).Row.ItemArray[6]));


                                                    if (objbt2.HighestScore < (Convert.ToInt16((dgvbat2firstinngs.Items[i] as DataRowView).Row.ItemArray[4])))
                                                    {
                                                        objbt2.HighestScore = Convert.ToInt16((dgvbat2firstinngs.Items[i] as DataRowView).Row.ItemArray[4]);
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


                                                if (!DBNull.Value.Equals((dgvbat2firstinngs.Items[i] as DataRowView).Row.ItemArray[7]) && ((dgvbat2firstinngs.Items[i] as DataRowView).Row.ItemArray[7]).ToString() != "")
                                                {
                                                    objbt2.Fours = Convert.ToInt16(Convert.ToInt16(objbt2.Fours) + Convert.ToInt16((dgvbat2firstinngs.Items[i] as DataRowView).Row.ItemArray[7]));
                                                }


                                                if (!DBNull.Value.Equals((dgvbat2firstinngs.Items[i] as DataRowView).Row.ItemArray[8]) && ((dgvbat2firstinngs.Items[i] as DataRowView).Row.ItemArray[8]).ToString() != "")
                                                {
                                                    objbt2.Sixes = Convert.ToInt16(Convert.ToInt16(objbt2.Sixes) + Convert.ToInt16((dgvbat2firstinngs.Items[i] as DataRowView).Row.ItemArray[8]));
                                                }
                                            }
                                        }
                                        ///////

                                        if (team2secondbattingnotplayed == true)
                                        {
                                            //objbt2.Matches++;
                                            objbt2.Innings = objbt2.Innings;


                                        }
                                        else
                                        {
                                            objbt2.Innings++;

                                            if (!DBNull.Value.Equals((dgvbat2secondinngs.Items[i] as DataRowView).Row.ItemArray[3]) && ((dgvbat2secondinngs.Items[i] as DataRowView).Row.ItemArray[3]).ToString() != "")
                                            {
                                                if (Convert.ToString((dgvbat2secondinngs.Items[i] as DataRowView).Row.ItemArray[3]) == "NOT OUT")
                                                {
                                                    objbt2.NotOut++;
                                                }
                                            }


                                            if (!DBNull.Value.Equals((dgvbat2secondinngs.Items[i] as DataRowView).Row.ItemArray[4]) && ((dgvbat2secondinngs.Items[i] as DataRowView).Row.ItemArray[4]).ToString() != "")
                                            {
                                                if (Convert.ToString((dgvbat2secondinngs.Items[i] as DataRowView).Row.ItemArray[6]) != Convert.ToString(batplayed2))
                                                {
                                                    objbt2.RunsScored = Convert.ToInt16(objbt2.RunsScored + Convert.ToInt16((dgvbat2secondinngs.Items[i] as DataRowView).Row.ItemArray[4]));
                                                    objbt2.BallsFaced = Convert.ToInt16(objbt2.BallsFaced + Convert.ToInt16((dgvbat2secondinngs.Items[i] as DataRowView).Row.ItemArray[6]));


                                                    if (objbt2.HighestScore < (Convert.ToInt16((dgvbat2secondinngs.Items[i] as DataRowView).Row.ItemArray[4])))
                                                    {
                                                        objbt2.HighestScore = Convert.ToInt16((dgvbat2secondinngs.Items[i] as DataRowView).Row.ItemArray[4]);
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


                                                if (!DBNull.Value.Equals((dgvbat2secondinngs.Items[i] as DataRowView).Row.ItemArray[7]) && ((dgvbat2secondinngs.Items[i] as DataRowView).Row.ItemArray[7]).ToString() != "")
                                                {
                                                    objbt2.Fours = Convert.ToInt16(Convert.ToInt16(objbt2.Fours) + Convert.ToInt16((dgvbat2secondinngs.Items[i] as DataRowView).Row.ItemArray[7]));
                                                }


                                                if (!DBNull.Value.Equals((dgvbat2secondinngs.Items[i] as DataRowView).Row.ItemArray[8]) && ((dgvbat2secondinngs.Items[i] as DataRowView).Row.ItemArray[8]).ToString() != "")
                                                {
                                                    objbt2.Sixes = Convert.ToInt16(Convert.ToInt16(objbt2.Sixes) + Convert.ToInt16((dgvbat2secondinngs.Items[i] as DataRowView).Row.ItemArray[8]));
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        objbt2.Matches++;
                                        objbt2.Innings = objbt2.Innings;
                                    }
                                    Database.SaveEntity<BestBatsman>(objbt2, oleconn);
                                    //  MessageBox.Show("batting 2 updated");
                                    progessbar.Value = 50;
                                }

                            }
                        }
                    }
                }
            }

            // update              Bowling 1
            if (isaddedbl1 == false)
            {
                for (int i = 0; i <= (firstinng1count1 - 1); i++)
                {

                    string playerkscauid = Convert.ToString((dgvbowl1firstinngs.Items[i] as DataRowView).Row.ItemArray[2]);
                    checkplayerbl1(playerkscauid);

                    if (playerexitbl1 == true)
                    {

                        if (!DBNull.Value.Equals((dgvbowl1firstinngs.Items[i] as DataRowView).Row.ItemArray[1]) && ((dgvbowl1firstinngs.Items[i] as DataRowView).Row.ItemArray[1]).ToString() != "")
                        {
                            string playername;
                            string id = Convert.ToString((dgvbowl1firstinngs.Items[i] as DataRowView).Row.ItemArray[1]);
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
                            //    if (obj2.KSCAUID == playerkscauid)
                            //    {
                            //        if (playername == obj2.FirstName)
                            //        {

                            foreach (BestBowler objbl1 in objbowl1)
                            {
                                if (objbl1.KSCAUID == playerkscauid)
                                {
                                    if (!(chkmatchnotplayed.IsChecked == true))
                                    {


                                        objbl1.Matches++;


                                        objbl1.Innings++;

                                        if (!DBNull.Value.Equals((dgvbowl1firstinngs.Items[i] as DataRowView).Row.ItemArray[3]) && ((dgvbowl1firstinngs.Items[i] as DataRowView).Row.ItemArray[3]).ToString() != "")
                                        {
                                            objbl1.Overs = addovers(objbl1.Overs, Convert.ToDecimal((dgvbowl1firstinngs.Items[i] as DataRowView).Row.ItemArray[3]));

                                            if (!DBNull.Value.Equals((dgvbowl1firstinngs.Items[i] as DataRowView).Row.ItemArray[4]) && ((dgvbowl1firstinngs.Items[i] as DataRowView).Row.ItemArray[4]).ToString() != "")
                                            {
                                                objbl1.Maidens = Convert.ToInt16(objbl1.Maidens + Convert.ToInt16((dgvbowl1firstinngs.Items[i] as DataRowView).Row.ItemArray[4]));
                                            }

                                        }

                                        if (!DBNull.Value.Equals((dgvbowl1firstinngs.Items[i] as DataRowView).Row.ItemArray[5]) && ((dgvbowl1firstinngs.Items[i] as DataRowView).Row.ItemArray[5]).ToString() != "")
                                        {

                                            objbl1.Runs = Convert.ToInt16(Convert.ToInt16(objbl1.Runs) + Convert.ToInt16((dgvbowl1firstinngs.Items[i] as DataRowView).Row.ItemArray[5]));
                                        }

                                        if (!DBNull.Value.Equals((dgvbowl1firstinngs.Items[i] as DataRowView).Row.ItemArray[6]) && ((dgvbowl1firstinngs.Items[i] as DataRowView).Row.ItemArray[6]).ToString() != "")
                                        {
                                            objbl1.Wickets = Convert.ToInt16(Convert.ToInt16(objbl1.Wickets) + Convert.ToInt16((dgvbowl1firstinngs.Items[i] as DataRowView).Row.ItemArray[6]));
                                        }

                                        if (!DBNull.Value.Equals((dgvbowl1firstinngs.Items[i] as DataRowView).Row.ItemArray[3]) && ((dgvbowl1firstinngs.Items[i] as DataRowView).Row.ItemArray[3]).ToString() != "")
                                        {
                                            objbl1.Econ = (Convert.ToDecimal(objbl1.Runs) / oversconvert(objbl1.Overs));
                                        }


                                        ////
                                        if (team2secondinningsbowlingnotplayed == true)
                                        {
                                            objbl1.Innings = objbl1.Innings;
                                        }

                                        else
                                        {
                                            objbl1.Innings++;

                                            if (!DBNull.Value.Equals((dgvbowl1secondinngs.Items[i] as DataRowView).Row.ItemArray[3]) && ((dgvbowl1secondinngs.Items[i] as DataRowView).Row.ItemArray[3]).ToString() != "")
                                            {
                                                objbl1.Overs = addovers(objbl1.Overs, Convert.ToDecimal((dgvbowl1secondinngs.Items[i] as DataRowView).Row.ItemArray[3]));

                                                if (!DBNull.Value.Equals((dgvbowl1secondinngs.Items[i] as DataRowView).Row.ItemArray[4]) && ((dgvbowl1secondinngs.Items[i] as DataRowView).Row.ItemArray[4]).ToString() != "")
                                                {
                                                    objbl1.Maidens = Convert.ToInt16(objbl1.Maidens + Convert.ToInt16((dgvbowl1secondinngs.Items[i] as DataRowView).Row.ItemArray[4]));
                                                }

                                            }

                                            if (!DBNull.Value.Equals((dgvbowl1secondinngs.Items[i] as DataRowView).Row.ItemArray[5]) && ((dgvbowl1secondinngs.Items[i] as DataRowView).Row.ItemArray[5]).ToString() != "")
                                            {

                                                objbl1.Runs = Convert.ToInt16(Convert.ToInt16(objbl1.Runs) + Convert.ToInt16((dgvbowl1secondinngs.Items[i] as DataRowView).Row.ItemArray[5]));
                                            }

                                            if (!DBNull.Value.Equals((dgvbowl1secondinngs.Items[i] as DataRowView).Row.ItemArray[6]) && ((dgvbowl1secondinngs.Items[i] as DataRowView).Row.ItemArray[6]).ToString() != "")
                                            {
                                                objbl1.Wickets = Convert.ToInt16(Convert.ToInt16(objbl1.Wickets) + Convert.ToInt16((dgvbowl1secondinngs.Items[i] as DataRowView).Row.ItemArray[6]));
                                            }

                                            if (!DBNull.Value.Equals((dgvbowl1secondinngs.Items[i] as DataRowView).Row.ItemArray[3]) && ((dgvbowl1secondinngs.Items[i] as DataRowView).Row.ItemArray[3]).ToString() != "")
                                            {
                                                objbl1.Econ = (Convert.ToDecimal(objbl1.Runs) / oversconvert(objbl1.Overs));
                                            }
                                        }
                                    }
                                    else
                                    {
                                        objbl1.Matches++;
                                        objbl1.Innings = objbl1.Innings;
                                    }
                                    Database.SaveEntity<BestBowler>(objbl1, oleconn);
                                    //  MessageBox.Show("bowling 1 updated");
                                    progessbar.Value = 50;
                                }

                            }
                            //        }
                            //    }
                            //}
                        }
                    }

                }

            }
            // update              Bowling 2
            if (isaddedbl2 == false)
            {
                for (int i = 0; i <= (firstinng1count2 - 1); i++)
                {

                    string playerkscauid = Convert.ToString((dgvbowl2firstinngs.Items[i] as DataRowView).Row.ItemArray[2]);
                    checkplayerbl2(playerkscauid);

                    if (playerexitbl2 == true)
                    {

                        if (!DBNull.Value.Equals((dgvbowl2firstinngs.Items[i] as DataRowView).Row.ItemArray[1]) && ((dgvbowl2firstinngs.Items[i] as DataRowView).Row.ItemArray[1]).ToString() != "")
                        {
                            string playername;
                            string id = Convert.ToString((dgvbowl2firstinngs.Items[i] as DataRowView).Row.ItemArray[1]);
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
                            //    if (obj2.KSCAUID == playerkscauid)
                            //    {
                            //        if (playername == obj2.FirstName)
                            //        {

                            foreach (BestBowler objbl2 in objbowl1)
                            {
                                if (objbl2.KSCAUID == playerkscauid)
                                {
                                    if (!(chkmatchnotplayed.IsChecked == true))
                                    {

                                        objbl2.Matches++;

                                        if (team1firstinningsbowlingnotplayed == true)
                                        {

                                            objbl2.Innings = objbl2.Innings;
                                        }
                                        else
                                        {
                                            //  objbl2.Matches++;
                                            objbl2.Innings++;

                                            if (!DBNull.Value.Equals((dgvbowl2firstinngs.Items[i] as DataRowView).Row.ItemArray[3]) && ((dgvbowl2firstinngs.Items[i] as DataRowView).Row.ItemArray[3]).ToString() != "")
                                            {
                                                objbl2.Overs = addovers(objbl2.Overs, Convert.ToDecimal((dgvbowl2firstinngs.Items[i] as DataRowView).Row.ItemArray[3]));

                                                if (!DBNull.Value.Equals((dgvbowl2firstinngs.Items[i] as DataRowView).Row.ItemArray[4]) && ((dgvbowl2firstinngs.Items[i] as DataRowView).Row.ItemArray[4]).ToString() != "")
                                                {
                                                    objbl2.Maidens = Convert.ToInt16(objbl2.Maidens + Convert.ToInt16((dgvbowl2firstinngs.Items[i] as DataRowView).Row.ItemArray[4]));
                                                }

                                            }

                                            if (!DBNull.Value.Equals((dgvbowl2firstinngs.Items[i] as DataRowView).Row.ItemArray[5]) && ((dgvbowl2firstinngs.Items[i] as DataRowView).Row.ItemArray[5]).ToString() != "")
                                            {

                                                objbl2.Runs = Convert.ToInt16(Convert.ToInt16(objbl2.Runs) + Convert.ToInt16((dgvbowl2firstinngs.Items[i] as DataRowView).Row.ItemArray[5]));
                                            }

                                            if (!DBNull.Value.Equals((dgvbowl2firstinngs.Items[i] as DataRowView).Row.ItemArray[6]) && ((dgvbowl2firstinngs.Items[i] as DataRowView).Row.ItemArray[6]).ToString() != "")
                                            {
                                                objbl2.Wickets = Convert.ToInt16(Convert.ToInt16(objbl2.Wickets) + Convert.ToInt16((dgvbowl2firstinngs.Items[i] as DataRowView).Row.ItemArray[6]));
                                            }

                                            if (!DBNull.Value.Equals((dgvbowl2firstinngs.Items[i] as DataRowView).Row.ItemArray[3]) && ((dgvbowl2firstinngs.Items[i] as DataRowView).Row.ItemArray[3]).ToString() != "")
                                            {
                                                objbl2.Econ = (Convert.ToDecimal(objbl2.Runs) / oversconvert(objbl2.Overs));
                                            }
                                        }
                                        ////

                                        if (team1secondinningsbowlingnotplayed == true)
                                        {
                                            //   objbl2.Matches++;
                                            objbl2.Innings = objbl2.Innings;
                                        }

                                        else
                                        {
                                            //   objbl2.Matches++;
                                            objbl2.Innings++;

                                            if (!DBNull.Value.Equals((dgvbowl2secondinngs.Items[i] as DataRowView).Row.ItemArray[3]) && ((dgvbowl2secondinngs.Items[i] as DataRowView).Row.ItemArray[3]).ToString() != "")
                                            {
                                                objbl2.Overs = addovers(objbl2.Overs, Convert.ToDecimal((dgvbowl2secondinngs.Items[i] as DataRowView).Row.ItemArray[3]));

                                                if (!DBNull.Value.Equals((dgvbowl2secondinngs.Items[i] as DataRowView).Row.ItemArray[4]) && ((dgvbowl2secondinngs.Items[i] as DataRowView).Row.ItemArray[4]).ToString() != "")
                                                {
                                                    objbl2.Maidens = Convert.ToInt16(objbl2.Maidens + Convert.ToInt16((dgvbowl2secondinngs.Items[i] as DataRowView).Row.ItemArray[4]));
                                                }

                                            }

                                            if (!DBNull.Value.Equals((dgvbowl2secondinngs.Items[i] as DataRowView).Row.ItemArray[5]) && ((dgvbowl2secondinngs.Items[i] as DataRowView).Row.ItemArray[5]).ToString() != "")
                                            {

                                                objbl2.Runs = Convert.ToInt16(Convert.ToInt16(objbl2.Runs) + Convert.ToInt16((dgvbowl2secondinngs.Items[i] as DataRowView).Row.ItemArray[5]));
                                            }

                                            if (!DBNull.Value.Equals((dgvbowl2secondinngs.Items[i] as DataRowView).Row.ItemArray[6]) && ((dgvbowl2secondinngs.Items[i] as DataRowView).Row.ItemArray[6]).ToString() != "")
                                            {
                                                objbl2.Wickets = Convert.ToInt16(Convert.ToInt16(objbl2.Wickets) + Convert.ToInt16((dgvbowl2secondinngs.Items[i] as DataRowView).Row.ItemArray[6]));
                                            }

                                            if (!DBNull.Value.Equals((dgvbowl2secondinngs.Items[i] as DataRowView).Row.ItemArray[3]) && ((dgvbowl2secondinngs.Items[i] as DataRowView).Row.ItemArray[3]).ToString() != "")
                                            {
                                                objbl2.Econ = (Convert.ToDecimal(objbl2.Runs) / oversconvert(objbl2.Overs));
                                            }
                                        }
                                    }
                                    else
                                    {
                                        objbl2.Matches++;
                                        objbl2.Innings = objbl2.Innings;
                                    }
                                    Database.SaveEntity<BestBowler>(objbl2, oleconn);
                                    //    MessageBox.Show("bowling 2 updated");
                                    progessbar.Value = 50;
                                }

                            }
                            //        }
                            //    }
                            //}
                        }
                    }

                }
            }
            progessbar.Value = 75;


            FirstDivScoreCardDetails firstdivscorecardobj = Database.GetNewEntity<FirstDivScoreCardDetails>();

            firstdivscorecardobj.MatchId = selectedgridvalue.matchid;
            firstdivscorecardobj.Group = selectedgridvalue.group;

            firstdivscorecardobj.TossWon = tosswon.team;
            firstdivscorecardobj.ElectedTo = electedto.batball;
            firstdivscorecardobj.Scorer = NameOf.Scorer;
            firstdivscorecardobj.UmpireOne = NameOf.Umpire1;
            firstdivscorecardobj.UmpireTwo = NameOf.Umpire2;

            //timeAfirstinngs();
            //timeAsecondinngs();
            //timeBfirstinngs();
            //timeBsecondinngs();


            firstdivscorecardobj.CommenceTimeAFirstInngs = start_time1firstinngs.Text;
            firstdivscorecardobj.CommenceTimeASecondInngs = start_time1secondinngs.Text;

            firstdivscorecardobj.EndTimeAFirstInngs = close_time1firstinngs.Text;
            firstdivscorecardobj.EndTimeASecondInngs = close_time1secondinngs.Text;

            firstdivscorecardobj.DurationAFirstInngs = Convert.ToInt16(txtDuration1firstinngs.Text);
            firstdivscorecardobj.DurationASecondInngs = Convert.ToInt16(txtDuration1secondinngs.Text);

            firstdivscorecardobj.ByesAFirstInngs = Convert.ToInt16(txtbyes1firstinngs.Text);
            firstdivscorecardobj.ByesASecondInngs = Convert.ToInt16(txtbyes1secondinngs.Text);

            firstdivscorecardobj.LegByesAFistInngs = Convert.ToInt16(txtlegbyes1firstinngs.Text);
            firstdivscorecardobj.LegByesASecondInngs = Convert.ToInt16(txtlegbyes1secondinngs.Text);

            firstdivscorecardobj.PenaltyAFirstInngs = Convert.ToInt16(txtpenalty1firstinngs.Text);
            firstdivscorecardobj.PenaltyASecondInngs = Convert.ToInt16(txtpenalty1secondinngs.Text);

            firstdivscorecardobj.TotalExtrasAFirstInngs = Convert.ToInt16(txttotalextras1firstinngs.Text);
            firstdivscorecardobj.TotalExtrasASecondInngs = Convert.ToInt16(txttotalextras1secondinngs.Text);

            firstdivscorecardobj.TotalRunsAFirstInngs = Convert.ToInt16(txtruns1firstinngs.Text);
            firstdivscorecardobj.TotalRunsASecondInngs = Convert.ToInt16(txtruns1secondinngs.Text);

            firstdivscorecardobj.TotalWicketsAFirstInngs = Convert.ToInt16(txtwickets1firstinngs.Text);
            firstdivscorecardobj.TotalWicketsASecondInngs = Convert.ToInt16(txtwickets1secondinngs.Text);

            firstdivscorecardobj.TotalOversAFirstInngs = Convert.ToDecimal(txtovers1firstinngs.Text);
            firstdivscorecardobj.TotalOversASecondInngs = Convert.ToDecimal(txtovers1secondinngs.Text);

            firstdivscorecardobj.CommenceTimeBFirstInngs = start_time2firstinngs.Text;
            firstdivscorecardobj.CommenceTimeBSecondInngs = start_time2secondinngs.Text;

            firstdivscorecardobj.EndTimeBFirstInngs = close_time2firstinngs.Text;
            firstdivscorecardobj.EndTimeBSecondInngs = close_time2secondinngs.Text;

            firstdivscorecardobj.DurationBFirstInngs = Convert.ToInt16(txtDuration2firstinngs.Text);
            firstdivscorecardobj.DurationBSecondInngs = Convert.ToInt16(txtDuration2secondinngs.Text);

            firstdivscorecardobj.ByesBFirstInngs = Convert.ToInt16(txtbyes2firstinngs.Text);
            firstdivscorecardobj.ByesBSecondInngs = Convert.ToInt16(txtbyes2secondinngs.Text);

            firstdivscorecardobj.LegByesBFistInngs = Convert.ToInt16(txtlegbyes2firstinngs.Text);
            firstdivscorecardobj.LegByesBSecondInngs = Convert.ToInt16(txtlegbyes2secondinngs.Text);

            firstdivscorecardobj.PenaltyBFirstInngs = Convert.ToInt16(txtpenalty2firstinngs.Text);
            firstdivscorecardobj.PenaltyBSecondInngs = Convert.ToInt16(txtpenalty2secondinngs.Text);

            firstdivscorecardobj.TotalExtrasBFirstInngs = Convert.ToInt16(txttotalextras2firstinngs.Text);
            firstdivscorecardobj.TotalExtrasBSecondInngs = Convert.ToInt16(txttotalextras2secondinngs.Text);

            firstdivscorecardobj.TotalRunsBFirstInngs = Convert.ToInt16(txtruns2firstinngs.Text);
            firstdivscorecardobj.TotalRunsBSecondInngs = Convert.ToInt16(txtruns2secondinngs.Text);

            firstdivscorecardobj.TotalWicketsBFirstInngs = Convert.ToInt16(txtwickets2firstinngs.Text);
            firstdivscorecardobj.TotalWicketsBSecondInngs = Convert.ToInt16(txtwickets2secondinngs.Text);

            firstdivscorecardobj.TotalOversBFirstInngs = Convert.ToDecimal(txtovers2firstinngs.Text);
            firstdivscorecardobj.TotalOversBSecondInngs = Convert.ToDecimal(txtovers2secondinngs.Text);

            firstdivscorecardobj.RunOutAFirst = Convert.ToInt16(txtrunout1firstinnings.Text);
            firstdivscorecardobj.RunOutASecond = Convert.ToInt16(txtrunout1seccondinnings.Text);
            firstdivscorecardobj.RunOutBFirst = Convert.ToInt16(txtrunout2firstinngs.Text);
            firstdivscorecardobj.RunOutBSecond = Convert.ToInt16(txtrunout2secondinngs.Text);

            firstdivscorecardobj.TeamAResult = TeamA.Result;
            firstdivscorecardobj.TeamBResult = TeamB.Result;
            firstdivscorecardobj.TeamAPoints = Convert.ToInt16(TeamA.Points);
            firstdivscorecardobj.TeamBPoints = Convert.ToInt16(TeamB.Points);
            firstdivscorecardobj.Result = Match.Result;
            firstdivscorecardobj.Remarks = Remark.Remarks;


            firstdivscorecardobj.SeasonId = selectedgridvalue._seasonid;
            firstdivscorecardobj.ZoneId = TempValues.LoadZoneId;
            firstdivscorecardobj.DivisionId = selectedgridvalue.LoadDivisionId;
            firstdivscorecardobj.TeamA = lblbat1firstinngs.Content.ToString();
            firstdivscorecardobj.TeamB = lblbowl1firstinngs.Content.ToString();


            string abcd = firstdivscorecardobj.FirstDivScoreCardDetailsId.ToString();




            //////////////
            foreach (DataRowView objmatch in dt.DefaultView)
            {
                FirstDivMatchDetails obj1 = Database.GetNewEntity<FirstDivMatchDetails>();

                obj1.FirstDivScoreCardId = abcd;


                obj1.SerialNoA = Convert.ToInt16(objmatch["SLNOA"].ToString());
                obj1.TeamAPlayer = objmatch["Name Of PlayersA"].ToString();
                obj1.KSCAUIDA = objmatch["KSCA UIDA"].ToString();
                obj1.DismissalAFirstInngs = objmatch["DismissalBAT1FI"].ToString();

                // TEAM 1  FIRST INNGS BATTING
                if (objmatch["RunsBAT1FI"].ToString() == "")
                {
                    obj1.BattingRunsAFirstInngs = 0;
                }
                else
                {
                    obj1.BattingRunsAFirstInngs = Convert.ToInt16(objmatch["RunsBAT1FI"].ToString());
                }

                if (objmatch["MinsBAT1FI"].ToString() == "")
                {
                    obj1.MinsAFirstInngs = 0;
                }
                else
                {
                    obj1.MinsAFirstInngs = Convert.ToInt16(objmatch["MinsBAT1FI"].ToString());
                }

                if (objmatch["BallsBAT1FI"].ToString() == "")
                {
                    obj1.BallsAFirstInngs = 0;

                }
                else
                {
                    obj1.BallsAFirstInngs = Convert.ToInt16(objmatch["BallsBAT1FI"].ToString());
                }

                if (objmatch["FoursBAT1FI"].ToString() == "")
                {
                    obj1.FoursAFirstInngs = 0;
                }
                else
                {
                    obj1.FoursAFirstInngs = Convert.ToInt16(objmatch["FoursBAT1FI"].ToString());
                }

                if (objmatch["SixesBAT1FI"].ToString() == "")
                {
                    obj1.SixesAFirstInngs = 0;
                }
                else
                {
                    obj1.SixesAFirstInngs = Convert.ToInt16(objmatch["SixesBAT1FI"].ToString());
                }



                // TEAM 1  SECOND INNGS BATTING
                obj1.DismissalASecondInngs = objmatch["DismissalBAT1SI"].ToString();

                if (objmatch["RunsBAT1SI"].ToString() == "")
                {
                    obj1.BattingRunsASecondInngs = 0;
                }
                else
                {
                    obj1.BattingRunsASecondInngs = Convert.ToInt16(objmatch["RunsBAT1SI"].ToString());
                }

                if (objmatch["MinsBAT1SI"].ToString() == "")
                {
                    obj1.MinsASecondInngs = 0;
                }
                else
                {
                    obj1.MinsASecondInngs = Convert.ToInt16(objmatch["MinsBAT1SI"].ToString());
                }

                if (objmatch["BallsBAT1SI"].ToString() == "")
                {
                    obj1.BallsASecondInngs = 0;

                }
                else
                {
                    obj1.BallsASecondInngs = Convert.ToInt16(objmatch["BallsBAT1SI"].ToString());
                }

                if (objmatch["FoursBAT1SI"].ToString() == "")
                {
                    obj1.FoursASecondInngs = 0;
                }
                else
                {
                    obj1.FoursASecondInngs = Convert.ToInt16(objmatch["FoursBAT1SI"].ToString());
                }

                if (objmatch["SixesBAT1SI"].ToString() == "")
                {
                    obj1.SixesASecondInngs = 0;
                }
                else
                {
                    obj1.SixesASecondInngs = Convert.ToInt16(objmatch["SixesBAT1SI"].ToString());
                }

                // TEAM 2  FIRST INNGS BOWLING
                if (objmatch["OversBOWL2FI"].ToString() == "")
                {
                    obj1.OversBFirstInngs = 0;
                }
                else
                {
                    obj1.OversBFirstInngs = Convert.ToDecimal(objmatch["OversBOWL2FI"].ToString());
                }

                if (objmatch["MaidensBOWL2FI"].ToString() == "")
                {
                    obj1.MaidensBFirstInngs = 0;
                }
                else
                {
                    obj1.MaidensBFirstInngs = Convert.ToInt16(objmatch["MaidensBOWL2FI"].ToString());
                }


                if (objmatch["BrunsBOWL2FI"].ToString() == "")
                {
                    obj1.BowlingRunsBFirstInngs = 0;
                }
                else
                {
                    obj1.BowlingRunsBFirstInngs = Convert.ToInt16(objmatch["BrunsBOWL2FI"].ToString());
                }

                if (objmatch["WicketsBOWL2FI"].ToString() == "")
                {
                    obj1.WicketsBFirstInngs = 0;
                }
                else
                {
                    obj1.WicketsBFirstInngs = Convert.ToInt16(objmatch["WicketsBOWL2FI"].ToString());
                }

                if (objmatch["No BallBOWL2FI"].ToString() == "")
                {
                    obj1.NoBallsBFirstInngs = 0;
                }
                else
                {
                    obj1.NoBallsBFirstInngs = Convert.ToInt16(objmatch["No BallBOWL2FI"].ToString());
                }

                if (objmatch["WideBOWL2FI"].ToString() == "")
                {
                    obj1.WidesBFirstinngs = 0;
                }
                else
                {
                    obj1.WidesBFirstinngs = Convert.ToInt16(objmatch["WideBOWL2FI"].ToString());
                }

                if (objmatch["AvgBOWL2FI"].ToString() == "")
                {
                    obj1.AverageBFirstInngs = 0;
                }
                else
                {
                    obj1.AverageBFirstInngs = Convert.ToInt16(objmatch["AvgBOWL2FI"].ToString());
                }

                // TEAM 2  SECOND INNGS BOWLING (CORRECT)
                if (objmatch["OversBOWL2SI"].ToString() == "")
                {
                    obj1.OversBSecondInngs = 0;
                }
                else
                {
                    obj1.OversBSecondInngs = Convert.ToDecimal(objmatch["OversBOWL2SI"].ToString());
                }

                if (objmatch["MaidensBOWL2SI"].ToString() == "")
                {
                    obj1.MaidensBSecondInngs = 0;
                }
                else
                {
                    obj1.MaidensBSecondInngs = Convert.ToInt16(objmatch["MaidensBOWL2SI"].ToString());
                }


                if (objmatch["BrunsBOWL2SI"].ToString() == "")
                {
                    obj1.BowlingRunsBSecondInngs = 0;
                }
                else
                {
                    obj1.BowlingRunsBSecondInngs = Convert.ToInt16(objmatch["BrunsBOWL2SI"].ToString());
                }

                if (objmatch["WicketsBOWL2SI"].ToString() == "")
                {
                    obj1.WicketsBSecondInngs = 0;
                }
                else
                {
                    obj1.WicketsBSecondInngs = Convert.ToInt16(objmatch["WicketsBOWL2SI"].ToString());
                }

                if (objmatch["No BallBOWL2SI"].ToString() == "")
                {
                    obj1.NoBallsBSecondInngs = 0;
                }
                else
                {
                    obj1.NoBallsBSecondInngs = Convert.ToInt16(objmatch["No BallBOWL2SI"].ToString());
                }

                if (objmatch["WideBOWL2SI"].ToString() == "")
                {
                    obj1.WidesBSecondinngs = 0;
                }
                else
                {
                    obj1.WidesBSecondinngs = Convert.ToInt16(objmatch["WideBOWL2SI"].ToString());
                }

                if (objmatch["AvgBOWL2SI"].ToString() == "")
                {
                    obj1.AverageBSecondInngs = 0;
                }
                else
                {
                    obj1.AverageBSecondInngs = Convert.ToInt16(objmatch["AvgBOWL2SI"].ToString());
                }

                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                // TEAM 2  FIRST INNGS BATTING
                obj1.SerialNoB = Convert.ToInt16(objmatch["SLNOB"].ToString());
                obj1.TeamBPlayer = objmatch["Name Of PlayersB"].ToString();
                obj1.KSCAUIDB = objmatch["KSCA UIDB"].ToString();

                obj1.DismissalBFirstInngs = objmatch["DismissalBAT2FI"].ToString();

                if (objmatch["RunsBAT2FI"].ToString() == "")
                {
                    obj1.BattingRunsBFirstInngs = 0;
                }
                else
                {
                    obj1.BattingRunsBFirstInngs = Convert.ToInt16(objmatch["RunsBAT2FI"].ToString());
                }

                if (objmatch["MinsBAT2FI"].ToString() == "")
                {
                    obj1.MinsBFirstInngs = 0;
                }
                else
                {
                    obj1.MinsBFirstInngs = Convert.ToInt16(objmatch["MinsBAT2FI"].ToString());
                }

                if (objmatch["BallsBAT2FI"].ToString() == "")
                {
                    obj1.BallsBFirstInngs = 0;

                }
                else
                {
                    obj1.BallsBFirstInngs = Convert.ToInt16(objmatch["BallsBAT2FI"].ToString());
                }

                if (objmatch["FoursBAT2FI"].ToString() == "")
                {
                    obj1.FoursBFirstInngs = 0;
                }
                else
                {
                    obj1.FoursBFirstInngs = Convert.ToInt16(objmatch["FoursBAT2FI"].ToString());
                }

                if (objmatch["SixesBAT2FI"].ToString() == "")
                {
                    obj1.SixesBFirstInngs = 0;
                }
                else
                {
                    obj1.SixesBFirstInngs = Convert.ToInt16(objmatch["SixesBAT2FI"].ToString());
                }


                // TEAM 2  SECOND INNGS BATTING

                obj1.DismissalBSecondInngs = objmatch["DismissalBAT2SI"].ToString();

                if (objmatch["RunsBAT2SI"].ToString() == "")
                {
                    obj1.BattingRunsBSecondInngs = 0;
                }
                else
                {
                    obj1.BattingRunsBSecondInngs = Convert.ToInt16(objmatch["RunsBAT2SI"].ToString());
                }

                if (objmatch["MinsBAT2SI"].ToString() == "")
                {
                    obj1.MinsBSecondInngs = 0;
                }
                else
                {
                    obj1.MinsBSecondInngs = Convert.ToInt16(objmatch["MinsBAT2SI"].ToString());
                }

                if (objmatch["BallsBAT2SI"].ToString() == "")
                {
                    obj1.BallsBSecondInngs = 0;

                }
                else
                {
                    obj1.BallsBSecondInngs = Convert.ToInt16(objmatch["BallsBAT2SI"].ToString());
                }

                if (objmatch["FoursBAT2SI"].ToString() == "")
                {
                    obj1.FoursBSecondInngs = 0;
                }
                else
                {
                    obj1.FoursBSecondInngs = Convert.ToInt16(objmatch["FoursBAT2SI"].ToString());
                }

                if (objmatch["SixesBAT2SI"].ToString() == "")
                {
                    obj1.SixesBSecondInngs = 0;
                }
                else
                {
                    obj1.SixesBSecondInngs = Convert.ToInt16(objmatch["SixesBAT2SI"].ToString());
                }


                // TEAM 1  FIRST INNGS BOWLING
                if (objmatch["OversBOWL1FI"].ToString() == "")
                {
                    obj1.OversAFirstInngs = 0;
                }
                else
                {
                    obj1.OversAFirstInngs = Convert.ToDecimal(objmatch["OversBOWL1FI"].ToString());
                }

                if (objmatch["MaidensBOWL1FI"].ToString() == "")
                {
                    obj1.MaidensAFirstInngs = 0;
                }
                else
                {
                    obj1.MaidensAFirstInngs = Convert.ToInt16(objmatch["MaidensBOWL1FI"].ToString());
                }


                if (objmatch["BrunsBOWL1FI"].ToString() == "")
                {
                    obj1.BowlingRunsAFirstInngs = 0;
                }
                else
                {
                    obj1.BowlingRunsAFirstInngs = Convert.ToInt16(objmatch["BrunsBOWL1FI"].ToString());
                }

                if (objmatch["WicketsBOWL1FI"].ToString() == "")
                {
                    obj1.WicketsAFirstInngs = 0;
                }
                else
                {
                    obj1.WicketsAFirstInngs = Convert.ToInt16(objmatch["WicketsBOWL1FI"].ToString());
                }

                if (objmatch["No BallBOWL1FI"].ToString() == "")
                {
                    obj1.NoBallsAFirstInngs = 0;
                }
                else
                {
                    obj1.NoBallsAFirstInngs = Convert.ToInt16(objmatch["No BallBOWL1FI"].ToString());
                }

                if (objmatch["WideBOWL1FI"].ToString() == "")
                {
                    obj1.WidesAFirstinngs = 0;
                }
                else
                {
                    obj1.WidesAFirstinngs = Convert.ToInt16(objmatch["WideBOWL1FI"].ToString());
                }

                if (objmatch["AvgBOWL1FI"].ToString() == "")
                {
                    obj1.AverageAFirstInngs = 0;
                }
                else
                {
                    obj1.AverageAFirstInngs = Convert.ToInt16(objmatch["AvgBOWL1FI"].ToString());
                }

                // TEAM 1  SECOND INNGS BOWLING
                if (objmatch["OversBOWL1SI"].ToString() == "")
                {
                    obj1.OversASecondInngs = 0;
                }
                else
                {
                    obj1.OversASecondInngs = Convert.ToDecimal(objmatch["OversBOWL1SI"].ToString());
                }

                if (objmatch["MaidensBOWL1SI"].ToString() == "")
                {
                    obj1.MaidensASecondInngs = 0;
                }
                else
                {
                    obj1.MaidensASecondInngs = Convert.ToInt16(objmatch["MaidensBOWL1SI"].ToString());
                }


                if (objmatch["BrunsBOWL1SI"].ToString() == "")
                {
                    obj1.BowlingRunsASecondInngs = 0;
                }
                else
                {
                    obj1.BowlingRunsASecondInngs = Convert.ToInt16(objmatch["BrunsBOWL1SI"].ToString());
                }

                if (objmatch["WicketsBOWL1SI"].ToString() == "")
                {
                    obj1.WicketsASecondInngs = 0;
                }
                else
                {
                    obj1.WicketsASecondInngs = Convert.ToInt16(objmatch["WicketsBOWL1SI"].ToString());
                }

                if (objmatch["No BallBOWL1SI"].ToString() == "")
                {
                    obj1.NoBallsASecondInngs = 0;
                }
                else
                {
                    obj1.NoBallsASecondInngs = Convert.ToInt16(objmatch["No BallBOWL1SI"].ToString());
                }

                if (objmatch["WideBOWL1SI"].ToString() == "")
                {
                    obj1.WidesASecondinngs = 0;
                }
                else
                {
                    obj1.WidesASecondinngs = Convert.ToInt16(objmatch["WideBOWL1SI"].ToString());
                }

                if (objmatch["AvgBOWL1SI"].ToString() == "")
                {
                    obj1.AverageASecondInngs = 0;
                }
                else
                {
                    obj1.AverageASecondInngs = Convert.ToInt16(objmatch["AvgBOWL1SI"].ToString());
                }

                obj1.MatchId = selectedgridvalue.matchid;
                Database.SaveEntity<FirstDivMatchDetails>(obj1, oleconn);

                obj1 = null;
            }

            Database.SaveEntity<FirstDivScoreCardDetails>(firstdivscorecardobj, oleconn);
            Database.SaveEntity<TeamReportTable>(ObjTeamReport1, oleconn);
            Database.SaveEntity<TeamReportTable>(ObjTeamReport2, oleconn);
            Database.SaveEntity<LocationReportTable>(ObjLocationReport, oleconn);

            MessageBox.Show("Saved");
            btnsave.IsEnabled = false;
            Resultsecondinngs.IsEnabled = false;
            progessbar.Value = 100;

            //Team1FirstAndSecoond Innings clear
            dt.Rows.Clear();
            start_time1firstinngs.Text = "";
            start_time2firstinngs.Text = "";
            close_time1firstinngs.Text = "";
            close_time2firstinngs.Text = "";
            txtDuration1firstinngs.Text = string.Empty;
            txtDuration1secondinngs.Text = string.Empty;
            txtbyes1firstinngs.Text = string.Empty;
            txtbyes1secondinngs.Text = string.Empty;
            txtlegbyes1firstinngs.Text = string.Empty;
            txtlegbyes1secondinngs.Text = string.Empty;
            txtpenalty1secondinngs.Text = string.Empty;
            txttotalextras1firstinngs.Text = string.Empty;
            txttotalextras1secondinngs.Text = string.Empty;
            txtruns1firstinngs.Text = string.Empty;
            txtruns1secondinngs.Text = string.Empty;
            txtwickets1firstinngs.Text = string.Empty;
            txtwickets1secondinngs.Text = string.Empty;
            txtovers1firstinngs.Text = string.Empty;
            txtovers1secondinngs.Text = string.Empty;
            txtrunout1firstinnings.Text = string.Empty;
            txtrunout1seccondinnings.Text = string.Empty;
            txtrunout2firstinngs.Text = string.Empty;
            txtrunout1seccondinnings.Text = string.Empty;
            //Team2FirstInnings
            start_time2firstinngs.Text = "";
            close_time2firstinngs.Text = "";
            txtDuration2firstinngs.Text = string.Empty;
            txtbyes2firstinngs.Text = string.Empty;
            txtlegbyes2firstinngs.Text = string.Empty;
            txtpenalty2firstinngs.Text = string.Empty;
            txttotalextras2firstinngs.Text = string.Empty;
            txtruns2firstinngs.Text = string.Empty;
            txtwickets2firstinngs.Text = string.Empty;
            txtovers2firstinngs.Text = string.Empty;

            //Team2SecondInnings
            start_time2secondinngs.Text = "";
            close_time2secondinngs.Text = "";
            txtDuration2secondinngs.Text = string.Empty;
            txtbyes2secondinngs.Text = string.Empty;
            txtlegbyes2secondinngs.Text = string.Empty;
            txtpenalty2secondinngs.Text = string.Empty;
            txttotalextras2secondinngs.Text = string.Empty;
            txtruns2secondinngs.Text = string.Empty;
            txtwickets2secondinngs.Text = string.Empty;
            txtovers2secondinngs.Text = string.Empty;

            chkmatchnotplayed.IsEnabled = false;
            chkteam1secondinnings.IsEnabled = false;
            chkteam2firstinnings.IsEnabled = false;
            chkteam2secondinnings.IsEnabled = false;

            lblbat1firstinngs.Content = null;
            lblbat2firstinngs.Content = null;
            lblbowl1firstinngs.Content = null;
            lblbowl2firstinngs.Content = null;

            lblbat1secondinngs.Content = null;
            lblbat2secondinngs.Content = null;
            lblbowl1secondinngs.Content = null;
            lblbowl2secondinngs.Content = null;

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



                //1

                dgvbat1firstinngs.ItemsSource = null;
                //2

                dgvbowl1firstinngs.ItemsSource = null;
                //3

                dgvbat1secondinngs.ItemsSource = null;
                //4

                dgvbowl1secondinngs.ItemsSource = null;
                //5

                dgvbat2firstinngs.ItemsSource = null;
                //6

                dgvbowl2firstinngs.ItemsSource = null;
                //7

                dgvbat2secondinngs.ItemsSource = null;
                //8

                dgvbowl2secondinngs.ItemsSource = null;

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


                ismatchnotpalyed = false;
                team1secondinningsbattingnotplayed = false;
                team2firstinningsbattingnotplayed = false;
                team2secondbattingnotplayed = false;
                team1firstinningsbowlingnotplayed = false;
                team1secondinningsbowlingnotplayed = false;
                team2secondinningsbowlingnotplayed = false;


                //objseason1.Clear();
                //objzone.Clear();
                //objdivision1.Clear();
                objteams.Clear();
                objlocationnames.Clear();
                objFirstDivPointsCricket.Clear();
                objplayer1.Clear();
                objbat1.Clear();
                objbowl1.Clear();


            }




        }

        bool playerexitbt1 = false;
        bool playerexitbl1 = false;
        bool playerexitbt2 = false;
        bool playerexitbl2 = false;


        // ObservableCollection<> batsman = Database.GetEntityList<BestBatsman>(false, false, false, Database.getReadConnection(), "RecordStatus", "PlayerName");
        // ObservableCollection<BestBowler> bowler = Database.GetEntityList<BestBowler>(false, false, false, Database.getReadConnection(), "RecordStatus", "PlayerName");

        public bool checkplayerbt1(string playerkscauid)
        {
            ObservableCollection<BestBatsman> batsman = Database.GetEntityList<BestBatsman>(false, false, false, Database.getReadConnection(), "RecordStatus", "PlayerName");


            try
            {

                foreach (var item in batsman)
                {
                    if (item.KSCAUID == playerkscauid)
                    {
                        playerexitbt1 = true;

                        isaddedbt1 = false;

                        return true;
                    }

                    else
                    {
                        playerexitbt1 = false;
                        // exist = false;
                        // return false;

                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool checkplayerbl1(string playerkscauid)
        {
            ObservableCollection<BestBowler> bowler = Database.GetEntityList<BestBowler>(false, false, false, Database.getReadConnection(), "RecordStatus", "PlayerName");

            try
            {

                foreach (var item in bowler)
                {
                    if (item.KSCAUID == playerkscauid)
                    {
                        playerexitbl1 = true;
                        isaddedbl1 = false;
                        return true;
                    }

                    else
                    {
                        playerexitbl1 = false;
                        // return false;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool checkplayerbt2(string playerkscauid)
        {
            ObservableCollection<BestBatsman> batsman = Database.GetEntityList<BestBatsman>(false, false, false, oleconn, "RecordStatus", "KSCAUID");
            try
            {

                foreach (var item in batsman)
                {
                    if (item.KSCAUID == playerkscauid)
                    {
                        playerexitbt2 = true;
                        isaddedbt2 = false;
                        return true;
                    }

                    else
                    {
                        playerexitbt2 = false;
                        // return false;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public bool checkplayerbl2(string playerkscauid)
        {
            ObservableCollection<BestBowler> bowler = Database.GetEntityList<BestBowler>(false, false, false, Database.getReadConnection(), "RecordStatus", "PlayerName");

            try
            {
                //  ObservableCollection<BestBowler> bowler = Database.GetEntityList<BestBowler>(false, false, false, oleconn, "RecordStatus", "KSCAUID");
                foreach (var item in bowler)
                {
                    if (item.KSCAUID == playerkscauid)
                    {
                        playerexitbl2 = true;
                        isaddedbl2 = false;
                        return true;
                    }

                    else
                    {
                        playerexitbl2 = false;
                        //return false;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {


            ObservableCollection<FirstDivScoreCardDetails> lstscorecard = Database.GetEntityList<FirstDivScoreCardDetails>(false, false, false, oleconn, "RecordStatus", "FirstDivScoreCardDetailsId");
            ObservableCollection<FirstDivMatchDetails> lstMatchDetails = Database.GetEntityList<FirstDivMatchDetails>(false, false, false, oleconn, "RecordStatus", "FirstDivMatchDetailsId");

            var objscorecard = from s in lstscorecard where s.MatchId == selectedgridvalue.matchid && s.DivisionId.ToString() == selectedgridvalue.LoadDivisionId && s.SeasonId.ToString() == TempValues.LoadSeasonId select s;
            List<FirstDivScoreCardDetails> lstscorecardfilter = objscorecard.ToList<FirstDivScoreCardDetails>();

            var result = from s in lstMatchDetails where s.MatchId == selectedgridvalue.matchid select s;
            List<FirstDivMatchDetails> lstMatchDetailsSort = result.ToList<FirstDivMatchDetails>();

            if (lstMatchDetailsSort.Count == 0)
            {
                if (_isResultclick == false)
                {
                    teamA.Clear();
                    teamB.Clear();
                    KSCAUIDA.Clear();
                    KSCAUIDB.Clear();


                    chkmatchnotplayed.IsEnabled = true;
                    chkteam2firstinnings.IsEnabled = true;
                    chkteam2secondinnings.IsEnabled = true;

                    //Team1FirstInnings & Team2FirstInnings
                    start_time1firstinngs.IsEnabled = true;
                    start_time2firstinngs.IsEnabled = true;
                    close_time1firstinngs.IsEnabled = true;
                    close_time2firstinngs.IsEnabled = true;
                    txtDuration1firstinngs.IsEnabled = true;
                    txtDuration1secondinngs.IsEnabled = true;
                    txtbyes1firstinngs.IsEnabled = true;
                    txtbyes1secondinngs.IsEnabled = true;
                    txtlegbyes1firstinngs.IsEnabled = true;
                    txtlegbyes1secondinngs.IsEnabled = true;
                    txtpenalty1firstinngs.IsEnabled = true;
                    txtpenalty1secondinngs.IsEnabled = true;
                    txttotalextras1firstinngs.IsEnabled = true;
                    txttotalextras2secondinngs.IsEnabled = true;
                    txtruns1firstinngs.IsEnabled = true;
                    txtruns1secondinngs.IsEnabled = true;
                    txtwickets1firstinngs.IsEnabled = true;
                    txtwickets1secondinngs.IsEnabled = true;
                    txtovers1firstinngs.IsEnabled = true;
                    txtovers1secondinngs.IsEnabled = true;
                    close_time1secondinngs.IsEnabled = true;
                    dgvbat1firstinngs.IsReadOnly = false;
                    dgvbat1secondinngs.IsReadOnly = false;
                    dgvbowl1firstinngs.IsReadOnly = false;
                    dgvbowl1secondinngs.IsReadOnly = false;
                    txtrunout1firstinnings.IsEnabled = true;
                    txtrunout1seccondinnings.IsEnabled = true;
                    txtrunout2firstinngs.IsEnabled = true;
                    txtrunout2secondinngs.IsEnabled = true;



                    //Team2FirstInnings
                    start_time1firstinngs.IsEnabled = true;
                    close_time2firstinngs.IsEnabled = true;
                    txtDuration2firstinngs.IsEnabled = true;
                    txtbyes2firstinngs.IsEnabled = true;
                    txtlegbyes2firstinngs.IsEnabled = true;
                    txtpenalty2firstinngs.IsEnabled = true;
                    txttotalextras2firstinngs.IsEnabled = true;
                    txtruns2firstinngs.IsEnabled = true;
                    txtwickets2firstinngs.IsEnabled = true;
                    txtovers2firstinngs.IsEnabled = true;
                    txttotalextras2firstinngs.IsEnabled = true;
                    dgvbat2firstinngs.IsReadOnly = false;
                    dgvbat2secondinngs.IsReadOnly = false;

                    //Team2SecondInnings
                    start_time1secondinngs.IsEnabled = true;
                    close_time2secondinngs.IsEnabled = true;
                    txtDuration2secondinngs.IsEnabled = true;
                    txtbyes2secondinngs.IsEnabled = true;
                    txtlegbyes2secondinngs.IsEnabled = true;
                    txtpenalty2secondinngs.IsEnabled = true;
                    txttotalextras2secondinngs.IsEnabled = true;
                    txtruns2secondinngs.IsEnabled = true;
                    txtwickets2secondinngs.IsEnabled = true;
                    txtovers2secondinngs.IsEnabled = true;
                    btnsave.IsEnabled = true;
                    Resultsecondinngs.IsEnabled = true;
                    dgvbowl2firstinngs.IsReadOnly = false;
                    dgvbowl2secondinngs.IsReadOnly = false;

                    //buttons                   
                    btn1scorefirstinngs.IsEnabled = true;
                    btn1scoresecondinngs.IsEnabled = true;
                    btn2scorefirstinngs.IsEnabled = true;
                    btn2scoresecondinngs.IsEnabled = true;
                    start_time2secondinngs.IsEnabled = true;


                    //////
                    //Team1FirstAndSecond Innings clear
                    start_time1firstinngs.Text = "";
                    start_time1secondinngs.Text = "";
                    close_time1firstinngs.Text = "";
                    close_time1secondinngs.Text = "";
                    txtDuration1firstinngs.Text = string.Empty;
                    txtDuration1secondinngs.Text = string.Empty;
                    txtbyes1firstinngs.Text = string.Empty;
                    txtbyes1secondinngs.Text = string.Empty;
                    txtlegbyes1firstinngs.Text = string.Empty;
                    txtlegbyes1secondinngs.Text = string.Empty;
                    txtpenalty1firstinngs.Text = string.Empty;
                    txtpenalty1secondinngs.Text = string.Empty;
                    txtrunout1firstinnings.Text = string.Empty;
                    txtrunout1seccondinnings.Text = string.Empty;
                    txttotalextras1firstinngs.Text = string.Empty;
                    txttotalextras1secondinngs.Text = string.Empty;
                    txtruns1firstinngs.Text = string.Empty;
                    txtruns1secondinngs.Text = string.Empty;
                    txtwickets1firstinngs.Text = string.Empty;
                    txtwickets1secondinngs.Text = string.Empty;
                    txtovers1firstinngs.Text = string.Empty;
                    txtovers1secondinngs.Text = string.Empty;

                    //Team2FirstInnings
                    start_time2firstinngs.Text = "";
                    close_time2firstinngs.Text = "";
                    txtDuration2firstinngs.Text = string.Empty;
                    txtbyes2firstinngs.Text = string.Empty;
                    txtlegbyes2firstinngs.Text = string.Empty;
                    txtpenalty2firstinngs.Text = string.Empty;
                    txtrunout2firstinngs.Text = string.Empty;
                    txttotalextras2firstinngs.Text = string.Empty;
                    txtruns2firstinngs.Text = string.Empty;
                    txtwickets2firstinngs.Text = string.Empty;
                    txtovers2firstinngs.Text = string.Empty;

                    //Team2SecondInnings
                    start_time2secondinngs.Text = "";
                    close_time2secondinngs.Text = "";
                    txtDuration2secondinngs.Text = string.Empty;
                    txtbyes2secondinngs.Text = string.Empty;
                    txtlegbyes2secondinngs.Text = string.Empty;
                    txtpenalty2secondinngs.Text = string.Empty;
                    txtrunout2secondinngs.Text = string.Empty;
                    txttotalextras2secondinngs.Text = string.Empty;
                    txtruns2secondinngs.Text = string.Empty;
                    txtwickets2secondinngs.Text = string.Empty;
                    txtovers2secondinngs.Text = string.Empty;

                    //bool functions
                    ismatchnotpalyed = false;
                    team1secondinningsbattingnotplayed = false;
                    team2firstinningsbattingnotplayed = false;
                    team2secondbattingnotplayed = false;
                    team1firstinningsbowlingnotplayed = false;
                    team1secondinningsbowlingnotplayed = false;
                    team2secondinningsbowlingnotplayed = false;


                    //checkbox
                    chkmatchnotplayed.IsEnabled = true;
                    chkteam1secondinnings.IsEnabled = true;
                    chkteam2firstinnings.IsEnabled = true;
                    chkteam2secondinnings.IsEnabled = true;

                    ///////
                    loadplayer();
                    lblseason.Content = season_zone_div.name[0].ToString();
                    lblzone.Content = season_zone_div.name[1].ToString();
                    lbldivision.Content = season_zone_div.name[2].ToString();

                }
            }
            if (lstMatchDetailsSort.Count > 0)
            {



                DataTable dtbat1First = new DataTable();
                dtbat1First.Columns.Add("SLNO");
                dtbat1First.Columns.Add("Name Of Players");
                dtbat1First.Columns.Add("KSCA UID");
                dtbat1First.Columns.Add("Dismissal");
                dtbat1First.Columns.Add("Runs");
                dtbat1First.Columns.Add("Mins");
                dtbat1First.Columns.Add("Balls");
                dtbat1First.Columns.Add("Fours");
                dtbat1First.Columns.Add("Sixes");

                DataTable dtbat1Second = new DataTable();
                dtbat1Second.Columns.Add("SLNO");
                dtbat1Second.Columns.Add("Name Of Players");
                dtbat1Second.Columns.Add("KSCA UID");
                dtbat1Second.Columns.Add("Dismissal");
                dtbat1Second.Columns.Add("Runs");
                dtbat1Second.Columns.Add("Mins");
                dtbat1Second.Columns.Add("Balls");
                dtbat1Second.Columns.Add("Fours");
                dtbat1Second.Columns.Add("Sixes");

                DataTable dtbowl1First = new DataTable();
                dtbowl1First.Columns.Add("SLNO");
                dtbowl1First.Columns.Add("Name Of Players");
                dtbowl1First.Columns.Add("KSCA UID");
                dtbowl1First.Columns.Add("Overs");
                dtbowl1First.Columns.Add("Maidens");
                dtbowl1First.Columns.Add("Runs");
                dtbowl1First.Columns.Add("Wickets");
                dtbowl1First.Columns.Add("No Ball");
                dtbowl1First.Columns.Add("Wide");
                dtbowl1First.Columns.Add("Avg");

                DataTable dtbowl1Second = new DataTable();
                dtbowl1Second.Columns.Add("SLNO");
                dtbowl1Second.Columns.Add("Name Of Players");
                dtbowl1Second.Columns.Add("KSCA UID");
                dtbowl1Second.Columns.Add("Overs");
                dtbowl1Second.Columns.Add("Maidens");
                dtbowl1Second.Columns.Add("Runs");
                dtbowl1Second.Columns.Add("Wickets");
                dtbowl1Second.Columns.Add("No Ball");
                dtbowl1Second.Columns.Add("Wide");
                dtbowl1Second.Columns.Add("Avg");

                DataTable dtbat2First = new DataTable();
                dtbat2First.Columns.Add("SLNO");
                dtbat2First.Columns.Add("Name Of Players");
                dtbat2First.Columns.Add("KSCA UID");
                dtbat2First.Columns.Add("Dismissal");
                dtbat2First.Columns.Add("Runs");
                dtbat2First.Columns.Add("Mins");
                dtbat2First.Columns.Add("Balls");
                dtbat2First.Columns.Add("Fours");
                dtbat2First.Columns.Add("Sixes");

                DataTable dtbat2Second = new DataTable();
                dtbat2Second.Columns.Add("SLNO");
                dtbat2Second.Columns.Add("Name Of Players");
                dtbat2Second.Columns.Add("KSCA UID");
                dtbat2Second.Columns.Add("Dismissal");
                dtbat2Second.Columns.Add("Runs");
                dtbat2Second.Columns.Add("Mins");
                dtbat2Second.Columns.Add("Balls");
                dtbat2Second.Columns.Add("Fours");
                dtbat2Second.Columns.Add("Sixes");

                DataTable dtbowl2First = new DataTable();
                dtbowl2First.Columns.Add("SLNO");
                dtbowl2First.Columns.Add("Name Of Players");
                dtbowl2First.Columns.Add("KSCA UID");
                dtbowl2First.Columns.Add("Overs");
                dtbowl2First.Columns.Add("Maidens");
                dtbowl2First.Columns.Add("Runs");
                dtbowl2First.Columns.Add("Wickets");
                dtbowl2First.Columns.Add("No Ball");
                dtbowl2First.Columns.Add("Wide");
                dtbowl2First.Columns.Add("Avg");

                DataTable dtbowl2Second = new DataTable();
                dtbowl2Second.Columns.Add("SLNO");
                dtbowl2Second.Columns.Add("Name Of Players");
                dtbowl2Second.Columns.Add("KSCA UID");
                dtbowl2Second.Columns.Add("Overs");
                dtbowl2Second.Columns.Add("Maidens");
                dtbowl2Second.Columns.Add("Runs");
                dtbowl2Second.Columns.Add("Wickets");
                dtbowl2Second.Columns.Add("No Ball");
                dtbowl2Second.Columns.Add("Wide");
                dtbowl2Second.Columns.Add("Avg");


                int Slno1 = 1;
                int Slno2 = 1;
                int Slno3 = 1;
                int Slno4 = 1;
                int Slno5 = 1;
                int Slno6 = 1;
                int Slno7 = 1;
                int Slno8 = 1;

                chkmatchnotplayed.IsEnabled = false;
                chkteam1secondinnings.IsEnabled = false;
                chkteam2firstinnings.IsEnabled = false;
                chkteam2secondinnings.IsEnabled = false;

                if (lstscorecardfilter.Count > 0)
                {
                    if (lstMatchDetailsSort.Count > 0)
                    {

                        foreach (var objscore in lstscorecardfilter)
                        {

                            foreach (var obj in lstMatchDetailsSort)
                            {
                                if (objscore.SeasonId.ToString() == TempValues.LoadSeasonId.ToString())
                                {
                                    if (objscore.DivisionId.ToString() == selectedgridvalue.LoadDivisionId)
                                    {
                                        lblbat1firstinngs.Content = objscore.TeamA;
                                        lblbowl1firstinngs.Content = objscore.TeamB;

                                        lblbat1secondinngs.Content = objscore.TeamA;
                                        lblbowl1secondinngs.Content = objscore.TeamB;

                                        lblbat2firstinngs.Content = objscore.TeamB;
                                        lblbowl2firstinngs.Content = objscore.TeamA;

                                        lblbat2secondinngs.Content = objscore.TeamB;
                                        lblbowl2secondinngs.Content = objscore.TeamA;


                                        //dgvbat1FirstInnings

                                        DataRow drFirst = dtbat1First.NewRow();

                                        drFirst["SLNO"] = Slno1;
                                        drFirst["Name Of Players"] = obj.TeamAPlayer.ToString();
                                        drFirst["KSCA UID"] = obj.KSCAUIDA;
                                        drFirst["Dismissal"] = obj.DismissalAFirstInngs;
                                        drFirst["Runs"] = obj.BattingRunsAFirstInngs;
                                        drFirst["Mins"] = obj.MinsAFirstInngs;
                                        drFirst["Balls"] = obj.BallsAFirstInngs;
                                        drFirst["Fours"] = obj.FoursAFirstInngs;
                                        drFirst["Sixes"] = obj.SixesAFirstInngs;



                                        dtbat1First.Rows.Add(drFirst);
                                        Slno1++;

                                        //dgvbat1SecondInnings

                                        DataRow drTeam1Second = dtbat1Second.NewRow();

                                        drTeam1Second["SLNO"] = Slno2;
                                        drTeam1Second["Name Of Players"] = obj.TeamAPlayer.ToString();
                                        drTeam1Second["KSCA UID"] = obj.KSCAUIDA;
                                        drTeam1Second["Dismissal"] = obj.DismissalASecondInngs;
                                        drTeam1Second["Runs"] = obj.BattingRunsASecondInngs;
                                        drTeam1Second["Mins"] = obj.MinsASecondInngs;
                                        drTeam1Second["Balls"] = obj.BallsASecondInngs;
                                        drTeam1Second["Fours"] = obj.FoursASecondInngs;
                                        drTeam1Second["Sixes"] = obj.SixesASecondInngs;



                                        dtbat1Second.Rows.Add(drTeam1Second);
                                        Slno2++;

                                        //dgvbowl1FirstInnings
                                        DataRow drteam1bowl1 = dtbowl1First.NewRow();

                                        drteam1bowl1["SLNO"] = Slno3;
                                        drteam1bowl1["Name Of Players"] = obj.TeamBPlayer.ToString();
                                        drteam1bowl1["KSCA UID"] = obj.KSCAUIDB;
                                        drteam1bowl1["Overs"] = Math.Round(obj.OversAFirstInngs, 1);
                                        drteam1bowl1["Maidens"] = obj.MaidensAFirstInngs;
                                        drteam1bowl1["Runs"] = obj.BowlingRunsAFirstInngs;
                                        drteam1bowl1["Wickets"] = obj.WicketsAFirstInngs;
                                        drteam1bowl1["No Ball"] = obj.NoBallsAFirstInngs;
                                        drteam1bowl1["Wide"] = obj.WidesAFirstinngs;
                                        drteam1bowl1["Avg"] = Math.Round(obj.AverageAFirstInngs, 1);

                                        dtbowl1First.Rows.Add(drteam1bowl1);
                                        Slno3++;


                                        //dgvbowl1secondInnings
                                        DataRow drteam1bowl2 = dtbowl1Second.NewRow();

                                        drteam1bowl2["SLNO"] = Slno4;
                                        drteam1bowl2["Name Of Players"] = obj.TeamBPlayer.ToString();
                                        drteam1bowl2["KSCA UID"] = obj.KSCAUIDB;
                                        drteam1bowl2["Overs"] = Math.Round(obj.OversASecondInngs, 1);
                                        drteam1bowl2["Maidens"] = obj.MaidensASecondInngs;
                                        drteam1bowl2["Runs"] = obj.BowlingRunsASecondInngs;
                                        drteam1bowl2["Wickets"] = obj.WicketsASecondInngs;
                                        drteam1bowl2["No Ball"] = obj.NoBallsASecondInngs;
                                        drteam1bowl2["Wide"] = obj.WidesASecondinngs;
                                        drteam1bowl2["Avg"] = Math.Round(obj.AverageASecondInngs, 1);

                                        dtbowl1Second.Rows.Add(drteam1bowl2);
                                        Slno4++;

                                        //dgvbat2FirstInnings
                                        DataRow drteam2firstbat = dtbat2First.NewRow();

                                        drteam2firstbat["SLNO"] = Slno5;
                                        drteam2firstbat["Name Of Players"] = obj.TeamBPlayer.ToString();
                                        drteam2firstbat["KSCA UID"] = obj.KSCAUIDB;
                                        drteam2firstbat["Dismissal"] = obj.DismissalBFirstInngs;
                                        drteam2firstbat["Runs"] = obj.BattingRunsBFirstInngs;
                                        drteam2firstbat["Mins"] = obj.MinsBFirstInngs;
                                        drteam2firstbat["Balls"] = obj.BallsBFirstInngs;
                                        drteam2firstbat["Fours"] = obj.FoursBFirstInngs;
                                        drteam2firstbat["Sixes"] = obj.SixesBFirstInngs;


                                        dtbat2First.Rows.Add(drteam2firstbat);
                                        Slno5++;

                                        //dgvbat2SecondInnings
                                        DataRow drteam2batSec = dtbat2Second.NewRow();

                                        drteam2batSec["SLNO"] = Slno6;
                                        drteam2batSec["Name Of Players"] = obj.TeamBPlayer.ToString();
                                        drteam2batSec["KSCA UID"] = obj.KSCAUIDB;
                                        drteam2batSec["Dismissal"] = obj.DismissalBSecondInngs;
                                        drteam2batSec["Runs"] = obj.BattingRunsBSecondInngs;
                                        drteam2batSec["Mins"] = obj.MinsBSecondInngs;
                                        drteam2batSec["Balls"] = obj.BallsBSecondInngs;
                                        drteam2batSec["Fours"] = obj.FoursBSecondInngs;
                                        drteam2batSec["Sixes"] = obj.SixesBSecondInngs;


                                        dtbat2Second.Rows.Add(drteam2batSec);
                                        Slno6++;


                                        //dgvbowl2Firstnnings
                                        DataRow drteam2bowl1 = dtbowl2First.NewRow();

                                        drteam2bowl1["SLNO"] = Slno7;
                                        drteam2bowl1["Name Of Players"] = obj.TeamAPlayer.ToString();
                                        drteam2bowl1["KSCA UID"] = obj.KSCAUIDA;
                                        drteam2bowl1["Overs"] = Math.Round(obj.OversBFirstInngs, 1);
                                        drteam2bowl1["Maidens"] = obj.MaidensBFirstInngs;
                                        drteam2bowl1["Runs"] = obj.BowlingRunsBFirstInngs;
                                        drteam2bowl1["Wickets"] = obj.WicketsBFirstInngs;
                                        drteam2bowl1["No Ball"] = obj.NoBallsBFirstInngs;
                                        drteam2bowl1["Wide"] = obj.WidesBFirstinngs;
                                        drteam2bowl1["Avg"] = Math.Round(obj.AverageBFirstInngs, 1);

                                        dtbowl2First.Rows.Add(drteam2bowl1);
                                        Slno7++;


                                        //dgvbowl2secondinnings
                                        DataRow drteam2bowl2 = dtbowl2Second.NewRow();

                                        drteam2bowl2["SLNO"] = Slno8;
                                        drteam2bowl2["Name Of Players"] = obj.TeamAPlayer.ToString();
                                        drteam2bowl2["KSCA UID"] = obj.KSCAUIDA;
                                        drteam2bowl2["Overs"] = Math.Round(obj.OversBSecondInngs, 1);
                                        drteam2bowl2["Maidens"] = obj.MaidensBSecondInngs;
                                        drteam2bowl2["Runs"] = obj.BowlingRunsBSecondInngs;
                                        drteam2bowl2["Wickets"] = obj.WicketsBSecondInngs;
                                        drteam2bowl2["No Ball"] = obj.NoBallsBSecondInngs;
                                        drteam2bowl2["Wide"] = obj.WidesBSecondinngs;
                                        drteam2bowl2["Avg"] = Math.Round(obj.AverageBSecondInngs, 1);

                                        dtbowl2Second.Rows.Add(drteam2bowl2);
                                        Slno8++;

                                    }

                                }
                            }

                            //Team1FirstAndSecoond Innings clear
                            //start_time1firstinngs.Text = "";
                            //start_time2firstinngs.Text = "";
                            //close_time1firstinngs.Text = "";
                            //close_time2firstinngs.Text = "";
                            txtDuration1firstinngs.Text = string.Empty;
                            txtDuration1secondinngs.Text = string.Empty;
                            txtbyes1firstinngs.Text = string.Empty;
                            txtbyes1secondinngs.Text = string.Empty;
                            txtlegbyes1firstinngs.Text = string.Empty;
                            txtlegbyes1secondinngs.Text = string.Empty;
                            txtpenalty1secondinngs.Text = string.Empty;
                            txttotalextras1firstinngs.Text = string.Empty;
                            txttotalextras1secondinngs.Text = string.Empty;
                            txtruns1firstinngs.Text = string.Empty;
                            txtruns1secondinngs.Text = string.Empty;
                            txtwickets1firstinngs.Text = string.Empty;
                            txtwickets1secondinngs.Text = string.Empty;
                            txtovers1firstinngs.Text = string.Empty;
                            txtovers1secondinngs.Text = string.Empty;
                            txtpenalty1firstinngs.Text = string.Empty;
                            //Team2FirstInnings
                            //start_time2firstinngs.Text = "";
                            //close_time2firstinngs.Text = "";
                            txtDuration2firstinngs.Text = string.Empty;
                            txtbyes2firstinngs.Text = string.Empty;
                            txtlegbyes2firstinngs.Text = string.Empty;
                            txtpenalty2firstinngs.Text = string.Empty;
                            txttotalextras2firstinngs.Text = string.Empty;
                            txtruns2firstinngs.Text = string.Empty;
                            txtwickets2firstinngs.Text = string.Empty;
                            txtovers2firstinngs.Text = string.Empty;
                            start_time1secondinngs.Text = "";
                            close_time1secondinngs.Text = "";
                            txtrunout1firstinnings.Text = string.Empty;
                            txtrunout1seccondinnings.Text = string.Empty;

                            txtrunout2firstinngs.Text = string.Empty;
                            txtrunout2secondinngs.Text = string.Empty;
                            //Team2SecondInnings
                            //start_time2secondinngs.Text = "";
                            //close_time2secondinngs.Text = "";
                            txtDuration2secondinngs.Text = string.Empty;
                            txtbyes2secondinngs.Text = string.Empty;
                            txtlegbyes2secondinngs.Text = string.Empty;
                            txtpenalty2secondinngs.Text = string.Empty;
                            txttotalextras2secondinngs.Text = string.Empty;
                            txtruns2secondinngs.Text = string.Empty;
                            txtwickets2secondinngs.Text = string.Empty;
                            txtovers2secondinngs.Text = string.Empty;


                            //Assign Values
                            //Team1FirstAndSecoond Innings clear
                            start_time1firstinngs.Text = (objscore.CommenceTimeAFirstInngs);
                            start_time1secondinngs.Text = (objscore.CommenceTimeASecondInngs);
                            close_time1firstinngs.Text = (objscore.EndTimeAFirstInngs);
                            close_time1secondinngs.Text = (objscore.EndTimeASecondInngs);
                            txtDuration1firstinngs.Text = objscore.DurationAFirstInngs.ToString();
                            txtDuration1secondinngs.Text = objscore.DurationASecondInngs.ToString();
                            txtbyes1firstinngs.Text = objscore.ByesAFirstInngs.ToString();
                            txtbyes1secondinngs.Text = objscore.ByesASecondInngs.ToString();
                            txtlegbyes1firstinngs.Text = objscore.LegByesAFistInngs.ToString();
                            txtlegbyes1secondinngs.Text = objscore.LegByesASecondInngs.ToString();
                            txtpenalty1firstinngs.Text = objscore.PenaltyAFirstInngs.ToString();
                            txtpenalty1secondinngs.Text = objscore.PenaltyASecondInngs.ToString();
                            txttotalextras1firstinngs.Text = objscore.TotalExtrasAFirstInngs.ToString();
                            txttotalextras1secondinngs.Text = objscore.TotalExtrasASecondInngs.ToString();
                            txtruns1firstinngs.Text = objscore.TotalRunsAFirstInngs.ToString();
                            txtruns1secondinngs.Text = objscore.TotalRunsASecondInngs.ToString();
                            txtwickets1firstinngs.Text = objscore.TotalWicketsAFirstInngs.ToString();
                            txtwickets1secondinngs.Text = objscore.TotalWicketsASecondInngs.ToString();
                            txtovers1firstinngs.Text = Math.Round(objscore.TotalOversAFirstInngs, 1).ToString();
                            txtovers1secondinngs.Text = Math.Round(objscore.TotalOversASecondInngs, 1).ToString();
                            txtrunout1firstinnings.Text = objscore.RunOutAFirst.ToString();
                            txtrunout1seccondinnings.Text = objscore.RunOutASecond.ToString();

                            lblseason.Content = season_zone_div.name[0].ToString();
                            lblzone.Content = season_zone_div.name[1].ToString();
                            lbldivision.Content = selectedgridvalue.LoadDivision;

                            txtrunout2firstinngs.Text = objscore.RunOutBFirst.ToString();
                            txtrunout2secondinngs.Text = objscore.RunOutBSecond.ToString();

                            start_time2firstinngs.Text = (objscore.CommenceTimeBFirstInngs);
                            close_time2firstinngs.Text = (objscore.EndTimeBFirstInngs);
                            txtDuration2firstinngs.Text = objscore.DurationBFirstInngs.ToString();
                            txtbyes2firstinngs.Text = objscore.ByesBFirstInngs.ToString();
                            txtlegbyes2firstinngs.Text = objscore.LegByesBFistInngs.ToString();
                            txtpenalty2firstinngs.Text = objscore.PenaltyBFirstInngs.ToString();
                            txttotalextras2firstinngs.Text = objscore.TotalExtrasBFirstInngs.ToString();
                            txtruns2firstinngs.Text = objscore.TotalRunsBFirstInngs.ToString();
                            txtwickets2firstinngs.Text = objscore.TotalWicketsBFirstInngs.ToString();
                            txtovers2firstinngs.Text = Math.Round(objscore.TotalOversBFirstInngs, 1).ToString();

                            //Team2SecondInnings
                            start_time2secondinngs.Text = (objscore.CommenceTimeBSecondInngs);
                            close_time2secondinngs.Text = (objscore.EndTimeBSecondInngs);
                            txtDuration2secondinngs.Text = objscore.DurationBSecondInngs.ToString();
                            txtbyes2secondinngs.Text = objscore.ByesBSecondInngs.ToString();
                            txtlegbyes2secondinngs.Text = objscore.LegByesBSecondInngs.ToString();
                            txtpenalty2secondinngs.Text = objscore.PenaltyBSecondInngs.ToString();
                            txttotalextras2secondinngs.Text = objscore.TotalExtrasBSecondInngs.ToString();
                            txtruns2secondinngs.Text = objscore.TotalRunsBSecondInngs.ToString();
                            txtwickets2secondinngs.Text = objscore.TotalWicketsBSecondInngs.ToString();
                            txtovers2secondinngs.Text = Math.Round(objscore.TotalOversBSecondInngs, 1).ToString();

                            //ReadOnly
                            start_time1firstinngs.IsEnabled = false;
                            start_time2firstinngs.IsEnabled = false;
                            close_time1firstinngs.IsEnabled = false;
                            close_time2firstinngs.IsEnabled = false;
                            txtDuration1firstinngs.IsEnabled = false;
                            txtDuration1secondinngs.IsEnabled = false;
                            txtbyes1firstinngs.IsEnabled = false;
                            txtbyes1secondinngs.IsEnabled = false;
                            txtlegbyes1firstinngs.IsEnabled = false;
                            txtlegbyes1secondinngs.IsEnabled = false;
                            txtpenalty1firstinngs.IsEnabled = false;
                            txtpenalty1secondinngs.IsEnabled = false;
                            txttotalextras1firstinngs.IsEnabled = false;
                            txttotalextras1secondinngs.IsEnabled = false;
                            txtruns1firstinngs.IsEnabled = false;
                            txtruns1secondinngs.IsEnabled = false;
                            txtwickets1firstinngs.IsEnabled = false;
                            txtwickets1secondinngs.IsEnabled = false;
                            txtovers1firstinngs.IsEnabled = false;
                            txtovers1secondinngs.IsEnabled = false;
                            close_time1secondinngs.IsEnabled = false;
                            //Team2FirstInnings
                            start_time1firstinngs.IsEnabled = false;
                            close_time2firstinngs.IsEnabled = false;
                            txtDuration2firstinngs.IsEnabled = false;
                            txtbyes2firstinngs.IsEnabled = false;
                            txtlegbyes2firstinngs.IsEnabled = false;
                            txtpenalty2firstinngs.IsEnabled = false;
                            txttotalextras2firstinngs.IsEnabled = false;
                            txtruns2firstinngs.IsEnabled = false;
                            txtwickets2firstinngs.IsEnabled = false;
                            txtovers2firstinngs.IsEnabled = false;
                            txttotalextras2firstinngs.IsEnabled = false;
                            //Team2SecondInnings
                            start_time1secondinngs.IsEnabled = false;
                            close_time2secondinngs.IsEnabled = false;
                            txtDuration2secondinngs.IsEnabled = false;
                            txtbyes2secondinngs.IsEnabled = false;
                            txtlegbyes2secondinngs.IsEnabled = false;
                            txtpenalty2secondinngs.IsEnabled = false;
                            txttotalextras2secondinngs.IsEnabled = false;
                            txtrunout1firstinnings.IsEnabled = false;
                            txtrunout1seccondinnings.IsEnabled = false;
                            txtrunout2firstinngs.IsEnabled = false;
                            txtrunout2secondinngs.IsEnabled = false;

                            txtruns2secondinngs.IsEnabled = false;
                            txtwickets2secondinngs.IsEnabled = false;
                            txtovers2secondinngs.IsEnabled = false;
                            btnsave.IsEnabled = false;
                            Resultsecondinngs.IsEnabled = false;
                            dgvbat1firstinngs.IsReadOnly = true;
                            dgvbat1secondinngs.IsReadOnly = true;
                            dgvbowl1firstinngs.IsReadOnly = true;
                            dgvbowl1secondinngs.IsReadOnly = true;
                            dgvbat2firstinngs.IsReadOnly = true;
                            dgvbat2secondinngs.IsReadOnly = true;
                            dgvbowl2firstinngs.IsReadOnly = true;
                            dgvbowl2secondinngs.IsReadOnly = true;
                            btn1scorefirstinngs.IsEnabled = false;
                            btn1scoresecondinngs.IsEnabled = false;
                            btn2scorefirstinngs.IsEnabled = false;
                            btn2scoresecondinngs.IsEnabled = false;
                            start_time2secondinngs.IsEnabled = false;

                        }
                        //1
                        dgvbat1firstinngs.ItemsSource = null;
                        dgvbat1firstinngs.DataContext = null;
                        dgvbat1firstinngs.DataContext = dtbat1First;
                        dgvbat1firstinngs.ItemsSource = dtbat1First.DefaultView;

                        //2
                        dgvbowl1firstinngs.ItemsSource = null;
                        dgvbowl1firstinngs.DataContext = null;
                        dgvbowl1firstinngs.DataContext = dtbowl1First;
                        dgvbowl1firstinngs.ItemsSource = dtbowl1First.DefaultView;


                        //3
                        dgvbat1secondinngs.ItemsSource = null;
                        dgvbat1secondinngs.DataContext = null;
                        dgvbat1secondinngs.DataContext = dtbat1Second;
                        dgvbat1secondinngs.ItemsSource = dtbat1Second.DefaultView;

                        //4
                        dgvbowl1secondinngs.ItemsSource = null;
                        dgvbowl1secondinngs.DataContext = null;
                        dgvbowl1secondinngs.DataContext = dtbowl1Second;
                        dgvbowl1secondinngs.ItemsSource = dtbowl1Second.DefaultView;

                        //5
                        dgvbat2firstinngs.ItemsSource = null;
                        dgvbat2firstinngs.DataContext = null;
                        dgvbat2firstinngs.DataContext = dtbat2First;
                        dgvbat2firstinngs.ItemsSource = dtbat2First.DefaultView;

                        //6
                        dgvbowl2firstinngs.ItemsSource = null;
                        dgvbowl2firstinngs.DataContext = null;
                        dgvbowl2firstinngs.DataContext = dtbowl2First;
                        dgvbowl2firstinngs.ItemsSource = dtbowl2First.DefaultView;

                        //7
                        dgvbat2secondinngs.ItemsSource = null;
                        dgvbat2secondinngs.DataContext = null;
                        dgvbat2secondinngs.DataContext = dtbat2Second;
                        dgvbat2secondinngs.ItemsSource = dtbat2Second.DefaultView;



                        //8
                        dgvbowl2secondinngs.ItemsSource = null;
                        dgvbowl2secondinngs.DataContext = null;
                        dgvbowl2secondinngs.DataContext = dtbowl2Second;
                        dgvbowl2secondinngs.ItemsSource = dtbowl2Second.DefaultView;




                    }
                }
                lstMatchDetailsSort.Clear();
                lstMatchDetails.Clear();

                lstscorecard.Clear();
                lstscorecardfilter.Clear();
            }

        }


        private void btnBack_Click_1(object sender, RoutedEventArgs e)
        {

            teamA.Clear();
            teamB.Clear();
            KSCAUIDA.Clear();
            KSCAUIDB.Clear();

            //1
            dgvbat1firstinngs.ItemsSource = null;
            //2
            dgvbowl1firstinngs.ItemsSource = null;
            //3
            dgvbat1secondinngs.ItemsSource = null;
            //4
            dgvbowl1secondinngs.ItemsSource = null;
            //5
            dgvbat2firstinngs.ItemsSource = null;
            //6
            dgvbowl2firstinngs.ItemsSource = null;
            //7
            dgvbat2secondinngs.ItemsSource = null;
            //8
            dgvbowl2secondinngs.ItemsSource = null;

            //clear
            //Team1FirstAndSecoond Innings clear
            start_time1firstinngs.Text = "";
            start_time2firstinngs.Text = "";
            close_time1firstinngs.Text = "";
            close_time2firstinngs.Text = "";
            txtDuration1firstinngs.Text = string.Empty;
            txtDuration1secondinngs.Text = string.Empty;
            txtbyes1firstinngs.Text = string.Empty;
            txtbyes1secondinngs.Text = string.Empty;
            txtlegbyes1firstinngs.Text = string.Empty;
            txtlegbyes1secondinngs.Text = string.Empty;
            txtpenalty1secondinngs.Text = string.Empty;
            txttotalextras1firstinngs.Text = string.Empty;
            txttotalextras1secondinngs.Text = string.Empty;
            txtruns1firstinngs.Text = string.Empty;
            txtruns1secondinngs.Text = string.Empty;
            txtwickets1firstinngs.Text = string.Empty;
            txtwickets1secondinngs.Text = string.Empty;
            txtovers1firstinngs.Text = string.Empty;
            txtovers1secondinngs.Text = string.Empty;

            //Team2FirstInnings
            start_time2firstinngs.Text = "";
            close_time2firstinngs.Text = "";
            txtDuration2firstinngs.Text = string.Empty;
            txtbyes2firstinngs.Text = string.Empty;
            txtlegbyes2firstinngs.Text = string.Empty;
            txtpenalty2firstinngs.Text = string.Empty;
            txttotalextras2firstinngs.Text = string.Empty;
            txtruns2firstinngs.Text = string.Empty;
            txtwickets2firstinngs.Text = string.Empty;
            txtovers2firstinngs.Text = string.Empty;

            //Team2SecondInnings
            start_time2secondinngs.Text = "";
            close_time2secondinngs.Text = "";
            txtDuration2secondinngs.Text = string.Empty;
            txtbyes2secondinngs.Text = string.Empty;
            txtlegbyes2secondinngs.Text = string.Empty;
            txtpenalty2secondinngs.Text = string.Empty;
            txttotalextras2secondinngs.Text = string.Empty;
            txtruns2secondinngs.Text = string.Empty;
            txtwickets2secondinngs.Text = string.Empty;
            txtovers2secondinngs.Text = string.Empty;


            lblbat1firstinngs.Content = null;
            lblbat2firstinngs.Content = null;
            lblbowl1firstinngs.Content = null;
            lblbowl2firstinngs.Content = null;

            lblbat1secondinngs.Content = null;
            lblbat2secondinngs.Content = null;
            lblbowl1secondinngs.Content = null;
            lblbowl2secondinngs.Content = null;



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

            Button btn = (Button)sender;
            btn.Command = NavigationCommands.GoToPage;
            btn.CommandParameter = "/View/SelectPlayers.xaml";
        }

        private void dgvbat1firstinngs_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.Column.Header.ToString() == "Dismissal" || e.Column.Header.ToString() == "Runs" || e.Column.Header.ToString() == "Mins" || e.Column.Header.ToString() == "Balls" || e.Column.Header.ToString() == "Fours" || e.Column.Header.ToString() == "Sixes")
            {

                if (!ismanualeditcommit)
                {
                    ismanualeditcommit = true;

                    DataGrid dggrid = (DataGrid)sender;
                    dggrid.CommitEdit(DataGridEditingUnit.Row, true);

                    drv = (DataRowView)e.Row.Item;
                    ismanualeditcommit = false;
                }
            }
        }

        private void dgvbat1firstinngs_CurrentCellChanged(object sender, EventArgs e)
        {
            if (drv != null)
            {
                drv.EndEdit();

                Regex nameEx = new Regex(@"^[A-Za-z ]+$");
                Regex numex = new Regex(@"^[0-9]+$");

                if (drv["Dismissal"].ToString() != "")
                {

                    if (nameEx.Match(drv["Dismissal"].ToString()).Success)
                    {

                    }
                    else if (!nameEx.Match(drv["Dismissal"].ToString()).Success)
                    {
                        drv["Dismissal"] = "";
                        MessageBox.Show("Invalid Dismissal Format");


                    }
                }

                if (drv["Runs"].ToString() != "")
                {

                    if (numex.Match(drv["Runs"].ToString()).Success)
                    {

                    }
                    else if (!numex.Match(drv["Runs"].ToString()).Success)
                    {
                        drv["Runs"] = "";
                        MessageBox.Show("Invalid Runs Forma");
                    }
                }
                if (drv["Mins"].ToString() != "")
                {
                    if (numex.Match(drv["Mins"].ToString()).Success)
                    {

                    }
                    else if (!numex.Match(drv["Mins"].ToString()).Success)
                    {
                        drv["Mins"] = "";
                        MessageBox.Show("Invalid Minutes Forma");
                    }
                }
                if (drv["Fours"].ToString() != "")
                {
                    if (numex.Match(drv["Fours"].ToString()).Success)
                    {

                    }
                    else if (!numex.Match(drv["Fours"].ToString()).Success)
                    {
                        drv["Fours"] = "";
                        MessageBox.Show("Invalid 4's Forma");
                    }
                }
                if (drv["Sixes"].ToString() != "")
                {
                    if (numex.Match(drv["Sixes"].ToString()).Success)
                    {

                    }
                    else if (!numex.Match(drv["Sixes"].ToString()).Success)
                    {
                        drv["Sixes"] = "";
                        MessageBox.Show("Invalid 6's Forma");
                    }
                }
                if (drv["Balls"].ToString() != "")
                {
                    if (numex.Match(drv["Balls"].ToString()).Success)
                    {

                    }
                    else if (!numex.Match(drv["Balls"].ToString()).Success)
                    {
                        drv["Balls"] = "";
                        MessageBox.Show("Invalid Ball's Faced Format");
                    }
                }
                drv.EndEdit();
                drv = null;
            }
        }

        private void dgvbowl1firstinngs_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.Column.Header.ToString() == "Overs" || e.Column.Header.ToString() == "Maidens" || e.Column.Header.ToString() == "Runs" || e.Column.Header.ToString() == "Wickets" || e.Column.Header.ToString() == "No Ball" || e.Column.Header.ToString() == "Wide" || e.Column.Header.ToString() == "Avg")
            {

                if (!ismanualeditcommit)
                {
                    ismanualeditcommit = true;

                    DataGrid dggrid = (DataGrid)sender;
                    dggrid.CommitEdit(DataGridEditingUnit.Row, true);

                    drv = (DataRowView)e.Row.Item;
                    ismanualeditcommit = false;
                }
            }
        }

        private void dgvbowl1firstinngs_CurrentCellChanged(object sender, EventArgs e)
        {
            if (drv != null)
            {
                drv.EndEdit();
                Regex numex = new Regex(@"^[0-9]+$");
                Regex numexOver = new Regex(@"^[0-9]+(\.[0-9]+)?$");

                if (drv["Overs"].ToString() != "")
                {

                    if (numex.Match(drv["Overs"].ToString()).Success)
                    {

                    }
                    else if (!numexOver.Match(drv["Overs"].ToString()).Success)
                    {
                        drv["Overs"] = "";
                        MessageBox.Show("Invalid Overs Forma");
                    }
                }
                if (drv["Maidens"].ToString() != "")
                {
                    if (numex.Match(drv["Maidens"].ToString()).Success)
                    {

                    }
                    else if (!numex.Match(drv["Maidens"].ToString()).Success)
                    {
                        drv["Maidens"] = "";
                        MessageBox.Show("Invalid Maidens Forma");
                    }
                }
                if (drv["Runs"].ToString() != "")
                {
                    if (numex.Match(drv["Runs"].ToString()).Success)
                    {

                    }
                    else if (!numex.Match(drv["Runs"].ToString()).Success)
                    {
                        drv["Runs"] = "";
                        MessageBox.Show("Invalid Runs Forma");
                    }
                }
                if (drv["Wickets"].ToString() != "")
                {
                    if (numex.Match(drv["Wickets"].ToString()).Success)
                    {

                    }
                    else if (!numex.Match(drv["Wickets"].ToString()).Success)
                    {
                        drv["Wickets"] = "";
                        MessageBox.Show("Invalid Wickets Format");
                    }
                }
                if (drv["No Ball"].ToString() != "")
                {
                    if (numex.Match(drv["No Ball"].ToString()).Success)
                    {

                    }
                    else if (!numex.Match(drv["No Ball"].ToString()).Success)
                    {
                        drv["No Ball"] = "";
                        MessageBox.Show("Invalid No Ball's Forma");
                    }
                }

                if (drv["Wide"].ToString() != "")
                {
                    if (numex.Match(drv["Wide"].ToString()).Success)
                    {

                    }
                    else if (!numex.Match(drv["Wide"].ToString()).Success)
                    {
                        drv["Wide"] = "";
                        MessageBox.Show("Invalid Wide Forma");
                    }
                }
                if (drv["Avg"].ToString() != "")
                {
                    if (numex.Match(drv["Avg"].ToString()).Success)
                    {

                    }
                    else if (!numex.Match(drv["Avg"].ToString()).Success)
                    {
                        drv["Avg"] = "";
                        MessageBox.Show("Invalid Average Forma");
                    }
                }
                drv.EndEdit();
                drv = null;
            }
        }

        private void dgvbat1secondinngs_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.Column.Header.ToString() == "Dismissal" || e.Column.Header.ToString() == "Runs" || e.Column.Header.ToString() == "Mins" || e.Column.Header.ToString() == "Balls" || e.Column.Header.ToString() == "Fours" || e.Column.Header.ToString() == "Sixes")
            {

                if (!ismanualeditcommit)
                {
                    ismanualeditcommit = true;

                    DataGrid dggrid = (DataGrid)sender;
                    dggrid.CommitEdit(DataGridEditingUnit.Row, true);

                    drv = (DataRowView)e.Row.Item;
                    ismanualeditcommit = false;
                }
            }
        }

        private void dgvbat1secondinngs_CurrentCellChanged(object sender, EventArgs e)
        {

            if (drv != null)
            {
                drv.EndEdit();

                Regex nameEx = new Regex(@"^[A-Za-z ]+$");
                Regex numex = new Regex(@"^[0-9]+$");

                if (drv["Dismissal"].ToString() != "")
                {

                    if (nameEx.Match(drv["Dismissal"].ToString()).Success)
                    {

                    }
                    else if (!nameEx.Match(drv["Dismissal"].ToString()).Success)
                    {
                        drv["Dismissal"] = "";
                        MessageBox.Show("Invalid Dismissal Forma");


                    }
                }

                if (drv["Runs"].ToString() != "")
                {

                    if (numex.Match(drv["Runs"].ToString()).Success)
                    {

                    }
                    else if (!numex.Match(drv["Runs"].ToString()).Success)
                    {
                        drv["Runs"] = "";
                        MessageBox.Show("Invalid Run's Forma");
                    }
                }
                if (drv["Mins"].ToString() != "")
                {
                    if (numex.Match(drv["Mins"].ToString()).Success)
                    {

                    }
                    else if (!numex.Match(drv["Mins"].ToString()).Success)
                    {
                        drv["Mins"] = "";
                        MessageBox.Show("Invalid Minutes Forma");
                    }
                }
                if (drv["Fours"].ToString() != "")
                {
                    if (numex.Match(drv["Fours"].ToString()).Success)
                    {

                    }
                    else if (!numex.Match(drv["Fours"].ToString()).Success)
                    {
                        drv["Fours"] = "";
                        MessageBox.Show("Invalid 4's Forma");
                    }
                }
                if (drv["Sixes"].ToString() != "")
                {
                    if (numex.Match(drv["Sixes"].ToString()).Success)
                    {

                    }
                    else if (!numex.Match(drv["Sixes"].ToString()).Success)
                    {
                        drv["Sixes"] = "";
                        MessageBox.Show("Invalid 6's Forma");
                    }
                }
                if (drv["Balls"].ToString() != "")
                {
                    if (numex.Match(drv["Balls"].ToString()).Success)
                    {

                    }
                    else if (!numex.Match(drv["Balls"].ToString()).Success)
                    {
                        drv["Balls"] = "";
                        MessageBox.Show("Invalid Ball's Faced Forma");
                    }
                }
                drv.EndEdit();
                drv = null;
            }
        }

        private void dgvbowl1secondinngs_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.Column.Header.ToString() == "Overs" || e.Column.Header.ToString() == "Maidens" || e.Column.Header.ToString() == "Runs" || e.Column.Header.ToString() == "Wickets" || e.Column.Header.ToString() == "No Ball" || e.Column.Header.ToString() == "Wide" || e.Column.Header.ToString() == "Avg")
            {

                if (!ismanualeditcommit)
                {
                    ismanualeditcommit = true;

                    DataGrid dggrid = (DataGrid)sender;
                    dggrid.CommitEdit(DataGridEditingUnit.Row, true);

                    drv = (DataRowView)e.Row.Item;
                    ismanualeditcommit = false;
                }
            }
        }

        private void dgvbowl1secondinngs_CurrentCellChanged(object sender, EventArgs e)
        {
            if (drv != null)
            {
                drv.EndEdit();
                Regex numex = new Regex(@"^[0-9]+$");
                Regex numexOver = new Regex(@"^[0-9]+(\.[0-9]+)?$");

                if (drv["Overs"].ToString() != "")
                {

                    if (numexOver.Match(drv["Overs"].ToString()).Success)
                    {

                    }
                    else if (!numexOver.Match(drv["Overs"].ToString()).Success)
                    {
                        drv["Overs"] = "";
                        MessageBox.Show("Invalid Overs Format");
                    }
                }
                if (drv["Maidens"].ToString() != "")
                {
                    if (numex.Match(drv["Maidens"].ToString()).Success)
                    {

                    }
                    else if (!numex.Match(drv["Maidens"].ToString()).Success)
                    {
                        drv["Maidens"] = "";
                        MessageBox.Show("Invalid Maidens Format");
                    }
                }
                if (drv["Runs"].ToString() != "")
                {
                    if (numex.Match(drv["Runs"].ToString()).Success)
                    {

                    }
                    else if (!numex.Match(drv["Runs"].ToString()).Success)
                    {
                        drv["Runs"] = "";
                        MessageBox.Show("Invalid Run's Format");
                    }
                }
                if (drv["Wickets"].ToString() != "")
                {
                    if (numex.Match(drv["Wickets"].ToString()).Success)
                    {

                    }
                    else if (!numex.Match(drv["Wickets"].ToString()).Success)
                    {
                        drv["Wickets"] = "";
                        MessageBox.Show("Numbers only");
                    }
                }
                if (drv["No Ball"].ToString() != "")
                {
                    if (numex.Match(drv["No Ball"].ToString()).Success)
                    {

                    }
                    else if (!numex.Match(drv["No Ball"].ToString()).Success)
                    {
                        drv["No Ball"] = "";
                        MessageBox.Show("Invalid No Ball's Format");
                    }
                }

                if (drv["Wide"].ToString() != "")
                {
                    if (numex.Match(drv["Wide"].ToString()).Success)
                    {

                    }
                    else if (!numex.Match(drv["Wide"].ToString()).Success)
                    {
                        drv["Wide"] = "";
                        MessageBox.Show("Invalid Wide's Format");
                    }
                }
                if (drv["Avg"].ToString() != "")
                {
                    if (numex.Match(drv["Avg"].ToString()).Success)
                    {

                    }
                    else if (!numex.Match(drv["Avg"].ToString()).Success)
                    {
                        drv["Avg"] = "";
                        MessageBox.Show("Invalid Average Format");
                    }
                }
                drv.EndEdit();
                drv = null;
            }
        }

        private void dgvbat2firstinngs_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.Column.Header.ToString() == "Dismissal" || e.Column.Header.ToString() == "Runs" || e.Column.Header.ToString() == "Mins" || e.Column.Header.ToString() == "Balls" || e.Column.Header.ToString() == "Fours" || e.Column.Header.ToString() == "Sixes")
            {

                if (!ismanualeditcommit)
                {
                    ismanualeditcommit = true;

                    DataGrid dggrid = (DataGrid)sender;
                    dggrid.CommitEdit(DataGridEditingUnit.Row, true);

                    drv = (DataRowView)e.Row.Item;
                    ismanualeditcommit = false;
                }
            }
        }

        private void dgvbat2firstinngs_CurrentCellChanged(object sender, EventArgs e)
        {
            if (drv != null)
            {
                drv.EndEdit();

                Regex nameEx = new Regex(@"^[A-Za-z ]+$");
                Regex numex = new Regex(@"^[0-9]+$");

                if (drv["Dismissal"].ToString() != "")
                {

                    if (nameEx.Match(drv["Dismissal"].ToString()).Success)
                    {

                    }
                    else if (!nameEx.Match(drv["Dismissal"].ToString()).Success)
                    {
                        drv["Dismissal"] = "";
                        MessageBox.Show("Invalid Dismissal Format");


                    }
                }

                if (drv["Runs"].ToString() != "")
                {

                    if (numex.Match(drv["Runs"].ToString()).Success)
                    {

                    }
                    else if (!numex.Match(drv["Runs"].ToString()).Success)
                    {
                        drv["Runs"] = "";
                        MessageBox.Show("Invalid Run's Format");
                    }
                }
                if (drv["Mins"].ToString() != "")
                {
                    if (numex.Match(drv["Mins"].ToString()).Success)
                    {

                    }
                    else if (!numex.Match(drv["Mins"].ToString()).Success)
                    {
                        drv["Mins"] = "";
                        MessageBox.Show("Invalid Minute's Format");
                    }
                }
                if (drv["Fours"].ToString() != "")
                {
                    if (numex.Match(drv["Fours"].ToString()).Success)
                    {

                    }
                    else if (!numex.Match(drv["Fours"].ToString()).Success)
                    {
                        drv["Fours"] = "";
                        MessageBox.Show("Invalid 4's Format");
                    }
                }
                if (drv["Sixes"].ToString() != "")
                {
                    if (numex.Match(drv["Sixes"].ToString()).Success)
                    {

                    }
                    else if (!numex.Match(drv["Sixes"].ToString()).Success)
                    {
                        drv["Sixes"] = "";
                        MessageBox.Show("Invalid 6's Format");
                    }
                }
                if (drv["Balls"].ToString() != "")
                {
                    if (numex.Match(drv["Balls"].ToString()).Success)
                    {

                    }
                    else if (!numex.Match(drv["Balls"].ToString()).Success)
                    {
                        drv["Balls"] = "";
                        MessageBox.Show("Invalid Ball's Faced Format");
                    }
                }
                drv.EndEdit();
                drv = null;
            }
        }

        private void dgvbowl2firstinngs_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.Column.Header.ToString() == "Overs" || e.Column.Header.ToString() == "Maidens" || e.Column.Header.ToString() == "Runs" || e.Column.Header.ToString() == "Wickets" || e.Column.Header.ToString() == "No Ball" || e.Column.Header.ToString() == "Wide" || e.Column.Header.ToString() == "Avg")
            {

                if (!ismanualeditcommit)
                {
                    ismanualeditcommit = true;

                    DataGrid dggrid = (DataGrid)sender;
                    dggrid.CommitEdit(DataGridEditingUnit.Row, true);

                    drv = (DataRowView)e.Row.Item;
                    ismanualeditcommit = false;
                }
            }
        }

        private void dgvbowl2firstinngs_CurrentCellChanged(object sender, EventArgs e)
        {
            if (drv != null)
            {
                drv.EndEdit();
                Regex numex = new Regex(@"^[0-9]+$");
                Regex numexOver = new Regex(@"^[0-9]+(\.[0-9]+)?$");

                if (drv["Overs"].ToString() != "")
                {

                    if (numexOver.Match(drv["Overs"].ToString()).Success)
                    {

                    }
                    else if (!numexOver.Match(drv["Overs"].ToString()).Success)
                    {
                        drv["Overs"] = "";
                        MessageBox.Show("Invalid Overs Format");
                    }
                }
                if (drv["Maidens"].ToString() != "")
                {
                    if (numex.Match(drv["Maidens"].ToString()).Success)
                    {

                    }
                    else if (!numex.Match(drv["Maidens"].ToString()).Success)
                    {
                        drv["Maidens"] = "";
                        MessageBox.Show("Invalid Maidens Format");
                    }
                }
                if (drv["Runs"].ToString() != "")
                {
                    if (numex.Match(drv["Runs"].ToString()).Success)
                    {

                    }
                    else if (!numex.Match(drv["Runs"].ToString()).Success)
                    {
                        drv["Runs"] = "";
                        MessageBox.Show("Invalid Runs Format");
                    }
                }
                if (drv["Wickets"].ToString() != "")
                {
                    if (numex.Match(drv["Wickets"].ToString()).Success)
                    {

                    }
                    else if (!numex.Match(drv["Wickets"].ToString()).Success)
                    {
                        drv["Wickets"] = "";
                        MessageBox.Show("Invalid Wicket's Format");
                    }
                }
                if (drv["No Ball"].ToString() != "")
                {
                    if (numex.Match(drv["No Ball"].ToString()).Success)
                    {

                    }
                    else if (!numex.Match(drv["No Ball"].ToString()).Success)
                    {
                        drv["No Ball"] = "";
                        MessageBox.Show("Invalid No Ball's Format");
                    }
                }

                if (drv["Wide"].ToString() != "")
                {
                    if (numex.Match(drv["Wide"].ToString()).Success)
                    {

                    }
                    else if (!numex.Match(drv["Wide"].ToString()).Success)
                    {
                        drv["Wide"] = "";
                        MessageBox.Show("Invalid Wide's Format");
                    }
                }
                if (drv["Avg"].ToString() != "")
                {
                    if (numex.Match(drv["Avg"].ToString()).Success)
                    {

                    }
                    else if (!numex.Match(drv["Avg"].ToString()).Success)
                    {
                        drv["Avg"] = "";
                        MessageBox.Show("Invalid Average's Format");
                    }
                }
                drv.EndEdit();
                drv = null;
            }
        }

        private void dgvbowl2secondinngs_CurrentCellChanged(object sender, EventArgs e)
        {
            if (drv != null)
            {
                drv.EndEdit();
                Regex numex = new Regex(@"^[0-9]+$");
                Regex numexOver = new Regex(@"^[0-9]+(\.[0-9]+)?$");


                if (drv["Overs"].ToString() != "")
                {

                    if (numexOver.Match(drv["Overs"].ToString()).Success)
                    {

                    }
                    else if (!numexOver.Match(drv["Overs"].ToString()).Success)
                    {
                        drv["Overs"] = "";
                        MessageBox.Show("Invalid Overs's Format");
                    }
                }
                if (drv["Maidens"].ToString() != "")
                {
                    if (numex.Match(drv["Maidens"].ToString()).Success)
                    {

                    }
                    else if (!numex.Match(drv["Maidens"].ToString()).Success)
                    {
                        drv["Maidens"] = "";
                        MessageBox.Show("Invalid Maiden's Format");
                    }
                }
                if (drv["Runs"].ToString() != "")
                {
                    if (numex.Match(drv["Runs"].ToString()).Success)
                    {

                    }
                    else if (!numex.Match(drv["Runs"].ToString()).Success)
                    {
                        drv["Runs"] = "";
                        MessageBox.Show("Invalid Run's Format");
                    }
                }
                if (drv["Wickets"].ToString() != "")
                {
                    if (numex.Match(drv["Wickets"].ToString()).Success)
                    {

                    }
                    else if (!numex.Match(drv["Wickets"].ToString()).Success)
                    {
                        drv["Wickets"] = "";
                        MessageBox.Show("Invalid Wicket's Format");
                    }
                }
                if (drv["No Ball"].ToString() != "")
                {
                    if (numex.Match(drv["No Ball"].ToString()).Success)
                    {

                    }
                    else if (!numex.Match(drv["No Ball"].ToString()).Success)
                    {
                        drv["No Ball"] = "";
                        MessageBox.Show("Invalid No Ball's Format");
                    }
                }

                if (drv["Wide"].ToString() != "")
                {
                    if (numex.Match(drv["Wide"].ToString()).Success)
                    {

                    }
                    else if (!numex.Match(drv["Wide"].ToString()).Success)
                    {
                        drv["Wide"] = "";
                        MessageBox.Show("Invalid Wide's Format");
                    }
                }
                if (drv["Avg"].ToString() != "")
                {
                    if (numex.Match(drv["Avg"].ToString()).Success)
                    {

                    }
                    else if (!numex.Match(drv["Avg"].ToString()).Success)
                    {
                        drv["Avg"] = "";
                        MessageBox.Show("Invalid Average's Format");
                    }
                }
                drv.EndEdit();
                drv = null;
            }
        }

        private void dgvbowl2secondinngs_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.Column.Header.ToString() == "Overs" || e.Column.Header.ToString() == "Maidens" || e.Column.Header.ToString() == "Runs" || e.Column.Header.ToString() == "Wickets" || e.Column.Header.ToString() == "No Ball" || e.Column.Header.ToString() == "Wide" || e.Column.Header.ToString() == "Avg")
            {

                if (!ismanualeditcommit)
                {
                    ismanualeditcommit = true;

                    DataGrid dggrid = (DataGrid)sender;
                    dggrid.CommitEdit(DataGridEditingUnit.Row, true);

                    drv = (DataRowView)e.Row.Item;
                    ismanualeditcommit = false;
                }
            }
        }

        private void dgvbat2secondinngs_CurrentCellChanged(object sender, EventArgs e)
        {
            if (drv != null)
            {
                drv.EndEdit();

                Regex nameEx = new Regex(@"^[A-Za-z ]+$");
                Regex numex = new Regex(@"^[0-9]+$");

                if (drv["Dismissal"].ToString() != "")
                {

                    if (nameEx.Match(drv["Dismissal"].ToString()).Success)
                    {

                    }
                    else if (!nameEx.Match(drv["Dismissal"].ToString()).Success)
                    {
                        drv["Dismissal"] = "";
                        MessageBox.Show("Invalid Dismissal Format");


                    }
                }

                if (drv["Runs"].ToString() != "")
                {

                    if (numex.Match(drv["Runs"].ToString()).Success)
                    {

                    }
                    else if (!numex.Match(drv["Runs"].ToString()).Success)
                    {
                        drv["Runs"] = "";
                        MessageBox.Show("Invalid Runs Format");
                    }
                }
                if (drv["Mins"].ToString() != "")
                {
                    if (numex.Match(drv["Mins"].ToString()).Success)
                    {

                    }
                    else if (!numex.Match(drv["Mins"].ToString()).Success)
                    {
                        drv["Mins"] = "";
                        MessageBox.Show("Invalid Mins Format");
                    }
                }
                if (drv["Fours"].ToString() != "")
                {
                    if (numex.Match(drv["Fours"].ToString()).Success)
                    {

                    }
                    else if (!numex.Match(drv["Fours"].ToString()).Success)
                    {
                        drv["Fours"] = "";
                        MessageBox.Show("Invalid 4's Format");
                    }
                }
                if (drv["Sixes"].ToString() != "")
                {
                    if (numex.Match(drv["Sixes"].ToString()).Success)
                    {

                    }
                    else if (!numex.Match(drv["Sixes"].ToString()).Success)
                    {
                        drv["Sixes"] = "";
                        MessageBox.Show("Invalid 6's Format");
                    }
                }
                if (drv["Balls"].ToString() != "")
                {
                    if (numex.Match(drv["Balls"].ToString()).Success)
                    {

                    }
                    else if (!numex.Match(drv["Balls"].ToString()).Success)
                    {
                        drv["Balls"] = "";
                        MessageBox.Show("Invalid Ball's Faced Format");
                    }
                }
                drv.EndEdit();
                drv = null;
            }
        }

        private void dgvbat2secondinngs_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.Column.Header.ToString() == "Dismissal" || e.Column.Header.ToString() == "Runs" || e.Column.Header.ToString() == "Mins" || e.Column.Header.ToString() == "Balls" || e.Column.Header.ToString() == "Fours" || e.Column.Header.ToString() == "Sixes")
            {

                if (!ismanualeditcommit)
                {
                    ismanualeditcommit = true;

                    DataGrid dggrid = (DataGrid)sender;
                    dggrid.CommitEdit(DataGridEditingUnit.Row, true);

                    drv = (DataRowView)e.Row.Item;
                    ismanualeditcommit = false;
                }
            }
        }

        private void start_time1firstinngs_LostFocus(object sender, RoutedEventArgs e)
        {
            System.Text.RegularExpressions.Regex rtime = new System.Text.RegularExpressions.Regex(@"[0-2][0-9]\:[0-6][0-9]");


            if (!rtime.IsMatch(start_time1firstinngs.Text))
            {

                MessageBox.Show("Please provide the time in hh:mm format");
                start_time1firstinngs.Text = "";
            }
        }

        private void close_time1firstinngs_LostFocus(object sender, RoutedEventArgs e)
        {
            System.Text.RegularExpressions.Regex rtime = new System.Text.RegularExpressions.Regex(@"[0-2][0-9]\:[0-6][0-9]");


            if (!rtime.IsMatch(close_time1firstinngs.Text))
            {

                MessageBox.Show("Please provide the time in hh:mm format");
                close_time1firstinngs.Text = "";
            }
        }

        private void start_time1secondinngs_LostFocus(object sender, RoutedEventArgs e)
        {
            System.Text.RegularExpressions.Regex rtime = new System.Text.RegularExpressions.Regex(@"[0-2][0-9]\:[0-6][0-9]");


            if (!rtime.IsMatch(start_time1secondinngs.Text))
            {

                MessageBox.Show("Please provide the time in hh:mm format");
                start_time1secondinngs.Text = "";
            }
        }

        private void close_time1secondinngs_LostFocus(object sender, RoutedEventArgs e)
        {
            System.Text.RegularExpressions.Regex rtime = new System.Text.RegularExpressions.Regex(@"[0-2][0-9]\:[0-6][0-9]");


            if (!rtime.IsMatch(close_time1secondinngs.Text))
            {

                MessageBox.Show("Please provide the time in hh:mm format");
                close_time1secondinngs.Text = "";
            }
        }

        private void start_time2firstinngs_LostFocus(object sender, RoutedEventArgs e)
        {
            System.Text.RegularExpressions.Regex rtime = new System.Text.RegularExpressions.Regex(@"[0-2][0-9]\:[0-6][0-9]");


            if (!rtime.IsMatch(start_time2firstinngs.Text))
            {

                MessageBox.Show("Please provide the time in hh:mm format");
                start_time2firstinngs.Text = "";
            }
        }

        private void close_time2firstinngs_LostFocus(object sender, RoutedEventArgs e)
        {
            System.Text.RegularExpressions.Regex rtime = new System.Text.RegularExpressions.Regex(@"[0-2][0-9]\:[0-6][0-9]");


            if (!rtime.IsMatch(close_time2firstinngs.Text))
            {

                MessageBox.Show("Please provide the time in hh:mm format");
                close_time2firstinngs.Text = "";
            }
        }

        private void start_time2secondinngs_LostFocus(object sender, RoutedEventArgs e)
        {
            System.Text.RegularExpressions.Regex rtime = new System.Text.RegularExpressions.Regex(@"[0-2][0-9]\:[0-6][0-9]");


            if (!rtime.IsMatch(start_time2secondinngs.Text))
            {

                MessageBox.Show("Please provide the time in hh:mm format");
                start_time2secondinngs.Text = "";
            }
        }

        private void close_time2secondinngs_LostFocus(object sender, RoutedEventArgs e)
        {
            System.Text.RegularExpressions.Regex rtime = new System.Text.RegularExpressions.Regex(@"[0-2][0-9]\:[0-6][0-9]");


            if (!rtime.IsMatch(close_time2secondinngs.Text))
            {

                MessageBox.Show("Please provide the time in hh:mm format");
                close_time2secondinngs.Text = "";
            }
        }
    }
}
