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
            try
            {
                return kPIValueRepository.KPIValuesSkladVRNGet(kPIValueGetData);
            }
            catch
            {
                return Enumerable.Empty<KPIValueEntity>();
            }

        }
    }
}
