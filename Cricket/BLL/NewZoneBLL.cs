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
    public class NewZoneBLL
    {
        private Zone _zone;

        public NewZoneBLL(Zone zone)
        {
            _zone = zone;
            _zone.PropertyChanging +=_zone_PropertyChanging;

        }

        public void _zone_PropertyChanging(object sender, PropertyChangingEventArgs e)
        {
            if (e.PropertyName == "AccountNumber")
            {
                if (((PropertyChangingCancelEventArgs<String>)e).NewValue.All(char.IsDigit))
                {
                    ((PropertyChangingCancelEventArgs<String>)e).Cancel = false;
                }
                else
                {
                    ((PropertyChangingCancelEventArgs<String>)e).Cancel = true;

                    throw new Exception("Account Number should have only numbers");
                }
            }

            else if (e.PropertyName == "ZoneName")
            {
                if (((PropertyChangingCancelEventArgs<String>)e).NewValue.All(char.IsLetter))
                {
                    ((PropertyChangingCancelEventArgs<String>)e).Cancel = false;
                }
                else
                {
                    ((PropertyChangingCancelEventArgs<String>)e).Cancel = true;

                    throw new Exception("Zone Name should contain only Characters");
                }
            }

            else if (e.PropertyName == "AccountName")
            {
                if (((PropertyChangingCancelEventArgs<String>)e).NewValue.All(char.IsLetter))
                {
                    ((PropertyChangingCancelEventArgs<String>)e).Cancel = false;
                }
                else
                {
                    ((PropertyChangingCancelEventArgs<String>)e).Cancel = true;

                    throw new Exception("Account Name should contain only Characters");
                }
            }

            else if (e.PropertyName == "AccountType")
            {
                if (((PropertyChangingCancelEventArgs<String>)e).NewValue.All(char.IsLetter))
                {
                    ((PropertyChangingCancelEventArgs<String>)e).Cancel = false;
                }
                else
                {
                    ((PropertyChangingCancelEventArgs<String>)e).Cancel = true;

                    throw new Exception("Account Type should contain only Characters");
                }
            }

            else if (e.PropertyName == "BankName")
            {
                if (((PropertyChangingCancelEventArgs<String>)e).NewValue.All(char.IsLetter))
                {
                    ((PropertyChangingCancelEventArgs<String>)e).Cancel = false;
                }
                else
                {
                    ((PropertyChangingCancelEventArgs<String>)e).Cancel = true;

                    throw new Exception("Bank Name should contain only Characters");
                }
            }

            else if (e.PropertyName == "BankBranch")
            {
                if (((PropertyChangingCancelEventArgs<String>)e).NewValue.All(char.IsLetter))
                {
                    ((PropertyChangingCancelEventArgs<String>)e).Cancel = false;
                }
                else
                {
                    ((PropertyChangingCancelEventArgs<String>)e).Cancel = true;

                    throw new Exception("Bank Branch should contain only Characters");
                }
            }
        }
    }
}
