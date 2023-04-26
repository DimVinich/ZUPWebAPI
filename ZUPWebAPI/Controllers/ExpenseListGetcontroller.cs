using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using ZUPWebAPI.Models;

namespace ZUPWebAPI.Controllers
{
    [Route("api/expense/[controller]")]
    [ApiController]
    public class ExpenseListGetcontroller : Controller
    {

        // поля , свойства класса

        //  обработака веб запроса
        [HttpPost]
        public IActionResult Get([FromBody] UserAuthenticationData userAuthenticationData)
        {
            //    using (IDbConnection db = new SqlConnection(Connection.ConnectionString))
            //    {
            //        kontr? new_kon = db.Query<kontr>($"SELECT id_kontr, login_www, pass_www FROM spr_kontr WHERE login_www='{kon.login_www}'").FirstOrDefault();
            //        if (new_kon == null)
            //            return NotFound();
            //        return Json(new_kon);
            //    }
            return Json(userAuthenticationData);
        }

    }
}
