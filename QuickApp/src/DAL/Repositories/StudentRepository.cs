using DAL.Models;
using DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;
    }
}
