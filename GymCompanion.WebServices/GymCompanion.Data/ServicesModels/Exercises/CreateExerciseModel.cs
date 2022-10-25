namespace GymCompanion.Data.ServicesModels.Exercises
{
    public class CreateExerciseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public int BodyPartId { get; set; }
    }
}
