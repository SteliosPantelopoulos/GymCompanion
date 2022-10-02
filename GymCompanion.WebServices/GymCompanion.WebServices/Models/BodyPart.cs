using MessagePack;
using System;
using System.Collections.Generic;

namespace GymCompanion.WebServices.Models
{
    public partial class BodyPart
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public ICollection<Exercise> Exercises { get; set; }

    }
}
