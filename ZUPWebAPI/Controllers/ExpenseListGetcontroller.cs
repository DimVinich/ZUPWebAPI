using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using ZUPWebAPI.Entities;
using ZUPWebAPI.Models;
using ZUPWebAPI.Repositories;
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
        protected ExpenseRepository expenseRepository = new ExpenseRepository();

        //  Класс сервиса пользователей

        //  обработака веб запроса
        [HttpPost]
        public IActionResult Get([FromBody] UserAuthenticationData userAuthenticationData)
        {

            //  Проверка аудиентификации
            user = userService.Authenticate(userAuthenticationData);

            // Если не ОК, то возвращаем ошибку. 
            if (user.Id < 1)
            {
                ErrorEntity errorEntity = new ErrorEntity(user.Id, user.Name);
                return Json(errorEntity);
            }

            //  а если  Ок, пошла обработка дальше и вернём спискок статей
            var expenseList = expenseRepository.ExpensesGet();
            return Json(expenseList);

            //return Json(userAuthenticationData);

        }

    }
}
