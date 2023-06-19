using Azure.Core;
using ZUPWebAPI.Entities;
using ZUPWebAPI.Models;
using ZUPWebAPI.Repositories;

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
    }
}
