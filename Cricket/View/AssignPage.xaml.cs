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
using System.Data.OleDb;
using System.Data;

using Cricket.View;
using Cricket.ReportData;
using Microsoft.Reporting.WinForms;

using System.Configuration;
using System.Collections.ObjectModel;

namespace Cricket.View
{
    /// <summary>
    /// Interaction logic for AssignPage.xaml
    /// </summary>
    public partial class AssignPage : UserControl
    {
        public List<string> list1 = new List<string>(15);
        OleDbConnection oleconn = Database.getConnection();
        ObservableCollection<DivisionLoad> lstteams = Database.GetEntityList<DivisionLoad>(false, false, false, Database.getReadConnection(), "RecordStatus", "DivisionLoadId");

        public AssignPage()
        {
            InitializeComponent();

            txtTeam1.Text = tempvalues.teamA;
            txtTeam2.Text = tempvalues.teamB;
            lblseason.Content = TempValues.SeasonName;
            // txtmatchid.Text = selectedgridvalue.matchid;
        }



        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (startDate.SelectedDate == null)
                {
                    MessageBox.Show("Select Start Date");
                }
                if (EndDate.SelectedDate == null)
                {
                    MessageBox.Show("Select End Date");
                }

                if (cbxumpire1.SelectedItem == null)
                {
                    MessageBox.Show("Select Umpire1");
                }

                if (cbxumpire2.SelectedItem == null)
                {
                    MessageBox.Show("Select Umpire2");
                }
                if (cbxscorer.SelectedItem == null)
                {
                    MessageBox.Show("Select Scorer");
                }
                if (cbxVenue.SelectedItem == null)
                {
                    MessageBox.Show("Select Venue");
                }
                else
                {
                    InduvidualTeamExport frm = new InduvidualTeamExport();
                    List<InduvidualExport> lstInduvidualExport = new List<InduvidualExport>();

                    InduvidualExport obj = new InduvidualExport();

                    obj.Season = TempValues.SeasonName;
                    obj.Division = selectedgridvalue.LoadDivision;
                    obj.DateFrom = startDate.SelectedDate.Value;
                    obj.DateTo = EndDate.SelectedDate.Value;
                    obj.Date = AssignDate.SelectedDate.Value;
                    obj.Venue = cbxVenue.Text;
                    obj.Umpire1 = cbxumpire1.Text;
                    obj.Umpire2 = cbxumpire2.Text;
                    obj.Scorer = cbxscorer.Text;
                    obj.Team1 = txtTeam1.Text;
                    obj.Team2 = txtTeam2.Text;
                    obj.MatchId = selectedgridvalue.matchid;

                    lstInduvidualExport.Add(obj);

                    ReportViewer rv = frm.GetReportViewer();
                    rv.Reset();
                    rv.LocalReport.ReportEmbeddedResource = @"Cricket.Reports.InduvidualExport.rdlc";
                    ReportDataSource RDS = new ReportDataSource("InduvidualDataSet", lstInduvidualExport);
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

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

            if (selectedgridvalue.group == "Semi-Final")
            {
                cbxTeam1.Visibility = Visibility.Visible;
                cbxTeam2.Visibility = Visibility.Visible;
                finalLabel1.Visibility = Visibility.Visible;
                FinalLabel2.Visibility = Visibility.Visible;
            }

            else if (selectedgridvalue.group == "Final")
            {
                cbxTeam1.Visibility = Visibility.Visible;
                cbxTeam2.Visibility = Visibility.Visible;
                finalLabel1.Visibility = Visibility.Visible;
                FinalLabel2.Visibility = Visibility.Visible;


            }

            else
            {
                cbxTeam1.Visibility = Visibility.Hidden;
                cbxTeam2.Visibility = Visibility.Hidden;
                finalLabel1.Visibility = Visibility.Hidden;
                FinalLabel2.Visibility = Visibility.Hidden;
            }


            cbxumpire1.ItemsSource = null;
            cbxumpire2.ItemsSource = null;
            cbxscorer.ItemsSource = null;
            cbxVenue.ItemsSource = null;
            lblseason.Content = null;
            cbxumpire1.Items.Clear();
            cbxumpire2.Items.Clear();
            cbxscorer.Items.Clear();
            cbxVenue.Items.Clear();
            txtTeam1.Text = string.Empty;
            txtTeam2.Text = string.Empty;


            txtTeam1.Text = tempvalues.teamA;
            txtTeam2.Text = tempvalues.teamB;
            lblSeason.Content = TempValues.SeasonName;
            lblDivision.Content = selectedgridvalue.LoadDivision;
            ObservableCollection<Fixture> lstfixture = Database.GetEntityList<Fixture>(false, false, false, oleconn, "RecordStatus", "FixtureId");

            var objfixtureid = from s in lstfixture where s.MatchId == selectedgridvalue.matchid select s;
            List<Fixture> lstfilter = objfixtureid.OrderBy(p => p.MatchId).ToList<Fixture>();

            if (lstfixture.Count > 0)
            {
                foreach (var objfixture in lstfilter)
                {


                    cbxumpire1.Items.Add(objfixture.Umpire);
                    cbxumpire1.SelectedIndex = 0;
                    cbxumpire2.Items.Add(objfixture.Umpiree);
                    cbxumpire2.SelectedIndex = 0;
                    cbxscorer.Items.Add(objfixture.Scorer);
                    cbxscorer.SelectedIndex = 0;
                    cbxVenue.Items.Add(objfixture.Venue);
                    cbxVenue.SelectedIndex = 0;

                    startDate.Text = objfixture.FromDate.ToString();
                    EndDate.Text = objfixture.ToDate.ToString();
                    AssignDate.Text = System.DateTime.Now.ToString();

                   
                }

                startDate.IsEnabled = false;
                EndDate.IsEnabled = false;
                AssignDate.IsEnabled = false;
                cbxumpire1.IsEnabled = false;
                cbxumpire2.IsEnabled = false;
                cbxscorer.IsEnabled = false;
                cbxVenue.IsEnabled = false;
                startDate.IsEnabled = false;
                EndDate.IsEnabled = false;
                AssignDate.IsEnabled = false;
            }

        }

