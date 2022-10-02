using GymCompanion.Data;
using GymCompanion.Data.Models.Exercises;
using GymCompanion.WebServices.DAL;
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
        public async Task<ActionResult> GetExerciseInfo(int exerciseId)
        {
            try
            {
                Exercise exerciseToReturn = await _context.Exercises.FirstOrDefaultAsync(x => x.Id == exerciseId);

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
        public async Task<ActionResult> CreateExercise(string name, int bodyPartId, string description)
        {
            try
            {
                BodyPart bodyPart = await _context.BodyParts.FirstOrDefaultAsync(x => x.Id == bodyPartId);
                if (bodyPart != null)
                {
                    bool exerciseExists = await _context.Exercises.CountAsync(x => x.Name == name) != 0;

                    if (!exerciseExists)
                    {
                        Exercise exerciseToAdd = new Exercise()
                        {
                            Name = name,
                            BodyPartId = bodyPart.Id,
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
        public async Task<ActionResult> DeleteExercise(int exerciseId)
        {
            Exercise exerciseToDelete = await _context.Exercises.FirstOrDefaultAsync(x => x.Id == exerciseId);

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
        public async Task<ActionResult> UpdateExercise(int exerciseId, string newExerciseName, int newBodyPartId, string newDescription)
        {
            try
            {
                Exercise exerciseToUpdate = await _context.Exercises.FirstOrDefaultAsync(x => x.Id == exerciseId);
                BodyPart bodyPart = await _context.BodyParts.FirstOrDefaultAsync(x => x.Id == newBodyPartId);
                bool exerciseNameExists = await _context.Exercises.CountAsync(x => x.Name == newExerciseName) != 0 && newExerciseName != exerciseToUpdate.Name;

                if (exerciseToUpdate == null)
                    return StatusCode(409, Numerators.ApiResponseMessages.ExerciseNotFound);
                else if (bodyPart == null)
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
