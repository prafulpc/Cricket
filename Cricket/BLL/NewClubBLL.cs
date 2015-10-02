using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CricketSol.Database;
using System.ComponentModel;
using CricketSol.Base;

namespace Cricket.BLL
{
    public class NewClubBLL 
    {
        private Club _club ;

        public NewClubBLL(Club club)
        {

            _club = club;
            _club.PropertyChanging+=_team_PropertyChanging;    

        }

        public void _team_PropertyChanging(object sender, PropertyChangingEventArgs e)
        {
          

             if (e.PropertyName == "PresidentName")
            {
                if (((PropertyChangingCancelEventArgs<String>)e).NewValue.All(char.IsLetter))
                {
                    ((PropertyChangingCancelEventArgs<String>)e).Cancel = false;
                }
                else
                {
                    ((PropertyChangingCancelEventArgs<String>)e).Cancel = true;

                    throw new Exception("President Name should contain only Characters");
                }
            }

            else if (e.PropertyName == "VicePresidentName")
            {
                if (((PropertyChangingCancelEventArgs<String>)e).NewValue.All(char.IsLetter))
                {
                    ((PropertyChangingCancelEventArgs<String>)e).Cancel = false;
                }
                else
                {
                    ((PropertyChangingCancelEventArgs<String>)e).Cancel = true;

                    throw new Exception("VicePresident Name should contain only Characters");
                }
            }

            else if (e.PropertyName == "SecretaryName")
            {
                if (((PropertyChangingCancelEventArgs<String>)e).NewValue.All(char.IsLetter))
                {
                    ((PropertyChangingCancelEventArgs<String>)e).Cancel = false;
                }
                else
                {
                    ((PropertyChangingCancelEventArgs<String>)e).Cancel = true;

                    throw new Exception("Secretary Name should contain only Characters");
                }
            }    

            else if (e.PropertyName == "PresidentPhNo")
            {
                if(((PropertyChangingCancelEventArgs<String>)e).NewValue.All(char.IsDigit))
                {
                    ((PropertyChangingCancelEventArgs<String>)e).Cancel = false;
                }
                else
                {
                    ((PropertyChangingCancelEventArgs<String>)e).Cancel = true;

                    throw new Exception("President PhNo should have only numbers");
                }
            }

            else if (e.PropertyName == "VicePresidentPhNo")
            {
                if (((PropertyChangingCancelEventArgs<String>)e).NewValue.All(char.IsDigit))
                {
                    ((PropertyChangingCancelEventArgs<String>)e).Cancel = false;
                }
                else
                {
                    ((PropertyChangingCancelEventArgs<String>)e).Cancel = true;

                    throw new Exception("Vice President PhNo should have only numbers");
                }
            }

            else if (e.PropertyName == "SecretaryPhNo")
            {
                if (((PropertyChangingCancelEventArgs<String>)e).NewValue.All(char.IsDigit))
                {
                    ((PropertyChangingCancelEventArgs<String>)e).Cancel = false;
                }
                else
                {
                    ((PropertyChangingCancelEventArgs<String>)e).Cancel = true;

                    throw new Exception("Secretary PhNo should have only numbers");
                }
            }
        }
    }
}
