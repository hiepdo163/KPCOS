using KPCOS.Data.Models;
using KPCOS.Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPCOS.Service.Interface
{
    public interface IServiceExecutionService
    {
        Task<IBusinessResult> GetAll();
        Task<IBusinessResult> GetById(string id);
        //Task<IBusinessResult> Create(ServiceExecution serviceExecution);
        //Task<IBusinessResult> Update(ServiceExecution serviceExecution);
        Task<IBusinessResult> Save(ServiceExecution serviceExecution);
        Task<IBusinessResult> DeleteById(string id);
    }
}
