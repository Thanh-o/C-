using System;
using System.Net.Http.Headers;
using MySql.Data.MySqlClient;

namespace ProjectManagement{
    class Program{
        static string connectionString = "Server=localhost;Database=prodb;User Id=root;Password=";
        public static void Main(string[] args){
            while(true){
                Console.WriteLine("Project Management");
                Console.WriteLine("1. Add product");
                Console.WriteLine("2. Display all products");
                Console.WriteLine("3. Edit product");
                Console.WriteLine("4. Delete product");
                Console.WriteLine("5. Exit");
                Console.WriteLine("Choose an option: ");
                string choice = Console.ReadLine();

                switch(choice){
                    case "1":AddProduct();
                    break;
                    case "2":DisplayAllProduct();
                    break;
                    case "3":
                        EditProduct();
                        break;
                    case "4":
                        DeleteProduct();
                        break;
                    case "5":
                        return;
                        default:
                    Console.WriteLine("Invalid choice, please try again");
                    break;
                }
            }
        }
        static void AddProduct(){
            Product product = new Product();
            Console.WriteLine("Enter product name: ");
            product.Name = Console.ReadLine();
            Console.WriteLine("Enter product price: ");
            product.Price = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Enter product description: ");
            product.Description = Console.ReadLine();

            using(MySqlConnection db = new MySqlConnection(connectionString)){
                db.Open();
                string sqlQuery = "insert into products(name,price,description) value(@Name, @Price,@Description)";
                using(MySqlCommand cmd = new MySqlCommand(sqlQuery,db)){
                    cmd.Parameters.AddWithValue("@Name",product.Name);
                    cmd.Parameters.AddWithValue("@Price",product.Price);
                    cmd.Parameters.AddWithValue("@Description",product.Description);
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Product added succesfully!");
                }

            }
        }
        static void DisplayAllProduct(){
            using(MySqlConnection db = new MySqlConnection(connectionString)){
                db.Open();
                string sqlQuery = "select * from products";
                using(MySqlCommand cmd = new MySqlCommand(sqlQuery,db)){
                    using(MySqlDataReader reader = cmd.ExecuteReader()){
                        while(reader.Read()){
                            Console.WriteLine($"Name:{reader["Name"]},Price:{reader["price"]},Desc:{reader["description"]}");
                        }
                    }
                }
            }
        }
         static void EditProduct() {
            Console.Write("Enter the ID of the product to edit: ");
            if (int.TryParse(Console.ReadLine(), out int id)) {
                Product product = new Product { Id = id };
                Console.Write("Enter new product name (leave blank to keep current): ");
                string name = Console.ReadLine();
                Console.Write("Enter new product price (leave blank to keep current): ");
                string priceInput = Console.ReadLine();
                Console.Write("Enter new product description (leave blank to keep current): ");
                string description = Console.ReadLine();

              
                    using (MySqlConnection db = new MySqlConnection(connectionString)) {
                        db.Open();
                        string sqlQuery = "UPDATE products SET name = COALESCE(NULLIF(@Name, ''), name), price = COALESCE(NULLIF(@Price, ''), price), description = COALESCE(NULLIF(@Description, ''), description) WHERE id = @Id";
                        using (MySqlCommand cmd = new MySqlCommand(sqlQuery, db)) {
                            cmd.Parameters.AddWithValue("@Id", product.Id);
                            cmd.Parameters.AddWithValue("@Name", string.IsNullOrWhiteSpace(name) ? (object)DBNull.Value : name);
                            cmd.Parameters.AddWithValue("@Price", string.IsNullOrWhiteSpace(priceInput) ? (object)DBNull.Value : Convert.ToDecimal(priceInput));
                            cmd.Parameters.AddWithValue("@Description", string.IsNullOrWhiteSpace(description) ? (object)DBNull.Value : description);
                            int rowsAffected = cmd.ExecuteNonQuery();
                            if (rowsAffected > 0) {
                                Console.WriteLine("Product updated successfully!");
                            } else {
                                Console.WriteLine("Product not found.");
                            }
                        }
                    }
               
            } else {
                Console.WriteLine("Invalid ID.");
            }
        }

        static void DeleteProduct() {
            Console.Write("Enter the ID of the product to delete: ");
            if (int.TryParse(Console.ReadLine(), out int id)) {
               
                    using (MySqlConnection db = new MySqlConnection(connectionString)) {
                        db.Open();
                        string sqlQuery = "DELETE FROM products WHERE id = @Id";
                        using (MySqlCommand cmd = new MySqlCommand(sqlQuery, db)) {
                            cmd.Parameters.AddWithValue("@Id", id);
                            int rowsAffected = cmd.ExecuteNonQuery();
                            if (rowsAffected > 0) {
                                Console.WriteLine("Product deleted successfully!");
                            } else {
                                Console.WriteLine("Product not found.");
                            }
                        }
                    }
               
            } else {
                Console.WriteLine("Invalid ID.");
            }
        }
    

    }
    public class Product{
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
    }
}
