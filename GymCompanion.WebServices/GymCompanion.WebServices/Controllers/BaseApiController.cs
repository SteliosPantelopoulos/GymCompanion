using GymCompanion.WebServices.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GymCompanion.WebServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        protected readonly GymCompanionContext _context;

        public BaseApiController(GymCompanionContext context)
        {
            _context = context;
        }
            
    }
    
}
