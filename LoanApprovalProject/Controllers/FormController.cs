namespace LoanApprovalProject.Controllers
{
    using System;
    using System.Collections.Generic;
    using global::Models;
    using Manager.Interface;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class FormController:ControllerBase
    {
        private readonly IFormManager manager;

        public FormController(IFormManager manager)
        {
            this.manager = manager;
        }

        [HttpPost]
        [Route("AddToForm")]
        public IActionResult AddToForm([FromBody] FormListModel formData)
        {
            try
            {
                var result = this.manager.AddToForm(formData);
                if (result == true)
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "successfully submited" });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "unsuccessfull" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }
        [HttpGet]
        [Route("FormDetails")]
        public IActionResult GetFormDetails (int userId)
        {
            try
            {
                List<FormModel> result = this.manager.GetFormDetails(userId);
                if (result.Count>0)
                {
                    return this.Ok(new ResponseModel<List<FormModel>>() { Status = true, Message = "successfully submited",Data = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "unsuccessfull" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        [HttpGet]
        [Route("propertyDetails")]
        public IActionResult GetPropertyDetails(int userId,int formId)
        {
            try
            {
                List<PropertyModel> result = this.manager.GetPropertyDetails(userId, formId);
                if (result.Count > 0)
                {
                    return this.Ok(new ResponseModel<List<PropertyModel>>() { Status = true, Message = "successfully submited", Data = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "unsuccessfull" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }
    }
   

}
