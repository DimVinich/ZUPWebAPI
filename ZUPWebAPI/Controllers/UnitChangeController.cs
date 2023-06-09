using Microsoft.AspNetCore.Mvc;
using ZUPWebAPI.Entities;
using ZUPWebAPI.Models;
using ZUPWebAPI.Services;

namespace ZUPWebAPI.Controllers
{
    [Route("api/expense/[controller]")]
    [ApiController]
    public class UnitChangeController : Controller
    {
        protected UserService userService = new UserService();
        protected User user = new User();
        protected UserAuthenticationData userAuthenticationData = new UserAuthenticationData();
        protected MessageEntity messageEntity = new MessageEntity();
        protected UnitService unitService = new UnitService();

        [HttpPost]
        public IActionResult Get([FromBody] UnitChangeData unitChangeData)
        {
            //  Проверка аудиентификации
            userAuthenticationData.Login = unitChangeData.Login;
            userAuthenticationData.Password = unitChangeData.Password;
            user = userService.Authenticate(userAuthenticationData);

            // Если не ОК, то возвращаем ошибку. 
            if (user.Id < 1)
            {
                ErrorEntity errorEntity = new ErrorEntity(user.Id, user.Name);
                return Json(errorEntity);
            }

            //  а если  Ок, пошла обработка дальше. ставим пометку на уделаение отдела.
            messageEntity = unitService.UnitChange(unitChangeData);

            return Json(messageEntity);
        }
    }
}
