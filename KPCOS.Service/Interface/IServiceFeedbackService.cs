using KPCOS.Data.Models;
using KPCOS.Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPCOS.Service.Interface
{
    public interface IServiceFeedbackService
    {
        Task<IBusinessResult> GetAll();
        Task<IBusinessResult> GetById(string id);
        //Task<IBusinessResult> Create(ServiceFeedback serviceFeedback);
        //Task<IBusinessResult> Update(ServiceFeedback serviceFeedback);
        Task<IBusinessResult> Save(ServiceFeedback serviceFeedback);
        Task<IBusinessResult> DeleteById(string Id);
    }
}
