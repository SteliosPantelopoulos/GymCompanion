using GymCompanion.WebServices.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GymCompanion.WebServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        protected readonly GymCompanionContext _context;

        public BaseApiController()
        {

        }

        public BaseApiController(GymCompanionContext context)
        {
            _context = context;
        }
            
    }
    
}
