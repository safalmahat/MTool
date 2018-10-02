using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class MarketingStudentList : AuditableEntity
    {
        public int MarketingStudentListId { get; set; }
        public int? StudentId { get; set; }
        public string UserId { get; set; }
    }
}


