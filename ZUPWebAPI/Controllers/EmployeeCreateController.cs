using Microsoft.AspNetCore.Mvc;
using ZUPWebAPI.Entities;
using ZUPWebAPI.Models;
using ZUPWebAPI.Services;

namespace ZUPWebAPI.Controllers
{
    [Route("api/employee/[controller]")]
    [ApiController]

    public class EmployeeCreateController : Controller
    {
        protected EmployeeService employeeService = new EmployeeService();
        protected User user = new User();
        protected UserAuthenticationData userAuthenticationData = new UserAuthenticationData();
        protected MessageEntity messageEntity = new MessageEntity();
        protected UserService userService = new UserService();

        [HttpPost]
        public IActionResult Get([FromBody] EmployeeChangeData employeeChangeData)
        {
            // Авторизация
            userAuthenticationData.Login = employeeChangeData.Login;
            userAuthenticationData.Password = employeeChangeData.Password;
            user = userService.Authenticate(userAuthenticationData);

            if(user.Id < 1)
            {
                messageEntity.message = user.Name;
                messageEntity.code = user.Id;
                return Json(messageEntity);
            }

            //  Создание сотрудника
            messageEntity = employeeService.EmployeeCreate(employeeChangeData);
            return Json(messageEntity);
        }

    }
}
