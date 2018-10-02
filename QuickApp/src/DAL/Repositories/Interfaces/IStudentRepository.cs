using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repositories.Interfaces
{
   public interface IStudentRepository : IRepository<StudentRegistrationInfo>
    {
        IEnumerable<StudentRegistrationInfo> GetStudentRegistrationInfos(string userID);
    }
}
