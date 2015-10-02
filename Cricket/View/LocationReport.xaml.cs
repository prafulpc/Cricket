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
    /// Interaction logic for LocationReport.xaml
    /// </summary>
    public partial class LocationReport : UserControl
    {
        OleDbConnection oleconn = Database.getConnection();


        public LocationReport()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                lbllocationname.Content = string.Empty;
                lbllocationname.Content = cbxlocation.Text;

                //1st div
                ObservableCollection<LocationReportTable> lstLocationreport1 = Database.GetEntityList<LocationReportTable>(false, false, false, oleconn, "RecordStatus", "LocationName");

                var objvalues1 = from s in lstLocationreport1 where s.SeasonId.ToString() == cbxseason.SelectedValue.ToString() select s;
                List<LocationReportTable> lstfilter1 = objvalues1.OrderBy(p => p.Division).ToList<LocationReportTable>();

                //2nd div
                //ObservableCollection<LocationReportTable> lstLocationreport2 = Database.GetEntityList<LocationReportTable>(false, false, false, oleconn, "RecordStatus", "LocationName");

                //var objvalues2 = from s in lstLocationreport2 where s.SeasonId.ToString() == cbxseason.SelectedValue.ToString() select s;
                //List<LocationReportTable> lstfilter2 = objvalues2.OrderBy(p => p.Division).ToList<LocationReportTable>();





                DataTable dt = new DataTable();
                dt.Columns.Add("SerialNo");
                dt.Columns.Add("Start Date");
                dt.Columns.Add("End Date");
                dt.Columns.Add("Division");
                dt.Columns.Add("Group");
                dt.Columns.Add("Match Type");
                dt.Columns.Add("MatchID");
                dt.Columns.Add("Team One");
                dt.Columns.Add("Team Two");
                dt.Columns.Add("Umpire One");
                dt.Columns.Add("Umpire Two");
                dt.Columns.Add("Scorer");
                dt.Columns.Add("Result");

                int SerialNo = 1;
                bool locationexist = false;


                //1st div
                foreach (LocationReportTable objlocationreport in lstfilter1)
                {
                    if (objlocationreport.SeasonId.ToString() == cbxseason.SelectedValue.ToString())
                    {
                        if (objlocationreport.LocationId.ToString() == cbxlocation.SelectedValue.ToString())
                        {
                            DataRow dr1 = dt.NewRow();

                            dr1["SerialNo"] = SerialNo;
                            dr1["Start Date"] = objlocationreport.StartDate;
                            dr1["End Date"] = objlocationreport.EndDate;
                            dr1["Division"] = objlocationreport.Division;
                            dr1["Group"] = objlocationreport.Group;
                            dr1["Match Type"] = objlocationreport.Matchtype;
                            dr1["MatchID"] = objlocationreport.MatchId;
                            dr1["Team One"] = objlocationreport.TeamOne;
                            dr1["Team Two"] = objlocationreport.TeamTwo;
                            dr1["Umpire One"] = objlocationreport.UmpireOne;
                            dr1["Umpire Two"] = objlocationreport.UmpireTwo;
                            dr1["Scorer"] = objlocationreport.Scorer;
                            dr1["Result"] = objlocationreport.Result;
                            dt.Rows.Add(dr1);
                            SerialNo++;
                            locationexist = true;
                        }
                        //break;

                    }
                }


                //2nd div
                //foreach (LocationReportTable objlocationreport in lstfilter2)
                //{
                //    if (objlocationreport.SeasonId.ToString() == cbxseason.SelectedValue.ToString())
                //    {
                //        if (objlocationreport.LocationId.ToString() == cbxlocation.SelectedValue.ToString())
                //        {
                //            DataRow dr = dt.NewRow();

                //            dr["SerialNo"] = SerialNo;
                //            dr["Start Date"] = objlocationreport.StartDate;
                //            dr["End Date"] = objlocationreport.EndDate;
                //            dr["Division"] = objlocationreport.Division;
                //            dr["Group"] = objlocationreport.Group;
                //            dr["Match Type"] = objlocationreport.Matchtype;
                //            dr["MatchID"] = objlocationreport.MatchId;
                //            dr["Team One"] = objlocationreport.TeamOne;
                //            dr["Team Two"] = objlocationreport.TeamTwo;
                //            dr["Umpire One"] = objlocationreport.UmpireOne;
                //            dr["Umpire Two"] = objlocationreport.UmpireTwo;
                //            dr["Scorer"] = objlocationreport.Scorer;
                //            dr["Result"] = objlocationreport.Result;
                //            dt.Rows.Add(dr);
                //            SerialNo++;
                //            locationexist = true;
                //        }
                //        //break;

                //    }
                //}



                if (locationexist == false)
                {
                    MessageBox.Show("No records Found For Selected Team");
                }

                dgvTeamReport.ItemsSource = null;

                dgvTeamReport.DataContext = dt;
                dgvTeamReport.ItemsSource = dt.DefaultView;
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

                //Select Ground
                string strRetrieve2 = "";

                strRetrieve2 = "select LocationId,StadiumName from Locations";

                OleDbDataAdapter adpt2 = new OleDbDataAdapter(strRetrieve2, oleconn);

                DataSet ds2 = new DataSet();
                adpt2.Fill(ds2);
                ds2.Tables[0].DefaultView.Sort = "StadiumName";
                cbxlocation.ItemsSource = ds2.Tables[0].DefaultView;
                cbxlocation.DisplayMemberPath = ds2.Tables[0].Columns["StadiumName"].ToString();
                cbxlocation.SelectedValuePath = ds2.Tables[0].Columns["LocationId"].ToString();
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
                LocationReportForm frm = new LocationReportForm();
                List<LocationReportRe> lstLocationReportRe = new List<LocationReportRe>();

                DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                dt = dgvTeamReport.DataContext as DataTable;
                ds.Tables.Add(dt);

                int slno = 1;
                foreach (DataRowView objPlayer in dt.DefaultView)
                {
                    LocationReportRe obj = new LocationReportRe();


                    obj.SlNo = slno;
                    obj.StartDate = objPlayer["Start Date"].ToString();
                    obj.EndDate = objPlayer["End Date"].ToString();
                    obj.Division = objPlayer["Division"].ToString();
                    obj.Group = objPlayer["Group"].ToString();
                    obj.MatchType = objPlayer["Match Type"].ToString();
                    obj.MatchId = objPlayer["MatchID"].ToString();
                    obj.TeamOne = objPlayer["Team One"].ToString();
                    obj.TeamTwo = objPlayer["Team Two"].ToString();
                    obj.UmpireOne = objPlayer["Umpire One"].ToString();
                    obj.UmpireTwo = objPlayer["Umpire Two"].ToString();
                    obj.Scorer = objPlayer["Scorer"].ToString();
                    obj.Result = objPlayer["Result"].ToString();
                    obj.Season = cbxseason.Text;
                    obj.Location = cbxlocation.Text;

                    slno++;
                    lstLocationReportRe.Add(obj);

                }
                ReportViewer rv = frm.GetReportViewer();
                rv.Reset();
                rv.LocalReport.ReportEmbeddedResource = @"Cricket.Reports.LocationReport.rdlc";
                ReportDataSource RDS = new ReportDataSource("LocationReportRe", lstLocationReportRe);
                rv.LocalReport.DataSources.Add(RDS);
                rv.Refresh();
                frm.ShowDialog();
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btnback_Click(object sender, RoutedEventArgs e)
        {
            dgvTeamReport.ItemsSource = null;
            Button btn = (Button)sender;
            btn.Command = NavigationCommands.GoToPage;
            btn.CommandParameter = "/Pages/Home.xaml";
        }
    }
}
