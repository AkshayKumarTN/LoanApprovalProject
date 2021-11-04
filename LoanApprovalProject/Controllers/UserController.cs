namespace LoanApprovalProject.Controllers
{
    using System;
    using global::Models;
    using Manager.Interface;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Controller for User API
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        /// <summary>
        /// The manager
        /// </summary>
        private readonly IUserManager manager;
        private readonly ILogger<UserController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="manager">The manager.</param>
        public UserController(IUserManager manager, ILogger<UserController> logger)
        {
            this.manager = manager;
            _logger = logger;
        }

        /// <summary>
        /// Registers the specified user data.
        /// </summary>
        /// <param name="userData">The user data.</param>
        /// <returns>Returns message and status</returns>
        [HttpPost]
        [Route("Register")]
        public IActionResult Register([FromBody] RegisterModel userData)
        {
            try
            {
                _logger.LogWarning("TRYING TO REGISTER !!!");
                var result = this.manager.Register(userData);
                if (result == true)
                {
                    _logger.LogWarning("Registration Successfull!!!!");
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Registration Successfull!" });
                }
                else
                {
                    _logger.LogWarning("Registration Unsuccessfull!!!");
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Registration Unsuccessfull!" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,ex.Message);
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// Logins the specified login data.
        /// </summary>
        /// <param name="loginData">The login data.</param>
        /// <returns>Returns message and status</returns>
        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromBody] LoginModel loginData)
        {
            try
            {
                _logger.LogWarning("TRYING TO LOGIN !!!");
                var result = this.manager.Login(loginData);
                if (result != null)
                {
                    _logger.LogWarning("LOGIN SUCCESS!!!");
                    return this.Ok(new { Status = true, Message = "Login Successful!!!", Data = result });
                }
                else
                {
                    _logger.LogWarning("LOGIN UNSUCCESS!!!");
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Login Unsuccessfull!" });
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
