using EFStudents.DTOs.Requests;
using EFStudents.DTOs.Responses;
using EFStudents.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace EFStudents.Services
{
    public class SqlServerDbService : IStudentsDbService
    {

        private readonly StudentContext _context;

        public SqlServerDbService(StudentContext context)
        {
            _context = context;
        }

        public async Task<int> AddStudent(AddStudentRequest req)
        {
            var student = new Student
            {
                IndexNumber = req.IndexNumber,
                FirstName = req.FirstName,
                LastName = req.LastName,
                BirthDate = DateTime.Parse(req.BirthDate),
                IdEnrollment = req.IdEnrollment
            };

            _context.Student.Add(student);

            await _context.SaveChangesAsync();

            return 1;
        }
        
        public async Task<int> DeleteStudent(string sIndex)
        {
            var exists = _context.Student.Any(s => s.IndexNumber == sIndex);

            if(!exists)
            {
                return -1;
            } else
            {
                var student = new Student
                {
                    IndexNumber = sIndex
                };

                _context.Attach(student);
                _context.Entry(student).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
                return 1;

            }

        }

        public async Task<EnrollStudentResponse> EnrollStudent(EnrollStudentRequest req)
        {
            var studiesExist = _context.Studies.Any(e => e.Name == req.Studies);

            if (!studiesExist)
            {
                return null;
            }
            else
            {
                var sList = _context.Studies.Where(s => s.Name == req.Studies).ToList();
                var idStudy = sList[0].IdStudy;
                int enrollId = 1;

                var enrollmentExist = _context.Enrollment.Any(e => e.IdStudy == idStudy && e.Semester == 1);

                if (enrollmentExist)
                {
                    var eList = _context.Enrollment.Where(e => e.IdStudy == idStudy && e.Semester == 1).ToList();
                    enrollId = eList[0].IdEnrollment;

                }
                else
                {
                    var enrollment = new Enrollment
                    {
                        Semester = 1,
                        IdStudy = idStudy,
                        StartDate = DateTime.Now

                    };

                    await _context.Enrollment.AddAsync(enrollment);
                    await _context.SaveChangesAsync();

                    var addStudent = new AddStudentRequest
                    {
                        IndexNumber = req.IndexNumber,
                        FirstName = req.FirstName,
                        LastName = req.LastName,
                        BirthDate = req.BirthDate,
                        IdEnrollment = enrollId
                    };

                    await AddStudent(addStudent);

                    var eStudentList = _context.Enrollment.Where(e => e.IdEnrollment == enrollId).ToList();

                    var resp = new EnrollStudentResponse
                    {
                        IdEnrollment = eStudentList[0].IdEnrollment,
                        IdStudy = eStudentList[0].IdStudy,
                        Semester = eStudentList[0].Semester,
                        StartDate = eStudentList[0].StartDate,

                    };
                    return resp;
                }
        }
            return null;
        }


        public IEnumerable<StudentResponse> GetStudents()
        {
            //var res = _context.Student.ToList();

            var res = _context.Student.Include(s => s.IdEnrollmentNavigation).ThenInclude(s => s.IdStudyNavigation).Select(student => new StudentResponse
            {
                IndexNumber = student.IndexNumber,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Birthdate = student.BirthDate.ToString(),
                Semester = student.IdEnrollmentNavigation.Semester,
                Studies = student.IdEnrollmentNavigation.IdStudyNavigation.Name

            }).ToList();

            return res;
        }

        public Task<PromoteStudentResponse> PromoteStudents(PromoteStudentRequest req)
        {
            throw new NotImplementedException();
        }

        public async Task<int> UpdateStudent(UpdateStudentRequest req)
        {
            var exists = _context.Student.Any(s => s.IndexNumber == req.IndexNumber);

            if(!exists)
            {
                return -1;
            } else
            {
                var student = new Student {

                    IndexNumber = req.IndexNumber,
                    FirstName = req.FirstName,
                    LastName = req.LastName,
                    BirthDate = DateTime.Parse(req.BirthDate),
                    IdEnrollment = req.IdEnrollment
                };

                _context.Attach(student);
                _context.Entry(student).State =EntityState.Modified;
                await _context.SaveChangesAsync();

                return 1;
            }

        }
    }
}
