using ZUPWebAPI.Entities;
using ZUPWebAPI.Models;
using ZUPWebAPI.Repositories;

namespace ZUPWebAPI.Services
{
    public class EmployeeService
    {
        EmployeeRepository employeeRepository = new EmployeeRepository();
        MessageEntity messageEntity = new MessageEntity();

        //  Если у сотрудник есть в КИС, смотреть есть ли  не указан имайл, нужно брать из базы.
        //  Для этого отдельный метод

        public EmployeeService()
        {
            messageEntity.code = -1;
            messageEntity.message = "Произошла не установленная ошибка. Обратитесь в сервис деск.";
        }

        //  Создание нового сотрудника
        public MessageEntity EmployeeCreate(EmployeeEntity employeeEntity)
        {
            int employeeId = -1;
            List<int> employeeFoundIDs = new List<int>();
            int countChanging = -1;

            // Поискать сотрудника по ИНН
            try
            {
                employeeFoundIDs = employeeRepository.EmloyeerFindeByINN(employeeEntity.TIN);
            }
            catch (Exception ex)
            {
                messageEntity.code = -1;
                messageEntity.message = ex.Message;
                return messageEntity;
            }

            if (employeeFoundIDs != null)
            {
                if (employeeFoundIDs.Count == 1)
                {
                    if (employeeFoundIDs[0] > 0)
                    {
                        employeeId = employeeFoundIDs[0];
                    }
                }
                else if (employeeFoundIDs.Count > 1)
                {
                    messageEntity.code = -1;
                    messageEntity.message = "Найденно более одного сотрудника с ИНН: " + employeeEntity.TIN + "/nНеобходимо исправить ошибку в КИС.";
                    return messageEntity;
                }
            }

            //  Если сотрудник не найден, поискать сотрдника по ФИО
            if (employeeId == -1)
            {
                try
                {
                    employeeFoundIDs = employeeRepository.EmployeerFindeByName(employeeEntity.nEmployeeShort);
                }
                catch (Exception ex)
                {
                    messageEntity.code = -1;
                    messageEntity.message = ex.Message;
                    return messageEntity;
                }
                if (employeeFoundIDs != null)
                {
                    if (employeeFoundIDs.Count == 1)
                    {
                        if (employeeFoundIDs[0] > 0)
                        {
                            employeeId = employeeFoundIDs[0];
                        }
                    }
                    else if (employeeFoundIDs.Count > 1)
                    {
                        messageEntity.code = -1;
                        messageEntity.message = "Найденно более одного сотрудника с ИНН: " + employeeEntity.TIN + "/nНеобходимо исправить ошибку в КИС.";
                        return messageEntity;
                    }
                }
            }

            //     Если сотрудинк был найден, то обновляем инфомрацию по нему.
            if (employeeId > 1)
            {
                try
                {
                    employeeEntity.idEmployee = employeeId;
                    EmployeeCheckMail(employeeEntity);          //      Если проверка, если почта пустая, то забрать с БД
                    countChanging = employeeRepository.EmployeerCahnge(employeeEntity);
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
                    messageEntity.message = "Был найден сотрудник с таким ИНН или ФИО, но при обновить информацию по нему не удалось. Обратитесь в сервис деск.";
                    return messageEntity;
                }

                messageEntity.code = employeeEntity.idEmployee;
                messageEntity.message = "Данный сотрудник был найден в КИС, обновление информации по нему успешно произведено.";
                return messageEntity;

            }

            //     Сотрудник не нейден, создаём нового
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

        // ================================== Изменение сотрудника
        public MessageEntity EmployeeChange(EmployeeEntity employeeEntity)
        {
            int countChanging = -1;

            try
            {
                EmployeeCheckMail(employeeEntity);          //      Если проверка, если почта пустая, то забрать с БД
                countChanging = employeeRepository.EmployeerCahnge(employeeEntity);
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

        //  Наличие мыла у сотрудника, если нет, забор из КИС
        public int EmployeeCheckMail( EmployeeEntity employeeEntity)
        {
            string employeeMail = "";

            //  с регуляными выражениями позже придётся разобраться.
            //  на данный момент на длину и не пустое.
            if (employeeEntity.eMail == null || employeeEntity.eMail.Length < 5 )
            {
                employeeMail = employeeRepository.EmployeerGetMail(employeeEntity.idEmployee);
            }

            if (employeeMail != null && employeeMail.Length > 5)
            {
                employeeEntity.eMail = employeeMail;
            }
            return 1;  //   пока заглушкой, что б потом везде код не переделывать, когда понадобится
        }
    }
}
