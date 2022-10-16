using System;
using System.Collections.Generic;

namespace GymCompanion.WebServices.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public string Email { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Firstname { get; set; } = null!;
        public string Lastname { get; set; } = null!;
        public string Country { get; set; } = null!;
        public DateTime Birthday { get; set; }
        public DateTime RegistrationDate { get; set; }
        public ICollection<Workout> Workouts { get; set; }

        public virtual List<UserExercise> UserExercises { get; set; }
    }
}
