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
using Cricket.Pages;

namespace Cricket.View
{
    /// <summary>
    /// Interaction logic for EditWindow.xaml
    /// </summary>
    public partial class EditWindow : UserControl
    {
        public EditWindow()
        {
            InitializeComponent();

            //for (int j = 0; j < Batman1.count1;j++ )
            //{
            //    testing.Items[j] = "xyz";
            //}

                for (int i = 0; i < Batman1.count1; i++)
                {
                    foreach (var item in Batman1.names1)
                    {
                        testing.Items.Add(item);
                        i++;
                    }
                }
        }
    }


}
