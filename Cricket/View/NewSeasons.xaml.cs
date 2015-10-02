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
using System.Collections.ObjectModel;

namespace Cricket.View
{
    /// <summary>
    /// Interaction logic for Seasons.xaml
    /// </summary>
    public partial class NewSeasons : UserControl
    {
       Season objseasons;
        public NewSeasons()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            objseasons = Database.GetNewEntity<Season>();
            this.DataContext = objseasons;

            try
            {
                objseasons.SeasonType = txt_seasons.Text;
                
                Database.SaveEntity<Season>(objseasons, Database.getConnection());
                MessageBox.Show("Seasons Saved");
                txt_seasons.Focus();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
    }
}
