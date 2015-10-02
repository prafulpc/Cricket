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
using System.Data.OleDb;
using System.Data;
using Cricket.ReportData;
using Microsoft.Reporting.WinForms;
using Cricket.BLL;
using FirstFloor.ModernUI.Windows.Navigation;




namespace Cricket.View
{
    /// <summary>
    /// Interaction logic for formFixtures.xaml
    /// </summary>
    public partial class formFixtures : UserControl
    {

        OleDbConnection oleconn = Database.getConnection();
        string div;
        string scorecardid;
        string exportid;
        string zoneno;
        string seasonno;
        string div_id;
        string divletter;

        public formFixtures()
        {

            InitializeComponent();
          
            

        }

        private void btntransfer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbxNoOfGroups.SelectedItem != null)
                {

                    if (lbxteams.SelectedItem != null)
                    {
                        if (rbtnGroupA.IsChecked == true)
                        {
                            string player = lbxteams.SelectedItem.ToString();
                            lbxGroupA.Items.Add(player);
                            lbxteams.Items.Remove(player);
                            lbxteams.SelectedIndex = 0;
                        }
                        else if (rbtnGroupB.IsChecked == true)
                        {
                            string player = lbxteams.SelectedItem.ToString();
                            lbxGroupB.Items.Add(player);
                            lbxteams.Items.Remove(player);
                            lbxteams.SelectedIndex = 0;
                        }
                        else if (rbtnGroupC.IsChecked == true)
                        {
                            string player = lbxteams.SelectedItem.ToString();
                            lbxGroupC.Items.Add(player);
                            lbxteams.Items.Remove(player);
                            lbxteams.SelectedIndex = 0;
                        }
                        else if (rbtnGroupD.IsChecked == true)
                        {
                            string player = lbxteams.SelectedItem.ToString();
                            lbxGroupD.Items.Add(player);
                            lbxteams.Items.Remove(player);
                            lbxteams.SelectedIndex = 0;
                        }
                        else
                        {
                            MessageBox.Show("Select Group to Transfer");
                        }
                    }

                    else
                    {
                        MessageBox.Show("Select Item Before Tansfer");
                    }
                }

