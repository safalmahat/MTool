using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
  public  class StudentRegistrationInfo : AuditableEntity
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public int Gender { get; set; }
        public string CompletedLevel { get; set; }
        public string CompletedFaculty { get; set; }
        public string IntrestedFaculty { get; set; }
        public string Percentage { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}
