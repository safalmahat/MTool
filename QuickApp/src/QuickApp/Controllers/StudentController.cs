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

            StudentRegistrationInfo registeredStudent = _unitOfWork.Students.GetAll().Where(sd => sd.PhoneNumber == item.PhoneNumber).FirstOrDefault();
            _unitOfWork.Students.Add(item);
            if (registeredStudent != null)
            {
                EnquiryList enquiryList = new EnquiryList
                {
                    StudentId = item.Id,
                    ChannelId = registeredStudent.ChannelId
                    
                };
                _unitOfWork.EnquiryList.Add(enquiryList);
            }
        
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
                    StudentRegistrationInfo duplicateRegistrationInfo =null;
                    if (workSheet.Cells[i, 7].Value != null && workSheet.Cells[i, 5].Value != null)
                    {
                         duplicateRegistrationInfo = _unitOfWork.Students.GetAll().Where(di => di.PhoneNumber == workSheet.Cells[i, 7].Value.ToString() && di.ChannelId == Convert.ToInt16(workSheet.Cells[i, 5].Value)).FirstOrDefault();
                    }
                    if (duplicateRegistrationInfo == null )
                    {
                        studentList.Add(new StudentRegistrationInfo
                        {
                            Token = workSheet.Cells[i, 1].Value != null ? workSheet.Cells[i, 1].Value.ToString() : string.Empty,
                            FirstName = workSheet.Cells[i, 2].Value != null ? workSheet.Cells[i, 2].Value.ToString() : string.Empty,
                            MiddleName = workSheet.Cells[i, 3].Value != null ? workSheet.Cells[i, 3].Value.ToString() : string.Empty,
                            LastName = workSheet.Cells[i, 4].Value != null ? workSheet.Cells[i, 4].Value.ToString() : string.Empty,
                            ChannelId = Convert.ToInt16(workSheet.Cells[i, 5].Value),
                            Address = workSheet.Cells[i, 6].Value != null ? workSheet.Cells[i, 6].Value.ToString() : string.Empty,
                            PhoneNumber = workSheet.Cells[i, 7].Value != null ? workSheet.Cells[i, 7].Value.ToString() : string.Empty,
                            Email = workSheet.Cells[i, 8].Value != null ? workSheet.Cells[i, 8].Value.ToString() : string.Empty,
                            Gender = Convert.ToInt16(workSheet.Cells[i, 9].Value),
                            City = workSheet.Cells[i, 10].Value != null ? workSheet.Cells[i, 10].Value.ToString() : string.Empty,
                            CompletedLevel = workSheet.Cells[i, 11].Value != null ? workSheet.Cells[i, 11].Value.ToString() : string.Empty,
                            CompletedFaculty = workSheet.Cells[i, 12].Value != null ? workSheet.Cells[i, 12].Value.ToString() : string.Empty,
                            IntrestedFaculty = workSheet.Cells[i, 13].Value != null ? workSheet.Cells[i, 13].Value.ToString() : string.Empty,
                            Percentage = workSheet.Cells[i, 14].Value != null ? workSheet.Cells[i, 14].Value.ToString() : string.Empty,
                        });
                    }
                }

                _unitOfWork.Students.AddRange(studentList);
                _unitOfWork.SaveChanges();
                List<StudentRegistrationInfo> studentRegistrationInfo = _unitOfWork.Students.GetAll().ToList();
                return Ok(studentRegistrationInfo);
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
