using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFStudents.DTOs.Responses
{
    public class StudentResponse
    {
        public string IndexNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Birthdate { get; set; }
        public int Semester { get; set; }
        public string Studies { get; set; }
    }
}
