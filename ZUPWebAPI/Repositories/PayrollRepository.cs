using ZUPWebAPI.Entities;

namespace ZUPWebAPI.Repositories
{
    public class PayrollRepository : BaseRepository
    {


        protected int li_H;
        /*
                Действие на записью, что произошло          статус записи                   Код в таблице PayrollZUP        
                --------------------------------------      -----------------------------   -------------------
                Начисление вставилось в PayrollZUP          Записано Новое                  1
                -------------------------------------       -----------------------------   -------------------
                На основании записи прошла обработка        
                по PayRoll и Salary                         Новая запись Обработана         10
                -------------------------------------       -----------------------------   --------------------
                Пометили запись на удаление,                Запись помечена на удаление,
                которая не была обработана                  обработки не требует            -10
                -------------------------------------       -----------------------------   --------------------
                Запись пометили на удаление,                Запись помечена на удаление
                но нужно ещё обработать                     , требует обработки             -1
                -------------------------------------       -----------------------------   --------------------
                Обработали запись                           Пометка на удаление
                                                            обработана                      -10

        */

        //  Удаление данных по документу из ЗУП 
        //  помечает данные на удаление и необходимость обработки . статус = -1, после чего нужно вызвать метод обработки удаления.
        public int PayrollDelete( string idDocZUP) 
        {
            li_H = Execute(@"update PayrollFromZUP set idStatus = -1 where idDocZUP = @idDocZUP_p and idStatus = 10", new { idDocZUP_p = idDocZUP });
            return li_H;

        }

        //  Обработака записей помеченных на удаление
        public int PayrollDeleteProcessing(string idDocZUP)
        {
            li_H = Execute(@"exec  up_PayrollFromZUPDelete @idDocZUP_p", new { idDocZUP_p = idDocZUP });
            return li_H;
        }

        //  Вставка данных по документу из ЗУП
        public int PayrollInsert( PayrollEntity payrollEntity )
        {
            return Execute(@"insert into PayrollFromZUP(
	                                            idDocZUP,
	                                            idEmployee,
	                                            Year,
	                                            Month,
	                                            idTypePayrollZUP,
                                                Amount,
                                                idStatus
                                            )
                                            values(
	                                            @idDocZUP,
	                                            @idEmployee,
	                                            @Year,
	                                            @Month,
	                                            @idTypePayroll,
                                                @Amount,
                                                1
                                            )", payrollEntity);
        }

        //  Обработка вставленных из ЗУП данных
        public int PayrollInserteProcessing(string idDocZUP)
        {
            li_H = Execute(@"exec  up_PayrollFromZUPInsert @idDocZUP_p", new { idDocZUP_p = idDocZUP });
            return li_H;
        }

        //  Веозвращает 1е число мексяца и года по коду документа из ЗУП
        public DateTime PayrollGetDate(string idDocZUP)
        {
            return QueryFirstOrDefault<DateTime>(@"select distinct DATEFROMPARTS(Year, Month, 1)
                                                    from PayrollFromZUP 
                                                    where idDocZUP = @idDocZUP_p", new { @idDocZUP_p = idDocZUP });
        }

    }
}
