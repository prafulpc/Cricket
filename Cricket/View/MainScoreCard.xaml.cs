using CricketSol.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
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
using Cricket.BLL;
using System.Collections.ObjectModel;

namespace Cricket.View
{
    /// <summary>
    /// Interaction logic for MainScoreCard.xaml
    /// </summary>
    public partial class MainScoreCard : UserControl
    {
        DataTable dtlItemDetails = new DataTable();
        public List<string> bowlingplayers = new List<string>(15);
        public List<string> battingplayers = new List<string>(15);

        ObservableCollection<string> teamA = new ObservableCollection<string>();    //Batting Team
        ObservableCollection<string> teamB = new ObservableCollection<string>();    //Bowling Team

        double Ball = 0;
        int TotalScore = 0;
        string Runs;
        int extra = 0;

        //batsman
        public List<string> Bowlertookwicket = new List<string>(15);
        public List<int> Runnnn= new List<int>(15);
        public List<int> Ballsssss = new List<int>(15);
        public List<int> Foursssss = new List<int>(15);
        public List<int> Sixesssss = new List<int>(15);


        //bowler
        public List<double> bowlerover = new List<double>(15);
        public List<int> dotssss = new List<int>(15);
        public List<int> BRunnnn = new List<int>(15);
        public List<int> BWickets = new List<int>(15);
        public List<int> Widessss = new List<int>(15);
        public List<int> Noballssssss = new List<int>(15);
        public List<int> bowlerovernumber = new List<int>(15);



        //batsman 1
        string batsman1;
        int Runs1 = 0;
        int Balls1 = 0;
        int Fours1 = 0;
        int Sixes1 = 0;

        string BowlerNameWhoTookWicket;
        string BatsmanNameWhoGotOut;
        //string BowlerName2;

        //batsman2
        string batsman2;
        int Runs2 = 0;
        int Balls2 = 0;
        int Fours2 = 0;
        int Sixes2 = 0;

        //bowler
        string bowler;
        double bowlerballs = 0;
        bool blnSwapBatsman;
        int bowlerwicket;

        //temporary bowler overs
        double bbb;
       
       


        public MainScoreCard()
        {
            InitializeComponent();
            MainScoreCardBLL objmainscorecard = new MainScoreCardBLL();

            for (int s = 0; s <= 14; s++)
            {
                Bowlertookwicket.Insert(s, null);
                Runnnn.Insert(s, 0);
                Ballsssss.Insert(s, 0);
                Foursssss.Insert(s, 0);
                Sixesssss.Insert(s, 0);

                bowlerover.Insert(s, 0);
                dotssss.Insert(s, 0);
                BRunnnn.Insert(s, 0);
                BWickets.Insert(s, 0);
                Widessss.Insert(s, 0);
                Noballssssss.Insert(s, 0);
                bowlerovernumber.Insert(s, 0);
            }

            

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
            }

            else if ((comboboxvalue.value[0] == "0" && comboboxvalue.value[1] == "1") || (comboboxvalue.value[0] == "1" && comboboxvalue.value[1] == "0"))
            {
                foreach (var abc in (TempValuesTeam2.team2))
                {
                    if (abc != null)
                    {
                        teamA.Add(abc.ToString());
                    }
                }

                foreach (var xyz in TempValuesTeam1.team1)
                {
                    if (xyz != null)
                    {
                        teamB.Add(xyz.ToString());
                    }
                }
            }


            dgvbat.DataContext = objmainscorecard.batting(teamA);
            dgvbowl.DataContext = objmainscorecard.bowling(teamB);


            for (int i = 0; i < player.No_Of_Players; i++)
            {
                foreach (var item1 in Batman.names)
                {
                    if (item1 != null)
                    {
                        cbxbatsmen.Items.Add(item1);
                        i++;

                    }
                }
            }

            for (int j = 0; j < Bowler.count; j++)
            {
                foreach (var item in Bowler.names)
                {
                    if (item != null)
                    {
                        cbxbowler.Items.Add(item);
                        j++;
                    }
                }
            }

            for (int i = 0; i < cbxbowler.Items.Count; i++)
            {
                foreach (var item in cbxbowler.Items)
                {
                    string xyz = Convert.ToString(item);
                    bowlingplayers.Insert(i, xyz);
                    i++;
                }
            }

