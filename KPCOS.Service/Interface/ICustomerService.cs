using KPCOS.Data.Models;
using KPCOS.Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPCOS.Service.Interface
{
    public interface ICustomerService
    {
        Task<IBusinessResult> GetAll();
        Task<IBusinessResult> GetById(string code);
        //Task<IBusinessResult> Create(Customer customer);
        //Task<IBusinessResult> Update(Customer customer);
        Task<IBusinessResult> Save(Customer customer);
        Task<IBusinessResult> DeleteById(string Id);
    }
}
