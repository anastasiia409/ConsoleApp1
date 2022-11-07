using System.Data.SqlClient;

namespace ConsoleApp1
{

    internal class Program
    {
        public static async Task Main(string[] args)
        {

            SqlConnection sqlConnection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=projects_contracts;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

            await sqlConnection.OpenAsync();

            SqlCommand command = new SqlCommand();
                                                    
            command.Connection = sqlConnection;
            command.CommandText =
                "SELECT" +
                    " e.ID, e.Name, e.Surname, e.Address, e.Description," +
                    "p.Name, p.Description, p.DateOfStart, p.DateOfEnd, pos.Name, pos.Salary, DATEDIFF(day, ep.DateOfJoin, ISNULL(p.DateOfEnd, CURRENT_TIMESTAMP)) AS DaysInProject" +
                " FROM Employee AS e" +
                " INNER JOIN EmployeeProject AS ep ON e.ID = ep.employeeId" +
                " INNER JOIN Project AS p ON ep.projectId = p.ID" +
                " INNER JOIN Position AS pos ON pos.ID = ep.positionId" +
                " ORDER BY e.ID"; 

            SqlDataReader reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                for(var i=0; i< reader.FieldCount; i++)
                {
                    string s = $"{reader.GetValue(i)}";
                    Console.Write($"{s.PadRight(12)}|");
                }
                Console.WriteLine();
            }
            await sqlConnection.CloseAsync();
        }
    }
}