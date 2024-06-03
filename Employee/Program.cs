using System;
using System.Data.Common;
using MySql.Data.MySqlClient;

namespace Employee{
    public class Employee{
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public long Sin { get; set; }
        public double Salary { get; set; }
        public override string ToString()
    {
        return $"Employee Info:\n " +
               $"Name: {LastName} {FirstName}  \n   " +
               $"Address: {Address} \n" +
               $"SIN: {Sin}  \n   " +
               $"Salary: ${Salary:F2}   \n  ";
    }

    }
    public class Program{
         static string connectionString = "Server=localhost;Database=emp;User Id=root;Password=";
         
         public static void Main(string[] args){
            Console.WriteLine("Enter Information Employees: ");
            Console.WriteLine("===========================");
            Employee em = new Employee();
            Console.Write("Enter First Name: ");
            em.FirstName = Console.ReadLine();
            Console.Write("Enter Last Name: ");
            em.LastName = Console.ReadLine();
            Console.Write("Enter Address: ");
            em.Address = Console.ReadLine();
            Console.Write("Enter SIN: ");
            em.Sin = Convert.ToInt64(Console.ReadLine());
            Console.Write("Enter Salary: ");
            em.Salary = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("===========================");
            
            Add(em);
            DisplayAll(10);

         }

         public static void Add(Employee em){
            using (MySqlConnection db = new MySqlConnection(connectionString)){
                db.Open();
                string sqlQuery = "insert into employee(firstName, lastName, address, sin, salary) values(@FirstName, @LastName, @Address, @Sin, @Salary)";
                using (MySqlCommand cmd = new MySqlCommand(sqlQuery, db))
                {
                    cmd.Parameters.AddWithValue("@FirstName", em.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", em.LastName);
                    cmd.Parameters.AddWithValue("@Address", em.Address);
                    cmd.Parameters.AddWithValue("@Sin", em.Sin);
                    cmd.Parameters.AddWithValue("@Salary", em.Salary);
                    cmd.ExecuteNonQuery();
                }
            }
         }
         public static void DisplayAll(double percentage){
            using (MySqlConnection db = new MySqlConnection(connectionString))
            {
                db.Open();
                string sqlQuery = "select * from employee";
                using (MySqlCommand cmd = new MySqlCommand(sqlQuery, db))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while( reader.Read())
                        {
                             Employee em = new Employee
                            {
                                FirstName = reader["firstName"].ToString(),
                                LastName = reader["lastName"].ToString(),
                                Address = reader["address"].ToString(),
                                Sin = Convert.ToInt64(reader["sin"]),
                                Salary = Convert.ToDouble(reader["salary"])
                            };
                        double bonus = em.Salary * (percentage / 100);
                        Console.WriteLine(em.ToString() + $"\nBonus ({percentage}%): ${bonus:F2}");
                        Console.WriteLine("********");
                        
                        }
                    }
                }
            }
         }

    }
}