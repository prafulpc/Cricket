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
using System.Configuration;
using Cricket.View;
using System.Collections.ObjectModel;
using Cricket.BLL;


namespace Cricket.View
{
    /// <summary>
    /// Interaction logic for BasicPage1.xaml
    /// </summary>



    public partial class NewDivision : UserControl
    {
        Division objdivision;

        public NewDivision()
        {
            InitializeComponent();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            objdivision = Database.GetNewEntity<Division>();
            this.DataContext = objdivision;
            NewDivisionBLL objdivisionbll = new NewDivisionBLL(objdivision);

            try
            {
                //if (txt_name.Text == "")
                //{
                //    MessageBox.Show("Division Name cannot be empty");
                //}
                //else if (txt_type.Text == "")
                //{
                //    MessageBox.Show("Match Type Cannot be empty");

                //}
                //else if (txt_NoOfInnings.Text == "")
                //{
                //    MessageBox.Show("Number Of Innings Cannot be empty");
                //}
                //else if (txt_NoOfDays.Text == "")
                //{
                //    MessageBox.Show("Number of days Cannot be empty");
                //}
                //else
                //{

                objdivision.DivisionName = txt_name.Text;
                objdivision.Type = txt_type.Text;
                objdivision.NoOfInnings = txt_NoOfInnings.Text;
                objdivision.NoOfDays = txt_NoOfDays.Text;
                
                Database.SaveEntity<Division>(objdivision, Database.getConnection());
                MessageBox.Show("Divison With The Given Name" +txt_name.Text+ "Added SuccessFully");
               
                txt_name.Clear();
                txt_type.Clear();
                txt_NoOfInnings.Clear();
                txt_NoOfDays.Clear();
                txt_name.Focus();
            }


            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


            //        string strRetrieve = "";
            //        strRetrieve = "select * from Divisions";


            //        MatchInfo obj1 = new MatchInfo();
            //        //obj1.


            //        OleDbDataAdapter adpt = new OleDbDataAdapter(strRetrieve, Database.getConnection());
            //        DataTable dt = new DataTable();
            //        adpt.Fill(dt);
            //        //     conn.Close();

            //        foreach (DataRowView dr in dt.DefaultView)
            //        {

            //            txt_name.Text = dt.ToString();
            //            //txt_competation .Text = dt.ToString();
            //            txt_NoOfDays.Text = dt.ToString();
            //            txt_NoOfInnings.Text = dt.ToString();
            //            txt_type.Text = dt.ToString();


            //        }
            //    }
            //}
        }

        

        private void btnclear_Click(object sender, RoutedEventArgs e)
        {
            txt_name.Clear();
            txt_type.Clear();
            txt_NoOfInnings.Clear();
            txt_NoOfDays.Clear();
            txt_name.Focus();
        }
    }
}
    