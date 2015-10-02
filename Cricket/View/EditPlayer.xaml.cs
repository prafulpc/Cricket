using CricketSol.DAL;
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
using Cricket.BLL;
using CricketSol.Base;

using CricketSol.Database;
using CricketSol.System;
using System.Collections.ObjectModel;

namespace Cricket.View
{
    /// <summary>
    /// Interaction logic for EditPlayer.xaml
    /// </summary>
    public partial class EditPlayer : UserControl
    {
      
       
        DataTable dtlItemDetails = new DataTable();
        OleDbConnection oleconn = Database.getConnection();

        public EditPlayer()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                string strRetrieve1 = "";

                strRetrieve1 = "select TeamName,TeamId from Teams";

                OleDbDataAdapter adpt1 = new OleDbDataAdapter(strRetrieve1, oleconn);

                DataSet ds1 = new DataSet();
                adpt1.Fill(ds1);
                ds1.Tables[0].DefaultView.Sort = "TeamName";
                cbxTeams.ItemsSource = ds1.Tables[0].DefaultView;
                cbxTeams.DisplayMemberPath = ds1.Tables[0].Columns["TeamName"].ToString();
                cbxTeams.SelectedValuePath = ds1.Tables[0].Columns["TeamId"].ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void cbxTeams_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {

                if (cbxTeams.SelectedValue == null)
                {
                    //  lbxTeams.Items.Clear();
                }
                else
                {
                    lbxTeams.DataContext = null;
                    ObservableCollection<Player> lbxTeam = Database.GetEntityList<Player>(false, false, false, Database.getConnection(), "RecordStatus", "RecordStatus");

                    Team objteam = Database.GetEntity<Team>(Guid.Parse(cbxTeams.SelectedValue.ToString()), false, false, false, oleconn);

                    List<Player> lstPlayer = lbxTeam.Where(p => p.TeamIdId.ToString() == cbxTeams.SelectedValue.ToString()).ToList<Player>();

                    DataTable dt = new DataTable();
                    dt.Columns.Add("PlayerName");
                    dt.Columns.Add("PlayerId");
                    if (lstPlayer.Count > 0)
                    {
                        foreach (Player objplayer in lstPlayer)
                        {
                            if (objplayer.TeamIdId.ToString() == cbxTeams.SelectedValue.ToString())
                            {
                                DataRow dr = dt.NewRow();


                                dr["PlayerName"] = objplayer.FirstName.ToString();
                                dr["PlayerId"] = objplayer.PlayerId.ToString();
                                dt.Rows.Add(dr);


                            }
                        }

                        lbxTeams.DataContext = dt;
                        lbxTeams.DisplayMemberPath = dt.Columns["PlayerName"].ToString();
                        lbxTeams.SelectedValuePath = dt.Columns["PlayerId"].ToString();
                        lbxTeam.Clear();
                    }


                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbxTeams.SelectedValue == null)
                {

                }
                else
                {

                    updateplayername.playername = lbxTeams.SelectedValue.ToString();
                    updateplayername.teamid = cbxTeams.SelectedValue.ToString();
                    Button btn = (Button)sender;
                    btn.Command = NavigationCommands.GoToPage;
                    btn.CommandParameter = "/View/EditSelectedPlayer.xaml";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }
    }


    public static class updateplayername
    {
        public static string playername;
        public static string teamid;
    }
}