using Cricket.BLL;
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
using Cricket.View;
using System.Data;
using System.Data.OleDb;
using CricketSol.DAL;
using CricketSol.Database;
using System.Collections.ObjectModel;


namespace Cricket.View
{
    

    public static class TempValuesTeam1
    {
        public static string[] team1 = new string[60];
        public static int index1 = 0;
    }

    public static class TempValuesTeam2
    {
        public static string[] team2 = new string[60];
        public static int index2 = 0;
    }

    public static class comboboxvalue
    {
        public static string[] value = new string[60];
        public static int count = 0;
    }

    public static class captainteam1
    {
        public static string[] captain1 = new string[60];
        public static int captaincount1 = 0;
    }

    public static class captainteam2
    {
        public static string[] captain2 = new string[60];
        public static int captaincount2 = 0;
    }

    public static class keeperteam1
    {
        public static string[] keeper1 = new string[60];
        public static int keepercount1 = 0;
    }

    public static class keeperteam2
    {
        public static string[] keeper2 = new string[60];
        public static int keepercount2 = 0;
    }

    public static class team
    {
        public static string[] name = new string[10];
        public static int count = 0;
    }

    public static class tosswon
    {
        public static string team;
        public static int index;
    }

    public static class electedto
    {
        public static string batball;
        public static int index;
    }

    public static class player
    {
        public static string striker;
        public static int striker_index;

        public static string nonstriker;
        public static int nonstriker_index;
        
        public static string bowler;
        public static int bowler_index;

        public static int No_Of_Players;
    }



    public static class KSCAUID
    {
        public static string[] teamA = new string[50];

        public static int countA = 0;

        public static string[] teamB = new string[50];

        public static int countB = 0;
    }

    public static class Batman
    {
        public static string[] names; // = new string[15];
        public static int count = 0;
    }

    public static class Bowler
    {
        public static string[] names; // = new string[15];
        public static int count = 0;
    }


    /// <summary>
    /// Interaction logic for SelectPlayers.xaml
    /// </summary>
    public partial class SelectPlayers : UserControl
    {
        //public List<string> Batman_names = new List<string>(15);
        //public List<string> Bowler_names = new List<string>(15);

        //string BallByBallOrScoreCard;
        public List<string> list1 = new List<string>(15);
        public List<string> RemoveTeams = new List<string>(15);

        //   ObservableCollection<Official> officialname = Database.GetEntityList<Official>(false, false, false, oleconn, "RecordStatus", "OfficialName");
        OleDbConnection oleconn = Database.getConnection();
        ObservableCollection<DivisionLoad> lstteams = Database.GetEntityList<DivisionLoad>(false, false, false, Database.getReadConnection(), "RecordStatus", "DivisionLoadId");


        DataTable dtlItemDetails = new DataTable();
        DataTable dtlItemDetails3 = new DataTable();
        DataTable dtlItemDetails4 = new DataTable();
        DataTable dtlItemDetails5 = new DataTable();


        public SelectPlayers()
        {

            InitializeComponent();


        }

        private void btnLoadTeam1_Click(object sender, RoutedEventArgs e)
        {
            {

                ObservableCollection<Team> lstteam = Database.GetEntityList<Team>(false, false, false, oleconn, "RecordStatus", "TeamId");
                ObservableCollection<Player> lstPlayer = Database.GetEntityList<Player>(false, false, false, oleconn, "RecordStatus", "PlayerId");

                var objteam = from s in lstteam where s.TeamName == txtTeam1.Text select s;
                List<Team> lstfilterteam = objteam.ToList<Team>();

                var objplayer = lstPlayer.Where(u => lstfilterteam.All(p => p.TeamId.ToString() == u.TeamIdId.ToString()));
                List<Player> lstPlayerfilter = objplayer.OrderBy(p => p.FirstName).ToList<Player>();


                if (txtTeam1.Text == "")
                {
                    MessageBox.Show("Players Cannot Be Loaded Without Selecting The Team");

                }

                else
                {

                    foreach (var objplayeradd in lstPlayerfilter)
                    {

                        //Removing Duplicate Values
                        if (!lbxTeam1.Items.Contains(objplayeradd.FirstName))
                        {
                            lbxTeam1.Items.Add(objplayeradd.FirstName);
                        }
                    }

                    lbxTeam1.SelectedIndex = 0;
                }
            }
        }

        private void btnLoadTeam2_Click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<Team> lstteam = Database.GetEntityList<Team>(false, false, false, oleconn, "RecordStatus", "TeamId");
            ObservableCollection<Player> lstPlayer = Database.GetEntityList<Player>(false, false, false, oleconn, "RecordStatus", "PlayerId");

            var objteam = from s in lstteam where s.TeamName == txtTeam2.Text select s;
            List<Team> lstfilterteam = objteam.ToList<Team>();

            var objplayer = lstPlayer.Where(u => lstfilterteam.All(p => p.TeamId.ToString() == u.TeamIdId.ToString()));
            List<Player> lstPlayerfilter = objplayer.OrderBy(p => p.FirstName).ToList<Player>();

            if (txtTeam1.Text == "")
            {
                MessageBox.Show("Players Cannot Be Loaded Without Selecting The Team");

            }

            else
            {

                foreach (var objplayeradd in lstPlayerfilter)
                {
                    //Removing Duplicate Values
                    if (!lbxTeam2.Items.Contains(objplayeradd.FirstName))
                    {
                        lbxTeam2.Items.Add(objplayeradd.FirstName);
                    }
                }

                lbxTeam2.SelectedIndex = 0;
            }
        }

        private void btnTransferedTeamA_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (lbltransferedTeamA.Items.Count < 11)
                {
                    if (lbxTeam1.SelectedItem != null)
                    {
                        string abc = lbxTeam1.SelectedItem.ToString();
                        lbltransferedTeamA.Items.Add(abc);
                        lbxTeam1.Items.Remove(abc);
                        lbxTeam1.SelectedIndex = 0;
                    }
                    else
                    {
                        MessageBox.Show("No players to Transfer");
                    }
                }
                else
                {
                    MessageBox.Show("Max 11 Players are Allowed");
                }

                lbltransferedTeamA.Focus();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btnReverseTeamA_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (lbltransferedTeamA.SelectedItem != null)
                {
                    string abc = lbltransferedTeamA.SelectedItem.ToString();
                    lbxTeam1.Items.Add(abc);
                    lbltransferedTeamA.Items.Remove(abc);
                }
                else if (lbltransferedTeamA.Items.Count == 0)
                {
                    MessageBox.Show("No players to Reverse");
                }
                else
                {
                    MessageBox.Show("Select Player to Reverse");
                }
                lbltransferedTeamA.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btnTransferedTeamB_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (lbltransferedTeamB.Items.Count < 11)
                {
                    if (lbxTeam2.SelectedItem != null)
                    {
                        string abc = lbxTeam2.SelectedItem.ToString();
                        lbltransferedTeamB.Items.Add(abc);
                        lbxTeam2.Items.Remove(abc);
                        lbxTeam2.SelectedIndex = 0;
                    }
                    else
                    {
                        MessageBox.Show("No players to Transfer");
                    }
                }
                else
                {
                    MessageBox.Show("Max 11 Players are Allowed");
                }

