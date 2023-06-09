using ZUPWebAPI.Entities;

namespace ZUPWebAPI.Repositories
{
    public class UnitRepository : BaseRepository
    {
        //  Получить отдел по его коду
        public UnitEntity UnitGetById (int idUnit)
        {
            return QueryFirstOrDefault<UnitEntity>(@"select id_unit as IdUnit, n_unit as NUnit, IdLevel as IdLevel
                                                            , IdTopLevel as IdTopLevel, id_chief as IdChief
                                                      from spr_unit with (nolock)
                                                       where id_unit = @idUnit_p", new { idUnit_p = idUnit });
        }

        //  Удалить отдел по его коду.
        //  Отдел помечатеся на удаление. Возвращает сколько строк было изменено
        public int UnitDelete(int idUnit)
        {
            return Execute("update spr_unit set idStatus = -1 where id_unit = @idUnit_p", new { idUnit_p = idUnit });
        }

        //  Изменить отдел по его коду
        //  Передаётся сразу сущьность отдел, по которой и происходт изменения
        //  Возвращает кол-во изменённых записей. По хорошем должна быть 1
        public int UnitChange(UnitEntity unitEntity)
        {
            return Execute(@"update spr_unit
	                            set n_unit = @NUnit
		                            , IdLevel = @IdLevel
		                            , IdTopLevel = @IdTopLevel
		                            , id_chief = @IdChief
                            where id_unit = @IdUnit", unitEntity);
        }

        //     Созадать новый отдел
        //      Возвращает код новосозданного отдела
        public int UnitCreate(UnitEntity unitEntity)
        {
            int? unitID = QueryFirstOrDefault<int>(@"Insert into spr_unit (
	                                                    n_unit
	                                                    , IdLevel
	                                                    , IdTopLevel
	                                                    , id_chief)
                                                    values (
	                                                    @NUnit
	                                                    , @IdLevel 
	                                                    , @IdTopLevel
	                                                    , @IdChief ); 
                                                    SELECT CAST(SCOPE_IDENTITY() as int)", unitEntity);
            if (unitID != null)
            {
                return unitID.Value; 
            }
            else
            {
                return -1;
            }
        }
    }
}