            for (int j = 0; j < cbxbatsmen.Items.Count; j++)
            {
                foreach (var item in cbxbatsmen.Items)
                {
                    string xyz = Convert.ToString(item);
                    battingplayers.Insert(j, xyz);
                    j++;
                }
            }

            cbxbatsmen.SelectedIndex = 0;
            cbxbowler.SelectedIndex = 0;

            
            cbxextras.SelectedIndex = 0;
            lblbatsman1.Content = player.striker;
            lblbatsman2.Content = player.nonstriker;
            lblbowler.Content = player.bowler;
        }

        private void cbxbowler_DropDownOpened()
        {
            cbxbowler.IsDropDownOpen = true;
        }


        
        private void btnzero_Click(object sender, RoutedEventArgs e)
        {
            btnzero.Background = Brushes.Orange;
            btnone.ClearValue(Button.BackgroundProperty);
            btntwo.ClearValue(Button.BackgroundProperty);
            btnthree.ClearValue(Button.BackgroundProperty);
            btnfour.ClearValue(Button.BackgroundProperty);
            btnsix.ClearValue(Button.BackgroundProperty);
        }

        private void btnone_Click(object sender, RoutedEventArgs e)
        {
            btnzero.ClearValue(Button.BackgroundProperty);
            btnone.Background = Brushes.Orange;
            btntwo.ClearValue(Button.BackgroundProperty);
            btnthree.ClearValue(Button.BackgroundProperty);
            btnfour.ClearValue(Button.BackgroundProperty);
            btnsix.ClearValue(Button.BackgroundProperty);
        }
        private void btntwo_Click(object sender, RoutedEventArgs e)
        {
            btntwo.Background = Brushes.Orange;
            btnone.ClearValue(Button.BackgroundProperty);
            btnzero.ClearValue(Button.BackgroundProperty);
            btnthree.ClearValue(Button.BackgroundProperty);
            btnfour.ClearValue(Button.BackgroundProperty);
            btnsix.ClearValue(Button.BackgroundProperty);
        }
        private void btnthree_Click(object sender, RoutedEventArgs e)
        {
            btnthree.Background = Brushes.Orange;
            btnone.ClearValue(Button.BackgroundProperty);
            btntwo.ClearValue(Button.BackgroundProperty);
            btnzero.ClearValue(Button.BackgroundProperty);
            btnfour.ClearValue(Button.BackgroundProperty);
            btnsix.ClearValue(Button.BackgroundProperty);
        }
        private void btnfour_Click(object sender, RoutedEventArgs e)
        {
            btnfour.Background = Brushes.Orange;
            btnone.ClearValue(Button.BackgroundProperty);
            btntwo.ClearValue(Button.BackgroundProperty);
            btnthree.ClearValue(Button.BackgroundProperty);
            btnzero.ClearValue(Button.BackgroundProperty);
            btnsix.ClearValue(Button.BackgroundProperty);
        }
        private void btnsix_Click(object sender, RoutedEventArgs e)
        {
            btnsix.Background = Brushes.Orange;
            btnone.ClearValue(Button.BackgroundProperty);
            btntwo.ClearValue(Button.BackgroundProperty);
            btnthree.ClearValue(Button.BackgroundProperty);
            btnfour.ClearValue(Button.BackgroundProperty);
            btnzero.ClearValue(Button.BackgroundProperty);
        }

        public void btnWicket_Click(object sender, RoutedEventArgs e)
        {
            bowlerwicket = 0;
            bowlerwicket++;

            calc();
            MessageBox.Show("Select Who Got Out");
            cbxWhoOut.Items.Clear();
            cbxWhoOut.Items.Add(batsman1);
            cbxWhoOut.Items.Add(batsman2);
            cbxWhoOut_DropDownOpened();
            
            //cbxWhoOut_DropDownClosed();
        }

        private void cbxWhoOut_DropDownClosed()
        {
            //throw new NotImplementedException();
            
        }

        private void cbxWhoOut_DropDownOpened()
        {
            //throw new NotImplementedException();
            cbxWhoOut.IsDropDownOpen = true;
            
            
        }

        private void cbxbatsmen_DropDownOpened()
        {
//            throw new NotImplementedException();
            cbxbatsmen.IsDropDownOpen = true;

        }

