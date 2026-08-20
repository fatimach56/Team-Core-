using Microsoft.Data.SqlClient;

class Program
{
    static string connectionString = "Server=DESKTOP-7OQMIB8\\SQLEXPRESS;Database=TeamCore;Trusted_Connection=True;TrustServerCertificate=True;";

    static void Main()
    {
        while (true)
        {
            Console.WriteLine("\n===== TeamCore =====");
            Console.WriteLine("1. Add Employee");
            Console.WriteLine("2. View All Employees");
            Console.WriteLine("3. Update Employee");
            Console.WriteLine("4. Delete Employee");
            Console.WriteLine("5. Exit");
            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1": AddEmployee(); break;
                case "2": ViewEmployees(); break;
                case "3": UpdateEmployee(); break;
                case "4": DeleteEmployee(); break;
                case "5": return;
                default: Console.WriteLine("Invalid choice, try again."); break;
            }
        }
    }

    static void AddEmployee()
    {
        Console.Write("Enter Name: ");
        string name = Console.ReadLine();
        Console.Write("Enter Email: ");
        string email = Console.ReadLine();
        Console.Write("Enter Department: ");
        string dept = Console.ReadLine();
        Console.Write("Enter Salary: ");
        float salary = float.Parse(Console.ReadLine());

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();
            string query = "INSERT INTO Employees (Name, Email, Department, Salary) VALUES (@Name, @Email, @Department, @Salary)";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Name", name);
            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@Department", dept);
            cmd.Parameters.AddWithValue("@Salary", salary);
            cmd.ExecuteNonQuery();
        }
        Console.WriteLine("Employee added successfully!");
    }
    static void ViewEmployees()
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();
            string query = "SELECT * FROM Employees";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();

            Console.WriteLine("\nID | Name | Email | Department | Salary");
            Console.WriteLine("--------------------------------------------");
            while (reader.Read())
            {
                Console.WriteLine($"{reader["Id"]} | {reader["Name"]} | {reader["Email"]} | {reader["Department"]} | {reader["Salary"]}");
            }
        }
    }

    static void UpdateEmployee()
    {
        Console.Write("Enter Employee Id to update: ");
        int id = int.Parse(Console.ReadLine());
        Console.Write("Enter New Salary: ");
        float salary = float.Parse(Console.ReadLine());

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();
            string query = "UPDATE Employees SET Salary = @Salary WHERE Id = @Id";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Salary", salary);
            cmd.Parameters.AddWithValue("@Id", id);
            int rows = cmd.ExecuteNonQuery();

            if (rows > 0)
                Console.WriteLine("Employee updated successfully!");
            else
                Console.WriteLine("Employee not found.");
        }
    }

    static void DeleteEmployee()
    {
        Console.Write("Enter Employee Id to delete: ");
        int id = int.Parse(Console.ReadLine());

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();
            string query = "DELETE FROM Employees WHERE Id = @Id";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Id", id);
            int rows = cmd.ExecuteNonQuery();

            if (rows > 0)
                Console.WriteLine("Employee deleted successfully!");
            else
                Console.WriteLine("Employee not found.");
        }
    }
}