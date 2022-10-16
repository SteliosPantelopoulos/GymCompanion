namespace GymCompanion.Data.ServicesModels.Account
{
    public class UserInfoModel
    {
        public string Email { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Firstname { get; set; } = null!;
        public string Lastname { get; set; } = null!;
        public string Country { get; set; } = null!;
        public DateTime Birthday { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}
