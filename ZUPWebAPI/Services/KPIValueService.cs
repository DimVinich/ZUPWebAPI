using ZUPWebAPI.Entities;
using ZUPWebAPI.Models;
using ZUPWebAPI.Repositories;

namespace ZUPWebAPI.Services
{
    public class KPIValueService
    {
        KPIValueRepository kPIValueRepository = new KPIValueRepository();

        public IEnumerable<KPIValueEntity> KPIValueGet(KPIValueGetData kPIValueGetData)
        {
            //  Нужно добавлять какие КПИ закреплены за отделом из аргмунта, и в зависимости от этого дёргать ту или иную функцию.
            //  на данный момент только склад ВРН, с показателем 40 . Скорость ед/час 

            //  Change Sokolov 25-04-2024
            //      добавился ОО, который трансп логистики и девушки на лип. складе
            //      т.к. собирается показатель от отделов, то пока тупо через CASE, дальше переделывать на спр. привязки должность и КПИ, 
            //      и собирать через него.

            //  Склад ВРН 2
            //  ОО 17
            //  Отдел док-ной трансп лог 47
            //  Склад Липецк, там тоже есть должности ОО 41. 46 - тоже пока оставлю, хоть он и записан под удаление.

            try
            {
                switch (kPIValueGetData.idUnit)
                {
                    case 2:
                        return kPIValueRepository.KPIValuesSkladVRNGet(kPIValueGetData);

                    case 17 or 47 or 41 or 46:
                        return kPIValueRepository.KPIValuesDocProcessed(kPIValueGetData);

                    default:
                        return Enumerable.Empty<KPIValueEntity>();
                }
                
            }
            catch
            {
                return Enumerable.Empty<KPIValueEntity>();
            }

        }
    }
}
