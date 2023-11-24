using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using ZUPWebAPI.Entities;
using ZUPWebAPI.Services;

namespace ZUPWebAPI.Controllers
{
    [Route("api/payroll/[controller]")]
    [ApiController]
    public class PayrollSetController : Controller
    {
        protected MessageEntity messageEntity = new MessageEntity();
        protected PayrolleService payrolleService = new PayrolleService();
        [HttpPost]

        public IActionResult Get([FromBody] List<PayrollEntity> payrollList)
        {

            messageEntity = payrolleService.PayrollSet(payrollList);
            return Json(messageEntity );
        }
    }
}
