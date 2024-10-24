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
    public interface IServiceFeedbackService
    {
        Task<IBusinessResult> GetAll(ServiceFeedbackFilterParam request);
        Task<IBusinessResult> GetById(string id);
        Task<IBusinessResult> Create(ServiceFeedbackDTO serviceFeedback);
        Task<IBusinessResult> Update(ServiceFeedbackDTO serviceFeedback);
        //Task<IBusinessResult> Save(ServiceFeedback serviceFeedback);
        Task<IBusinessResult> DeleteById(string Id);
    }
}
