using DAL.Models;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repositories
{
   public class EnquiryListRepository :
    Repository<EnquiryList>, IEnquiryListRepository
    {
        public EnquiryListRepository(DbContext context) : base(context)
        { }

        public override void Add(EnquiryList item)
        {
            _appContext.EnquiryList.Add(item);
            _appContext.SaveChanges();
        }
        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;
    }
}