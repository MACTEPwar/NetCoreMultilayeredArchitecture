using System;
using System.Collections.Generic;
using System.Text;
using BLL.DTOs;
using BLL.Filters;
using BLL.Infrastructure;

namespace BLL.Interfaces
{
    public interface IAccountService
    {
        OperationResult<IList<AccountDTO>> Read(AccountFilter filter = null);
        OperationResult Create(AccountDTO account);
        OperationResult Update(Guid id, AccountDTO account);
        OperationResult Delete(Guid id);
    }
}
