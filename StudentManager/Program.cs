using System;//import in java
using System.Collections.Generic;//Generic same in java

namespace StudentManagement{
    class Student{
        public int Id{get;set;}
        public string Name{get;set;}
        public int Age{get;set;}
        public string Major{get;set;}
    
    public override string ToString(){
        return $"ID: {Id}, Name:{Name}, Age:{Age}, Major:{Major} ";
    }
}
     class Program{
        static List<Student> students = new List<Student>();
        static int currentId = 1;
        public static void Main(string[] args)
        {
            while(true){
                Console.WriteLine("Student Management");
                Console.WriteLine("1. Add new Student");
                Console.WriteLine("2. Display all Students");
                Console.WriteLine("3. Search student by id");
                Console.WriteLine("4. Delete student");
                Console.WriteLine("5. Exit");

                string choice = Console.ReadLine();//Scanner ...(System.in)
                switch(choice){
                    case "1": AddStudent();
                        break;
                    case "2": DisplayAllStudent();
                        break;
                    case "3": SearchStudentById();
                        break;
                    case "4": Delete();
                        break;
                    case "5": return;        
                    default:
                        Console.WriteLine("Invalid choice. Try again");
                        break;    
                }
                Console.WriteLine();
            }
        }
        static void AddStudent(){
            Console.WriteLine("Enter name: ");
            string name = Console.ReadLine();

            Console.WriteLine("Enter age: ");
            int age;
            while(!int.TryParse(Console.ReadLine(), out age)){
                Console.WriteLine("Invalid input, Pls enter again");
                Console .WriteLine("Enter age: ");
            }
            Console.WriteLine("Enter major: ");
            string major = Console.ReadLine();

            // Student newStudent = new Student();
            // newStudent.Id = currentId++;
            // newStudent.Name = name;

            Student newStudent = new Student{
                Id = currentId++,
                Name = name,
                Age = age,
                Major = major
            };
            students.Add(newStudent);
            Console.WriteLine("Add student successfully!");
        }
        static void DisplayAllStudent(){
            Console.WriteLine("List of all students: ");
            foreach(var student in students){
                Console.WriteLine(student);
            }
        }
        static void SearchStudentById(){
            Console.WriteLine("Enter id: ");
            int id;
              while(!int.TryParse(Console.ReadLine(), out id)){
                Console.WriteLine("Invalid input, Pls enter again");
                Console .WriteLine("Enter id to search: ");
            }
            var student = students.Find(s =>s.Id == id);//lambda expression
            if(student != null){
                Console.WriteLine("Student found: ");
                Console.WriteLine(student);
            }else{
                Console.WriteLine("Student not found: ");
            }
        }
         static void Delete()
        {
            Console.WriteLine("Enter id: ");
            int id;
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Invalid input. Please enter again.");
                Console.WriteLine("Enter id to delete: ");
            }

            var student = students.Find(s => s.Id == id);
            if (student != null)
            {
                students.Remove(student);
                Console.WriteLine("Student deleted successfully!");
            }
            else
            {
                Console.WriteLine("Student not found.");
            }
        }
        
}
}
