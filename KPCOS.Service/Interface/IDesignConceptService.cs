using KPCOS.Data.Models;
using KPCOS.Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPCOS.Service.Interface
{
    public interface IDesignConceptService
    {
        Task<IBusinessResult> GetAll();
        Task<IBusinessResult> GetById(string code);
        //Task<IBusinessResult> Create(DesignConcept designConcept);
        //Task<IBusinessResult> Update(DesignConcept designConcept);
        Task<IBusinessResult> Save(DesignConcept designConcept);
        Task<IBusinessResult> DeleteById(string Id);
    }
}