        private void cbxVenue_DropDownOpened(object sender, EventArgs e)
        {
            //venue


            cbxVenue.ItemsSource = null;


            cbxVenue.Items.Clear();

            string strvenue = "";
            strvenue = "select stadiumname,locationid from locations";

            OleDbDataAdapter adptvenue = new OleDbDataAdapter(strvenue, oleconn);

            DataSet dsvenue = new DataSet();
            adptvenue.Fill(dsvenue);
            cbxVenue.ItemsSource = dsvenue.Tables[0].DefaultView;
            cbxVenue.DisplayMemberPath = dsvenue.Tables[0].Columns["StadiumName"].ToString();
            cbxVenue.SelectedValuePath = dsvenue.Tables[0].Columns["LocationId"].ToString();
        }

        private void cbxumpire1_DropDownOpened(object sender, EventArgs e)
        {
            ObservableCollection<Official> officialname = Database.GetEntityList<Official>(false, false, false, oleconn, "RecordStatus", "OfficialName");

            //u-2 not selected && cbxscorer not selected
            if (cbxumpire2.SelectedIndex == -1 && cbxscorer.SelectedIndex == -1)
            {
                cbxumpire1.Items.Clear();
                cbxumpire2.Items.Clear();
                cbxscorer.Items.Clear();
                list1.Clear();
                cbxumpire1.SelectedIndex = -1;
                cbxumpire2.SelectedIndex = -1;
                cbxscorer.SelectedIndex = -1;


                foreach (var ofcname in officialname)
                {
                    list1.Add(ofcname.OfficialName);
                }


                for (int i = 0; i <= (list1.Count - 1); i++)
                {
                    cbxumpire1.Items.Add(list1[i]);
                }
            }

                //u-2 selected && cbxscorer not selected
            else if (cbxumpire2.SelectedIndex != -1 && cbxscorer.SelectedIndex == -1)
            {
                cbxumpire1.Items.Clear();
                cbxscorer.Items.Clear();
                list1.Clear();
                cbxumpire1.SelectedIndex = -1;
                cbxscorer.SelectedIndex = -1;

                string abc = cbxumpire2.SelectedItem.ToString();

                foreach (var ofcname in officialname)
                {
                    list1.Add(ofcname.OfficialName);
                }

                for (int i = 0; i <= (list1.Count - 1); i++)
                {
                    if (list1[i] != abc)
                    {
                        cbxumpire1.Items.Add(list1[i]);
                    }
                }
            }
            // cbxscorer selected && u-2 not selected
            else if (cbxscorer.SelectedIndex != -1 && cbxumpire2.SelectedIndex == -1)
            {
                cbxumpire1.Items.Clear();
                cbxumpire2.Items.Clear();
                list1.Clear();
                cbxumpire1.SelectedIndex = -1;
                cbxumpire2.SelectedIndex = -1;

                string abc = cbxscorer.SelectedItem.ToString();

                foreach (var ofcname in officialname)
                {
                    list1.Add(ofcname.OfficialName);
                }

                for (int i = 0; i <= (list1.Count - 1); i++)
                {
                    if (list1[i] != abc)
                    {
                        cbxumpire1.Items.Add(list1[i]);
                    }
                }
            }
            //u-2 selected && cbxscorer selected
            else if (cbxumpire2.SelectedIndex != -1 && cbxscorer.SelectedIndex != -1)
            {
                cbxumpire1.Items.Clear();
                list1.Clear();
                cbxumpire1.SelectedIndex = -1;
                string abc = cbxumpire2.SelectedItem.ToString();
                string def = cbxscorer.SelectedItem.ToString();

                foreach (var ofcname in officialname)
                {
                    list1.Add(ofcname.OfficialName);
                }

                for (int i = 0; i <= (list1.Count - 1); i++)
                {
                    if (list1[i] != abc && list1[i] != def)
                    {
                        cbxumpire1.Items.Add(list1[i]);
                    }
                }
            }

        }

