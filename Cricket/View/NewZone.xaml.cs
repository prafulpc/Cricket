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
using System.Collections.ObjectModel;
using Cricket.BLL;


namespace Cricket.View
{
    /// <summary>
    /// Interaction logic for NewZone.xaml
    /// </summary>
    public partial class NewZone : UserControl
    {
        /// <summary>
        /// 
        /// </summary>
        Zone objZone;
        public NewZone()
        {
            InitializeComponent();
            
        }


        private void btnsave_Click(object sender, RoutedEventArgs e)
        {
            objZone = Database.GetNewEntity<Zone>();
            this.DataContext = objZone;
            NewZoneBLL objzonebll = new NewZoneBLL(objZone);

            try
            {
                //if (txt_ZoneName.Text=="")
                //{
                //    MessageBox.Show("Zone Name cannot be empty");
                //}
                //    else if(txtaccountno.Text=="")
                //{
                //    MessageBox.Show("Account Number cannot be empty");
                //}
                //else if(txtaccountname.Text=="")
                //{
                //    MessageBox.Show("Account Name cannot be empty");
                //}
                //    else if(txtaccounttype.Text=="")
                //{
                //    MessageBox.Show("Account Type cannot be empty");
                //}
                //else if(txtbankname.Text=="")
                //{
                //    MessageBox.Show("Bank Name cannot be empty");
                //}
                //    else if(txtbankbranch.Text=="")
                //{
                //    MessageBox.Show("Bank Branch cannot be empty");
                //}
                //    else if(txtifsc.Text=="")
                //{
                //    MessageBox.Show("IFSC code cannot be empty");
                //}
                //else
                //{


                objZone.ZoneName = txt_ZoneName.Text;
                objZone.AccountNumber = (txtaccountno.Text);
                objZone.AccountName = (txtaccountname.Text);
                objZone.AccountType = txtaccounttype.Text;
                objZone.BankBranch = txtbankbranch.Text;
                objZone.BankName = txtbankname.Text;
                objZone.IFSCCode = txtifsc.Text;

                Database.SaveEntity<Zone>(objZone, Database.getConnection());
                MessageBox.Show("Zone With The Name " +txt_ZoneName.Text+ "Added Succesfully");

                txt_ZoneName.Clear();
                txtaccountno.Clear();
                txtaccountname.Clear();
                txtaccounttype.Clear();
                txtbankbranch.Clear();
                txtbankname.Clear();
                txtifsc.Clear();
                txt_ZoneName.Focus();

                //}
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
                
         }

        private void btnclear_Click(object sender, RoutedEventArgs e)
        {
            txt_ZoneName.Clear();
            txtaccountno.Clear();
            txtaccountname.Clear();
            txtaccounttype.Clear();
            txtbankbranch.Clear();
            txtbankname.Clear();
            txtifsc.Clear();
            txt_ZoneName.Focus();
        }

        

        //public void validate_zonename()
        //{
        //    //string result = null;
        //    if (!string.IsNullOrWhiteSpace(txt_ZoneName.Text))
        //    {
        //        objZone.ZoneName = txt_ZoneName.Text;
        //    }
        //    else
        //    {
        //        MessageBox.Show("zone name is empty");
        //        txt_ZoneName.Focus();

        //        //validate_acc_no();
        //    }
        //}


        //private void txtaccountno_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.Key < Key.D0 || e.Key > Key.D9)
        //    {
        //        e.Handled = true;
        //    }
        //}

       //public void validate_acc_no()
       // {
       //    if(txtaccountno.Text=="")
       //    {
       //        MessageBox.Show("acc no is empty");
       //    }
       //    else
       //    {
       //        objZone.AccountNumber = (txtaccountno.Text);
       //        validate_acc_name();
       //    }
       // }

       // public void validate_acc_name()
       //{
       //     if(txtaccountname.Text=="")
       //     {
       //         MessageBox.Show("acc no is empty");
       //     }
       //     else
       //     {
       //         objZone.AccountName = (txtaccountname.Text);
       //         validate_acc_type();
       //     }
       //}
            
       // public void validate_acc_type()
       // {
       //     if (txtaccounttype.Text == "")
       //     {
       //         MessageBox.Show("acc type is empty");
       //     }
       //     else
       //     {
       //         objZone.AccountType= (txtaccounttype.Text);
       //         validate_bank_name();
       //     }
       // }

       // public void validate_bank_name()
       // {
       //     if (txtbankname.Text == "")
       //     {
       //         MessageBox.Show("bank name is empty");
       //     }
       //     else
       //     {
       //         objZone.BankName = (txtbankname.Text);
       //         validate_bank_branch();
       //     }
       // }

       // public void validate_bank_branch()
       // {
       //     if (txtbankbranch.Text == "")
       //     {
       //         MessageBox.Show("bank branch is empty");
       //     }
       //     else
       //     {
       //         objZone.BankBranch = (txtbankbranch.Text);
       //     }
       // }


            
            //   conn.Open();
            //string strRetrieve = "";
            //strRetrieve = "select * from Zones";


            //MatchInfo obj1 = new MatchInfo();
            //obj1.

           // ObservableCollection<Zone> obczone = Database.GetEntityList<Zone>(false, true, true, Database.getConnection(), "RecordStatus='Added'", "Zonename");

           // OleDbDataAdapter adpt = new OleDbDataAdapter(strRetrieve, Database.getConnection());
           //// Zone obj11 = Database.GetEntity<Zone>(Guid.Parse(txt_ZoneName.ToString()), false, false, false, Database.getConnection());
           // DataTable dt = new DataTable();
           // adpt.Fill(dt);
           // //     conn.Close();

           // foreach (DataRowView dr in dt.DefaultView)
           // {
           //     txt_ZoneName.Text = dt.ToString();
           // }
        }
    }

