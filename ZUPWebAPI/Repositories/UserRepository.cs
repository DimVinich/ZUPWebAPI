using ZUPWebAPI.Entities;

namespace ZUPWebAPI.Repositories
{
    public class UserRepository : BaseRepository
    {
        public IEnumerable<UserEntity> UserFindeByLogin(string login)
        {
            //  будет выдавать списко всех у кого есть такой логин,
            //  а определять нашли одного, нашли больше одного, никого не нашли,
            //  будем на уровне UserService

            return Query<UserEntity>("select id_kontr as id , n_kontr as name , login_www as login, pass_www as password" +
                " from spr_kontr with (nolock) where login_www = @login_p", new { login_p = login}).ToList() ;
        }
    }
}
