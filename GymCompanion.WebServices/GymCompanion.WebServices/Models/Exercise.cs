using System;
using System.Collections.Generic;

namespace GymCompanion.WebServices.Models
{
    public partial class Exercise
    {
        public Exercise()
        {
            Workouts = new HashSet<Workout>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int BodyPartId { get; set; }
        public string? Description { get; set; }

        public virtual BodyPart BodyPart { get; set; } = null!;
        public virtual ICollection<Workout> Workouts { get; set; }
    }
}
