using KPCOS.Data.Models;
using KPCOS.Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPCOS.Service.Interface
{
    public interface IPaymentPolicyService
    {
        Task<IBusinessResult> GetAll();
        Task<IBusinessResult> GetById(string code);
        //Task<IBusinessResult> Create(PaymentPolicy paymentPolicy);
        //Task<IBusinessResult> Update(PaymentPolicy paymentPolicy);
        Task<IBusinessResult> Save(PaymentPolicy paymentPolicy);
        Task<IBusinessResult> DeleteById(string Id);
    }
}
