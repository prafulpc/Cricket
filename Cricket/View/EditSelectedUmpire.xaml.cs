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
    /// Interaction logic for EditSelectedUmpire.xaml
    /// </summary>
    public partial class EditSelectedUmpire : UserControl
    {
        public EditSelectedUmpire()
        {
            InitializeComponent();
        }

        private void cbxZone_DropDownOpened(object sender, EventArgs e)
        {
            string strRetrieve1 = "";

            strRetrieve1 = "select Zonename,Zoneid from zones";

            OleDbDataAdapter adpt1 = new OleDbDataAdapter(strRetrieve1, Database.getConnection());

            DataSet ds1 = new DataSet();
            adpt1.Fill(ds1);
            cbxZone.ItemsSource = ds1.Tables[0].DefaultView;
            cbxZone.DisplayMemberPath = ds1.Tables[0].Columns["ZoneName"].ToString();
            cbxZone.SelectedValuePath = ds1.Tables[0].Columns["ZoneId"].ToString();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                cbxZone.SelectedIndex = 0;
                ObservableCollection<Official> lbxOfficial = Database.GetEntityList<Official>(false, false, false, Database.getReadConnection(), "RecordStatus", "OfficialId");
                foreach (Official objplayer in lbxOfficial)
                {
                    if (objplayer.OfficialId.ToString() == updateUmpire.umpireid)
                    {
                        txtName.Text = objplayer.OfficialName;
                        txtId.Text = objplayer.OfficialPrimaryId;
                        txtemailid.Text = objplayer.EmailId;
                        txtmobno.Text = objplayer.MobileNo;
                        dateselect.SelectedDate = objplayer.DateOfBirth;
                        txtPlace.Text = objplayer.Place;
                       

                    }

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

                Official objOfficial = new Official();

                ObservableCollection<Official> lbxOfficial = Database.GetEntityList<Official>(false, false, false, Database.getReadConnection(), "RecordStatus", "OfficialId");
                foreach (Official objoff in lbxOfficial)
                {


                    if (objoff.OfficialId.ToString() == updateUmpire.umpireid)
                    {
                        objOfficial.OfficialId = (Guid.Parse(updateUmpire.umpireid));
                        objOfficial.OfficialName = txtName.Text;
                        objOfficial.EmailId = txtemailid.Text;
                       
                        objOfficial.MobileNo = txtmobno.Text;
                        objOfficial.DateOfBirth = dateselect.SelectedDate.Value;
                        objOfficial.OfficialPrimaryId = txtId.Text;
                        objOfficial.Place = txtPlace.Text;
                       

                     

                        Zone objZone = Database.GetEntity<Zone>(Guid.Parse(cbxZone.SelectedValue.ToString()), false, false, false, Database.getConnection());
                        objOfficial.Zone = objZone;


                        //Gender Select
                        if (rbtnmale.IsChecked == true)
                        {
                            objOfficial.Gender = "Male";

                        }
                        else if (rbtnfemale.IsChecked == true)
                        {
                            objOfficial.Gender = "Female";
                        }
                        else
                        {
                            MessageBox.Show("Select Gender");
                        }


                        //FileStream fs = new FileStream(txt_browse.Text, FileMode.OpenOrCreate, FileAccess.Read);
                        //byte[] rawData = new byte[fs.Length];
                        //fs.Read(rawData, 0, System.Convert.ToInt32(fs.Length));
                        //fs.Close();

                        //objOfficial.PlayerImage = rawData;

                        break;
                    }

                }



                Database.SaveEntity<Official>(objOfficial, Database.getConnection());
                MessageBox.Show("Official Updated");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
