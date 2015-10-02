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
using CricketSol.Base;
using CricketSol.DAL;
using CricketSol.Database;
using CricketSol.System;
using System.Data;
using System.Data.OleDb;
using System.Configuration;
using System.Collections.ObjectModel;
using Microsoft.Win32;
using System.IO;
using Cricket.BLL;



namespace Cricket.View
{
    /// <summary>
    /// Interaction logic for NewClub.xaml
    /// </summary>
    public partial class NewClub : UserControl
    {
        Club objclub;
        public NewClub()
        {
            InitializeComponent();
            //string strRetrieve1 = "";

            //strRetrieve1 = "select Zonename,Zoneid from zones";

            //OleDbDataAdapter adpt1 = new OleDbDataAdapter(strRetrieve1, Database.getConnection());

            //DataSet ds1 = new DataSet();
            //adpt1.Fill(ds1);
            //cbxzonename.ItemsSource = ds1.Tables[0].DefaultView;
            //cbxzonename.DisplayMemberPath = ds1.Tables[0].Columns["ZoneName"].ToString();
            //cbxzonename.SelectedValuePath = ds1.Tables[0].Columns["ZoneId"].ToString();
        }


        private void btnsave_Click(object sender, RoutedEventArgs e)
        {
            objclub = Database.GetNewEntity<Club>();
            this.DataContext = objclub;
            NewClubBLL objclubbll = new NewClubBLL(objclub);

            try
            {
                //if(txtclubname.Text=="") 
                //{
                //    MessageBox.Show("Club Name is Empty");
                //}

                //else if(cbxzonename.SelectedItem==null)
                //{
                //    MessageBox.Show("Select Zone Name");
                //}
                //else if (txtKSCAIM.Text=="")
                //{
                //    MessageBox.Show("KSCA IM is empty");
                //}
                //else if (radioia.IsChecked == false || radioim.IsChecked == false)
                //{
                //    MessageBox.Show("Select IM or IA");
                //}
                //else if (txtpostaladdr.Text == "")
                //{
                //    MessageBox.Show("Postal Address is empty");
                //}
                //else if (txtemailid.Text == "")
                //{
                //    MessageBox.Show("Email is empty");
                //}

                 
                //else 
                //{
                objclub.ClubName = txtclubname.Text;
                objclub.KSCAUID = txtKSCAIM.Text;
                objclub.Address = txtpostaladdr.Text;
                objclub.EmailId = txtemailid.Text;
                objclub.PresidentName = txtpresidentname.Text;
                objclub.VicePresidentName = txtvpresidentname.Text;
                objclub.SecretaryName = txtsecretaryname.Text;
                objclub.PresidentPhNo = (txtpresidentphno.Text);
                objclub.VicePresidentPhNo = (txtvicepresidentphno.Text);
                objclub.SecretaryPhNo = (txtsecretaryphno.Text);

                ObservableCollection<Zone> obczone = Database.GetEntityList<Zone>(false, true, true, Database.getConnection(), "RecordStatus='Added'", "ZoneName");
                Zone obj1 = Database.GetEntity<Zone>(Guid.Parse(cbxzonename.SelectedValue.ToString()), false, false, false, Database.getConnection());
                objclub.Zone = obj1;

                objclub.AccountName = txtaccountname.Text;
                objclub.AccountNumber = (txtaccountno.Text);
                objclub.AccountType = txtaccounttype.Text;
                objclub.BankBranch = txtbankbranch.Text;
                objclub.BankName = txtbankname.Text;
                objclub.IFSCCode = txtifsc.Text;



                if (objclub.ClubLogo == null)
                {
                    Database.SaveEntity<Club>(objclub, Database.getConnection());
                    MessageBox.Show("New Club Saved...");
                }

                else
                {

                    FileStream fs = new FileStream(txt_browse.Text, FileMode.OpenOrCreate, FileAccess.Read);
                    byte[] rawData = new byte[fs.Length];
                    fs.Read(rawData, 0, System.Convert.ToInt32(fs.Length));
                    fs.Close();

                    objclub.ClubLogo = rawData;

                    Database.SaveEntity<Club>(objclub, Database.getConnection());
                    MessageBox.Show("New Club Saved...");
                }

                //txt_browse.Clear();
                //txtaccountname.Clear();
                //txtaccountno.Clear();
                //txtaccounttype.Clear();
                //txtbankbranch.Clear();
                //txtbankname.Clear();
                //txtclubname.Clear();
                //txtemailid.Clear();
                //txtifsc.Clear();
                //txtKSCAIM.Clear();
                //txtpostaladdr.Clear();
                //txtpresidentname.Clear();
                //txtpresidentphno.Clear();
                //txtsecretaryname.Clear();
                //txtsecretaryphno.Clear();
                //txtvicepresidentphno.Clear();
                //txtvpresidentname.Clear();
                //txtclubname.Focus();
                img.Source = null;

            }



            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void cbxzonename_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ObservableCollection<Zone> objzone = Database.GetEntityList<Zone>(false, true, true, Database.getConnection(), "RecordStatus='Added'", "ZoneName");
            int count = 0;

          
            while (objzone[count].ZoneId.ToString() != cbxzonename.SelectedValue.ToString())
            {
                count++;
            }

            txtaccountname.Text = objzone[count].AccountName;
            txtaccountno.Text = ((objzone[count].AccountNumber.ToString()));
            txtaccounttype.Text = objzone[count].AccountType;
            txtbankname.Text = objzone[count].BankName;
            txtbankbranch.Text = objzone[count].BankBranch;
            txtifsc.Text = objzone[count].IFSCCode;
        }

        private void btnupload_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            Nullable<bool> result = ofd.ShowDialog();
            if (result == true)
            {
                BitmapImage bip = new BitmapImage();
                txt_browse.Text = ofd.FileName;
                img.Source = new BitmapImage(new Uri(ofd.FileName));
            }
        }

        private void btnclear_Click(object sender, RoutedEventArgs e)
        {
            txt_browse.Clear();
            txtaccountname.Clear();
            txtaccountno.Clear();
            txtaccounttype.Clear();
            txtbankbranch.Clear();
            txtbankname.Clear();
            txtclubname.Clear();
            txtemailid.Clear();
            txtifsc.Clear();
            txtKSCAIM.Clear();
            txtpostaladdr.Clear();
            txtpresidentname.Clear();
            txtpresidentphno.Clear();
            txtsecretaryname.Clear();
            txtsecretaryphno.Clear();
            txtvicepresidentphno.Clear();
            txtvpresidentname.Clear();
            txtclubname.Focus();
        }

        private void cbxzonename_DropDownOpened(object sender, EventArgs e)
        {
            string strRetrieve1 = "";

            strRetrieve1 = "select Zonename,Zoneid from zones";

            OleDbDataAdapter adpt1 = new OleDbDataAdapter(strRetrieve1, Database.getConnection());

            DataSet ds1 = new DataSet();
            adpt1.Fill(ds1);
            cbxzonename.ItemsSource = ds1.Tables[0].DefaultView;
            cbxzonename.DisplayMemberPath = ds1.Tables[0].Columns["ZoneName"].ToString();
            cbxzonename.SelectedValuePath = ds1.Tables[0].Columns["ZoneId"].ToString();
        }
    }
}