                else
                {
                    MessageBox.Show("Select Number Of Groups");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void btnreverse_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbxNoOfGroups.SelectedItem != null)
                {
                    if (rbtnGroupA.IsChecked != null || rbtnGroupB.IsChecked != null || rbtnGroupC.IsChecked != null)
                    {
                        if (rbtnGroupA.IsChecked == true)
                        {
                            if (lbxGroupA.Items.Count != 0)
                            {
                                if (lbxGroupA.SelectedItem == null)
                                {
                                    MessageBox.Show("Select Group A Team to Reverse");
                                }
                                else
                                {

                                    string abc = lbxGroupA.SelectedItem.ToString();
                                    lbxteams.Items.Add(abc);
                                    lbxGroupA.Items.Remove(abc);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Group A is Empty");
                            }



                        }
                        else if (rbtnGroupB.IsChecked == true)
                        {
                            if (lbxGroupB.SelectedItem == null)
                            {
                                MessageBox.Show("Select Group B Team to Reverse");
                            }
                            else
                            {

                                string abc = lbxGroupB.SelectedItem.ToString();
                                lbxteams.Items.Add(abc);
                                lbxGroupB.Items.Remove(abc);
                            }
                        }
                        else if (rbtnGroupC.IsChecked == true)
                        {
                            if (lbxGroupC.SelectedItem == null)
                            {
                                MessageBox.Show("Select Group C Team to Reverse");
                            }
                            else
                            {

                                string abc = lbxGroupC.SelectedItem.ToString();
                                lbxteams.Items.Add(abc);
                                lbxGroupC.Items.Remove(abc);
                            }
                        }
                        else if (rbtnGroupD.IsChecked == true)
                        {
                            if (lbxGroupD.SelectedItem == null)
                            {
                                MessageBox.Show("Select Group D Team to Reverse");
                            }
                            else
                            {

                                string abc = lbxGroupD.SelectedItem.ToString();
                                lbxteams.Items.Add(abc);
                                lbxGroupD.Items.Remove(abc);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Groups are Empty");
                    }

                }
                else
                {
                    MessageBox.Show("Select The no of Groups");

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void btngeneratefix_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // season_zone_div.name[2] = 


                //DIVISION
                DataRowView rowview = cbxdivision.SelectedItem as DataRowView;

                if (cbxdivision.SelectedItem != null)
                {
                    div_id = rowview.Row[0].ToString();
                }
                else if (cbxdivision.SelectedItem == null)
                {
                    MessageBox.Show("Select Division");
                }


                if (cbxdivision.SelectedIndex == 0)
                {
                    div = "1";
                    divletter = "F";

                }
                else if (cbxdivision.SelectedIndex == 1)
                {
                    div = "2";
                    divletter = "S";
                }
                else if (cbxdivision.SelectedIndex == 2)
                {
                    div = "3";
                    divletter = "T";
                }


                //ZONE
                if (cbxzone.Text == "Tumkur")
                {
                    zoneno = "7";
                }

                //SEASON
                string seas = cbxseason.Text;
                seasonno = seas.Substring(2, 2);

                int groups;
                if (cbxNoOfGroups.SelectedIndex == 0)
                {
                    groups = 1;
                }
                else if (cbxNoOfGroups.SelectedIndex == 1)
                {
                    groups = 2;
                }
                else if (cbxNoOfGroups.SelectedIndex == 2)
                {
                    groups = 3;
                }
                else
                {
                    groups = 4;
                }


                FixtureGeneration objfix = new FixtureGeneration();

                DataTable dt = new DataTable();

                ObservableCollection<string> collteam_A = new ObservableCollection<string>();
                ObservableCollection<string> collteam_B = new ObservableCollection<string>();
                ObservableCollection<string> collteam_C = new ObservableCollection<string>();
                ObservableCollection<string> collteam_D = new ObservableCollection<string>();


                //Gropu A
                if (((lbxGroupA.Items.Count) % 2) == 1)
                {
                    foreach (string A in lbxGroupA.Items)
                    {
                        collteam_A.Add(A);
                    }
                    collteam_A.Add("Dummy A");
                }

                else
                {
                    foreach (string A in lbxGroupA.Items)
                    {
                        collteam_A.Add(A);
                    }
                }
                //Gropu B
                if (((lbxGroupB.Items.Count) % 2) == 1)
                {
                    foreach (string B in lbxGroupB.Items)
                    {
                        collteam_B.Add(B);
                    }
                    collteam_B.Add("Dummy B");
                }

                else
                {
                    foreach (string B in lbxGroupB.Items)
                    {
                        collteam_B.Add(B);
                    }
                }

                //Gropu C
                if (((lbxGroupC.Items.Count) % 2) == 1)
                {
                    foreach (string C in lbxGroupC.Items)
                    {
                        collteam_C.Add(C);
                    }
                    collteam_C.Add("Dummy C");
                }

                else
                {
                    foreach (string C in lbxGroupC.Items)
                    {
                        collteam_C.Add(C);
                    }
                }

                //Gropu D
                if (((lbxGroupD.Items.Count) % 2) == 1)
                {
                    foreach (string D in lbxGroupD.Items)
                    {
                        collteam_D.Add(D);
                    }
                    collteam_D.Add("Dummy D");
                }

                else
                {
                    foreach (string D in lbxGroupD.Items)
                    {
                        collteam_D.Add(D);
                    }
                }

                DataTable das = new DataTable();
                dgvfixtures.DataContext = null;

                //string zonename, string seasonname,string divname, int NoOfGroups 
                das = objfix.fixture(collteam_A, collteam_B, collteam_C, collteam_D, zoneno, seasonno, groups, divletter);

                dgvfixtures.ItemsSource = null;
                dgvfixtures.DataContext = das;
                dgvfixtures.ItemsSource = das.DefaultView;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ObservableCollection<Team> lbx = Database.GetEntityList<Team>(false, true, true, oleconn, "RecordStatus", "TeamName");
            ObservableCollection<Fixture> lbxFixtures = Database.GetEntityList<Fixture>(false, false, false, oleconn, "RecordStatus", "SerialNo");
         //   ObservableCollection<DivisionLoad> lstDivisionLoad = Database.GetEntityList<DivisionLoad>(false, false, false, Database.getReadConnection(), "RecordStatus", "DivisionLoadId");


            try
            {
                if (cbxdivision.SelectedItem == null)
                {
                    MessageBox.Show("Select Division To Continue....");

                }
                else
                {
                    FixtureGenerator frm = new FixtureGenerator();
                    List<FixtureReport> lstFixtureReport = new List<FixtureReport>();

                    ObservableCollection<Fixture> lstfixture = Database.GetEntityList<Fixture>(false, false, false, oleconn, "RecordStatus", "FixtureId");

                    var objvalues = from s in lbxFixtures where s.DivisionId.ToString() == cbxdivision.SelectedValue.ToString() && s.SeasonId.ToString() == cbxseason.SelectedValue.ToString() select s;
                    List<Fixture> lstselecteddiv = objvalues.OrderBy(p => p.MatchId).ToList<Fixture>();

                    int slno = 1;
                    foreach (var objPlayer in lstselecteddiv)
                    {
                        FixtureReport obj = new FixtureReport();
                        obj.SlNo = slno;
                        obj.Group = objPlayer.Group;
                        obj.Versus = objPlayer.Teams;
                        obj.Venue = objPlayer.Venue;
                        obj.Umpire1 = objPlayer.Umpire;
                        obj.Umpire2 = objPlayer.Umpiree;
                        obj.Scorer = objPlayer.Scorer;
                        slno++;
                        lstFixtureReport.Add(obj);

                    }
                    ReportViewer rv = frm.GetReportViewer();
                    rv.Reset();
                    rv.LocalReport.ReportEmbeddedResource = @"Cricket.Reports.FixtureGeneration.rdlc";
                    ReportDataSource RDS = new ReportDataSource("FixtureGeneration", lstFixtureReport);
                    rv.LocalReport.DataSources.Add(RDS);
                    rv.Refresh();
                    frm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            string strseasons = "";
            strseasons = "select * from Zones";
            OleDbDataAdapter adpt11 = new OleDbDataAdapter(strseasons, oleconn);
            DataTable aa1 = new DataTable();
            adpt11.Fill(aa1);
            aa1.DefaultView.Sort = "ZoneName";
            cbxzone.ItemsSource = aa1.DefaultView;
            cbxzone.DisplayMemberPath = aa1.Columns["ZoneName"].ToString();
            cbxzone.SelectedValuePath = aa1.Columns["ZoneId"].ToString();

            cbxzone.SelectedIndex = 0;
          //  ObservableCollection<Team> lbx = Database.GetEntityList<Team>(false, true, true, oleconn, "RecordStatus", "TeamName");
            ObservableCollection<Fixture> lbxFixtures = Database.GetEntityList<Fixture>(false, false, false, oleconn, "RecordStatus", "SerialNo");
            //   ObservableCollection<DivisionLoad> lstDivisionLoad = Database.GetEntityList<DivisionLoad>(false, false, false, Database.getReadConnection(), "RecordStatus", "DivisionLoadId");


            try
            {
                NavigationWindow ws = new NavigationWindow();
                ws.ShowsNavigationUI = false;

                cbxzone.SelectedIndex = 0;
                cbxdivision.ItemsSource = null;
                dgvfixtures.ItemsSource = null;
                dgvfixtures.DataContext = null;
                dgvfixtures.Items.Clear();
                lbxGroupA.ItemsSource = null;
                lbxGroupB.ItemsSource = null;
                lbxGroupC.ItemsSource = null;
                lbxGroupD.ItemsSource = null;
                lbxteams.ItemsSource = null;
                lbxGroupA.Items.Clear();
                lbxGroupB.Items.Clear();
                lbxGroupC.Items.Clear();
                lbxGroupD.Items.Clear();
                lbxteams.Items.Clear();
                clearall();


           

                if (cbxseason.SelectedValue == null)
                {

                }
                if (cbxdivision.SelectedValue == null)
                {

                }

                else
                {
                    
                    lbxFixtures.Clear();

                    lbxFixtures = Database.GetEntityList<Fixture>(false, false, false, oleconn, "RecordStatus", "SerialNo");
                    var objvalues = from s in lbxFixtures where s.DivisionId.ToString() == selectedgridvalue.LoadDivisionId && s.SeasonId.ToString() == TempValues.LoadSeasonId select s;
                    List<Fixture> lstselecteddiv = objvalues.OrderBy(p => p.MatchId).ToList<Fixture>();

            

                    if (lstselecteddiv.Count > 0)
                    {


                        DataTable dt = new DataTable();
                        dt.Columns.Add("Venue");
                        dt.Columns.Add("Umpire");
                        dt.Columns.Add("Umpiree");
                        dt.Columns.Add("Scorer");
                        dt.Columns.Add("MatchId");
                        dt.Columns.Add("SerialNo");
                        dt.Columns.Add("Day");
                        dt.Columns.Add("Group");
                        dt.Columns.Add("Teams");

                        foreach (var objFixte in lstselecteddiv)
                        {
                            //  Fixture obj = Database.GetEntity<Fixture>(Guid.Parse(selectedgridvalue._fixtureid),false,false,false,oleconn);
                            if (objFixte.SeasonId.ToString() == selectedgridvalue._seasonid)
                            {

                                DataRow dr = dt.NewRow();

                                dr["SerialNo"] = objFixte.SerialNo;
                                dr["Day"] = objFixte.Day;

                               
                                dr["Group"] = objFixte.Group;
                                dr["Teams"] = objFixte.Teams;
                                dr["MatchId"] = objFixte.MatchId;
                                dr["Venue"] = objFixte.Venue;
                                dr["Umpire"] = objFixte.Umpire;
                                dr["Umpiree"] = objFixte.Umpiree;
                                dr["Scorer"] = objFixte.Scorer;
                                dt.Rows.Add(dr);
                            }

                            // dgvfixtures.Items.Add(obj);
                        }

                        dgvfixtures.ItemsSource = null;
                        dgvfixtures.Items.Clear();
                        dgvfixtures.DataContext = dt;
                        dgvfixtures.ItemsSource = dt.DefaultView;



                    }
                }
                //  }

                //else
                //{
                //    lbxteams.Items.Clear();

                //    if (TempValues.index != 0)
                //    {

                //        dgvfixtures.ItemsSource = null;
                //        lbxteams.IsEnabled = true;



                //        lbxGroupA.Items.Clear();
                //        lbxGroupB.Items.Clear();
                //        lbxGroupC.Items.Clear();
                //        lbxGroupD.Items.Clear();
                //        foreach (var objGroupTeamValues in lstGroupTeamValues)
                //        {

                //            // if (objDivision.SeasonId.ToString() == cbxseason.SelectedItem.ToString())
                //            if (objGroupTeamValues.SeasonId.ToString() == cbxseason.SelectedValue.ToString())
                //            {
                //                if (objGroupTeamValues.DivisionId.ToString() == cbxdivision.SelectedValue.ToString())
                //                {
                //                    if (objGroupTeamValues.NoOfGroups == cbxNoOfGroups.Text)
                //                    {
                //                        cbxNoOfGroups.IsEnabled = true;
                //                        ok.IsEnabled = true;
                //                        btntransfer.IsEnabled = false;
                //                        btnreverse.IsEnabled = false;
                //                        btnSave.IsEnabled = false;
                //                        btngeneratefix.IsEnabled = false;
                //                        // lbxTeamLoad.IsEnabled = false;
                //                        if (objGroupTeamValues.GroupA != null)
                //                        {
                //                            lbxGroupA.Items.Add(objGroupTeamValues.GroupA);
                //                        }


                //                        if (objGroupTeamValues.GroupB != null)
                //                        {

                //                            lbxGroupB.Items.Add(objGroupTeamValues.GroupB);

                //                        }

                //                        if (objGroupTeamValues.GroupC != null)
                //                        {

                //                            lbxGroupC.Items.Add(objGroupTeamValues.GroupC);

                //                        }

                //                        if (objGroupTeamValues.GroupD != null)
                //                        {

                //                            lbxGroupD.Items.Add(objGroupTeamValues.GroupD);

                //                        }
                //                    }
                //                }

                //            }
                //        }
                //    }
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                // DataSet ds = new DataSet();
                dt = dgvfixtures.DataContext as DataTable;
                //  ds.Tables.Add(dt);

                Season objSeason = Database.GetEntity<Season>(Guid.Parse(cbxseason.SelectedValue.ToString().ToString()), false, false, false, oleconn);
                Division objDivision = Database.GetEntity<Division>(Guid.Parse(cbxdivision.SelectedValue.ToString()), false, false, false, oleconn);


                List<Fixture> lstFixtureReport = new List<Fixture>();
                foreach (DataRowView objPlayer in dt.DefaultView)
                {
                    Fixture obj1 = Database.GetNewEntity<Fixture>();
                    obj1.SerialNo = objPlayer["SerialNo"].ToString();
                    obj1.Group = objPlayer["Group"].ToString();
                    obj1.Teams = objPlayer["Teams"].ToString();
                    obj1.MatchId = objPlayer["MatchId"].ToString();
                    obj1.Venue = objPlayer["Venue"].ToString();
                    obj1.Umpire = objPlayer["Umpire"].ToString();
                    obj1.Umpiree = objPlayer["Umpiree"].ToString();
                    obj1.Scorer = objPlayer["Scorer"].ToString();
                    obj1.Day = objPlayer["Day"].ToString();

                    obj1.Season = objSeason;

                    obj1.Division = objDivision;

                    // lstFixtureReport.Add(obj1);
                    Database.SaveEntity<Fixture>(obj1, oleconn);

                }



                MessageBox.Show("Fixtures Saved For The Given Division = " + cbxdivision.Text);

                btnSave.IsEnabled = false;
                dgvfixtures.ItemsSource = null;
                dgvfixtures.DataContext = null;
                dgvfixtures.Items.Clear();

                if (lbxGroupA.Items.Count > 0)
                {

                    foreach (var abc in lbxGroupA.Items)
                    {
                        GroupTeamValues objGroupTeamValues = Database.GetNewEntity<GroupTeamValues>();
                        objGroupTeamValues.GroupA = abc.ToString();

                        objGroupTeamValues.Season = objSeason;
                        objGroupTeamValues.Division = objDivision;
                        objGroupTeamValues.NoOfGroups = cbxNoOfGroups.Text;

                        Database.SaveEntity<GroupTeamValues>(objGroupTeamValues, oleconn);
                    }
                }

                if (lbxGroupB.Items.Count > 0)
                {
                    foreach (var abc in lbxGroupB.Items)
                    {
                        GroupTeamValues objGroupTeamValues = Database.GetNewEntity<GroupTeamValues>();
                        objGroupTeamValues.GroupB = abc.ToString();

                        objGroupTeamValues.Season = objSeason;
                        objGroupTeamValues.Division = objDivision;
                        objGroupTeamValues.NoOfGroups = cbxNoOfGroups.Text;

                        Database.SaveEntity<GroupTeamValues>(objGroupTeamValues, oleconn);
                    }
                }


                if (lbxGroupC.Items.Count > 0)
                {
                    foreach (var abc in lbxGroupC.Items)
                    {
                        GroupTeamValues objGroupTeamValues = Database.GetNewEntity<GroupTeamValues>();
                        objGroupTeamValues.GroupC = abc.ToString();

                        objGroupTeamValues.Season = objSeason;
                        objGroupTeamValues.Division = objDivision;
                        objGroupTeamValues.NoOfGroups = cbxNoOfGroups.Text;

                        Database.SaveEntity<GroupTeamValues>(objGroupTeamValues, oleconn);
                    }
                }

                if (lbxGroupD.Items.Count >= 0)
                {
                    foreach (var abc in lbxGroupD.Items)
                    {
                        GroupTeamValues objGroupTeamValues = Database.GetNewEntity<GroupTeamValues>();
                        objGroupTeamValues.GroupD = abc.ToString();

                        objGroupTeamValues.Season = objSeason;
                        objGroupTeamValues.Division = objDivision;
                        objGroupTeamValues.NoOfGroups = cbxNoOfGroups.Text;

                        Database.SaveEntity<GroupTeamValues>(objGroupTeamValues, oleconn);
                    }

                }

                load();

                dgvfixtures.ItemsSource = null;
                dgvfixtures.DataContext = null;
                dgvfixtures.Items.Clear();

                //dgvfixtures.ItemsSource = dt.DefaultView;

                //    //lbxGroupA.Items.Clear();
                //    //lbxGroupB.Items.Clear();
                //    //lbxGroupC.Items.Clear();
                //    //lbxGroupD.Items.Clear();
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void BallByBallScoring_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                dgvfixtures.ItemsSource = null;
                ObservableCollection<Fixture> objgixtureid = Database.GetEntityList<Fixture>(false, false, false, oleconn, "RecordStatus", "SerialNo");
                ObservableCollection<ListBoxSave> lbxListBoxSave = Database.GetEntityList<ListBoxSave>(false, false, false, oleconn, "RecordStatus", "ListBoxSaveId");


                Fixture objviewscorecard = ((FrameworkElement)sender).DataContext as Fixture;

                //DataRowView rowview = dgvfixtures.SelectedItem as DataRowView;
                scorecardid = objviewscorecard.Teams;
                tempvalues.teamA = scorecardid.Substring(0, scorecardid.IndexOf("v/s") - 1);
                tempvalues.teamB = scorecardid.Substring(scorecardid.IndexOf("v/s") + 4);
                selectedgridvalue.matchid = objviewscorecard.MatchId;
                selectedgridvalue.group = objviewscorecard.Group;
                TempValues.LoadSeasonId = cbxseason.SelectedValue.ToString();
                TempValues.LoadZoneId = cbxzone.SelectedValue.ToString();
                TempValues.LoadZone = cbxzone.Text;
                TempValues.LoadSeason = cbxseason.Text;
                if (cbxdivision.SelectedIndex == 0)
                {
                    division.num = 1;
                }
                else
                {
                    division.num = 2;
                }


                foreach (Fixture objfix in objgixtureid)
                {
                    if (objfix.MatchId == objviewscorecard.MatchId)
                    {
                        string startdate = Convert.ToString(objfix.FromDate);
                        string s_time = ((startdate.ToString()).Substring(startdate.ToString().IndexOf(" ") - 9, 9));

                        string enddate = Convert.ToString(objfix.ToDate);
                        string e_time = ((enddate.ToString()).Substring(enddate.ToString().IndexOf(" ") - 9, 9));

                        temp.stratdate = s_time;
                        temp.enddate = e_time;
                        NameOf.Venue = objfix.Venue;
                        NameOf.Umpire1 = objfix.Umpire;
                        NameOf.Umpire2 = objfix.Umpiree;
                        NameOf.Scorer = objfix.Scorer;
                        temp.matchtype = objfix.MatchType;

                    }
                }


                Fixture obj = (Fixture)((Button)e.Source).DataContext;


                exportid = ((Button)sender).CommandParameter.ToString();

                Button btn1 = (Button)sender;
                btn1.Command = NavigationCommands.GoToPage;
                btn1.CommandParameter = exportid;

                List<Fixture> lstselecteddiv = objgixtureid.Where(p => p.MatchId.ToString() == exportid).OrderBy(p => p.MatchId).ToList<Fixture>();

                if (lstselecteddiv.Count > 0)
                {
                    foreach (var items in lstselecteddiv)
                    {

                        tempvalues.teamA = items.Teams.Substring(0, items.Teams.IndexOf("v/s") - 1);
                        tempvalues.teamB = items.Teams.Substring(items.Teams.IndexOf("v/s") + 4);

                        selectedgridvalue._fixtureid = items.FixtureId.ToString();
                        loadmatchid.loadmatchidab = items.MatchId;

                        foreach (ListBoxSave lbxtosave in lbxListBoxSave)
                        {
                            if (obj.MatchId == lbxtosave.MatchId)
                            {
                                if (obj.Group == "Semi-Final")
                                {
                                    if (obj.SeasonId.ToString() == lbxtosave.SeasonId.ToString())
                                    {

                                        string teamA = lbxtosave.TeamOneName;
                                        string teamB = lbxtosave.TeamtwoName;

                                        obj.Teams = teamA + " v/s " + teamB;

                                        tempvalues.teamA = lbxtosave.TeamOneName;
                                        tempvalues.teamB = lbxtosave.TeamtwoName;

                                        break;
                                    }
                                }

                                if (obj.Group == "Final")
                                {
                                    if (obj.SeasonId.ToString() == lbxtosave.SeasonId.ToString())
                                    {

                                        string teamA = lbxtosave.TeamOneName;
                                        string teamB = lbxtosave.TeamtwoName;

                                        obj.Teams = teamA + " v/s " + teamB;

                                        tempvalues.teamA = lbxtosave.TeamOneName;
                                        tempvalues.teamB = lbxtosave.TeamtwoName;

                                        break;
                                    }
                                }


                            }

                        }

                    }
                }

                selectedgridvalue.LoadDivision = cbxdivision.Text;
                selectedgridvalue.LoadDivisionId = cbxdivision.SelectedValue.ToString();
                selectedgridvalue._seasonid = cbxseason.SelectedValue.ToString();

                selectedgridvalue.LoadDivisionId = cbxdivision.SelectedValue.ToString();
                Button btn = (Button)sender;
                btn.Command = NavigationCommands.GoToPage;
                btn.CommandParameter = "/View/selectplayers1.xaml";

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void viewscorecard_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                dgvfixtures.ItemsSource = null;
                ObservableCollection<Fixture> objgixtureid = Database.GetEntityList<Fixture>(false, false, false, oleconn, "RecordStatus", "SerialNo");
                ObservableCollection<ListBoxSave> lbxListBoxSave = Database.GetEntityList<ListBoxSave>(false, false, false, oleconn, "RecordStatus", "ListBoxSaveId");


                Fixture objviewscorecard = ((FrameworkElement)sender).DataContext as Fixture;

                //DataRowView rowview = dgvfixtures.SelectedItem as DataRowView;
                scorecardid = objviewscorecard.Teams;
                tempvalues.teamA = scorecardid.Substring(0, scorecardid.IndexOf("v/s") - 1);
                tempvalues.teamB = scorecardid.Substring(scorecardid.IndexOf("v/s") + 4);
                selectedgridvalue.matchid = objviewscorecard.MatchId;
                selectedgridvalue.group = objviewscorecard.Group;
                TempValues.LoadSeasonId = cbxseason.SelectedValue.ToString();
                TempValues.LoadZoneId = cbxzone.SelectedValue.ToString();
                TempValues.LoadZone = cbxzone.Text;
                TempValues.LoadSeason = cbxseason.Text;
                if (cbxdivision.SelectedIndex == 0)
                {
                    division.num = 1;
                }
                else
                {
                    division.num = 2;
                }


                foreach (Fixture objfix in objgixtureid)
                {
                    if (objfix.MatchId == objviewscorecard.MatchId)
                    {
                        string startdate = Convert.ToString(objfix.FromDate);
                        string s_time = ((startdate.ToString()).Substring(startdate.ToString().IndexOf(" ") - 9, 9));

                        string enddate = Convert.ToString(objfix.ToDate);
                        string e_time = ((enddate.ToString()).Substring(enddate.ToString().IndexOf(" ") - 9, 9));

                        temp.stratdate = s_time;
                        temp.enddate = e_time;
                        NameOf.Venue = objfix.Venue;
                        NameOf.Umpire1 = objfix.Umpire;
                        NameOf.Umpire2 = objfix.Umpiree;
                        NameOf.Scorer = objfix.Scorer;
                        temp.matchtype = objfix.MatchType;
                       
                    }
                }


                Fixture obj = (Fixture)((Button)e.Source).DataContext;


                exportid = ((Button)sender).CommandParameter.ToString();

                Button btn1 = (Button)sender;
                btn1.Command = NavigationCommands.GoToPage;
                btn1.CommandParameter = exportid;

                List<Fixture> lstselecteddiv = objgixtureid.Where(p => p.MatchId.ToString() == exportid).OrderBy(p => p.MatchId).ToList<Fixture>();

                if (lstselecteddiv.Count > 0)
                {
                    foreach (var items in lstselecteddiv)
                    {

                        tempvalues.teamA = items.Teams.Substring(0, items.Teams.IndexOf("v/s") - 1);
                        tempvalues.teamB = items.Teams.Substring(items.Teams.IndexOf("v/s") + 4);

                        selectedgridvalue._fixtureid = items.FixtureId.ToString();
                        loadmatchid.loadmatchidab = items.MatchId;

                        foreach (ListBoxSave lbxtosave in lbxListBoxSave)
                        {
                            if (obj.MatchId == lbxtosave.MatchId)
                            {
                                if (obj.Group == "Semi-Final")
                                {
                                    if (obj.SeasonId.ToString() == lbxtosave.SeasonId.ToString())
                                    {

                                        string teamA = lbxtosave.TeamOneName;
                                        string teamB = lbxtosave.TeamtwoName;

                                        obj.Teams = teamA + " v/s " + teamB;

                                        tempvalues.teamA = lbxtosave.TeamOneName;
                                        tempvalues.teamB = lbxtosave.TeamtwoName;

                                        break;
                                    }
                                }

                                if (obj.Group == "Final")
                                {
                                    if (obj.SeasonId.ToString() == lbxtosave.SeasonId.ToString())
                                    {

                                        string teamA = lbxtosave.TeamOneName;
                                        string teamB = lbxtosave.TeamtwoName;

                                        obj.Teams = teamA + " v/s " + teamB;

                                        tempvalues.teamA = lbxtosave.TeamOneName;
                                        tempvalues.teamB = lbxtosave.TeamtwoName;

                                        break;
                                    }
                                }


                            }

                        }
                    
                    }
                }

                selectedgridvalue.LoadDivision = cbxdivision.Text;
                selectedgridvalue.LoadDivisionId = cbxdivision.SelectedValue.ToString();
                selectedgridvalue._seasonid = cbxseason.SelectedValue.ToString();

                selectedgridvalue.LoadDivisionId = cbxdivision.SelectedValue.ToString();
                Button btn = (Button)sender;
                btn.Command = NavigationCommands.GoToPage;
                btn.CommandParameter = "/View/SelectPlayers.xaml";

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        

        public void load()
        {
            Season objSeason1 = Database.GetEntity<Season>(Guid.Parse(cbxseason.SelectedValue.ToString().ToString()), false, false, false, oleconn);
            ObservableCollection<Fixture> lbxFixtures1 = Database.GetEntityList<Fixture>(false, false, false, oleconn, "RecordStatus", "SerialNo");


            if (objSeason1.SeasonId.ToString() == cbxseason.SelectedValue.ToString().ToString())
            {


                List<Fixture> lstselecteddiv = lbxFixtures1.Where(p => p.DivisionId.ToString() == cbxdivision.SelectedValue.ToString()).OrderBy(p => p.MatchId).ToList<Fixture>();


                Division objDivision = Database.GetEntity<Division>(Guid.Parse(cbxdivision.SelectedValue.ToString()), false, false, false, oleconn);
               

                if (lstselecteddiv.Count > 0)
                {
                    dgvfixtures.ItemsSource = null;
                    dgvfixtures.Items.Clear();
                    foreach (Fixture obj in lstselecteddiv)
                    {
                        if (obj.SeasonId.ToString() == cbxseason.SelectedValue.ToString().ToString())
                        {
                            if (obj.DivisionId.ToString() == cbxdivision.SelectedValue.ToString())
                            {
                              

                                //lbxteams.IsEnabled = false;
                                dgvfixtures.Items.Add(obj);
                                dgvfixtures.DataContext = obj;

                            }

                            if (obj.DivisionId.ToString() != cbxdivision.SelectedValue.ToString())
                            {

                                dgvfixtures.ItemsSource = null;
                                if (TempValues.index != 0)
                                {
                                    for (int i = 0; i <= TempValues.index - 1; i++)
                                    {

                                        lbxteams.Items.Add(TempValues.teamname[i].ToString());

                                    }
                                }

                            }
                        }
                    }
                }
            }
        }


        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Fixture objexport = ((FrameworkElement)sender).DataContext as Fixture;

                //DataRowView rowview = dgvfixtures.SelectedItem as DataRowView;
                exportid = objexport.Teams;
                tempvalues.teamA = exportid.Substring(0, exportid.IndexOf("v/s") - 1);
                tempvalues.teamB = exportid.Substring(exportid.IndexOf("v/s") + 4);
                selectedgridvalue.selectedstring = dgvfixtures.SelectedItem.ToString();
                selectedgridvalue.LoadDivision = cbxdivision.Text;
                selectedgridvalue.matchid = objexport.MatchId;
                TempValues.SeasonName = cbxseason.Text;
                selectedgridvalue.group = objexport.Group;
                selectedgridvalue._seasonid = cbxseason.SelectedValue.ToString();
                selectedgridvalue.LoadDivisionId = cbxdivision.SelectedValue.ToString();
                //ObservableCollection<Fixture> lbxFixtures = Database.GetEntityList<Fixture>(false, false, false, oleconn, "RecordStatus", "SerialNo");

                //foreach (Fixture objfixture in lbxFixtures)
                //{
                //    selectedgridvalue.matchid = objfixture.MatchId;
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void txtSearch1_TextChanged(object sender, TextChangedEventArgs e)
        {
           // ObservableCollection<Team> lbx = Database.GetEntityList<Team>(false, true, true, oleconn, "RecordStatus", "TeamName");
            ObservableCollection<Fixture> lbxFixtures = Database.GetEntityList<Fixture>(false, false, false, oleconn, "RecordStatus", "SerialNo");
            //   ObservableCollection<DivisionLoad> lstDivisionLoad = Database.GetEntityList<DivisionLoad>(false, false, false, Database.getReadConnection(), "RecordStatus", "DivisionLoadId");

            try
            {

                if (cbxdivision.SelectedItem == null)
                {
                    MessageBox.Show("Select Division to Search");
                }
                else
                {
                    dgvfixtures.ItemsSource = null;
                    dgvfixtures.Items.Clear();

                    if (txtSearch1.Text != "")
                    {
                        selectedgridvalue.LoadDivisionId = cbxdivision.SelectedValue.ToString();
                        List<Fixture> lstselecteddiv = lbxFixtures.Where(p => p.DivisionId.ToString() == selectedgridvalue.LoadDivisionId).OrderBy(p => p.MatchId).ToList<Fixture>();
                        string str = txtSearch1.Text.ToString().ToUpper();

                        foreach (Fixture obj in lstselecteddiv)
                        {
                            if (obj.Teams != null)
                            {
                                if (obj.Teams.Contains(str) == true)
                                {
                                    dgvfixtures.Items.Add(obj);
                                }
                            }
                        }
                    }

                    else if (txtSearch1.Text == "")
                    {


                        List<Fixture> lstselecteddiv = lbxFixtures.Where(p => p.DivisionId.ToString() == selectedgridvalue.LoadDivisionId).OrderBy(p => p.MatchId).ToList<Fixture>();
                        string str = txtSearch1.Text.ToString().ToUpper();

                        foreach (Fixture obj in lstselecteddiv)
                        {
                            if (obj.Teams != null)
                            {
                                if (obj.Teams.Contains(str) == true)
                                {
                                    dgvfixtures.Items.Add(obj);
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

        private void cbxdivision_DropDownOpened(object sender, EventArgs e)
        {
            if(cbxseason.SelectedIndex!=-1)
            { 

            cbxdivision.ItemsSource = null;
            cbxdivision.Items.Clear();
            string strseasons = "";
            strseasons = "select * from divisions";
            OleDbDataAdapter adpt11 = new OleDbDataAdapter(strseasons, oleconn);
            DataTable aa1 = new DataTable();
            adpt11.Fill(aa1);
            aa1.DefaultView.Sort = "DivisionName";
            cbxdivision.ItemsSource = aa1.DefaultView;
            cbxdivision.DisplayMemberPath = aa1.Columns["DivisionName"].ToString();
            cbxdivision.SelectedValuePath = aa1.Columns["DivisionId"].ToString();
            }

            else
            {
                MessageBox.Show("Select Season");
            }
            //}
        }

        private void ok_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbxNoOfGroups.SelectedItem == null)
                {
                    MessageBox.Show("Select Number Of Groups !!!");
                }
                else if (cbxdivision.SelectedItem == null)
                {
                    MessageBox.Show("Select Division !!!");
                }
                else
                {
                    season_zone_div.name[0] = cbxseason.Text;
                    season_zone_div.name[1] = cbxzone.Text;
                    season_zone_div.name[2] = cbxdivision.Text;

                    btntransfer.IsEnabled = true;
                    btnreverse.IsEnabled = true;
                    btngeneratefix.IsEnabled = true;
                    btnSave.IsEnabled = true;

                    lbxteams.ItemsSource = null;
                    lbxteams.Items.Clear();

                    ObservableCollection<DivisionLoad> lstDivisionLoad = Database.GetEntityList<DivisionLoad>(false, false, false, Database.getReadConnection(), "RecordStatus", "DivisionLoadId");
                    var objvalues = from s in lstDivisionLoad where s.SeasonId.ToString() == cbxseason.SelectedValue.ToString() select s;
                    List<DivisionLoad> lstselecteddiv = objvalues.ToList<DivisionLoad>();

                    Season objSeason = Database.GetEntity<Season>(Guid.Parse(cbxseason.SelectedValue.ToString()), false, false, false, Database.getReadConnection());

                    if (cbxdivision.SelectedIndex == 0)
                    {
                        if (lstselecteddiv.Count > 0)
                        {
                            foreach (var firstdiv in lstselecteddiv)
                            {
                                if (firstdiv.DivisionOne != null)
                                {
                                    lbxteams.Items.Add(firstdiv.DivisionOne);
                                }
                            }
                        }
                    }

                    if (cbxdivision.SelectedIndex == 1)
                    {
                        if (lstselecteddiv.Count > 0)
                        {
                            foreach (var firstdiv in lstselecteddiv)
                            {
                                if (firstdiv.DivisionTwo != null)
                                {
                                    lbxteams.Items.Add(firstdiv.DivisionTwo);
                                }
                            }
                        }
                    }

                    else if (cbxdivision.SelectedIndex == 2)
                    {
                        if (lstselecteddiv.Count > 0)
                        {
                            foreach (var firstdiv in lstselecteddiv)
                            {
                                if (firstdiv.DivisionThree != null)
                                {
                                    lbxteams.Items.Add(firstdiv.DivisionThree);
                                }
                            }
                        }
                    }



                    if (cbxNoOfGroups.SelectedIndex == 0)
                    {
                        rbtnGroupA.Visibility = Visibility.Visible;
                        rbtnGroupB.Visibility = Visibility.Hidden;
                        rbtnGroupC.Visibility = Visibility.Hidden;
                        rbtnGroupD.Visibility = Visibility.Hidden;

                        lbxGroupA.Visibility = Visibility.Visible;
                        lbxGroupB.Visibility = Visibility.Hidden;
                        lbxGroupC.Visibility = Visibility.Hidden;
                        lbxGroupD.Visibility = Visibility.Hidden;

                    }

                    else if (cbxNoOfGroups.SelectedIndex == 1)
                    {
                        rbtnGroupA.Visibility = Visibility.Visible;
                        rbtnGroupB.Visibility = Visibility.Visible;
                        rbtnGroupC.Visibility = Visibility.Hidden;
                        rbtnGroupD.Visibility = Visibility.Hidden;

                        lbxGroupA.Visibility = Visibility.Visible;
                        lbxGroupB.Visibility = Visibility.Visible;
                        lbxGroupC.Visibility = Visibility.Hidden;
                        lbxGroupD.Visibility = Visibility.Hidden;
                    }
                    else if (cbxNoOfGroups.SelectedIndex == 2)
                    {
                        rbtnGroupA.Visibility = Visibility.Visible;
                        rbtnGroupB.Visibility = Visibility.Visible;
                        rbtnGroupC.Visibility = Visibility.Visible;
                        rbtnGroupD.Visibility = Visibility.Hidden;

                        lbxGroupA.Visibility = Visibility.Visible;
                        lbxGroupB.Visibility = Visibility.Visible;
                        lbxGroupC.Visibility = Visibility.Visible;
                        lbxGroupD.Visibility = Visibility.Hidden;
                    }
                    else if (cbxNoOfGroups.SelectedIndex == 3)
                    {
                        rbtnGroupA.Visibility = Visibility.Visible;
                        rbtnGroupB.Visibility = Visibility.Visible;
                        rbtnGroupC.Visibility = Visibility.Visible;
                        rbtnGroupD.Visibility = Visibility.Visible;

                        lbxGroupA.Visibility = Visibility.Visible;
                        lbxGroupB.Visibility = Visibility.Visible;
                        lbxGroupC.Visibility = Visibility.Visible;
                        lbxGroupD.Visibility = Visibility.Visible;
                    }



                    Season objSeason1 = Database.GetEntity<Season>(Guid.Parse(cbxseason.SelectedValue.ToString()), false, false, false, oleconn);
                    ObservableCollection<GroupTeamValues> lstGroupTeamValues = Database.GetEntityList<GroupTeamValues>(false, false, false, oleconn, "RecordStatus", "GroupTeamValuesId");


                    if (objSeason1.SeasonId.ToString() == cbxseason.SelectedValue.ToString().ToString())
                    {
                        load();

                        lbxGroupA.Items.Clear();
                        lbxGroupB.Items.Clear();
                        lbxGroupC.Items.Clear();
                        lbxGroupD.Items.Clear();
                        foreach (var objGroupTeamValues in lstGroupTeamValues)
                        {

                            // if (objDivision.SeasonId.ToString() == cbxseason.SelectedItem.ToString())
                            if (objGroupTeamValues.SeasonId.ToString() == cbxseason.SelectedValue.ToString())
                            {
                                if (objGroupTeamValues.DivisionId.ToString() == cbxdivision.SelectedValue.ToString())
                                {
                                    if (objGroupTeamValues.NoOfGroups == cbxNoOfGroups.Text)
                                    {
                                        btntransfer.IsEnabled = false;
                                        btnreverse.IsEnabled = false;
                                        btnSave.IsEnabled = false;
                                        btngeneratefix.IsEnabled = false;
                                        // lbxTeamLoad.IsEnabled = false;
                                        if (objGroupTeamValues.GroupA != null)
                                        {
                                            lbxGroupA.Items.Add(objGroupTeamValues.GroupA);
                                        }


                                        if (objGroupTeamValues.GroupB != null)
                                        {

                                            lbxGroupB.Items.Add(objGroupTeamValues.GroupB);

                                        }

                                        if (objGroupTeamValues.GroupC != null)
                                        {

                                            lbxGroupC.Items.Add(objGroupTeamValues.GroupC);

                                        }

                                        if (objGroupTeamValues.GroupD != null)
                                        {

                                            lbxGroupD.Items.Add(objGroupTeamValues.GroupD);

                                        }
                                    }
                                }

                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Fixture Are Not Generated.........Click Generate");


                        dgvfixtures.ItemsSource = null;
                        lbxteams.IsEnabled = true;

                        if (objSeason1.SeasonId.ToString() == cbxseason.SelectedValue.ToString().ToString())
                        {
                            load();

                            lbxGroupA.Items.Clear();
                            lbxGroupB.Items.Clear();
                            lbxGroupC.Items.Clear();
                            lbxGroupD.Items.Clear();
                            foreach (var objGroupTeamValues in lstGroupTeamValues)
                            {

                                // if (objDivision.SeasonId.ToString() == cbxseason.SelectedItem.ToString())
                                if (objGroupTeamValues.SeasonId.ToString() == cbxseason.SelectedValue.ToString())
                                {
                                    if (objGroupTeamValues.DivisionId.ToString() == cbxdivision.SelectedValue.ToString())
                                    {
                                        if (objGroupTeamValues.NoOfGroups == cbxNoOfGroups.Text)
                                        {
                                            cbxNoOfGroups.IsEnabled = true;
                                            ok.IsEnabled = true;
                                            btntransfer.IsEnabled = false;
                                            btnreverse.IsEnabled = false;
                                            btnSave.IsEnabled = false;
                                            btngeneratefix.IsEnabled = false;
                                            // lbxTeamLoad.IsEnabled = false;
                                            if (objGroupTeamValues.GroupA != null)
                                            {
                                                lbxGroupA.Items.Add(objGroupTeamValues.GroupA);
                                            }


                                            if (objGroupTeamValues.GroupB != null)
                                            {

                                                lbxGroupB.Items.Add(objGroupTeamValues.GroupB);

                                            }

                                            if (objGroupTeamValues.GroupC != null)
                                            {

                                                lbxGroupC.Items.Add(objGroupTeamValues.GroupC);

                                            }

                                            if (objGroupTeamValues.GroupD != null)
                                            {

                                                lbxGroupD.Items.Add(objGroupTeamValues.GroupD);

                                            }
                                        }
                                    }

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

        private void UmpiresAssign_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void StartScoring_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cbxseason_DropDownOpened(object sender, EventArgs e)
        {
           
            cbxseason.ItemsSource = null;
            cbxseason.Items.Clear();
            string strseasons = "";
            strseasons = "select * from seasons";
            OleDbDataAdapter adpt11 = new OleDbDataAdapter(strseasons, oleconn);
            DataTable aa1 = new DataTable();
            adpt11.Fill(aa1);
            aa1.DefaultView.Sort = "SeasonType";
            cbxseason.ItemsSource = aa1.DefaultView;
            cbxseason.DisplayMemberPath = aa1.Columns["SeasonType"].ToString();
            cbxseason.SelectedValuePath = aa1.Columns["SeasonId"].ToString();
           
        }

        private void cbxzone_DropDownOpened(object sender, EventArgs e)
        {
            //string strZones = "";
            //strZones = "select * from zones";



            //OleDbDataAdapter adpt1 = new OleDbDataAdapter(strZones, oleconn);
            //DataTable aa = new DataTable();
            //adpt1.Fill(aa);

            //foreach (DataRowView dr1 in aa.DefaultView)
            //{

            //    if (!cbxzone.Items.Contains(dr1["ZoneName"]))  // To remove list duplicacy
            //    {
            //        cbxzone.Items.Add(dr1["ZoneName"]);
            //    }
            //}

            //string strseasons = "";
            //strseasons = "select * from Zones";
            //OleDbDataAdapter adpt11 = new OleDbDataAdapter(strseasons, oleconn);
            //DataTable aa1 = new DataTable();
            //adpt11.Fill(aa1);
            //aa1.DefaultView.Sort = "ZoneName";
            //cbxzone.ItemsSource = aa1.DefaultView;
            //cbxzone.DisplayMemberPath = aa1.Columns["ZoneName"].ToString();
            //cbxzone.SelectedValuePath = aa1.Columns["ZoneId"].ToString();

           // cbxzone.SelectedIndex = 0;
        }

        private void cbxdivision_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            clearall();

         
        }

        private void cbxseason_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            clearall();
            cbxdivision.SelectedIndex = -1;
            
        }

        

        private void cbxNoOfGroups_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lbxGroupA.ItemsSource = null;
            lbxGroupA.Items.Clear();
            lbxGroupB.ItemsSource = null;
            lbxGroupB.Items.Clear();
            lbxGroupC.ItemsSource = null;
            lbxGroupC.Items.Clear();
            lbxGroupD.ItemsSource = null;
            lbxGroupD.Items.Clear();

        }

        private void cbxdivision_DropDownOpened_1(object sender, EventArgs e)
        {
            if (cbxseason.SelectedIndex != -1)
            {

                string strseasons = "";
                strseasons = "select * from divisions";
                OleDbDataAdapter adpt11 = new OleDbDataAdapter(strseasons, oleconn);
                DataTable aa1 = new DataTable();
                adpt11.Fill(aa1);
                aa1.DefaultView.Sort = "DivisionName";
                cbxdivision.ItemsSource = aa1.DefaultView;
                cbxdivision.DisplayMemberPath = aa1.Columns["DivisionName"].ToString();
                cbxdivision.SelectedValuePath = aa1.Columns["DivisionId"].ToString(); 
            }

            else
            {
                MessageBox.Show("Select Season");
            }
        }

        private void cbxdivision_DropDownClosed(object sender, EventArgs e)
        {

            lbxGroupA.ItemsSource = null;
            lbxGroupB.ItemsSource = null;
            lbxGroupC.ItemsSource = null;
            lbxGroupD.ItemsSource = null;
            

            if (cbxdivision.SelectedValue == null)
            {

            }
            else
            {
                //if(cbxseason.SelectedIndex==-1)
                //{ }
             
                ObservableCollection<GroupTeamValues> lstGroupTeamValues = Database.GetEntityList<GroupTeamValues>(false, false, false, oleconn, "RecordStatus", "GroupTeamValuesId");

                ObservableCollection<Fixture> lbxFixturesssssssss = Database.GetEntityList<Fixture>(false, false, false, oleconn, "RecordStatus", "SerialNo");
                ObservableCollection<DivisionLoad> lstDivisionLoad = Database.GetEntityList<DivisionLoad>(false, false, false, Database.getReadConnection(), "RecordStatus", "DivisionLoadId");

                cbxNoOfGroups.IsEnabled = true;
                ok.IsEnabled = true;
                var objfixture = from s in lbxFixturesssssssss where s.SeasonId.ToString() == cbxseason.SelectedValue.ToString() && s.DivisionId.ToString() == cbxdivision.SelectedValue.ToString() select s;
                List<Fixture> lstfixturefilter = objfixture.ToList<Fixture>();

                var objgroupfixture = lstGroupTeamValues.Where(u => lstfixturefilter.All(p => p.DivisionId.ToString() == u.DivisionId.ToString() && p.SeasonId.ToString() == u.SeasonId.ToString()));
                List<GroupTeamValues> lstgroupSort = objgroupfixture.ToList<GroupTeamValues>();


                season_zone_div.name[0] = cbxseason.Text;
                season_zone_div.name[1] = cbxzone.Text;
                season_zone_div.name[2] = cbxdivision.Text;


                lbxteams.ItemsSource = null;
                lbxteams.Items.Clear();
               
                var objvalues = from s in lstDivisionLoad where s.SeasonId.ToString() == cbxseason.SelectedValue.ToString() select s;
                List<DivisionLoad> lstselecteddiv1 = objvalues.ToList<DivisionLoad>();

                Season objSeason = Database.GetEntity<Season>(Guid.Parse(cbxseason.SelectedValue.ToString()), false, false, false, Database.getReadConnection());

            

                if (cbxdivision.SelectedIndex == 0)
                {
                    if (lstselecteddiv1.Count > 0)
                    {
                        foreach (var firstdiv in lstselecteddiv1)
                        {

                            if (firstdiv.DivisionOne != null)
                            {
                                lbxteams.Items.Add(firstdiv.DivisionOne);
                                cbxNoOfGroups.IsEnabled = true;
                                ok.IsEnabled = true;
                                lbxteams.IsEnabled = true;
                                

                            }
                        }
                    }
                }

                if (cbxdivision.SelectedIndex == 1)
                {
                    if (lstselecteddiv1.Count > 0)
                    {
                        foreach (var firstdiv in lstselecteddiv1)
                        {
                            if (firstdiv.DivisionTwo != null)
                            {
                                lbxteams.Items.Add(firstdiv.DivisionTwo);
                                cbxNoOfGroups.IsEnabled = true;
                                ok.IsEnabled = true;
                                lbxteams.IsEnabled = true;
                            }
                        }
                    }
                }

                else if (cbxdivision.SelectedIndex == 2)
                {
                    if (lstselecteddiv1.Count > 0)
                    {
                        foreach (var firstdiv in lstselecteddiv1)
                        {
                            if (firstdiv.DivisionThree != null)
                            {
                                lbxteams.Items.Add(firstdiv.DivisionThree);
                                cbxNoOfGroups.IsEnabled = true;
                                ok.IsEnabled = true;
                                lbxteams.IsEnabled = true;
                            }
                        }
                    }
                }

                //
                ObservableCollection<ListBoxSave> lbxListBoxSave = Database.GetEntityList<ListBoxSave>(false, false, false, oleconn, "RecordStatus", "ListBoxSaveId");


                Season objSeason1 = Database.GetEntity<Season>(Guid.Parse(cbxseason.SelectedValue.ToString().ToString()), false, false, false, oleconn);

                if (objSeason1.SeasonId.ToString() == cbxseason.SelectedValue.ToString().ToString())
                {
                    lbxGroupA.Items.Clear();
                    lbxGroupB.Items.Clear();
                    lbxGroupC.Items.Clear();
                    lbxGroupD.Items.Clear();
                    // Division objDivision = Database.GetEntity<Division>(Guid.Parse(cbxdivision.SelectedValue.ToString()), false, false, false, oleconn);
                    List<Fixture> lstselecteddiv = lbxFixturesssssssss.Where(p => p.DivisionId.ToString() == cbxdivision.SelectedValue.ToString()).OrderBy(p => p.MatchId).ToList<Fixture>();

                    if (lstselecteddiv.Count > 0)
                    {

                        dgvfixtures.ItemsSource = null;
                        dgvfixtures.Items.Clear();
                        foreach (Fixture obj in lstselecteddiv)
                        {

                          
                            if (obj.SeasonId.ToString() == cbxseason.SelectedValue.ToString().ToString())
                            {
                                if (obj.DivisionId.ToString() == cbxdivision.SelectedValue.ToString())
                                {
                                   // lbxteams.IsEnabled = false;

                                    foreach (ListBoxSave lbxtosave in lbxListBoxSave)
                                    {
                                        if (obj.MatchId == lbxtosave.MatchId)
                                        {
                                            if (obj.Group == "Semi-Final")
                                            {
                                                if (obj.SeasonId.ToString() == lbxtosave.SeasonId.ToString())
                                                {

                                                    string teamA = lbxtosave.TeamOneName;
                                                    string teamB = lbxtosave.TeamtwoName;

                                                    obj.Teams = teamA + " v/s " + teamB;
                                                    break;
                                                }
                                            }

                                            if (obj.Group == "Final")
                                            {
                                                if (obj.SeasonId.ToString() == lbxtosave.SeasonId.ToString())
                                                {

                                                    string teamA = lbxtosave.TeamOneName;
                                                    string teamB = lbxtosave.TeamtwoName;

                                                    obj.Teams = teamA + " v/s " + teamB;
                                                    break;
                                                }
                                            }
                                        }

                                    }
                                    dgvfixtures.Items.Add(obj);
                                    dgvfixtures.DataContext = obj;
                                    cbxNoOfGroups.IsEnabled = false;
                                    ok.IsEnabled = false;

                                    //

                                    if (lstgroupSort.Count > 0)
                                    {
                                        lbxGroupA.Items.Clear();
                                        lbxGroupB.Items.Clear();
                                        lbxGroupC.Items.Clear();
                                        lbxGroupD.Items.Clear();
                                        

                                        foreach (var objGroupTeamValues in lstgroupSort)
                                        {



                                            cbxNoOfGroups.Text = objGroupTeamValues.NoOfGroups;
                                            //

                                            if (cbxNoOfGroups.Text == objGroupTeamValues.NoOfGroups)
                                            {

                                                if (objGroupTeamValues.SeasonId.ToString() == cbxseason.SelectedValue.ToString())
                                                {
                                                    if (objGroupTeamValues.DivisionId.ToString() == cbxdivision.SelectedValue.ToString())
                                                    {


                                                        //  cbxNoOfGroups.SelectedValue = 0;
                                                        btntransfer.IsEnabled = false;
                                                        btnreverse.IsEnabled = false;
                                                        btnSave.IsEnabled = false;
                                                        btngeneratefix.IsEnabled = false;

                                                        if (cbxNoOfGroups.SelectedIndex == 0)
                                                        {
                                                            lbxGroupA.Visibility = Visibility.Visible;
                                                            rbtnGroupA.Visibility = Visibility.Visible;
                                                        }
                                                        else if (cbxNoOfGroups.SelectedIndex == 1)
                                                        {

                                                            lbxGroupA.Visibility = Visibility.Visible;
                                                            rbtnGroupA.Visibility = Visibility.Visible;

                                                            lbxGroupB.Visibility = Visibility.Visible;
                                                            rbtnGroupB.Visibility = Visibility.Visible;
                                                        }
                                                        else if (cbxNoOfGroups.SelectedIndex == 2)
                                                        {
                                                            lbxGroupA.Visibility = Visibility.Visible;
                                                            rbtnGroupA.Visibility = Visibility.Visible;

                                                            lbxGroupB.Visibility = Visibility.Visible;
                                                            rbtnGroupB.Visibility = Visibility.Visible;

                                                            lbxGroupC.Visibility = Visibility.Visible;
                                                            rbtnGroupC.Visibility = Visibility.Visible;
                                                        }

                                                        else
                                                        {
                                                            lbxGroupA.Visibility = Visibility.Visible;
                                                            rbtnGroupA.Visibility = Visibility.Visible;

                                                            lbxGroupB.Visibility = Visibility.Visible;
                                                            rbtnGroupB.Visibility = Visibility.Visible;

                                                            lbxGroupC.Visibility = Visibility.Visible;
                                                            rbtnGroupC.Visibility = Visibility.Visible;

                                                            lbxGroupD.Visibility = Visibility.Visible;
                                                            rbtnGroupD.Visibility = Visibility.Visible;
                                                        }
                                                      
                                                        // lbxTeamLoad.IsEnabled = false;
                                                        if (objGroupTeamValues.GroupA != null)
                                                        {
                                                            lbxGroupA.Items.Add(objGroupTeamValues.GroupA);
                                                            lbxGroupA.Visibility = Visibility.Visible;
                                                        }


                                                        if (objGroupTeamValues.GroupB != null)
                                                        {

                                                            lbxGroupB.Items.Add(objGroupTeamValues.GroupB);
                                                            lbxGroupB.Visibility = Visibility.Visible;
                                                        }

                                                        if (objGroupTeamValues.GroupC != null)
                                                        {

                                                            lbxGroupC.Items.Add(objGroupTeamValues.GroupC);
                                                            lbxGroupC.Visibility = Visibility.Visible;
                                                        }

                                                        if (objGroupTeamValues.GroupD != null)
                                                        {

                                                            lbxGroupD.Items.Add(objGroupTeamValues.GroupD);
                                                            lbxGroupD.Visibility = Visibility.Visible;
                                                        }
                                                    }

                                                }
                                            }
                                        }
                                    }

                                    cbxNoOfGroups.IsEnabled = false;
                                    ok.IsEnabled = false;
                               

                                }

                                if (obj.DivisionId.ToString() != cbxdivision.SelectedValue.ToString())
                                {

                                    dgvfixtures.ItemsSource = null;
                                    lbxteams.IsEnabled = true;
                                    cbxNoOfGroups.IsEnabled = true;
                                    ok.IsEnabled = true;

                                    if (objSeason1.SeasonId.ToString() == cbxseason.SelectedValue.ToString().ToString())
                                    {
                                        load();
                                       
                                        foreach (var objGroupTeamValues in lstGroupTeamValues)
                                        {

                                            // if (objDivision.SeasonId.ToString() == cbxseason.SelectedItem.ToString())
                                            if (objGroupTeamValues.SeasonId.ToString() == cbxseason.SelectedValue.ToString())
                                            {
                                                if (objGroupTeamValues.DivisionId.ToString() == cbxdivision.SelectedValue.ToString())
                                                {
                                                    if (objGroupTeamValues.NoOfGroups == cbxNoOfGroups.Text)
                                                    {
                                                        cbxNoOfGroups.IsEnabled = true;
                                                        ok.IsEnabled = true;
                                                        btntransfer.IsEnabled = false;
                                                        btnreverse.IsEnabled = false;
                                                        btnSave.IsEnabled = false;
                                                        btngeneratefix.IsEnabled = false;

                                                        lbxGroupA.Items.Clear();
                                                        lbxGroupB.Items.Clear();
                                                        lbxGroupC.Items.Clear();
                                                        lbxGroupD.Items.Clear();
                                                        if (objGroupTeamValues.GroupA != null)
                                                        {
                                                            lbxGroupA.Items.Add(objGroupTeamValues.GroupA);
                                                        }


                                                        if (objGroupTeamValues.GroupB != null)
                                                        {

                                                            lbxGroupB.Items.Add(objGroupTeamValues.GroupB);

                                                        }

                                                        if (objGroupTeamValues.GroupC != null)
                                                        {

                                                            lbxGroupC.Items.Add(objGroupTeamValues.GroupC);

                                                        }

                                                        if (objGroupTeamValues.GroupD != null)
                                                        {

                                                            lbxGroupD.Items.Add(objGroupTeamValues.GroupD);

                                                        }
                                                    }
                                                }

                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                lbxFixturesssssssss.Clear();
                lstDivisionLoad.Clear();
                lstGroupTeamValues.Clear();
            }
        }

        public void clearall()
        {
            cbxNoOfGroups.SelectedIndex = -1;
            lbxteams.Items.Clear();
            dgvfixtures.ItemsSource = null;
            dgvfixtures.Items.Clear();
            lbxGroupA.Visibility = Visibility.Hidden;
            lbxGroupB.Visibility = Visibility.Hidden;
            lbxGroupC.Visibility = Visibility.Hidden;
            lbxGroupD.Visibility = Visibility.Hidden;
            rbtnGroupA.Visibility = Visibility.Hidden;
            rbtnGroupB.Visibility = Visibility.Hidden;
            rbtnGroupC.Visibility = Visibility.Hidden;
            rbtnGroupD.Visibility = Visibility.Hidden;
        }

        private void btnback_Click(object sender, RoutedEventArgs e)
        {
            lbxGroupA.Items.Clear();
            lbxGroupB.Items.Clear();
            lbxGroupC.Items.Clear();
            lbxGroupD.Items.Clear();
            cbxNoOfGroups.SelectedIndex = -1;
            lbxteams.Items.Clear();
            lbxGroupA.Visibility = Visibility.Hidden;
            lbxGroupB.Visibility = Visibility.Hidden;
            lbxGroupC.Visibility = Visibility.Hidden;
            lbxGroupD.Visibility = Visibility.Hidden;
            rbtnGroupA.Visibility = Visibility.Hidden;
            rbtnGroupB.Visibility = Visibility.Hidden;
            rbtnGroupC.Visibility = Visibility.Hidden;
            rbtnGroupD.Visibility = Visibility.Hidden;
            dgvfixtures.ItemsSource = null;

            Button btn = (Button)sender;
            btn.Command = NavigationCommands.GoToPage;
            btn.CommandParameter = "/Pages/Home.xaml";
        }

        
    }

    public static class selectedgridvalue
    {
        public static string selectedstring;
        public static string LoadDivision;
        public static string matchid;
        public static string group;
        public static string LoadDivisionId;
        public static string _fixtureid;
        public static string _seasonid;
    }

    public static class season_zone_div
    {
        public static string[] name = new string[3];
        public static int count = 0;

    }

    public static class division
    {
        public static int num;
    }

    public static class loadmatchid
    {
        public static string loadmatchidab;
    }

    public static class NameOf
    {
        public static string Venue;
        public static string Umpire1;
        public static string Umpire2;
        public static string Scorer;
    }

    public static class temp
    {
        public static string stratdate;
        public static string enddate;
        public static string matchtype;
        //public static string matchid;

    }

    public static class TempValues
    {
        public static string[] teamname = new string[60];
        public static int index = 0;
        public static string LoadSeason;
        public static string SeasonName;
        public static string LoadZone;
        public static string LoadZoneId;
        public static string LoadSeasonId;
    }

    public static class tempvalues
    {
        public static string teamA;
        public static string teamB;

    }

 
}
