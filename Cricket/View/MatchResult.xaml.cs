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
 
namespace Cricket.View
{
    /// <summary>
    /// Interaction logic for MatchResult.xaml
    /// </summary>
    public partial class MatchResult : UserControl
    {
        public MatchResult()
        {
            InitializeComponent();
        }

        private void Label_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                // if ((comboboxvalue.value[0] == "0" && comboboxvalue.value[1] == "0") || (comboboxvalue.value[0] == "1" && comboboxvalue.value[1] == "1"))
                {
                    lblteam1.Content = team.name[0];
                    lblteam2.Content = team.name[1];

                }

                txtresult.Text = Match.Result;
                txtremarks.Text = Match.Result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }

        private void rbtnwon1_Click(object sender, RoutedEventArgs e)
        {
           
            rbtnlost2.IsChecked = true;
        }

        private void rbtnwon2_Checked(object sender, RoutedEventArgs e)
        {
           
            rbtnlost1.IsChecked = true;
        }
        private void rbtnlost2_Checked(object sender, RoutedEventArgs e)
        {
          
            rbtnwon1.IsChecked = true;

        }

        private void rbtnlost1_Checked(object sender, RoutedEventArgs e)
        {
           
            rbtnwon2.IsChecked = true;
        }

        private void rbtndraw1_Checked(object sender, RoutedEventArgs e)
        {
           
            rbtndraw2.IsChecked = true;

        }

        private void rbtndraw2_Checked(object sender, RoutedEventArgs e)
        {
           
            rbtndraw1.IsChecked = true;
        }

        private void btnok_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(txtpoints1.Text == "")
                {
                    MessageBox.Show("Enter Team 1 Points");
                }

                if(txtpoints2.Text == "")
                {
                    MessageBox.Show("Enter Team 2 Points");
                }
                if (txtremarks.Text == "")
                {
                    MessageBox.Show("Enter Remarks Points");
                }

                else
                {

                    if (Convert.ToBoolean(rbtnwon1.IsChecked) && Convert.ToBoolean(rbtnlost2.IsChecked))
                    {
                        TeamA.Result = "WON";
                        TeamB.Result = "LOST";

                    }

                    else if (Convert.ToBoolean(rbtnwon2.IsChecked) && Convert.ToBoolean(rbtnlost1.IsChecked))
                    {
                        TeamA.Result = "LOST";
                        TeamB.Result = "WON";
                    }
                    else
                    {
                        TeamA.Result = "DRAW";
                        TeamB.Result = "DRAW";
                    }
                    TeamA.Points = txtpoints1.Text;
                    TeamB.Points = txtpoints2.Text;
                    Remark.Remarks = txtremarks.Text;

                    if (division.num == 1)
                    {
                        Button btn = (Button)sender;
                        btn.Command = NavigationCommands.GoToPage;
                        btn.CommandParameter = "/View/FirstDivScoreCard.xaml";

                    }

                    else
                    {
                        Button btn = (Button)sender;
                        btn.Command = NavigationCommands.GoToPage;
                        btn.CommandParameter = "/View/ScoreCard.xaml";

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            txtpoints1.Text = string.Empty;
            txtpoints2.Text = string.Empty;
            txtresult.Text = string.Empty;
            txtremarks.Text = string.Empty;
            rbtnwon1.IsChecked = false;
            rbtnwon2.IsChecked = false;
            rbtnlost1.IsChecked = false;
            rbtnlost2.IsChecked = false;
            rbtndraw1.IsChecked = false;
            rbtndraw2.IsChecked = false;
        }
    }
    public static class TeamA
    {
        public static string Result;
        public static string Points;
    }

    public static class TeamB
    {
        public static string Result;
        public static string Points;
    }

    public static class Remark
    {
        public static string Remarks;
        
    }
}
