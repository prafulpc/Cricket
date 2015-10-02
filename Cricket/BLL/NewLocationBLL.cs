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
    public class NewLocationBLL
    {
        private Location _location;

        public NewLocationBLL(Location location)
        {

            _location = location;
            _location.PropertyChanging += _location_PropertyChanging;

        }

        public void _location_PropertyChanging(object sender, PropertyChangingEventArgs e)
        {
            //if (e.PropertyName == "StadiumName")
            //{
            //    if (((PropertyChangingCancelEventArgs<String>)e).NewValue.All(char.IsLetter))
            //    {
            //        ((PropertyChangingCancelEventArgs<String>)e).Cancel = false;
            //    }
            //    else
            //    {
            //        ((PropertyChangingCancelEventArgs<String>)e).Cancel = true;

            //        throw new Exception("StadiumName should not have numbers");
            //    }
            //}

             if (e.PropertyName == "LocationName")
            {
                if (((PropertyChangingCancelEventArgs<String>)e).NewValue.All(char.IsLetter))
                {
                    ((PropertyChangingCancelEventArgs<String>)e).Cancel = false;
                }
                else
                {
                    ((PropertyChangingCancelEventArgs<String>)e).Cancel = true;

                    throw new Exception("LocationName should not have numbers");
                }
            }
        }
    }
}
