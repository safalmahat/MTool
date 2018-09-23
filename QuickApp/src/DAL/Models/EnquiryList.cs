using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
   public class EnquiryList : AuditableEntity
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int ChannelId { get; set; }
    }
}

