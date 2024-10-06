using KPCOS.Common;
using KPCOS.Data;
using KPCOS.Data.Models;
using KPCOS.Service.Base;
using KPCOS.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPCOS.Service.Service
{
    public class PaymentPolicyService: IPaymentPolicyService
    {
        private readonly UnitOfWork _unitOfWork;
        public PaymentPolicyService()
        {
            _unitOfWork ??= new UnitOfWork();
        }
        public async Task<IBusinessResult> GetAll()
        {
            #region Business rule
            #endregion

            var payments = await _unitOfWork.PaymentPolicy.GetAllAsync();
            if (payments == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new List<PaymentPolicy>());
            }
            else
            {
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, payments);
            }
        }
        public async Task<IBusinessResult> GetById(string id)
        {
            var payment = await _unitOfWork.PaymentPolicy.GetByIdAsync(id);
            if (payment == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new PaymentPolicy());
            }
            else
            {
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, payment);
            }
        }
        public async Task<IBusinessResult> Save(PaymentPolicy payment)
        {
            try
            {
                int result = -1;
                var paymentTmp = await _unitOfWork.PaymentPolicy.GetByIdAsync(payment.Id);
                if (paymentTmp != null)
                {
                    paymentTmp.Description = payment.Description;
                    paymentTmp.Name = payment.Name;
                    paymentTmp.PaymentDeadlineDay = payment.PaymentDeadlineDay;
                    paymentTmp.PaymentMethodAvailable = payment.PaymentMethodAvailable;
                    paymentTmp.RefundPolicy = payment.RefundPolicy;
                    paymentTmp.UpdateDate = DateTime.Now;

                    result = await _unitOfWork.PaymentPolicy.UpdateAsync(paymentTmp);
                    if (result > 0)
                    {
                        return new BusinessResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG, payment);
                    }
                    else
                    {
                        return new BusinessResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);
                    }
                }
                else
                {
                    result = await _unitOfWork.PaymentPolicy.CreateAsync(payment);
                    if (result > 0)
                    {
                        return new BusinessResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG, payment);
                    }
                    else
                    {
                        return new BusinessResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG, payment);
                    }
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.ToString());
            }
        }
        public async Task<IBusinessResult> DeleteById(string id)
        {
            try
            {
                var payment = await _unitOfWork.PaymentPolicy.GetByIdAsync(id);
                if (payment == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new PaymentPolicy());
                }
                else
                {
                    var result = await _unitOfWork.PaymentPolicy.RemoveAsync(payment);
                    if (result)
                    {
                        return new BusinessResult(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG, payment);
                    }
                    else
                    {
                        return new BusinessResult(Const.FAIL_DELETE_CODE, Const.FAIL_DELETE_MSG, payment);
                    }

                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.ToString());
            }
        }
    }
}
