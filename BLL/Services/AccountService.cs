using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using Arch.EntityFrameworkCore.UnitOfWork;
using AutoMapper;
using BLL.DTOs;
using BLL.Filters;
using BLL.Infrastructure;
using BLL.Interfaces;
using DAL.Models.Entitys;
using FilterBuilder.Infrastructure;

namespace BLL.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _db = null;
        private readonly IMapper _mapper = null;
        public AccountService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _db = unitOfWork;
            _mapper = mapper;
        }

        public OperationResult<IList<AccountDTO>> Read(AccountFilter filter = null)
        {
            Expression<Func<IQueryable<Account>, IOrderedQueryable<Account>>> expressionOrderByAccount = null;
            Expression<Func<Account, bool>> expressionWhereAccount = new ExpressionMaker().GetExpressionWhere<Account, AccountDTO>(filter, _mapper);

            if (filter?.Sort != null)
            {
                expressionOrderByAccount = source => source.OrderBy(string.Join(",", filter.Sort.Select(item => item.GetSortCriteria())));
            }

            IList<Account> result = null;

            if (filter?.Paging != null)
            {
                result = _db.GetRepository<Account>().GetPagedList(expressionWhereAccount, expressionOrderByAccount?.Compile(), pageIndex: (int)filter?.Paging?.Page, pageSize: (int)filter?.Paging?.PageItems).Items;
            }
            else
            {
                result = _db.GetRepository<Account>().GetPagedList(expressionWhereAccount, expressionOrderByAccount?.Compile()).Items;
            }

            return OperationResult<IList<AccountDTO>>.Success(_mapper.Map<IList<AccountDTO>>(result));
        }

        public OperationResult Create(AccountDTO account)
        {
            try
            {
                _db.GetRepository<Account>().Insert(_mapper.Map<Account>(account));
                _db.SaveChanges();
                return OperationResult.Success("Новый аккаунт успешно создан.", "BLL.Services.AccountService.Create");
            }
            catch (Exception e)
            {
                return OperationResult.Exception(e);
            }
        }

        public OperationResult Update(Guid id, AccountDTO account)
        {
            var oldAccount = _db.GetRepository<Account>().GetFirstOrDefault(a => a.Id == id, null, null, true, false);
            if (oldAccount == null)
            {
                return OperationResult.Exception($"Аккаунт с id {id} не найден.", "BLL.Services.AccountService.Update");
            }
            account.Id = oldAccount.Id;
            try
            {
                _db.GetRepository<Account>().Update(_mapper.Map<Account>(account));
                _db.SaveChanges();
                return OperationResult.Success($"Аккаунт {id} успешно обновлен.", "BLL.Services.AccountService.Update");
            }
            catch (Exception e)
            {
                return OperationResult.Exception(e);
            }
        }

        public OperationResult Delete(Guid id)
        {
            var account = _db.GetRepository<Account>().GetFirstOrDefault(a => a.Id == id, null, null, true, false);
            if (account == null)
            {
                return OperationResult.Exception($"Аккаунт с id {id} не найден.", "BLL.Services.AccountService.Delete");
            }
            try
            {
                _db.GetRepository<Account>().Delete(_mapper.Map<Account>(account));
                _db.SaveChanges();
                return OperationResult.Success($"Аккаунт {id} успешно удален.", "BLL.Services.AccountService.Delete");
            }
            catch (Exception e)
            {
                return OperationResult.Exception(e);
            }
        }
    }
}
