using ZUPWebAPI.Entities;
using ZUPWebAPI.Models;
using ZUPWebAPI.Repositories;

namespace ZUPWebAPI.Services
{
    public class TimeSheetService
    {
        TimeSheetRepository TimeSheetRepository = new TimeSheetRepository();

        //MessageEntity messageEntity = new MessageEntity();  а нафига ?

        /*        public TimeSheetService()    а нафига ?
                {
                    messageEntity.code = -1;
                    messageEntity.message = "Произошла не установленная ошибка. Обратитесь в сервис деск.";
                }
        */

        // ===========================получение табеля
        public IEnumerable<TimeSheetEntity> TimeSheetGet(TimeSheetGetData timeSheetGetData)
        {
            
            try
            {
                return TimeSheetRepository.TimeSheetGet(timeSheetGetData);
            }
            catch 
            { 
                return new List<TimeSheetEntity>();
            }
        }

    }
}
