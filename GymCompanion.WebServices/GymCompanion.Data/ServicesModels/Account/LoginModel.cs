using GymCompanion.Data.Models.General;
using System.ComponentModel.DataAnnotations;

namespace GymCompanion.Data.ServicesModels.Account
{
    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
