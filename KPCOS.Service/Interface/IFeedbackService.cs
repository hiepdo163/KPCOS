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
    public interface IFeedbackService
    {
        Task<IBusinessResult> GetAll();
        Task<IBusinessResult> GetById(string code);
        Task<IBusinessResult> Create(FeedbackDTO feedback);
        Task<IBusinessResult> Update(FeedbackDTO feedback);
        //Task<IBusinessResult> Save(Feedback feedback);
        Task<IBusinessResult> DeleteById(string Id);
        Task<IBusinessResult> GetByProjectId(string projectId);
    }
}
