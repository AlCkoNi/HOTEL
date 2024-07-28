using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace HOTEL.authentication
{
    public class auth
    {
        private string login = "";
        private string passw = "";
        public void purpose(string name, string pass, string role, bool b)//sign-in
        {

        }
        public void purpose2(string name, string pass,bool b)//log-in
        {

        }
        private void encrapt(string name, string pass)//shifr
        {
            for (int i = 0; i < name.Length; i++)
            {
                login += (char)(name[i] + 1);
            }
            for (int i = 0; i < pass.Length; i++) 
            {
                passw += (char)(pass[i] + 1);
            }
        }
        private void decrapt(string name, string pass)//deshifr
        {
            for (int i = 0; i < name.Length; i++)
            {
                login += (char)(name[i] - 1);
            }
            for (int i = 0; i < pass.Length; i++)
            {
                passw += (char)(pass[i] - 1);
            }
        }

    }
}
