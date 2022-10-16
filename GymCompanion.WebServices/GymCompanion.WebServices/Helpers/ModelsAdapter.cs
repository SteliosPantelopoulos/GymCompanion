using GymCompanion.Data.Models.BodyParts;
using GymCompanion.Data.Models.Exercises;
using GymCompanion.Data.ServicesModels.Account;
using GymCompanion.Data.ServicesModels.Workouts;
using GymCompanion.WebServices.Models;
using Microsoft.EntityFrameworkCore;

namespace GymCompanion.WebServices.Helpers
{
    /// <summary>
    /// Transform data from type GymCompanion.WebServices.Models to type GymCompanion.Data.ServicesModels
    /// </summary>
    public class ModelsAdapter
    {
        public static UserInfoModel UserInfo(User user)
        {
            UserInfoModel userInfoModel = new()
            {
                Username = user.Username,
                Email = user.Email,
                Password = user.Password,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Country = user.Country,
                Birthday = user.Birthday,
                RegistrationDate = user.RegistrationDate
            };

            return userInfoModel;
        }
        public static BodyPartModel BodyPart(BodyPart bodyPart)
        {
            BodyPartModel bodyPartModel = new()
            {
                Id = bodyPart.Id,
                Name = bodyPart.Name
            };

            return bodyPartModel;
        }
        public static List<BodyPartModel> BodyParts(List<BodyPart> bodyParts)
        {
            List<BodyPartModel> bodyPartsModels = new();

            foreach (BodyPart bodyPart in bodyParts)
            {
                bodyPartsModels.Add(new BodyPartModel()
                {
                    Id = bodyPart.Id,
                    Name = bodyPart.Name
                });
            }

            return bodyPartsModels;
        }

        public static ExerciseModel Exercise(Exercise exercise)
        {
            ExerciseModel exerciseModel = new()
            {
                Id = exercise.Id,
                Name = exercise.Name,
                BodyPartId = exercise.BodyPartId,
                Description = exercise.Description
            };

            return exerciseModel;
        }

        public static List<ExerciseModel> Exercises(List<Exercise> exercises)
        {
            List<ExerciseModel> exerciseModels = new();

            foreach (Exercise exercise in exercises)
            {
                exerciseModels.Add(new ExerciseModel()
                {
                    Id = exercise.Id,
                    Name = exercise.Name,
                    BodyPartId = exercise.BodyPartId,
                    Description = exercise.Description
                });
            }

            return exerciseModels;
        }

        public static WorkoutModel Workout(Workout workout, List<Exercise> exercises)
        {
            WorkoutModel workoutModel = new()
            {
                Id = workout.Id,
                Name = workout.Name,
                UserId = workout.UserId,
                Time = workout.Time,
                Date = workout.Date,
                Exercises = Exercises(exercises)
            };

            return workoutModel;
        }

        public static List<WorkoutModel> Workouts(List<Workout> workouts, Dictionary<int, List<Exercise>> exercisesValuePairs)
        {
            List<WorkoutModel> workoutModels = new();

            foreach (Workout workout in workouts)
            {
                workoutModels.Add(new WorkoutModel()
                {
                    Id = workout.Id,
                    Name = workout.Name,
                    UserId = workout.UserId,
                    Time = workout.Time,
                    Date = workout.Date,
                    Exercises = Exercises(exercisesValuePairs[workout.Id])
                });
            }

            return workoutModels;
        }

    }
}
