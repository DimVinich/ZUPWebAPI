using ZUPWebAPI.Entities;
using ZUPWebAPI.Models;
using ZUPWebAPI.Repositories;

namespace ZUPWebAPI.Services
{
    public class PayrolleService
    {

        //  ------------------------------------------------- Обработка начислений дальше по ЗП
        //      т.е. менять данные по таблице Payroll
        //      и да, удаление / добавление информации с ЗУП и обработку дальше по таблицам нужно делать в одной транзацкии
        //      все изменения нужно будет в лог писать.

        //  класс репозитария
        PayrollRepository payrollRepository = new PayrollRepository();
        MessageEntity messageEntity = new MessageEntity();
        ServiceService serviceService = new ServiceService();   

        //  конструктор, первоначальное заполнение
        public PayrolleService()
        {
            messageEntity.code = -1;
            messageEntity.message = "Произошла не установленная ошибка. Обратитесь в сервис деск.";
        }

        //  -----------------------------------------------Удалить данные по документу ЗУП
        public MessageEntity PayrollDel(string idDocZUP)
        {
            //      И в лог чего либо записыват нужно 

            //  проверим за какой период данный документ

            //  получим дату из загруженных данных
            DateTime? dateDocZUP;
            try
            {
                dateDocZUP = payrollRepository.PayrollGetDate(idDocZUP);
            }
            catch (Exception ex)
            {
                messageEntity.code = -1;
                messageEntity.message = ex.Message;
                return messageEntity;
            }

            if(dateDocZUP == null)
            {
                //  а нет записей по этому документу стоит ли дальше суетиться ? нет, валим.
                messageEntity.code = 1;
                messageEntity.message = "Данных по документу ЗУП не найдено.";
                return messageEntity;
            }

            //  получим дату полсе которой периоды доступны для правки
            DateTime datePeriodAn;

            try
            {
                datePeriodAn = serviceService.OptionsNoteGetAsDateTime(50, 487);
            }
            catch (Exception ex)
            {
                messageEntity.code = -1;
                messageEntity.message = ex.Message;
                return messageEntity;
            }
            //  сравним
            if (dateDocZUP < datePeriodAn)
            {
                messageEntity.code = -1;
                messageEntity.message = "Дата документа " + dateDocZUP.ToString() + " меньше даты открытых периодов "+ datePeriodAn.ToString();
                return messageEntity;
            }

            //  ставим пометку на удаление
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

            //  дёргаем обработку записей, помеченных на удаление
            try
            {
                messageEntity.code = payrollRepository.PayrollDeleteProcessing(idDocZUP);
            }
            catch (Exception ex2)
            {
                messageEntity.code = -1;
                messageEntity.message = ex2.Message;
                return messageEntity;
            }

            messageEntity.code = 1;
            messageEntity.message = "Данные по документу ЗУП успешно удалены.";
            return messageEntity;
        }

        // ------------------------------------------------- Запись начислений из переданного с ЗУП
        public MessageEntity PayrollSet(PayrollSetData payrollList)
        {
            string idDocZup;
            if ( payrollList.payrolls.Count < 1)
            {
                messageEntity.code = -1;
                messageEntity.message = "Вы передали пустой список начислений. Никаих действий сделано не было.";
                return messageEntity;
            }

            //  т.к. в Josn из ЗУП код докмумента передаётся только в "шапке", нужно проставить его по всему списку начислений
            idDocZup = payrollList.idDocZup;

            //  Удалить по документу информацию
            messageEntity = PayrollDel( idDocZup);
            if (messageEntity.code < 1) { return messageEntity; }

            //  Проверить, что период, за который выгружаются данные допступен к корректировке.
            
            //  Соберём дату документа ЗУП
            PayrollEntity payroll;
            payroll = payrollList.payrolls.FirstOrDefault();
            DateTime dateDocZUP = new DateTime(payroll.Year , payroll.Month, 1  );

            //  получим дату полсе которой периоды доступны для правки
            DateTime datePeriodAn;

            try
            {
                datePeriodAn = serviceService.OptionsNoteGetAsDateTime(50, 487);
            }
            catch (Exception ex)
            {
                messageEntity.code = -1;
                messageEntity.message = ex.Message;
                return messageEntity;
            }
            //  сравним
            if (dateDocZUP < datePeriodAn)
            {
                messageEntity.code = -1;
                messageEntity.message = "Дата документа " + dateDocZUP.ToString() + " меньше даты открытых периодов " + datePeriodAn.ToString();
                return messageEntity;
            }

            //  Вставить записи в базу, через перебор массива и вставку
            foreach (PayrollEntity payrollEntity in payrollList.payrolls)
            {
                try
                {
                    payrollEntity.idDocZUP = idDocZup;
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
            try
            {
                
                messageEntity.code = payrollRepository.PayrollInserteProcessing(idDocZup);
            }
            catch (Exception ex2)
            {
                messageEntity.code = -1;
                messageEntity.message = ex2.Message;
                return messageEntity;
            }

            messageEntity.code = 1;
            messageEntity.message = "Данные по документу ЗУП успешно добавлены.";
            return messageEntity;
        }

    }
}
