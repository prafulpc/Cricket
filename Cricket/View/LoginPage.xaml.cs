using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for TestPage.xaml
    /// </summary>
    public partial class TestPage : UserControl
    {
        public TestPage()
        {
            InitializeComponent();

        }


        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DateTime _expiredt = DateTime.Now.AddMonths(3);
                if (txtusername.Text == "admin" && txtpassword.Password == "admin123")
                {
                    if (DateTime.Now < _expiredt)
                    {
                        Button btn = (Button)sender;
                        btn.Command = NavigationCommands.GoToPage;
                        btn.CommandParameter = "/Pages/Home.xaml";
                    }
                    else
                    {
                        MessageBox.Show("License Expired");
                    }
                }
                else
                {
                    MessageBox.Show("Enter Correct UserName And Password");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
