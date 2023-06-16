using Azure.Core.Pipeline;
using ZUPWebAPI.Entities;

namespace ZUPWebAPI.Repositories
{
    public class PostRepository : BaseRepository
    {
        //  Создать должность
        public int PostCreate(PostEntity postEntity)
        {
            int? postId = QueryFirstOrDefault<int>(@"insert into spr_post
															(	 n_post
																, idArt
																, idUnit
																, id_managment
																, KolStaffMember
															)
														value
															(	 @NPost
																, @IdExpense
																, @IdUnit
																, @IsChief
																, @NumberOfStaffUnits
															);
														SELECT CAST(SCOPE_IDENTITY() as int)", postEntity);
            if (postId != null)
            {
                return postId.Value;
            }
            else
            {
                return -1;
            }
        }

        //  Изменить должность
        public int PostChange(PostEntity postEntity)
        {
            return Execute(@"update spr_post
	                            set  n_post = @NPost
		                            , idArt = @IdExpense
		                            , idUnit = @IdUnit
		                            , id_managment = @IsChief
		                            , KolStaffMember = @NumberOfStaffUnits
                            where id_post = @IdPost", postEntity);
        }

        //  Удалить должность (пометить на удаление)
        public int PostDelete(int idPost)
        {
            return Execute(@"update spr_post
	                            set  idStatus = -1
                            where id_post = @idPost_p", new { idPost_p = idPost });
        }
    }
}
