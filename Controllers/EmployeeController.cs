using EmployeeApp.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EmployeeApp.Model.Request;
using EmployeeApp.DAL.IRepositories;
using Newtonsoft.Json;
using EmployeeApp.Model.Utils;
using EmployeeApp.Model.Responce;
using Azure;
using Microsoft.Identity.Client;
  

namespace EmployeeAppService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]         
    public class EmployeeController : ControllerBase
    {
        //ApiResponseConstants response = null;
           

        //var errorResp = new ErrorResponse();

        // ApiResponseStatus apiResponse = null;
        private readonly IEmployee _employeeController;

        private readonly ILogger<EmployeeController> _logger;
        public EmployeeController(IEmployee employee, ILogger<EmployeeController> logger)
        {   

            _employeeController = employee;
            _logger = logger;
        }


        [HttpPost("AddEmployee")]
        public IActionResult AddEmployee([FromBody] EmployeeData employee)
        {
            _logger.LogInformation("AddEmployee" + JsonConvert.SerializeObject(employee));
            if (employee == null)
            {
                return BadRequest("Employee not Available");
            }
            _employeeController.AddEmployee(employee);

            return Ok(employee);

            //return CreatedAtRoute("Get",

            //    new {id=  customer.Id},customer);
        }

        [HttpPost("AddRoles")]
     public IActionResult AddRoles([FromBody] Role role)
        {
            _logger.LogInformation("AddRoles" + JsonConvert.SerializeObject(role));
            if (role == null)
            {
                return BadRequest("Role not Available");
            }
            _employeeController.AddRoles(role);

            return Ok(role);
        }



        [HttpGet("GetEmployee")]

        public IActionResult GetEmployee()
        {
            IEnumerable<EmployeeData> customers = _employeeController.GetEmployee();

            return Ok(customers);
        }
        [HttpGet("EmployeeData")]
        public IActionResult EmployeeData(int? EMPId = null)
        {
            var response = new ApiResponseConstants();
            var  resdata = new ApiResponse();


            var EMPData = _employeeController.EmployeeData(EMPId);
            if (EMPData == null  || EMPData.Count ==0)
            {
                resdata.Data = new { EmployeeData = EMPData };
                resdata.StatusCode = (int)ApiResponseStatus.NotFound;
                resdata.Message = response.GetResponceMessage(resdata.StatusCode);

                return NotFound(resdata);

            }
            else
            {
                
                resdata.Data = new { EmployeeData = EMPData };
                resdata.StatusCode = (int)ApiResponseStatus.Success;
                resdata.Message = response.GetResponceMessage(resdata.StatusCode);





                //response.Data = new { Employees = EMPData };
                // apiResponse.Data =  EMPData;
                // apiResponse.StatusCode = (int)ApiResponseStatus.Success;
                // apiResponse.Message = response.GetResponceMessage((int)apiResponse.StatusCode);


                //apiResponse.Data = EMPData;
                //apiResponse.StatusCode = (int)ApiResponseStatus.Success;
                //apiResponse.Message = response.GetResponceMessage((int)apiResponse.StatusCode);
                return Ok(resdata);
               
            }
         
        }



        [HttpGet("EmployeeInfo")]
        public IActionResult EmployeeInfo ()
        {
            var response = new ApiResponseConstants();
            var resdata = new ApiResponse();


            var empinfo = _employeeController.EmployeeInfo();
            if (empinfo == null || empinfo.Count == 0)
            {
                resdata.Data = new { EmployeeDataInfo = empinfo };
                resdata.StatusCode = (int)ApiResponseStatus.NotFound;
                resdata.Message = response.GetResponceMessage(resdata.StatusCode);

                return NotFound(resdata);

            }
            else
            {

                resdata.Data = new { EmployeeDataInfo = empinfo };


                //resdata.Data = new
                //{
                //    Data = new EmployeeDataResponce_v1
                //    {
                //        EmployeeRoleInfo = empinfo
                //    }
                //};

                //resdata.Data = new EmployeeDataResponce_v1
                //{
                //      = new DataWrapper
                //    {
                //        EmployeeData = empinfo
                //    },
                //};

                    resdata.StatusCode = (int)ApiResponseStatus.Success;
                resdata.Message = response.GetResponceMessage(resdata.StatusCode);





                //response.Data = new { Employees = EMPData };
                // apiResponse.Data =  EMPData;
                // apiResponse.StatusCode = (int)ApiResponseStatus.Success;
                // apiResponse.Message = response.GetResponceMessage((int)apiResponse.StatusCode);


                //apiResponse.Data = EMPData;
                //apiResponse.StatusCode = (int)ApiResponseStatus.Success;
                //apiResponse.Message = response.GetResponceMessage((int)apiResponse.StatusCode);
                return Ok(resdata);

            }


        }

        [HttpGet("GetEmployeeByID")]
        public IActionResult GetEmployeeByID(int EMPId)
        {
            EmployeeData employee = _employeeController.GetEmployeeById(EMPId);
            if (employee == null)
            {
                return NotFound("Customer Can't Found");
            }
            return Ok(employee);
        }

        [HttpPut("UpdateEmployee")]
        public IActionResult UpdateEmployee(int EMPId, [FromBody] EmployeeData employee)
        {
            if (employee == null)
            {
                return BadRequest("Customer Not Available");

            }
            EmployeeData employeetoupdate = _employeeController.GetEmployeeById(EMPId);
            if (employeetoupdate == null)
            {
                return BadRequest("Customer Not Updated");
            }
            _employeeController.UpdateEmployee(employeetoupdate, employee);
            return Ok(employee);

        }

        [HttpDelete("DeleteEmployee")]
        public IActionResult DeleteEmployee(int EMPId)
        {
            EmployeeData employee = _employeeController.GetEmployeeById(EMPId);
            if (employee == null)
            {
                return NotFound("Data Not Found");
            }
            _employeeController.DeleteEmployee(employee);
            return Ok(employee);

        }


        [HttpPost("AddCropData")]
        public IActionResult AddCropData( CropMaster cropMaster)
        {
            
            _employeeController.AddCropData(cropMaster);
            return Ok(cropMaster);
            
        }


        [HttpPost("AddProduct")]
        public IActionResult AddProdcut(ProductMaster productMaster)
        {
            _employeeController.AddProduct(productMaster);
            return Ok(productMaster);
        }

        [HttpGet("GetCropData")]
        public IActionResult GetCropData()
        {
            var cropData = _employeeController.GetCropData();
            if (cropData == null)
            {
                return NotFound("Crop Data Not Found");
            }
            return Ok(cropData);
        }

        [HttpGet("GetCropDataById")]
        public IActionResult GetCropDataById(int Id)
        {
           CropMaster cropMaster = _employeeController.GetCropDataById(Id);
            return Ok(cropMaster);
        }

        [HttpDelete("GetCropDataDeleteByID")]
        public IActionResult GetCropDataDeleteByID( int Id)
        {
            CropMaster cropMaster = _employeeController.GetCropDataById(Id);
            if (cropMaster == null)
            {
                return NotFound("Crop Data Not Found");
            }
          var Messgae =  _employeeController.GetCropDataDeleteByID(cropMaster);
            return Ok(Messgae);
        }
        [HttpGet("GetCrop_ProductData")]
        public IActionResult GetCrop_ProductData()
        {
         
            var resdata = new ApiResponse();
            var response = new ApiResponseConstants();

            var res = _employeeController.GetCrop_ProductData();
            if(res == null || res.Count == 0)
            {
                resdata.Data = new { Crop_ProductData = res };
                resdata.StatusCode = (int)ApiResponseStatus.NotFound;
                resdata.Message = response.GetResponceMessage(resdata.StatusCode);

                return NotFound(resdata);
            }
            else 
            {
                resdata.Data = new { Crop_ProductData = res };
                resdata.StatusCode = (int)ApiResponseStatus.Success;
                resdata.Message = response.GetResponceMessage(resdata.StatusCode);
            }
            return Ok(resdata); 


            
        }

        [HttpPost("AddSeasonData")]
        public IActionResult AddSeasonData(Season_Master season_Master)
        {
            _employeeController.AddSeasonData(season_Master);
            return Ok(season_Master);
            return Ok(season_Master);
        }

    }
}
