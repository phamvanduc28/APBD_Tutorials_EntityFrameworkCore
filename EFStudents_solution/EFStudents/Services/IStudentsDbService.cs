using EFStudents.DTOs.Requests;
using EFStudents.DTOs.Responses;
using EFStudents.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFStudents.Services
{
    public interface IStudentsDbService
    {

        //task 2
        public IEnumerable<StudentResponse> GetStudents();
        public Task<int> AddStudent(AddStudentRequest req);
        public Task<int> UpdateStudent(UpdateStudentRequest req);
        public Task<int> DeleteStudent(string index);

        //task 3
        public Task<EnrollStudentResponse> EnrollStudent(EnrollStudentRequest req);
        public Task<PromoteStudentResponse> PromoteStudents(PromoteStudentRequest req);



    }
}
