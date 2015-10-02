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
using Microsoft.Win32;
using System.IO;
using Cricket.BLL;


namespace Cricket.View
{
    /// <summary>
    /// Interaction logic for NewLocation.xaml
    /// </summary>
    public partial class NewLocation : UserControl
    {

        Location objlocation;

        public NewLocation()
        {
            InitializeComponent();

            string strRetrieve = "";

            strRetrieve = "select Zonename,Zoneid from zones";

            OleDbDataAdapter adpt = new OleDbDataAdapter(strRetrieve, Database.getConnection());

            DataSet ds = new DataSet();
            adpt.Fill(ds);
            cbxzone.ItemsSource = ds.Tables[0].DefaultView;
            cbxzone.DisplayMemberPath = ds.Tables[0].Columns["ZoneName"].ToString();
            cbxzone.SelectedValuePath = ds.Tables[0].Columns["ZoneId"].ToString();
             
        }

        private void btnsave_Click(object sender, RoutedEventArgs e)
        {
            objlocation = Database.GetNewEntity<Location>();
            this.DataContext = objlocation;
            NewLocationBLL objlocationbll = new NewLocationBLL(objlocation);
            
            try
            {
                objlocation.LocationName = txtlocationname.Text;
                objlocation.StadiumName = txtstadium.Text;
                objlocation.Address = txtaddress.Text;
                objlocation.AccountName = txtaccountname.Text;

                Zone obj1 = Database.GetEntity<Zone>(Guid.Parse(cbxzone.SelectedValue.ToString()), false, false, false, Database.getConnection());
                objlocation.Zone = obj1;

                objlocation.AccountNumber = (txtaccountno.Text);
                objlocation.AccountType = txtaccounttype.Text;
                objlocation.BankName = txtbankname.Text;
                objlocation.BankBranch = txtbankbranch.Text;
                objlocation.IFSCCode = txtifsc.Text;
                objlocation.PitchType = txtPitchType.Text;
                objlocation.GroundId = txtgroundid.Text; 

                Database.SaveEntity<Location>(objlocation, Database.getConnection());
                MessageBox.Show("Location Saved Successfully With Given Ground Name = " +txtstadium.Text+ " And Ground Id ="+txtgroundid.Text +"'");

                txtaccountname.Clear();
                txtaccountno.Clear();
                txtaccounttype.Clear();
                txtaddress.Clear();
                txtbankbranch.Clear();
                txtbankname.Clear();
                txtifsc.Clear();
                txtlocationname.Clear();
                txtstadium.Clear();
                cbxzone.SelectedValue = null;
                txtgroundid.Clear();
                txtPitchType.Clear();

                Button btn = (Button)sender;
                btn.Command = NavigationCommands.GoToPage;
                btn.CommandParameter = "/View/AddNew.xaml";

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

      

        private void cbxzone_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //ObservableCollection<Zone> objzone = Database.GetEntityList<Zone>(false, true, true, Database.getConnection(), "RecordStatus='Added'", "ZoneName");
            //int count = 0;

            //while (objzone[count].ZoneId.ToString() != cbxzone.SelectedValue.ToString())
            //{
            //    count++;
            //}

            //txtaccountname.Text = objzone[count].AccountName;
            //txtaccountno.Text = ((objzone[count].AccountNumber.ToString()));
            //txtaccounttype.Text = objzone[count].AccountType;
            //txtbankname.Text = objzone[count].BankName;
            //txtbankbranch.Text = objzone[count].BankBranch;
            //txtifsc.Text = objzone[count].IFSCCode;
        }

        private void btnclear_Click(object sender, RoutedEventArgs e)
        {
            txtaccountname.Clear();
            txtaccountno.Clear();
            txtaccounttype.Clear();
            txtaddress.Clear();
            txtbankbranch.Clear();
            txtbankname.Clear();
            txtifsc.Clear();
            txtlocationname.Clear();
            txtstadium.Clear();
            txtlocationname.Focus();
            
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ObservableCollection<Location> lstLocation = Database.GetEntityList<Location>(false, false, false, Database.getReadConnection(), "RecordStatus", "LocationId");
            int count = 0;
            int numberofzeros = 0;

            count = lstLocation.Count;

            while (count > 10)
            {
                count = (count / 10);
                numberofzeros++;

            }

            if (numberofzeros == 1)
            {
                txtgroundid.Text = "7U0" + (lstLocation.Count + 1).ToString();
            }
            else if (numberofzeros == 2)
            {
                txtgroundid.Text = "7U" + (lstLocation.Count + 1).ToString();
            }

        }
    }
}

