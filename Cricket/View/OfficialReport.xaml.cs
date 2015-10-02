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
    /// Interaction logic for OfficialReport.xaml
    /// </summary>
    public partial class OfficialReport : UserControl
    {
        OleDbConnection oleconn = Database.getConnection();

        public OfficialReport()
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

                //Select Officail
                string strRetrieve2 = "";

                strRetrieve2 = "select * from Official";

                OleDbDataAdapter adpt2 = new OleDbDataAdapter(strRetrieve2, oleconn);

                DataSet ds2 = new DataSet();
                adpt2.Fill(ds2);
                ds2.Tables[0].DefaultView.Sort = "OfficialPrimaryId";
                cbxofficial.ItemsSource = ds2.Tables[0].DefaultView;
                cbxofficial.DisplayMemberPath = ds2.Tables[0].Columns["OfficialName"].ToString();
                cbxofficial.SelectedValuePath = ds2.Tables[0].Columns["OfficialId"].ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btnUmpire_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                UmpireReportForm frm = new UmpireReportForm();
                List<UmpireReport> lstUmpireReport = new List<UmpireReport>();
                DataTable dt = new DataTable();
                dt = dgvUmpireReport.DataContext as DataTable;

                int slno = 1;
                foreach (DataRowView objPlayer in dt.DefaultView)
                {
                    UmpireReport obj = new UmpireReport();

                    obj.SlNo = slno;
                    obj.Date = objPlayer["Date"].ToString();
                    obj.Division = objPlayer["Division"].ToString();
                    obj.MatchId = objPlayer["MatchID"].ToString();
                    obj.Group = objPlayer["Group"].ToString();
                    obj.TeamOne = objPlayer["Team One"].ToString();
                    obj.TeamTwo = objPlayer["Team Two"].ToString();
                    obj.Location = objPlayer["Location"].ToString();
                    obj.OtherUmpire = objPlayer["Other Umpire"].ToString();
                    obj.Scorer = objPlayer["Scorer"].ToString();
                    obj.Result = objPlayer["Result"].ToString();
                    obj.Season = cbxseason.Text;
                    obj.Name = cbxofficial.Text;
                    slno++;
                    lstUmpireReport.Add(obj);

                }
                ReportViewer rv = frm.GetReportViewer();
                rv.Reset();
                rv.LocalReport.ReportEmbeddedResource = @"Cricket.Reports.UmpireReport.rdlc";
                ReportDataSource RDS = new ReportDataSource("UmpireReport", lstUmpireReport);
                rv.LocalReport.DataSources.Add(RDS);
                rv.Refresh();
                frm.ShowDialog();


                //Scorer Report
            }
            catch (Exception ex)
            {
                throw ex;
            }
     
        }

        private void btnScorer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ScorerReportForm frm1 = new ScorerReportForm();
                List<ScorerReport> lstScorerReport = new List<ScorerReport>();
                DataTable dt1 = new DataTable();
                dt1 = dgvScorerReport.DataContext as DataTable;

                int slno1 = 1;
                foreach (DataRowView objPlayer in dt1.DefaultView)
                {
                    ScorerReport obj = new ScorerReport();

                    obj.SlNo = slno1;
                    obj.Date = objPlayer["Date"].ToString();
                    obj.Division = objPlayer["Division"].ToString();
                    obj.MatchId = objPlayer["MatchID"].ToString();
                    obj.Group = objPlayer["Group"].ToString();
                    obj.TeamOne = objPlayer["Team One"].ToString();
                    obj.TeamTwo = objPlayer["Team Two"].ToString();
                    obj.Location = objPlayer["Location"].ToString();
                    obj.UmpireOne = objPlayer["Umpire One"].ToString();
                    obj.UmpireTwo = objPlayer["Umpire Two"].ToString();
                    obj.Result = objPlayer["Result"].ToString();
                    obj.Season = cbxseason.Text;
                    obj.Name = cbxofficial.Text;
                    slno1++;
                    lstScorerReport.Add(obj);

                }
                ReportViewer rv1 = frm1.GetReportViewer();
                rv1.Reset();
                rv1.LocalReport.ReportEmbeddedResource = @"Cricket.Reports.ScorerReport.rdlc";
                ReportDataSource RDS1 = new ReportDataSource("ScorerReport", lstScorerReport);
                rv1.LocalReport.DataSources.Add(RDS1);
                rv1.Refresh();
                frm1.ShowDialog();

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

                if(cbxofficial.SelectedValue == null)
                {
                    MessageBox.Show("Select Official to View the Details");

                }

                if (cbxseason.SelectedValue == null)
                {
                    MessageBox.Show("Select Season");

                }

                else
                {
                    //umpire
                    lblumpirename.Content = string.Empty;
                    lblumpirename.Content = cbxofficial.Text;

                    //1st div
                    ObservableCollection<LocationReportTable> lstOfficialreport1 = Database.GetEntityList<LocationReportTable>(false, false, false, oleconn, "RecordStatus", "LocationName");

                    var objvalues1 = from s in lstOfficialreport1 where s.SeasonId.ToString() == cbxseason.SelectedValue.ToString() select s;
                    List<LocationReportTable> lstfilter1 = objvalues1.OrderBy(p => p.Division).ToList<LocationReportTable>();


                    ////2nd div
                    //ObservableCollection<LocationReportTable> lstOfficialreport2 = Database.GetEntityList<LocationReportTable>(false, false, false, oleconn, "RecordStatus", "LocationName");

                    //var objvalues2 = from s in lstOfficialreport2 where s.SeasonId.ToString() == cbxseason.SelectedValue.ToString() select s;
                    //List<LocationReportTable> lstfilter2 = objvalues2.OrderBy(p => p.Division).ToList<LocationReportTable>();

                    DataTable dt = new DataTable();
                    dt.Columns.Add("SerialNo");
                    dt.Columns.Add("Date");
                    dt.Columns.Add("Division");
                    dt.Columns.Add("MatchID");
                    dt.Columns.Add("Group");
                    dt.Columns.Add("Team One");
                    dt.Columns.Add("Team Two");
                    dt.Columns.Add("Location");
                    dt.Columns.Add("Other Umpire");
                    dt.Columns.Add("Scorer");
                    dt.Columns.Add("Result");

                    int SerialNo = 1;
                    bool umpireexist = false;

                    //1st div
                    foreach (LocationReportTable objofficalreport1 in lstfilter1)
                    {
                        if (objofficalreport1.SeasonId.ToString() == cbxseason.SelectedValue.ToString())
                        {
                            if ((objofficalreport1.UmpireOne.ToString() == cbxofficial.Text) || (objofficalreport1.UmpireTwo.ToString() == cbxofficial.Text))
                            {
                                string startdate;
                                DataRow dr = dt.NewRow();
                                string date = objofficalreport1.StartDate;

                                if (date.Contains(" "))
                                {

                                     startdate = date.Substring(0, date.IndexOf(" "));
                                }
                                else
                                {
                                     startdate = objofficalreport1.StartDate;
                                }

                                dr["SerialNo"] = SerialNo;
                                dr["Date"] = startdate;
                                dr["Division"] = objofficalreport1.Division;
                                dr["MatchID"] = objofficalreport1.MatchId;
                                dr["Group"] = objofficalreport1.Group;
                                dr["Team One"] = objofficalreport1.TeamOne;
                                dr["Team Two"] = objofficalreport1.TeamTwo;
                                dr["Location"] = objofficalreport1.LocationName;
                                if (objofficalreport1.UmpireOne == cbxofficial.Text)
                                {
                                    dr["Other Umpire"] = objofficalreport1.UmpireTwo;

                                }
                                else if (objofficalreport1.UmpireTwo == cbxofficial.Text)
                                {
                                    dr["Other Umpire"] = objofficalreport1.UmpireOne;

                                }

                                dr["Scorer"] = objofficalreport1.Scorer;
                                dr["Result"] = objofficalreport1.Result;
                                dt.Rows.Add(dr);
                                SerialNo++;
                                umpireexist = true;
                            }
                            //break;

                        }
                    }




                    if (umpireexist == false)
                    {
                        MessageBox.Show("No Umpiring Record Found");
                    }

                    dgvUmpireReport.ItemsSource = null;
                    dgvUmpireReport.DataContext = dt;
                    dgvUmpireReport.ItemsSource = dt.DefaultView;

                    //Scorer

                    lblscorername.Content = string.Empty;
                    lblscorername.Content = cbxofficial.Text;



                    ObservableCollection<LocationReportTable> lstOfficialreport3 = Database.GetEntityList<LocationReportTable>(false, false, false, oleconn, "RecordStatus", "LocationName");

                    var objvalues3 = from s in lstOfficialreport3 where s.SeasonId.ToString() == cbxseason.SelectedValue.ToString() select s;
                    List<LocationReportTable> lstfilter3 = objvalues3.OrderBy(p => p.Division).ToList<LocationReportTable>();

                    DataTable dt2 = new DataTable();
                    dt2.Columns.Add("SerialNo");
                    dt2.Columns.Add("Date");
                    dt2.Columns.Add("Division");
                    dt2.Columns.Add("MatchID");
                    dt2.Columns.Add("Group");
                    dt2.Columns.Add("Team One");
                    dt2.Columns.Add("Team Two");
                    dt2.Columns.Add("Location");
                    dt2.Columns.Add("Umpire One");
                    dt2.Columns.Add("Umpire Two");
                    dt2.Columns.Add("Result");

                    int SerialNo2 = 1;
                    bool scorerexist = false;
                    foreach (LocationReportTable objofficalreport3 in lstfilter3)
                    {
                        if (objofficalreport3.SeasonId.ToString() == cbxseason.SelectedValue.ToString())
                        {
                            if (objofficalreport3.Scorer.ToString() == cbxofficial.Text)
                            {
                                DataRow dr2 = dt2.NewRow();

                                dr2["SerialNo"] = SerialNo2;
                                dr2["Date"] = objofficalreport3.StartDate;
                                dr2["Division"] = objofficalreport3.Division;
                                dr2["MatchID"] = objofficalreport3.MatchId;
                                dr2["Group"] = objofficalreport3.Group;
                                dr2["Team One"] = objofficalreport3.TeamOne;
                                dr2["Team Two"] = objofficalreport3.TeamTwo;
                                dr2["Location"] = objofficalreport3.LocationName;
                                dr2["Umpire One"] = objofficalreport3.UmpireOne;
                                dr2["Umpire Two"] = objofficalreport3.UmpireTwo;
                                dr2["Result"] = objofficalreport3.Result;
                                dt2.Rows.Add(dr2);
                                SerialNo2++;
                                scorerexist = true;
                            }
                            //break;

                        }
                    }
                    if (scorerexist == false)
                    {
                        MessageBox.Show("No Scorer Record Found");
                    }

                    dgvScorerReport.ItemsSource = null;
                    dgvScorerReport.DataContext = dt2;
                    dgvScorerReport.ItemsSource = dt2.DefaultView;

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btnback_Click(object sender, RoutedEventArgs e)
        {
            dgvScorerReport.ItemsSource = null;
            dgvUmpireReport.ItemsSource = null;


            Button btn = (Button)sender;
            btn.Command = NavigationCommands.GoToPage;
            btn.CommandParameter = "/Pages/Home.xaml";
        }
    }
}
