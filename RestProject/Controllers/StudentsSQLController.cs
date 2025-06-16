//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using RestProject.Data;
//using RestProject.Models;

//namespace RestProject.Controllers
//{
//    [Route("api/[controller]")]

//    [ApiController]
//    public class StudentsSQLController : ControllerBase
//    {
//        [HttpGet("All", Name = "Students")]
//        public ActionResult<IEnumerable<Student>> GetAllStudents()
//        {
//            return Ok(StudentDataSQL.GetAllStudents());
//        }
//        [HttpPost("AddNewStudent")]
//        [ProducesResponseType(StatusCodes.Status200OK)]
//        [ProducesResponseType(StatusCodes.Status201Created)]
//        [ProducesResponseType(StatusCodes.Status400BadRequest)]
//        [ProducesResponseType(StatusCodes.Status404NotFound)]
//        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

//        public ActionResult<Student> CreatNewStudent(Student stu)
//        {
//            if (stu == null || stu.Age < 0 || string.IsNullOrEmpty(stu.Name))
//            {
//                return NotFound();
//            }
//            int Id = StudentDataSQL.AddStudent(stu);
//            if(Id != -1)
//            {
//                stu.Id = Id;
//                return CreatedAtRoute("GetStudentById", new { Id = Id }, stu);
//            }
//            else
//                return BadRequest();
//        }
//        [HttpGet("{Id}", Name = "GetStudentById")]
//        [ProducesResponseType(StatusCodes.Status200OK)]
//        [ProducesResponseType(StatusCodes.Status400BadRequest)]
//        [ProducesResponseType(StatusCodes.Status404NotFound)]
//        public ActionResult<Student> GetStudentById(int Id)
//        {
//            if (Id < 1)
//                return BadRequest("InValid Id");
//            Student? stu = StudentDataSQL.GetStudent(Id);
//            if (stu == null)
//                return NotFound("Not Found");
//            return Ok(stu);
//        }
//    }
//}
