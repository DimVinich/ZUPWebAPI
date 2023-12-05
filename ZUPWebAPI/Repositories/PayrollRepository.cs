using ZUPWebAPI.Entities;

namespace ZUPWebAPI.Repositories
{
    public class PayrollRepository : BaseRepository
    {

        protected int li_H;

        //  Удаление данных по документу из 1С
        public int PayrollDelete( string idDocZUP) 
        {
            li_H = Execute(@"update PayrollFromZUP set idStatus = -1 where idDocZUP = @idDocZUP_p and idStatus = 10", new { idDocZUP_p = idDocZUP });
            li_H += Execute(@"update PayrollFromZUP set idStatus = -10 where idDocZUP = @idDocZUP_p and idStatus = 1", new { idDocZUP_p = idDocZUP });
            return li_H;

            //      Вот как то теперь обработать нужно.
        }

        //  Вставка данных по документу из 1С
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

    }
}
