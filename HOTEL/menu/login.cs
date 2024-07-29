using HOTEL.authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace HOTEL.menu
{
    public class login
    {
        auth auth = new auth();
        private string username_login = ""; // NAme
        public string password_login = ""; //PAsswd
        public string role_login = ""; //KAsbi
        public bool b = false;
        public void kirish()//chose
        {
            int tanlov;
            metka:
            Write("_-== Hush kelibsiz =-_\n 1.Tizimga kirish\n 2.Royxatdan otish\n 3.Chiqish\n ---> ");
            if (!int.TryParse(ReadLine(), out tanlov) || tanlov > 3 || tanlov <= 0)
            {
                Clear();
                goto metka;
            }
            switch (tanlov)
            {
                case 1:tk();break;
                case 2:ro();break;
                case 3: Environment.Exit(0);   ; break;                
            }
        }
        public void tk()//log-in
        {
            Clear();
            Write("_-= Tizimga kirish uchun =-_\n");
            Write(" Login : ");username_login = ReadLine()!;
            Write(" Passwd : ");password_login = ReadLine()!;
            b = true;
            auth.purpose2(username_login, password_login);
        }
        private void ro()//sign-in
        {
            Clear();
            metka1:
            Write("_-= Royxatdan otish uchun =-_\n");
            Write(" Login kirgizing : "); username_login = ReadLine()!;
            if(username_login.Length > 30)
            {
                Clear();
                WriteLine("No more 30 lеtters");goto metka1;
            }metka2:
            Write(" Passwd kirgizing: "); password_login = ReadLine()!;
            if(password_login.Length > 15)
            {
                Clear();
                WriteLine("No more 15 chars");
                Write($"_-= Royxatdan otish uchun =-_\n Login kirgizing : {username_login}\n");goto metka2;
            }metka3:
            Write(" Lavozimingiz tanlang \n  1.ADMIN\n  2.Employ\n  3.user\n--->");int a;
            if (!int.TryParse(ReadLine(), out a) || a > 3 || a <= 0)
            {
                Clear();
                WriteLine(" _-= Menudan birini tanlang! =-_");
                goto metka3;
                
            }
            switch(a)
            {
                case 1:role_login = "admin";break;
                case 2:role_login = "employ"; break;
                case 3:role_login = "user"; break;
            }
            b = true;
            auth.purpose(username_login, password_login, role_login,b);
        }
    }
}
