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
using CricketSol.DAL;

namespace Cricket.View
{
    /// <summary>
    /// Interaction logic for EditUmpire.xaml
    /// </summary>
       

    public partial class EditUmpire : UserControl
    {
        OleDbConnection oleconn = Database.getConnection();
        public EditUmpire()
        {
            InitializeComponent();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbxumpires.SelectedValue == null)
                {

                }
                else
                {

                    updateUmpire.umpirename = lbxTeams.SelectedValue.ToString();
                    updateUmpire.umpireid = cbxumpires.SelectedValue.ToString();
                    Button btn = (Button)sender;
                    btn.Command = NavigationCommands.GoToPage;
                    btn.CommandParameter = "/View/EditSelectedUmpire.xaml";
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

                strRetrieve1 = "select OfficialName,OfficialId from official";

                OleDbDataAdapter adpt1 = new OleDbDataAdapter(strRetrieve1, oleconn);

                DataSet ds1 = new DataSet();
                adpt1.Fill(ds1);
                ds1.Tables[0].DefaultView.Sort = "OfficialName";
                cbxumpires.ItemsSource = ds1.Tables[0].DefaultView;
                cbxumpires.DisplayMemberPath = ds1.Tables[0].Columns["OfficialName"].ToString();
                cbxumpires.SelectedValuePath = ds1.Tables[0].Columns["OfficialId"].ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void cbxumpires_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbxumpires.SelectedValue == null)
            {
                //  lbxTeams.Items.Clear();
            }
            else
            {
                lbxTeams.DataContext = null;
                ObservableCollection<Official> lbxOfficial = Database.GetEntityList<Official>(false, false, false, Database.getConnection(), "RecordStatus", "OfficialId");

                //Team objteam = Database.GetEntity<Team>(Guid.Parse(cbxumpires.SelectedValue.ToString()), false, false, false, oleconn);

                //List<Player> lstPlayer = lbxOfficial.Where(p => p.TeamIdId.ToString() == cbxumpires.SelectedValue.ToString()).ToList<Player>();

                DataTable dt = new DataTable();
                dt.Columns.Add("OfficialName");
                dt.Columns.Add("OfficialId");
                if (lbxOfficial.Count > 0)
                {
                    foreach (Official objplayer in lbxOfficial)
                    {
                        if (objplayer.OfficialId.ToString() == cbxumpires.SelectedValue.ToString())
                        {
                            DataRow dr = dt.NewRow();


                            dr["OfficialName"] = objplayer.OfficialName.ToString();
                            dr["OfficialId"] = objplayer.OfficialId.ToString();
                            dt.Rows.Add(dr);


                        }
                    }

                    lbxTeams.DataContext = dt;
                    lbxTeams.DisplayMemberPath = dt.Columns["OfficialName"].ToString();
                    lbxTeams.SelectedValuePath = dt.Columns["OfficialId"].ToString();
                    lbxOfficial.Clear();
                }


            }
        }
    }

    public static class updateUmpire
    {
        public static string umpirename;
        public static string umpireid;
    }
}
