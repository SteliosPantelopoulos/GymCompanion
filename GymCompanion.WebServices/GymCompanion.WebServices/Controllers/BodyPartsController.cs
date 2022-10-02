using GymCompanion.Data;
using GymCompanion.Data.Models.BodyParts;
using GymCompanion.WebServices.DAL;
using GymCompanion.WebServices.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            try
            {
                BodyPart bodyPart = await _context.BodyParts.FirstOrDefaultAsync(x => x.Id == bodyPartId);

                if (bodyPart != null)
                {
                    GetBodyPartInfoModel model = new GetBodyPartInfoModel()
                    {
                        Name = bodyPart.Name
                    };
                    return Ok(model);
                }
                else
                    return StatusCode(409, Numerators.ApiResponseMessages.BodyPartNotFound);
            }
            catch (Exception exception)
            {
                return StatusCode(500, exception);
            }
        }

        [Route("GetBodyPartsInfo")]
        [HttpGet]
        public async Task<ActionResult> GetBodyPartsInfo()
        {
            try
            {
                List<BodyPart> bodyParts = await _context.BodyParts.ToListAsync();

                if (bodyParts != null)
                {
                    GetBodyPartsInfoModel model = new GetBodyPartsInfoModel();

                    foreach (BodyPart bodyPart in bodyParts)
                        model.BodyParts.Add(new GetBodyPartInfoModel() { Name = bodyPart.Name});

                    bodyParts = bodyParts.OrderBy(x => x.Id).ToList();
                    return Ok(bodyParts);
                }
                else
                    return StatusCode(409, Numerators.ApiResponseMessages.BodyPartsNotFound);
            }
            catch (Exception exception)
            {
                return StatusCode(500, exception);
            }
        }

        [Route("CreateBodyPart")]
        [HttpPost]
        public async Task<ActionResult> CreateBodyPart(string name)
        {
            try
            {
                bool bodyPartExists = await _context.BodyParts.CountAsync(x => x.Name == name) != 0;

                if (bodyPartExists)
                    return StatusCode(409, Numerators.ApiResponseMessages.BodyPartNameIsUsed);
                else
                {
                    BodyPart bodyPart = new BodyPart()
                    {
                        Name = name
                    };

                    await _context.BodyParts.AddAsync(bodyPart);
                    await _context.SaveChangesAsync();

                    return Ok(true);
                }
            }
            catch (Exception exception)
            {
                return StatusCode(500, exception);
            }
        }

        [Route("DeleteBodyPart")]
        [HttpDelete]
        public async Task<ActionResult> DeleteBodyPart(int bodyPartId)
        {
            try
            {
                BodyPart bodyPartToDelete = await _context.BodyParts.FirstOrDefaultAsync(x => x.Id == bodyPartId);

                if (bodyPartToDelete == null)
                    return StatusCode(409, Numerators.ApiResponseMessages.BodyPartNotFound);
                else
                {
                    _context.Remove(bodyPartToDelete);
                    await _context.SaveChangesAsync();

                    return Ok(true);
                }
            }
            catch (Exception exception)
            {
                return StatusCode(500, exception);
            }
        }

        [Route("UpdateBodyPart")]
        [HttpPost]
        public async Task<ActionResult> UpdateBodyPart(int bodyPartId, string newName)
        {
            try
            {
                BodyPart bodyPartToUdpate = await _context.BodyParts.FirstOrDefaultAsync(x => x.Id == bodyPartId);
                bool nameIsUsed = await _context.BodyParts.CountAsync(x => x.Name == newName) != 0 && bodyPartToUdpate.Name != newName;

                if (bodyPartToUdpate == null)
                    return StatusCode(409, Numerators.ApiResponseMessages.BodyPartNotFound);
                else if (nameIsUsed)
                    return StatusCode(409, Numerators.ApiResponseMessages.BodyPartNameIsUsed);
                else
                {
                    bodyPartToUdpate.Name = newName;
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
