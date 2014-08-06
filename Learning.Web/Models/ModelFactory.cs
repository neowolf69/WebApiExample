using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Learning.Data.Entities;
using AutoMapper;
using System.Net.Http;
using Learning.Data;


namespace Learning.Web.Models
{
    public class ModelFactory
    {
        private System.Web.Http.Routing.UrlHelper _UrlHelper;
        private ILearningRepository _repo;

        public ModelFactory(HttpRequestMessage request, ILearningRepository repo)
        {
            _UrlHelper = new System.Web.Http.Routing.UrlHelper(request);
            _repo = repo;

        }

        public CourseModel Create(Course course)
        {

            CourseModel courseModel = Mapper.Map<CourseModel>(course);
            courseModel.Url = _UrlHelper.Link("Courses", new { id = course.Id });
            
            return courseModel;

            //return new CourseModel()
            //{

            //    Url = _UrlHelper.Link("Courses", new { id = course.Id }),
            //    Id = course.Id,
            //    Name = course.Name,
            //    Duration = course.Duration,
            //    Description = course.Description,
            //    Tutor = Create(course.CourseTutor),
            //    Subject = Create(course.CourseSubject)
            //};
        }

        public TutorModel Create(Tutor tutor)
        {
            
            return Mapper.Map<TutorModel>(tutor);

            //return new TutorModel()
            //{
            //    Id = tutor.Id,
            //    Email = tutor.Email,
            //    UserName = tutor.UserName,
            //    FirstName = tutor.FirstName,
            //    LastName = tutor.LastName,
            //    Gender = tutor.Gender
            //};
        }

        public SubjectModel Create(Subject subject)
        {
            
            return Mapper.Map<SubjectModel>(subject);
            //return new SubjectModel()
            //{
            //    Id = subject.Id,
            //    Name = subject.Name
            //};
        }

        public EnrollmentModel Create(Enrollment enrollment)
        {
            return Mapper.Map<EnrollmentModel>(enrollment);
            //return new EnrollmentModel()
            //{
            //    EnrollmentDate = enrollment.EnrollmentDate,
            //    Course = Create(enrollment.Course)
            //};
        }

        public StudentModel Create(Student student)
        {
            StudentModel studentModel = Mapper.Map<StudentModel>(student);
            studentModel.Url = _UrlHelper.Link("Students", new { userName = student.UserName });
            return studentModel;

            //return new StudentModel()
            //{
            //    Url = _UrlHelper.Link("Students", new { userName = student.UserName }),
            //    Id = student.Id,
            //    Email = student.Email,
            //    UserName = student.UserName,
            //    FirstName = student.FirstName,
            //    LastName = student.LastName,
            //    Gender = student.Gender,
            //    DateOfBirth = student.DateOfBirth,
            //    RegistrationDate = student.RegistrationDate.Value,
            //    EnrollmentsCount = student.Enrollments.Count(),
            //    Enrollments = student.Enrollments.Select(e => Create(e))
            //};
        }

        public StudentBaseModel CreateSummary(Student student)
        {
            StudentBaseModel studentBase = Mapper.Map<Student, StudentBaseModel>(student);
            studentBase.Url = _UrlHelper.Link("Students", new { userName = student.UserName });
            return studentBase;
            //return new StudentBaseModel()
            //{
            //    Url = 
            //    Id = student.Id,
            //    FirstName = student.FirstName,
            //    LastName = student.LastName,
            //    Gender = student.Gender,
            //    EnrollmentsCount = student.Enrollments.Count(),
            //};
        }

        public StudentV2BaseModel CreateV2Summary(Student student)
        {
            StudentV2BaseModel studentV2BaseModel = Mapper.Map<StudentV2BaseModel>(student);
            studentV2BaseModel.Url = _UrlHelper.Link("Students", new { userName = student.UserName });
            return studentV2BaseModel;
            //return new StudentV2BaseModel()
            //{
            //    Url = _UrlHelper.Link("Students", new { userName = student.UserName }),
            //    Id = student.Id,
            //    FullName = string.Format("{0} {1}", student.FirstName, student.LastName),
            //    Gender = student.Gender,
            //    EnrollmentsCount = student.Enrollments.Count(),
            //    CoursesDuration = Math.Round(student.Enrollments.Sum(c => c.Course.Duration))
            //};
        }

        public Course Parse(CourseModel model)
        {
            try
            {
                var course = new Course()
                {
                    Name = model.Name,
                    Description = model.Description,
                    Duration = model.Duration,
                    CourseSubject = _repo.GetSubject(model.Subject.Id),
                    CourseTutor = _repo.GetTutor(model.Tutor.Id)
                };

                return course;
            }
            catch (Exception)
            {

                return null;
            }
        }


    }
}
