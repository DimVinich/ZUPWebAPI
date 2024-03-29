﻿using ZUPWebAPI.Entities;
using ZUPWebAPI.Models;

namespace ZUPWebAPI.Repositories
{
    public class KPIValueRepository : BaseRepository
    {
        // Метод который возвращает список занчений KPI на данный момент только для склада ВРН
        //  Как разделять отделы - пока на уровне класса сервиса, ни футуре ... по практике посмотрим.

        public IEnumerable<KPIValueEntity> KPIValuesSkladVRNGet( KPIValueGetData kPIValueGetData)
        {
            return Query<KPIValueEntity>(@"
					declare @DateS datetime, @DateE datetime
					set @DateS = Convert(datetime , Cast( @Year as char(4))+'.'+  Cast( @Month as char(2))+ '.01' )
					set @dateE = dateAdd(day, -Day(@DateS), dateAdd(month, 1, @DateS))

						SELECT 
						dd.idEmployee as idEmployee,
						dd.idValue as idKPI,
					--	dd.Value as ValuePlan,
					--	dd2.Value as ValueFact,
						Cast(Cast( dd2.Value as decimal) / cast(dd.Value as decimal) * 100 as Decimal(5,1) ) as Value
					--	spr_kontr.n_kontr
					FROM uf_CalcValueSkladEmployee(1, @DateS, @DateE, 0, 0, 1) dd
						inner join spr_kontr with (nolock)
						on spr_kontr.id_kontr = dd.idEmployee  
					inner join spr_post with (nolock)
						on dd.idpost = spr_post.id_post
					inner join (
							select 
								ddh.idEmployee as idEmployee
								, ddh.idValue as idValue
								, ddh.Value as Value
								, ddh.yy as yy
								, ddh.mm as mm
							from uf_CalcValueSkladEmployee(1, '2024.02.01', '2024.02.29', 0, 0, 1) as ddh
							where ddh.idValue = 40 
								and ddh.dd = -3
							) as dd2 on
							dd.idEmployee  = dd2.idEmployee
							and dd.yy = dd2.yy
							and dd.mm = dd2.mm
					where
						dd.idValue = 40 
						and dd.dd = -2
						and (dd.idEmployee = @idEmployee or @idEmployee = 0)
						and (spr_kontr.id_torg = @idUnit or @idUnit = 0)
					order by dd.idEmployee
                    ", kPIValueGetData).ToList();
        }
    }
}
