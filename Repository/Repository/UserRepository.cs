namespace Repository.Repository
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;
    using global::Repository.Context;
    using global::Repository.Interface;
    using Microsoft.Extensions.Configuration;
    using Models;
    using StackExchange.Redis;

    /// <summary>
    /// User repository implements interface
    /// </summary>
    /// <seealso cref="Repository.Interface.IUserRepository" />
    public class UserRepository : IUserRepository
    {
        private readonly UserContext userContext;
        public IConfiguration configuration { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public UserRepository(UserContext userContext, IConfiguration configuration)
        {
            this.userContext = userContext;
            this.configuration = configuration;
        }

        

        /// <summary>
        /// Registers the specified user data.
        /// </summary>
        /// <param name="userData">The user data.</param>
        /// <returns>
        /// Return model
        /// </returns>
        /// <exception cref="System.Exception">Returns exception message</exception>
        public bool Register(RegisterModel userData)
        {
            try
            {
                if (userData != null)
                {
                    userData.Password = EncryptPassWord(userData.Password);
                    this.userContext.Users.Add(userData);
                    this.userContext.SaveChanges();
                    return true;
                }

                return false;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Encrypts the pass word.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <returns>Return string</returns>
        public string EncryptPassWord(string password)
        {
            var passwordInBytes = Encoding.UTF8.GetBytes(password);
            string encodePassword = Convert.ToBase64String(passwordInBytes);
            return encodePassword;
        }

        /// <summary>
        /// Logins the specified login data.
        /// </summary>
        /// <param name="loginData">The login data.</param>
        /// <returns>Returns model</returns>
        /// <exception cref="System.Exception">
        /// EmailId does not exist
        /// or
        /// Password does not match
        /// or
        /// </exception>
        public RegisterModel Login(LoginModel userLoginData)
        {
            try
            {
                string encodedPassword = EncryptPassWord(userLoginData.Password);
                var loginUser = this.userContext.Users.Where(x => x.EmailId == userLoginData.EmailId && x.Password == encodedPassword).FirstOrDefault();

                if (loginUser != null)
                { 
                    loginUser.Password = null;
                    return loginUser;
                }

                return null;
            }
            catch (ArgumentNullException ex)
            {
                throw new ArgumentNullException(ex.Message);
            }
        }
    }
}