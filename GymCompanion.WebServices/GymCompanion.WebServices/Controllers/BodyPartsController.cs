using GymCompanion.Data.Models.BodyParts;
using GymCompanion.Data.ServicesModels.BodyParts;
using GymCompanion.WebServices.DAL;
using GymCompanion.WebServices.Helpers;
using GymCompanion.WebServices.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace GymCompanion.WebServices.Controllers
{
    [Authorize]
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
                    return NotFound();
                }
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [Route("GetBodyPartsInfo")]
        [HttpGet]
        public async Task<ActionResult> GetBodyPartsInfo()
        {
            GetBodyPartsInfoModel model = new();
            try
            {
                List<BodyPart> bodyPartsToReturn = await _context.BodyParts.OrderBy(x => x.Name).ToListAsync();

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
                    return NotFound();
                }
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [Route("CreateBodyPart")]
        [HttpPost]
        public async Task<ActionResult> CreateBodyPart([FromBody] CreateBodyPartModel model)
        {
            try
            {
                bool bodyPartExists = await _context.BodyParts.CountAsync(x => x.Name == model.Name) != 0;

                if (bodyPartExists)
                {
                    return Conflict();
                }
                else
                {
                    BodyPart bodyPart = new()
                    {
                        Name = model.Name
                    };

                    await _context.BodyParts.AddAsync(bodyPart);
                    await _context.SaveChangesAsync();

                    return Ok();
                }
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [Route("DeleteBodyPart")]
        [HttpDelete]
        public async Task<ActionResult> DeleteBodyPart(int bodyPartId)
        {
            try
            {
                BodyPart bodyPartToDelete = await _context.BodyParts.FirstOrDefaultAsync(x => x.Id == bodyPartId);

                if (bodyPartToDelete != null)
                {
                    List<Exercise> exercisesToDelete = await _context.Exercises.Where(x => x.BodyPartId == bodyPartId).ToListAsync();

                    _context.RemoveRange(exercisesToDelete);
                    _context.Remove(bodyPartToDelete);
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

        [Route("UpdateBodyPart")]
        [HttpPost]
        public async Task<ActionResult> UpdateBodyPart([FromBody] UpdateBodyPartModel model)
        {
            try
            {
                BodyPart bodyPartToUdpate = await _context.BodyParts.FirstOrDefaultAsync(x => x.Id == model.Id);
                bool nameIsUsed = await _context.BodyParts.CountAsync(x => x.Name == model.Name) != 0 && bodyPartToUdpate.Name != model.Name;

                if (bodyPartToUdpate == null)
                    return NotFound();
                else if (nameIsUsed)
                    return Conflict();
                else
                {
                    bodyPartToUdpate.Name = model.Name;
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
