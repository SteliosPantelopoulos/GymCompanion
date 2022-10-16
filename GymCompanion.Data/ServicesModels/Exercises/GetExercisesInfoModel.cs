using GymCompanion.Data.Models.General;

namespace GymCompanion.Data.Models.Exercises
{
    public class GetExercisesInfoModel : BaseModel
    {
        public List<ExerciseModel> Exercises { get; set; } = new ();
    }
}
