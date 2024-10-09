using KPCOS.Data.Models;
using KPCOS.Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPCOS.Service.Interface
{
    public interface IEmployeeService
    {
        Task<IBusinessResult> GetAll();
        Task<IBusinessResult> GetById(string code);
        Task<IBusinessResult> Create(Employee employee);
        Task<IBusinessResult> Update(Employee employee);
        //Task<IBusinessResult> Save(Employee employee);
        Task<IBusinessResult> DeleteById(string Id);
    }
}
