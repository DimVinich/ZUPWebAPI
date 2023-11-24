using ZUPWebAPI.Entities;
using ZUPWebAPI.Repositories;

namespace ZUPWebAPI.Services
{
    public class PayrolleService
    {

        //  ------------------------------------------------- Обработка начислений дальше по ЗП
        //      т.е. менять данные по таблицам Payroll, Salary и т.д.
        //      Смешно, но для удаления информации с ЗУП и для проведения нужно будет писать разную обработку.
        //      и да, удаление / добавление информации с ЗУП и обработку дальше по таблицам нужно делать в одной транзацкии
        //      все изменения нужно будет в лог писать.

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
        public MessageEntity PayrollDel(string idDocZUP)
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
        public MessageEntity PayrollSet(List<PayrollEntity> payrollList)
        {
            //  Выделить из массива JSON с кучей начислений код документа. А нафига брать первый попавщийся
            //  Удалить по документу информацию
            string idDocZup;
            if (payrollList == null || payrollList.Count < 1)
            {
                messageEntity.code = -1;
                messageEntity.message = "Вы передали пустой список начислений. Никаих действий сделано не было.";
                return messageEntity;
            }

            idDocZup = payrollList[0].idDocZUP;
            messageEntity = PayrollDel( idDocZup);
            if (messageEntity.code < 1) { return messageEntity; }

            //  Вставить записи в базу, через перебор массива и вставку
            foreach(PayrollEntity payrollEntity in payrollList)
            {
                try
                {
                    messageEntity.code = payrollRepository.PayrollInsert(payrollEntity);
                }
                catch (Exception ex)
                {
                    messageEntity.code = -1;
                    messageEntity.message = ex.Message;
                    return messageEntity;
                }
            }

            //  Запустить процедуру обработки начислений , которую тоже нужно в репозитарий запихать
            //  Похорошему удаление , вставку и обработку нужно в одну транзакцию запускать
            messageEntity.code = 1;
            messageEntity.message = "Данные по документу ЗУП успешно добавлены.";
            return messageEntity;
        }

        
        

    }
}
