using ZUPWebAPI.Entities;
using ZUPWebAPI.Models;

namespace ZUPWebAPI.Repositories
{
    public class TimeSheetRepository : BaseRepository 
    {
        public IEnumerable<TimeSheetEntity> TimeSheetGet(TimeSheetGetData timeSheetGetData)
        {
            return Query<TimeSheetEntity>(@"Select 
							TabelH.TheDate as Date
							, Case
								when TabelH.TypeDay = 'no' and TabelH.AmountHour > 0 then 'Я'
								when TabelH.TypeDay = 'no' and TabelH.AmountHour = 0 and TabelH.TypeDayOff > 10 then 'В'
								when TabelH.TypeDay = 'no' and TabelH.AmountHour = 0 and TabelH.TypeDayOff = 10 then 'Er'
								when TabelH.TypeDay <> 'no' then TabelH.TypeDay
							end as TypeDate
							, TabelH.AmountHour as Worked
							, TabelH.idEmployee as idEmployee

						from (
								select  Calendar.TheDate as TheDate 
									, isNull(TimeBoard.IdTypeWorkDays, 'no') as TypeDay
									, IsNull(TimeBoard.AmountHour, 0) as AmountHour
									, Calendar.TypeDate as TypeDayOff
									, TimeBoard.IdEmploer as idEmployee 
								from  TimeBoard with (nolock)
								inner join Calendar with (nolock) on   
									TimeBoard.Year = @Year
									and TimeBoard.Month = @Month
									and Month(Calendar.TheDate) = TimeBoard.Month
									and Year(Calendar.TheDate) = TimeBoard.Year
									and Day(Calendar.TheDate) = TimeBoard.NumDay
							) as TabelH

						inner join spr_kontr with (nolock) on
							spr_kontr.id_kontr = TabelH.idEmployee
							and spr_kontr.salary_incl > 0
							and (spr_kontr.id_kontr = @idEmployee or @idEmployee = 0)
							and (spr_kontr.id_torg = @idUnit or @idUnit = 0)
						", timeSheetGetData).ToList();
        }
    }
}
