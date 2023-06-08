﻿using ZUPWebAPI.Entities;
using ZUPWebAPI.Repositories;

namespace ZUPWebAPI.Services
{
    public class UnitService
    {
        UnitRepository unitRepository;
        MessageEntity messageEntity;

        public UnitService()
        {
            unitRepository = new UnitRepository();
            MessageEntity messageEntity = new MessageEntity();
            messageEntity.code = -1;
            messageEntity.message = "Произошла не установленная ошибка. Обратитесь в сервис деск.";

        }

        //  погнали привешивать методы работы с отделами.
        //  проверки на ошибки - в след. релиз.  Ща нуна, что б работало и сделать по быстрому.

        //  Создание отдела
        public MessageEntity UnitCreate(UnitEntity unitEntity)
        {
            int unitId;

            try
            {
                unitId = unitRepository.UnitCreate(unitEntity);
            }
            catch (Exception ex)
            {
                messageEntity.code = -1;
                messageEntity.message = ex.Message;
                return messageEntity;
            }
            if (unitId <= 0)
            {
                messageEntity.code = -1;
                messageEntity.message = "Созадание отдела не произошло. Обратитесь в сервис деск.";
                return messageEntity;
            }
            messageEntity.code = unitId;
            messageEntity.message = "Отдел успешно создан";
            return messageEntity;
        }

        //  Изменение отдела
        public MessageEntity UnitChange(UnitEntity unitEntity)
        {
            int countChanging;
            try
            {
                countChanging = unitRepository.UnitChange(unitEntity);
            }
            catch (Exception ex)
            {
                messageEntity.code = -1;
                messageEntity.message = ex.Message;
                return messageEntity;
            }
            if (countChanging <= 1)
            {
                messageEntity.code = -1;
                messageEntity.message = "Изменение отдела не произошло. Обратитесь в сервис деск.";
                return messageEntity;
            }

            messageEntity.code = countChanging;
            messageEntity.message = "Изменение данных по отделу - успешно произведено.";
            return messageEntity;
        }

        //  Удаление отдела. Ставиться статус отдела в -1
        public  MessageEntity UnitDelete(int unitId)
        {
            int countDeleted = -1;
            try
            {
                countDeleted = unitRepository.UnitDelete(unitId);
            }
            catch (Exception ex)
            {
                messageEntity.code = -1;
                messageEntity.message = ex.Message;
            }
            if(countDeleted <= 1)
            {
                messageEntity.code = -1;
                messageEntity.message = "Ни один отдел не был удалён. Обратитесь в сервис деск";
            }

            messageEntity.code = countDeleted;
            messageEntity.message = "Отдел успешно удалён";
            return messageEntity;
        }
    }
}
