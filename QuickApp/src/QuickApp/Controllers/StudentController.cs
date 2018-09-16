using DAL;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OpenIddict.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickApp.Controllers
{
    [Authorize(AuthenticationSchemes = OpenIddictValidationDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class StudentController:Controller
    {
        private IUnitOfWork _unitOfWork;
        readonly ILogger _logger;
        public StudentController(IUnitOfWork unitOfWork, ILogger<CustomerController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        // POST api/values
        [HttpPost]
        public void Post([FromBody]StudentRegistrationInfo item)
        {
            _unitOfWork.Students.Add(item);
        }
        // POST api/values
        [HttpGet]
        public  IActionResult Get()
        {

            return Ok(_unitOfWork.Students.GetAll());
        }
    }
}
