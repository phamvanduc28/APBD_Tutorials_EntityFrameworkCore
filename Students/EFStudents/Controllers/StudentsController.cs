using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFStudents.DTOs.Requests;
using EFStudents.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EFStudents.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentsDbService _service;

        public StudentsController(IStudentsDbService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetStudents()
        {
            var res = _service.GetStudents();
            return Ok(res);
        }
        
        [HttpPost]
        public async Task<IActionResult> AddStudent(AddStudentRequest req)
        {

            int res = await _service.AddStudent(req);

            if(res == -1)
            {
                return BadRequest();
            } else
            {
                return Ok("Student was added");
            }

        }

        [HttpDelete("{sIndex}")]
        public async Task<IActionResult> DeleteStudent(string sIndex)
        {
            int res = await _service.DeleteStudent(sIndex);


            if(res == -1)
            {
                return NotFound("Student with given index does not exist");
            }
            else
            {
                return Ok("Student deleted");
            }
        }

        [HttpPut]

        public async Task<IActionResult> UpdateStudent(UpdateStudentRequest req)
        {

            int res = await _service.UpdateStudent(req);

            if(res == -1)
            {
                return NotFound("Student with given index does not exist");
            } else
            {
                return Ok("Student information updated");
            }
        }

    
    }
}