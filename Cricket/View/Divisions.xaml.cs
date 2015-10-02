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

using CricketSol;
using CricketSol.Base;
using CricketSol.DAL;
using CricketSol.Database;
using CricketSol.System;
using System.Configuration;

using System.Collections.ObjectModel;
using System.Collections;

namespace Cricket.View
{
    /// <summary>
    /// Interaction logic for Divisions.xaml
    /// </summary>
    public partial class Divisions : UserControl
    {
        OleDbConnection oleconn = Database.getConnection();

        public Divisions()
        {
            InitializeComponent();

            cbxzone.SelectedIndex = 0;
            ObservableCollection<Team> lbx = Database.GetEntityList<Team>(false, true, true, oleconn, "RecordStatus='Added'", "TeamName");
            int count = lbx.Count;
            int i;

            for (i = 0; i <= count - 1; i++)
            {
                lbxTeamLoad.Items.Add(lbx[i].TeamName);
            }
        }



        private void btnfixtures_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbxseason.SelectedIndex != -1)
                {
                    if (lbxdiv1.Items.Count != 0 || lbxdiv2.Items.Count != 0 || lbxdiv3.Items.Count != 0)
                    {
                        LoadDivision.count = 0;
                        LoadDivision1.count = 0;
                        LoadDivision2.count = 0;

                        foreach (var div1 in lbxdiv1.Items)
                        {
                            LoadDivision.teamname[LoadDivision.count] = div1.ToString();
                            LoadDivision.count++;
                        }

                        foreach (var div2 in lbxdiv2.Items)
                        {
                            LoadDivision1.teamname[LoadDivision1.count] = div2.ToString();
                            LoadDivision1.count++;
                        }

                        foreach (var div3 in lbxdiv3.Items)
                        {
                            LoadDivision2.teamname[LoadDivision2.count] = div3.ToString();
                            LoadDivision2.count++;
                        }

                        TempValues.LoadSeason = cbxseason.SelectedValue.ToString();

                        TempValues.SeasonName = cbxseason.Text;
                        TempValues.LoadZone = cbxzone.Text;
                        TempValues.LoadZone = cbxzone.SelectedValue.ToString();
                        TempValues.LoadSeasonId = cbxseason.SelectedValue.ToString();
                        Button btn = (Button)sender;
                        btn.Command = NavigationCommands.GoToPage;
                        btn.CommandParameter = "/View/formFixtures.xaml";
                    }
                    else
                    {
                        MessageBox.Show("Divisions are Empty");
                    }
                }
                else
                {
                    MessageBox.Show("Select Season");
                }


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

                Season objSeason = Database.GetEntity<Season>(Guid.Parse(cbxseason.SelectedValue.ToString()), false, false, false, oleconn);
                ObservableCollection<DivisionLoad> lbxFixtures = Database.GetEntityList<DivisionLoad>(false, false, false, oleconn, "RecordStatus='Added'", "DivisionLoadId");
                ObservableCollection<Division> lbxDivision = Database.GetEntityList<Division>(false, false, false, oleconn, "RecordStatus", "DivisionId");


                if (lbxFixtures.Any(str => str.SeasonId.ToString() == cbxseason.SelectedValue.ToString()))
                {

                    MessageBox.Show("Value Already Exists");
                }

