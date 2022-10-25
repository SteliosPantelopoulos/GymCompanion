using GymCompanion.Data.Models.Exercises;
using GymCompanion.Data.ServicesModels.Exercises;
using GymCompanion.WebServices.DAL;
using GymCompanion.WebServices.Helpers;
using GymCompanion.WebServices.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Xml.Linq;

namespace GymCompanion.WebServices.Controllers
{
    [Authorize]
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

                if (exerciseToReturn != null)
                {
                    model.Exercise = ModelsAdapter.Exercise(exerciseToReturn);
                    return Ok(JsonConvert.SerializeObject(model));
                    
                }
                else
                {
                    return NotFound();
                }

            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [Route("GetExercisesInfo")]
        [HttpGet]
        public async Task<ActionResult> GetExercisesInfo()
        {
            GetExercisesInfoModel model = new();
            try
            {
                List<Exercise> exercisesToReturn = await _context.Exercises.OrderBy(x => x.Name).ToListAsync();

                if (exercisesToReturn != null)
                {
                    model = new GetExercisesInfoModel()
                    {
                        Exercises = ModelsAdapter.Exercises(exercisesToReturn)
                    };

                    return Ok(JsonConvert.SerializeObject(model));
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [Route("CreateExercise")]
        [HttpPost]
        public async Task<ActionResult> CreateExercise([FromBody] CreateExerciseModel model)
        {
            try
            {
                BodyPart bodyPart = await _context.BodyParts.FirstOrDefaultAsync(x => x.Id == model.BodyPartId);
                if (bodyPart != null)
                {
                    bool exerciseExists = await _context.Exercises.CountAsync(x => x.Name == model.Name) != 0;

                    if (exerciseExists)
                    {
                        return Conflict();
                    }
                    else
                    {
                        Exercise exerciseToAdd = new Exercise()
                        {
                            Name = model.Name,
                            BodyPartId = bodyPart.Id,
                            Description = model.Description
                        };

                        await _context.Exercises.AddAsync(exerciseToAdd);
                        await _context.SaveChangesAsync();

                        return Ok();
                    }
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [Route("DeleteExercise")]
        [HttpDelete]
        public async Task<ActionResult> DeleteExercise(int exerciseId)
        {
            try
            {
                Exercise exerciseToDelete = await _context.Exercises.FirstOrDefaultAsync(x => x.Id == exerciseId);

                if (exerciseToDelete != null)
                {
                    _context.Exercises.Remove(exerciseToDelete);
                    await _context.SaveChangesAsync();

                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [Route("UpdateExercise")]
        [HttpPost]
        public async Task<ActionResult> UpdateExercise([FromBody] UpdateExerciseModel model)
        {
            try
            {
                Exercise exerciseToUpdate = await _context.Exercises.FirstOrDefaultAsync(x => x.Id == model.Id);
                BodyPart bodyPart = await _context.BodyParts.FirstOrDefaultAsync(x => x.Id == model.BodyPartId);
                bool exerciseNameExists = await _context.Exercises.CountAsync(x => x.Name == model.Name) != 0 && model.Name != exerciseToUpdate.Name;

                if (exerciseToUpdate == null)
                    return NotFound("exercise");
                else if (bodyPart == null)
                    return NotFound("bodyPart");
                else if (exerciseNameExists)
                    return Conflict();
                else
                {
                    exerciseToUpdate.Name = model.Name;
                    exerciseToUpdate.BodyPartId = bodyPart.Id;
                    exerciseToUpdate.Description = model.Description;
                    await _context.SaveChangesAsync();

                    return Ok();
                }
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
