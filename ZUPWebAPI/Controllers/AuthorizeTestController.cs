using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZUPWebAPI.Entities;
using ZUPWebAPI.Services;

namespace ZUPWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class AuthorizeTestController : Controller
    {
        protected MessageEntity messageEntity = new MessageEntity();
        protected ServiceService serviceService = new ServiceService();

        // Даже не будем авторизоваться, просто запрос на select count(*) from v_today
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        //public IActionResult Get()
        //{
        //    messageEntity = serviceService.ServiceIsAvailable();
        //    return Json(messageEntity);
        //}


    }
}
