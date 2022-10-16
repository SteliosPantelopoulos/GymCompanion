namespace GymCompanion.Data.Models.Exercises
{
    public class ExerciseModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int BodyPartId { get; set; }
        public string? Description { get; set; }
    }
}
