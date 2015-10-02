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
    public class NewPlayerBLL
    {
        private Player _player;

        public NewPlayerBLL(Player player)
        {
            _player = player;
            _player.PropertyChanging += _player_PropertyChanging;  
        }

        public void  _player_PropertyChanging(object sender, PropertyChangingEventArgs e)
        {
            //if (e.PropertyName == "FirstName")
            //{
            //    if (((PropertyChangingCancelEventArgs<String>)e).NewValue.All(char.IsLetter))
            //    {
            //        ((PropertyChangingCancelEventArgs<String>)e).Cancel = false;
            //    }
            //    else
            //    {
            //        ((PropertyChangingCancelEventArgs<String>)e).Cancel = true;

            //        throw new Exception("First Name should contain only Characters");
            //    }
            //}

            //else if (e.PropertyName == "LastName")
            //{
            //    if (((PropertyChangingCancelEventArgs<String>)e).NewValue.All(char.IsLetter))
            //    {
            //        ((PropertyChangingCancelEventArgs<String>)e).Cancel = false;
            //    }
            //    else
            //    {
            //        ((PropertyChangingCancelEventArgs<String>)e).Cancel = true;

            //        throw new Exception("Last Name should contain only Characters");
            //    }
            //}

             if (e.PropertyName == "MobileNo")
            {
                if (((PropertyChangingCancelEventArgs<String>)e).NewValue.All(char.IsDigit))
                {
                    ((PropertyChangingCancelEventArgs<String>)e).Cancel = false;
                }
                else
                {
                    ((PropertyChangingCancelEventArgs<String>)e).Cancel = true;

                    throw new Exception("Mobile No should contain only Numbers");
                }
            }
        }
    }
}
