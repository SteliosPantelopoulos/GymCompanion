using System;
using System.Collections.Generic;

namespace GymCompanion.WebServices.Models
{
    public partial class Workout
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int UserId { get; set; }
        public double Time { get; set; }
        public DateTime Date { get; set; }
        public virtual ICollection<Exercise> Exercises { get; set; }

        public virtual User User { get; set; }

    }
}
