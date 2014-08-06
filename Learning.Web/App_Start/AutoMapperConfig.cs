using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Learning.Web.Models;
using Learning.Data.Entities;
using System.Net.Http;

namespace Learning.Web
{
    public static class AutoMapperConfig
    {
        

        public static void RegisterMapping()
        {
            Mapper.CreateMap<Tutor, TutorModel>();
            Mapper.CreateMap<Subject, SubjectModel>();

            Mapper.CreateMap<Course, CourseModel>()
                .ForMember(dest => dest.Url, opts => opts.Ignore())
                .ForMember(d => d.Tutor, o => o.MapFrom(q => Mapper.Map<TutorModel>(q.CourseTutor)))
                .ForMember(d => d.Subject, o => o.MapFrom(q => Mapper.Map<SubjectModel>(q.CourseSubject)));

            Mapper.CreateMap<Enrollment, EnrollmentModel>()
                .ForMember(d => d.Course, o => o.MapFrom(q => Mapper.Map<CourseModel>(q.Course)));

            
            Mapper.CreateMap<Student, StudentBaseModel>()
                .ForMember( d=> d.Url, o => o.Ignore())
                .ForMember (d=> d.EnrollmentsCount, o => o.MapFrom( q => (q.Enrollments.Count())));
            

            Mapper.CreateMap<Student, StudentV2BaseModel>()
                .ForMember ( d=> d.Url, o => o.Ignore())
                .ForMember (d=> d.EnrollmentsCount, o => o.MapFrom( q => (q.Enrollments.Count())))
                .ForMember ( d=> d.FullName, o => o.MapFrom ( q => string.Format("{0} {1}", q.FirstName, q.LastName)))
                .ForMember ( d=> d.CoursesDuration, o => o.MapFrom ( q => q.Enrollments.Sum( c => c.Course.Duration)) );

                

            Mapper.CreateMap<Student, StudentModel>()
                .ForMember (d => d.Url, o => o.Ignore() )
                .ForMember ( d => d.RegistrationDate, o => o.MapFrom( q => q.RegistrationDate.Value) )
                .ForMember ( d => d.EnrollmentsCount, o => o.MapFrom ( q=> q.Enrollments.Count()))
                .ForMember ( d => d.Enrollments, o=> o.MapFrom ( q => q.Enrollments.Select( e => Mapper.Map<EnrollmentModel>(e) )));

            


        }
    }
}