                else
                {
                    if (lbxdiv1.Items.Count > 0)
                    {

                        foreach (var abc in lbxdiv1.Items)
                        {
                            DivisionLoad objLoad = Database.GetNewEntity<DivisionLoad>();
                            objLoad.DivisionOne = abc.ToString();
                            objLoad.Season = objSeason;

                            Database.SaveEntity<DivisionLoad>(objLoad, oleconn);
                        }
                    }

                    if (lbxdiv2.Items.Count > 0)
                    {
                        foreach (var abc in lbxdiv2.Items)
                        {
                            DivisionLoad objLoad = Database.GetNewEntity<DivisionLoad>();
                            objLoad.DivisionTwo = abc.ToString();

                            //ObservableCollection<Season> obcseason = Database.GetEntityList<Season>(false, true, true, oleconn, "RecordStatus='Added'", "SeasonType");
                            objLoad.Season = objSeason;

                            Database.SaveEntity<DivisionLoad>(objLoad, oleconn);
                        }
                    }


                    if (lbxdiv3.Items.Count > 0)
                    {
                        foreach (var abc in lbxdiv3.Items)
                        {
                            DivisionLoad objLoad = Database.GetNewEntity<DivisionLoad>();
                            objLoad.DivisionThree = abc.ToString();

                            //ObservableCollection<Season> obcseason = Database.GetEntityList<Season>(false, true, true, oleconn, "RecordStatus='Added'", "SeasonType");
                            objLoad.Season = objSeason;

                            Database.SaveEntity<DivisionLoad>(objLoad, oleconn);
                        }
                    }

                    if (lbxTeamLoad.Items.Count >= 0)
                    {
                        foreach (var abc in lbxTeamLoad.Items)
                        {
                            DivisionLoad objLoad = Database.GetNewEntity<DivisionLoad>();
                            objLoad.OnHold = abc.ToString();

                            // ObservableCollection<Season> obcseason = Database.GetEntityList<Season>(false, true, true, oleconn, "RecordStatus='Added'", "SeasonType");
                            objLoad.Season = objSeason;

                            Database.SaveEntity<DivisionLoad>(objLoad, oleconn);
                        }

                    }

                    MessageBox.Show("Saved for the Season = '" + cbxseason.Text + "'");


                    Button btn = (Button)sender;
                    btn.Command = NavigationCommands.GoToPage;
                    btn.CommandParameter = "/Pages/Home.xaml";


                    //lbxdiv1.Items.Clear();
                    //lbxdiv2.Items.Clear();
                    //lbxdiv3.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private void btnReverseTeams_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbxseason.SelectedIndex != -1)
                {
                    //Reverse from Div 1
                    if (rbtndiv1.IsChecked == true)
                    {

                        if (lbxdiv1.Items.Count != 0)
                        {
                            if (lbxdiv1.SelectedItem != null)
                            {
                                string abc = lbxdiv1.SelectedItem.ToString();
                                lbxTeamLoad.Items.Add(abc);
                                lbxdiv1.Items.Remove(abc);
                                lbxdiv1.SelectedIndex = 0;
                            }
                            else
                            {
                                MessageBox.Show("Select Team to Reverse from Div 1");
                            }


                        }

                    }

                    //Reverse from Div 2
                    else if (rbtndiv2.IsChecked == true)
                    {

                        if (lbxdiv2.Items.Count != 0)
                        {
                            if (lbxdiv2.SelectedItem != null)
                            {
                                string abc = lbxdiv2.SelectedItem.ToString();
                                lbxTeamLoad.Items.Add(abc);
                                lbxdiv2.Items.Remove(abc);
                                lbxdiv2.SelectedIndex = 0;
                            }
                            else
                            {
                                MessageBox.Show("Select Team to Reverse from Div 2");
                            }
                        }


                    }

                    //Reverse from Div 3
                    else if (rbtndiv3.IsChecked == true)
                    {
                        if (lbxdiv3.Items.Count != 0)
                        {
                            if (lbxdiv3.SelectedItem != null)
                            {
                                string abc = lbxdiv3.SelectedItem.ToString();
                                lbxTeamLoad.Items.Add(abc);
                                lbxdiv3.Items.Remove(abc);
                                lbxdiv3.SelectedIndex = 0;
                            }
                            else
                            {
                                MessageBox.Show("Select Team to Reverse from Div 3");
                            }
                        }


                    }
                    else
                    {
                        MessageBox.Show("Select Division to Transfer");
                    }
                }
                else
                {
                    MessageBox.Show("Select Season");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btnTtransferTeams_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbxseason.SelectedIndex != -1)
                {
                    //transfer to div 1 
                    if (rbtndiv1.IsChecked == true)
                    {

                        if (lbxTeamLoad.Items.Count != 0)
                        {
                            if (lbxTeamLoad.SelectedItem != null)
                            {
                                string abc = lbxTeamLoad.SelectedItem.ToString();
                                lbxdiv1.Items.Add(abc);
                                lbxTeamLoad.Items.Remove(abc);
                                lbxTeamLoad.SelectedIndex = 0;
                                lbxdiv1.SelectedIndex = 0;
                            }
                            else
                            {
                                MessageBox.Show("Select Players to Transfer from Teams");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Players List Is Empty");
                        }
                    }

                    //transfer to div 2
                    else if (rbtndiv2.IsChecked == true)
                    {

                        if (lbxTeamLoad.Items.Count != 0)
                        {
                            if (lbxTeamLoad.SelectedItem != null)
                            {
                                string abc = lbxTeamLoad.SelectedItem.ToString();
                                lbxdiv2.Items.Add(abc);
                                lbxTeamLoad.Items.Remove(abc);
                                lbxTeamLoad.SelectedIndex = 0;
                                lbxdiv2.SelectedIndex = 0;
                            }
                            else
                            {
                                MessageBox.Show("Select Players to Transfer from Teams");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Players List Is Empty");
                        }
                    }

                    //transfer to div 3
                    else if (rbtndiv3.IsChecked == true)
                    {

                        if (lbxTeamLoad.Items.Count != 0)
                        {
                            if (lbxTeamLoad.SelectedItem != null)
                            {
                                string abc = lbxTeamLoad.SelectedItem.ToString();
                                lbxdiv3.Items.Add(abc);
                                lbxTeamLoad.Items.Remove(abc);
                                lbxTeamLoad.SelectedIndex = 0;
                                lbxdiv3.SelectedIndex = 0;
                            }
                            else
                            {
                                MessageBox.Show("Select Teams to Transfer");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Teams List Is Empty");
                        }
                    }

                    else
                    {
                        MessageBox.Show("Select Division to Transfer");
                    }



                }
                else
                {
                    MessageBox.Show("Select Season");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void UserControl_Loaded_1(object sender, RoutedEventArgs e)
        {
            try
            {
                cbxseason.ItemsSource = null;
                lbxdiv1.ItemsSource = null;
                lbxdiv2.ItemsSource = null;
                lbxdiv3.ItemsSource = null;
                btnfixtures.Visibility = Visibility.Hidden;


                //Select Season
                string strRetrieve = "";

                strRetrieve = "select SeasonType,SeasonId from Seasons";
                OleDbDataAdapter adpt = new OleDbDataAdapter(strRetrieve, oleconn);
                DataSet ds = new DataSet();
                adpt.Fill(ds);

                cbxseason.ItemsSource = ds.Tables[0].DefaultView;
                ds.Tables[0].DefaultView.Sort = "SeasonType";
                cbxseason.DisplayMemberPath = ds.Tables[0].Columns["SeasonType"].ToString();
                cbxseason.SelectedValuePath = ds.Tables[0].Columns["SeasonId"].ToString();


                //Selects Zone As Tumkur
                string strZones = "";
                strZones = "select * from zones";



                OleDbDataAdapter adpt1 = new OleDbDataAdapter(strZones, oleconn);
                DataTable aa = new DataTable();
                adpt1.Fill(aa);

                foreach (DataRowView dr1 in aa.DefaultView)
                {

                    if (!cbxzone.Items.Contains(dr1["ZoneName"]))  // To remove list duplicacy
                    {
                        cbxzone.Items.Add(dr1["ZoneName"]);
                    }
                }
                cbxzone.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        private void cbxseason_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cbxseason.SelectedValue == null)
                {
                    // Validation message. 
                    //errorProvider1.SetError(comboBox1, "comboBox value is required.");
                }
                else
                {
                    cbxseason.Items.Refresh();
                    lbxdiv1.Items.Clear();
                    lbxdiv2.Items.Clear();
                    lbxdiv3.Items.Clear();

                    lbxTeamLoad.Items.Clear();

                    //lbxdiv1.IsEnabled = false;
                    //lbxdiv2.IsEnabled = false;
                    //lbxdiv3.IsEnabled = false;

                    //lbxTeamLoad.IsEnabled = false;
                    btnReverseTeams.IsEnabled = false;
                    btnTtransferTeams.IsEnabled = false;
                    btnSave.IsEnabled = false;
                    lbxTeamLoad.SelectedIndex = -1;
                    rbtndiv1.IsEnabled = false;
                    rbtndiv2.IsEnabled = false;
                    rbtndiv3.IsEnabled = false;

                    ObservableCollection<DivisionLoad> objDivisionLoad = Database.GetEntityList<DivisionLoad>(false, false, false, oleconn, "RecordStatus='Added'", "DivisionLoadId");
                    ObservableCollection<Team> lbx = Database.GetEntityList<Team>(false, true, true, oleconn, "RecordStatus='Added'", "TeamName");


                    foreach (DivisionLoad objDivision in objDivisionLoad)
                    {

                        // if (objDivision.SeasonId.ToString() == cbxseason.SelectedItem.ToString())
                        if (objDivision.SeasonId.ToString() == cbxseason.SelectedValue.ToString())
                        {

                            // lbxTeamLoad.IsEnabled = false;
                            if (objDivision.DivisionOne != null)
                            {
                                lbxdiv1.Items.Add(objDivision.DivisionOne);
                            }


                            if (objDivision.DivisionTwo != null)
                            {
                                if (objDivision.SeasonId.ToString() == cbxseason.SelectedValue.ToString())
                                {
                                    lbxdiv2.Items.Add(objDivision.DivisionTwo);
                                }
                            }

                            if (objDivision.DivisionThree != null)
                            {
                                if (objDivision.SeasonId.ToString() == cbxseason.SelectedValue.ToString())
                                {
                                    lbxdiv3.Items.Add(objDivision.DivisionThree);
                                }
                            }

                            if (objDivision.OnHold != null)
                            {
                                if (objDivision.SeasonId.ToString() == cbxseason.SelectedValue.ToString())
                                {
                                    lbxTeamLoad.Items.Add(objDivision.OnHold);
                                }
                            }

                        }
                    }

                    if (lbxdiv1.Items.Count == 0 && lbxdiv2.Items.Count == 0 && lbxdiv3.Items.Count == 0)
                    {
                        lbxdiv1.IsEnabled = true;
                        lbxdiv2.IsEnabled = true;
                        lbxdiv3.IsEnabled = true;

                        lbxTeamLoad.IsEnabled = true;

                        lbxTeamLoad.IsEnabled = true;
                        btnReverseTeams.IsEnabled = true;
                        btnTtransferTeams.IsEnabled = true;
                        btnSave.IsEnabled = true;

                        rbtndiv1.IsEnabled = true;
                        rbtndiv2.IsEnabled = true;
                        rbtndiv3.IsEnabled = true;

                        MessageBox.Show("No Record Available for " + cbxseason.Text);

                        int count = lbx.Count;
                        int i;

                        for (i = 0; i <= count - 1; i++)
                        {
                            lbxTeamLoad.Items.Add(lbx[i].TeamName);
                        }
                    }
                }
                //lbxTeamLoad.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        private void rbtndiv1_Click(object sender, RoutedEventArgs e)
        {
            lbxdiv1.SelectedIndex = 0;
            lbxdiv2.SelectedIndex = -1;
            lbxdiv3.SelectedIndex = -1;
        }

        private void rbtndiv2_Click(object sender, RoutedEventArgs e)
        {
            lbxdiv1.SelectedIndex = -1;
            lbxdiv2.SelectedIndex = 0;
            lbxdiv3.SelectedIndex = -1;
        }

        private void rbtndiv3_Click(object sender, RoutedEventArgs e)
        {
            lbxdiv1.SelectedIndex = -1;
            lbxdiv2.SelectedIndex = -1;
            lbxdiv3.SelectedIndex = 0;
        }

        private void txtsearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (txtsearch.Text != "")
                {
                    lbxTeamLoad.Items.Clear();
                    ObservableCollection<Team> objTeam = Database.GetEntityList<Team>(false, false, false, oleconn, "RecordStatus='Added'", "TeamId");
                    string str = txtsearch.Text.ToString().ToUpper();

                    foreach (Team obj in objTeam)
                    {
                        if (obj.TeamName != null)
                        {

                            if (obj.TeamName.Contains(str))
                            {
                                lbxTeamLoad.Items.Add(obj.TeamName);
                            }

                        }

                    }
                }

                else if (txtsearch.Text != "")
                {
                    lbxTeamLoad.Items.Clear();
                    ObservableCollection<Team> objTeam = Database.GetEntityList<Team>(false, false, false, oleconn, "RecordStatus='Added'", "TeamId");
                    string str = txtsearch.Text.ToString().ToUpper();

                    foreach (Team obj in objTeam)
                    {
                        if (obj.TeamName != null)
                        {

                            if (obj.TeamName.Contains(str))
                            {
                                lbxTeamLoad.Items.Add(obj.TeamName);
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

        //private void txtSearch1_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    try
        //    {
        //        lbxdiv1.Items.Clear();
        //        if (txtSearch1.Text != "")
        //        {


        //            ObservableCollection<DivisionLoad> objDivisionLoad = Database.GetEntityList<DivisionLoad>(false, false, false, oleconn, "RecordStatus='Added'", "DivisionLoadId");
        //            string str = txtSearch1.Text.ToString().ToUpper();

        //            foreach (DivisionLoad objDivision in objDivisionLoad)
        //            {
        //                if (objDivision.DivisionOne != null)
        //                {
        //                    if (objDivision.SeasonId.ToString() == cbxseason.SelectedValue.ToString())
        //                    {
        //                        if (objDivision.DivisionOne.Contains(str))
        //                            lbxdiv1.Items.Add(objDivision.DivisionOne);
        //                    }
        //                }

        //            }
        //        }

        //        else if (txtSearch1.Text == "")
        //        {


        //            ObservableCollection<DivisionLoad> objDivisionLoad = Database.GetEntityList<DivisionLoad>(false, false, false, oleconn, "RecordStatus='Added'", "DivisionLoadId");
        //            string str = txtSearch1.Text.ToString().ToUpper();

        //            foreach (DivisionLoad objDivision in objDivisionLoad)
        //            {
        //                if (objDivision.DivisionOne != null)
        //                {
        //                    if (objDivision.SeasonId.ToString() == cbxseason.SelectedValue.ToString())
        //                    {
        //                        if (objDivision.DivisionOne.Contains(str))
        //                            lbxdiv1.Items.Add(objDivision.DivisionOne);
        //                    }
        //                }

        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}



        //private void txtSearch2_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    try
        //    {
        //        if (txtSearch2.Text != "")
        //        {
        //            lbxdiv2.Items.Clear();
        //            ObservableCollection<DivisionLoad> objDivisionLoad = Database.GetEntityList<DivisionLoad>(false, false, false, oleconn, "RecordStatus='Added'", "DivisionLoadId");
        //            string str = txtSearch2.Text.ToString().ToUpper();

        //            foreach (DivisionLoad objDivision in objDivisionLoad)
        //            {
        //                if (objDivision.DivisionTwo != null)
        //                {
        //                    if (objDivision.SeasonId.ToString() == cbxseason.SelectedValue.ToString())
        //                    {
        //                        if (objDivision.DivisionTwo.Contains(str))
        //                            lbxdiv2.Items.Add(objDivision.DivisionTwo);
        //                    }
        //                }

        //            }
        //        }

        //        else if (txtSearch2.Text == "")
        //        {
        //            lbxdiv2.Items.Clear();
        //            ObservableCollection<DivisionLoad> objDivisionLoad = Database.GetEntityList<DivisionLoad>(false, false, false, oleconn, "RecordStatus='Added'", "DivisionLoadId");
        //            string str = txtSearch2.Text.ToString().ToUpper();

        //            foreach (DivisionLoad objDivision in objDivisionLoad)
        //            {
        //                if (objDivision.DivisionTwo != null)
        //                {
        //                    if (objDivision.SeasonId.ToString() == cbxseason.SelectedValue.ToString())
        //                    {
        //                        if (objDivision.DivisionTwo.Contains(str))
        //                            lbxdiv2.Items.Add(objDivision.DivisionTwo);
        //                    }
        //                }

        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //private void txtSearch3_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    try
        //    {
        //        if (txtSearch3.Text != "")
        //        {
        //            lbxdiv3.Items.Clear();
        //            ObservableCollection<DivisionLoad> objDivisionLoad = Database.GetEntityList<DivisionLoad>(false, false, false, oleconn, "RecordStatus='Added'", "DivisionLoadId");
        //            string str = txtSearch3.Text.ToString().ToUpper();

        //            foreach (DivisionLoad objDivision in objDivisionLoad)
        //            {
        //                if (objDivision.DivisionThree != null)
        //                {
        //                    if (objDivision.SeasonId.ToString() == cbxseason.SelectedValue.ToString())
        //                    {
        //                        if (objDivision.DivisionThree.Contains(str))
        //                            lbxdiv3.Items.Add(objDivision.DivisionThree);
        //                    }
        //                }

        //            }
        //        }

        //        else if (txtSearch3.Text != "")
        //        {
        //            lbxdiv3.Items.Clear();
        //            ObservableCollection<DivisionLoad> objDivisionLoad = Database.GetEntityList<DivisionLoad>(false, false, false, oleconn, "RecordStatus='Added'", "DivisionLoadId");
        //            string str = txtSearch3.Text.ToString().ToUpper();

        //            foreach (DivisionLoad objDivision in objDivisionLoad)
        //            {
        //                if (objDivision.DivisionThree != null)
        //                {
        //                    if (objDivision.SeasonId.ToString() == cbxseason.SelectedValue.ToString())
        //                    {
        //                        if (objDivision.DivisionThree.Contains(str))
        //                            lbxdiv3.Items.Add(objDivision.DivisionThree);
        //                    }
        //                }

        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //private void txtSearch4_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    try
        //    {
        //        if (txtSearch4.Text != "")
        //        {
        //            lbxTeamLoad.Items.Clear();
        //            ObservableCollection<Team> objTeam = Database.GetEntityList<Team>(false, false, false, oleconn, "RecordStatus='Added'", "TeamId");
        //            string str = txtSearch4.Text.ToString().ToUpper();

        //            foreach (Team obj in objTeam)
        //            {
        //                if (obj.TeamName != null)
        //                {

        //                    if (obj.TeamName.Contains(str))
        //                    {
        //                        lbxTeamLoad.Items.Add(obj.TeamName);
        //                    }

        //                }

        //            }
        //        }

        //        else if (txtSearch4.Text != "")
        //        {
        //            lbxTeamLoad.Items.Clear();
        //            ObservableCollection<Team> objTeam = Database.GetEntityList<Team>(false, false, false, oleconn, "RecordStatus='Added'", "TeamId");
        //            string str = txtSearch4.Text.ToString().ToUpper();

        //            foreach (Team obj in objTeam)
        //            {
        //                if (obj.TeamName != null)
        //                {

        //                    if (obj.TeamName.Contains(str))
        //                    {
        //                        lbxTeamLoad.Items.Add(obj.TeamName);
        //                    }

        //                }

        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

    }

    public static class LoadDivision
    {
        public static string[] teamname = new string[60];
        public static int count = 0;
    }

    public static class LoadDivision1
    {
        public static string[] teamname = new string[60];
        public static int count = 0;
    }

    public static class LoadDivision2
    {
        public static string[] teamname = new string[60];
        public static int count = 0;
    }



}