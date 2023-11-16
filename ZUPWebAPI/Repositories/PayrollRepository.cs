using ZUPWebAPI.Entities;

namespace ZUPWebAPI.Repositories
{
    public class PayrollRepository : BaseRepository
    {
        //  Удаление данных по документу из 1С
        public int PayrollDelete( string idDocPayRoll ) 
        {
            return Execute(@"delete from PayrollFromZUP where PayrollFromZUP.idDocZUP = @idDocZUP", idDocPayRoll);
        }

        //  Вставка данных по документу из 1С
        public int PayrollInsert( PayrollEntity payrollEntity )
        {
            return Execute(@"insert into PayrollFromZUP(
	                                            idDocZUP,
	                                            idEmployee,
	                                            Year,
	                                            Month,
	                                            idTypePayroll
                                            )
                                            values(
	                                            @idDocZUP,
	                                            @idEmployee,
	                                            @Year,
	                                            @Month,
	                                            @idTypePayroll
                                            )", payrollEntity);
        }

    }
}
