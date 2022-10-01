using GymCompanion.Data.Models.Exercises;

namespace GymCompanion.Data.Models.Workouts
{
    public class GetUserWorkoutsModel
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public int UserId { get; set; }

        public double Time { get; set; }

        public DateTime Date { get; set; }

        public List<GetExerciseInfoModel> Exercises { get; set; } = new List<GetExerciseInfoModel>();
    }
}
