using ZUPWebAPI.Entities;

namespace ZUPWebAPI.Repositories
{
    public class ExpenseRepository : BaseRepository
    {
        public IEnumerable<ExpenseEntity> ExpensesGet()
        {
            return Query<ExpenseEntity>("select id_art, n_art from spr_art where (id_group2 = 6 or id_group3 = 37) and idStatus > -1 order by n_art").ToList();
        }
    }
}
