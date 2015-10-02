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
using System.Data;
using System.Data.OleDb;
using System.Configuration;
using System.Collections.ObjectModel;


namespace Cricket.View
{
    /// <summary>
    /// Interaction logic for MatchData.xaml
    /// </summary>
    public partial class MatchData : UserControl
    {
        public MatchData()
        {
            InitializeComponent();

        }

        private void btnteam_Click(object sender, RoutedEventArgs e)
        {


        }

        private void cbxzone_DropDownOpened(object sender, EventArgs e)
        {
            try
            {
                if (cbxseason.SelectedItem == null)
                {
                    MessageBox.Show("Select Season First");
                }

                string strZones = "";
                strZones = "select * from zones";

                OleDbDataAdapter adpt1 = new OleDbDataAdapter(strZones, Database.getConnection());
                DataTable aa = new DataTable();
                adpt1.Fill(aa);

                foreach (DataRowView dr1 in aa.DefaultView)
                {

                    if (!cbxzone.Items.Contains(dr1["ZoneName"]))// For remove list duplicacy
                    {
                        cbxzone.Items.Add(dr1["ZoneName"]);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void cbxteam_DropDownOpened(object sender, EventArgs e)
        {
            try
            {

                string strRetrieve = "";
                strRetrieve = "select * from teams";

                OleDbDataAdapter adpt = new OleDbDataAdapter(strRetrieve, Database.getConnection());
                // cbxteam.Items.Clear();
                DataTable dt = new DataTable();
                adpt.Fill(dt);


                foreach (DataRowView dr in dt.DefaultView)
                {
                    //   List<Team> lstFilter = Database.GetEntityList<Team>( false, false,false,Database.getConnection(),RecordStatus true);
                    //cbxteam.Items.Add(dr["TeamName"]);
                    //cbxteam.Items.Refresh();
                    if (!cbxteam.Items.Contains(dr["TeamName"]))// For remove list duplicacy
                    {
                        cbxteam.Items.Add(dr["TeamName"]);
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void cbxclub_DropDownOpened(object sender, EventArgs e)
        {
            try
            {
                if (cbxseason.SelectedItem == null || cbxzone.SelectedItem == null || cbxdivision.SelectedItem == null)
                {
                    MessageBox.Show("Select Season, Zone, Division");
                }

                string strRetrieve = "";
                strRetrieve = "select * from clubdetails";
                //  cbxclub.Items.Clear();
                OleDbDataAdapter adpt = new OleDbDataAdapter(strRetrieve, Database.getConnection());
                DataTable dt = new DataTable();
                adpt.Fill(dt);


                foreach (DataRowView dr in dt.DefaultView)
                {
                    //   List<Team> lstFilter = Database.GetEntityList<Team>( false, false,false,Database.getConnection(),RecordStatus true);
                    //cbxclub.Items.Add(dr["ClubName"]);
                    //cbxteam.Items.Refresh();

                    if (!cbxclub.Items.Contains(dr["ClubName"]))// For remove list duplicacy
                    {
                        cbxclub.Items.Add(dr["ClubName"]);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void cbxlocation_DropDownOpened(object sender, EventArgs e)
        {
            try
            {
                string strRetrieve = "";
                strRetrieve = "select * from locations";

                OleDbDataAdapter adpt = new OleDbDataAdapter(strRetrieve, Database.getConnection());

                // cbxlocation.Items.Clear();
                DataTable dt = new DataTable();
                adpt.Fill(dt);


                foreach (DataRowView dr in dt.DefaultView)
                {
                    //   List<Team> lstFilter = Database.GetEntityList<Team>( false, false,false,Database.getConnection(),RecordStatus true);
                    //cbxlocation.Items.Add(dr["LocationName"]);
                    //cbxlocation.Items.Refresh();

                    if (!cbxlocation.Items.Contains(dr["LocationName"]))// For remove list duplicacy
                    {
                        cbxlocation.Items.Add(dr["LocationName"]);
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
            try
            {
                if (cbxseason.SelectedItem == null || cbxzone.SelectedItem == null)
                {
                    MessageBox.Show("Select Season, Zone");
                }

                string strRetrieve = "";
                strRetrieve = "select * from Divisions";

                OleDbDataAdapter adpt = new OleDbDataAdapter(strRetrieve, Database.getConnection());

                //  cbxdivision.Items.Clear(); 
                DataTable dt = new DataTable();
                adpt.Fill(dt);


                foreach (DataRowView dr in dt.DefaultView)
                {
                    //   List<Team> lstFilter = Database.GetEntityList<Team>( false, false,false,Database.getConnection(),RecordStatus true);
                    //cbxdivision.Items.Add(dr["DivisionName"]);
                    //cbxdivision.Items.Refresh();
                    if (!cbxdivision.Items.Contains(dr["DivisionName"]))// For remove list duplicacy
                    {
                        cbxdivision.Items.Add(dr["DivisionName"]);
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private void cbxseason_DropDownOpened(object sender, EventArgs e)
        {
            try
            {
                string selectZones;
                selectZones = "select * from Seasons";

                OleDbDataAdapter adpt = new OleDbDataAdapter(selectZones, Database.getConnection());

                //  cbxdivision.Items.Clear(); 
                DataTable dt = new DataTable();
                adpt.Fill(dt);


                foreach (DataRowView dr in dt.DefaultView)
                {

                    if (!cbxseason.Items.Contains(dr["SeasonType"]))// For remove list duplicacy
                    {
                        cbxseason.Items.Add(dr["SeasonType"]);
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
