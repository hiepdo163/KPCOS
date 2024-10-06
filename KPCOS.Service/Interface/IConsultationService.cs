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
    public interface IConsultationService
    {
        Task<IBusinessResult> GetAll();
        Task<IBusinessResult> GetById(string code);
        //Task<IBusinessResult> Create(Consultation consultation);
        //Task<IBusinessResult> Update(Consultation consultation);
        Task<IBusinessResult> Save(Consultation consultation);
        Task<IBusinessResult> DeleteById(string Id);
    }
}
