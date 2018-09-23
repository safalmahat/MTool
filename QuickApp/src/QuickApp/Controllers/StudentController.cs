using DAL;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OfficeOpenXml;
using OpenIddict.Validation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace QuickApp.Controllers
{
    [Authorize(AuthenticationSchemes = OpenIddictValidationDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class StudentController:Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private IUnitOfWork _unitOfWork;
        readonly ILogger _logger;
        public StudentController(IUnitOfWork unitOfWork, ILogger<StudentController> logger, IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        [HttpPost]
        public IActionResult Post([FromBody]StudentRegistrationInfo item)
        {
            _unitOfWork.Students.Add(item);
            return Ok(item);
        }
        [HttpGet]
        public  IActionResult Get()
        {

            return Ok(_unitOfWork.Students.GetAll());
        }
        [HttpGet]
        [Route("ImportStudents")]
        public IActionResult ImportStudent()
        {
            string rootFolder = _hostingEnvironment.WebRootPath;
            string fileName = @"ImportStudentInfo.xlsx";
            FileInfo file = new FileInfo(Path.Combine(rootFolder, fileName));

            using (ExcelPackage package = new ExcelPackage(file))
            {
                ExcelWorksheet workSheet = package.Workbook.Worksheets["Sheet1"];
                int totalRows = workSheet.Dimension.Rows;

                List<StudentRegistrationInfo> studentList = new List<StudentRegistrationInfo>();

                for (int i = 2; i <= totalRows; i++)
                {
                    studentList.Add(new StudentRegistrationInfo
                    {
                        Token = workSheet.Cells[i, 1].Value.ToString(),
                        FirstName = workSheet.Cells[i, 2].Value.ToString(),
                        Address = workSheet.Cells[i, 3].Value.ToString(),
                        PhoneNumber = workSheet.Cells[i, 4].Value.ToString(),
                        Gender = Convert.ToInt16( workSheet.Cells[i, 5].Value),
                        ChannelId = Convert.ToInt16(workSheet.Cells[i, 6].Value),

                    });
                }

                _unitOfWork.Students.AddRange(studentList);
                _unitOfWork.SaveChanges();
                return Ok(studentList);
            }
        }
        
        [HttpGet]
        [Route("ExportStudent")]
        public string ExportStudent()
        {
            string rootFolder = _hostingEnvironment.WebRootPath;
            string fileName = @"ExportStudents.xlsx";

            FileInfo file = new FileInfo(Path.Combine(rootFolder, fileName));

            using (ExcelPackage package = new ExcelPackage(file))
            {

                IList<StudentRegistrationInfo> studentList = _unitOfWork.Students.GetAll().ToList();

                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Student");
                int totalRows = studentList.Count();

                worksheet.Cells[1, 1].Value = "Token";
                worksheet.Cells[1, 2].Value = "FirstName";
                worksheet.Cells[1, 3].Value = "Address";
                worksheet.Cells[1, 4].Value = "PhoneNumber";
                worksheet.Cells[1, 5].Value = "Gender";
                worksheet.Cells[1, 6].Value = "ChannelId";
                int i = 0;
                for (int row = 2; row <= totalRows + 1; row++)
                {
                    worksheet.Cells[row, 1].Value = studentList[i].Id;
                    worksheet.Cells[row, 2].Value = studentList[i].FirstName;
                    worksheet.Cells[row, 3].Value = studentList[i].Email;
                    worksheet.Cells[row, 4].Value = studentList[i].PhoneNumber;
                    worksheet.Cells[row, 5].Value = studentList[i].Gender;
                    worksheet.Cells[row, 6].Value = studentList[i].ChannelId;
                    i++;
                }

                package.Save();

            }

            return " Student list has been exported successfully";
        }

    }
}
