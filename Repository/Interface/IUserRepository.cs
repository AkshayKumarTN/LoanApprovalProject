namespace Repository.Interface
{
    using Models;

    /// <summary>
    /// User  interface
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Registers the specified user data.
        /// </summary>
        /// <param name="userData">The user data.</param>
        /// <returns>Return </returns>
        public bool Register(RegisterModel userData);

        /// <summary>
        /// Logins the specified login data.
        /// </summary>
        /// <param name="loginData">The login data.</param>
        /// <returns></returns>
        RegisterModel Login(LoginModel loginData);

    }
}
