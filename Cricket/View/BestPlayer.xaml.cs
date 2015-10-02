using Cricket.ReportData;
using CricketSol.DAL;
using CricketSol.Database;
using Microsoft.Reporting.WinForms;
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
    /// Interaction logic for BestPlayer.xaml
    /// </summary>
    public partial class BestPlayer : UserControl
    {
        OleDbConnection oleconn = Database.getConnection();



        public BestPlayer()
        {
            InitializeComponent();

        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            
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

        private void btnOk_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                if(cbxdivision.SelectedItem == null)
                {
                    MessageBox.Show("Select Division");

                }

                if (cbxseason.SelectedItem == null)
                {
                    MessageBox.Show("Select Season");

                }

                else
                {
                    ObservableCollection<BestBatsman> lstBestBatsman = Database.GetEntityList<BestBatsman>(false, false, false, oleconn, "RecordStatus", "BestBatsmanId");
                    ObservableCollection<BestBowler> lstsBestBowler = Database.GetEntityList<BestBowler>(false, false, false, oleconn, "RecordStatus", "BestBowlerId");
                    ObservableCollection<BestBowler> lbxFixtures = Database.GetEntityList<BestBowler>(false, false, false, oleconn, "RecordStatus", "BestBowlerId");
        
                    //batsmen
                    var objvalues = from s in lstBestBatsman where s.DivisionId.ToString() == cbxdivision.SelectedValue.ToString() && s.SeasonId.ToString() == cbxseason.SelectedValue.ToString() select s;
                    List<BestBatsman> lstBestBatsmanfilter = objvalues.OrderByDescending(p => p.RunsScored).ThenBy(p => p.StrikeRate).ThenBy(p => p.Matches).ToList<BestBatsman>();

                    //bowler
                    var objbestbowler = from s in lstsBestBowler where s.DivisionId.ToString() == cbxdivision.SelectedValue.ToString() && s.SeasonId.ToString() == cbxseason.SelectedValue.ToString() select s;
                    List<BestBowler> lstBestBowlerfilter = objbestbowler.OrderByDescending(p => p.Wickets).ThenBy(p => p.Overs).ThenBy(p => p.Econ).ToList<BestBowler>();


                    DataTable dt = new DataTable();
                    DataTable dt1 = new DataTable();

                    //batsmen
                    dt.Columns.Add("SerialNo");
                    dt.Columns.Add("PlayerName");
                    dt.Columns.Add("KSCAUID");
                    dt.Columns.Add("TeamName");
                    dt.Columns.Add("Matches");
                    dt.Columns.Add("Innings");
                    dt.Columns.Add("NotOut");
                    dt.Columns.Add("Runs");
                    dt.Columns.Add("HighestScore");
                    dt.Columns.Add("Strikerate");
                    dt.Columns.Add("100s");
                    dt.Columns.Add("50s");
                    dt.Columns.Add("4s");
                    dt.Columns.Add("6s");

                    //bowler
                    dt1.Columns.Add("SerialNo");
                    dt1.Columns.Add("PlayerName");
                    dt1.Columns.Add("KSCAUID");
                    dt1.Columns.Add("TeamName");
                    dt1.Columns.Add("Matches");
                    dt1.Columns.Add("Innings");
                    dt1.Columns.Add("Overs");
                    dt1.Columns.Add("Maidens");
                    dt1.Columns.Add("Runs");
                    dt1.Columns.Add("Wickets");
                    dt1.Columns.Add("Economy");


                    int SerialNo = 1;
                    foreach (BestBatsman objscore in lstBestBatsmanfilter)
                    {
                        if (objscore.SeasonId.ToString() == cbxseason.SelectedValue.ToString())
                        {
                            if (objscore.DivisionId.ToString() == cbxdivision.SelectedValue.ToString())
                            {

                                DataRow dr = dt.NewRow();

                                dr["SerialNo"] = SerialNo;
                                dr["PlayerName"] = objscore.PlayerName;
                                dr["KSCAUID"] = objscore.KSCAUID;
                                dr["TeamName"] = objscore.TeamName;
                                dr["Matches"] = objscore.Matches;
                                dr["Innings"] = objscore.Innings;
                                dr["4s"] = objscore.Fours;
                                dr["6s"] = objscore.Sixes;
                                dr["NotOut"] = objscore.NotOut;
                                dr["Runs"] = objscore.RunsScored;
                                dr["HighestScore"] = objscore.HighestScore;
                                dr["Strikerate"]  =Math.Round( objscore.StrikeRate,1);
                                dr["100s"] = objscore.Hundreds;
                                dr["50s"] = objscore.Fifties;
                                dt.Rows.Add(dr);
                                SerialNo++;

                            }
                        }
                    }
                    dgvBatting.ItemsSource = null;
                    dgvBatting.DataContext = dt;
                    dgvBatting.ItemsSource = dt.DefaultView;

                    lbxFixtures.Clear();
                    lstBestBatsman.Clear();
                    lstsBestBowler.Clear();

                    int SerialNo1 = 1;
                    foreach (BestBowler objBestBowler in lstBestBowlerfilter)
                    {
                        if (objBestBowler.SeasonId.ToString() == cbxseason.SelectedValue.ToString())
                        {
                            if (objBestBowler.DivisionId.ToString() == cbxdivision.SelectedValue.ToString())
                            {

                                DataRow dr = dt1.NewRow();

                                dr["SerialNo"] = SerialNo1;
                                dr["PlayerName"] = objBestBowler.PlayerName;
                                dr["KSCAUID"] = objBestBowler.KSCAUID;
                                dr["TeamName"] = objBestBowler.TeamName;
                                dr["Matches"] = objBestBowler.Matches;
                                dr["Innings"] = objBestBowler.Innings;
                                dr["Overs"] = Math.Round(objBestBowler.Overs,1);
                                dr["Maidens"] = objBestBowler.Maidens;
                                dr["Runs"] = objBestBowler.Runs;
                                dr["Wickets"] = objBestBowler.Wickets;
                                dr["Economy"] =Math.Round( objBestBowler.Econ,1);
                                dt1.Rows.Add(dr);
                                SerialNo1++;

                            }
                        }
                    }
                    dgvBowling.ItemsSource = null;

                    dgvBowling.DataContext = dt1;
                    dgvBowling.ItemsSource = dt1.DefaultView;

                    lbxFixtures.Clear();
                    lstBestBatsman.Clear();
                    lstsBestBowler.Clear();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btnbatsmen_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BestBatsmenReportForm frm = new BestBatsmenReportForm();
                List<BestBatsmenReport> lstTeamReport = new List<BestBatsmenReport>();

                DataTable dt = new DataTable();
                dt.Clear();
                DataSet ds = new DataSet();
                dt = dgvBatting.DataContext as DataTable;
                ds.Tables.Add(dt);

                int slno = 1;
                foreach (DataRowView objPlayer in dt.DefaultView)
                {
                    BestBatsmenReport obj = new BestBatsmenReport();

                    obj.SlNo = slno;
                    obj.PlayeName = objPlayer["PlayerName"].ToString();
                    obj.KSCAUID = objPlayer["KSCAUID"].ToString();
                    obj.TeamName = objPlayer["TeamName"].ToString();
                    obj.Matches = objPlayer["Matches"].ToString();
                    obj.Innings = objPlayer["Innings"].ToString();
                    obj.NotOut = objPlayer["NotOut"].ToString();
                    obj.Runs = objPlayer["Runs"].ToString();
                    obj.HighestScore = objPlayer["HighestScore"].ToString();
                    obj.StrikeRate = objPlayer["Strikerate"].ToString();
                    obj.Hundreds = objPlayer["100s"].ToString();
                    obj.Fifties = objPlayer["50s"].ToString();
                    obj.Fours = objPlayer["4s"].ToString();
                    obj.sixes = objPlayer["6s"].ToString();
                    obj.Season = cbxseason.Text;
                    obj.Division = cbxdivision.Text;

                    slno++;
                    lstTeamReport.Add(obj);

                }
                ReportViewer rv = frm.GetReportViewer();
                rv.Reset();
                rv.LocalReport.ReportEmbeddedResource = @"Cricket.Reports.BestBatsmenReport.rdlc";
                ReportDataSource RDS = new ReportDataSource("BestBatsmenReport", lstTeamReport);
                rv.LocalReport.DataSources.Add(RDS);
                rv.Refresh();
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btnBowler_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BestBowlerReportForm frm = new BestBowlerReportForm();
                List<BestBowlerReport> lstTeamReport = new List<BestBowlerReport>();

                DataTable dt1 = new DataTable();
                DataSet ds = new DataSet();
                dt1 = dgvBowling.DataContext as DataTable;
                ds.Tables.Add(dt1);

                int slno = 1;
                foreach (DataRowView objPlayer in dt1.DefaultView)
                {
                    BestBowlerReport obj = new BestBowlerReport();

                    obj.SlNo = slno;
                    obj.PlayeName = objPlayer["PlayerName"].ToString();
                    obj.KSCAUID = objPlayer["KSCAUID"].ToString();
                    obj.TeamName = objPlayer["TeamName"].ToString();
                    obj.Matches = objPlayer["Matches"].ToString();
                    obj.Innings = objPlayer["Innings"].ToString();
                    obj.Overs = objPlayer["Overs"].ToString();
                    obj.Maidens = objPlayer["Maidens"].ToString();
                    obj.Runs = objPlayer["Runs"].ToString();
                    obj.Wickets = objPlayer["Wickets"].ToString();
                    obj.Economy = objPlayer["Economy"].ToString();
                    obj.Season = cbxseason.Text;
                    obj.Division = cbxdivision.Text;

                    slno++;
                    lstTeamReport.Add(obj);

                }
                ReportViewer rv = frm.GetReportViewer();
                rv.Reset();
                rv.LocalReport.ReportEmbeddedResource = @"Cricket.Reports.BestBowlerReport.rdlc";
                ReportDataSource RDS = new ReportDataSource("BestBowlerReport", lstTeamReport);
                rv.LocalReport.DataSources.Add(RDS);
                rv.Refresh();
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public void Load()
        //{
        //    try
        //    {
        //        ObservableCollection<BestBatsman> lstBestBatsman = Database.GetEntityList<BestBatsman>(false, false, false, oleconn, "RecordStatus", "BestBatsmanId");
        //        ObservableCollection<BestBowler> lstsBestBowler = Database.GetEntityList<BestBowler>(false, false, false, oleconn, "RecordStatus", "BestBowlerId");
        //        ObservableCollection<BestBowler> lbxFixtures = Database.GetEntityList<BestBowler>(false, false, false, oleconn, "RecordStatus", "BestBowlerId");
        
        //        //batsmen
        //        var objvalues = from s in lstBestBatsman where s.DivisionId.ToString() == cbxdivision.SelectedValue.ToString() && s.SeasonId.ToString() == cbxseason.SelectedValue.ToString() select s;
        //        List<BestBatsman> lstBestBatsmanfilter = objvalues.OrderByDescending(p => p.RunsScored).ThenBy(p => p.StrikeRate).ThenBy(p => p.Matches).ToList<BestBatsman>();

        //        //bowler
        //        var objbestbowler = from s in lstsBestBowler where s.DivisionId.ToString() == cbxdivision.SelectedValue.ToString() && s.SeasonId.ToString() == cbxseason.SelectedValue.ToString() select s;
        //        List<BestBowler> lstBestBowlerfilter = objbestbowler.OrderByDescending(p => p.Wickets).ThenBy(p => p.Econ).ThenBy(p => p.Matches).ToList<BestBowler>();


        //        DataTable dt = new DataTable();
        //        DataTable dt1 = new DataTable();

        //        //batsmen
        //        dt.Columns.Add("SerialNo");
        //        dt.Columns.Add("PlayerName");
        //        dt.Columns.Add("KSCAUID");
        //        dt.Columns.Add("TeamName");
        //        dt.Columns.Add("Matches");
        //        dt.Columns.Add("Innings");
        //        dt.Columns.Add("NotOut");
        //        dt.Columns.Add("Runs");
        //        dt.Columns.Add("HighestScore");
        //        dt.Columns.Add("Strikerate");
        //        dt.Columns.Add("100s");
        //        dt.Columns.Add("50s");
        //        dt.Columns.Add("4s");
        //        dt.Columns.Add("6s");

        //        //bowler
        //        dt1.Columns.Add("SerialNo");
        //        dt1.Columns.Add("PlayerName");
        //        dt1.Columns.Add("KSCAUID");
        //        dt1.Columns.Add("TeamName");
        //        dt1.Columns.Add("Matches");
        //        dt1.Columns.Add("Innings");
        //        dt1.Columns.Add("Overs");
        //        dt1.Columns.Add("Maidens");
        //        dt1.Columns.Add("Runs");
        //        dt1.Columns.Add("Wickets");
        //        dt1.Columns.Add("Economy");


        //        int SerialNo = 1;
        //        foreach (BestBatsman objscore in lstBestBatsmanfilter)
        //        {
        //            if (objscore.SeasonId.ToString() == cbxseason.SelectedValue.ToString())
        //            {
        //                if (objscore.DivisionId.ToString() == cbxdivision.SelectedValue.ToString())
        //                {

        //                    DataRow dr = dt.NewRow();

        //                    dr["SerialNo"] = SerialNo;
        //                    dr["PlayerName"] = objscore.PlayerName;
        //                    dr["KSCAUID"] = objscore.KSCAUID;
        //                    dr["TeamName"] = objscore.TeamName;
        //                    dr["Matches"] = objscore.Matches;
        //                    dr["Innings"] = objscore.Innings;
        //                    dr["4s"] = objscore.Fours;
        //                    dr["6s"] = objscore.Sixes;
        //                    dr["NotOut"] = objscore.NotOut;
        //                    dr["Runs"] = objscore.RunsScored;
        //                    dr["HighestScore"] = objscore.HighestScore;
        //                    dr["Strikerate"] = objscore.StrikeRate;
        //                    dr["100s"] = objscore.Hundreds;
        //                    dr["50s"] = objscore.Fifties;
        //                    dt.Rows.Add(dr);
        //                    SerialNo++;

        //                }
        //            }
        //        }
        //        dgvBatting.ItemsSource = null;
        //        dgvBatting.DataContext = dt;
        //        dgvBatting.ItemsSource = dt.DefaultView;

        //        int SerialNo1 = 1;
        //        foreach (BestBowler objBestBowler in lstBestBowlerfilter)
        //        {
        //            if (objBestBowler.SeasonId.ToString() == cbxseason.SelectedValue.ToString())
        //            {
        //                if (objBestBowler.DivisionId.ToString() == cbxdivision.SelectedValue.ToString())
        //                {

        //                    DataRow dr = dt1.NewRow();

        //                    dr["SerialNo"] = SerialNo1;
        //                    dr["PlayerName"] = objBestBowler.PlayerName;
        //                    dr["KSCAUID"] = objBestBowler.KSCAUID;
        //                    dr["TeamName"] = objBestBowler.TeamName;
        //                    dr["Matches"] = objBestBowler.Matches;
        //                    dr["Innings"] = objBestBowler.Innings;
        //                    dr["Overs"] = objBestBowler.Overs;
        //                    dr["Maidens"] = objBestBowler.Maidens;
        //                    dr["Runs"] = objBestBowler.Runs;
        //                    dr["Wickets"] = objBestBowler.Wickets;
        //                    dr["Economy"] = objBestBowler.Econ;
        //                    dt1.Rows.Add(dr);
        //                    SerialNo1++;

        //                }
        //            }
        //        }
        //        dgvBowling.ItemsSource = null;

        //        dgvBowling.DataContext = dt1;
        //        dgvBowling.ItemsSource = dt1.DefaultView;

        //        lbxFixtures.Clear();
        //        lstBestBatsman.Clear();
        //        lstsBestBowler.Clear();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public void LoadBowler()
        //{
        //    try
        //    {
        //        ObservableCollection<BestBatsman> lstBestBatsman = Database.GetEntityList<BestBatsman>(false, false, false, oleconn, "RecordStatus", "BestBatsmanId");
        //        ObservableCollection<BestBowler> lstsBestBowler = Database.GetEntityList<BestBowler>(false, false, false, oleconn, "RecordStatus", "BestBowlerId");
        //        ObservableCollection<BestBowler> lbxFixtures = Database.GetEntityList<BestBowler>(false, false, false, oleconn, "RecordStatus", "BestBowlerId");
        
        //        //batsmen
        //        var objvalues = from s in lstBestBatsman where s.DivisionId.ToString() == cbxdivision.SelectedValue.ToString() && s.SeasonId.ToString() == cbxseason.SelectedValue.ToString() select s;
        //        List<BestBatsman> lstBestBatsmanfilter = objvalues.OrderByDescending(p => p.RunsScored).ThenBy(p => p.StrikeRate).ThenBy(p => p.Matches).ToList<BestBatsman>();

        //        //bowler
        //        var objbestbowler = from s in lstsBestBowler where s.DivisionId.ToString() == cbxdivision.SelectedValue.ToString() && s.SeasonId.ToString() == cbxseason.SelectedValue.ToString() select s;
        //        List<BestBowler> lstBestBowlerfilter = objbestbowler.OrderByDescending(p => p.Wickets).ThenBy(p => p.Econ).ThenBy(p => p.Matches).ToList<BestBowler>();


        //        DataTable dt = new DataTable();
        //        DataTable dt1 = new DataTable();

        //        //batsmen
        //        dt.Columns.Add("SerialNo");
        //        dt.Columns.Add("PlayerName");
        //        dt.Columns.Add("KSCAUID");
        //        dt.Columns.Add("TeamName");
        //        dt.Columns.Add("Matches");
        //        dt.Columns.Add("Innings");
        //        dt.Columns.Add("NotOut");
        //        dt.Columns.Add("Runs");
        //        dt.Columns.Add("HighestScore");
        //        dt.Columns.Add("Strikerate");
        //        dt.Columns.Add("100s");
        //        dt.Columns.Add("50s");
        //        dt.Columns.Add("4s");
        //        dt.Columns.Add("6s");

        //        //bowler
        //        dt1.Columns.Add("SerialNo");
        //        dt1.Columns.Add("PlayerName");
        //        dt1.Columns.Add("KSCAUID");
        //        dt1.Columns.Add("TeamName");
        //        dt1.Columns.Add("Matches");
        //        dt1.Columns.Add("Innings");
        //        dt1.Columns.Add("Overs");
        //        dt1.Columns.Add("Maidens");
        //        dt1.Columns.Add("Runs");
        //        dt1.Columns.Add("Wickets");
        //        dt1.Columns.Add("Economy");


        //        int SerialNo = 1;
        //        foreach (BestBatsman objscore in lstBestBatsmanfilter)
        //        {
        //            if (objscore.SeasonId.ToString() == cbxseason.SelectedValue.ToString())
        //            {
        //                if (objscore.DivisionId.ToString() == cbxdivision.SelectedValue.ToString())
        //                {

        //                    DataRow dr = dt.NewRow();

        //                    dr["SerialNo"] = SerialNo;
        //                    dr["PlayerName"] = objscore.PlayerName;
        //                    dr["KSCAUID"] = objscore.KSCAUID;
        //                    dr["TeamName"] = objscore.TeamName;
        //                    dr["Matches"] = objscore.Matches;
        //                    dr["Innings"] = objscore.Innings;
        //                    dr["4s"] = objscore.Fours;
        //                    dr["6s"] = objscore.Sixes;
        //                    dr["NotOut"] = objscore.NotOut;
        //                    dr["Runs"] = objscore.RunsScored;
        //                    dr["HighestScore"] = objscore.HighestScore;
        //                    dr["Strikerate"] = objscore.StrikeRate;
        //                    dr["100s"] = objscore.Hundreds;
        //                    dr["50s"] = objscore.Fifties;
        //                    dt.Rows.Add(dr);
        //                    SerialNo++;

        //                }
        //            }
        //        }
        //        dgvBatting.ItemsSource = null;
        //        dgvBatting.DataContext = dt;
        //        dgvBatting.ItemsSource = dt.DefaultView;

        //        int SerialNo1 = 1;
        //        foreach (BestBowler objBestBowler in lstBestBowlerfilter)
        //        {
        //            if (objBestBowler.SeasonId.ToString() == cbxseason.SelectedValue.ToString())
        //            {
        //                if (objBestBowler.DivisionId.ToString() == cbxdivision.SelectedValue.ToString())
        //                {

        //                    DataRow dr = dt1.NewRow();

        //                    dr["SerialNo"] = SerialNo1;
        //                    dr["PlayerName"] = objBestBowler.PlayerName;
        //                    dr["KSCAUID"] = objBestBowler.KSCAUID;
        //                    dr["TeamName"] = objBestBowler.TeamName;
        //                    dr["Matches"] = objBestBowler.Matches;
        //                    dr["Innings"] = objBestBowler.Innings;
        //                    dr["Overs"] = objBestBowler.Overs;
        //                    dr["Maidens"] = objBestBowler.Maidens;
        //                    dr["Runs"] = objBestBowler.Runs;
        //                    dr["Wickets"] = objBestBowler.Wickets;
        //                    dr["Economy"] = objBestBowler.Econ;
        //                    dt1.Rows.Add(dr);
        //                    SerialNo1++;

        //                }
        //            }
        //        }
        //        dgvBowling.ItemsSource = null;

        //        dgvBowling.DataContext = dt1;
        //        dgvBowling.ItemsSource = dt1.DefaultView;

        //        lbxFixtures.Clear();
        //        lstBestBatsman.Clear();
        //        lstsBestBowler.Clear();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
     

        private void txtbowlersearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            //ObservableCollection<BestBowler> lbxFixtures = Database.GetEntityList<BestBowler>(false, false, false, oleconn, "RecordStatus", "BestBowlerId");
        
            //try
            //{
            //    dgvBowling.ItemsSource = null;
            //    dgvBowling.Items.Clear();

            //    if (txtbowlersearch.Text != "")
            //    {
            //        //selectedgridvalue.LoadDivisionId = cbxdivision.SelectedValue.ToString();
            //        List<BestBowler> lstselecteddiv = lbxFixtures.Where(p => p.DivisionId.ToString() == cbxdivision.SelectedValue.ToString()).OrderBy(p => p.Econ).ToList<BestBowler>();
            //        string str = txtbowlersearch.Text.ToString().ToUpper();

            //        foreach (var obj in lstselecteddiv)
            //        {
            //            if (obj.TeamName != null)
            //            {
            //                if (obj.TeamName.Contains(str) == true)
            //                {
            //                    dgvBowling.Items.Add(obj);
            //                }
            //            }
            //        }
            //    }

            //    else if (txtbowlersearch.Text == "")
            //    {

            //        LoadBowler();
               
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }

        private void txtbatsmensearch_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            //ObservableCollection<BestBatsman> lstBestBatsman = Database.GetEntityList<BestBatsman>(false, false, false, oleconn, "RecordStatus", "BestBatsmanId");

            //try
            //{
            //    dgvBatting.ItemsSource = null;
            //    dgvBatting.Items.Clear();

            //    if (txtbatsmensearch.Text != "")
            //    {
            //        //selectedgridvalue.LoadDivisionId = cbxdivision.SelectedValue.ToString();
            //        List<BestBatsman> lstselecteddiv = lstBestBatsman.Where(p => p.DivisionId.ToString() == cbxdivision.SelectedValue.ToString()).OrderBy(p => p.HighestScore).ToList<BestBatsman>();
            //        string str = txtbatsmensearch.Text.ToString().ToUpper();

            //        foreach (var obj in lstselecteddiv)
            //        {
            //            if (obj.TeamName != null)
            //            {
            //                if (obj.TeamName.Contains(str) == true)
            //                {
            //                    dgvBatting.Items.Add(obj);
            //                }
            //            }
            //        }
            //    }

            //    else if (txtbatsmensearch.Text == "")
            //    {

            //        Load();
                    
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }

        private void btnback_Click(object sender, RoutedEventArgs e)
        {
            dgvBatting.ItemsSource = null;
            dgvBowling.ItemsSource = null;

            Button btn = (Button)sender;
            btn.Command = NavigationCommands.GoToPage;
            btn.CommandParameter = "/Pages/Home.xaml";
        }
    }
}
