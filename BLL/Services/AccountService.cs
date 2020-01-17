using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Text;
using Arch.EntityFrameworkCore.UnitOfWork;
using AutoMapper;
using BLL.DTOs;
using BLL.Filters;
using BLL.Infrastructure;
using BLL.Interfaces;
using DAL.Models.Entitys;

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

            Expression<Func<Account, bool>> expressionWhereAccount = (a => true);//по умолчанию все
            ParameterExpression parameter = Expression.Parameter(typeof(Account), "a");
            Expression conditions = null;

            if (filter != null)
            {
                if (filter.IdEqual != null)
                {
                    Expression left = Expression.PropertyOrField(parameter, nameof(Account.Id));
                    Expression right = Expression.Constant(filter.IdEqual, typeof(Guid));
                    var condition = Expression.Equal(left, right);

                    conditions = conditions == null ? condition : Expression.AndAlso(conditions, condition);
                }

                if (!string.IsNullOrEmpty(filter.NameContains))
                {
                    var condition = Expression.Call(Expression.Property(parameter, nameof(Account.Name)), typeof(string).GetMethods().FirstOrDefault(f => f.Name == "Contains"), Expression.Constant(filter.NameContains));

                    if (conditions == null)
                        conditions = condition;
                    else
                        conditions = Expression.AndAlso(conditions, condition);
                }

                if (filter.Sorts.Any())
                {
                    expressionOrderByAccount = source => source.OrderBy(string.Join(",", filter.Sorts.Select(item => item.GetSortCriteria())));
                }
            }

            expressionWhereAccount = conditions == null ? expressionWhereAccount : Expression.Lambda<Func<Account, bool>>(conditions, parameter);

            IList<Account> accounts = null;

            if (filter?.Paging != null)
            {
                accounts = _db.GetRepository<Account>()
                 .GetPagedList(expressionWhereAccount, expressionOrderByAccount?.Compile(), pageIndex: filter.Paging.Page, pageSize: filter.Paging.PageItems)
                 .Items;
            }
            else
            {
                accounts = _db.GetRepository<Account>()
                 .GetPagedList(expressionWhereAccount, expressionOrderByAccount?.Compile())
                 .Items;
            }

            return OperationResult<IList<AccountDTO>>.Success(_mapper.Map<IList<AccountDTO>>(accounts));
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
