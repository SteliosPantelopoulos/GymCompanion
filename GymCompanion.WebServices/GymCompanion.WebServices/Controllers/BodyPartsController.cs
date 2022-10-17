using GymCompanion.Data;
using GymCompanion.Data.Models.BodyParts;
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
    public class BodyPartsController : BaseApiController
    {
        public BodyPartsController(GymCompanionContext context)
            : base(context)
        {
        }

        [Route("GetBodyPartInfo")]
        [HttpGet]
        public async Task<ActionResult> GetBodyPartInfo(int bodyPartId)
        {
            GetBodyPartInfoModel model = new();
            try
            {
                BodyPart bodyPartToReturn = await _context.BodyParts.FirstOrDefaultAsync(x => x.Id == bodyPartId);

                if (bodyPartToReturn != null)
                {
                    model.BodyPart = ModelsAdapter.BodyPart(bodyPartToReturn);

                    return Ok(JsonConvert.SerializeObject(model));
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

        [Route("GetBodyPartsInfo")]
        [HttpGet]
        public async Task<ActionResult> GetBodyPartsInfo()
        {
            GetBodyPartsInfoModel model = new();
            try
            {
                List<BodyPart> bodyPartsToReturn = await _context.BodyParts.OrderBy(x=>x.Id).ToListAsync();

                if (bodyPartsToReturn != null)
                {
                    model = new GetBodyPartsInfoModel()
                    {
                        BodyParts = ModelsAdapter.BodyParts(bodyPartsToReturn)
                    };

                    return Ok(JsonConvert.SerializeObject(model));
                }
                else
                {
                    model.ApiResponseMessage = (int)Numerators.ApiResponseMessages.BodyPartsNotFound;
                    return StatusCode(409, JsonConvert.SerializeObject(model));
                }
            }
            catch (Exception exception)
            {
                model.ExceptionMessage = exception;
                return StatusCode(500, JsonConvert.SerializeObject(model));
            }
        }

        [Route("CreateBodyPart")]
        [HttpPost]
        public async Task<ActionResult> CreateBodyPart(string name)
        {
            BooleanModel model = new();
            try
            {
                bool bodyPartExists = await _context.BodyParts.CountAsync(x => x.Name == name) != 0;

                if (bodyPartExists)
                {
                    model.ApiResponseMessage = (int)Numerators.ApiResponseMessages.BodyPartNameIsUsed;
                    return StatusCode(409, JsonConvert.SerializeObject(model));
                }
                else
                {
                    BodyPart bodyPart = new()
                    {
                        Name = name
                    };

                    await _context.BodyParts.AddAsync(bodyPart);
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

        [Route("DeleteBodyPart")]
        [HttpDelete]
        public async Task<ActionResult> DeleteBodyPart(int bodyPartId)
        {
            BooleanModel model = new();
            try
            {
                BodyPart bodyPartToDelete = await _context.BodyParts.FirstOrDefaultAsync(x => x.Id == bodyPartId);

                if (bodyPartToDelete == null)
                {
                    model.Result = false;
                    model.ApiResponseMessage = (int)Numerators.ApiResponseMessages.BodyPartNotFound;
                    return StatusCode(409, JsonConvert.SerializeObject(model));
                }
                else
                {
                    List<Exercise> exercisesToDelete = await _context.Exercises.Where(x => x.BodyPartId == bodyPartId).ToListAsync();

                    _context.RemoveRange(exercisesToDelete);
                    _context.Remove(bodyPartToDelete);
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

        [Route("UpdateBodyPart")]
        [HttpPost]
        public async Task<ActionResult> UpdateBodyPart(int bodyPartId, string newName)
        {
            BooleanModel model = new();

            try
            {
                BodyPart bodyPartToUdpate = await _context.BodyParts.FirstOrDefaultAsync(x => x.Id == bodyPartId);
                bool nameIsUsed = await _context.BodyParts.CountAsync(x => x.Name == newName) != 0 && bodyPartToUdpate.Name != newName;

                if (bodyPartToUdpate == null)
                {
                    model.Result = false;
                    model.ApiResponseMessage = (int)Numerators.ApiResponseMessages.BodyPartNotFound;
                    return StatusCode(409, JsonConvert.SerializeObject(model));
                }
                else if (nameIsUsed)
                {
                    model.Result = false;
                    model.ApiResponseMessage = (int)Numerators.ApiResponseMessages.BodyPartNameIsUsed;
                    return StatusCode(409, JsonConvert.SerializeObject(model));
                }
                else
                {
                    bodyPartToUdpate.Name = newName;
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
