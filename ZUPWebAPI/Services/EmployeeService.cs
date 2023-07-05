using ZUPWebAPI.Entities;
using ZUPWebAPI.Repositories;

namespace ZUPWebAPI.Services
{
    public class EmployeeService
    {
        EmployeeRepository employeeRepository = new EmployeeRepository();
        MessageEntity messageEntity = new MessageEntity();

        public EmployeeService()
        {
            messageEntity.code = -1;
            messageEntity.message = "Произошла не установленная ошибка. Обратитесь в сервис деск.";
        }

        //  Создание нового сотрудника
        public MessageEntity EmployeeCreate(EmployeeEntity employeeEntity)
        {
            int employeeId = -1;

            try
            {
                employeeId = employeeRepository.EmployeeCreate(employeeEntity); 
            }
            catch (Exception ex)
            {
                messageEntity.code = -1;
                messageEntity.message = ex.Message;
                return messageEntity;
            }

            messageEntity.code = employeeId;
            messageEntity.message = "Сотрудник успешно создан";
            return messageEntity;

        }

        //  Изменение сотрудника
        public MessageEntity EmployeeChange(EmployeeEntity employeeEntity)
        {
            int countChanging = -1;

            try
            {
                countChanging = employeeRepository.EmployeeCreate(employeeEntity);
            }
            catch (Exception ex)
            {
                messageEntity.code = -1;
                messageEntity.message = ex.Message;
                return messageEntity;
            }
            if (countChanging <= 0)
            {
                messageEntity.code = -1;
                messageEntity.message = "Изменения сотрудника не произошло. Обратитесь в сервис деск.";
                return messageEntity;
            }

            messageEntity.code = employeeEntity.idEmployee;
            messageEntity.message = "Изменение данных по сотруднику - успешно произведено.";
            return messageEntity;
        }

        //  Пометка сотрудника как удалённого
        public MessageEntity EmployeeDelete(int employeeId)
        {
            int countDeleted = -1;
            try
            {
                countDeleted = employeeRepository.EmployeerDelete(employeeId);
            }
            catch (Exception ex)
            {
                messageEntity.code = -1;
                messageEntity.message = ex.Message;
                return messageEntity;
            }
            if (countDeleted <= 0)
            {
                messageEntity.code = -1;
                messageEntity.message = "Ни один сотрудник не был удалён. Обратитесь в сервис деск";
                return messageEntity;
            }

            messageEntity.code = employeeId;
            messageEntity.message = "Сотрудник успешно удалён";
            return messageEntity;
        }

    }

}
