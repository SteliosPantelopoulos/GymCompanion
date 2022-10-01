using System;
using System.Collections.Generic;

namespace GymCompanion.WebServices.Models
{
    public partial class Workout
    {
        public Workout()
        {
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int UserId { get; set; }
        public double Time { get; set; }
        public DateTime Date { get; set; }
        public int? Exercises { get; set; }

        public virtual Exercise? ExercisesNavigation { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
