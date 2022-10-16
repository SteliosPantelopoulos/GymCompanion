using GymCompanion.Data;
using GymCompanion.Data.Models.Account;
using GymCompanion.Data.Models.General;
using GymCompanion.WebServices.DAL;
using GymCompanion.WebServices.Helpers;
using GymCompanion.WebServices.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace GymCompanion.WebServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseApiController
    {
        public AccountController(GymCompanionContext context) 
            : base(context)
        {
        }

        [HttpGet]
        [Route("Login")]
        public async Task<ActionResult> Login(string username, string password)
        {
            BooleanModel model = new();
            try
            {
                if (await CheckCredentials(username, password))
                {
                    model.Result = true;
                    return Ok(JsonConvert.SerializeObject(model));
                }
                else
                {
                    model.ApiResponseMessage = (int)Numerators.ApiResponseMessages.WrongCredentials;
                    model.Result = false;
                    return StatusCode(409, JsonConvert.SerializeObject(model));
                }
            }
            catch (Exception exception)
            {
                model.ExceptionMessage = exception;
                return StatusCode(500, JsonConvert.SerializeObject(model));
            }
        }

        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult> Register(string username, string password, string email, string firstname, string lastname, string country, string birthday, string registrationDate)
        {
            BooleanModel model = new();
            try
            {
                if(await CheckIfEmailExists(email))
                {
                    model.ApiResponseMessage = (int)Numerators.ApiResponseMessages.EmailIsUsed;
                    model.Result = false;
                    return StatusCode(409, JsonConvert.SerializeObject(model));
                }
                else if ( await CheckIfUsernameExists(username))
                {
                    model.ApiResponseMessage = (int)Numerators.ApiResponseMessages.UsernameIsUsed;
                    model.Result = false;
                    return StatusCode(409, JsonConvert.SerializeObject(model));
                }
                else
                {
                    User userToRegister = new User()
                    {
                        Username = username,
                        Password = password,
                        Email = email,
                        Firstname = firstname,
                        Lastname = lastname,
                        Country = country,
                        Birthday = DateTime.ParseExact(birthday, "dd/MM/yyyy", null),
                        RegistrationDate = DateTime.ParseExact(registrationDate, "dd/MM/yyyy", null)
                    };

                    await _context.Users.AddAsync(userToRegister);
                    await _context.SaveChangesAsync();

                    model.Result = true;
                    return Ok(JsonConvert.SerializeObject(model));
                }
            }
            catch (Exception exception)
            {
                model.ExceptionMessage = exception;
                return StatusCode(500, JsonConvert.SerializeObject(model));
            }
        }

        [HttpGet]
        [Route("GetAccountInfo")]
        public async Task<ActionResult> GetUserInfo(string username)
        {
            GetUserInfoModel model = new();
            try
            {
                User userToReturn = await GetUserByUsername(username);

                if(userToReturn!= null)
                {
                    model = new GetUserInfoModel()
                    {
                        UserInfo = ModelsAdapter.UserInfo(userToReturn)
                    };

                    return Ok(JsonConvert.SerializeObject(model));
                }
                else
                {
                    model.ApiResponseMessage = (int)Numerators.ApiResponseMessages.UserNotFound;
                    return StatusCode(409, JsonConvert.SerializeObject(model));
                }   
            }
            catch (Exception exception)
            {
                model.ExceptionMessage = exception;
                return StatusCode(500, JsonConvert.SerializeObject(model));
            }
        }

        [HttpPost]
        [Route("UpdateAccountInfo")]
        public async Task<ActionResult> UpdateAccountInfo(string username, string newEmail, string newFirstname, string newLastname, string newCountry, string newBirthday)
        {
            BooleanModel model = new();
            try
            {
                User userToUpdate = await GetUserByUsername(username);
                int emailUsages = await _context.Users.CountAsync(x => x.Email == newEmail);

                if (emailUsages != 0)
                {
                    model.ApiResponseMessage = (int)Numerators.ApiResponseMessages.EmailIsUsed;
                    model.Result = false;
                    return StatusCode(409, JsonConvert.SerializeObject(model));
                }
                else if (userToUpdate == null)
                {
                    model.ApiResponseMessage = (int)Numerators.ApiResponseMessages.UserNotFound;
                    model.Result = false;
                    return StatusCode(409, JsonConvert.SerializeObject(model));
                }
                else
                {
                    userToUpdate.Email = newEmail;
                    userToUpdate.Firstname = newFirstname;
                    userToUpdate.Lastname = newLastname;
                    userToUpdate.Country = newCountry;
                    userToUpdate.Birthday = DateTime.ParseExact(newBirthday, "dd/MM/yyyy", null);
                    _context.Users.Update(userToUpdate);
                    _context.SaveChanges();

                    model.Result = true;
                    return Ok(JsonConvert.SerializeObject(model));
                }
            }
            catch (Exception exception)
            {
                model.ExceptionMessage = exception;
                return StatusCode(500, JsonConvert.SerializeObject(model));
            }
        }

        [HttpDelete]
        [Route("DeleteAccount")]
        public async Task<ActionResult> DeleteAccount(string username)
        {
            BooleanModel model = new();
            try
            {
                if(await CheckIfUsernameExists(username))
                {
                    User userToDelete = await GetUserByUsername(username);
                    _context.Users.Remove(userToDelete);
                    await _context.SaveChangesAsync();

                    model.Result = true;
                    return Ok(JsonConvert.SerializeObject(model));
                }
                else
                {
                    model.ApiResponseMessage = (int)Numerators.ApiResponseMessages.UserNotFound;
                    model.Result = false;
                    return StatusCode(409, JsonConvert.SerializeObject(model));
                }
            }
            catch (Exception exception)
            {
                model.ExceptionMessage = exception;
                return StatusCode(500, JsonConvert.SerializeObject(model));
            }
        }

        [HttpPost]
        [Route("ChangeUsername")]
        public async Task<ActionResult> ChangeUsername(string oldUsername, string newUsername)
        {
            BooleanModel model = new();
            try
            {
                User userToChangeUsername = await GetUserByUsername(oldUsername);
                int newUsernameUsages = await _context.Users.CountAsync(X => X.Username == newUsername);

                if (newUsernameUsages != 0)
                {
                    model.ApiResponseMessage = (int)Numerators.ApiResponseMessages.UsernameIsUsed;
                    model.Result = false;
                    return StatusCode(409, JsonConvert.SerializeObject(model));
                }
                else if (userToChangeUsername == null)
                {
                    model.ApiResponseMessage = (int)Numerators.ApiResponseMessages.UserNotFound;
                    model.Result = false;
                    return StatusCode(409, JsonConvert.SerializeObject(model));
                }
                else
                {
                    userToChangeUsername.Username = newUsername;
                    await _context.SaveChangesAsync();

                    model.Result = true;
                    return Ok(JsonConvert.SerializeObject(model));
                }
            }
            catch (Exception exception)
            {
                model.ExceptionMessage = exception;
                return StatusCode(500, JsonConvert.SerializeObject(model));
            }
        }

        [HttpPost]
        [Route("ChangePassword")]
        public async Task<ActionResult> ChangePassword(string username, string oldPassword, string newPassword)
        {
            BooleanModel model = new();
            try
            {
                User userToChangePassword = await GetUserByUsername(username);

                if (userToChangePassword == null)
                {
                    model.ApiResponseMessage = (int)Numerators.ApiResponseMessages.UserNotFound;
                    model.Result = false;
                    return StatusCode(409, JsonConvert.SerializeObject(model));
                }
                else if (!await CheckCredentials(username, oldPassword))
                {
                    model.ApiResponseMessage = (int)Numerators.ApiResponseMessages.WrongCredentials;
                    model.Result = false;
                    return StatusCode(409, JsonConvert.SerializeObject(model));
                }
                else
                {
                    userToChangePassword.Password = newPassword;
                    await _context.SaveChangesAsync();

                    model.Result = true;
                    return Ok(JsonConvert.SerializeObject(model));
                }
            }
            catch (Exception exception)
            {
                model.ExceptionMessage = exception;
                return StatusCode(500, JsonConvert.SerializeObject(model));
            }
        }

        #region HelpingFunctions
        private async Task<User> GetUserByUsername(string username)
        {
            User user = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);

            return user;
        }

        private async Task<User> GetUserByEmail(string email)
        {
            User user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);

            return user;
        }

        private async Task<bool> CheckIfUsernameExists(string username)
        {
            return await GetUserByUsername(username) != null;
        }

        private async Task<bool> CheckIfEmailExists(string email)
        {
            return await GetUserByEmail(email) != null;
        }

        private async Task<bool> CheckCredentials(string username, string password)
        {
            User userToCheck = await GetUserByUsername(username);

            if (userToCheck != null && userToCheck.Password == password)
                return true;
            else
                return false;
        }

        #endregion
    }
}
