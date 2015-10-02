
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

using CricketSol;
using CricketSol.DAL;
using CricketSol.Base;
using CricketSol.System;
using System.Configuration;
using CricketSol.Database;
using System.Collections.ObjectModel;
using Cricket.ReportData;
using Microsoft.Reporting.WinForms;

namespace Cricket.View
{
    /// <summary>
    /// Interaction logic for PointsTable.xaml
    /// </summary>
    public partial class PointsTable : UserControl
    {
        OleDbConnection oleconn = Database.getConnection();


        public PointsTable()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                //Select Season
                string strRetrieve = "";

                strRetrieve = "select SeasonType,SeasonId from Seasons";
                OleDbDataAdapter adpt = new OleDbDataAdapter(strRetrieve, oleconn);
                DataSet ds = new DataSet();
                adpt.Fill(ds);

                cbxseason.ItemsSource = ds.Tables[0].DefaultView;
                ds.Tables[0].DefaultView.Sort = "SeasonType";
                cbxseason.DisplayMemberPath = ds.Tables[0].Columns["SeasonType"].ToString();
                cbxseason.SelectedValuePath = ds.Tables[0].Columns["SeasonId"].ToString();

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
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //1st division
                ObservableCollection<FirstDivPointsCricket> lstscorecard1 = Database.GetEntityList<FirstDivPointsCricket>(false, false, false, oleconn, "RecordStatus", "FirstDivPointsCricketId");

                var objvalues1 = from s in lstscorecard1 where s.DivisionId.ToString() == cbxdivision.SelectedValue.ToString() && s.SeasonId.ToString() == cbxseason.SelectedValue.ToString() && s.Group ==cbxgroup.Text select s;
                List<FirstDivPointsCricket> lstfilter1 = objvalues1.OrderByDescending(p => p.Won).ThenByDescending(p => p.Points).ThenByDescending(p => p.RunRate).ToList<FirstDivPointsCricket>();


                //2nd division
                ObservableCollection<SecondDivPointsCricket> lstscorecard = Database.GetEntityList<SecondDivPointsCricket>(false, false, false, oleconn, "RecordStatus", "SecondDivPointsCricketId");

                var objvalues = from s in lstscorecard where s.DivisionId.ToString() == cbxdivision.SelectedValue.ToString() && s.SeasonId.ToString() == cbxseason.SelectedValue.ToString() && s.Group== cbxgroup.Text select s;
                List<SecondDivPointsCricket> lstfilter = objvalues.OrderByDescending(p => p.Won).ThenByDescending(p => p.Points).ThenByDescending(p => p.RunRate).ToList<SecondDivPointsCricket>();




                DataTable dt = new DataTable();
                dt.Columns.Add("SerialNo");
                dt.Columns.Add("Team Name");
                dt.Columns.Add("Matches");
                dt.Columns.Add("Won");
                dt.Columns.Add("Lost");
                dt.Columns.Add("Tie");
                dt.Columns.Add("No Result");
                dt.Columns.Add("Points");
                dt.Columns.Add("Run Rate");
                dt.Columns.Add("For");
                dt.Columns.Add("Against");

                bool teamfound = false;
                int SerialNo = 1;


                //1st div
                foreach (var objscore1 in lstfilter1)
                {
                    if (objscore1.SeasonId.ToString() == cbxseason.SelectedValue.ToString())
                    {
                        if (objscore1.DivisionId.ToString() == cbxdivision.SelectedValue.ToString())
                        {

                            DataRow dr1 = dt.NewRow();

                            dr1["SerialNo"] = SerialNo;
                            dr1["Team Name"] = objscore1.TeamName;
                            dr1["Matches"] = objscore1.Matches;
                            dr1["Won"] = objscore1.Won;
                            dr1["Lost"] = objscore1.Lost;
                            dr1["Tie"] = objscore1.Tie;
                            dr1["No Result"] = objscore1.NoResult;
                            dr1["Points"] = objscore1.Points;
                            dr1["Run Rate"] = objscore1.RunRate;
                            dr1["For"] = objscore1.For;
                            dr1["Against"] = objscore1.Against;
                            dt.Rows.Add(dr1);
                            SerialNo++;
                            teamfound = true;
                        }
                    }
                }

               

               

                //2nd divi
                foreach (var objscore in lstfilter)
                {
                    if (objscore.SeasonId.ToString() == cbxseason.SelectedValue.ToString())
                    {
                        if (objscore.DivisionId.ToString() == cbxdivision.SelectedValue.ToString())
                        {
                            
                            DataRow dr = dt.NewRow();

                            dr["SerialNo"] = SerialNo;
                            dr["Team Name"] = objscore.TeamName;
                            dr["Matches"] = objscore.Matches;
                            dr["Won"] = objscore.Won;
                            dr["Lost"] = objscore.Lost;
                            dr["Tie"] = objscore.Tie;
                            dr["No Result"] = objscore.NoResult;
                            dr["Points"] = objscore.Points;
                            dr["Run Rate"] = objscore.RunRate;
                            dr["For"] = objscore.For;
                            dr["Against"] = objscore.Against;
                            dt.Rows.Add(dr);
                            SerialNo++;
                             teamfound = true;

                        }
                    }
                }



                if (teamfound == false)
                {
                    MessageBox.Show("No Records Found");
                }

                dgvPointsTable.ItemsSource = null;

                dgvPointsTable.DataContext = dt;
                dgvPointsTable.ItemsSource = dt.DefaultView;

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
                PointsTableReportForm frm = new PointsTableReportForm();
                List<PointsTableReport> lstPointsTableReport = new List<PointsTableReport>();

                DataTable dt = new DataTable();
                // DataSet ds = new DataSet();
                dt = dgvPointsTable.DataContext as DataTable;
                // ds.Tables.Add(dt);

                int slno = 1;
                foreach (DataRowView objPlayer in dt.DefaultView)
                {
                    PointsTableReport obj = new PointsTableReport();

                    obj.SlNo = slno;
                    obj.TeamName = objPlayer["Team Name"].ToString();
                    obj.Matches = objPlayer["Matches"].ToString();
                    obj.Won = objPlayer["Won"].ToString();
                    obj.Lost = objPlayer["Lost"].ToString();
                    obj.Tie = objPlayer["Tie"].ToString();
                    obj.NoResult = objPlayer["No Result"].ToString();
                    obj.Points = objPlayer["Points"].ToString();
                    obj.RunRate = objPlayer["Run Rate"].ToString();
                    obj.For = objPlayer["For"].ToString();
                    obj.Against = objPlayer["Against"].ToString();
                    obj.Season = cbxseason.Text;
                    obj.Division = cbxdivision.Text;
                    obj.Group = cbxgroup.Text;
                    slno++;
                    lstPointsTableReport.Add(obj);

                }
                ReportViewer rv = frm.GetReportViewer();
                rv.Reset();
                rv.LocalReport.ReportEmbeddedResource = @"Cricket.Reports.PointsTableReport.rdlc";
                ReportDataSource RDS = new ReportDataSource("PointsTable", lstPointsTableReport);
                rv.LocalReport.DataSources.Add(RDS);
                rv.Refresh();
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            dgvPointsTable.ItemsSource = null;
            Button btn = (Button)sender;
            btn.Command = NavigationCommands.GoToPage;
            btn.CommandParameter = "/Pages/Home.xaml";
        }
    }
}
