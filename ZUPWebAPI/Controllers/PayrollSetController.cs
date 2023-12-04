using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using ZUPWebAPI.Entities;
using ZUPWebAPI.Models;
using ZUPWebAPI.Services;

namespace ZUPWebAPI.Controllers
{
    [Route("api/payroll/[controller]")]
    [ApiController]
    public class PayrollSetController : Controller
    {
        protected User user = new User();
        protected UserService userService = new UserService();
        protected UserAuthenticationData userAuthenticationData = new UserAuthenticationData();

        protected MessageEntity messageEntity = new MessageEntity();
        protected PayrolleService payrolleService = new PayrolleService();
        [HttpPost]

        public IActionResult Get([FromBody] PayrollSetData payrollZUP)
        {
            userAuthenticationData.Login = payrollZUP.Login;
            userAuthenticationData.Password = payrollZUP.Password;
            user = userService.Authenticate(userAuthenticationData);

            if (user.Id < 1)
            {
                messageEntity.code = user.Id;
                messageEntity.message = user.Name;
                return Json(messageEntity);
            }
            
            messageEntity = payrolleService.PayrollSet(payrollZUP);
            return Json(messageEntity );
        }
    }
}
