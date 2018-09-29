using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickApp.ViewModels
{
    public class MarketingStudentVModel
    {
        public int Id { get; set; }
        public int? StudentId { get; set; }
        public string UserId { get; set; }
        public string NumberOfStudents { get; set; }
    }
}
