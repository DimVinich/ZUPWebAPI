﻿using ZUPWebAPI.Entities;

namespace ZUPWebAPI.Repositories
{
    public class EmployeeRepository : BaseRepository
    {
        // создать сотрудника
		//	возвращает код созданного сотрудника
        public int EmployeeCreate(EmployeeEntity employeeEntity)
        {
            int? employeeId = QueryFirstOrDefault<int>(@" 
						declare @idNewKontr int;
						select @idNewKontr = max(id_kontr)
						from spr_kontr;
						set @idNewKontr = @idNewKontr + 1;

						Insert into spr_kontr (
								id_kontr
								, n_kontr
								, n_kontr_full
								, n_kontr_short
								, n_boss
								, id_cond
								, el_mail
								, id_region -- 1 стоит.
								, id_road -- 73601  Воронеж ? С кладра потом забирать ?
								, delivery -- 10 самовывоз
								, location -- строка адреса, как доехать.
								, PostOfficeBox -- почтовый адрес
								, adress_ur
								, adress_fact
								, inn
								, id_agent -- 163536  Агент Х
								, employer -- 1
								, fpraisimag -- 1
								, id_torg -- Код отдела
								, id_post
								, seller_bayer -- 20 покупатель
								, id_direct
								, salary_incl -- 1 выплачивать
								, salary_type --  1  ЗС, 0 Касса
								, last_compare   -- когда начал работать
								, svid_nomer    -- паспорт номер
								, svid_vidano   -- паспорт кем выдан
								, sviddate -- паспорт когда выдан
								, id_firm	
								, faice   -- f - физическое, предпрениматель
								)
						values (
								@idNewKontr
								, @nEmployeeShort
								, @nEmployee
								, @nEmployee
								, @nEmployee
								, 10
								, @eMail
								, 1
								, 73601 -- 73601  Воронеж ? С кладра потом забирать ?
								, 10 -- 10 самовывоз
								, @ActualAddress -- строка адреса, как доехать.
								, @ActualAddress
								, @RegisteredAddress
								, @ActualAddress
								, @TIN
								, 163536 -- 163536  Агент Х
								, 1 -- 1
								, 1 -- 1
								, @idUnit -- Код отдела
								, @idPost
								, 20 -- 20 покупатель
								, 60
								, 1 -- 1 выплачивать
								, CAST( @TypeSalaryIssue as int ) --  1  ЗС, 0 Касса
								, @DateOfEmloyment   -- когда начал работать
								, @PassportNumbers    -- паспорт номер
								, @PassportIssued   -- паспорт кем выдан
								, @PassportDate -- паспорт когда выдан
								, 550861
								, 'f'
								);
							 SELECT @idNewKontr", employeeEntity);

            if (employeeId != null)
            {
                return employeeId.Value;
            }
            else
            {
                return -1;
            }

        }

        //  изменить сотрудника
		//	возвращает кол-во изменённых записей
        public int EmployeerCahnge(EmployeeEntity employeeEntity)
		{
			return Execute(@"update spr_kontr
								set n_kontr = @nEmployeeShort
								, n_kontr_full = @nEmployee
								, n_kontr_short = @nEmployee
								, n_boss = @nEmployee
								, id_cond = 10
								, el_mail = @eMail
								, id_region = 1 -- 1 стоит.
								, id_road = 73601 -- 73601  Воронеж ? С кладра потом забирать ?
								, delivery = 10 -- 10 самовывоз
								, location = @ActualAddress -- строка адреса, как доехать.
								, PostOfficeBox = @ActualAddress -- почтовый адрес
								, adress_ur = @RegisteredAddress
								, adress_fact = @ActualAddress
								, inn = @TIN
								, id_agent = 163536 -- 163536  Агент Х
								, employer = 1 -- 1
								, fpraisimag = 1  -- 1
								, id_torg = @idUnit -- Код отдела
								, id_post = @idPost
								, seller_bayer = 20 -- 20 покупатель
								, id_direct = 60
								, salary_incl = 1 -- 1 выплачивать
								, salary_type = CAST( @TypeSalaryIssue as int ) --  1  ЗС, 0 Касса
								, last_compare = @DateOfEmloyment   -- когда начал работать
								, svid_nomer = @PassportNumbers   -- паспорт номер
								, svid_vidano = @PassportIssued  -- паспорт кем выдан
								, sviddate = @PassportDate -- паспорт когда выдан
								, id_firm = 550861	
							where spr_kontr.id_kontr = @idEmployee", employeeEntity);
		}

        //  пометить сотрудника на удаление
		//	возвращает кол-во изменнёных строк
		public int EmployeerDelete(int idEmlpoyeer)
		{
			return Execute(@"update spr_kontr
					set id_cond = 30 
						, salary_incl = 0
						, salary_type = 0
				where spr_kontr.id_kontr = @idEmployeer_p" , new { idEmployeer_p = idEmlpoyeer});
		}

        //  найти сотрудника по ИНН ??
		//  возвращает список! кодов сотрудников.   Т.к. может быть ситуация, что сотрудников ИНН "11111" будет несколько
		public List<int> EmloyeerFindeByINN(string iNN)
		{
            return Query<int>(@"select id_kontr
								from spr_kontr with (nolock)
								where inn = Trim(@iNN_p)", new { iNN_p = iNN }).ToList();
        }

        //  найти сотрудника по фамили и ициниалам. 
        //	возвращает список! кодов сотрудников. т.к. можте быть сируация, что Сидоров.А.А. могу быть два сотрудника.
        public List<int> EmployeerFindeByName (string nEmployeer)
		{
            return Query<int>(@"select id_kontr
							from spr_kontr with (nolock)
							where n_kontr = Trim(@nEmployeer_p)", new { nEmployeer_p = nEmployeer }).ToList();
        }
		
		//	по коду сотрудника возвращает его имайл
		public string EmployeerGetMail (int employeerId)
		{
			return QueryFirstOrDefault<string>(@"select el_mail
												from spr_kontr with (nolock)
												where id_kontr = (@idKontr_p)", new { idKontr_p = employeerId });
        }
    }
}
