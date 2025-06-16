//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using RestProject.Data;
//using RestProject.Models;

//namespace RestProject.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class StudentsController : ControllerBase
//    {


//        [HttpGet("All", Name = "Students")]
//        public ActionResult<IEnumerable<Student>> GetAllStudents()
//        {
//            return Ok(StudentData.Students);
//        }
//        [HttpGet("Passed", Name = "PassedStudents")]
//        public ActionResult<IEnumerable<Student>> GetPassedStudents()
//        {
//            return Ok(StudentData.Students.Where(s => s.Grade >= 50));
//        }
//        [HttpGet("AVG")]
//        public ActionResult<double> GetGradesAVG()
//        {
//            if (StudentData.Students.Count == 0)
//                return NotFound("No Student Found");
//            return Ok(StudentData.Students.Average(s => s.Grade));
//        }
//        [HttpGet("{Id}", Name = "GetStudentById")]
//        [ProducesResponseType(StatusCodes.Status200OK)]
//        [ProducesResponseType(StatusCodes.Status400BadRequest)]
//        [ProducesResponseType(StatusCodes.Status404NotFound)]

//        public ActionResult<Student> GetStudentById(int Id)
//        {
//            if (Id < 1)
//                return BadRequest("InValid Id");
//            var stu = StudentData.Students.FirstOrDefault(s => s.Id == Id);
//            if (stu == null)
//                return NotFound("Not Found");
//            return Ok(stu);
//        }
//        [HttpPost("AddNewStudent")]
//        [ProducesResponseType(StatusCodes.Status200OK)]
//        [ProducesResponseType(StatusCodes.Status201Created)]
//        [ProducesResponseType(StatusCodes.Status400BadRequest)]
//        [ProducesResponseType(StatusCodes.Status404NotFound)]
//        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

//        public ActionResult<Student> CreatNewStudent(Student stu)
//        {
//            if (stu == null || stu.Id < 1 || stu.Id > StudentData.Students.Max(s => s.Id) + 1 || stu.Age < 0 || string.IsNullOrEmpty(stu.Name))
//            {
//                return NotFound();
//            }
//            // stu.Id = StudentData.Students.Max(s => s.Id) + 1;
//            StudentData.Students.Add(stu);
//            return CreatedAtRoute("GetStudentById", new { stu.Id }, stu);
//        }

//        [HttpDelete("{Id}", Name = "DeleteStudent")]
//        [ProducesResponseType(StatusCodes.Status200OK)]
//        [ProducesResponseType(StatusCodes.Status400BadRequest)]
//        [ProducesResponseType(StatusCodes.Status404NotFound)]
//        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

//        public ActionResult DeleteStudent(int Id)
//        {
//            if (Id < 1)
//                return BadRequest();
//            var stu = StudentData.Students.FirstOrDefault(s => s.Id == Id);
//            if (stu == null)
//                return NotFound();
//            StudentData.Students.Remove(stu);
//            return Ok();
//        }

//        [HttpPut("{Id}", Name = "UpdateStudent")]
//        [ProducesResponseType(StatusCodes.Status200OK)]
//        [ProducesResponseType(StatusCodes.Status400BadRequest)]
//        [ProducesResponseType(StatusCodes.Status404NotFound)]
//        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

//        public ActionResult<Student> UpdateStudent(int Id, Student updatedStu)
//        {
//            if (Id < 1 )
//                return BadRequest();
//            var stu = StudentData.Students.FirstOrDefault(s => s.Id == Id);
//            if (stu == null)
//                return NotFound();
//            stu.Name = updatedStu.Name;
//            stu.Age = updatedStu.Age;
//            stu.Grade = updatedStu.Grade;
//            return Ok(stu);
//        }
//    }

//}

