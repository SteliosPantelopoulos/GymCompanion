
namespace GymCompanion.Data.ServicesModels.Exercises
{
    public class UpdateExerciseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public int BodyPartId { get; set; }
    }
}
