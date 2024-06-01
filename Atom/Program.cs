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

            while (true)
            {
                Atom atom = new Atom();

                Console.WriteLine("Enter atomic number (or 0 to quit): ");
                atom.Id = Convert.ToInt32(Console.ReadLine());
                if (atom.Id == 0) break;

                Console.WriteLine("Enter symbol: ");
                atom.Symbol = Console.ReadLine();
                Console.WriteLine("Enter full name: ");
                atom.FullName = Console.ReadLine();
                Console.WriteLine("Enter atomic weight: ");
                atom.Weight = Convert.ToDouble(Console.ReadLine());

                SaveAtomToDatabase(atom);
            }

            DisplayAllAtoms();
        }

        public static void SaveAtomToDatabase(Atom atom)
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
                            Console.WriteLine($"{reader["id"],1} {reader["symbol"],1} {reader["full_name"],1} {reader["weight"],1}");
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
            public double Weight { get; set; }
        }
    }
}
