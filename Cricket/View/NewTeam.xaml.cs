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
using Cricket.View;
using System.Collections.ObjectModel;
using Cricket.BLL;


namespace Cricket.View
{
    /// <summary>
    /// Interaction logic for NewTeam.xaml
    /// </summary>
    public partial class NewTeam : UserControl
    {
        Team obj;

        public NewTeam()
        { 
            InitializeComponent();
            
        }

        public void btnsave_Click(object sender, RoutedEventArgs e)
        {
            obj = Database.GetNewEntity<Team>();
            this.DataContext = obj;
            NewTeamBLL objteambll = new NewTeamBLL(obj);

            try
            {

                obj.TeamName = txtteamname.Text;
                obj.PrimaryTeamId = txtshortname.Text;
               
            
                //ObservableCollection<Club> obcClub = Database.GetEntityList<Club>(false, true, true, Database.getConnection(), "RecordStatus='Added'", "ClubName");
                //Club obj1 = Database.GetEntity<Club>(Guid.Parse(cbxclub.SelectedValue.ToString()), false, false, false, Database.getConnection());
                //obj.Club = obj1;

                
                Database.SaveEntity<Team>(obj, Database.getConnection());
                MessageBox.Show("Team With Name " + txtteamname.Text+ "and With TeamId" +txtshortname.Text +"Added Successfully");

                txtshortname.Clear();
                txtteamname.Clear();
                txtteamname.Focus();
            }


            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cbxclub_DropDownOpened(object sender, EventArgs e)
        {
            string strRetrieve1 = "";

            strRetrieve1 = "select ClubName,ClubId from ClubDetails";

            OleDbDataAdapter adpt1 = new OleDbDataAdapter(strRetrieve1, Database.getConnection());

            DataSet ds1 = new DataSet();
            adpt1.Fill(ds1);
            cbxclub.ItemsSource = ds1.Tables[0].DefaultView;
            cbxclub.DisplayMemberPath = ds1.Tables[0].Columns["ClubName"].ToString();
            cbxclub.SelectedValuePath = ds1.Tables[0].Columns["ClubId"].ToString();
        }

        private void btnclear_Click(object sender, RoutedEventArgs e)
        {
            txtshortname.Clear();
            txtteamname.Clear();
            txtteamname.Focus();
        }
    }
}   
 
