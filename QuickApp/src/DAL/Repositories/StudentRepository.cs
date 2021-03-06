﻿using DAL.Models;
using DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class StudentRepository :
    Repository<StudentRegistrationInfo>, IStudentRepository
    {
        public StudentRepository(DbContext context) : base(context)
        { }

        public override void Add(StudentRegistrationInfo item)
        {
            _appContext.StudentRegistrationInfo.Add(item);
            _appContext.SaveChanges();
        }


        public IEnumerable<StudentRegistrationInfo> GetStudentRegistrationInfos(string userID)
        {
            var studentRegistrationInfo = (from p in _appContext.StudentRegistrationInfo
                                           join e in _appContext.MarketingStudentList
                                           on p.Id equals e.StudentId
                                           where e.UserId == userID
                                           select p).ToList();
            return studentRegistrationInfo;
        }

        public IEnumerable<int> GetUnregisteredStudentsID(int toAssignNumber)
        {
            IEnumerable<int> registeredStudent = (from ep in _appContext.StudentRegistrationInfo
                                                  join e in _appContext.MarketingStudentList on ep.Id equals e.StudentId into gj
                                                  from x in gj.DefaultIfEmpty()
                                                  where x.StudentId == null
                                                  select ep.Id
             ).Take(toAssignNumber);
            return registeredStudent;

        }

        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;
    }
}
