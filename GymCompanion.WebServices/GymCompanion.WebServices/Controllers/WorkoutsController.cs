using GymCompanion.Data;
using GymCompanion.Data.Models.Workouts;
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
        public async Task<ActionResult> GetUserWorkouts(User user, string workoutName)
        {
            try
            {
                List<Workout> userWorkouts = await _context.Workouts.Where(x => x.UserId == user.Id).ToListAsync();

                List<GetUserWorkoutsModel> model = new List<GetUserWorkoutsModel>();

                foreach (Workout workout in userWorkouts)
                {
                    GetUserWorkoutsModel tempWorkout = new GetUserWorkoutsModel()
                    {
                        Id = workout.Id,
                        Name = workout.Name,
                        UserId = workout.Id,
                        Time = (double)workout.Time,
                        Date = workout.Date,
                    };

                    //TODO pass exercises
                }

                return Ok(model);
            }
            catch (Exception exception)
            {
                return StatusCode(500, exception);
            }
        }

        [Route("CreateUserWorkout")]
        [HttpPost]
        public async Task<ActionResult> CreateUserWorkout(string name, int userId, double time, DateTime date, List<object> exercises)
        {
            //todo fix the exercises

            try
            {
                User user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

                if (user == null)
                    return StatusCode(409, Numerators.ApiResponseMessages.UserNotFound);
                else
                {
                    Workout workout = new Workout()
                    {
                        Name = name,
                        UserId = userId,
                        Time = time,
                        Date = date,
                        //Exercises = exercises 
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
        public async Task<ActionResult> DeleteUserWorkout()
        {
            //todo
            return Ok();
        }
    }
}
