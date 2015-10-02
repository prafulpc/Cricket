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
using CricketSol.DAL;
using CricketSol.Base;
using CricketSol.System;
using System.Configuration;
using CricketSol.Database;
using System.Collections.ObjectModel;
using System.Data.OleDb;
using System.Data;
using Cricket.ReportData;
using Microsoft.Reporting.WinForms;

namespace Cricket.View
{
    /// <summary>
    /// Interaction logic for TeamReport.xaml
    /// </summary>
    public partial class TeamReport : UserControl
    {
        OleDbConnection oleconn = Database.getConnection();


        public TeamReport()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
           
        }

        private void cbxdivision_DropDownOpened(object sender, EventArgs e)
        {
            //Select Division
            string strRetrieve1 = "";

            strRetrieve1 = "select DivisionName,DivisionId from Divisions";

            OleDbDataAdapter adpt1 = new OleDbDataAdapter(strRetrieve1, oleconn);

            DataSet ds1 = new DataSet();
            adpt1.Fill(ds1);
            ds1.Tables[0].DefaultView.Sort = "DivisionName";
            cbxdivision.ItemsSource = ds1.Tables[0].DefaultView;
            cbxdivision.DisplayMemberPath = ds1.Tables[0].Columns["DivisionName"].ToString();
            cbxdivision.SelectedValuePath = ds1.Tables[0].Columns["DivisionId"].ToString();

            
              
        }

