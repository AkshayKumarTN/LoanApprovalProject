// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUserManager.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// ----------------------------------------------------------------------------------------------------------
namespace Manager.Interface
{
    using Models;

    /// <summary>
    /// Interface for manager function
    /// </summary>
    public interface IUserManager
    {
        /// <summary>
        /// Registers the specified user data.
        /// </summary>
        /// <param name="userData">The user data.</param>
        /// <returns>returns register model</returns>
        bool Register(RegisterModel userData);

        /// <summary>
        /// Logins the specified login data.
        /// </summary>
        /// <param name="loginData">The login data.</param>
        /// <returns>returns register model</returns>
        RegisterModel Login(LoginModel loginData);
    }
}
