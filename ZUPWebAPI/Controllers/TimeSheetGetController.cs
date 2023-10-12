using Microsoft.AspNetCore.Mvc;
using ZUPWebAPI.Entities;
using ZUPWebAPI.Models;
using ZUPWebAPI.Repositories;
using ZUPWebAPI.Services;

namespace ZUPWebAPI.Controllers
{
    [Route("api/TimeSheet/[controller]")]
    [ApiController]
    public class TimeSheetGetController : Controller
    {
        protected UserService userService = new UserService();
        protected User user = new User();
        protected UserAuthenticationData userAuthenticationData = new UserAuthenticationData();
        protected TimeSheetRepository timeSheetRepository = new TimeSheetRepository();

        [HttpPost]
        public IActionResult Get([FromBody] TimeSheetGetData timeSheetGetData)
        {
            //  Проверка аудиентификации
            userAuthenticationData.Login = timeSheetGetData.Login;
            userAuthenticationData.Password = timeSheetGetData.Password;
            user = userService.Authenticate(userAuthenticationData);

            // Если не ОК, то возвращаем ошибку. 
            if (user.Id < 1)
            {
                ErrorEntity errorEntity = new ErrorEntity(user.Id, user.Name);
                return Json(errorEntity);
            }

            // Если аутентификация прошла успешно, запрашиваем табель
            var timeSheet = timeSheetRepository.TimeSheetGet(timeSheetGetData);
            return Json(timeSheet);


        }
    }
}