        private void cbxumpire2_DropDownOpened(object sender, EventArgs e)
        {
            ObservableCollection<Official> officialname = Database.GetEntityList<Official>(false, false, false, oleconn, "RecordStatus", "OfficialName");

            //u-1 not selected && cbxscorer not selected
            if (cbxumpire1.SelectedIndex == -1 && cbxscorer.SelectedIndex == -1)
            {
                cbxumpire1.Items.Clear();
                cbxumpire2.Items.Clear();
                cbxscorer.Items.Clear();
                list1.Clear();
                cbxumpire1.SelectedIndex = -1;
                cbxumpire2.SelectedIndex = -1;
                cbxscorer.SelectedIndex = -1;


                foreach (var ofcname in officialname)
                {
                    list1.Add(ofcname.OfficialName);
                }


                for (int i = 0; i <= (list1.Count - 1); i++)
                {
                    cbxumpire2.Items.Add(list1[i]);
                }
            }

                //u-1 selected && cbxscorer not selected
            else if (cbxumpire1.SelectedIndex != -1 && cbxscorer.SelectedIndex == -1)
            {
                cbxumpire2.Items.Clear();
                cbxscorer.Items.Clear();
                list1.Clear();
                cbxumpire2.SelectedIndex = -1;
                cbxscorer.SelectedIndex = -1;

                string abc = cbxumpire1.SelectedItem.ToString();

                foreach (var ofcname in officialname)
                {
                    list1.Add(ofcname.OfficialName);
                }

                for (int i = 0; i <= (list1.Count - 1); i++)
                {
                    if (list1[i] != abc)
                    {
                        cbxumpire2.Items.Add(list1[i]);
                    }
                }
            }
            // cbxscorer selected && u-1 not selected
            else if (cbxscorer.SelectedIndex != -1 && cbxumpire1.SelectedIndex == -1)
            {
                cbxumpire1.Items.Clear();
                cbxumpire2.Items.Clear();
                list1.Clear();
                cbxumpire1.SelectedIndex = -1;
                cbxumpire2.SelectedIndex = -1;

                string abc = cbxscorer.SelectedItem.ToString();

                foreach (var ofcname in officialname)
                {
                    list1.Add(ofcname.OfficialName);
                }

                for (int i = 0; i <= (list1.Count - 1); i++)
                {
                    if (list1[i] != abc)
                    {
                        cbxumpire2.Items.Add(list1[i]);
                    }
                }
            }
            //u-1 selected && cbxscorer selected
            else if (cbxumpire1.SelectedIndex != -1 && cbxscorer.SelectedIndex != -1)
            {
                cbxumpire2.Items.Clear();
                list1.Clear();
                cbxumpire2.SelectedIndex = -1;
                string abc = cbxumpire1.SelectedItem.ToString();
                string def = cbxscorer.SelectedItem.ToString();

                foreach (var ofcname in officialname)
                {
                    list1.Add(ofcname.OfficialName);
                }

                for (int i = 0; i <= (list1.Count - 1); i++)
                {
                    if (list1[i] != abc && list1[i] != def)
                    {
                        cbxumpire2.Items.Add(list1[i]);
                    }
                }
            }

        }

