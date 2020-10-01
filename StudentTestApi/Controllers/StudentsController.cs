using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BusinessLayer.Services;
using BusinessLayer.Model;
using Microsoft.AspNetCore.Routing;

namespace StudentTestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {

        private readonly IStudentServices _STUDDAL;

        public StudentsController(IStudentServices studdal)
        {
            _STUDDAL = studdal;

        }



        [HttpGet]

        public ActionResult<IEnumerable<Student>> GetAllStudent()
        {


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                var allstudents = _STUDDAL.GetAllStudent().ToList();
                return Ok(allstudents);
            }


        }

        [HttpGet("{id}")]
        public ActionResult<IEnumerable<Student>> GetStudentById([FromRoute] int id)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
              var stud=  _STUDDAL.GetStudentById(id).FirstOrDefault();
                return Ok(stud);
            }

        }


        [HttpPost]
        public ActionResult AddNewStudent([FromBody] Student student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _STUDDAL.AddNewStudent(student);
            return Ok();
        }


        [HttpPut("{id}")]
        public ActionResult UpdateStudent([FromRoute]int id, [FromBody] Student student)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            if (id != student.Id)
            {
                return BadRequest("Id's did not match!! ");
            }
            else
            {
                _STUDDAL.UpdateStudent(id, student);
                return Ok();
            }
        }



        [HttpDelete("{id}")]
        public ActionResult DeleteStudent([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest("Not a valid student id");
            }
            else
            {
                _STUDDAL.DeleteStudent(id);

            }
            return Ok();


        }

    }
}

