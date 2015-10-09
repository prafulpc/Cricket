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

        ObservableCollection<string> teamA = new ObservableCollection<string>();    //Batting Team
        ObservableCollection<string> teamB = new ObservableCollection<string>();    //Bowling Team

        double Ball = 0;
        int TotalScore = 0;
        string Runs;
        int extra = 0;

        //batsman
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
        
        

        //batsman 1
        string batsman1;
        int Runs1 = 0;
        int Balls1 = 0;
        int Fours1 = 0;
        int Sixes1 = 0;

        //batsman2
        string batsman2;
        int Runs2 = 0;
        int Balls2 = 0;
        int Fours2 = 0;
        int Sixes2 = 0;

        //bowler
        string bowler;
        double bowlerballs = 0;
        int dots = 0;

       // double BallNumber = 0;
        int overs = 0;

        
        bool blnSwapBatsman;


        public MainScoreCard()
        {
            InitializeComponent();
            MainScoreCardBLL objmainscorecard = new MainScoreCardBLL();

            for (int s = 0; s <= 14; s++)
            {
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


            //for (int i = 0; i < Batman1.count1; i++)
            //{
            //    foreach (var item in Batman1.names1)
            //    {
            //        testing.Items.Add(item);
            //        i++;
            //    }
            //}

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
                //cbxbatsmen.Items[i] = Batman.names[i];
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

            //cbxbatsmen.Items.Add(player.striker);
            //cbxbatsmen.Items.Add(player.nonstriker);
            //cbxbowler.Items.Add(player.bowler);

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

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
           if(btnzero.Background == Brushes.Orange)
           {
               CalculateTotalRuns(0);
               swapbatsman(0);
           }
           else if(btnone.Background==Brushes.Orange)
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

        public void CalculateTotalRuns (int i)
        {
            int TotalRuns = 0;

            //No-Extra
            if(cbxextras.SelectedIndex==0)
            {
                Ball = Ball + 1;
                bowlerballs = bowlerballs + 1;
                if(chbxOverThrow.IsChecked==true)
                {
                    TotalRuns = i + Convert.ToInt16(cbxoverthrowruns.SelectedItem.ToString()); 
                    TotalScore = TotalScore + TotalRuns;
                    Runs = TotalRuns.ToString() + " runs.";
                }
                else
                {
                    Runs = i.ToString() + " runs.";
                    TotalScore = TotalScore + i;
                }
            }
                //Wide
            else if (cbxextras.SelectedIndex == 1)
            {
                if (chbxOverThrow.IsChecked == true)
                {
                    TotalRuns = i + 1 + Convert.ToInt16(cbxoverthrowruns.SelectedItem.ToString());
                    TotalScore = TotalScore + TotalRuns;
                    Runs = TotalRuns.ToString() + " runs.";
                    extra = extra + i + 1;
                }
                else
                {
                    Runs = (i+1).ToString() + " runs.";
                    TotalScore = TotalScore + i + 1;
                    extra = extra + i + 1;
                }
            }

            //No-Ball(Front Foot)
            else if (cbxextras.SelectedIndex == 2)
            {
                if (chbxOverThrow.IsChecked == true)
                {
                    TotalRuns = i + 1 + Convert.ToInt16(cbxoverthrowruns.SelectedItem.ToString());
                    TotalScore = TotalScore + TotalRuns;
                    Runs = TotalRuns.ToString() + " runs.";
                    extra = extra + 1;
                }
                else
                {
                    Runs = (i + 1).ToString() + " runs.";
                    TotalScore = TotalScore + i + 1;
                    extra = extra + 1;
                }
            }

            //No-Ball(Above Waist)
            else if (cbxextras.SelectedIndex == 3)
            {
                if (chbxOverThrow.IsChecked == true)
                {
                    TotalRuns = i + 1 + Convert.ToInt16(cbxoverthrowruns.SelectedItem.ToString());
                    TotalScore = TotalScore + TotalRuns;
                    Runs = TotalRuns.ToString() + " runs.";
                    extra = extra + 1;
                }
                else
                {
                    Runs = (i + 1).ToString() + " runs.";
                    TotalScore = TotalScore + i + 1;
                    extra = extra + 1;
                }
            }

            //Leg-Byes
            else if (cbxextras.SelectedIndex==4)
            {
                Ball = Ball + 1;
                bowlerballs = bowlerballs + 1;
                if (chbxOverThrow.IsChecked == true)
                {
                    TotalRuns = i + Convert.ToInt16(cbxoverthrowruns.SelectedItem.ToString());
                    TotalScore = TotalScore + TotalRuns;
                    Runs = TotalRuns.ToString() + " runs.";
                    extra = extra + TotalRuns;
                }
                else
                {
                    Runs = i.ToString() + " runs.";
                    TotalScore = TotalScore + i;
                    extra = extra + i;
                }
            }
                //Byes
            else if (cbxextras.SelectedIndex==5)
            {
                Ball = Ball + 1;
                bowlerballs = bowlerballs + 1;
                if (chbxOverThrow.IsChecked == true)
                {
                    TotalRuns = i + Convert.ToInt16(cbxoverthrowruns.SelectedItem.ToString());
                    TotalScore = TotalScore + TotalRuns;
                    Runs = TotalRuns.ToString() + " runs.";
                    extra = extra + TotalRuns;
                }
                else
                {
                    Runs = i.ToString() + " runs.";
                    TotalScore = TotalScore + i;
                    extra = extra + i;
                }
            }

            lbltotalscore.Content = TotalScore;         //Total Score
            lblOvers.Content = Convert.ToInt16(Math.Truncate(Ball / 6)) + "." + (Ball % 6);     //Total Overs
            batsman1 = cbxbatsmen.Items[0].ToString();
            batsman2 = cbxbatsmen.Items[1].ToString();
            bowler = cbxbowler.Items[0].ToString();
               
           // if ((comboboxvalue.value[0] == "0" && comboboxvalue.value[1] == "0") || (comboboxvalue.value[0] == "1" && comboboxvalue.value[1] == "1"))
            {
                ObservableCollection<string> team1 = teamA;
                ObservableCollection<string> team2 = teamB;
                //teamA (batting)  teamB (bowling)
                //dgvbat.DataContext = objmainscorecard.batting(teamA);
                //dgvbowl.DataContext = objmainscorecard.bowling(teamB);
            }

            //teamA (bowling)  teamB (batting)
            //else if ((comboboxvalue.value[0] == "0" && comboboxvalue.value[1] == "1") || (comboboxvalue.value[0] == "1" && comboboxvalue.value[1] == "0"))
            //{
            //    ObservableCollection<string> team1 = teamB;
            //    ObservableCollection<string> team2 = teamA;
            //    //dgvbat.DataContext = objmainscorecard.batting(teamB);
            //    //dgvbowl.DataContext = objmainscorecard.bowling(teamA);
            //}

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
                            double bbb = Convert.ToDouble(Convert.ToInt16(Math.Truncate(bowlerballs / 6)) + "." + (bowlerballs % 6));
                            bowlerover[m] = bbb;

                            DataRow drbowler = dtbowler.NewRow();
                            drbowler.BeginEdit();
                            drbowler["SLNO"] = count + 1;
                            drbowler["Bowler"] = teamB[count];
                            drbowler["Overs"] = bowlerover[m];
                            //dr["Balls"] = Ballsssss[m];
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
                         //  drbowler["Overs"] = bowlerover[m];
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






        
        }
}
