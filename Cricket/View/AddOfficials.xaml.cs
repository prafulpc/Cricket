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

using CricketSol.Base;
using CricketSol.DAL;
using CricketSol.Database;
using CricketSol.System;
using System.Collections.ObjectModel;
using System.Data.OleDb;
using System.Data;

namespace Cricket.View
{
    /// <summary>
    /// Interaction logic for AddOfficials.xaml
    /// </summary>
    public partial class AddOfficials : UserControl
    {

        OleDbConnection oleconn = Database.getConnection();

        public AddOfficials()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(txtName.Text == "")
                {
                    MessageBox.Show("Name Field Cannot be be empty");

                }
                if(txtId.Text =="")
                {
                    MessageBox.Show("Id Field Cannot be empty");
                }
                if (txtmobno.Text == "")
                {
                    MessageBox.Show("Mobile Number Field  Cannot be empty");
                }
                if(txtPlace.Text == "")
                {
                    MessageBox.Show("Umpire Location  Field  Cannot be empty");
                }
                if (txtemailid.Text == "")
                {
                    MessageBox.Show("EmailId  Field  Cannot be empty");
                }
            
                if (cbxZone.SelectedItem == null)
                {
                    MessageBox.Show("Select Zone");
                }
              
                

                else
                {
                    Official objOfficial = Database.GetNewEntity<Official>();
                    objOfficial.OfficialPrimaryId = txtId.Text;
                    objOfficial.OfficialName = txtName.Text;
                    objOfficial.Place = txtPlace.Text;

                    ObservableCollection<Zone> obczone = Database.GetEntityList<Zone>(false, true, true, oleconn, "RecordStatus='Added'", "ZoneName");
                    Zone obj1 = Database.GetEntity<Zone>(Guid.Parse(cbxZone.SelectedValue.ToString()), false, false, false, oleconn);
                    objOfficial.Zone = obj1;

                    objOfficial.DateOfBirth = dateselect.SelectedDate.Value;
                    objOfficial.MobileNo = (txtmobno.Text);
                    objOfficial.EmailId = txtemailid.Text;

                  //  objOfficial.OfficialType = cbxOfficialType.Text;
                    if (rbtnmale.IsChecked == true)
                    {
                        objOfficial.Gender = "Male";
                        objOfficial.Indicator = RecordStatus.Added;
                    }
                    else if (rbtnfemale.IsChecked == true)
                    {
                        objOfficial.Gender = "Female";
                    }
                   
                    Database.SaveEntity<Official>(objOfficial, oleconn);
                    MessageBox.Show("Official With The Given Name = " + txtName.Text + " With The Official Given Id = " + txtId.Text + " Added Successfully");

                    Button btn = (Button)sender;
                    btn.Command = NavigationCommands.GoToPage;
                    btn.CommandParameter = "/View/AddNew.xaml";

                    //clear
                    txtName.Clear();
                    txtemailid.Clear();
                    txtId.Clear();
                    txtmobno.Clear();
                    txtPlace.Clear();
                    dateselect.SelectedDate = null;
                    cbxZone.SelectedValue = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void cbxZone_DropDownOpened(object sender, EventArgs e)
        {
            try
            {
                string strRetrieve = "";

                strRetrieve = "select zonename,zoneid from Zones";
                OleDbDataAdapter adpt = new OleDbDataAdapter(strRetrieve, oleconn);
                DataSet ds = new DataSet();
                adpt.Fill(ds);

                cbxZone.ItemsSource = ds.Tables[0].DefaultView;
                cbxZone.DisplayMemberPath = ds.Tables[0].Columns["zonename"].ToString();
                cbxZone.SelectedValuePath = ds.Tables[0].Columns["zoneid"].ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ObservableCollection<Official> lstofficial = Database.GetEntityList<Official>(false, false, false, Database.getReadConnection(), "RecordStatus", "OfficialId");
            int count = 0;
            int numberofzeros = 0;

            count = lstofficial.Count;

            while(count>10)
            {
                count = (count / 10);
                numberofzeros++;

            }

            if(numberofzeros == 1)
            {
                txtId.Text = "7U0" + (lstofficial.Count + 1).ToString();
            }
            else if (numberofzeros == 2)
            {
                txtId.Text = "7U"+ (lstofficial.Count + 1).ToString();
            }

        }
    }
}
