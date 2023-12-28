using Azure.Core;
using Microsoft.IdentityModel.Tokens;
using System;
using ZUPWebAPI.Entities;
using ZUPWebAPI.Models;
using ZUPWebAPI.Repositories;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ZUPWebAPI.Services
{
    public class ServiceService : BaseRepository
    {
        public MessageEntity ServiceIsAvailable()
        {
            MessageEntity messageEntity = new MessageEntity();
            messageEntity.code = -1;
            messageEntity.message = connectionString;
            int iH;

            //  В трае запрашиваем кол-во записей v_today
            try
            {
                iH = QueryFirstOrDefault<int>(@"select count(*) from v_today");
            }
            catch (Exception ex)
            {
                messageEntity.code = -1;
                messageEntity.message = ex.Message;
                return messageEntity;
            }
            if (iH < 1)
            {
                messageEntity.code = -1;
                messageEntity.message += "  Проблемы с работой базы данных. Обратитесь в сервис деск.";
                return messageEntity;
            }

            messageEntity.code = 1;
            messageEntity.message += "   Сервис и БД нормально работают.";

            return messageEntity;

        }

        //  Считать примечание настройки
        public string OptionsNoteGet(int idApplication, int idParam)
        {
            return QueryFirstOrDefault<string>(@"select yes_no from application_param with (nolock)
                                        where id_application = @idApplication_p  and id_param = @idParam_p",
                                        new { idApplication_p = idApplication, idParam_p = idParam });
        }

        //  Считать примечание настройки как DatiTime
        //  Если параметр не найден, или задан не корректно, возвращает текущую дату
        public DateTime OptionsNoteGetAsDateTime(int idApplication, int idParam)
        {
            string? ls_h;
            DateTime ldt;
            ls_h = OptionsNoteGet(idApplication, idParam);
            if (ls_h == null)
            {
                ldt = DateTime.Today;
            }

            if ( ! DateTime.TryParse(ls_h, out ldt))
            {
                ldt = DateTime.Today;
            }

            return ldt;
        }

    }
}
