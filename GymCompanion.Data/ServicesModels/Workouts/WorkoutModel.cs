using GymCompanion.Data.Models.Exercises;

namespace GymCompanion.Data.ServicesModels.Workouts
{
    public class WorkoutModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public double Time { get; set; }
        public DateTime Date { get; set; }

        public List<ExerciseModel> Exercises { get; set; } = new();
    }
}
