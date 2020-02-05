using BLL.DTOs;
using FilterBuilder;

namespace BLL.Filters
{
    public class AccountFilter : Filter<AccountDTO>
    {
        public AccountFilter()
        {
            this.ApplyPendingOperation();
        }
    }
}
