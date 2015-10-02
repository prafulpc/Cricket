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
    public class NewTeamBLL 
    {
        private Team _team ; 

        public NewTeamBLL(Team team)
        {

            _team = team;
            _team.PropertyChanging+=_team_PropertyChanging;    

        }

        public void _team_PropertyChanging(object sender, PropertyChangingEventArgs e)
        {
           

            

             if (e.PropertyName == "ShortName")
            {
                if(((PropertyChangingCancelEventArgs<String>)e).NewValue.All(char.IsLetter))
                {
                    ((PropertyChangingCancelEventArgs<String>)e).Cancel = false;
                }
                else
                {
                    ((PropertyChangingCancelEventArgs<String>)e).Cancel = true;

                    throw new Exception("Short Name should not have numbers");
                }
            }
        }

    }
     
}
