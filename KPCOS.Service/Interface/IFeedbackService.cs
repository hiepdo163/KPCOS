using KPCOS.Data.Models;
using KPCOS.Service.Base;
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
        //Task<IBusinessResult> Create(Feedback feedback);
        //Task<IBusinessResult> Update(Feedback feedback);
        Task<IBusinessResult> Save(Feedback feedback);
        Task<IBusinessResult> DeleteById(string Id);
    }
}
