using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFStudents.DTOs.Requests
{
    public class AddStudentRequest
    {
        public string IndexNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BirthDate { get; set; }
        public int IdEnrollment { get; set; }
    }
}
