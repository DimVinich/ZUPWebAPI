using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using ZUPWebAPI.Models;
using ZUPWebAPI.Services;

namespace ZUPWebAPI.Controllers
{
    [Route("api/expense/[controller]")]
    [ApiController]
    public class ExpenseListGetcontroller : Controller
    {

        // поля , свойства класса
        //protected User user = new User();
        protected UserService userService = new UserService();
        protected User user = new User();

        //  Класс сервиса пользователей

        //  обработака веб запроса
        [HttpPost]
        public IActionResult Get([FromBody] UserAuthenticationData userAuthenticationData)
        {

            //  Проверка аудиентификации
            user = userService.Authenticate(userAuthenticationData);

            // Если Ок, то для начала отдаём приветствие
            if(user.Id != 0)
            {

            }
            return Json(userAuthenticationData);
        }

    }
}
