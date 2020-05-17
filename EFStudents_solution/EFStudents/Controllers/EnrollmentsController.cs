using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFStudents.DTOs.Requests;
using EFStudents.DTOs.Responses;
using EFStudents.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EFStudents.Controllers
{
    [Route("api/enrollments")]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {

        private readonly IStudentsDbService _service;

        public EnrollmentsController(IStudentsDbService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> EnrollStudent(EnrollStudentRequest req)
        {
            EnrollStudentResponse resp = away _service.EnrollStudent(req);

            if(resp == null)
            {
                return BadRequest();
            }
            else
            {
                return this.StatusCode(201, resp);
            }
        }

        [HttpPost("promote")]
        public async Task<IActionResult> PromoteStudents(PromoteStudentRequest req)
        {
            PromoteStudentResponse resp = await _service.PromoteStudents(req);

            if(resp == null)
            {
                return BadRequest();
            }
            else
            {
                return this.StatusCode(201, resp);
            }

        }



    }
}