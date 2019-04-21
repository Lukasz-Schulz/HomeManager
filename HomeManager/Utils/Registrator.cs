using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Home;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HomeManager.Controllers
{
    public class Registrator
    {
        public IActionResult Register(Dictionary<string,string> profile)
        {
            Models.Security security = new Models.Security();
            DatabaseConnection connection = new DatabaseConnection();
            try
            {
                if (ValidateCredentials(profile))
                {
                    connection.AddUserToDatabase(profile["email"], security.Encrypt(profile["password"]));

                    ConcreteOwnerBuilder ownerBuilder = new ConcreteOwnerBuilder();
                    ownerBuilder.AddEmail(profile["email"]);
                    ownerBuilder.AddFirstName(profile["firstName"]);
                    ownerBuilder.AddSecondName(profile["secondName"]);
                    Random random = new Random();
                    ownerBuilder.AddID(random.Next(100, 999));

                    connection.AddOwnerToDatabase(ownerBuilder.BuildOwner());
                    return new StatusCodeResult(StatusCodes.Status201Created);
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (SqlException)
            {
                return new StatusCodeResult(StatusCodes.Status409Conflict);
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status400BadRequest);
            }
        }

        private bool ValidateCredentials(Dictionary<string, string> profile)
        {
            try
            {
                bool email = EmailIsOk(profile["email"]);
                bool pwd = PasswordIsOk(profile["password"]);
                bool fname = profile["firstName"].Length >= 3;
                bool sname = profile["secondName"].Length >= 3;

                if (email && pwd && fname && sname)
                {
                    return true;
                }
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        private bool EmailIsOk(string email)
        {
            if (email.Length >= 8 && 
                email.Contains("@") && 
                (email.Substring(email.Length - 3,1).Equals(".") | email.Substring(email.Length - 4,1).Equals(".")) &&
                !email.Substring(0, 3).Contains("@"))
            {
                return true;
            }
            else
                return false;
        }

        private bool PasswordIsOk(string password)
        {
            if (password.Length >= 8 &&
                password.Any(char.IsDigit) &&
                password.Any(char.IsLetter) &&
                password.Any(char.IsUpper) &&
                password.Any(char.IsLower) &&
                !password.Any(char.IsWhiteSpace))
            {
                return true;
            }
            else
                return false;
        }

        //private void ConfirmEmail(string email)
        //{            
        //    string confirmationUrl = Program.TheRegistrationConfirmation.CreateConfirmationUrl(email);
        //    var emailSender = new Utils.EmailSender();
        //    emailSender.SendEmailAsync(email, confirmationUrl);
        //}
    }
}
