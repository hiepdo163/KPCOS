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
    public interface IServiceAssignmentService
    {
        Task<IBusinessResult> GetPage(QueryPagedServiceAssignment query);
        Task<IBusinessResult> GetAll();
        Task<IBusinessResult> GetById(string code);
        Task<IBusinessResult> Create(ServiceAssignmentDTO serviceAssignment);
        Task<IBusinessResult> Update(string id, ServiceAssignmentDTO serviceAssignment);
        //Task<IBusinessResult> Save(ServiceAssignment serviceAssignment);
        Task<IBusinessResult> DeleteById(string Id);
    }
}
