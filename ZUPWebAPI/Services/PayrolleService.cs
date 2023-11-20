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


        //  -----------------------------------------------Удалить данные по документу ЗУП
        MessageEntity PayrollDel(string idDocZUP)
        {
            try
            {
                messageEntity.code = payrollRepository.PayrollDelete(idDocZUP);
            }
            catch (Exception ex)
            {
                messageEntity.code = -1;
                messageEntity.message = ex.Message;
                return messageEntity;
            }

            messageEntity.code = 1;
            messageEntity.message = "Данные по документу ЗУП успешно удалены.";
            return messageEntity;
        }

        // ------------------------------------------------- Запись начислений из переданного с ЗУП
        MessageEntity PayrollSet(PayrollEntity payrollEntity)
        {
            //  Выделить из массива JSON с кучей начислений код документа. А нафига брать первый попавщийся
            //  Удалить по документу информацию

            //  Вставить записи в базу

            //  Запустить процедуру обработки начислений , которую тоже нужно в репозитарий запихать
                    //  Похорошему удаление , вставку и обработку нужно в одну транзакцию запускать

            return messageEntity;
        }
        

    }
}
