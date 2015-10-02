using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CricketSol.Database;
using System.ComponentModel;
using CricketSol.Base;
using Cricket.View;

namespace Cricket.BLL
{
    public class NewDivisionBLL
    {
        private Division _division;
       
        public NewDivisionBLL(Division division)
        {
            _division = division;
            _division.PropertyChanging +=_division_PropertyChanging;

        }

        public void _division_PropertyChanging(object sender, PropertyChangingEventArgs e)
        {   
            if (e.PropertyName == "NoOfInnings")
            {
                if (((PropertyChangingCancelEventArgs<String>)e).NewValue.All(char.IsDigit))
                {
                    ((PropertyChangingCancelEventArgs<String>)e).Cancel = false;
                }
                else
                {
                    ((PropertyChangingCancelEventArgs<String>)e).Cancel = true;

                    throw new Exception("Number Of Innings should have only numbers");
                }
            }

            else if (e.PropertyName == "NoOfDays")
            {
                if (((PropertyChangingCancelEventArgs<String>)e).NewValue.All(char.IsDigit))
                {
                    ((PropertyChangingCancelEventArgs<String>)e).Cancel = false;
                }
                else
                {
                    ((PropertyChangingCancelEventArgs<String>)e).Cancel = true;

                    throw new Exception("Number Of Days should have only numbers");
                }
            }
        }
    }
}
