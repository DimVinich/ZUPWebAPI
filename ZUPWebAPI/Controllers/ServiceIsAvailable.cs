using Microsoft.AspNetCore.Mvc;
using ZUPWebAPI.Entities;
using ZUPWebAPI.Services;

namespace ZUPWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceIsAvailableController : Controller
    {
        protected MessageEntity messageEntity = new MessageEntity();
        protected ServiceService serviceService = new ServiceService();

        // Даже не будем авторизоваться, просто запрос на select count(*) from v_today
        [HttpGet]
        public IActionResult Get()
        {
            messageEntity = serviceService.ServiceIsAvailable();
            return Json(messageEntity);
        }

    }
}
