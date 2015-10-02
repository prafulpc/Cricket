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
using System.Data;
using System.Collections.ObjectModel;

using CricketSol;
using CricketSol.Base;
using CricketSol.DAL;
using CricketSol.Database;
using CricketSol.System;
using Cricket.ReportData;
using System.Data.OleDb;
using Cricket.View;
using Microsoft.Reporting.WinForms;


namespace Cricket.View
{
    /// <summary>
    /// Interaction logic for PlayerId.xaml
    /// </summary>
    /// 
    public partial class PlayerId : UserControl
    {
        DataTable dtlItemDetails = new DataTable();
        ObservableCollection<Player> lstplayernames = Database.GetEntityList<Player>(false, true, true, Database.getConnection(), "RecordStatus='Added'", "FirstName");
        public PlayerId()
        {
            InitializeComponent();
        }

        private void reportViewer_Loaded(object sender, RoutedEventArgs e)
        {
            //ObservableCollection<Player> objplayer = Database.GetEntityList<Player>(false, true, true, Database.getReadConnection(), "RecordedStatus=Added", "PlayerId");
           // objplayer.Add()
        }

       

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int count = lstplayernames.Count;
            string[] player = new string[50];
            //int i;
            
            
            //for (i = 0; i <= count - 1; i++)
            //{
            //    player[i] = lstplayernames[i].FirstName;
            //    lstPlayers.Items.Add(player[i]);
              
            //}
            
            string sql="select firstname from players";
            //DataTable dtlItemDetails = new DataTable();
            OleDbDataAdapter adpt = new OleDbDataAdapter(sql,Database.getConnection());
            adpt.Fill(dtlItemDetails);
            dtlItemDetails.Columns.Add("IsItemSelected2", Type.GetType("System.Boolean"));
            foreach (DataRow R in dtlItemDetails.Rows)
            {
                R["IsItemSelected2"] = false;
            }
            DataView vw6 = dtlItemDetails.DefaultView;
            lstPlayers.DataContext = vw6;
            
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            PlayerReport objReportPlayer = new PlayerReport();

            List<PlayerReport> lstPlayerreport = new List<PlayerReport>();

            if (lstplayernames.Count > 0)
            {
               
                ReportGenerate frmReportGenerate = new ReportGenerate();


                int count = lstplayernames.Count;
                string[] player = new string[50];
                int i;
                for (i = 0; i <= count-1; i++)
                {
                    foreach (Player item in lstPlayers.SelectedItems)
                    {
                    
                        if (lstPlayers.SelectedItem != null)
                        {

                            objReportPlayer.PlayerName = item.FirstName;
                            objReportPlayer.Address = item.Address;
                            objReportPlayer.DOB = item.DateOfBirth;
                            objReportPlayer.EmailId = item.Emaild;
                            objReportPlayer.Gender = item.Gender;
                            objReportPlayer.KSCAUID = item.KSCAUID;
                            objReportPlayer.PhoneNumber =item.MobileNo;
                            objReportPlayer.PlayerPhoto = item.PlayerImage;

                            lstPlayerreport.Add(objReportPlayer);

                        }
                    }
                }

                ReportViewer rv = new ReportViewer();
                rv.LocalReport.ReportEmbeddedResource = @"Reports\PlayerId.rdlc";
                //this.reportViewer1.LocalReport.ReportPath("");
                ReportDataSource RDS = new ReportDataSource("PlayerId", lstPlayerreport);
                rv.LocalReport.DataSources.Add(RDS);
                rv.RefreshReport();
                frmReportGenerate.Show();
               
            }
        }

       

        private void cbxPlayer_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void cbxPlayer_Checked(object sender, RoutedEventArgs e)
        {
            //CheckBox chkValue = (CheckBox)sender;
            //if(chkValue)
        }
    }
}
