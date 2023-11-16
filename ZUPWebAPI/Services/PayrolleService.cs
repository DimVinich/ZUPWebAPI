using ZUPWebAPI.Entities;
using ZUPWebAPI.Repositories;

namespace ZUPWebAPI.Services
{
    public class PayrolleService
    {
        //  класс репозитария
        PayrollRepository payrollRepository = new PayrollRepository();
        MessageEntity messageEntity = new MessageEntity();

        //  конструктор, первоначальное заполнение
        public PayrolleService()
        {
            messageEntity.code = -1;
            messageEntity.message = "Произошла не установленная ошибка. Обратитесь в сервис деск.";
        }

        //  Выделить код документа

        //  Затереть данные по этому документу

        //  Вставить данные с документа

        //  Запустить процедуру обработки начислений 

    }
}
