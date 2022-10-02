using GymCompanion.Data;
using GymCompanion.Data.Models.Workouts;
using GymCompanion.WebServices.DAL;
using GymCompanion.WebServices.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GymCompanion.WebServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkoutsController : BaseApiController
    {
        public WorkoutsController(GymCompanionContext context)
            : base(context)
        {
        }

        [Route("GetUserWorkouts")]
        [HttpGet]
        public async Task<ActionResult> GetUserWorkouts(int userId)
        {
            try
            {
                User user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

                if (user == null)
                    return StatusCode(409, Numerators.ApiResponseMessages.UserNotFound);
                else
                {
                    List<GetUserWorkoutsModel> model = new();

                    List<Workout> userWorkouts = await _context.Workouts.Where(x => x.UserId == user.Id).ToListAsync();
                    foreach (Workout workout in userWorkouts)
                    {
                        List<Exercise> listOfExercises = await _context.Exercises.Where(e => e.Workouts.Contains(workout)).ToListAsync();
                        List<string> listOfExercisesId = new();
                        foreach (Exercise exercise in listOfExercises)
                            listOfExercisesId.Add(exercise.Id.ToString());

                        string exercises = String.Join(",", listOfExercisesId);


                        GetUserWorkoutsModel tempWorkout = new()
                        {
                            Id = workout.Id,
                            Name = workout.Name,
                            UserId = workout.Id,
                            Time = (double)workout.Time,
                            Date = workout.Date,
                            Exercises = exercises
                        };

                        model.Add(tempWorkout);
                    }

                    return Ok(model);
                }   
            }
            catch (Exception exception)
            {
                return StatusCode(500, exception);
            }
        }

        [Route("CreateUserWorkout")]
        [HttpPost]
        public async Task<ActionResult> CreateUserWorkout(string name, int userId, double time, DateTime date, string exercises)
        {
            try
            {
                List<int> exercisesIds = (exercises.Split(',').ToList()).Select(int.Parse).ToList();

                User user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

                if (user == null)
                    return StatusCode(409, Numerators.ApiResponseMessages.UserNotFound);
                else
                {
                    List<Exercise> exercisesToAdd = new();

                    foreach (int exerciseId in exercisesIds)
                    {
                        Exercise exercise = await _context.Exercises.FirstOrDefaultAsync(x => x.Id == exerciseId);

                        if (exercise != null)
                            exercisesToAdd.Add(exercise);
                        else
                            return StatusCode(409, Numerators.ApiResponseMessages.ExerciseNotFound);
                    }

                    Workout workout = new()
                    {
                        Name = name,
                        UserId = userId,
                        Time = time,
                        Date = date,
                        Exercises = exercisesToAdd
                    };

                    await _context.Workouts.AddAsync(workout);
                    await _context.SaveChangesAsync();

                    return Ok(true);
                }
            }
            catch (Exception exception)
            {
                return StatusCode(500, exception);
            }
        }

        [Route("DeleteUserWorkout")]
        [HttpDelete]
        public async Task<ActionResult> DeleteUserWorkout(int workoutId)
        {
            try
            {
                Workout workoutToDelete = await _context.Workouts.FirstOrDefaultAsync(x => x.Id == workoutId);

                if (workoutToDelete == null)
                    return StatusCode(409, Numerators.ApiResponseMessages.WorkoutNotFound);
                else
                {
                    _context.Workouts.Remove(workoutToDelete);
                    await _context.SaveChangesAsync();

                    return Ok(true);
                }
            }
            catch (Exception exception)
            {
                return StatusCode(500, exception);
            }
        }
    }
}
