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
    /// Interaction logic for NewPlayer.xaml
    /// </summary>
    public partial class NewPlayer : UserControl
    {
        
        Player objplayer;
     
        public NewPlayer()
        {
                    
            InitializeComponent();
            //int count = 0;
            //int numberofzero = 0;

            //ObservableCollection<Player> lstplayer = Database.GetEntityList<Player>(false, false, false, Database.getReadConnection(), "RecordStatus", "PlayerId");
            //count = lstplayer.Count;

            //rbtnmale.IsChecked = true;

            //while(count>10)
            //{
            //    count = (count / 10);

            //    numberofzero++;
            //}

            //if(numberofzero == 1)
            //{
            //    txtkscauid.Text = "7P000" + (lstplayer.Count + 1).ToString();
            //}

            //else if(numberofzero == 2)
            //{
            //    txtkscauid.Text = "7P00" + (lstplayer.Count + 1).ToString();
            //}
            //else if (numberofzero == 3)
            //{
            //    txtkscauid.Text = "7P0" + (lstplayer.Count + 1).ToString();
            //}

            //else if (numberofzero == 4)
            //{
            //    txtkscauid.Text = "7P" + (lstplayer.Count + 1).ToString();
            //}

      
            // TeamName
           
        }

        private void btnsave_Click(object sender, RoutedEventArgs e)
        {
            objplayer = Database.GetNewEntity<Player>();
            this.DataContext = objplayer;
            NewPlayerBLL objplayerbll = new NewPlayerBLL(objplayer);
            



            try
            {

               
                    OleDbConnection objconn = Database.getConnection();
                    objplayer.FirstName = txtfname.Text;
                    objplayer.LastName = txtlname.Text;
                    objplayer.MobileNo = (txtmobno.Text);
                    objplayer.Emaild = txtemailid.Text;

                    ObservableCollection<Zone> obczone = Database.GetEntityList<Zone>(false, true, true, Database.getConnection(), "RecordStatus='Added'", "ZoneName");
                    Zone obj1 = Database.GetEntity<Zone>(Guid.Parse(cbxzone.SelectedValue.ToString()), false, false, false, objconn);
                    objplayer.Zone = obj1;

                    //ObservableCollection<Club> obcclub = Database.GetEntityList<Club>(false, true, true, Database.getConnection(), "RecordStatus='Added'", "ClubName");
                    //Club obj2 = Database.GetEntity<Club>(Guid.Parse(cbxclub.SelectedValue.ToString()), false, false, false, Database.getConnection());
                    //objplayer.Club = obj2;

                    ObservableCollection<Team> obcteam = Database.GetEntityList<Team>(false, true, true, Database.getConnection(), "RecordStatus='Added'", "TeamName");
                    Team obj3 = Database.GetEntity<Team>(Guid.Parse(cbxTeam.SelectedValue.ToString()), false, false, false, objconn);
                    objplayer.TeamId = obj3;

                    objplayer.DateOfBirth = dateselect.SelectedDate.Value;
                    objplayer.Address = txtaddress.Text;
                    objplayer.KSCAUID = txtkscauid.Text;
                    objplayer.FatherName = txtFather.Text;

                    //  objplayer.ImageField = txt_browse.Text;

                    //Gender Select
                    if (rbtnmale.IsChecked == true)
                    {
                        objplayer.Gender = "Male";
                        objplayer.Indicator = RecordStatus.Added;
                    }
                    else if (rbtnfemale.IsChecked == true)
                    {
                        objplayer.Gender = "Female";
                    }
                    else
                    {
                        MessageBox.Show("Select Gender");
                    }


                    //Batting Style Select
                    if (rbtnrightbat.IsChecked == true)
                    {
                        objplayer.battingstyle = "Right Handed";
                    }
                    else if (rbtnleftbat.IsChecked == true)
                    {
                        objplayer.battingstyle = "Left Handed";
                    }
                    else
                    {
                        MessageBox.Show("Select Batting Style");
                    }

                    string bowlingstyle = cbxbalstyle.Text;

                    //Bowling Style Select
                    if (rbtnrightbal.IsChecked == true)
                    {
                        objplayer.BowlingStyle = "Right Arm, " + bowlingstyle;
                    }
                    else if (rbtnleftbal.IsChecked == true)
                    {
                        objplayer.BowlingStyle = "Left Arm, " + bowlingstyle;
                    }
                    else
                    {
                        MessageBox.Show("Select Bowling Style");
                    }


                    //Wicket Keeper
                    if (cbkeeper.IsChecked == true)
                    {
                        objplayer.WicketKeeper = "Wicket Keeper";
                    }

                    //textbox validation


                    Database.SaveEntity<Player>(objplayer, Database.getConnection());
                    MessageBox.Show("Saved With The PlayerName = '" + txtfname.Text + "  With The Assigned KSCAUID = " + txtkscauid.Text + "  For The Team = " + cbxTeam.Text + "'");
                    //image.Source = null;
                    //txt_browse.Clear();

                    // txtkscauid.Clear();
            

                //    if (objplayer.PlayerImage == null)
                //{
                //     Database.SaveEntity<Player>(objplayer, Database.getConnection());
                //    MessageBox.Show("Saved...");
                //    image.Source = null;
                //    txt_browse.Clear();
                //}

                //textbox clear 
                    txt_browse.Text = string.Empty;
                    txtaddress.Text = string.Empty;
                    txtemailid.Text = string.Empty;
                    txtFather.Text = string.Empty;
                    txtfname.Text = string.Empty;
                    txtkscauid.Text = string.Empty;
                    txtlname.Text = string.Empty;
                    txtmobno.Text = string.Empty;
                    dateselect.SelectedDate = null;
                    cbxzone.SelectedValue = null;
                    cbxTeam.SelectedValue = null;
                    cbxbalstyle.SelectedValue = null;

                    Button btn = (Button)sender;
                    btn.Command = NavigationCommands.GoToPage;
                    btn.CommandParameter = "/View/AddNew.xaml";

               
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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

        private void cbxclub_DropDownOpened(object sender, EventArgs e)
        {
            //string strRetrieve2 = "";

            //strRetrieve2 = "select Clubname,Clubid from Clubdetails";

            //OleDbDataAdapter adpt2 = new OleDbDataAdapter(strRetrieve2, Database.getConnection());

            //DataSet ds2 = new DataSet();
            //adpt2.Fill(ds2);
            //cbxclub.ItemsSource = ds2.Tables[0].DefaultView;
            //cbxclub.DisplayMemberPath = ds2.Tables[0].Columns["Clubname"].ToString();
            //cbxclub.SelectedValuePath = ds2.Tables[0].Columns["ClubId"].ToString();
        }

        private void cbxTeam_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //string strRetrieve3 = "";
            //OleDbDataAdapter adpt3 = new OleDbDataAdapter(strRetrieve3, Database.getConnection());
            //DataSet ds3 = new DataSet();
            //adpt3.Fill(ds3);
            //cbxclub.ItemsSource = ds3.Tables[0].DefaultView;
            //cbxclub.DisplayMemberPath = ds3.Tables[0].Columns["TeamName"].ToString();
            //cbxclub.SelectedValuePath = ds3.Tables[0].Columns["TeamId"].ToString();

        }

        private void btnupload_Click(object sender, RoutedEventArgs e)
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

        private void cbxTeam_DropDownOpened(object sender, EventArgs e)
        {
            string strRetrieve3 = "Select * from Teams";
            OleDbDataAdapter adpt3 = new OleDbDataAdapter(strRetrieve3, Database.getConnection());
            DataSet ds3 = new DataSet();
            adpt3.Fill(ds3);
            cbxTeam.ItemsSource = ds3.Tables[0].DefaultView;
            cbxTeam.DisplayMemberPath = ds3.Tables[0].Columns["TeamName"].ToString();
            cbxTeam.SelectedValuePath = ds3.Tables[0].Columns["TeamId"].ToString();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            int count = 0;
            int numberofzero = 0;

            ObservableCollection<Player> lstplayer = Database.GetEntityList<Player>(false, false, false, Database.getReadConnection(), "RecordStatus", "PlayerId");
            count = lstplayer.Count;

            rbtnmale.IsChecked = true;

            while (count > 10)
            {
                count = (count / 10);

                numberofzero++;
            }

            if (numberofzero == 1)
            {
                txtkscauid.Text = "7P000" + (lstplayer.Count + 1).ToString();
            }

            else if (numberofzero == 2)
            {
                txtkscauid.Text = "7P00" + (lstplayer.Count + 1).ToString();
            }
            else if (numberofzero == 3)
            {
                txtkscauid.Text = "7P0" + (lstplayer.Count + 1).ToString();
            }

            else if (numberofzero == 4)
            {
                txtkscauid.Text = "7P" + (lstplayer.Count + 1).ToString();
            }

        }

       
    }
}
