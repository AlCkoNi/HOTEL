using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace HOTEL.crud_rooms
{
    internal class rooms
    {
        public void run()
        {
            Console.WriteLine("Xush kelibsiz");

            CRUD room = new CRUD();
            int tanlov;
        metka:
            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine("1-Yangi xona qo'shish\n2-Xonani o'chirish\n3-Xona malumotlarini taxrirlash\n4-Xonalarni ko'rish");
            Console.WriteLine("---------------------------------------------------");
            Console.Write("Tanlovdan birini tanlang :   ");

            tanlov = int.Parse(Console.ReadLine());
            switch (tanlov)
            {
                case 1:
                    room.CreateRoom();
                    break;
                case 2:
                    room.DeleteRoom();
                    break;
                case 3:
                    room.UpdateRoom();
                    break;
                case 4:
                    room.Info();
                    break;
                default:
                    Console.WriteLine("Noto'g'ri tanlov!");
                    break;
            }

            Console.WriteLine("1-Davom etaman\n2-Dasturni yakunlayman");
            Console.WriteLine("--------------------------------------------------");
            Console.Write("Tanlovni kiriting : ");
            tanlov = int.Parse(Console.ReadLine());
            switch (tanlov)
            {
                case 1:
                    goto metka;
                case 2:
                    return;
                default:
                    Console.WriteLine("Noto'g'ri tanlov!");
                    goto metka;
            }
        }
    }
    
    class CRUD
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["RoomManagementDB"].ConnectionString;

        public void CreateRoom()
        {
            try
            {
                Rooms room = new Rooms();
                Console.Write("Familya kiriting : ");
                room.Last_Name = Console.ReadLine();
                Console.Write("Ism kiriting : ");
                room.First_Name = Console.ReadLine();
                Console.Write("xonaning turini tanlang : ");
                room.Room_type = Console.ReadLine();
                Console.Write("xonaning qavatini tanlang : ");
                room.Room_flour = int.Parse(Console.ReadLine());
                Console.Write("xonaning raqamini tanlang : ");
                room.Room_number = int.Parse(Console.ReadLine());
                Console.Write("xonaning malumotini kirgizing : ");
                room.Room_info = Console.ReadLine();
                Console.Write("xonaning narxini kiriting : ");
                room.Room_price = double.Parse(Console.ReadLine());

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO Rooms (First_Name, Last_Name, Room_type, Room_flour, Room_number, Room_info, Room_price) VALUES (@First_Name, @Last_Name, @Room_type, @Room_flour, @Room_number, @Room_info, @Room_price)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@First_Name", room.First_Name);
                        command.Parameters.AddWithValue("@Last_Name", room.Last_Name);
                        command.Parameters.AddWithValue("@Room_type", room.Room_type);
                        command.Parameters.AddWithValue("@Room_flour", room.Room_flour);
                        command.Parameters.AddWithValue("@Room_number", room.Room_number);
                        command.Parameters.AddWithValue("@Room_info", room.Room_info);
                        command.Parameters.AddWithValue("@Room_price", room.Room_price);
                        command.ExecuteNonQuery();
                    }
                }
                Console.WriteLine("Xona muvofaqqiyatli qo'shildi");
                Console.WriteLine("-----------------------------------------------------");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        public void DeleteRoom()
        {
            int index = IndexFind();
            if (index > -1)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "DELETE FROM Rooms WHERE RoomId = @RoomId";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@RoomId", index);
                        command.ExecuteNonQuery();
                    }
                }
                Console.WriteLine("Xona o'chirildi");
            }
            else
            {
                Console.WriteLine("Siz qidirgan xona mavjud emas");
            }
        }

        public void UpdateRoom()
        {
            int index = IndexFind();
            if (index > -1)
            {
                Rooms room = new Rooms();
                Console.WriteLine("Kerakli ma'lumotlarni kiriting");
                Console.Write("Ism: ");
                room.First_Name = Console.ReadLine();
                Console.Write("Familya: ");
                room.Last_Name = Console.ReadLine();
                Console.Write("Room type: ");
                room.Room_type = Console.ReadLine();
                Console.Write("Room flour: ");
                room.Room_flour = int.Parse(Console.ReadLine());
                Console.Write("Room number: ");
                room.Room_number = int.Parse(Console.ReadLine());
                Console.Write("Room info: ");
                room.Room_info = Console.ReadLine();
                Console.Write("Room price: ");
                room.Room_price = double.Parse(Console.ReadLine());

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "UPDATE Rooms SET First_Name = @First_Name, Last_Name = @Last_Name, Room_type = @Room_type, Room_flour = @Room_flour, Room_number = @Room_number, Room_info = @Room_info, Room_price = @Room_price WHERE RoomId = @RoomId";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@First_Name", room.First_Name);
                        command.Parameters.AddWithValue("@Last_Name", room.Last_Name);
                        command.Parameters.AddWithValue("@Room_type", room.Room_type);
                        command.Parameters.AddWithValue("@Room_flour", room.Room_flour);
                        command.Parameters.AddWithValue("@Room_number", room.Room_number);
                        command.Parameters.AddWithValue("@Room_info", room.Room_info);
                        command.Parameters.AddWithValue("@Room_price", room.Room_price);
                        command.Parameters.AddWithValue("@RoomId", index);
                        command.ExecuteNonQuery();
                    }
                }
                Console.WriteLine("Xona ma'lumotlari muvofaqqiyatli yangilandi");
            }
            else
            {
                Console.WriteLine("Siz qidirgan xona mavjud emas");
            }
        }

        public void Info()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Rooms";
                using (SqlCommand command = new SqlCommand(query, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"|Familya| => {reader["Last_Name"]}; |Ism| => {reader["First_Name"]}; |Room type| => {reader["Room_type"]}; |Room flour| => {reader["Room_flour"]}; |Room number| => {reader["Room_number"]}; |Room info| => {reader["Room_info"]}; |Room price| => {reader["Room_price"]};");
                    }
                }
            }
            Console.WriteLine("---------------------------------------------------------");
        }

        public int IndexFind()
        {
            Console.WriteLine("1-Familya\n2-Ism");
            int tanlov;
            string s;
            Console.WriteLine("---------------------------------------------------------------");
            Console.WriteLine("Tanlovni kiriting: ");
            tanlov = int.Parse(Console.ReadLine());

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                switch (tanlov)
                {
                    case 1:
                        Console.WriteLine("---------------------------------------------------------------");
                        Console.Write("Familya kiriting: ");
                        s = Console.ReadLine();
                        string query1 = "SELECT RoomId FROM Rooms WHERE Last_Name = @Last_Name";
                        using (SqlCommand command = new SqlCommand(query1, connection))
                        {
                            command.Parameters.AddWithValue("@Last_Name", s);
                            object result = command.ExecuteScalar();
                            if (result != null)
                            {
                                return Convert.ToInt32(result);
                            }
                        }
                        break;

                    case 2:
                        Console.WriteLine("---------------------------------------------------------------");
                        Console.Write("Ism kiriting: ");
                        s = Console.ReadLine();
                        string query2 = "SELECT RoomId FROM Rooms WHERE First_Name = @First_Name";
                        using (SqlCommand command = new SqlCommand(query2, connection))
                        {
                            command.Parameters.AddWithValue("@First_Name", s);
                            object result = command.ExecuteScalar();
                            if (result != null)
                            {
                                return Convert.ToInt32(result);
                            }
                        }
                        break;
                }
            }
            return -1;
        }
    }

    class Rooms
    {
        public int RoomId { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Room_type { get; set; }
        public int Room_flour { get; set; }
        public int Room_number { get; set; }
        public string Room_info { get; set; }
        public double Room_price { get; set; }
    }

}
