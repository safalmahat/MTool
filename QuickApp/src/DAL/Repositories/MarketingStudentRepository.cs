using DAL.Models;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repositories
{
   public class MarketingStudentRepository:
    Repository<MarketingStudentList>, IMarketingStudentRepository
    {
        public MarketingStudentRepository(DbContext context) : base(context)
        { }

        public override void Add(MarketingStudentList item)
        {
            _appContext.MarketingStudentList.Add(item);
            _appContext.SaveChanges();
        }
        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;
    }
}