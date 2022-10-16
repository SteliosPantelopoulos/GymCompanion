namespace GymCompanion.WebServices.Models
{
    public class Set
    {
        public int Id { get; set; }
        public double Kilograms { get; set; }
        public int Reps { get; set; }

        public int UserExerciseId { get; set; }

        public virtual UserExercise UserExercise { get; set; }

    }
}
