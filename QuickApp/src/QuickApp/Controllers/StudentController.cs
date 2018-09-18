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
        public StudentController(IUnitOfWork unitOfWork, ILogger<CustomerController> logger, IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        [HttpPost]
        public void Post([FromBody]StudentRegistrationInfo item)
        {
            _unitOfWork.Students.Add(item);
        }
        [HttpGet]
        public  IActionResult Get()
        {

            return Ok(_unitOfWork.Students.GetAll());
        }
        [HttpGet]
        [Route("ImportStudents")]
        public IActionResult ImportCustomer()
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

                    });
                }

                _unitOfWork.Students.AddRange(studentList);
                _unitOfWork.SaveChanges();
                return Ok(studentList);
            }
        }
        .
        [HttpGet]
        [Route("ExportCustomer")]
        public string ExportCustomer()
        {
            string rootFolder = _hostingEnvironment.WebRootPath;
            string fileName = @"ExportCustomers.xlsx";

            FileInfo file = new FileInfo(Path.Combine(rootFolder, fileName));

            using (ExcelPackage package = new ExcelPackage(file))
            {

                IList<StudentRegistrationInfo> customerList = _unitOfWork.Students.GetAll().ToList();

                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Customer");
                int totalRows = customerList.Count();

                worksheet.Cells[1, 1].Value = "Customer ID";
                worksheet.Cells[1, 2].Value = "Customer Name";
                worksheet.Cells[1, 3].Value = "Customer Email";
                worksheet.Cells[1, 4].Value = "customer Country";
                int i = 0;
                for (int row = 2; row <= totalRows + 1; row++)
                {
                    worksheet.Cells[row, 1].Value = customerList[i].Id;
                    worksheet.Cells[row, 2].Value = customerList[i].FirstName;
                    worksheet.Cells[row, 3].Value = customerList[i].Email;
                    worksheet.Cells[row, 4].Value = customerList[i].PhoneNumber;
                    i++;
                }

                package.Save();

            }

            return " Customer list has been exported successfully";
        }

    }
}
