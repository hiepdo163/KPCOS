using KPCOS.Data.Models;
using KPCOS.Service.Base;
using KPCOS.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPCOS.Service.Interface
{
    public interface IEmployeeService
    {
        Task<IBusinessResult> GetByPage(QueryPagedEmployee query);
        Task<IBusinessResult> GetAll();
        Task<IBusinessResult> GetById(string code);
        Task<IBusinessResult> Create(EmployeeDTO employee);
        Task<IBusinessResult> Update(string id, EmployeeDTO employee);
        //Task<IBusinessResult> Save(Employee employee);
        Task<IBusinessResult> DeleteById(string Id);
    }
}
