using GymCompanion.Data.ServicesModels.Workouts;

namespace GymCompanion.Data.Models.Workouts
{
    public class GetUserWorkoutsModel
    {
        public List<WorkoutModel> Workouts { get; set; } = new();
    }
}
