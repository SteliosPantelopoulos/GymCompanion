using System.ComponentModel.DataAnnotations;

namespace GymCompanion.Data.ServicesModels.Account
{
    public class RegisterModel
    {
        public string Username { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Country { get; set; }

        public DateTime Birthday { get; set; }
    }
}
