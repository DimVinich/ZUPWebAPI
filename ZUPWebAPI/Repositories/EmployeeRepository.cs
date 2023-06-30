using ZUPWebAPI.Entities;

namespace ZUPWebAPI.Repositories
{
    public class EmployeeRepository : BaseRepository
    {
        // создать сотрудника
        public int EmployeeCreate(EmployeeEntity employeeEntity)
        {
            int? employeeId = QueryFirstOrDefault<int>(@"", employeeEntity);

            return employeeId.Value;
        }

        //  изменить сотрудника

        //  пометить сотрудника на удаление

        //  найти сотрудника по ИНН ??

        //  найти сотрудника по фамили и ициниалам
    }
}
