using HOTEL.menu;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace HOTEL.authentication
{
    public class auth
    {
        admin_menu admin_Menu = new admin_menu();
        employ_menu employ_Menu = new employ_menu();
        user_menu user_Menu = new user_menu();
        private string login = "";
        private string passw = "";
        private int id;
        private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Windows 11\source\repos\HOTEL\HOTEL\Data_Set.mdf"";Integrated Security=True";
        public void purpose(string name, string pass, string role, bool b)//sign-in
        {
            encrapt(name, pass);
            string sql = "INSERT INTO users (user_id, login, passwd, role, sysin) VALUES (@user_id, @login, @passwd, @role, @sysin)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand getMaxIdCommand = new SqlCommand("SELECT MAX(user_id) FROM users", connection))
                {
                    object result = getMaxIdCommand.ExecuteScalar();
                    if (result != DBNull.Value)
                    {
                        id = Convert.ToInt32(result) + 1;
                    }
                }
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@user_id", id);
                    command.Parameters.AddWithValue("@login", login);
                    command.Parameters.AddWithValue("@passwd", passw);
                    command.Parameters.AddWithValue("@role", role);
                    command.Parameters.AddWithValue("@sysin", b);
                    int rowsAffected = command.ExecuteNonQuery();
                }
            }
            choise(role);
        }
        public void purpose2(string name, string pass)//log-in
        {
            encrapt(name, pass);
            string sqlSelect = @"SELECT role FROM users WHERE login = @login AND passwd = @passwd;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlSelect, connection))
                {
                    command.Parameters.AddWithValue("@login", login);
                    command.Parameters.AddWithValue("@passwd", passw);
                    object result = command.ExecuteScalar();
                    string role = Convert.ToString(result)!;
                    if (role != null)
                    {
                        choise(role);
                    }
                    else
                    {
                        Clear();
                        WriteLine("Invalid login or password");
                        login login = new login();
                        login.tk();
                    }
                }
            }
        }
        private void choise(string role)
        {
            if (role != string.Empty)
            {
                Clear();
                switch (role)
                {
                    case "admin":admin_Menu.menu(); WriteLine(role); break;
                    case "employ":employ_Menu.menu(); WriteLine(role); break;
                    case "user": user_Menu.menu(); WriteLine(role); break;
                }
            }
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
