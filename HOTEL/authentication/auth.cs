using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
        private int id = 3;

        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Windows 11\source\repos\HOTEL\HOTEL\Data_Set.mdf"";Integrated Security=True";
        
        public void purpose(string name, string pass, string role, bool b)//sign-in
        {
            encrapt(name, pass);
            string sql = "INSERT INTO users (user_id, login, passwd, role, sysin) VALUES (@user_id, @login, @passwd, @role, @sysin)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@user_id", id);
                    command.Parameters.AddWithValue("@login", login);
                    command.Parameters.AddWithValue("@passwd", passw);
                    command.Parameters.AddWithValue("@role", role);
                    command.Parameters.AddWithValue("@sysin", b);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        WriteLine("Данные успешно добавлены в базу данных.");
                    }
                    else
                    {
                        WriteLine("Произошла ошибка при добавлении данных.");
                    }
                }
            }

        }
        public void purpose2(string name, string pass,bool b)//log-in
        {
            encrapt(name, pass);
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