        private void cbxseason_DropDownOpened(object sender, EventArgs e)
        {
            string strRetrieve = "";
            strRetrieve = "select SeasonType,SeasonId from Seasons";
            OleDbDataAdapter adpt = new OleDbDataAdapter(strRetrieve, oleconn);
            DataSet ds = new DataSet();
            adpt.Fill(ds);

            cbxseason.ItemsSource = ds.Tables[0].DefaultView;
            ds.Tables[0].DefaultView.Sort = "SeasonType";
            cbxseason.DisplayMemberPath = ds.Tables[0].Columns["SeasonType"].ToString();
            cbxseason.SelectedValuePath = ds.Tables[0].Columns["SeasonId"].ToString();


        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if(cbxdivision.SelectedValue == null)
                {
                    MessageBox.Show("Select Division");
                }

                if(cbxseason.SelectedValue == null)
                {
                    MessageBox.Show("Select Season");
                }

                if (cbxTeam.SelectedValue == null)
                {
                    MessageBox.Show("Select Team");
                }

                else
                {
                    lblteamname.Content = string.Empty;
                    lblteamname.Content = cbxTeam.Text;


                    //1st div
                    ObservableCollection<TeamReportTable> lstteamreport1 = Database.GetEntityList<TeamReportTable>(false, false, false, oleconn, "RecordStatus", "DivisionId");

                    //2nd div

                    var objvalues2 = from s in lstteamreport1 where s.DivisionId.ToString() == cbxdivision.SelectedValue.ToString() && s.SeasonId.ToString() == cbxseason.SelectedValue.ToString() && s.TeamName.ToString() == cbxTeam.Text select s;
                    List<TeamReportTable> lstfilter2 = objvalues2.OrderBy(p => p.TeamName).ToList<TeamReportTable>();



                    DataTable dt = new DataTable();
                    dt.Columns.Add("SerialNo");
                    dt.Columns.Add("Opponent");
                    dt.Columns.Add("Toss");
                    dt.Columns.Add("Venue");
                    dt.Columns.Add("RunsScored");
                    dt.Columns.Add("OversFaced");
                    dt.Columns.Add("RunsConceded");
                    dt.Columns.Add("OversBowled");
                    dt.Columns.Add("Points");
                    dt.Columns.Add("Result");
                    dt.Columns.Add("Remarks");


                    DataTable dt1 = new DataTable();
                    dt1.Columns.Add("SerialNo");
                    dt1.Columns.Add("Opponent");
                    dt1.Columns.Add("Toss");
                    dt1.Columns.Add("Venue");
                    dt1.Columns.Add("RunsScored");
                    dt1.Columns.Add("OversFaced");
                    dt1.Columns.Add("RunsConceded");
                    dt1.Columns.Add("OversBowled");
                    dt1.Columns.Add("Points");
                    dt1.Columns.Add("Result");
                    dt1.Columns.Add("Remarks");

                    int SerialNo = 1;
                    bool teamexist = false;


                    //1st div
                    foreach (var objteamreport1 in lstfilter2)
                    {
                        DataRow dr1 = dt1.NewRow();
                        if (objteamreport1.SeasonId.ToString() == cbxseason.SelectedValue.ToString())
                        {

                            if (objteamreport1.DivisionId.ToString() == cbxdivision.SelectedValue.ToString())
                            {


                                dr1["SerialNo"] = SerialNo;
                                dr1["Opponent"] = objteamreport1.Opponent;
                                dr1["Toss"] = objteamreport1.Toss;
                                dr1["Venue"] = objteamreport1.Venue;
                                dr1["RunsScored"] = objteamreport1.RunsScored;
                                dr1["OversFaced"] = objteamreport1.OversFaced;
                                dr1["RunsConceded"] = objteamreport1.RunsConceded;
                                dr1["OversBowled"] = objteamreport1.OversBowled;
                                dr1["Points"] = objteamreport1.Points;
                                dr1["Result"] = objteamreport1.Result;
                                dr1["Remarks"] = objteamreport1.Remarks;
                                dt1.Rows.Add(dr1);
                                SerialNo++;
                                teamexist = true;

                                //break;
                            }
                        }
                    }

                    dgvTeamReport.DataContext = dt1;
                    dgvTeamReport.ItemsSource = dt1.DefaultView;

               
                    if (teamexist == false)
                    {
                        MessageBox.Show("No records Found For Selected Team");
                    }

                    
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgvTeamReport.Items.Count == 0)
                {
                    MessageBox.Show("No Records To Export");
                }
                else
                {


                    TeamReportForm frm = new TeamReportForm();
                    List<TeamReportRe> lstTeamReport = new List<TeamReportRe>();
                    DataTable dt = new DataTable();
                    dt = dgvTeamReport.DataContext as DataTable;

                    int slno = 1;
                    foreach (DataRowView objPlayer in dt.DefaultView)
                    {
                        TeamReportRe obj = new TeamReportRe();


                        obj.SlNo = slno;
                        obj.Opponent = objPlayer["Opponent"].ToString();
                        obj.Toss = objPlayer["Toss"].ToString();
                        obj.Venue = objPlayer["Venue"].ToString();
                        obj.RunsScored = objPlayer["RunsScored"].ToString();
                        obj.OversFaced = objPlayer["OversFaced"].ToString();
                        obj.RunsConceded = objPlayer["RunsConceded"].ToString();
                        obj.OverBowled = objPlayer["OversBowled"].ToString();
                        obj.Points = objPlayer["Points"].ToString();
                        obj.Result = objPlayer["Result"].ToString();
                        obj.Remarks = objPlayer["Remarks"].ToString();
                        obj.Season = cbxseason.Text;
                        obj.Division = cbxdivision.Text;
                        obj.Team = cbxTeam.Text;
                        slno++;
                        lstTeamReport.Add(obj);

                    }
                    ReportViewer rv = frm.GetReportViewer();
                    rv.Reset();
                    rv.LocalReport.ReportEmbeddedResource = @"Cricket.Reports.TeamReport.rdlc";
                    ReportDataSource RDS = new ReportDataSource("TeamReportRe", lstTeamReport);
                    rv.LocalReport.DataSources.Add(RDS);
                    rv.Refresh();
                    frm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void cbxTeam_DropDownOpened(object sender, EventArgs e)
        {

            cbxTeam.ItemsSource = null;
            cbxTeam.Items.Clear();
            ObservableCollection<DivisionLoad> lstdivisionload = Database.GetEntityList<DivisionLoad>(false, false, false, oleconn, "RecordStatus", "DivisionLoadId");
            var objvalues2 = from s in lstdivisionload where s.SeasonId.ToString() == cbxseason.SelectedValue.ToString() select s;
            List<DivisionLoad> lstfilter2 = objvalues2.ToList<DivisionLoad>();



            if (lstfilter2.Count > 0)
            {
                foreach (var objload in lstfilter2.OrderBy(i => i.DivisionOne))
                {
                    if (cbxdivisionvalues.cbxvalues == "1st Division")
                    {
                        if (objload.DivisionOne != null)
                        {
                            cbxTeam.Items.Add(objload.DivisionOne);

                        }
                    }
                }
                foreach (var objload in lstfilter2.OrderBy(i => i.DivisionTwo))
                {
                    if (cbxdivisionvalues.cbxvalues == "2nd Division")
                    {
                        if (objload.DivisionTwo != null)
                        {
                            cbxTeam.Items.Add(objload.DivisionTwo);
                        }
                    }
                }
                foreach (var objload in lstfilter2.OrderBy(i => i.DivisionThree))
                {
                    if (cbxdivisionvalues.cbxvalues == "3rd Division")
                    {
                        if (objload.DivisionThree != null)
                        {
                            cbxTeam.Items.Add(objload.DivisionThree);
                        }
                    }
                }
            }

        }
      
        private void cbxdivision_MouseLeave(object sender, MouseEventArgs e)
        {
            cbxdivisionvalues.cbxvalues = cbxdivision.Text;
          
        }

        private void btnback_Click(object sender, RoutedEventArgs e)
        {
            dgvTeamReport.ItemsSource = null;
            Button btn = (Button)sender;
            btn.Command = NavigationCommands.GoToPage;
            btn.CommandParameter = "/Pages/Home.xaml";
        }

    }

    public static class cbxdivisionvalues
    {
        public static string cbxvalues;
       
    }
}

