using CricketSol.DAL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using CricketSol.Database;

namespace Cricket.View
{
    /// <summary>
    /// Interaction logic for EditLocation.xaml
    /// </summary>
    public partial class EditLocation : UserControl
    {
        OleDbConnection oleconn = Database.getConnection();
        public EditLocation()
        {
            InitializeComponent();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbxLocation.SelectedValue == null)
                {

                }
                else
                {

                    updateLocation.Locationname = lbxLocation.SelectedValue.ToString();
                    updateLocation.Locationid = cbxLocation.SelectedValue.ToString();
                    Button btn = (Button)sender;
                    btn.Command = NavigationCommands.GoToPage;
                    btn.CommandParameter = "/View/EditSelectedLocation.xaml";
                }
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
                string strRetrieve1 = "";

                strRetrieve1 = "select StadiumName,LocationId from locations";

                OleDbDataAdapter adpt1 = new OleDbDataAdapter(strRetrieve1, oleconn);

                DataSet ds1 = new DataSet();
                adpt1.Fill(ds1);
                ds1.Tables[0].DefaultView.Sort = "StadiumName";
                cbxLocation.ItemsSource = ds1.Tables[0].DefaultView;
                cbxLocation.DisplayMemberPath = ds1.Tables[0].Columns["StadiumName"].ToString();
                cbxLocation.SelectedValuePath = ds1.Tables[0].Columns["LocationId"].ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void cbxLocation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbxLocation.SelectedValue == null)
            {
                //  lbxTeams.Items.Clear();
            }
            else
            {
                lbxLocation.DataContext = null;
                ObservableCollection<Location> lbxLocations = Database.GetEntityList<Location>(false, false, false, Database.getConnection(), "RecordStatus", "LocationId");

              

                DataTable dt = new DataTable();
                dt.Columns.Add("StadiumName");
                dt.Columns.Add("LocationId");
                if (lbxLocations.Count > 0)
                {
                    foreach (Location objplayer in lbxLocations)
                    {
                        if (objplayer.LocationId.ToString() == cbxLocation.SelectedValue.ToString())
                        {
                            DataRow dr = dt.NewRow();


                            dr["StadiumName"] = objplayer.StadiumName.ToString();
                            dr["LocationId"] = objplayer.LocationId.ToString();
                            dt.Rows.Add(dr);


                        }
                    }

                    lbxLocation.DataContext = dt;
                    lbxLocation.DisplayMemberPath = dt.Columns["StadiumName"].ToString();
                    lbxLocation.SelectedValuePath = dt.Columns["LocationId"].ToString();
                    lbxLocations.Clear();
                }


            }
        }
    }

    public static class updateLocation
    {
        public static string Locationname;
        public static string Locationid;
    }
}
