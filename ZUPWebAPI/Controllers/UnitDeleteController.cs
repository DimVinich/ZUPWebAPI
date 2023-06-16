using Microsoft.AspNetCore.Mvc;
using ZUPWebAPI.Entities;
using ZUPWebAPI.Models;
using ZUPWebAPI.Services;

namespace ZUPWebAPI.Controllers
{

    [Route("api/unit/[controller]")]
    [ApiController]
    public class UnitDeleteController : Controller
    {
        protected UserService userService = new UserService();
        protected User user = new User();
        protected UserAuthenticationData userAuthenticationData = new UserAuthenticationData();
        protected MessageEntity messageEntity = new MessageEntity();
        protected UnitService unitService = new UnitService();

        [HttpPost]
        public IActionResult Get([FromBody] UnitDeleteData unitDeleteData)
        {
            //  Проверка аудиентификации
            userAuthenticationData.Login = unitDeleteData.Login;
            userAuthenticationData.Password = unitDeleteData.Password;
            user = userService.Authenticate(userAuthenticationData);

            // Если не ОК, то возвращаем ошибку. 
            if (user.Id < 1)
            {
                ErrorEntity errorEntity = new ErrorEntity(user.Id, user.Name);
                return Json(errorEntity);
            }

            //  а если  Ок, пошла обработка дальше. ставим пометку на уделаение отдела.
            messageEntity = unitService.UnitDelete(unitDeleteData.IdUnit);

            return Json(messageEntity);

        }


    }
}
