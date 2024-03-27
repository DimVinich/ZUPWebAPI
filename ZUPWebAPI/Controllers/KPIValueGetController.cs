using Microsoft.AspNetCore.Mvc;
using ZUPWebAPI.Entities;
using ZUPWebAPI.Models;
using ZUPWebAPI.Repositories;
using ZUPWebAPI.Services;

namespace ZUPWebAPI.Controllers
{

    [Route("api/KPIValue/[controller]")]
    [ApiController]
    public class KPIValueGetController : Controller
    {
        protected UserService userService = new UserService();
        protected User user = new User();
        protected UserAuthenticationData userAuthenticationData = new UserAuthenticationData();
        protected KPIValueService kPIValueService = new KPIValueService();

        [HttpPost]
        public IActionResult Get([FromBody] KPIValueGetData kPIValueGetData)
        {
            //  Проверка аудиентификации
            userAuthenticationData.Login = kPIValueGetData.Login;
            userAuthenticationData.Password = kPIValueGetData.Password;
            user = userService.Authenticate(userAuthenticationData);

            // Если не ОК, то возвращаем ошибку. 
            if (user.Id < 1)
            {
                ErrorEntity errorEntity = new ErrorEntity(user.Id, user.Name);
                return Json(errorEntity);
            }

            // Если аутентификация прошла успешно, запрашиваем показатели КПИ
            var kPIValueEntitys = kPIValueService.KPIValueGet(kPIValueGetData);
            return Json(kPIValueEntitys);
        }

    }
}
