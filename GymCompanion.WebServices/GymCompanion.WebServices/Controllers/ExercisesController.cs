using GymCompanion.Data;
using GymCompanion.Data.Models.Exercises;
using GymCompanion.Data.Models.General;
using GymCompanion.WebServices.DAL;
using GymCompanion.WebServices.Helpers;
using GymCompanion.WebServices.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

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
            GetExerciseInfoModel model = new();
            try
            {
                Exercise exerciseToReturn = await _context.Exercises.FirstOrDefaultAsync(x => x.Id == exerciseId);

                if (exerciseToReturn == null)
                {
                    model.ApiResponseMessage = (int)Numerators.ApiResponseMessages.ExerciseNotFound;
                    return StatusCode(409, JsonConvert.SerializeObject(model));
                }
                else
                {
                    model.Exercise = ModelsAdapter.Exercise(exerciseToReturn);

                    return Ok(JsonConvert.SerializeObject(model));
                }

            }
            catch (Exception exception)
            {
                model.ExceptionMessage = exception;
                return StatusCode(500, JsonConvert.SerializeObject(model));
            }
        }

        [Route("GetExercisesInfo")]
        [HttpGet]
        public async Task<ActionResult> GetExercisesInfo()
        {
            GetExercisesInfoModel model = new();
            try
            {
                List<Exercise> exercisesToReturn = await _context.Exercises.ToListAsync();

                if (exercisesToReturn == null)
                {
                    model.ApiResponseMessage = (int)Numerators.ApiResponseMessages.ExercisesNotFound;
                    return StatusCode(409, JsonConvert.SerializeObject(model));
                }
                else
                {
                    model = new GetExercisesInfoModel()
                    {
                        Exercises = ModelsAdapter.Exercises(exercisesToReturn)
                    };

                    return Ok(JsonConvert.SerializeObject(model));
                }
            }
            catch (Exception exception)
            {
                model.ExceptionMessage = exception;
                return StatusCode(500, JsonConvert.SerializeObject(model));
            }
        }

        [Route("CreateExercise")]
        [HttpPost]
        public async Task<ActionResult> CreateExercise(string name, int bodyPartId, string description)
        {
            BooleanModel model = new();
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

                        model.Result = true;
                        return Ok(JsonConvert.SerializeObject(model));
                    }
                    else
                    {
                        model.ApiResponseMessage = (int)Numerators.ApiResponseMessages.ExerciseNameIsUsed;
                        return StatusCode(409, JsonConvert.SerializeObject(model));
                    }
                }
                else
                {
                    model.ApiResponseMessage = (int)Numerators.ApiResponseMessages.BodyPartNotFound;
                    return StatusCode(409, JsonConvert.SerializeObject(model));
                }
            }
            catch (Exception exception)
            {
                model.ExceptionMessage = exception;
                return StatusCode(500, JsonConvert.SerializeObject(model));
            }
        }

        [Route("DeleteExercise")]
        [HttpDelete]
        public async Task<ActionResult> DeleteExercise(int exerciseId)
        {
            BooleanModel model = new();
            try
            {
                Exercise exerciseToDelete = await _context.Exercises.FirstOrDefaultAsync(x => x.Id == exerciseId);

                if (exerciseToDelete != null)
                {
                    _context.Exercises.Remove(exerciseToDelete);
                    await _context.SaveChangesAsync();

                    model.Result = true;
                    return Ok(JsonConvert.SerializeObject(model));
                }
                else
                {
                    model.ApiResponseMessage = (int)Numerators.ApiResponseMessages.ExerciseNotFound;
                    return StatusCode(409, JsonConvert.SerializeObject(model));
                }
            }
            catch (Exception exception)
            {
                model.ExceptionMessage = exception;
                return StatusCode(500, JsonConvert.SerializeObject(model));
            }
        }

        [Route("UpdateExercise")]
        [HttpPost]
        public async Task<ActionResult> UpdateExercise(int exerciseId, string newExerciseName, int newBodyPartId, string newDescription)
        {
            BooleanModel model = new();
            try
            {
                Exercise exerciseToUpdate = await _context.Exercises.FirstOrDefaultAsync(x => x.Id == exerciseId);
                BodyPart bodyPart = await _context.BodyParts.FirstOrDefaultAsync(x => x.Id == newBodyPartId);
                bool exerciseNameExists = await _context.Exercises.CountAsync(x => x.Name == newExerciseName) != 0 && newExerciseName != exerciseToUpdate.Name;

                if (exerciseToUpdate == null)
                {
                    model.ApiResponseMessage = (int)Numerators.ApiResponseMessages.ExerciseNotFound;
                    return StatusCode(409, JsonConvert.SerializeObject(model));
                }
                else if (bodyPart == null)
                {
                    model.ApiResponseMessage = (int)Numerators.ApiResponseMessages.BodyPartNotFound;
                    return StatusCode(409, JsonConvert.SerializeObject(model));
                }
                else if (exerciseNameExists)
                {
                    model.ApiResponseMessage = (int)Numerators.ApiResponseMessages.ExerciseNameIsUsed;
                    return StatusCode(409, JsonConvert.SerializeObject(model));
                }
                else
                {
                    exerciseToUpdate.Name = newExerciseName;
                    exerciseToUpdate.BodyPartId = bodyPart.Id;
                    exerciseToUpdate.Description = newDescription;
                    await _context.SaveChangesAsync();

                    model.Result = true;
                    return Ok(JsonConvert.SerializeObject(model));
                }
            }
            catch (Exception exception)
            {
                model.ExceptionMessage = exception;
                return StatusCode(500, JsonConvert.SerializeObject(model));
            }
        }
    }
}
