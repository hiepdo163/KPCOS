using KPCOS.Data.Models;
using KPCOS.Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPCOS.Service.Interface
{
    public interface IServiceAssignmentService
    {
        Task<IBusinessResult> GetAll();
        Task<IBusinessResult> GetById(string code);
        //Task<IBusinessResult> Create(ServiceAssignment serviceAssignment);
        //Task<IBusinessResult> Update(ServiceAssignment serviceAssignment);
        Task<IBusinessResult> Save(ServiceAssignment serviceAssignment);
        Task<IBusinessResult> DeleteById(string Id);
    }
}
