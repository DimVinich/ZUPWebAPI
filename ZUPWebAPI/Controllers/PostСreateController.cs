using Microsoft.AspNetCore.Mvc;
using ZUPWebAPI.Entities;
using ZUPWebAPI.Models;
using ZUPWebAPI.Services;

namespace ZUPWebAPI.Controllers
{
    [Route("api/post/[controller]")]
    [ApiController]
    public class PostCreateController : Controller
    {
        protected UserService userService = new UserService();
        protected User user = new User();
        protected UserAuthenticationData userAuthenticationData = new UserAuthenticationData();
        protected MessageEntity messageEntity = new MessageEntity();
        protected UnitService unitService = new UnitService();

        [HttpPost]
        public IActionResult Get([FromBody] PostChangeData postChangeData)
        {

            //  Проверка аудиентификации
            userAuthenticationData.Login = postChangeData.Login;
            userAuthenticationData.Password = postChangeData.Password;
            user = userService.Authenticate(userAuthenticationData);

            // Если не ОК, то возвращаем ошибку. 
            if (user.Id < 1)
            {
                ErrorEntity errorEntity = new ErrorEntity(user.Id, user.Name);
                return Json(errorEntity);
            }

            //  а если  Ок, пошла обработка дальше. создаём должность
            //messageEntity = unitService.UnitCreate(unitChangeData);

            return Json(messageEntity);

        }
    }
}
