using KPCOS.Data.Models;
using KPCOS.Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPCOS.Service.Interface
{
    public interface IUserService
    {
        Task<IBusinessResult> GetAll();
        Task<IBusinessResult> GetById(string code);
        //Task<IBusinessResult> Create(User user);
        //Task<IBusinessResult> Update(User user);
        Task<IBusinessResult> Save(User user);
        Task<IBusinessResult> DeleteById(string Id);
    }
}