                lbltransferedTeamB.Focus();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btnReverseTeamB_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (lbltransferedTeamB.SelectedItem != null)
                {
                    string abc = lbltransferedTeamB.SelectedItem.ToString();
                    lbxTeam2.Items.Add(abc);
                    lbltransferedTeamB.Items.Remove(abc);
                }
                else if (lbltransferedTeamB.Items.Count == 0)
                {
                    MessageBox.Show("No players to Reverse");
                }
                else
                {
                    MessageBox.Show("Select Player to Reverse");
                }
                lbltransferedTeamB.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btnloadplayers_Click(object sender, RoutedEventArgs e)
        {

            KSCAUID.teamA = new string[60];
            KSCAUID.teamB = new string[60];
            KSCAUID.countA = 0;
            KSCAUID.countB = 0;

            try
            {
                if (lbltransferedTeamA.Items.Count != 0)
                {
                    if (lbltransferedTeamB.Items.Count != 0)
                    {
                        if (cbxteam1captain.SelectedIndex != -1)
                        {
                            if (cbxteam1keeper.SelectedIndex != -1)
                            {
                                if (cbxteam2captain.SelectedIndex != -1)
                                {
                                    if (cbxteam2keeper.SelectedIndex != -1)
                                    {
                                        if (cbxtoss.SelectedIndex != -1)
                                        {
                                            if (cbxelected.SelectedIndex != -1)
                                            {
                                                if (cbxumpire1.SelectedIndex != -1)
                                                {
                                                    if (cbxumpire2.SelectedIndex != -1)
                                                    {
                                                        if (cbxscorer.SelectedIndex != -1)
                                                        {
                                                            if (cbxVenue.SelectedIndex != -1)
                                                            {
                                                                if (startDate.SelectedDate != null)
                                                                {
                                                                    if (EndDate.SelectedDate != null)
                                                                    {
                                                                        if (cbxmatchtype.SelectedItem != null)
                                                                        {
                                                                            if (EndDate.SelectedDate.Value > startDate.SelectedDate.Value)
                                                                            {
                                                                                TempValuesTeam1.team1 = new string[60];
                                                                                TempValuesTeam2.team2 = new string[60];
                                                                                TempValuesTeam1.index1 = 0;
                                                                                TempValuesTeam2.index2 = 0;
                                                                                comboboxvalue.value = new string[60];
                                                                                comboboxvalue.count = 0;
                                                                                foreach (var items1 in lbltransferedTeamA.Items)
                                                                                {
                                                                                    TempValuesTeam1.team1[TempValuesTeam1.index1] = items1.ToString();
                                                                                    TempValuesTeam1.index1++;
                                                                                }



                                                                                foreach (var items2 in lbltransferedTeamB.Items)
                                                                                {
                                                                                    TempValuesTeam2.team2[TempValuesTeam2.index2] = items2.ToString();
                                                                                    TempValuesTeam2.index2++;
                                                                                }

                                                                                if (cbxtoss.SelectedIndex == 0)
                                                                                {
                                                                                    comboboxvalue.value[comboboxvalue.count] = "0";
                                                                                    comboboxvalue.count++;
                                                                                    tosswon.index = 0;

                                                                                }
                                                                                if (cbxtoss.SelectedIndex == 1)
                                                                                {
                                                                                    comboboxvalue.value[comboboxvalue.count] = "1";
                                                                                    comboboxvalue.count++;
                                                                                    tosswon.index = 1;
                                                                                }

                                                                                if (cbxelected.SelectedIndex == 0)
                                                                                {
                                                                                    comboboxvalue.value[comboboxvalue.count] = "0";
                                                                                    comboboxvalue.count++;
                                                                                    electedto.index = 0;
                                                                                }
                                                                                if (cbxelected.SelectedIndex == 1)
                                                                                {
                                                                                    comboboxvalue.value[comboboxvalue.count] = "1";
                                                                                    comboboxvalue.count++;
                                                                                    electedto.index = 1;
                                                                                }

                                                                                {
                                                                                    string sql = "select firstname,playerid,Teamid,kscauid from players";
                                                                                    OleDbDataAdapter adpt = new OleDbDataAdapter(sql, oleconn);
                                                                                    adpt.Fill(dtlItemDetails);

                                                                                    string sql2 = "select * from teams";
                                                                                    OleDbDataAdapter adpt1 = new OleDbDataAdapter(sql2, oleconn);
                                                                                    adpt1.Fill(dtlItemDetails5);




                                                                                    foreach (DataRowView objPlayer in dtlItemDetails.DefaultView)
                                                                                    {
                                                                                        foreach (DataRowView objTeam in dtlItemDetails5.DefaultView)
                                                                                        {

                                                                                            for (int p = 0; p <= (lbltransferedTeamA.Items.Count - 1); p++)
                                                                                            {
                                                                                                if (lbltransferedTeamA.Items[p].ToString() == (objPlayer["FirstName"].ToString()))
                                                                                                {
                                                                                                    if (txtTeam1.Text == objTeam["TeamName"].ToString())
                                                                                                    {
                                                                                                        if (objTeam["TeamId"].ToString() == objPlayer["TeamId"].ToString())
                                                                                                        {
                                                                                                            KSCAUID.teamA[p] = (objPlayer["KSCAUID"].ToString());
                                                                                                            p++;
                                                                                                        }
                                                                                                    }

                                                                                                }
                                                                                            }



                                                                                            //KSCAUID

                                                                                            for (int q = 0; q <= (lbltransferedTeamB.Items.Count - 1); q++)
                                                                                            {
                                                                                                if (lbltransferedTeamB.Items[q].ToString() == (objPlayer["FirstName"].ToString()))
                                                                                                {
                                                                                                    if (txtTeam2.Text == objTeam["TeamName"].ToString())
                                                                                                    {
                                                                                                        if (objTeam["TeamId"].ToString() == objPlayer["TeamId"].ToString())
                                                                                                        {
                                                                                                            KSCAUID.teamB[q] = (objPlayer["KSCAUID"].ToString());
                                                                                                            q++;
                                                                                                        }
                                                                                                    }

                                                                                                }

                                                                                            }
                                                                                        }

                                                                                    }




                                                                                    captainkeeperselect();



                                                                                    //save

                                                                                    Season objSeason = Database.GetEntity<Season>(Guid.Parse(TempValues.LoadSeasonId), false, false, false, oleconn);
                                                                                    Division objDivision = Database.GetEntity<Division>(Guid.Parse(selectedgridvalue.LoadDivisionId), false, false, false, oleconn);

                                                                                    ObservableCollection<ListBoxSave> lstlist = Database.GetEntityList<ListBoxSave>(false, false, false, oleconn, "RecordStatus", "ListBoxSaveId");
                                                                                    var objlist = from s in lstlist where s.MatchId == selectedgridvalue.matchid && s.SeasonId.ToString() == TempValues.LoadSeasonId && s.DivisionId.ToString() == selectedgridvalue.LoadDivisionId select s;
                                                                                    List<ListBoxSave> lstfilterlist = objlist.ToList<ListBoxSave>();

                                                                                    if (lstfilterlist.Count > 0)
                                                                                    {
                                                                                        if (division.num == 1)
                                                                                        {
                                                                                            Button btn = (Button)sender;
                                                                                            btn.Command = NavigationCommands.GoToPage;
                                                                                            btn.CommandParameter = "/View/FirstDivScoreCard.xaml";


                                                                                            //////////
                                                                                            lbxTeam1.ItemsSource = null;
                                                                                            lbxTeam1.Items.Clear();

                                                                                            lbxTeam2.ItemsSource = null;
                                                                                            lbxTeam2.Items.Clear();

                                                                                            lbltransferedTeamA.ItemsSource = null;
                                                                                            lbltransferedTeamA.Items.Clear();

                                                                                            lbltransferedTeamB.ItemsSource = null;
                                                                                            lbltransferedTeamB.Items.Clear();

                                                                                            cbxteam1captain.ItemsSource = null;
                                                                                            cbxteam1captain.Items.Clear();

                                                                                            cbxteam2captain.ItemsSource = null;
                                                                                            cbxteam2captain.Items.Clear();

                                                                                            cbxteam1keeper.ItemsSource = null;
                                                                                            cbxteam1keeper.Items.Clear();

                                                                                            cbxteam2keeper.ItemsSource = null;
                                                                                            cbxteam2keeper.Items.Clear();

                                                                                            cbxelected.ItemsSource = null;
                                                                                            cbxelected.Items.Clear();

                                                                                            cbxtoss.ItemsSource = null;
                                                                                            cbxtoss.Items.Clear();

                                                                                            cbxstriker.ItemsSource = null;
                                                                                            cbxstriker.Items.Clear();

                                                                                            cbxnonstriker.ItemsSource = null;
                                                                                            cbxnonstriker.Items.Clear();

                                                                                            cbxbowler.ItemsSource = null;
                                                                                            cbxbowler.Items.Clear();

                                                                                            cbxumpire1.ItemsSource = null;
                                                                                            cbxumpire1.Items.Clear();

                                                                                            cbxumpire2.ItemsSource = null;
                                                                                            cbxumpire2.Items.Clear();

                                                                                            cbxscorer.ItemsSource = null;
                                                                                            cbxscorer.Items.Clear();

                                                                                            cbxVenue.ItemsSource = null;
                                                                                            cbxVenue.Items.Clear();
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            Button btn = (Button)sender;
                                                                                            btn.Command = NavigationCommands.GoToPage;
                                                                                            btn.CommandParameter = "/View/ScoreCard.xaml";

                                                                                            //////
                                                                                            lbxTeam1.ItemsSource = null;
                                                                                            lbxTeam1.Items.Clear();

                                                                                            lbxTeam2.ItemsSource = null;
                                                                                            lbxTeam2.Items.Clear();

                                                                                            lbltransferedTeamA.ItemsSource = null;
                                                                                            lbltransferedTeamA.Items.Clear();

                                                                                            lbltransferedTeamB.ItemsSource = null;
                                                                                            lbltransferedTeamB.Items.Clear();

                                                                                            cbxteam1captain.ItemsSource = null;
                                                                                            cbxteam1captain.Items.Clear();

                                                                                            cbxteam2captain.ItemsSource = null;
                                                                                            cbxteam2captain.Items.Clear();

                                                                                            cbxteam1keeper.ItemsSource = null;
                                                                                            cbxteam1keeper.Items.Clear();

                                                                                            cbxteam2keeper.ItemsSource = null;
                                                                                            cbxteam2keeper.Items.Clear();

                                                                                            cbxelected.ItemsSource = null;
                                                                                            cbxelected.Items.Clear();

                                                                                            cbxtoss.ItemsSource = null;
                                                                                            cbxtoss.Items.Clear();

                                                                                            cbxstriker.ItemsSource = null;
                                                                                            cbxstriker.Items.Clear();

                                                                                            cbxnonstriker.ItemsSource = null;
                                                                                            cbxnonstriker.Items.Clear();

                                                                                            cbxbowler.ItemsSource = null;
                                                                                            cbxbowler.Items.Clear();

                                                                                            cbxumpire1.ItemsSource = null;
                                                                                            cbxumpire1.Items.Clear();

                                                                                            cbxumpire2.ItemsSource = null;
                                                                                            cbxumpire2.Items.Clear();

                                                                                            cbxscorer.ItemsSource = null;
                                                                                            cbxscorer.Items.Clear();

                                                                                            cbxVenue.ItemsSource = null;
                                                                                            cbxVenue.Items.Clear();
                                                                                        }

                                                                                    }
                                                                                    else
                                                                                    {

                                                                                        Fixture objfixturestoadd = Database.GetEntity<Fixture>(Guid.Parse(selectedgridvalue._fixtureid), false, false, false, oleconn);

                                                                                        objfixturestoadd.FromDate = startDate.SelectedDate.Value.Date;
                                                                                        objfixturestoadd.ToDate = EndDate.SelectedDate.Value.Date;
                                                                                        objfixturestoadd.Venue = cbxVenue.Text;
                                                                                        objfixturestoadd.Umpire = cbxumpire1.Text;
                                                                                        objfixturestoadd.Umpiree = cbxumpire2.Text;
                                                                                        objfixturestoadd.Scorer = cbxscorer.Text;
                                                                                        objfixturestoadd.MatchType = cbxmatchtype.Text;

                                                                                        temp.stratdate = startDate.SelectedDate.Value.Date.ToString();
                                                                                        temp.enddate = EndDate.SelectedDate.Value.Date.ToString();
                                                                                        NameOf.Venue = cbxVenue.Text;
                                                                                        NameOf.Umpire1 = cbxumpire1.Text;
                                                                                        NameOf.Umpire2 = cbxumpire2.Text;
                                                                                        NameOf.Scorer = cbxscorer.Text;
                                                                                        temp.matchtype = cbxmatchtype.Text;
                                                                                        Database.SaveEntity<Fixture>(objfixturestoadd, oleconn);


                                                                                        if (lbxTeam1.Items.Count > 0)
                                                                                        {

                                                                                            foreach (var abc in lbxTeam1.Items)
                                                                                            {
                                                                                                ListBoxSave objListBoxSave = Database.GetNewEntity<ListBoxSave>();
                                                                                                objListBoxSave.TeamOne = abc.ToString();
                                                                                                objListBoxSave.Season = objSeason;
                                                                                                objListBoxSave.Division = objDivision;
                                                                                                objListBoxSave.MatchId = selectedgridvalue.matchid;
                                                                                                objListBoxSave.StartDate = startDate.SelectedDate.Value.Date.ToString();
                                                                                                objListBoxSave.EndDate = EndDate.SelectedDate.Value.Date.ToString();
                                                                                                objListBoxSave.Venue = cbxVenue.Text;
                                                                                                objListBoxSave.UmpireOne = cbxumpire1.Text;
                                                                                                objListBoxSave.UmpireTwo = cbxumpire2.Text;
                                                                                                objListBoxSave.Scorer = cbxscorer.Text;
                                                                                                objListBoxSave.MatchType = cbxmatchtype.Text;
                                                                                                objListBoxSave.TeamOneCaptain = cbxteam1captain.Text;
                                                                                                objListBoxSave.TeamTwoCaptain = cbxteam2captain.Text;
                                                                                                objListBoxSave.TeamOneWk = cbxteam1keeper.Text;
                                                                                                objListBoxSave.TeamTwoWk = cbxteam2keeper.Text;
                                                                                                objListBoxSave.TossWon = cbxtoss.Text;
                                                                                                objListBoxSave.ElectedTo = cbxelected.Text;
                                                                                                objListBoxSave.TeamOneName = txtTeam1.Text;
                                                                                                objListBoxSave.TeamtwoName = txtTeam2.Text;
                                                                                                objListBoxSave.TossWonIndex = Convert.ToInt16(tosswon.index);
                                                                                                objListBoxSave.ElectedToIndex = Convert.ToInt16(electedto.index);

                                                                                                //objListBoxSave.Striker = cbxstriker.Text;
                                                                                                //objListBoxSave.NonStriker = cbxnonstriker.Text;
                                                                                                //objListBoxSave.Bowler = cbxbowler.Text;

                                                                                                Database.SaveEntity<ListBoxSave>(objListBoxSave, oleconn);


                                                                                            }
                                                                                        }

                                                                                        if (lbxTeam2.Items.Count > 0)
                                                                                        {
                                                                                            foreach (var abc in lbxTeam2.Items)
                                                                                            {
                                                                                                ListBoxSave objListBoxSave = Database.GetNewEntity<ListBoxSave>();
                                                                                                objListBoxSave.TeamTwo = abc.ToString();
                                                                                                objListBoxSave.Season = objSeason;
                                                                                                objListBoxSave.Division = objDivision;
                                                                                                objListBoxSave.MatchId = selectedgridvalue.matchid;
                                                                                                objListBoxSave.StartDate = startDate.SelectedDate.Value.Date.ToString();
                                                                                                objListBoxSave.EndDate = EndDate.SelectedDate.Value.Date.ToString();
                                                                                                objListBoxSave.Venue = cbxVenue.Text;
                                                                                                objListBoxSave.UmpireOne = cbxumpire1.Text;
                                                                                                objListBoxSave.UmpireTwo = cbxumpire2.Text;
                                                                                                objListBoxSave.Scorer = cbxscorer.Text;
                                                                                                objListBoxSave.MatchType = cbxmatchtype.Text;
                                                                                                objListBoxSave.TeamOneCaptain = cbxteam1captain.Text;
                                                                                                objListBoxSave.TeamTwoCaptain = cbxteam2captain.Text;
                                                                                                objListBoxSave.TeamOneWk = cbxteam1keeper.Text;
                                                                                                objListBoxSave.TeamTwoWk = cbxteam2keeper.Text;
                                                                                                objListBoxSave.TossWon = cbxtoss.Text;
                                                                                                objListBoxSave.ElectedTo = cbxelected.Text;
                                                                                                objListBoxSave.TeamOneName = txtTeam1.Text;
                                                                                                objListBoxSave.TeamtwoName = txtTeam2.Text;
                                                                                                objListBoxSave.TossWonIndex = Convert.ToInt16(tosswon.index);
                                                                                                objListBoxSave.ElectedToIndex = Convert.ToInt16(electedto.index);

                                                                                                //objListBoxSave.Striker = cbxstriker.Text;
                                                                                                //objListBoxSave.NonStriker = cbxnonstriker.Text;
                                                                                                //objListBoxSave.Bowler = cbxbowler.Text;

                                                                                                Database.SaveEntity<ListBoxSave>(objListBoxSave, oleconn);
                                                                                            }
                                                                                        }


                                                                                        if (lbltransferedTeamA.Items.Count > 0)
                                                                                        {
                                                                                            foreach (var abc in lbltransferedTeamA.Items)
                                                                                            {
                                                                                                ListBoxSave objListBoxSave = Database.GetNewEntity<ListBoxSave>();
                                                                                                objListBoxSave.TransferedTeamOne = abc.ToString();
                                                                                                objListBoxSave.Season = objSeason;
                                                                                                objListBoxSave.Division = objDivision;
                                                                                                objListBoxSave.MatchId = selectedgridvalue.matchid;
                                                                                                objListBoxSave.StartDate = startDate.SelectedDate.Value.Date.ToString();
                                                                                                objListBoxSave.EndDate = EndDate.SelectedDate.Value.Date.ToString();
                                                                                                objListBoxSave.Venue = cbxVenue.Text;
                                                                                                objListBoxSave.UmpireOne = cbxumpire1.Text;
                                                                                                objListBoxSave.UmpireTwo = cbxumpire2.Text;
                                                                                                objListBoxSave.Scorer = cbxscorer.Text;
                                                                                                objListBoxSave.MatchType = cbxmatchtype.Text;
                                                                                                objListBoxSave.TeamOneCaptain = cbxteam1captain.Text;
                                                                                                objListBoxSave.TeamTwoCaptain = cbxteam2captain.Text;
                                                                                                objListBoxSave.TeamOneWk = cbxteam1keeper.Text;
                                                                                                objListBoxSave.TeamTwoWk = cbxteam2keeper.Text;
                                                                                                objListBoxSave.TossWon = cbxtoss.Text;
                                                                                                objListBoxSave.ElectedTo = cbxelected.Text;
                                                                                                objListBoxSave.TeamOneName = txtTeam1.Text;
                                                                                                objListBoxSave.TeamtwoName = txtTeam2.Text;
                                                                                                objListBoxSave.TossWonIndex = Convert.ToInt16(tosswon.index);
                                                                                                objListBoxSave.ElectedToIndex = Convert.ToInt16(electedto.index);

                                                                                                //objListBoxSave.Striker = cbxstriker.Text;
                                                                                                //objListBoxSave.NonStriker = cbxnonstriker.Text;
                                                                                                //objListBoxSave.Bowler = cbxbowler.Text;


                                                                                                Database.SaveEntity<ListBoxSave>(objListBoxSave, oleconn);
                                                                                            }
                                                                                        }

                                                                                        if (lbltransferedTeamB.Items.Count >= 0)
                                                                                        {
                                                                                            foreach (var abc in lbltransferedTeamB.Items)
                                                                                            {
                                                                                                ListBoxSave objListBoxSave = Database.GetNewEntity<ListBoxSave>();
                                                                                                objListBoxSave.TransferedTeamTwo = abc.ToString();
                                                                                                objListBoxSave.Season = objSeason;
                                                                                                objListBoxSave.Division = objDivision;
                                                                                                objListBoxSave.MatchId = selectedgridvalue.matchid;
                                                                                                objListBoxSave.StartDate = startDate.SelectedDate.Value.Date.ToString();
                                                                                                objListBoxSave.EndDate = EndDate.SelectedDate.Value.Date.ToString();
                                                                                                objListBoxSave.Venue = cbxVenue.Text;
                                                                                                objListBoxSave.UmpireOne = cbxumpire1.Text;
                                                                                                objListBoxSave.UmpireTwo = cbxumpire2.Text;
                                                                                                objListBoxSave.Scorer = cbxscorer.Text;
                                                                                                objListBoxSave.MatchType = cbxmatchtype.Text;
                                                                                                objListBoxSave.TeamOneCaptain = cbxteam1captain.Text;
                                                                                                objListBoxSave.TeamTwoCaptain = cbxteam2captain.Text;
                                                                                                objListBoxSave.TeamOneWk = cbxteam1keeper.Text;
                                                                                                objListBoxSave.TeamTwoWk = cbxteam2keeper.Text;
                                                                                                objListBoxSave.TossWon = cbxtoss.Text;
                                                                                                objListBoxSave.ElectedTo = cbxelected.Text;
                                                                                                objListBoxSave.TeamOneName = txtTeam1.Text;
                                                                                                objListBoxSave.TeamtwoName = txtTeam2.Text;
                                                                                                objListBoxSave.TossWonIndex = Convert.ToInt16(tosswon.index);
                                                                                                objListBoxSave.ElectedToIndex = Convert.ToInt16(electedto.index);

                                                                                                //objListBoxSave.Striker = cbxstriker.Text;
                                                                                                //objListBoxSave.NonStriker = cbxnonstriker.Text;
                                                                                                //objListBoxSave.Bowler = cbxbowler.Text;

                                                                                                Database.SaveEntity<ListBoxSave>(objListBoxSave, oleconn);
                                                                                            }
                                                                                        }

                                                                                        if (division.num == 1)
                                                                                        {
                                                                                            Button btn = (Button)sender;
                                                                                            btn.Command = NavigationCommands.GoToPage;
                                                                                            btn.CommandParameter = "/View/FirstDivScoreCard.xaml";

                                                                                            ///////////

                                                                                            lbxTeam1.ItemsSource = null;
                                                                                            lbxTeam1.Items.Clear();

                                                                                            lbxTeam2.ItemsSource = null;
                                                                                            lbxTeam2.Items.Clear();

                                                                                            lbltransferedTeamA.ItemsSource = null;
                                                                                            lbltransferedTeamA.Items.Clear();

                                                                                            lbltransferedTeamB.ItemsSource = null;
                                                                                            lbltransferedTeamB.Items.Clear();

                                                                                            cbxteam1captain.ItemsSource = null;
                                                                                            cbxteam1captain.Items.Clear();

                                                                                            cbxteam2captain.ItemsSource = null;
                                                                                            cbxteam2captain.Items.Clear();

                                                                                            cbxteam1keeper.ItemsSource = null;
                                                                                            cbxteam1keeper.Items.Clear();

                                                                                            cbxteam2keeper.ItemsSource = null;
                                                                                            cbxteam2keeper.Items.Clear();

                                                                                            cbxelected.ItemsSource = null;
                                                                                            cbxelected.Items.Clear();

                                                                                            cbxtoss.ItemsSource = null;
                                                                                            cbxtoss.Items.Clear();

                                                                                            cbxstriker.ItemsSource = null;
                                                                                            cbxstriker.Items.Clear();

                                                                                            cbxnonstriker.ItemsSource = null;
                                                                                            cbxnonstriker.Items.Clear();

                                                                                            cbxbowler.ItemsSource = null;
                                                                                            cbxbowler.Items.Clear();

                                                                                            cbxumpire1.ItemsSource = null;
                                                                                            cbxumpire1.Items.Clear();

                                                                                            cbxumpire2.ItemsSource = null;
                                                                                            cbxumpire2.Items.Clear();

                                                                                            cbxscorer.ItemsSource = null;
                                                                                            cbxscorer.Items.Clear();

                                                                                            cbxVenue.ItemsSource = null;
                                                                                            cbxVenue.Items.Clear();
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            Button btn = (Button)sender;
                                                                                            btn.Command = NavigationCommands.GoToPage;
                                                                                            btn.CommandParameter = "/View/ScoreCard.xaml";
                                                                                            //////////

                                                                                            lbxTeam1.ItemsSource = null;
                                                                                            lbxTeam1.Items.Clear();

                                                                                            lbxTeam2.ItemsSource = null;
                                                                                            lbxTeam2.Items.Clear();

                                                                                            lbltransferedTeamA.ItemsSource = null;
                                                                                            lbltransferedTeamA.Items.Clear();

                                                                                            lbltransferedTeamB.ItemsSource = null;
                                                                                            lbltransferedTeamB.Items.Clear();

                                                                                            cbxteam1captain.ItemsSource = null;
                                                                                            cbxteam1captain.Items.Clear();

                                                                                            cbxteam2captain.ItemsSource = null;
                                                                                            cbxteam2captain.Items.Clear();

                                                                                            cbxteam1keeper.ItemsSource = null;
                                                                                            cbxteam1keeper.Items.Clear();

                                                                                            cbxteam2keeper.ItemsSource = null;
                                                                                            cbxteam2keeper.Items.Clear();

                                                                                            cbxelected.ItemsSource = null;
                                                                                            cbxelected.Items.Clear();

                                                                                            cbxtoss.ItemsSource = null;
                                                                                            cbxtoss.Items.Clear();

                                                                                            cbxstriker.ItemsSource = null;
                                                                                            cbxstriker.Items.Clear();

                                                                                            cbxnonstriker.ItemsSource = null;
                                                                                            cbxnonstriker.Items.Clear();

                                                                                            cbxbowler.ItemsSource = null;
                                                                                            cbxbowler.Items.Clear();

                                                                                            cbxumpire1.ItemsSource = null;
                                                                                            cbxumpire1.Items.Clear();

                                                                                            cbxumpire2.ItemsSource = null;
                                                                                            cbxumpire2.Items.Clear();

                                                                                            cbxscorer.ItemsSource = null;
                                                                                            cbxscorer.Items.Clear();

                                                                                            cbxVenue.ItemsSource = null;
                                                                                            cbxVenue.Items.Clear();                                                                                            //////////////
                                                                                        }

                                                                                        lstfilterlist.Clear();
                                                                                    }
                                                                                }
                                                                            }
                                                                            else
                                                                            {
                                                                                MessageBox.Show("End Date must be greater than start date for a match !!");
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            MessageBox.Show("Select Match Type For This Match");
                                                                        }

                                                                    }
                                                                    else
                                                                    {
                                                                        MessageBox.Show("Select End Date For This Match");
                                                                    }

                                                                }
                                                                else
                                                                {
                                                                    MessageBox.Show("Select Start Date For This Match");
                                                                }


                                                            }
                                                            else
                                                            {
                                                                MessageBox.Show("Select Venue For This Match");
                                                            }

                                                        }
                                                        else
                                                        {
                                                            MessageBox.Show("Select Scorer For This Match");
                                                        }
                                                    }
                                                    else
                                                    {
                                                        MessageBox.Show("Select Umpire2 For This Match");
                                                    }


                                                }

                                                else
                                                {
                                                    MessageBox.Show("Select Umpire1 For This Match");
                                                }

                                            }
                                            else
                                            {
                                                MessageBox.Show("Select who Elected to Bat or Bowl");
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("Select Who Won Toss");
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Select Second Team Keeper");
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Select Second Team Captian");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Select First Team Keeper");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Select First Team Captian");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Players List 2 is Empty");
                    }
                }
                else
                {
                    MessageBox.Show("Players List 1 is Empty");
                }

            }

            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void captainkeeperselect()
        {
            try
            {
                captainteam1.captain1 = new string[60];
                captainteam1.captaincount1 = 0;
                captainteam2.captain2 = new string[60];
                captainteam2.captaincount2 = 0;
                keeperteam1.keeper1 = new string[60];
                keeperteam1.keepercount1 = 0;
                keeperteam2.keeper2 = new string[60];
                keeperteam2.keepercount2 = 0;


                if ((cbxtoss.SelectedIndex == 0 && cbxelected.SelectedIndex == 0) || (cbxtoss.SelectedIndex == 1 && cbxelected.SelectedIndex == 1))
                {
                    team.name = new string[60];
                    team.count = 0;

                    captainteam1.captain1[captainteam1.captaincount1] = cbxteam1captain.SelectedValue.ToString();
                    keeperteam1.keeper1[keeperteam1.keepercount1] = cbxteam1keeper.SelectedValue.ToString();

                    captainteam2.captain2[captainteam2.captaincount2] = cbxteam2captain.SelectedValue.ToString();
                    keeperteam2.keeper2[keeperteam2.keepercount2] = cbxteam2keeper.SelectedValue.ToString();
                    team.name[team.count] = txtTeam1.Text;
                    team.count++;
                    team.name[team.count] = txtTeam2.Text;

                    tempvalues.teamA = txtTeam1.Text;
                    tempvalues.teamB = txtTeam2.Text;
                }

                else if ((cbxtoss.SelectedIndex == 0 && cbxelected.SelectedIndex == 1) || (cbxtoss.SelectedIndex == 1 && cbxelected.SelectedIndex == 0))
                {
                    team.name = new string[60];
                    team.count = 0;

                    captainteam1.captain1[captainteam1.captaincount1] = cbxteam2captain.SelectedValue.ToString();
                    captainteam2.captain2[captainteam2.captaincount2] = cbxteam1captain.SelectedValue.ToString();

                    keeperteam1.keeper1[keeperteam1.keepercount1] = cbxteam2keeper.SelectedValue.ToString();
                    keeperteam2.keeper2[keeperteam2.keepercount2] = cbxteam1keeper.SelectedValue.ToString();
                    team.name[team.count] = txtTeam2.Text;
                    team.count++;
                    team.name[team.count] = txtTeam1.Text;

                    tempvalues.teamA = txtTeam2.Text;
                    tempvalues.teamB = txtTeam1.Text;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void cbxteam1captain_DropDownOpened(object sender, EventArgs e)
        {
            try
            {
                cbxteam1captain.Items.Clear();
                foreach (var team1 in lbltransferedTeamA.Items)
                {
                    cbxteam1captain.Items.Add(team1);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void cbxteam1keeper_DropDownOpened(object sender, EventArgs e)
        {
            try
            {
                cbxteam1keeper.Items.Clear();
                foreach (var team1 in lbltransferedTeamA.Items)
                {
                    cbxteam1keeper.Items.Add(team1);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void cbxteam2captain_DropDownOpened(object sender, EventArgs e)
        {
            try
            {
                cbxteam2captain.Items.Clear();
                foreach (var team2 in lbltransferedTeamB.Items)
                {
                    cbxteam2captain.Items.Add(team2);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void cbxteam2keeper_DropDownOpened(object sender, EventArgs e)
        {
            try
            {
                cbxteam2keeper.Items.Clear();
                foreach (var team2 in lbltransferedTeamB.Items)
                {
                    cbxteam2keeper.Items.Add(team2);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btnUp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedIndex = this.lbltransferedTeamA.SelectedIndex;

                if (selectedIndex > 0)
                {
                    var itemToMoveUp = this.lbltransferedTeamA.Items[selectedIndex];
                    this.lbltransferedTeamA.Items.RemoveAt(selectedIndex);
                    this.lbltransferedTeamA.Items.Insert(selectedIndex - 1, itemToMoveUp);
                    this.lbltransferedTeamA.SelectedIndex = selectedIndex - 1;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btnDown_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedIndex = this.lbltransferedTeamA.SelectedIndex;

                if (selectedIndex + 1 < this.lbltransferedTeamA.Items.Count)
                {
                    var itemToMoveDown = this.lbltransferedTeamA.Items[selectedIndex];
                    this.lbltransferedTeamA.Items.RemoveAt(selectedIndex);
                    this.lbltransferedTeamA.Items.Insert(selectedIndex + 1, itemToMoveDown);
                    this.lbltransferedTeamA.SelectedIndex = selectedIndex + 1;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btnUp2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedIndex = this.lbltransferedTeamB.SelectedIndex;

                if (selectedIndex > 0)
                {
                    var itemToMoveUp = this.lbltransferedTeamB.Items[selectedIndex];
                    this.lbltransferedTeamB.Items.RemoveAt(selectedIndex);
                    this.lbltransferedTeamB.Items.Insert(selectedIndex - 1, itemToMoveUp);
                    this.lbltransferedTeamB.SelectedIndex = selectedIndex - 1;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btnDown2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedIndex = this.lbltransferedTeamB.SelectedIndex;

                if (selectedIndex + 1 < this.lbltransferedTeamB.Items.Count)
                {
                    var itemToMoveDown = this.lbltransferedTeamB.Items[selectedIndex];
                    this.lbltransferedTeamB.Items.RemoveAt(selectedIndex);
                    this.lbltransferedTeamB.Items.Insert(selectedIndex + 1, itemToMoveDown);
                    this.lbltransferedTeamB.SelectedIndex = selectedIndex + 1;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void cbxtoss_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cbxtoss.SelectedIndex == 0)
                {
                    tosswon.team = txtTeam1.Text;
                }
                else if (cbxtoss.SelectedIndex == 1)
                {
                    tosswon.team = txtTeam2.Text;
                }
                
                cbxelected.SelectedIndex = -1;
                cbxstriker.SelectedIndex = -1;
                cbxnonstriker.SelectedIndex = -1;
                cbxbowler.SelectedIndex = -1;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void cbxelected_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cbxelected.SelectedIndex == 0)
                {
                    electedto.batball = "BAT";
                }
                else if (cbxelected.SelectedIndex == 1)
                {
                    electedto.batball = "BOWL";
                }

                cbxstriker.SelectedIndex = -1;
                cbxnonstriker.SelectedIndex = -1;
                cbxbowler.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {

                chksemifinalfinals1.IsChecked = false;


                lbxTeam1.IsEnabled = true;
                lbxTeam2.IsEnabled = true;
                lbltransferedTeamA.IsEnabled = true;
                lbltransferedTeamB.IsEnabled = true;
                cbxteam1captain.IsEnabled = true;
                cbxteam2captain.IsEnabled = true;
                cbxteam1keeper.IsEnabled = true;
                cbxteam2keeper.IsEnabled = true;
                cbxtoss.IsEnabled = true;
                cbxelected.IsEnabled = true;

                cbxstriker.IsEnabled = true;
                cbxnonstriker.IsEnabled = true;
                cbxbowler.IsEnabled = true;

                startDate.IsEnabled = true;
                EndDate.IsEnabled = true;
                cbxumpire1.IsEnabled = true;
                cbxumpire2.IsEnabled = true;
                cbxscorer.IsEnabled = true;
                cbxVenue.IsEnabled = true;
                cbxmatchtype.IsEnabled = true;
                txtTeam1.IsEnabled = true;
                txtTeam2.IsEnabled = true;
                startDate.IsEnabled = true;
                EndDate.IsEnabled = true;
                btnUp.IsEnabled = true;
                btnDown.IsEnabled = true;
                btnDown2.IsEnabled = true;
                btnUp2.IsEnabled = true;
                btnLoadTeam1.IsEnabled = true;
                btnLoadTeam2.IsEnabled = true;
                btnTransferedTeamA.IsEnabled = true;
                btnTransferedTeamB.IsEnabled = true;
                btnReverseTeamA.IsEnabled = true;
                btnReverseTeamB.IsEnabled = true;


                txtTeam1.Text = String.Empty;
                txtTeam2.Text = String.Empty;
                lbxTeam1.Items.Clear();
                lbxTeam2.Items.Clear();
                
                cbxtoss.ItemsSource = null;
                cbxelected.ItemsSource = null;
                cbxtoss.Items.Clear();
                cbxelected.Items.Clear();

                cbxstriker.ItemsSource = null;
                cbxnonstriker.ItemsSource = null;
                cbxbowler.ItemsSource = null;
                cbxstriker.Items.Clear();
                cbxnonstriker.Items.Clear();
                cbxbowler.Items.Clear();

                startDate.Text = null;
                EndDate.Text = null;

                lbxTeam1.ItemsSource = null;
                lbxTeam1.Items.Clear();

                lbxTeam2.ItemsSource = null;
                lbxTeam2.Items.Clear();

                lbltransferedTeamA.ItemsSource = null;
                lbltransferedTeamA.Items.Clear();

                lbltransferedTeamB.ItemsSource = null;
                lbltransferedTeamB.Items.Clear();

                cbxteam1captain.ItemsSource = null;
                cbxteam1captain.Items.Clear();

                cbxteam2captain.ItemsSource = null;
                cbxteam2captain.Items.Clear();

                cbxteam1keeper.ItemsSource = null;
                cbxteam1keeper.Items.Clear();

                cbxteam2keeper.ItemsSource = null;
                cbxteam2keeper.Items.Clear();

                cbxelected.ItemsSource = null;
                cbxelected.Items.Clear();

                cbxtoss.ItemsSource = null;
                cbxtoss.Items.Clear();

                cbxstriker.ItemsSource = null;
                cbxstriker.Items.Clear();

                cbxnonstriker.ItemsSource = null;
                cbxnonstriker.Items.Clear();

                cbxbowler.ItemsSource = null;
                cbxbowler.Items.Clear();

                cbxumpire1.ItemsSource = null;
                cbxumpire1.Items.Clear();

                cbxumpire2.ItemsSource = null;
                cbxumpire2.Items.Clear();

                cbxscorer.ItemsSource = null;
                cbxscorer.Items.Clear();

                cbxVenue.ItemsSource = null;
                cbxVenue.Items.Clear();




                ObservableCollection<ListBoxSave> lstlistbox = Database.GetEntityList<ListBoxSave>(false, false, false, oleconn, "RecordStatus", "ListBoxSaveId");
                var objvalue = from s in lstlistbox where s.MatchId == selectedgridvalue.matchid && s.DivisionId.ToString() == selectedgridvalue.LoadDivisionId && s.SeasonId.ToString() == TempValues.LoadSeasonId && s.TeamOneName == tempvalues.teamA && s.TeamtwoName == tempvalues.teamB select s;
                List<ListBoxSave> lstfilter = objvalue.OrderBy(p => p.MatchId).ThenBy(p => p.CreatedDatetime.Millisecond).ToList<ListBoxSave>();




                if (lstfilter.Count > 0)
                {
                    lbxTeam1.ItemsSource = null;
                    lbxTeam1.Items.Clear();

                    lbxTeam2.ItemsSource = null;
                    lbxTeam2.Items.Clear();

                    lbxTeam1.ItemsSource = null;
                    lbxTeam1.Items.Clear();
                    
                    lbxTeam2.ItemsSource = null;
                    lbxTeam2.Items.Clear();
                    
                    lbltransferedTeamA.ItemsSource = null;
                    lbltransferedTeamA.Items.Clear();
                    
                    lbltransferedTeamB.ItemsSource = null;
                    lbltransferedTeamB.Items.Clear();

                    cbxstriker.ItemsSource = null;
                    cbxstriker.Items.Clear();

                    cbxnonstriker.ItemsSource = null;
                    cbxnonstriker.Items.Clear();

                    cbxbowler.ItemsSource = null;
                    cbxbowler.Items.Clear();

                    foreach (var objLoad in lstfilter)
                    {
                        // lbxTeamLoad.IsEnabled = false;
                        if (objLoad.TeamOne != null)
                        {
                            if (objLoad.MatchId == selectedgridvalue.matchid)
                            {
                                lbxTeam1.Items.Add(objLoad.TeamOne);

                            }
                        }


                        if (objLoad.TeamTwo != null)
                        {
                            if (objLoad.SeasonId.ToString() == TempValues.LoadSeasonId)
                            {
                                lbxTeam2.Items.Add(objLoad.TeamTwo);
                            }
                        }

                        if (objLoad.TransferedTeamOne != null)
                        {
                            if (objLoad.SeasonId.ToString() == TempValues.LoadSeasonId)
                            {
                                lbltransferedTeamA.Items.Add(objLoad.TransferedTeamOne);
                            }
                        }

                        if (objLoad.TransferedTeamTwo != null)
                        {
                            if (objLoad.SeasonId.ToString() == TempValues.LoadSeasonId)
                            {
                                lbltransferedTeamB.Items.Add(objLoad.TransferedTeamTwo);
                            }
                        }


                        //clearing values

                        cbxteam1captain.IsEnabled = false;
                        cbxteam2captain.IsEnabled = false;
                        cbxteam1keeper.IsEnabled = false;
                        cbxteam2keeper.IsEnabled = false;
                        cbxtoss.IsEnabled = false;
                        cbxelected.IsEnabled = false;

                        cbxstriker.IsEnabled = false;
                        cbxnonstriker.IsEnabled = false;
                        cbxbowler.IsEnabled = false;

                        startDate.IsEnabled = false;
                        EndDate.IsEnabled = false;
                        cbxumpire1.IsEnabled = false;
                        cbxumpire2.IsEnabled = false;
                        cbxscorer.IsEnabled = false;
                        cbxVenue.IsEnabled = false;
                        cbxmatchtype.IsEnabled = false;
                        txtTeam1.IsEnabled = false;
                        txtTeam2.IsEnabled = false;
                        startDate.IsEnabled = false;
                        EndDate.IsEnabled = false;
                        btnUp.IsEnabled = false;
                        btnDown.IsEnabled = false;
                        btnDown2.IsEnabled = false;
                        btnUp2.IsEnabled = false;
                        btnLoadTeam1.IsEnabled = false;
                        btnLoadTeam2.IsEnabled = false;
                        btnTransferedTeamA.IsEnabled = false;
                        btnTransferedTeamB.IsEnabled = false;
                        btnReverseTeamA.IsEnabled = false;
                        btnReverseTeamB.IsEnabled = false;
                        cbxumpire1.ItemsSource = null;
                        cbxumpire2.ItemsSource = null;
                        cbxscorer.ItemsSource = null;
                        cbxVenue.ItemsSource = null;
                        startDate.Text = string.Empty;
                        EndDate.Text = string.Empty;
                        txtTeam1.Text = string.Empty;
                        txtTeam2.Text = string.Empty;
                        lblseason.Content = null;
                        lblzone.Content = null;
                        lbldivision.Content = null;

                        //add values

                        startDate.Text = objLoad.StartDate;
                        EndDate.Text = objLoad.EndDate;
                        cbxteam1captain.Items.Add(objLoad.TeamOneCaptain);
                        cbxteam1captain.SelectedIndex = 0;
                        cbxteam2captain.Items.Add(objLoad.TeamTwoCaptain);
                        cbxteam2captain.SelectedIndex = 0;
                        cbxteam1keeper.Items.Add(objLoad.TeamOneWk);
                        cbxteam1keeper.SelectedIndex = 0;
                        cbxteam2keeper.Items.Add(objLoad.TeamTwoWk);
                        cbxteam2keeper.SelectedIndex = 0;

                        cbxstriker.Items.Add(objLoad.Striker);
                        cbxstriker.SelectedIndex = 0;
                        cbxnonstriker.Items.Add(objLoad.NonStriker);
                        cbxnonstriker.SelectedIndex = 0;
                        cbxbowler.Items.Add(objLoad.Bowler);
                        cbxbowler.SelectedIndex = 0;


                        if (objLoad.TossWonIndex == 0)
                        {
                            cbxtoss.Items.Add(objLoad.TossWon);

                        }
                        else
                        {
                            cbxtoss.Items.Add("DUMMY");
                            cbxtoss.Items.Add(objLoad.TossWon);
                        }
                        cbxtoss.SelectedIndex = objLoad.TossWonIndex;

                        if (objLoad.ElectedToIndex == 0)
                        {
                            cbxelected.Items.Add(objLoad.ElectedTo);

                        }
                        else
                        {
                            cbxelected.Items.Add("DUMMY");
                            cbxelected.Items.Add(objLoad.ElectedTo);

                        }

                        cbxelected.SelectedIndex = objLoad.ElectedToIndex;


                        cbxumpire1.Items.Add(objLoad.UmpireOne);
                        cbxumpire1.SelectedIndex = 0;
                        cbxumpire2.Items.Add(objLoad.UmpireTwo);
                        cbxumpire2.SelectedIndex = 0;
                        cbxscorer.Items.Add(objLoad.Scorer);
                        cbxscorer.SelectedIndex = 0;


                        cbxVenue.Items.Add(objLoad.Venue);
                        cbxVenue.SelectedIndex = 0;


                        cbxmatchtype.Items.Add(objLoad.MatchType);
                        cbxmatchtype.SelectedIndex = 0;
                        txtTeam1.Text = tempvalues.teamA;
                        txtTeam2.Text = tempvalues.teamB;
                        lblseason.Content = season_zone_div.name[0].ToString();
                        lblzone.Content = season_zone_div.name[1].ToString();
                        lbldivision.Content = season_zone_div.name[2].ToString();

                    }
                }


                else
                {

                    //assigning values

                    txtTeam1.Text = tempvalues.teamA;
                    txtTeam2.Text = tempvalues.teamB;


                    if (selectedgridvalue.group == "Semi-Final")
                    {
                        cbxtoss.Items.Clear();
                        chksemifinalfinals1.IsEnabled = true;

                    }

                    else if (selectedgridvalue.group == "Final")
                    {
                        cbxtoss.Items.Clear();
                        chksemifinalfinals1.IsEnabled = true;

                    }

                    else
                    {
                        chksemifinalfinals1.IsEnabled = false;
                        cbxtoss.Items.Add(tempvalues.teamA);
                        cbxtoss.Items.Add(tempvalues.teamB);
                    }

                    cbxmatchtype.Items.Add("League");
                    cbxmatchtype.Items.Add("Semi Final");
                    cbxmatchtype.Items.Add("Final");

                    cbxelected.Items.Add("BAT");
                    cbxelected.Items.Add("BOWL");
                    lblseason.Content = season_zone_div.name[0].ToString();
                    lblzone.Content = season_zone_div.name[1].ToString();
                    lbldivision.Content = season_zone_div.name[2].ToString();

                }
                lstlistbox.Clear();


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void cbxVenue_DropDownOpened(object sender, EventArgs e)
        {
            try
            {
                string strRetrieve = "";
                strRetrieve = "select *from locations";

                OleDbDataAdapter adpt = new OleDbDataAdapter(strRetrieve, oleconn);

                DataSet ds = new DataSet();
                adpt.Fill(ds);
                cbxVenue.ItemsSource = ds.Tables[0].DefaultView;
                cbxVenue.DisplayMemberPath = ds.Tables[0].Columns["StadiumName"].ToString();
                cbxVenue.SelectedValuePath = ds.Tables[0].Columns["LocationId"].ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void cbxumpire1_DropDownOpened(object sender, EventArgs e)
        {
            ObservableCollection<Official> officialname = Database.GetEntityList<Official>(false, false, false, oleconn, "RecordStatus", "OfficialName");

            //u-2 not selected && cbxscorer not selected
            if (cbxumpire2.SelectedIndex == -1 && cbxscorer.SelectedIndex == -1)
            {
                cbxumpire1.Items.Clear();
                cbxumpire2.Items.Clear();
                cbxscorer.Items.Clear();
                list1.Clear();
                cbxumpire1.SelectedIndex = -1;
                cbxumpire2.SelectedIndex = -1;
                cbxscorer.SelectedIndex = -1;


                foreach (var ofcname in officialname)
                {
                    list1.Add(ofcname.OfficialName);
                }


                for (int i = 0; i <= (list1.Count - 1); i++)
                {
                    cbxumpire1.Items.Add(list1[i]);
                }
            }

                //u-2 selected && cbxscorer not selected
            else if (cbxumpire2.SelectedIndex != -1 && cbxscorer.SelectedIndex == -1)
            {
                cbxumpire1.Items.Clear();
                cbxscorer.Items.Clear();
                list1.Clear();
                cbxumpire1.SelectedIndex = -1;
                cbxscorer.SelectedIndex = -1;

                string abc = cbxumpire2.SelectedItem.ToString();

                foreach (var ofcname in officialname)
                {
                    list1.Add(ofcname.OfficialName);
                }

                for (int i = 0; i <= (list1.Count - 1); i++)
                {
                    if (list1[i] != abc)
                    {
                        cbxumpire1.Items.Add(list1[i]);
                    }
                }
            }
            // cbxscorer selected && u-2 not selected
            else if (cbxscorer.SelectedIndex != -1 && cbxumpire2.SelectedIndex == -1)
            {
                cbxumpire1.Items.Clear();
                cbxumpire2.Items.Clear();
                list1.Clear();
                cbxumpire1.SelectedIndex = -1;
                cbxumpire2.SelectedIndex = -1;

                string abc = cbxscorer.SelectedItem.ToString();

                foreach (var ofcname in officialname)
                {
                    list1.Add(ofcname.OfficialName);
                }

                for (int i = 0; i <= (list1.Count - 1); i++)
                {
                    if (list1[i] != abc)
                    {
                        cbxumpire1.Items.Add(list1[i]);
                    }
                }
            }
            //u-2 selected && cbxscorer selected
            else if (cbxumpire2.SelectedIndex != -1 && cbxscorer.SelectedIndex != -1)
            {
                cbxumpire1.Items.Clear();
                list1.Clear();
                cbxumpire1.SelectedIndex = -1;
                string abc = cbxumpire2.SelectedItem.ToString();
                string def = cbxscorer.SelectedItem.ToString();

                foreach (var ofcname in officialname)
                {
                    list1.Add(ofcname.OfficialName);
                }

                for (int i = 0; i <= (list1.Count - 1); i++)
                {
                    if (list1[i] != abc && list1[i] != def)
                    {
                        cbxumpire1.Items.Add(list1[i]);
                    }
                }
            }


        }

        private void cbxumpire2_DropDownOpened(object sender, EventArgs e)
        {
            ObservableCollection<Official> officialname = Database.GetEntityList<Official>(false, false, false, oleconn, "RecordStatus", "OfficialName");

            //u-1 not selected && cbxscorer not selected
            if (cbxumpire1.SelectedIndex == -1 && cbxscorer.SelectedIndex == -1)
            {
                cbxumpire1.Items.Clear();
                cbxumpire2.Items.Clear();
                cbxscorer.Items.Clear();
                list1.Clear();
                cbxumpire1.SelectedIndex = -1;
                cbxumpire2.SelectedIndex = -1;
                cbxscorer.SelectedIndex = -1;


                foreach (var ofcname in officialname)
                {
                    list1.Add(ofcname.OfficialName);
                }


                for (int i = 0; i <= (list1.Count - 1); i++)
                {
                    cbxumpire2.Items.Add(list1[i]);
                }
            }

                //u-1 selected && cbxscorer not selected
            else if (cbxumpire1.SelectedIndex != -1 && cbxscorer.SelectedIndex == -1)
            {
                cbxumpire2.Items.Clear();
                cbxscorer.Items.Clear();
                list1.Clear();
                cbxumpire2.SelectedIndex = -1;
                cbxscorer.SelectedIndex = -1;

                string abc = cbxumpire1.SelectedItem.ToString();

                foreach (var ofcname in officialname)
                {
                    list1.Add(ofcname.OfficialName);
                }

                for (int i = 0; i <= (list1.Count - 1); i++)
                {
                    if (list1[i] != abc)
                    {
                        cbxumpire2.Items.Add(list1[i]);
                    }
                }
            }
            // cbxscorer selected && u-1 not selected
            else if (cbxscorer.SelectedIndex != -1 && cbxumpire1.SelectedIndex == -1)
            {
                cbxumpire1.Items.Clear();
                cbxumpire2.Items.Clear();
                list1.Clear();
                cbxumpire1.SelectedIndex = -1;
                cbxumpire2.SelectedIndex = -1;

                string abc = cbxscorer.SelectedItem.ToString();

                foreach (var ofcname in officialname)
                {
                    list1.Add(ofcname.OfficialName);
                }

                for (int i = 0; i <= (list1.Count - 1); i++)
                {
                    if (list1[i] != abc)
                    {
                        cbxumpire2.Items.Add(list1[i]);
                    }
                }
            }
            //u-1 selected && cbxscorer selected
            else if (cbxumpire1.SelectedIndex != -1 && cbxscorer.SelectedIndex != -1)
            {
                cbxumpire2.Items.Clear();
                list1.Clear();
                cbxumpire2.SelectedIndex = -1;
                string abc = cbxumpire1.SelectedItem.ToString();
                string def = cbxscorer.SelectedItem.ToString();

                foreach (var ofcname in officialname)
                {
                    list1.Add(ofcname.OfficialName);
                }

                for (int i = 0; i <= (list1.Count - 1); i++)
                {
                    if (list1[i] != abc && list1[i] != def)
                    {
                        cbxumpire2.Items.Add(list1[i]);
                    }
                }
            }

            //try
            //{
            //    string strRetrieve = "";
            //    strRetrieve = "select officialname,officialid from official";

            //    OleDbDataAdapter adpt = new OleDbDataAdapter(strRetrieve, oleconn);

            //    DataSet ds = new DataSet();
            //    adpt.Fill(ds);
            //    cbxumpire2.ItemsSource = ds.Tables[0].DefaultView;
            //    cbxumpire2.DisplayMemberPath = ds.Tables[0].Columns["OfficialName"].ToString();
            //    cbxumpire2.SelectedValuePath = ds.Tables[0].Columns["OfficialId"].ToString();
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }

        private void cbxscorer_DropDownOpened(object sender, EventArgs e)
        {
            ObservableCollection<Official> officialname = Database.GetEntityList<Official>(false, false, false, oleconn, "RecordStatus", "OfficialName");

            //u-2 not selected && u-1 not selected
            if (cbxumpire2.SelectedIndex == -1 && cbxumpire1.SelectedIndex == -1)
            {
                cbxumpire1.Items.Clear();
                cbxumpire2.Items.Clear();
                cbxscorer.Items.Clear();
                list1.Clear();
                cbxumpire1.SelectedIndex = -1;
                cbxumpire2.SelectedIndex = -1;
                cbxscorer.SelectedIndex = -1;


                foreach (var ofcname in officialname)
                {
                    list1.Add(ofcname.OfficialName);
                }


                for (int i = 0; i <= (list1.Count - 1); i++)
                {
                    cbxscorer.Items.Add(list1[i]);
                }
            }

                //u-2 selected  && u-1 not selected
            else if (cbxumpire2.SelectedIndex != -1 && cbxumpire1.SelectedIndex == -1)
            {
                cbxumpire1.Items.Clear();
                cbxscorer.Items.Clear();
                list1.Clear();
                cbxumpire1.SelectedIndex = -1;
                cbxscorer.SelectedIndex = -1;

                string abc = cbxumpire2.SelectedItem.ToString();

                foreach (var ofcname in officialname)
                {
                    list1.Add(ofcname.OfficialName);
                }

                for (int i = 0; i <= (list1.Count - 1); i++)
                {
                    if (list1[i] != abc)
                    {
                        cbxscorer.Items.Add(list1[i]);
                    }
                }
            }
            // u-1 selected && u-2 not selected 
            else if (cbxumpire1.SelectedIndex != -1 && cbxumpire2.SelectedIndex == -1)
            {
                cbxscorer.Items.Clear();
                cbxumpire2.Items.Clear();
                list1.Clear();
                cbxscorer.SelectedIndex = -1;
                cbxumpire2.SelectedIndex = -1;

                string abc = cbxumpire1.SelectedItem.ToString();

                foreach (var ofcname in officialname)
                {
                    list1.Add(ofcname.OfficialName);
                }

                for (int i = 0; i <= (list1.Count - 1); i++)
                {
                    if (list1[i] != abc)
                    {
                        cbxscorer.Items.Add(list1[i]);
                    }
                }
            }
            //u-2 selected && u-1 selected
            else if (cbxumpire2.SelectedIndex != -1 && cbxumpire1.SelectedIndex != -1)
            {
                cbxscorer.Items.Clear();
                list1.Clear();
                cbxscorer.SelectedIndex = -1;
                string abc = cbxumpire2.SelectedItem.ToString();
                string def = cbxumpire1.SelectedItem.ToString();

                foreach (var ofcname in officialname)
                {
                    list1.Add(ofcname.OfficialName);
                }

                for (int i = 0; i <= (list1.Count - 1); i++)
                {
                    if (list1[i] != abc && list1[i] != def)
                    {
                        cbxscorer.Items.Add(list1[i]);
                    }
                }
            }
        }

      
        private void btnumpire_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            btn.Command = NavigationCommands.GoToPage;
            btn.CommandParameter = "/View/AssignOfficialsToGrid.xaml";
        }

        private void btnback_Click_1(object sender, RoutedEventArgs e)
        {
            lbxTeam1.ItemsSource = null;
            lbxTeam1.Items.Clear();

            lbxTeam2.ItemsSource = null;
            lbxTeam2.Items.Clear();

            lbltransferedTeamA.ItemsSource = null;
            lbltransferedTeamA.Items.Clear();

            lbltransferedTeamB.ItemsSource = null;
            lbltransferedTeamB.Items.Clear();

            cbxteam1captain.ItemsSource = null;
            cbxteam1captain.Items.Clear();

            cbxteam2captain.ItemsSource = null;
            cbxteam2captain.Items.Clear();

            cbxteam1keeper.ItemsSource = null;
            cbxteam1keeper.Items.Clear();

            cbxteam2keeper.ItemsSource = null;
            cbxteam2keeper.Items.Clear();

            cbxelected.ItemsSource = null;
            cbxelected.Items.Clear();

            cbxtoss.ItemsSource = null;
            cbxtoss.Items.Clear();

            cbxstriker.ItemsSource = null;
            cbxstriker.Items.Clear();

            cbxnonstriker.ItemsSource = null;
            cbxnonstriker.Items.Clear();

            cbxbowler.ItemsSource = null;
            cbxbowler.Items.Clear();

            cbxumpire1.ItemsSource = null;
            cbxumpire1.Items.Clear();

            cbxumpire2.ItemsSource = null;
            cbxumpire2.Items.Clear();

            cbxscorer.ItemsSource = null;
            cbxscorer.Items.Clear();

            cbxVenue.ItemsSource = null;
            cbxVenue.Items.Clear();                                                                                            //////////////


            Button btn = (Button)sender;
            btn.Command = NavigationCommands.GoToPage;
            btn.CommandParameter = "/View/formFixtures.xaml";
        }

        private void chksemifinalfinals1_Checked(object sender, RoutedEventArgs e)
        {
            cbxteam1.Visibility = Visibility.Visible;
            cbxtteam2.Visibility = Visibility.Visible;
            cbxtoss.Items.Clear();
            txtTeam1.Clear();
            txtTeam2.Clear();

            lbxTeam1.ItemsSource = null;
            lbxTeam2.ItemsSource = null;
            lbxTeam1.Items.Clear();
            lbxTeam2.Items.Clear();
            lbxTeam1.ItemsSource = null;
            lbxTeam1.Items.Clear();
            lbxTeam2.ItemsSource = null;
            lbxTeam2.Items.Clear();
            lbltransferedTeamA.ItemsSource = null;
            lbltransferedTeamA.Items.Clear();
            lbltransferedTeamB.ItemsSource = null;
            lbltransferedTeamB.Items.Clear();
            cbxteam1captain.ItemsSource = null;
            cbxteam2captain.ItemsSource = null;
            cbxteam1keeper.ItemsSource = null;
            cbxteam2keeper.ItemsSource = null;
            cbxelected.ItemsSource = null;
            cbxtoss.ItemsSource = null;

            //cbxstriker.ItemsSource = null;
            //cbxnonstriker.ItemsSource = null;
            //cbxbowler.ItemsSource = null;

            cbxumpire1.ItemsSource = null;
            cbxumpire2.ItemsSource = null;
            cbxscorer.ItemsSource = null;
            cbxVenue.ItemsSource = null;
            cbxmatchtype.Items.Clear();

            cbxteam1captain.Items.Clear();
            cbxteam2captain.Items.Clear();
            cbxteam1keeper.Items.Clear();
            cbxteam2keeper.Items.Clear();
            cbxelected.Items.Clear();
            cbxtoss.Items.Clear();
            cbxumpire1.Items.Clear();
            cbxumpire2.Items.Clear();
            cbxscorer.Items.Clear();
            cbxVenue.Items.Clear();
            cbxelected.Items.Clear();

            //cbxstriker.Items.Clear();
            //cbxnonstriker.Items.Clear();
            //cbxbowler.Items.Clear();

            //isenabled

            lbxTeam1.IsEnabled = true;
            lbxTeam2.IsEnabled = true;
            lbltransferedTeamA.IsEnabled = true;
            lbltransferedTeamB.IsEnabled = true;
            cbxteam1captain.IsEnabled = true;
            cbxteam2captain.IsEnabled = true;
            cbxteam1keeper.IsEnabled = true;
            cbxteam2keeper.IsEnabled = true;
            cbxtoss.IsEnabled = true;
            cbxelected.IsEnabled = true;

            //cbxstriker.IsEnabled = true;
            //cbxnonstriker.IsEnabled = true;
            //cbxbowler.IsEnabled = true;

            startDate.IsEnabled = true;
            EndDate.IsEnabled = true;
            cbxumpire1.IsEnabled = true;
            cbxumpire2.IsEnabled = true;
            cbxscorer.IsEnabled = true;
            cbxVenue.IsEnabled = true;
            cbxmatchtype.IsEnabled = true;
            txtTeam1.IsEnabled = true;
            txtTeam2.IsEnabled = true;
            startDate.IsEnabled = true;
            EndDate.IsEnabled = true;
            btnUp.IsEnabled = true;
            btnDown.IsEnabled = true;
            btnDown2.IsEnabled = true;
            btnUp2.IsEnabled = true;
            btnLoadTeam1.IsEnabled = true;
            btnLoadTeam2.IsEnabled = true;
            btnTransferedTeamA.IsEnabled = true;
            btnTransferedTeamB.IsEnabled = true;
            btnReverseTeamA.IsEnabled = true;
            btnReverseTeamB.IsEnabled = true;
            cbxmatchtype.Items.Add("League");
            cbxmatchtype.Items.Add("Semi Final");
            cbxmatchtype.Items.Add("Final");

            cbxelected.Items.Add("BAT");
            cbxelected.Items.Add("BOWL");
        }

        private void cbxteam1_DropDownOpened(object sender, EventArgs e)
        {

            cbxteam1.Items.Clear();
            lbxTeam1.Items.Clear();
            string team2selected = cbxtteam2.Text;
            try
            {
                cbxteam1.Items.Clear();
                foreach (var team1 in lstteams)
                {
                    if (team1.SeasonId.ToString() == selectedgridvalue._seasonid)
                    {
                        if (selectedgridvalue.LoadDivision == "1st Division")
                        {

                            if (!(team1.DivisionOne == team2selected))
                            {
                                if (team1.DivisionOne != null)
                                {
                                    cbxteam1.Items.Add(team1.DivisionOne);
                                }
                            }
                        }
                        else if (selectedgridvalue.LoadDivision == "2nd Division")
                        {
                            if (!(team1.DivisionTwo == team2selected))
                            {
                                if (team1.DivisionTwo != null)
                                {
                                    cbxteam1.Items.Add(team1.DivisionTwo);
                                }
                            }
                        }
                        else if (selectedgridvalue.LoadDivision == "3rd Division")
                        {
                            if (!(team1.DivisionThree == team2selected))
                            {
                                if (team1.DivisionThree != null)
                                {
                                    cbxteam1.Items.Add(team1.DivisionThree);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void cbxtteam2_DropDownOpened(object sender, EventArgs e)
        {

            cbxtteam2.Items.Clear();
            lbxTeam2.Items.Clear();
            string team1selected = cbxteam1.Text;

            try
            {
                cbxtteam2.Items.Clear();
                foreach (var team2 in lstteams)
                {
                    if (team2.SeasonId.ToString() == selectedgridvalue._seasonid)
                    {
                        if (selectedgridvalue.LoadDivision == "1st Division")
                        {
                            if (!(team2.DivisionOne == team1selected))
                            {
                                if (team2.DivisionOne != null)
                                {
                                    cbxtteam2.Items.Add(team2.DivisionOne);
                                }
                            }
                        }

                        else if (selectedgridvalue.LoadDivision == "2nd Division")
                        {
                            if (!(team2.DivisionTwo == team1selected))
                            {
                                if (team2.DivisionTwo != null)
                                {
                                    cbxtteam2.Items.Add(team2.DivisionTwo);
                                }
                            }
                        }

                        else if (selectedgridvalue.LoadDivision == "3rd Division")
                        {
                            if (!(team2.DivisionThree == team1selected))
                            {
                                if (team2.DivisionThree != null)
                                {
                                    cbxtteam2.Items.Add(team2.DivisionThree);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void cbxteam1_DropDownClosed(object sender, EventArgs e)
        {
            txtTeam1.Clear();

            txtTeam1.Text = cbxteam1.Text;
            if (txtTeam1.Text == "")
            {
                cbxtoss.Items.Remove(txtTeam1.Text);

            }
            else
            {

                cbxtoss.Items.Add(txtTeam1.Text);
                tempvalues.teamA = txtTeam1.Text;

            }
        }

        private void cbxtteam2_DropDownClosed(object sender, EventArgs e)
        {
            txtTeam2.Clear();

            txtTeam2.Text = cbxtteam2.Text;
            if (txtTeam2.Text == "")
            {
                cbxtoss.Items.Remove(txtTeam2.Text);

            }
            else
            {
                cbxtoss.Items.Add(txtTeam2.Text);
                tempvalues.teamB = txtTeam2.Text;
            }
        }

        private void chksemifinalfinals1_Unchecked(object sender, RoutedEventArgs e)
        {
            cbxteam1.Visibility = Visibility.Hidden;
            cbxtteam2.Visibility = Visibility.Hidden;
            lbxTeam1.ItemsSource = null;
            lbxTeam2.ItemsSource = null;
            lbltransferedTeamA.ItemsSource = null;
            lbltransferedTeamB.ItemsSource = null;

        }

        private void BallByBallScoring_Click(object sender, RoutedEventArgs e)
        {
            KSCAUID.teamA = new string[60];
            KSCAUID.teamB = new string[60];
            KSCAUID.countA = 0;
            KSCAUID.countB = 0;

            try
            {
                if (lbltransferedTeamA.Items.Count != 0)
                {
                    if (lbltransferedTeamB.Items.Count != 0)
                    {
                        if (cbxteam1captain.SelectedIndex != -1)
                        {
                            if (cbxteam1keeper.SelectedIndex != -1)
                            {
                                if (cbxteam2captain.SelectedIndex != -1)
                                {
                                    if (cbxteam2keeper.SelectedIndex != -1)
                                    {
                                        if (cbxtoss.SelectedIndex != -1)
                                        {
                                            if (cbxelected.SelectedIndex != -1)
                                            {
                                                if(cbxstriker.SelectedIndex !=-1)
                                                {
                                                    if(cbxnonstriker.SelectedIndex !=-1)
                                                    {
                                                        if(cbxbowler.SelectedIndex != -1)
                                                        {
                                                            if (cbxumpire1.SelectedIndex != -1)
                                                            {
                                                                if (cbxumpire2.SelectedIndex != -1)
                                                                {
                                                                    if (cbxscorer.SelectedIndex != -1)
                                                                    {
                                                                        if (cbxVenue.SelectedIndex != -1)
                                                                        {
                                                                            if (startDate.SelectedDate != null)
                                                                            {
                                                                                if (EndDate.SelectedDate != null)
                                                                                {
                                                                                    if (cbxmatchtype.SelectedItem != null)
                                                                                    {
                                                                                        if (EndDate.SelectedDate.Value > startDate.SelectedDate.Value)
                                                                                        {
                                                                                            TempValuesTeam1.team1 = new string[60];
                                                                                            TempValuesTeam2.team2 = new string[60];
                                                                                            TempValuesTeam1.index1 = 0;
                                                                                            TempValuesTeam2.index2 = 0;
                                                                                            comboboxvalue.value = new string[60];
                                                                                            comboboxvalue.count = 0;
                                                                                            foreach (var items1 in lbltransferedTeamA.Items)
                                                                                            {
                                                                                                TempValuesTeam1.team1[TempValuesTeam1.index1] = items1.ToString();
                                                                                                TempValuesTeam1.index1++;
                                                                                            }



                                                                                            foreach (var items2 in lbltransferedTeamB.Items)
                                                                                            {
                                                                                                TempValuesTeam2.team2[TempValuesTeam2.index2] = items2.ToString();
                                                                                                TempValuesTeam2.index2++;
                                                                                            }

                                                                                            if (cbxtoss.SelectedIndex == 0)
                                                                                            {
                                                                                                comboboxvalue.value[comboboxvalue.count] = "0";
                                                                                                comboboxvalue.count++;
                                                                                                tosswon.index = 0;

                                                                                            }
                                                                                            if (cbxtoss.SelectedIndex == 1)
                                                                                            {
                                                                                                comboboxvalue.value[comboboxvalue.count] = "1";
                                                                                                comboboxvalue.count++;
                                                                                                tosswon.index = 1;
                                                                                            }

                                                                                            if (cbxelected.SelectedIndex == 0)
                                                                                            {
                                                                                                comboboxvalue.value[comboboxvalue.count] = "0";
                                                                                                comboboxvalue.count++;
                                                                                                electedto.index = 0;
                                                                                            }
                                                                                            if (cbxelected.SelectedIndex == 1)
                                                                                            {
                                                                                                comboboxvalue.value[comboboxvalue.count] = "1";
                                                                                                comboboxvalue.count++;
                                                                                                electedto.index = 1;
                                                                                            }

                                                                                            {
                                                                                                string sql = "select firstname,playerid,Teamid,kscauid from players";
                                                                                                OleDbDataAdapter adpt = new OleDbDataAdapter(sql, oleconn);
                                                                                                adpt.Fill(dtlItemDetails);

                                                                                                string sql2 = "select * from teams";
                                                                                                OleDbDataAdapter adpt1 = new OleDbDataAdapter(sql2, oleconn);
                                                                                                adpt1.Fill(dtlItemDetails5);




                                                                                                foreach (DataRowView objPlayer in dtlItemDetails.DefaultView)
                                                                                                {
                                                                                                    foreach (DataRowView objTeam in dtlItemDetails5.DefaultView)
                                                                                                    {

                                                                                                        for (int p = 0; p <= (lbltransferedTeamA.Items.Count - 1); p++)
                                                                                                        {
                                                                                                            if (lbltransferedTeamA.Items[p].ToString() == (objPlayer["FirstName"].ToString()))
                                                                                                            {
                                                                                                                if (txtTeam1.Text == objTeam["TeamName"].ToString())
                                                                                                                {
                                                                                                                    if (objTeam["TeamId"].ToString() == objPlayer["TeamId"].ToString())
                                                                                                                    {
                                                                                                                        KSCAUID.teamA[p] = (objPlayer["KSCAUID"].ToString());
                                                                                                                        p++;
                                                                                                                    }
                                                                                                                }

                                                                                                            }
                                                                                                        }



                                                                                                        //KSCAUID

                                                                                                        for (int q = 0; q <= (lbltransferedTeamB.Items.Count - 1); q++)
                                                                                                        {
                                                                                                            if (lbltransferedTeamB.Items[q].ToString() == (objPlayer["FirstName"].ToString()))
                                                                                                            {
                                                                                                                if (txtTeam2.Text == objTeam["TeamName"].ToString())
                                                                                                                {
                                                                                                                    if (objTeam["TeamId"].ToString() == objPlayer["TeamId"].ToString())
                                                                                                                    {
                                                                                                                        KSCAUID.teamB[q] = (objPlayer["KSCAUID"].ToString());
                                                                                                                        q++;
                                                                                                                    }
                                                                                                                }

                                                                                                            }

                                                                                                        }
                                                                                                    }

                                                                                                }




                                                                                                captainkeeperselect();

                                                                                                player.striker = cbxstriker.SelectedValue.ToString();
                                                                                                player.striker_index = cbxstriker.SelectedIndex;

                                                                                                player.nonstriker = cbxnonstriker.SelectedValue.ToString();
                                                                                                player.nonstriker_index = cbxnonstriker.SelectedIndex;

                                                                                                player.bowler = cbxbowler.SelectedValue.ToString();
                                                                                                player.bowler_index = cbxbowler.SelectedIndex;

                                                                                                string bat1 = cbxstriker.SelectedItem.ToString();
                                                                                                string bat2 = cbxnonstriker.SelectedItem.ToString();
                                                                                                string bowl = cbxbowler.SelectedItem.ToString();

                                                                                                player.No_Of_Players = lbltransferedTeamA.Items.Count;

                                                                                                for (int q = 0; q <= player.No_Of_Players; q++)
                                                                                                {
                                                                                                    Batman.names = new string[q];
                                                                                                    Bowler.names = new string[q];
                                                                                                }

                                                                                                Batman.names[0] = bat1;
                                                                                                Batman.names[1] =  bat2;
                                                                                                Batman.count += 2;
                                                                                                Bowler.names[0] =  bowl;
                                                                                                Bowler.count += 1;

                                                                                                if ((cbxtoss.SelectedIndex == 0 && cbxelected.SelectedIndex == 0) || (cbxtoss.SelectedIndex == 1 && cbxelected.SelectedIndex == 1))
                                                                                                {
                                                                                                    for (int i = 2; i < player.No_Of_Players; i++)
                                                                                                    {
                                                                                                        foreach (var item1 in lbltransferedTeamA.Items)
                                                                                                        {
                                                                                                            if (item1.ToString() != bat1 && item1.ToString() != bat2)
                                                                                                            {
                                                                                                                string abc = Convert.ToString(item1);
                                                                                                                Batman.names[i] = abc;
                                                                                                                Batman.count++;
                                                                                                                i++;
                                                                                                            }
                                                                                                        }
                                                                                                    }

                                                                                                    for (int i = 1; i < player.No_Of_Players; i++)
                                                                                                    {
                                                                                                        foreach (var item2 in lbltransferedTeamB.Items)
                                                                                                        {
                                                                                                            if (item2.ToString() != bowl)
                                                                                                            {
                                                                                                                string xyz = Convert.ToString(item2);
                                                                                                                Bowler.names[i] = xyz;
                                                                                                                Bowler.count++;
                                                                                                                i++;
                                                                                                            }
                                                                                                        }
                                                                                                    }
                                                                                                }
                                                                                                else
                                                                                                {
                                                                                                    for (int i = 2; i < player.No_Of_Players; i++)
                                                                                                    {
                                                                                                        foreach (var item1 in lbltransferedTeamB.Items)
                                                                                                        {
                                                                                                            if (item1.ToString() != bat1 && item1.ToString() != bat2)
                                                                                                            {
                                                                                                                string abc = Convert.ToString(item1);
                                                                                                                Batman.names[i] = abc;
                                                                                                                Batman.count++;
                                                                                                                i++;
                                                                                                            }
                                                                                                        }
                                                                                                    }

                                                                                                    for (int i = 1; i < player.No_Of_Players; i++)
                                                                                                    {
                                                                                                        foreach (var item2 in lbltransferedTeamA.Items)
                                                                                                        {
                                                                                                            if (item2.ToString() != bowl)
                                                                                                            {
                                                                                                                string xyz = Convert.ToString(item2);
                                                                                                                Bowler.names[i] = xyz;
                                                                                                                Bowler.count++;
                                                                                                                i++;
                                                                                                            }
                                                                                                        }
                                                                                                    }
                                                                                                }

                                                                                                //save

                                                                                                Season objSeason = Database.GetEntity<Season>(Guid.Parse(TempValues.LoadSeasonId), false, false, false, oleconn);
                                                                                                Division objDivision = Database.GetEntity<Division>(Guid.Parse(selectedgridvalue.LoadDivisionId), false, false, false, oleconn);

                                                                                                ObservableCollection<ListBoxSave> lstlist = Database.GetEntityList<ListBoxSave>(false, false, false, oleconn, "RecordStatus", "ListBoxSaveId");
                                                                                                var objlist = from s in lstlist where s.MatchId == selectedgridvalue.matchid && s.SeasonId.ToString() == TempValues.LoadSeasonId && s.DivisionId.ToString() == selectedgridvalue.LoadDivisionId select s;
                                                                                                List<ListBoxSave> lstfilterlist = objlist.ToList<ListBoxSave>();

                                                                                                if (lstfilterlist.Count > 0)
                                                                                                {
                                                                                                    if (division.num == 1)
                                                                                                    {
                                                                                                        Button btn = (Button)sender;
                                                                                                        btn.Command = NavigationCommands.GoToPage;
                                                                                                        btn.CommandParameter = "/View/MainScoreCard1st.xaml";


                                                                                                        //////////

                                                                                                        lbxTeam1.ItemsSource = null;
                                                                                                        lbxTeam1.Items.Clear();

                                                                                                        lbxTeam2.ItemsSource = null;
                                                                                                        lbxTeam2.Items.Clear();

                                                                                                        lbltransferedTeamA.ItemsSource = null;
                                                                                                        lbltransferedTeamA.Items.Clear();

                                                                                                        lbltransferedTeamB.ItemsSource = null;
                                                                                                        lbltransferedTeamB.Items.Clear();

                                                                                                        cbxteam1captain.ItemsSource = null;
                                                                                                        cbxteam1captain.Items.Clear();

                                                                                                        cbxteam2captain.ItemsSource = null;
                                                                                                        cbxteam2captain.Items.Clear();

                                                                                                        cbxteam1keeper.ItemsSource = null;
                                                                                                        cbxteam1keeper.Items.Clear();

                                                                                                        cbxteam2keeper.ItemsSource = null;
                                                                                                        cbxteam2keeper.Items.Clear();

                                                                                                        cbxelected.ItemsSource = null;
                                                                                                        cbxelected.Items.Clear();

                                                                                                        cbxtoss.ItemsSource = null;
                                                                                                        cbxtoss.Items.Clear();

                                                                                                        cbxstriker.ItemsSource = null;
                                                                                                        cbxstriker.Items.Clear();

                                                                                                        cbxnonstriker.ItemsSource = null;
                                                                                                        cbxnonstriker.Items.Clear();

                                                                                                        cbxbowler.ItemsSource = null;
                                                                                                        cbxbowler.Items.Clear();

                                                                                                        cbxumpire1.ItemsSource = null;
                                                                                                        cbxumpire1.Items.Clear();

                                                                                                        cbxumpire2.ItemsSource = null;
                                                                                                        cbxumpire2.Items.Clear();

                                                                                                        cbxscorer.ItemsSource = null;
                                                                                                        cbxscorer.Items.Clear();

                                                                                                        cbxVenue.ItemsSource = null;
                                                                                                        cbxVenue.Items.Clear();
                                                                                                    }
                                                                                                    else
                                                                                                    {
                                                                                                        Button btn = (Button)sender;
                                                                                                        btn.Command = NavigationCommands.GoToPage;
                                                                                                        btn.CommandParameter = "/View/MainScoreCard.xaml";

                                                                                                        //////


                                                                                                        lbxTeam1.ItemsSource = null;
                                                                                                        lbxTeam1.Items.Clear();
                                                                                                        
                                                                                                        lbxTeam2.ItemsSource = null;
                                                                                                        lbxTeam2.Items.Clear();

                                                                                                        lbltransferedTeamA.ItemsSource = null;
                                                                                                        lbltransferedTeamA.Items.Clear();

                                                                                                        lbltransferedTeamB.ItemsSource = null;
                                                                                                        lbltransferedTeamB.Items.Clear();

                                                                                                        cbxteam1captain.ItemsSource = null;
                                                                                                        cbxteam1captain.Items.Clear();

                                                                                                        cbxteam2captain.ItemsSource = null;
                                                                                                        cbxteam2captain.Items.Clear();

                                                                                                        cbxteam1keeper.ItemsSource = null;
                                                                                                        cbxteam1keeper.Items.Clear();

                                                                                                        cbxteam2keeper.ItemsSource = null;
                                                                                                        cbxteam2keeper.Items.Clear();

                                                                                                        cbxelected.ItemsSource = null;
                                                                                                        cbxelected.Items.Clear();

                                                                                                        cbxtoss.ItemsSource = null;
                                                                                                        cbxtoss.Items.Clear();

                                                                                                        cbxstriker.ItemsSource = null;
                                                                                                        cbxstriker.Items.Clear();

                                                                                                        cbxnonstriker.ItemsSource = null;
                                                                                                        cbxnonstriker.Items.Clear();

                                                                                                        cbxbowler.ItemsSource = null;
                                                                                                        cbxbowler.Items.Clear();

                                                                                                        cbxumpire1.ItemsSource = null;
                                                                                                        cbxumpire1.Items.Clear();

                                                                                                        cbxumpire2.ItemsSource = null;
                                                                                                        cbxumpire2.Items.Clear();

                                                                                                        cbxscorer.ItemsSource = null;
                                                                                                        cbxscorer.Items.Clear();

                                                                                                        cbxVenue.ItemsSource = null;
                                                                                                        cbxVenue.Items.Clear();
                                                                                                    }

                                                                                                }
                                                                                                else
                                                                                                {

                                                                                                    Fixture objfixturestoadd = Database.GetEntity<Fixture>(Guid.Parse(selectedgridvalue._fixtureid), false, false, false, oleconn);

                                                                                                    objfixturestoadd.FromDate = startDate.SelectedDate.Value.Date;
                                                                                                    objfixturestoadd.ToDate = EndDate.SelectedDate.Value.Date;
                                                                                                    objfixturestoadd.Venue = cbxVenue.Text;
                                                                                                    objfixturestoadd.Umpire = cbxumpire1.Text;
                                                                                                    objfixturestoadd.Umpiree = cbxumpire2.Text;
                                                                                                    objfixturestoadd.Scorer = cbxscorer.Text;
                                                                                                    objfixturestoadd.MatchType = cbxmatchtype.Text;



                                                                                                    temp.stratdate = startDate.SelectedDate.Value.Date.ToString();
                                                                                                    temp.enddate = EndDate.SelectedDate.Value.Date.ToString();
                                                                                                    NameOf.Venue = cbxVenue.Text;
                                                                                                    NameOf.Umpire1 = cbxumpire1.Text;
                                                                                                    NameOf.Umpire2 = cbxumpire2.Text;
                                                                                                    NameOf.Scorer = cbxscorer.Text;
                                                                                                    temp.matchtype = cbxmatchtype.Text;
                                                                                                    Database.SaveEntity<Fixture>(objfixturestoadd, oleconn);


                                                                                                    if (lbxTeam1.Items.Count > 0)
                                                                                                    {

                                                                                                        foreach (var abc in lbxTeam1.Items)
                                                                                                        {
                                                                                                            ListBoxSave objListBoxSave = Database.GetNewEntity<ListBoxSave>();
                                                                                                            objListBoxSave.TeamOne = abc.ToString();
                                                                                                            objListBoxSave.Season = objSeason;
                                                                                                            objListBoxSave.Division = objDivision;
                                                                                                            objListBoxSave.MatchId = selectedgridvalue.matchid;
                                                                                                            objListBoxSave.StartDate = startDate.SelectedDate.Value.Date.ToString();
                                                                                                            objListBoxSave.EndDate = EndDate.SelectedDate.Value.Date.ToString();
                                                                                                            objListBoxSave.Venue = cbxVenue.Text;
                                                                                                            objListBoxSave.UmpireOne = cbxumpire1.Text;
                                                                                                            objListBoxSave.UmpireTwo = cbxumpire2.Text;
                                                                                                            objListBoxSave.Scorer = cbxscorer.Text;
                                                                                                            objListBoxSave.MatchType = cbxmatchtype.Text;
                                                                                                            objListBoxSave.TeamOneCaptain = cbxteam1captain.Text;
                                                                                                            objListBoxSave.TeamTwoCaptain = cbxteam2captain.Text;
                                                                                                            objListBoxSave.TeamOneWk = cbxteam1keeper.Text;
                                                                                                            objListBoxSave.TeamTwoWk = cbxteam2keeper.Text;
                                                                                                            objListBoxSave.TossWon = cbxtoss.Text;
                                                                                                            objListBoxSave.ElectedTo = cbxelected.Text;

                                                                                                            objListBoxSave.Striker = cbxstriker.Text;
                                                                                                            objListBoxSave.NonStriker = cbxnonstriker.Text;
                                                                                                            objListBoxSave.Bowler = cbxbowler.Text;

                                                                                                            objListBoxSave.TeamOneName = txtTeam1.Text;
                                                                                                            objListBoxSave.TeamtwoName = txtTeam2.Text;
                                                                                                            objListBoxSave.TossWonIndex = Convert.ToInt16(tosswon.index);
                                                                                                            objListBoxSave.ElectedToIndex = Convert.ToInt16(electedto.index);

                                                                                                            Database.SaveEntity<ListBoxSave>(objListBoxSave, oleconn);


                                                                                                        }
                                                                                                    }

                                                                                                    if (lbxTeam2.Items.Count > 0)
                                                                                                    {
                                                                                                        foreach (var abc in lbxTeam2.Items)
                                                                                                        {
                                                                                                            ListBoxSave objListBoxSave = Database.GetNewEntity<ListBoxSave>();
                                                                                                            objListBoxSave.TeamTwo = abc.ToString();
                                                                                                            objListBoxSave.Season = objSeason;
                                                                                                            objListBoxSave.Division = objDivision;
                                                                                                            objListBoxSave.MatchId = selectedgridvalue.matchid;
                                                                                                            objListBoxSave.StartDate = startDate.SelectedDate.Value.Date.ToString();
                                                                                                            objListBoxSave.EndDate = EndDate.SelectedDate.Value.Date.ToString();
                                                                                                            objListBoxSave.Venue = cbxVenue.Text;
                                                                                                            objListBoxSave.UmpireOne = cbxumpire1.Text;
                                                                                                            objListBoxSave.UmpireTwo = cbxumpire2.Text;
                                                                                                            objListBoxSave.Scorer = cbxscorer.Text;
                                                                                                            objListBoxSave.MatchType = cbxmatchtype.Text;
                                                                                                            objListBoxSave.TeamOneCaptain = cbxteam1captain.Text;
                                                                                                            objListBoxSave.TeamTwoCaptain = cbxteam2captain.Text;
                                                                                                            objListBoxSave.TeamOneWk = cbxteam1keeper.Text;
                                                                                                            objListBoxSave.TeamTwoWk = cbxteam2keeper.Text;
                                                                                                            objListBoxSave.TossWon = cbxtoss.Text;
                                                                                                            objListBoxSave.ElectedTo = cbxelected.Text;
                                                                                                            objListBoxSave.TeamOneName = txtTeam1.Text;
                                                                                                            objListBoxSave.TeamtwoName = txtTeam2.Text;
                                                                                                            objListBoxSave.TossWonIndex = Convert.ToInt16(tosswon.index);
                                                                                                            objListBoxSave.ElectedToIndex = Convert.ToInt16(electedto.index);

                                                                                                            objListBoxSave.Striker = cbxstriker.Text;
                                                                                                            objListBoxSave.NonStriker = cbxnonstriker.Text;
                                                                                                            objListBoxSave.Bowler = cbxbowler.Text;

                                                                                                            Database.SaveEntity<ListBoxSave>(objListBoxSave, oleconn);
                                                                                                        }
                                                                                                    }


                                                                                                    if (lbltransferedTeamA.Items.Count > 0)
                                                                                                    {
                                                                                                        foreach (var abc in lbltransferedTeamA.Items)
                                                                                                        {
                                                                                                            ListBoxSave objListBoxSave = Database.GetNewEntity<ListBoxSave>();
                                                                                                            objListBoxSave.TransferedTeamOne = abc.ToString();
                                                                                                            objListBoxSave.Season = objSeason;
                                                                                                            objListBoxSave.Division = objDivision;
                                                                                                            objListBoxSave.MatchId = selectedgridvalue.matchid;
                                                                                                            objListBoxSave.StartDate = startDate.SelectedDate.Value.Date.ToString();
                                                                                                            objListBoxSave.EndDate = EndDate.SelectedDate.Value.Date.ToString();
                                                                                                            objListBoxSave.Venue = cbxVenue.Text;
                                                                                                            objListBoxSave.UmpireOne = cbxumpire1.Text;
                                                                                                            objListBoxSave.UmpireTwo = cbxumpire2.Text;
                                                                                                            objListBoxSave.Scorer = cbxscorer.Text;
                                                                                                            objListBoxSave.MatchType = cbxmatchtype.Text;
                                                                                                            objListBoxSave.TeamOneCaptain = cbxteam1captain.Text;
                                                                                                            objListBoxSave.TeamTwoCaptain = cbxteam2captain.Text;
                                                                                                            objListBoxSave.TeamOneWk = cbxteam1keeper.Text;
                                                                                                            objListBoxSave.TeamTwoWk = cbxteam2keeper.Text;
                                                                                                            objListBoxSave.TossWon = cbxtoss.Text;
                                                                                                            objListBoxSave.ElectedTo = cbxelected.Text;

                                                                                                            objListBoxSave.Striker = cbxstriker.Text;
                                                                                                            objListBoxSave.NonStriker = cbxnonstriker.Text;
                                                                                                            objListBoxSave.Bowler = cbxbowler.Text;

                                                                                                            objListBoxSave.TeamOneName = txtTeam1.Text;
                                                                                                            objListBoxSave.TeamtwoName = txtTeam2.Text;
                                                                                                            objListBoxSave.TossWonIndex = Convert.ToInt16(tosswon.index);
                                                                                                            objListBoxSave.ElectedToIndex = Convert.ToInt16(electedto.index);

                                                                                                            Database.SaveEntity<ListBoxSave>(objListBoxSave, oleconn);
                                                                                                        }
                                                                                                    }

                                                                                                    if (lbltransferedTeamB.Items.Count >= 0)
                                                                                                    {
                                                                                                        foreach (var abc in lbltransferedTeamB.Items)
                                                                                                        {
                                                                                                            ListBoxSave objListBoxSave = Database.GetNewEntity<ListBoxSave>();
                                                                                                            objListBoxSave.TransferedTeamTwo = abc.ToString();
                                                                                                            objListBoxSave.Season = objSeason;
                                                                                                            objListBoxSave.Division = objDivision;
                                                                                                            objListBoxSave.MatchId = selectedgridvalue.matchid;
                                                                                                            objListBoxSave.StartDate = startDate.SelectedDate.Value.Date.ToString();
                                                                                                            objListBoxSave.EndDate = EndDate.SelectedDate.Value.Date.ToString();
                                                                                                            objListBoxSave.Venue = cbxVenue.Text;
                                                                                                            objListBoxSave.UmpireOne = cbxumpire1.Text;
                                                                                                            objListBoxSave.UmpireTwo = cbxumpire2.Text;
                                                                                                            objListBoxSave.Scorer = cbxscorer.Text;
                                                                                                            objListBoxSave.MatchType = cbxmatchtype.Text;
                                                                                                            objListBoxSave.TeamOneCaptain = cbxteam1captain.Text;
                                                                                                            objListBoxSave.TeamTwoCaptain = cbxteam2captain.Text;
                                                                                                            objListBoxSave.TeamOneWk = cbxteam1keeper.Text;
                                                                                                            objListBoxSave.TeamTwoWk = cbxteam2keeper.Text;
                                                                                                            objListBoxSave.TossWon = cbxtoss.Text;
                                                                                                            objListBoxSave.ElectedTo = cbxelected.Text;

                                                                                                            objListBoxSave.Striker = cbxstriker.Text;
                                                                                                            objListBoxSave.NonStriker = cbxnonstriker.Text;
                                                                                                            objListBoxSave.Bowler = cbxbowler.Text;

                                                                                                            objListBoxSave.TeamOneName = txtTeam1.Text;
                                                                                                            objListBoxSave.TeamtwoName = txtTeam2.Text;
                                                                                                            objListBoxSave.TossWonIndex = Convert.ToInt16(tosswon.index);
                                                                                                            objListBoxSave.ElectedToIndex = Convert.ToInt16(electedto.index);

                                                                                                            Database.SaveEntity<ListBoxSave>(objListBoxSave, oleconn);
                                                                                                        }
                                                                                                    }

                                                                                                    if (division.num == 1)
                                                                                                    {
                                                                                                        Button btn = (Button)sender;
                                                                                                        btn.Command = NavigationCommands.GoToPage;
                                                                                                        btn.CommandParameter = "/View/MainScoreCard1st.xaml";

                                                                                                        lbxTeam1.ItemsSource = null;
                                                                                                        lbxTeam1.Items.Clear();

                                                                                                        lbxTeam2.ItemsSource = null;
                                                                                                        lbxTeam2.Items.Clear();

                                                                                                        lbltransferedTeamA.ItemsSource = null;
                                                                                                        lbltransferedTeamA.Items.Clear();

                                                                                                        lbltransferedTeamB.ItemsSource = null;
                                                                                                        lbltransferedTeamB.Items.Clear();

                                                                                                        cbxteam1captain.ItemsSource = null;
                                                                                                        cbxteam1captain.Items.Clear();

                                                                                                        cbxteam2captain.ItemsSource = null;
                                                                                                        cbxteam2captain.Items.Clear();

                                                                                                        cbxteam1keeper.ItemsSource = null;
                                                                                                        cbxteam1keeper.Items.Clear();

                                                                                                        cbxteam2keeper.ItemsSource = null;
                                                                                                        cbxteam2keeper.Items.Clear();

                                                                                                        cbxelected.ItemsSource = null;
                                                                                                        cbxelected.Items.Clear();

                                                                                                        cbxtoss.ItemsSource = null;
                                                                                                        cbxtoss.Items.Clear();

                                                                                                        cbxstriker.ItemsSource = null;
                                                                                                        cbxstriker.Items.Clear();

                                                                                                        cbxnonstriker.ItemsSource = null;
                                                                                                        cbxnonstriker.Items.Clear();

                                                                                                        cbxbowler.ItemsSource = null;
                                                                                                        cbxbowler.Items.Clear();

                                                                                                        cbxumpire1.ItemsSource = null;
                                                                                                        cbxumpire1.Items.Clear();

                                                                                                        cbxumpire2.ItemsSource = null;
                                                                                                        cbxumpire2.Items.Clear();

                                                                                                        cbxscorer.ItemsSource = null;
                                                                                                        cbxscorer.Items.Clear();

                                                                                                        cbxVenue.ItemsSource = null;
                                                                                                        cbxVenue.Items.Clear();
                                                                                                    }
                                                                                                    else
                                                                                                    {
                                                                                                        Button btn = (Button)sender;
                                                                                                        btn.Command = NavigationCommands.GoToPage;
                                                                                                        btn.CommandParameter = "/View/MainScoreCard.xaml";
                                                                                                        //////////
                                                                                                        lbxTeam1.ItemsSource = null;
                                                                                                        lbxTeam1.Items.Clear();

                                                                                                        lbxTeam2.ItemsSource = null;
                                                                                                        lbxTeam2.Items.Clear();

                                                                                                        lbltransferedTeamA.ItemsSource = null;
                                                                                                        lbltransferedTeamA.Items.Clear();

                                                                                                        lbltransferedTeamB.ItemsSource = null;
                                                                                                        lbltransferedTeamB.Items.Clear();

                                                                                                        cbxteam1captain.ItemsSource = null;
                                                                                                        cbxteam1captain.Items.Clear();

                                                                                                        cbxteam2captain.ItemsSource = null;
                                                                                                        cbxteam2captain.Items.Clear();

                                                                                                        cbxteam1keeper.ItemsSource = null;
                                                                                                        cbxteam1keeper.Items.Clear();

                                                                                                        cbxteam2keeper.ItemsSource = null;
                                                                                                        cbxteam2keeper.Items.Clear();

                                                                                                        cbxelected.ItemsSource = null;
                                                                                                        cbxelected.Items.Clear();

                                                                                                        cbxtoss.ItemsSource = null;
                                                                                                        cbxtoss.Items.Clear();

                                                                                                        cbxstriker.ItemsSource = null;
                                                                                                        cbxstriker.Items.Clear();

                                                                                                        cbxnonstriker.ItemsSource = null;
                                                                                                        cbxnonstriker.Items.Clear();

                                                                                                        cbxbowler.ItemsSource = null;
                                                                                                        cbxbowler.Items.Clear();

                                                                                                        cbxumpire1.ItemsSource = null;
                                                                                                        cbxumpire1.Items.Clear();

                                                                                                        cbxumpire2.ItemsSource = null;
                                                                                                        cbxumpire2.Items.Clear();

                                                                                                        cbxscorer.ItemsSource = null;
                                                                                                        cbxscorer.Items.Clear();

                                                                                                        cbxVenue.ItemsSource = null;
                                                                                                        cbxVenue.Items.Clear();
                                                                                                    }

                                                                                                    lstfilterlist.Clear();
                                                                                                }
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            MessageBox.Show("End Date must be greater than start date for a match !!");
                                                                                        }
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        MessageBox.Show("Select Match Type For This Match");
                                                                                    }

                                                                                }
                                                                                else
                                                                                {
                                                                                    MessageBox.Show("Select End Date For This Match");
                                                                                }

                                                                            }
                                                                            else
                                                                            {
                                                                                MessageBox.Show("Select Start Date For This Match");
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            MessageBox.Show("Select Venue For This Match");
                                                                        }

                                                                    }
                                                                    else
                                                                    {
                                                                        MessageBox.Show("Select Scorer For This Match");
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    MessageBox.Show("Select Umpire2 For This Match");
                                                                }
                                                            }
                                                            else
                                                            {
                                                                MessageBox.Show("Select Umpire1 For This Match");
                                                            }
                                                        }
                                                        else
                                                        {
                                                            MessageBox.Show("Select Bowler");
                                                        }
                                                    }
                                                    else
                                                    {
                                                        MessageBox.Show("Select Non-Striker");
                                                    }
                                                }
                                                else
                                                {
                                                    MessageBox.Show("Select Striker");
                                                }

                                            }
                                            else
                                            {
                                                MessageBox.Show("Select who Elected to Bat or Bowl");
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("Select Who Won Toss");
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Select Second Team Keeper");
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Select Second Team Captian");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Select First Team Keeper");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Select First Team Captian");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Players List 2 is Empty");
                    }
                }
                else
                {
                    MessageBox.Show("Players List 1 is Empty");
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }




        }

        private void cbxstriker_DropDownOpened(object sender, EventArgs e)
        {
            cbxstriker.Items.Clear();
            
            cbxnonstriker.SelectedIndex = -1;

            if (cbxtoss.SelectedIndex != -1 && cbxelected.SelectedIndex !=- 1)
            {
                if ((cbxtoss.SelectedIndex == 0 && cbxelected.SelectedIndex == 0) || (cbxtoss.SelectedIndex == 1 && cbxelected.SelectedIndex == 1))
                {
                    
                    foreach (var item in lbltransferedTeamA.Items)
                    {
                        cbxstriker.Items.Add(item);
                    }
                }

                else if ((cbxtoss.SelectedIndex == 0 && cbxelected.SelectedIndex == 1) || (cbxtoss.SelectedIndex == 1 && cbxelected.SelectedIndex == 0))
                {
                  //  cbxstriker.Items.Clear();
                    foreach (var item in lbltransferedTeamB.Items)
                    {
                        cbxstriker.Items.Add(item);
                    }
                }
            }

            else
            {
                MessageBox.Show("Select TossWon and ElectedTo");
            }
        }


        private void cbxnonstriker_DropDownOpened(object sender, EventArgs e)
        {
            cbxnonstriker.Items.Clear();
            if (cbxstriker.SelectedIndex != -1)
            {
                var striker = cbxstriker.SelectedItem;
                if ((cbxtoss.SelectedIndex == 0 && cbxelected.SelectedIndex == 0) || (cbxtoss.SelectedIndex == 1 && cbxelected.SelectedIndex == 1))
                {
                    
                    foreach (var item in lbltransferedTeamA.Items)
                    {
                        if (item != striker)
                        {
                            cbxnonstriker.Items.Add(item);
                        }
                    }
                }

                else if ((cbxtoss.SelectedIndex == 0 && cbxelected.SelectedIndex == 1) || (cbxtoss.SelectedIndex == 1 && cbxelected.SelectedIndex == 0))
                {
                    //cbxnonstriker.Items.Clear();
                    foreach (var item in lbltransferedTeamB.Items)
                    {
                        if (item != striker)
                        {
                            cbxnonstriker.Items.Add(item);
                        }
                    }
                }
            }

            else
            {
                MessageBox.Show("Select Striker First");
            }

        }

        private void cbxbowler_DropDownOpened(object sender, EventArgs e)
        {
            cbxbowler.Items.Clear();

            if (cbxtoss.SelectedIndex != -1 && cbxelected.SelectedIndex != -1)
            {
                if ((cbxtoss.SelectedIndex == 0 && cbxelected.SelectedIndex == 0) || (cbxtoss.SelectedIndex == 1 && cbxelected.SelectedIndex == 1))
                {
                    cbxbowler.Items.Clear();
                    foreach (var item in lbltransferedTeamB.Items)
                    {
                        cbxbowler.Items.Add(item);
                    }
                }

                else if ((cbxtoss.SelectedIndex == 0 && cbxelected.SelectedIndex == 1) || (cbxtoss.SelectedIndex == 1 && cbxelected.SelectedIndex == 0))
                {
                    foreach (var item in lbltransferedTeamA.Items)
                    {
                        cbxbowler.Items.Add(item);
                    }
                }
            }

            else
            {
                MessageBox.Show("Select TossWon and ElectedTo");
            }
        }


    }

    public static class comboteam
    {
        public static string teamA;
        public static string teamB;

    }
}