        public void calc()
        {
            if (btnzero.Background == Brushes.Orange)
            {
                CalculateTotalRuns(0);
                swapbatsman(0);
            }
            else if (btnone.Background == Brushes.Orange)
            {
                CalculateTotalRuns(1);
                swapbatsman(1);
            }
            else if (btntwo.Background == Brushes.Orange)
            {
                CalculateTotalRuns(2);
                swapbatsman(2);
            }
            else if (btnthree.Background == Brushes.Orange)
            {
                CalculateTotalRuns(3);
                swapbatsman(3);
            }
            else if (btnfour.Background == Brushes.Orange)
            {
                CalculateTotalRuns(4);
                swapbatsman(4);
            }
            else if (btnsix.Background == Brushes.Orange)
            {
                CalculateTotalRuns(6);
                swapbatsman(6);
            }

            cbxextras.SelectedIndex = 0;
            btnzero.ClearValue(Button.BackgroundProperty);
            btnone.ClearValue(Button.BackgroundProperty);
            btntwo.ClearValue(Button.BackgroundProperty);
            btnthree.ClearValue(Button.BackgroundProperty);
            btnfour.ClearValue(Button.BackgroundProperty);
            btnsix.ClearValue(Button.BackgroundProperty);

            cbxoverthrowruns.SelectedIndex = -1;
        }
        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            calc();
            
        }

        public void CalculateTotalRuns (int i)
        {
            int TotalRuns = 0;
            int BowlerRuns = 0;

            //No-Extra
            if (cbxextras.SelectedIndex==0)
            {
                Ball = Ball + 1;
                bowlerballs = bowlerballs + 1;
                //temp_overs = temp_overs + 1;
                if (chbxOverThrow.IsChecked==true)
                {   
                    if(cbxoverthrowruns.SelectedIndex !=-1)
                    {
                        BowlerRuns = TotalRuns = i + Convert.ToInt16(cbxoverthrowruns.SelectedItem.ToString());
                        //BowlerRuns = i + Convert.ToInt16(cbxoverthrowruns.SelectedItem.ToString());
                        TotalScore = TotalScore + TotalRuns;
                        Runs = TotalRuns.ToString() + " runs.";
                    }
                    else
                    {
                        MessageBox.Show("Select Runs for OverThrough");
                    }

                }
                else
                {
                    Runs = i.ToString() + " runs.";
                    TotalScore = TotalScore + i;
                    BowlerRuns = i;
                }
            }
                //Wide
            else if (cbxextras.SelectedIndex == 1)
            {
                if (chbxOverThrow.IsChecked == true)
                {
                    if (cbxoverthrowruns.SelectedIndex != -1)
                    {
                        BowlerRuns = TotalRuns = i + 1 + Convert.ToInt16(cbxoverthrowruns.SelectedItem.ToString());
                        TotalScore = TotalScore + TotalRuns;
                        Runs = TotalRuns.ToString() + " runs.";
                        extra = extra + i + 1;

                    }
                    else
                    {
                        MessageBox.Show("Select Runs for OverThrough");
                    }
                }
                else
                {
                    Runs = (i+1).ToString() + " runs.";
                    TotalScore = TotalScore + i + 1;
                    extra = extra + i + 1;
                    BowlerRuns = i + 1;
                }
            }

            //No-Ball(Front Foot)
            else if (cbxextras.SelectedIndex == 2)
            {
                if (chbxOverThrow.IsChecked == true)
                {
                    BowlerRuns = TotalRuns = i + 1 + Convert.ToInt16(cbxoverthrowruns.SelectedItem.ToString());
                    TotalScore = TotalScore + TotalRuns;
                    Runs = TotalRuns.ToString() + " runs.";
                    extra = extra + 1;
                }
                else
                {
                    Runs = (i + 1).ToString() + " runs.";
                    TotalScore = TotalScore + i + 1;
                    extra = extra + 1;
                    BowlerRuns = i + 1;
                }
            }

            //No-Ball(Above Waist)
            else if (cbxextras.SelectedIndex == 3)
            {
                if (chbxOverThrow.IsChecked == true)
                {
                    BowlerRuns = TotalRuns = i + 1 + Convert.ToInt16(cbxoverthrowruns.SelectedItem.ToString());
                    TotalScore = TotalScore + TotalRuns;
                    Runs = TotalRuns.ToString() + " runs.";
                    extra = extra + 1;
                }
                else
                {
                    Runs = (i + 1).ToString() + " runs.";
                    TotalScore = TotalScore + i + 1;
                    extra = extra + 1;
                    BowlerRuns = i + 1;
                }
            }

            //Leg-Byes
            else if (cbxextras.SelectedIndex==4)
            {
                Ball = Ball + 1;
                bowlerballs = bowlerballs + 1;
                //temp_overs = temp_overs + 1;
                if (chbxOverThrow.IsChecked == true)
                {
                    BowlerRuns = TotalRuns = i + Convert.ToInt16(cbxoverthrowruns.SelectedItem.ToString());
                    TotalScore = TotalScore + TotalRuns;
                    Runs = TotalRuns.ToString() + " runs.";
                    extra = extra + TotalRuns;
                }
                else
                {
                    Runs = i.ToString() + " runs.";
                    TotalScore = TotalScore + i;
                    extra = extra + i;
                    BowlerRuns = i + 1;
                }
            }
                //Byes
            else if (cbxextras.SelectedIndex==5)
            {
                Ball = Ball + 1;
                bowlerballs = bowlerballs + 1;
                //temp_overs = temp_overs + 1;
                if (chbxOverThrow.IsChecked == true)
                {
                    BowlerRuns = TotalRuns = i + Convert.ToInt16(cbxoverthrowruns.SelectedItem.ToString());
                    TotalScore = TotalScore + TotalRuns;
                    Runs = TotalRuns.ToString() + " runs.";
                    extra = extra + TotalRuns;
                }
                else
                {
                    Runs = i.ToString() + " runs.";
                    TotalScore = TotalScore + i;
                    extra = extra + i;
                    BowlerRuns = i + 1;
                }
            }

            lbltotalscore.Content = TotalScore;         //Total Score
            lblOvers.Content = Convert.ToInt16(Math.Truncate(Ball / 6)) + "." + (Ball % 6);     //Total Overs
            lblExtras.Content = extra;
            


            batsman1 = cbxbatsmen.Items[0].ToString();
            batsman2 = cbxbatsmen.Items[1].ToString();
            bowler = cbxbowler.Items[0].ToString();
               
            ObservableCollection<string> team1 = teamA;
            ObservableCollection<string> team2 = teamB;

            if(cbxbatsmen.SelectedIndex==0)
            {
                DataTable dt1 = new DataTable();

                dt1.Columns.Add("SLNO", Type.GetType("System.String"));
                dt1.Columns.Add("Batsman", Type.GetType("System.String"));
                dt1.Columns.Add("Dismissal", Type.GetType("System.String"));
                dt1.Columns.Add("Bowler", Type.GetType("System.String"));
                dt1.Columns.Add("Runs", Type.GetType("System.String"));
                dt1.Columns.Add("Balls", Type.GetType("System.String"));
                dt1.Columns.Add("Mins", Type.GetType("System.String"));
                dt1.Columns.Add("Fours", Type.GetType("System.String"));
                dt1.Columns.Add("Sixes", Type.GetType("System.String"));
                dt1.Columns.Add("SR", Type.GetType("System.String"));

                int count = 0;
                for (int m = 0; m <= (teamA.Count - 1); m++)
                {
                    if (!DBNull.Value.Equals((dgvbat.Items[m] as DataRowView).Row.ItemArray[1]) && ((dgvbat.Items[m] as DataRowView).Row.ItemArray[1]).ToString() != "")
                    {
                        if (cbxbatsmen.SelectedValue.ToString() == Convert.ToString((dgvbat.Items[m] as DataRowView).Row.ItemArray[1]))
                        {
                            Runs1 = Runs1 + i;
                            Runnnn[m] = Runs1;

                            if (cbxextras.SelectedIndex == 0)
                            {
                                Balls1 = Balls1 + 1;
                            }
                            Ballsssss[m] = Balls1;

                            //4's
                            if(i==4)
                            {
                                Fours1 = Fours1 + 1;
                            }
                            Foursssss[m] = Fours1;

                            //6's
                            if(i==6)
                            {
                                Sixes1 = Sixes1 + 1;
                            }
                            Sixesssss[m] = Sixes1;

                            DataRow dr = dt1.NewRow();
                            dr.BeginEdit();
                            dr["SLNO"] = count+1;
                            dr["Batsman"] = teamA[count];

                            if (teamA[count] == BatsmanNameWhoGotOut)
                            {
                                Bowlertookwicket[m] = BowlerNameWhoTookWicket;
                            }
                            dr["Bowler"] = Bowlertookwicket[m];
                            dr["Runs"] = Runnnn[m];
                            dr["Balls"] = Ballsssss[m];
                            dr["Fours"] = Foursssss[m];
                            dr["Sixes"] = Sixesssss[m];
                            count++;
                            dr.EndEdit();
                            dt1.Rows.Add(dr);
                            dt1.AcceptChanges();
                        }
                        else
                        {
                            DataRow dr = dt1.NewRow();
                            dr.BeginEdit();
                            dr["SLNO"] = count+1;
                            dr["Batsman"] = teamA[count];

                            if (teamA[count] == BatsmanNameWhoGotOut)
                            {
                                Bowlertookwicket[m] = BowlerNameWhoTookWicket;
                            }
                            dr["Bowler"] = Bowlertookwicket[m];

                            if (Runnnn[count] != null)
                            {
                                dr["Runs"] = Runnnn[m];
                                dr["Balls"] = Ballsssss[m];
                                dr["Fours"] = Foursssss[m];
                                dr["Sixes"] = Sixesssss[m];
                            }
                            count++;
                            dr.EndEdit();
                            dt1.Rows.Add(dr);
                            dt1.AcceptChanges();
                        }
                    }
                }
                dgvbat.DataContext = dt1;
            }

            else
            {
                DataTable dt1 = new DataTable();

                dt1.Columns.Add("SLNO", Type.GetType("System.String"));
                dt1.Columns.Add("Batsman", Type.GetType("System.String"));
                dt1.Columns.Add("Dismissal", Type.GetType("System.String"));
                dt1.Columns.Add("Bowler", Type.GetType("System.String"));
                dt1.Columns.Add("Runs", Type.GetType("System.String"));
                dt1.Columns.Add("Balls", Type.GetType("System.String"));
                dt1.Columns.Add("Mins", Type.GetType("System.String"));
                dt1.Columns.Add("Fours", Type.GetType("System.String"));
                dt1.Columns.Add("Sixes", Type.GetType("System.String"));
                dt1.Columns.Add("SR", Type.GetType("System.String"));

                int count = 0;
                for (int m = 0; m <= (teamA.Count - 1); m++)
                {
                    if (!DBNull.Value.Equals((dgvbat.Items[m] as DataRowView).Row.ItemArray[1]) && ((dgvbat.Items[m] as DataRowView).Row.ItemArray[1]).ToString() != "")
                    {
                        if (cbxbatsmen.SelectedValue.ToString() == Convert.ToString((dgvbat.Items[m] as DataRowView).Row.ItemArray[1]))
                        {
                            Runs2 = Runs2 + i;
                            Runnnn[m] = Runs2;

                            if (cbxextras.SelectedIndex == 0)
                            {
                                Balls2 = Balls2 + 1;
                            }
                            Ballsssss[m] = Balls2;

                            //4's
                            if (i == 4)
                            {
                                Fours2 = Fours2 + 1;
                            }
                            Foursssss[m] = Fours2;

                            //6's
                            if (i == 6)
                            {
                                Sixes2 = Sixes2 + 1;
                            }
                            Sixesssss[m] = Sixes2;

                            DataRow dr = dt1.NewRow();
                            dr.BeginEdit();
                            dr["SLNO"] = count+1;
                            dr["Batsman"] = teamA[count];

                            if (teamA[count] == BatsmanNameWhoGotOut)
                            {
                                Bowlertookwicket[m] = BowlerNameWhoTookWicket;
                            }
                            dr["Bowler"] = Bowlertookwicket[m];

                            dr["Runs"] = Runnnn[m];
                            dr["Balls"] = Ballsssss[m];
                            dr["Fours"] = Foursssss[m];
                            dr["Sixes"] = Sixesssss[m];
                            count++;
                            dr.EndEdit();
                            dt1.Rows.Add(dr);
                            dt1.AcceptChanges();

                        }
                        else
                        {
                            DataRow dr = dt1.NewRow();
                            dr.BeginEdit();
                            dr["SLNO"] = count+1;
                            dr["Batsman"] = teamA[count];

                            if (teamA[count] == BatsmanNameWhoGotOut)
                            {
                                Bowlertookwicket[m] = BowlerNameWhoTookWicket;
                            }
                            dr["Bowler"] = Bowlertookwicket[m];

                            if (Runnnn[count] != null)
                            {
                                dr["Runs"] = Runnnn[m];
                                dr["Balls"] = Ballsssss[m];
                                dr["Fours"] = Foursssss[m];
                                dr["Sixes"] = Sixesssss[m];
                            }
                            count++;
                            dr.EndEdit();
                            dt1.Rows.Add(dr);
                            dt1.AcceptChanges();
                        }
                    }
                }
                dgvbat.DataContext = dt1;
            }

            lblbatsman1runs.Content = Runs1 + " runs";
            lblbatsman2runs.Content = Runs2 + " runs";
            lblbatsman1balls.Content = Balls1 + " balls";
            lblbatsman2balls.Content = Balls2 + " balls";
            lblbatsman1fours.Content = Fours1 + " (4)";
            lblbatsman2fours.Content = Fours2 + " (4)";
            lblbatsman1sixes.Content = Sixes1 + " (6)";
            lblbatsman2sixes.Content = Sixes2 + " (6)";

            if (cbxbowler.SelectedIndex == 0)
            {
                DataTable dtbowler = new DataTable();

                dtbowler.Columns.Add("SLNO", Type.GetType("System.String"));
                dtbowler.Columns.Add("Bowler", Type.GetType("System.String"));
                dtbowler.Columns.Add("Overs", Type.GetType("System.String"));
                dtbowler.Columns.Add("Dots", Type.GetType("System.String"));
                dtbowler.Columns.Add("Maidens", Type.GetType("System.String"));
                dtbowler.Columns.Add("Runs", Type.GetType("System.String"));
                dtbowler.Columns.Add("Wickets", Type.GetType("System.String"));
                dtbowler.Columns.Add("Wides", Type.GetType("System.String"));
                dtbowler.Columns.Add("NoBalls", Type.GetType("System.String"));
                dtbowler.Columns.Add("Econ", Type.GetType("System.String"));

                int count = 0;
                for (int m = 0; m <= (teamA.Count - 1); m++)
                {
                    if (!DBNull.Value.Equals((dgvbowl.Items[m] as DataRowView).Row.ItemArray[1]) && ((dgvbowl.Items[m] as DataRowView).Row.ItemArray[1]).ToString() != "")
                    {
                        if (cbxbowler.SelectedValue.ToString() == Convert.ToString((dgvbowl.Items[m] as DataRowView).Row.ItemArray[1]))
                        {
                            if((bowlerballs % 6) == 0)
                            {
                                bowlerovernumber[m] = bowlerovernumber[m] + 1;
                            }
                            
                            bbb = Convert.ToDouble(Convert.ToInt16(bowlerovernumber[m]) + "." + (bowlerballs % 6));
                            bowlerover[m] =  bbb;
                            double abc = bowlerover[m];

                            //temp_overs = temp_overs + 1;

                            DataRow drbowler = dtbowler.NewRow();
                            drbowler.BeginEdit();
                            drbowler["SLNO"] = count + 1;
                            drbowler["Bowler"] = teamB[count];
                            drbowler["Overs"] = bowlerover[m];
                            if(i == 0)
                            {
                                dotssss[m] += 1;
                                drbowler["Dots"] = dotssss[m];
                            }
                            else
                            {
                                drbowler["Dots"] = dotssss[m];
                            }
                            BRunnnn[m] += BowlerRuns;
                            drbowler["Runs"] = BRunnnn[m];
                            if(bowlerwicket!=0)
                            {
                                BWickets[m] += bowlerwicket;
                            }

                            drbowler["Wickets"] = BWickets[m];
                            bowlerwicket = 0;
                            //dr["Fours"] = Foursssss[m];
                            //dr["Sixes"] = Sixesssss[m];
                            count++;
                            drbowler.EndEdit();
                            dtbowler.Rows.Add(drbowler);
                            dtbowler.AcceptChanges();
                        }
                        else
                        {
                            DataRow drbowler = dtbowler.NewRow();
                            drbowler.BeginEdit();
                            drbowler["SLNO"] = count + 1;
                            drbowler["Bowler"] = teamB[count];
                            drbowler["Overs"] = bowlerover[m];
                            drbowler["Dots"] = dotssss[m];
                            drbowler["Runs"] = BRunnnn[m];
                            drbowler["Wickets"] = BWickets[m];



                            //if (Runnnn[count] != null)
                            //{
                            //    dr["Runs"] = Runnnn[m];
                            //    dr["Balls"] = Ballsssss[m];
                            //    dr["Fours"] = Foursssss[m];
                            //    dr["Sixes"] = Sixesssss[m];
                            //}
                            count++;
                            drbowler.EndEdit();
                            dtbowler.Rows.Add(drbowler);
                            dtbowler.AcceptChanges();
                        }
                    }
                }
                dgvbowl.DataContext = dtbowler;
            }

            if ((Ball % 6) == 0)
            {
                MessageBox.Show("Select Next Bowler");
                cbxbowler_DropDownOpened();
               // temp_overs = 0;
            }
            
        }

        public void swapbatsman(int j)
        {
            if((j % 2) ==0)
            {
                blnSwapBatsman = false;
            }
            else
            {
                blnSwapBatsman = true;
            }

            if(blnSwapBatsman==true)
            {
                if (cbxbatsmen.SelectedIndex == 0)
                {
                    cbxbatsmen.SelectedIndex = 1;
                }
                else
                {
                    cbxbatsmen.SelectedIndex = 0;
                }
            }

            if(blnSwapBatsman==false)
            {
                if (cbxbatsmen.SelectedIndex == 0)
                {
                    cbxbatsmen.SelectedIndex = 0;
                }
                else
                {
                    cbxbatsmen.SelectedIndex = 1;
                }
            }

            if(Ball % 6 == 0)
            {
                if (cbxbatsmen.SelectedIndex == 0)
                {
                    cbxbatsmen.SelectedIndex = 1;
                }
                else
                {
                    cbxbatsmen.SelectedIndex = 0;
                }
            }

            //for (BallNumber=1; BallNumber<=500; BallNumber++)
            //{

            //}


        }
      
        private void cbxbowler_DropDownOpened(object sender, EventArgs e)
        {

        }

        private void cbxbowler_DropDownClosed(object sender, EventArgs e)
        {
            int a = cbxbowler.SelectedIndex;
            string abc = cbxbowler.SelectedItem.ToString();

            bowlingplayers.Remove(abc);
            bowlingplayers.Insert(0, abc);
            cbxbowler.Items.Clear();
            for (int i = 0; i < bowlingplayers.Count; i++)
            {
                foreach (var item in bowlingplayers)
                {
                    cbxbowler.Items.Add(item);
                    i++;
                }
            }
            cbxbowler.SelectedIndex = 0;
        }

        private void cbxbatsmen_DropDownOpened(object sender, EventArgs e)
        {
            cbxbatsmen.IsDropDownOpen = true;
        }

        private void cbxbatsmen_DropDownClosed(object sender, EventArgs e)
        {
            int a = cbxbatsmen.SelectedIndex;
            string abc = cbxbatsmen.SelectedItem.ToString();
           

            battingplayers.Remove(abc);
            if(cbxWhoOut.SelectedIndex ==1)
            {
                battingplayers.Insert(1, abc);
            }
            else
            {
                battingplayers.Insert(0, abc);
            }

            cbxbatsmen.Items.Clear();
            for (int i = 0; i < battingplayers.Count; i++)
            {
                foreach (var item in battingplayers)
                {
                    cbxbatsmen.Items.Add(item);
                    i++;
                }
            }
            cbxbatsmen.SelectedIndex = 0;
            calc();
        }
    

        private void cbxWhoOut_DropDownOpened(object sender, EventArgs e)
        {

        }

        private void cbxWhoOut_DropDownClosed(object sender, EventArgs e)
        {
            string whoout = cbxWhoOut.SelectedItem.ToString();
            cbxbatsmen.Items.Remove(whoout);
            battingplayers.Remove(whoout);
            if(cbxWhoOut.SelectedIndex==0)
            {
                Runs1 = 0;
                Balls1 = 0;
                //BowlerName1 = null;
                //BowlerName1 = cbxbowler.SelectedValue.ToString();

            }
            else if(cbxWhoOut.SelectedIndex==1)
            {
                Runs2 = 0;
                Balls2 = 0;
                //BowlerName2 = null;
                //BowlerName2 = cbxbowler.SelectedValue.ToString();
            }
            BowlerNameWhoTookWicket = cbxbowler.SelectedValue.ToString();
            BatsmanNameWhoGotOut = cbxWhoOut.SelectedValue.ToString();

            //calc();
            MessageBox.Show("Select Next Batsman");
            cbxbatsmen_DropDownOpened();
        }
    }
}
