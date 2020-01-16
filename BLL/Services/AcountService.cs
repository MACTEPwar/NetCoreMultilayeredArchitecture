using System;
using System.Collections.Generic;
using System.Text;
using Arch.EntityFrameworkCore.UnitOfWork;
using DAL.Models.Entitys;

namespace BLL.Services
{
    public class AcountService
    {
        private readonly IUnitOfWork _db = null;
        public AcountService(IUnitOfWork unitOfWork)
        {
            _db = unitOfWork;
        }

        //public bool Create()
        //{
        //    _db.GetRepository<Account>().
        //}
    }
}
