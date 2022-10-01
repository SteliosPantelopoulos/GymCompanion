using GymCompanion.Data;
using GymCompanion.Data.Models.Exercises;
using GymCompanion.WebServices.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GymCompanion.WebServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExercisesController : BaseApiController
    {
        public ExercisesController(GymCompanionContext context)
            : base(context)
        {
        }

        [Route("GetExerciseInfo")]
        [HttpGet]
        public async Task<ActionResult> GetExerciseInfo(string name)
        {
            try
            {
                Exercise exerciseToReturn = await _context.Exercises.FirstOrDefaultAsync(x => x.Name == name);

                if (exerciseToReturn == null)
                    return StatusCode(409, Numerators.ApiResponseMessages.ExerciseNotFound);
                else
                {
                    GetExerciseInfoModel model = new GetExerciseInfoModel()
                    {
                        Name = exerciseToReturn.Name,
                        BodyPartId = exerciseToReturn.BodyPartId,
                        Description = exerciseToReturn.Description
                    };

                    return Ok(model);
                }

            }
            catch (Exception exception)
            {
                return StatusCode(500, exception);
            }
        }

        [Route("GetExercisesInfo")]
        [HttpGet]
        public async Task<ActionResult> GetExercisesInfo()
        {
            try
            {
                List<Exercise> exercisesToReturn = await _context.Exercises.ToListAsync();

                if (exercisesToReturn == null)
                    return StatusCode(409, Numerators.ApiResponseMessages.ExercisesNotFound);
                else
                {
                    GetExercisesInfoModel model = new GetExercisesInfoModel();
                    foreach (Exercise exercise in exercisesToReturn)
                    {
                        model.Exercises.Add(new GetExerciseInfoModel()
                        {
                            Name = exercise.Name,
                            BodyPartId = exercise.BodyPartId,
                            Description = exercise.Description
                        });
                    }

                    return Ok(model);
                }

            }
            catch (Exception exception)
            {
                return StatusCode(500, exception);
            }
        }

        [Route("CreateExercise")]
        [HttpPost]
        public async Task<ActionResult> CreateExercise(string name, string bodyPartName, string description)
        {
            BodyPart bodyPart = await _context.BodyParts.FirstOrDefaultAsync(x => x.Name == bodyPartName);


            try
            {
                if (bodyPart != null)
                {
                    bool exerciseExists = await _context.Exercises.CountAsync(x => x.Name == name) == 0;

                    if (!exerciseExists)
                    {
                        Exercise exerciseToAdd = new Exercise()
                        {
                            Name = name,
                            /*BodyPart =bodyPart.Id,*/
                            Description = description
                        };

                        await _context.Exercises.AddAsync(exerciseToAdd);
                        await _context.SaveChangesAsync();
                        return Ok(true);
                    }
                    else
                    {
                        return StatusCode(409, Numerators.ApiResponseMessages.ExerciseNameIsUsed);
                    }
                }
                else
                    return StatusCode(409, Numerators.ApiResponseMessages.BodyPartNotFound);
            }
            catch (Exception exception)
            {
                return StatusCode(500, exception);
            }
        }

        [Route("DeleteExercise")]
        [HttpDelete]
        public async Task<ActionResult> DeleteExercise(string name)
        {
            Exercise exerciseToDelete = await _context.Exercises.FirstOrDefaultAsync(x => x.Name == name);

            try
            {
                if (exerciseToDelete != null)
                {
                    _context.Exercises.Remove(exerciseToDelete);
                    await _context.SaveChangesAsync();

                    return Ok(true);
                }
                else
                    return StatusCode(409, Numerators.ApiResponseMessages.ExerciseNotFound);

            }
            catch (Exception exception)
            {
                return StatusCode(500, exception);
            }
        }

        [Route("UpdateExercise")]
        [HttpPost]
        public async Task<ActionResult> UpdateExercise(string oldExerciseName, string newExerciseName, string newBodyPart, string newDescription)
        {
            try
            {
                Exercise exerciseToUpdate = await _context.Exercises.FirstOrDefaultAsync(x => x.Name == oldExerciseName);
                BodyPart bodyPart = await _context.BodyParts.FirstOrDefaultAsync(x => x.Name == newBodyPart);
                bool exerciseNameExists = await _context.Exercises.CountAsync(x => x.Name == newExerciseName) != 0;

                if (exerciseToUpdate == null)
                    return StatusCode(409, Numerators.ApiResponseMessages.ExerciseNotFound);
                else if (newBodyPart == null)
                    return StatusCode(409, Numerators.ApiResponseMessages.BodyPartNotFound);
                else if (exerciseNameExists)
                    return StatusCode(409, Numerators.ApiResponseMessages.ExerciseNameIsUsed);
                else
                {
                    exerciseToUpdate.Name = newExerciseName;
                    exerciseToUpdate.BodyPartId = bodyPart.Id;
                    exerciseToUpdate.Description = newDescription;

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
