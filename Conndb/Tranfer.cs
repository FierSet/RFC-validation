using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.Data.Sqlite;

using RFC.Models;
using RFC.conndb;
using System.Data;

namespace RFC.conndb;

public class Transfer {

    public List<EmployeeModel> Employeelist(string ID, string RFC, string Name, string LastName, string BornDate, string Status) {

        var list_employee = new List<EmployeeModel>();  
        var cn = new Connection().getstringsql(); //$"Data Source={Path.Combine(Environment.CurrentDirectory, "empdb.db")};";
        
        try
        {
            using var conn = new SqliteConnection(cn);

            conn.Open(); // Open connection
            string sql = @"SELECT * FROM emp
                            WHERE
                                (@ID IS NULL OR ID = @ID) AND
                                (@RFC IS NULL OR RFC = @RFC) AND
                                (@Name IS NULL OR name LIKE '%' || @Name || '%') AND
                                (@LastName IS NULL OR lastname LIKE '%' || @LastName || '%') AND
                                (@BornDate IS NULL OR borndate = @BornDate) AND
                                (@Status IS NULL OR status = @Status);"; // Query to execute

            //string sql2 = @"SELECT * FROM emp;";

            SqliteCommand cmd = new SqliteCommand(sql, conn);

            cmd.Parameters.AddWithValue("@ID", (object)ID ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@RFC", (object)RFC ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Name", (object)Name ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@LastName", (object)LastName ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@BornDate", (object)BornDate ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Status", (object)Status ?? DBNull.Value);

            SqliteDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {

                list_employee.Add(new EmployeeModel()
                {
                    ID = Convert.ToInt32(reader["ID"]),
                    Name = reader["name"].ToString(),
                    LastName = reader["lastname"].ToString(),
                    RFC = reader["RFC"].ToString(),
                    BornDate = DateTime.Parse(reader["borndate"].ToString(), System.Globalization.CultureInfo.InvariantCulture),
                    Status = (EmployeeStatus)Convert.ToInt32(reader["status"])
                });
            }
            reader.Close(); // Close the reader

            conn.Close(); // Close connection
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error in list method: " + ex.Message + ":"  + ex.StackTrace); // Handle any exceptions
        }
        finally{}

        return list_employee;
    }

    
    public bool Save(EmployeeModel employee){
        bool RESP;
        
        var cn = $"Data Source={Path.Combine(Environment.CurrentDirectory, "empdb.db")};";//new Connection().getstringsql();
        try
        {
            using var conn = new SqliteConnection(cn);

            conn.Open(); // Open connection

            string sql = @"INSERT INTO emp (name, lastname, RFC, borndate, status) VALUES(@name, @lastname, @RFC, @date, @status)"; // Query to execute

            var cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            //SqliteCommand cmd = new SqliteCommand(sql, conn);

            cmd.Parameters.AddWithValue("@name", employee.Name);
            cmd.Parameters.AddWithValue("@lastname", employee.LastName);
            cmd.Parameters.AddWithValue("@RFC", employee.RFC);
            cmd.Parameters.AddWithValue("@date", employee.BornDate.ToString("yyyy-MM-dd"));
            cmd.Parameters.AddWithValue("@status", employee.Status);
            cmd.ExecuteNonQuery();
            //Console.WriteLine("passdata");
            RESP = true;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error in save method: " + ex.Message ); // Handle any exceptions
            //Console.WriteLine("Error in save method: " + ex.StackTrace);
            RESP = false;
        }

        return RESP;

    }
}