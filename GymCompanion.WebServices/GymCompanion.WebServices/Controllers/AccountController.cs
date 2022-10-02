using GymCompanion.Data;
using GymCompanion.Data.Models.Account;
using GymCompanion.WebServices.DAL;
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
            try
            {
                if (await CheckCredentials(username, password))
                    return Ok(true);
                else
                    return StatusCode(409, Numerators.ApiResponseMessages.WrongCredentials);
            }
            catch (Exception exception)
            {
                return StatusCode(500, exception);
            }
        }

        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult> Register(string username, string password, string email, string firstname, string lastname, string country, string birthday, string registrationDate)
        {
            try
            {
                if(await CheckIfEmailExists(email))
                    return StatusCode(409, Numerators.ApiResponseMessages.EmailIsUsed);
                else if ( await CheckIfUsernameExists(username))
                    return StatusCode(409, Numerators.ApiResponseMessages.UsernameIsUsed);
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

                    return Ok(true);
                }
            }
            catch (Exception exception)
            {
                return StatusCode(500, exception);
            }
        }

        [HttpGet]
        [Route("GetAccountInfo")]
        public async Task<ActionResult> GetAccountInfo(string username)
        {
            try
            {
                User user = await GetUserByUsername(username);

                if(user!= null)
                {
                    GetAccountInfoModel model = new GetAccountInfoModel()
                    {
                        Username = user.Username,
                        Email = user.Email,
                        Firstname = user.Firstname,
                        Lastname = user.Lastname,
                        Country = user.Country,
                        Birthday = user.Birthday,
                        RegistrationDate = user.RegistrationDate
                    };

                    return Ok(JsonConvert.SerializeObject(model));
                }
                else
                {
                    return StatusCode(409, Numerators.ApiResponseMessages.UserNotFound);
                }   
            }
            catch (Exception exception)
            {
                return StatusCode(500, exception);
            }
        }

        [HttpPost]
        [Route("UpdateAccountInfo")]
        public async Task<ActionResult> UpdateAccountInfo(string username, string newEmail, string newFirstname, string newLastname, string newCountry, string newBirthday)
        {
            try
            {
                User userToUpdate = await GetUserByUsername(username);

                int emailUsages = await _context.Users.CountAsync(x => x.Email == newEmail);

                if (emailUsages != 0)
                    return StatusCode(409, Numerators.ApiResponseMessages.EmailIsUsed);
                else if (userToUpdate == null)
                    return StatusCode(409, Numerators.ApiResponseMessages.UserNotFound);
                else
                {
                    userToUpdate.Email = newEmail;
                    userToUpdate.Firstname = newFirstname;
                    userToUpdate.Lastname = newLastname;
                    userToUpdate.Country = newCountry;
                    userToUpdate.Birthday = DateTime.ParseExact(newBirthday, "dd/MM/yyyy", null);

                    _context.Users.Update(userToUpdate);
                    _context.SaveChanges();

                    return Ok(true);
                }
            }
            catch (Exception exception)
            {
                return StatusCode(500, exception);
            }
        }

        [HttpDelete]
        [Route("DeleteAccount")]
        public async Task<ActionResult> DeleteAccount(string username)
        {
            try
            {
                if(await CheckIfUsernameExists(username))
                {
                    User userToDelete = await GetUserByUsername(username);
                    _context.Users.Remove(userToDelete);

                    await _context.SaveChangesAsync();

                    return Ok(true);
                }
                else
                {
                    return StatusCode(409, Numerators.ApiResponseMessages.UserNotFound);
                }
            }
            catch (Exception exception)
            {
                return StatusCode(500, exception);
            }
        }

        [HttpPost]
        [Route("ChangeUsername")]
        public async Task<ActionResult> ChangeUsername(string oldUsername, string newUsername)
        {
            try
            {
                User userToChangeUsername = await GetUserByUsername(oldUsername);

                int newUsernameUsages = await _context.Users.CountAsync(X => X.Username == newUsername);

                if (newUsernameUsages != 0)
                    return StatusCode(409, Numerators.ApiResponseMessages.UsernameIsUsed);
                else if (userToChangeUsername == null)
                    return StatusCode(409, Numerators.ApiResponseMessages.UserNotFound);
                else
                {
                    userToChangeUsername.Username = newUsername;

                    await _context.SaveChangesAsync();
                    return Ok(true);
                }
            }
            catch (Exception exception)
            {
                return StatusCode(500, exception);
            }
        }

        [HttpPost]
        [Route("ChangePassword")]
        public async Task<ActionResult> ChangePassword(string username, string oldPassword, string newPassword)
        {
            try
            {
                User userToChangePassword = await GetUserByUsername(username);

                if (userToChangePassword == null)
                    return StatusCode(409, Numerators.ApiResponseMessages.UserNotFound);
                else if (!await CheckCredentials(username, oldPassword))
                    return StatusCode(409, Numerators.ApiResponseMessages.WrongCredentials);
                else
                {
                    userToChangePassword.Password = newPassword;

                    await _context.SaveChangesAsync();
                    return Ok(true);
                }
            }
            catch (Exception exception)
            {
                return StatusCode(500, exception);
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
