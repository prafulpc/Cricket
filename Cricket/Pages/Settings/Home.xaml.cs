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
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : UserControl
    {
        public Home()
        {
            InitializeComponent();
            this.Height = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
            this.Width = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;

        }
        double ball = 0;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string startdate = "06-May-15 12:00:00 AM";

            string s_time = ((startdate.ToString()).Substring(startdate.ToString().IndexOf(" ")-9, 9));  




            int a1 = 0;
            int b1 = 0;
            double forovers = 3.2;
            double txtovers1 = 4.3;

            a1 = Convert.ToInt16(Math.Truncate(forovers));
            string str1 = Convert.ToString(Math.Round(forovers - a1, 1));
            if (str1 != "0")
            {
                b1 = Convert.ToInt16(str1.Split('.')[1].Trim());
            }
            else
            {
                b1 = 0;
            }

            int a2 = 0;
            int b2 = 0;

            a2 = Convert.ToInt16(Math.Truncate(txtovers1));
            string str2 = Convert.ToString(Math.Round(txtovers1 - a2, 1));
            if (str2 != "0")
            {
                b2 = Convert.ToInt16(str2.Split('.')[1].Trim());
            }
            else
            {
                b2 = 0;
            }

            if ((b1 + b2) >= 6)
            {
                int asd = a1 + a2 + 1;
                int qwe = (b1 + b2) % 6;
                decimal var = Convert.ToDecimal(asd + "." + qwe);
                //return forovers;
            }
            else
            {
                int asd = a1 + a2;
                int qwe = (b1 + b2) % 6;
                decimal var = Convert.ToDecimal(asd + "." + qwe);
                //return forovers;
            }


            ///////////
            double sa = 6.2;
            decimal sw = Convert.ToDecimal(sa);
            
            
            //double  asd = 6.3;
            //int a1 = 0;
            //int b1 = 0;
            //int ol1 = 0;
            //int or1 = 0;
            //double asf = 2.3;

            //a1 = Convert.ToInt16(Math.Truncate(asf)); //a=2
            //string str1 = Convert.ToString(Math.Round(asf - a1, 1));
            //if (str1 != "0")
            //{
            //    b1 = Convert.ToInt16(str1.Split('.')[1].Trim()); //b=3
            //}
            //else
            //{
            //    b1 = 0;
            //}
            //int ballss = a1 * 6 + b1;


            //if ((or1 + b1) >= 6)
            //{
            //    ol1++;
            //    ol1 = ol1 + a1;
            //    or1 = (or1 + b1) % 6;
            //}
            //else
            //{
            //    ol1 = ol1 + a1;
            //    or1 = or1 + b1;
            //}


            decimal a = 100;
            decimal b = 300;
            decimal c = 30;

            int balls = Convert.ToInt16((a / b) * c);

            string id = "PREETHAM S (W)";
            if(id.Contains("("))
            {
                string abc = id.Substring(0, id.IndexOf("(") - 1);
            }
            else
            {
                string abc = id;
            }
            
        }
    }
}