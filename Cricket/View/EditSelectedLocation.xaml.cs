using CricketSol.DAL;
using CricketSol.Database;
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

namespace Cricket.View
{
    /// <summary>
    /// Interaction logic for EditSelectedLocation.xaml
    /// </summary>
    public partial class EditSelectedLocation : UserControl
    {
        public EditSelectedLocation()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            string strRetrieve1 = "";

            strRetrieve1 = "select Zonename,Zoneid from zones";

            OleDbDataAdapter adpt1 = new OleDbDataAdapter(strRetrieve1, Database.getConnection());

            DataSet ds1 = new DataSet();
            adpt1.Fill(ds1);
            cbxzone.ItemsSource = ds1.Tables[0].DefaultView;
            cbxzone.DisplayMemberPath = ds1.Tables[0].Columns["ZoneName"].ToString();
            cbxzone.SelectedValuePath = ds1.Tables[0].Columns["ZoneId"].ToString();


            ///

            try
            {
                cbxzone.SelectedIndex = 0;
                ObservableCollection<Location> lbxLocation = Database.GetEntityList<Location>(false, false, false, Database.getReadConnection(), "RecordStatus", "LocationId");
                foreach (Location objLocation in lbxLocation)
                {
                    if (objLocation.LocationId.ToString() == updateLocation.Locationid)
                    {
                        txtstadium.Text = objLocation.StadiumName;
                        txtgroundid.Text = objLocation.GroundId;
                        txtlocationname.Text = objLocation.LocationName;
                        txtPitchType.Text = objLocation.PitchType;
                        txtaddress.Text = objLocation.Address;
                        txtaccountno.Text = objLocation.AccountNumber;
                        txtaccountname.Text = objLocation.AccountName;
                        txtaccounttype.Text = objLocation.AccountType;
                        txtbankname.Text = objLocation.BankName;
                        txtbankbranch.Text = objLocation.BankBranch;
                        txtifsc.Text = objLocation.IFSCCode;


                    }

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btnsave_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                Location objLocation = new Location();

                ObservableCollection<Location> lbxLocation = Database.GetEntityList<Location>(false, false, false, Database.getReadConnection(), "RecordStatus", "LocationId");
                foreach (Location objloc in lbxLocation)
                {


                    if (objloc.LocationId.ToString() == updateLocation.Locationid)
                    {
                        objLocation.LocationId = (Guid.Parse(updateLocation.Locationid));
                        objLocation.StadiumName = txtstadium.Text;
                        objLocation.GroundId = txtgroundid.Text;
                        objLocation.LocationName = txtlocationname.Text;
                        objLocation.PitchType = txtPitchType.Text;
                        objLocation.Address = txtaddress.Text;
                        objLocation.AccountNumber = txtaccountno.Text;
                        objLocation.AccountName = txtaccountname.Text;
                        objLocation.AccountType = txtaccounttype.Text;
                        objLocation.BankName = txtbankname.Text;
                        objLocation.BankBranch = txtbankbranch.Text;
                        objLocation.IFSCCode = txtifsc.Text;



                        Zone objZone = Database.GetEntity<Zone>(Guid.Parse(cbxzone.SelectedValue.ToString()), false, false, false, Database.getConnection());
                        objLocation.Zone = objZone;



                        break;
                    }

                }



                Database.SaveEntity<Location>(objLocation, Database.getConnection());
                MessageBox.Show("Location Updated");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