        private void cbxscorer_DropDownOpened(object sender, EventArgs e)
        {
            ObservableCollection<Official> officialname = Database.GetEntityList<Official>(false, false, false, oleconn, "RecordStatus", "OfficialName");

            //u-2 not selected && u-1 not selected
            if (cbxumpire2.SelectedIndex == -1 && cbxumpire1.SelectedIndex == -1)
            {
                cbxumpire1.Items.Clear();
                cbxumpire2.Items.Clear();
                cbxscorer.Items.Clear();
                list1.Clear();
                cbxumpire1.SelectedIndex = -1;
                cbxumpire2.SelectedIndex = -1;
                cbxscorer.SelectedIndex = -1;


                foreach (var ofcname in officialname)
                {
                    list1.Add(ofcname.OfficialName);
                }


                for (int i = 0; i <= (list1.Count - 1); i++)
                {
                    cbxscorer.Items.Add(list1[i]);
                }
            }

                //u-2 selected  && u-1 not selected
            else if (cbxumpire2.SelectedIndex != -1 && cbxumpire1.SelectedIndex == -1)
            {
                cbxumpire1.Items.Clear();
                cbxscorer.Items.Clear();
                list1.Clear();
                cbxumpire1.SelectedIndex = -1;
                cbxscorer.SelectedIndex = -1;

                string abc = cbxumpire2.SelectedItem.ToString();

                foreach (var ofcname in officialname)
                {
                    list1.Add(ofcname.OfficialName);
                }

                for (int i = 0; i <= (list1.Count - 1); i++)
                {
                    if (list1[i] != abc)
                    {
                        cbxscorer.Items.Add(list1[i]);
                    }
                }
            }
            // u-1 selected && u-2 not selected 
            else if (cbxumpire1.SelectedIndex != -1 && cbxumpire2.SelectedIndex == -1)
            {
                cbxscorer.Items.Clear();
                cbxumpire2.Items.Clear();
                list1.Clear();
                cbxscorer.SelectedIndex = -1;
                cbxumpire2.SelectedIndex = -1;

                string abc = cbxumpire1.SelectedItem.ToString();

                foreach (var ofcname in officialname)
                {
                    list1.Add(ofcname.OfficialName);
                }

                for (int i = 0; i <= (list1.Count - 1); i++)
                {
                    if (list1[i] != abc)
                    {
                        cbxscorer.Items.Add(list1[i]);
                    }
                }
            }
            //u-2 selected && u-1 selected
            else if (cbxumpire2.SelectedIndex != -1 && cbxumpire1.SelectedIndex != -1)
            {
                cbxscorer.Items.Clear();
                list1.Clear();
                cbxscorer.SelectedIndex = -1;
                string abc = cbxumpire2.SelectedItem.ToString();
                string def = cbxumpire1.SelectedItem.ToString();

                foreach (var ofcname in officialname)
                {
                    list1.Add(ofcname.OfficialName);
                }

                for (int i = 0; i <= (list1.Count - 1); i++)
                {
                    if (list1[i] != abc && list1[i] != def)
                    {
                        cbxscorer.Items.Add(list1[i]);
                    }
                }
            }

        }

        private void cbxTeam1_DropDownOpened(object sender, EventArgs e)
        {
            cbxTeam1.Items.Clear();
         
            string team2selected = cbxTeam2.Text;
            try
            {
                cbxTeam1.Items.Clear();
                foreach (var team1 in lstteams)
                {
                    if (team1.SeasonId.ToString() == selectedgridvalue._seasonid)
                    {
                        if (selectedgridvalue.LoadDivision == "1st Division")
                        {

                            if (!(team1.DivisionOne == team2selected))
                            {
                                if (team1.DivisionOne != null)
                                {
                                    cbxTeam1.Items.Add(team1.DivisionOne);
                                }
                            }
                        }
                        else if (selectedgridvalue.LoadDivision == "2nd Division")
                        {
                            if (!(team1.DivisionTwo == team2selected))
                            {
                                if (team1.DivisionTwo != null)
                                {
                                    cbxTeam1.Items.Add(team1.DivisionTwo);
                                }
                            }
                        }
                        else if (selectedgridvalue.LoadDivision == "3rd Division")
                        {
                            if (!(team1.DivisionThree == team2selected))
                            {
                                if (team1.DivisionThree != null)
                                {
                                    cbxTeam1.Items.Add(team1.DivisionThree);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void cbxTeam2_DropDownOpened(object sender, EventArgs e)
        {
            cbxTeam2.Items.Clear();
           
            string team1selected = cbxTeam1.Text;

            try
            {
                cbxTeam2.Items.Clear();
                foreach (var team2 in lstteams)
                {
                    if (team2.SeasonId.ToString() == selectedgridvalue._seasonid)
                    {
                        if (selectedgridvalue.LoadDivision == "1st Division")
                        {
                            if (!(team2.DivisionOne == team1selected))
                            {
                                if (team2.DivisionOne != null)
                                {
                                    cbxTeam2.Items.Add(team2.DivisionOne);
                                }
                            }
                        }

                        else if (selectedgridvalue.LoadDivision == "2nd Division")
                        {
                            if (!(team2.DivisionTwo == team1selected))
                            {
                                if (team2.DivisionTwo != null)
                                {
                                    cbxTeam2.Items.Add(team2.DivisionTwo);
                                }
                            }
                        }

                        else if (selectedgridvalue.LoadDivision == "3rd Division")
                        {
                            if (!(team2.DivisionThree == team1selected))
                            {
                                if (team2.DivisionThree != null)
                                {
                                    cbxTeam2.Items.Add(team2.DivisionThree);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void cbxTeam1_DropDownClosed(object sender, EventArgs e)
        {
            txtTeam1.Text = cbxTeam1.Text;
           
        }

        private void cbxTeam2_DropDownClosed(object sender, EventArgs e)
        {
            txtTeam2.Text = cbxTeam2.Text;
        }
    }
}