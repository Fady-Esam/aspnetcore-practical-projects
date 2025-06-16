using RestProject.Models;

namespace RestProject.Data
{
    public class StudentData
    {

        public static List<Student> Students = new List<Student>
        {
            new Student {Id = 1, Name = "Fady", Age = 20, Grade = 80},
            new Student {Id = 2, Name = "Hani", Age = 21, Grade = 4},
            new Student {Id = 3, Name = "Youssef", Age = 25, Grade = 75},

        };



    }
}
