using Microsoft.AspNetCore.Mvc;
using ZUPWebAPI.Entities;
using ZUPWebAPI.Models;
using ZUPWebAPI.Services;

namespace ZUPWebAPI.Controllers
{
    [Route("api/employee/[controller]")]
    [ApiController]

    public class EmployeeDeleteController : Controller
    {
        protected UserService userService = new UserService();
        protected User user = new User();
        protected UserAuthenticationData userAuthenticationData = new UserAuthenticationData();
        protected MessageEntity messageEntity = new MessageEntity();
        protected EmployeeService employeeService = new EmployeeService();

        [HttpPost]
        public IActionResult Get([FromBody] EmployeeDeleteData employeeDeleteData)
        {
            //  Проверка аудентификации
            userAuthenticationData.Login = employeeDeleteData.Login;
            userAuthenticationData.Password = employeeDeleteData.Password;
            user = userService.Authenticate(userAuthenticationData); 

            if(user.Id < 1)
            {
                messageEntity.code = user.Id;
                messageEntity.message = user.Name;
                return Json(messageEntity);
            }

            //  Вызов метода удаления сотрудника
            messageEntity = employeeService.EmployeeDelete(employeeDeleteData.IdEmployee);
            return Json(messageEntity);

        }
    }
}
