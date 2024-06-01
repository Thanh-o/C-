using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace Atom
{
    public class Program
    {
        static string connectionString = "Server=localhost;Database=atomic;User Id=root;Password=";

        public static void Main(string[] args)
        {
            System.Console.WriteLine("Atomic Information");
            System.Console.WriteLine("==================");

              for (int i = 0; i < 10; i++)
            {
                Atom atom = new Atom();

                Console.Write("Enter atomic number  : ");
                atom.Id = Convert.ToInt32(Console.ReadLine());
                if (atom.Id <= 0) break;

                Console.Write("Enter symbol  : ");
                atom.Symbol = Console.ReadLine();
                Console.Write("Enter full name  : ");
                atom.FullName = Console.ReadLine();
                Console.Write("Enter atomic weight  : ");
                atom.Weight = Convert.ToSingle(Console.ReadLine());
                 if (atom.Weight <= 0 ||string.IsNullOrEmpty(atom.Symbol)||string.IsNullOrEmpty(atom.FullName)) throw new Exception("Invalid input.");


                Save(atom);
            }

            DisplayAllAtoms();
        }

        public static void Save(Atom atom)
        {
            using (MySqlConnection db = new MySqlConnection(connectionString))
            {
                db.Open();
                string sqlQuery = "INSERT INTO atoms(id, symbol, full_name, weight) VALUES (@Id, @Symbol, @FullName, @Weight)";
                using (MySqlCommand cmd = new MySqlCommand(sqlQuery, db))
                {
                    cmd.Parameters.AddWithValue("@Id", atom.Id);
                    cmd.Parameters.AddWithValue("@Symbol", atom.Symbol);
                    cmd.Parameters.AddWithValue("@FullName", atom.FullName);
                    cmd.Parameters.AddWithValue("@Weight", atom.Weight);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void DisplayAllAtoms()
        {
            Console.WriteLine("No Sym Name Weight");
            Console.WriteLine("------------------------------------");

            using (MySqlConnection db = new MySqlConnection(connectionString))
            {
                db.Open();
                string sqlQuery = "SELECT id, symbol, full_name, weight FROM atoms";
                using (MySqlCommand cmd = new MySqlCommand(sqlQuery, db))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                           float weight = reader.GetFloat("weight");

                            int integerPart = (int)weight / 1000;
                            float decimalPart = (weight % 1000) / 1000;

                            float newWeight = integerPart + decimalPart;
                            Console.WriteLine($"{reader["id"]} {reader["symbol"]} {reader["full_name"]} {newWeight}");
                        }
                    }
                }
            }
        }

        public class Atom
        {
            public int Id { get; set; }
            public string Symbol { get; set; }
            public string FullName { get; set; }
            public float Weight { get; set; }
        }
    }
}
