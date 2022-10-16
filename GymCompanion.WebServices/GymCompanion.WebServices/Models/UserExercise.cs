namespace GymCompanion.WebServices.Models
{
    public class UserExercise
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int ExerciseId { get; set; }


        public List<Set> Sets { get; set; }

        public virtual User User { get; set; }

        public virtual Exercise Exercise { get; set; }
    }
}
