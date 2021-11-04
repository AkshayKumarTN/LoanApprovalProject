namespace LoanApprovalProject.Controllers
{
    using System;
    using System.Collections.Generic;
    using global::Models;
    using Manager.Interface;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    [ApiController]
    [Route("api/[controller]")]
    public class FormController:ControllerBase
    {
        private readonly IFormManager manager;
        private readonly ILogger<UserController> _logger;


        public FormController(IFormManager manager, ILogger<UserController> logger)
        {
            this.manager = manager;
            _logger = logger;
        }

        [HttpPost]
        [Route("AddToForm")]
        public IActionResult AddToForm([FromBody] FormListModel formData)
        {
            try
            {
                _logger.LogWarning("TRYING TO ADD LOAN FORM !!!");
                var result = this.manager.AddToForm(formData);
                if (result == true)
                {
                    _logger.LogWarning("Submited Form Successfull!!!!");
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "successfully submited" });
                }
                else
                {
                    _logger.LogWarning("Submited Form Unsuccessfull!!!");
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "unsuccessfull" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }
        [HttpGet]
        [Route("FormDetails")]
        public IActionResult GetFormDetails (int userId)
        {
            try
            {
                _logger.LogWarning("TRYING TO RETRIVE FORMS !!!");
                List<FormModel> result = this.manager.GetFormDetails(userId);
                if (result.Count>0)
                {
                    _logger.LogWarning("Retrived Forms Successfull!!!!");
                    return this.Ok(new ResponseModel<List<FormModel>>() { Status = true, Message = "successfully",Data = result });
                }
                else
                {
                    _logger.LogWarning("Retrived Forms Unsuccessfull!!!");
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "unsuccessfull" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        [HttpGet]
        [Route("propertyDetails")]
        public IActionResult GetPropertyDetails(int userId,int formId)
        {
            try
            {
                _logger.LogWarning("TRYING TO Get Property Details !!!");
                List<PropertyModel> result = this.manager.GetPropertyDetails(userId, formId);
                if (result.Count > 0)
                {
                    _logger.LogWarning("Property Details Retrived Successfull!!!!");
                    return this.Ok(new ResponseModel<List<PropertyModel>>() { Status = true, Message = "successfully", Data = result });
                }
                else
                {
                    _logger.LogWarning("Property Details Retrived Unsuccessfull!!!");
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "unsuccessfull" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }
    }
   

}
