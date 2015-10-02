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
using System.IO;
using Microsoft.Win32;
using System.Data.OleDb;
using System.Data;


namespace Cricket.View
{
    /// <summary>
    /// Interaction logic for EditSelectedPlayer.xaml
    /// </summary>
    public partial class EditSelectedPlayer : UserControl
    {
        public EditSelectedPlayer()
        {
            InitializeComponent();
            
         
        }

        private void btnsave_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                Player objplayertoadd = new Player();

                ObservableCollection<Player> lbxTeam = Database.GetEntityList<Player>(false, false, false, Database.getReadConnection(), "RecordStatus", "FirstName");
                foreach (Player objplayer12 in lbxTeam)
                {
                  

                    if (objplayer12.PlayerId.ToString() == updateplayername.playername)
                    {
                        objplayertoadd.PlayerId = (Guid.Parse(updateplayername.playername));
                        objplayertoadd.FirstName = txtfname.Text;
                        objplayertoadd.LastName = txtlname.Text;
                        objplayertoadd.FatherName = txtFather.Text;
                        objplayertoadd.MobileNo = txtmobno.Text;
                        objplayertoadd.DateOfBirth = dateselect.SelectedDate.Value;
                        objplayertoadd.Emaild = txtemailid.Text;
                        objplayertoadd.Address = txtaddress.Text;
                        objplayertoadd.KSCAUID = txtkscauid.Text;
                        // objplayertoadd.ImageField = txt_browse.Text;

                        Team obj3 = Database.GetEntity<Team>(Guid.Parse(updateplayername.teamid), false, false, false, Database.getConnection());
                        objplayertoadd.TeamId = obj3;

                        Zone objZone = Database.GetEntity<Zone>(Guid.Parse(cbxzone.SelectedValue.ToString()), false, false, false, Database.getConnection());
                        objplayertoadd.Zone = objZone;


                        //Gender Select
                        if (rbtnmale.IsChecked == true)
                        {
                            objplayertoadd.Gender = "Male";
                           
                        }
                        else if (rbtnfemale.IsChecked == true)
                        {
                            objplayertoadd.Gender = "Female";
                        }
                        else
                        {
                            MessageBox.Show("Select Gender");
                        }


                        //Batting Style Select
                        if (rbtnrightbat.IsChecked == true)
                        {
                            objplayertoadd.battingstyle = "Right Handed";
                        }
                        else if (rbtnleftbat.IsChecked == true)
                        {
                            objplayertoadd.battingstyle = "Left Handed";
                        }
                        else
                        {
                            MessageBox.Show("Select Batting Style");
                        }

                        string bowlingstyle = cbxbalstyle.Text;

                        //Bowling Style Select
                        if (rbtnrightbal.IsChecked == true)
                        {
                            objplayertoadd.BowlingStyle = "Right Arm, " + bowlingstyle;
                        }
                        else if (rbtnleftbal.IsChecked == true)
                        {
                            objplayertoadd.BowlingStyle = "Left Arm, " + bowlingstyle;
                        }
                        else
                        {
                            MessageBox.Show("Select Bowling Style");
                        }



                        //Wicket Keeper
                        if (cbkeeper.IsChecked == true)
                        {
                            objplayertoadd.WicketKeeper = "Wicket Keeper";
                        }



                        //FileStream fs = new FileStream(txt_browse.Text, FileMode.OpenOrCreate, FileAccess.Read);
                        //byte[] rawData = new byte[fs.Length];
                        //fs.Read(rawData, 0, System.Convert.ToInt32(fs.Length));
                        //fs.Close();

                        //objplayertoadd.PlayerImage = rawData;

                        break;
                    }

                }



                Database.SaveEntity<Player>(objplayertoadd, Database.getConnection());
                MessageBox.Show("saved");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btnupload_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                Nullable<bool> result = ofd.ShowDialog();
                if (result == true)
                {
                    BitmapImage bip = new BitmapImage();
                    txt_browse.Text = ofd.FileName;
                    image.Source = new BitmapImage(new Uri(ofd.FileName));
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
                cbxzone.SelectedIndex = 0;
                ObservableCollection<Player> lbxTeam = Database.GetEntityList<Player>(false, false, false, Database.getReadConnection(), "RecordStatus", "FirstName");
                foreach (Player objplayer in lbxTeam)
                {
                    if (objplayer.PlayerId.ToString() == updateplayername.playername)
                    {
                        txtfname.Text = objplayer.FirstName;
                        txtlname.Text = objplayer.LastName;
                        txtFather.Text = objplayer.FatherName;
                        txtmobno.Text = objplayer.MobileNo;
                        dateselect.SelectedDate = objplayer.DateOfBirth;
                        txtemailid.Text = objplayer.Emaild;
                        txtkscauid.Text = objplayer.KSCAUID;
                        txtaddress.Text = objplayer.Address;
                        cbxbalstyle.Text = objplayer.BowlingStyle;
                        
                    }

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        private void cbxzone_DropDownOpened(object sender, EventArgs e)
        {
            string strRetrieve1 = "";

            strRetrieve1 = "select Zonename,Zoneid from zones";

            OleDbDataAdapter adpt1 = new OleDbDataAdapter(strRetrieve1, Database.getConnection());

            DataSet ds1 = new DataSet();
            adpt1.Fill(ds1);
            cbxzone.ItemsSource = ds1.Tables[0].DefaultView;
            cbxzone.DisplayMemberPath = ds1.Tables[0].Columns["ZoneName"].ToString();
            cbxzone.SelectedValuePath = ds1.Tables[0].Columns["ZoneId"].ToString();
        }

      
    }
}
