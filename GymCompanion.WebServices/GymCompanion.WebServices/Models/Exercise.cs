using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymCompanion.WebServices.Models
{
    public partial class Exercise
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int BodyPartId { get; set; }
        public string? Description { get; set; }

        public virtual BodyPart BodyPart { get; set; } = null!;

        public virtual List<Workout> Workouts { get; set; } = null!;

        public virtual List<UserExercise> UserExercises { get; set; } = null!;
    }
}
