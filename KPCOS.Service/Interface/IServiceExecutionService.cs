using KPCOS.Common;
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
    public interface IServiceExecutionService
    {
        Task<IBusinessResult> GetPaged(QueryPagedServiceExecution query);
        Task<IBusinessResult> GetAll();
        Task<IBusinessResult> GetById(string id);
        Task<IBusinessResult> Create(ServiceExecutionDTO serviceExecution);
        Task<IBusinessResult> Update(string id, ServiceExecutionDTO serviceExecution);
        //Task<IBusinessResult> Save(ServiceExecution serviceExecution);
        Task<IBusinessResult> DeleteById(string id);
    }
}
