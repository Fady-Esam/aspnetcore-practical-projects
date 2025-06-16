using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.Data;
using System.Data;
using RestProject.Models;

namespace RestProject.Data
{
    public class StudentDataSQL
    {
        private const string conString = "Server = .\\SQLSERVERTEST; DataBase = LearnASP; TrustServerCertificate=True;  User Id = sa; Password = 123;";
        public static List<Student> GetAllStudents()
        {
            List<Student> students = new List<Student> { };
            using(SqlConnection sqlcon = new SqlConnection(conString))
            {  
                sqlcon.Open();
                using (SqlCommand cmd = new SqlCommand("sp_GetAllStudents", sqlcon))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Student student = new Student
                            {
                                Id = (int)reader["Id"],
                                Name = (string)reader["Name"],
                                Age = (int)reader["Age"],
                                Grade = (int)reader["Grade"]

                            };
                            students.Add(student);
                        }
                    }
                }
            }
            return students;
        }
        public static int AddStudent(Student stu)
        {
            int result = -1;
            try
            {
                using (SqlConnection sqlcon = new SqlConnection(conString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_AddStudent", sqlcon))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Name", stu.Name);
                        cmd.Parameters.AddWithValue("@Age", stu.Age);
                        cmd.Parameters.AddWithValue("@Grade", stu.Grade);


                        SqlParameter outputParam = new SqlParameter
                        {
                            ParameterName = "@NewStudentId",
                            SqlDbType = SqlDbType.Int,
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(outputParam);
                        sqlcon.Open();

                        cmd.ExecuteNonQuery();
                        result = (int)outputParam.Value;
                    }
                }

            } catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return result;
        }
        public static Student? GetStudent(int Id)
        {
            try
            {
                using (SqlConnection sqlcon = new SqlConnection(conString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_GetStudent", sqlcon))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", Id);
                        sqlcon.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Student student = new Student
                                {
                                    Id = (int)reader["Id"],
                                    Name = (string)reader["Name"],
                                    Age = (int)reader["Age"],
                                    Grade = (int)reader["Grade"]

                                };
                                return student;
                            }
                        }
                        
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }

    }
    
}
