using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace GymCompanion.WebServices.Models
{
    public partial class User : IdentityUser
    {
        public string Firstname { get; set; } = null!;
        public string Lastname { get; set; } = null!;
        public string Country { get; set; } = null!;
        public DateTime Birthday { get; set; }
        public DateTime RegistrationDate { get; set; }
        public ICollection<Workout>? Workouts { get; set; }

        public virtual List<UserExercise>? UserExercises { get; set; }
    }
}
