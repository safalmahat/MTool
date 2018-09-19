using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class Channel : AuditableEntity
    {

        public int Id { get; set; }
        public int ChannelName { get; set; }
      
     
    }
}
