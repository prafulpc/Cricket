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

using Cricket.View;

namespace Cricket.Pages
{

    public static class Batman1
    {
        public static string[] names1 = new string[15];
        public static int count1 = 0;
    }
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : UserControl
    {
        public List<string> players = new List<string>(15);

        public Home()
        {
            InitializeComponent();
            this.Height = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
            this.Width = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;
            cbxtest.Items.Add("q");
            cbxtest.Items.Add("w");
            cbxtest.Items.Add("e");
            cbxtest.Items.Add("r");


            for(int i=0; i<cbxtest.Items.Count;i++)
            {
                foreach (var item in cbxtest.Items)
                {
                     string xyz = Convert.ToString(item);
                    players.Insert(i, xyz);
                    i++;
                }
                
            }

            for (int i = 0; i < 15; i++)
            {
                 Batman1.names1[i] = "abc";
                 Batman1.count1++;
            }

        }

        

        private void test_Click(object sender, RoutedEventArgs e)
        {
            string abc = cbxtest.SelectedValue.ToString();
        }


        private void cbxtest_DropDownClosed(object sender, EventArgs e)
        {
            int a = cbxtest.SelectedIndex;
            string abc = cbxtest.SelectedItem.ToString();

            players.Remove(abc);
            players.Insert(0, abc);
            cbxtest.Items.Clear();
            for (int i = 0; i < players.Count;i++)
            {
                foreach (var item in players)
                {
                    cbxtest.Items.Add(item);
                    i++;
                }
            }
            cbxtest.SelectedIndex = 0;
                
        }
    }
}