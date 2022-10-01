using System;
using System.Collections.Generic;

namespace GymCompanion.WebServices.Models
{
    public partial class BodyPart
    {
        public BodyPart()
        {
            Exercises = new HashSet<Exercise>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Exercise> Exercises { get; set; }
    }
}
