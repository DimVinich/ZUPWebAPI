﻿using Microsoft.AspNetCore.Mvc;
using ZUPWebAPI.Entities;
using ZUPWebAPI.Models;
using ZUPWebAPI.Services;

namespace ZUPWebAPI.Controllers
{

    [Route("api/expense/[controller]")]
    [ApiController]
    public class UnitItemDeletecontroller : Controller
    {
        protected UserService userService = new UserService();
        protected User user = new User();

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

            //  а если  Ок, пошла обработка дальше? ставим пометку на уделаение отдела.


            return Json(userAuthenticationData);

        }


    }
}
