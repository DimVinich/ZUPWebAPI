using ZUPWebAPI.Entities;

namespace ZUPWebAPI.Repositories
{
    public class ExpenseRepository : BaseRepository
    {
        public IEnumerable<ExpenseEntity> ExpensesGet()
        {
            return Query<ExpenseEntity>("select id_art as idExpense, n_art as nExpense from spr_art with (nolock) where id_group2 = 6").ToList();
        }
    }
}
