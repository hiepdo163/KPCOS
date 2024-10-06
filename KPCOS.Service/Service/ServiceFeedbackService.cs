using KPCOS.Common;
using KPCOS.Data.Models;
using KPCOS.Data;
using KPCOS.Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KPCOS.Service.Interface;

namespace KPCOS.Service.Service
{
    public class ServiceFeedbackService : IServiceFeedbackService
    {
        private readonly UnitOfWork _unitOfWork;
        public ServiceFeedbackService()
        {
            _unitOfWork ??= new UnitOfWork();
        }
        public async Task<IBusinessResult> GetAll()
        {
            #region Business rule
            #endregion

            var serviceFeedbacks = await _unitOfWork.ServiceFeedback.GetAllAsync();
            if (serviceFeedbacks == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new List<ServiceFeedback>());
            }
            else
            {
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, serviceFeedbacks);
            }
        }
        public async Task<IBusinessResult> GetById(string id)
        {
            var serviceFeedback = await _unitOfWork.ServiceFeedback.GetByIdAsync(id);
            if (serviceFeedback == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new ServiceFeedback());
            }
            else
            {
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, serviceFeedback);
            }
        }
        public async Task<IBusinessResult> Save(ServiceFeedback serviceFeedback)
        {
            try
            {
                int result = -1;
                var serviceFeedbackTmp = await _unitOfWork.ServiceFeedback.GetByIdAsync(serviceFeedback.Id);
                if (serviceFeedbackTmp != null)
                {
                    serviceFeedbackTmp.ServiceBookingId = serviceFeedback.ServiceBookingId;
                    serviceFeedbackTmp.CustomerId = serviceFeedback.CustomerId;
                    serviceFeedbackTmp.Rating = serviceFeedback.Rating;
                    serviceFeedbackTmp.Feedback = serviceFeedback.Feedback;

                    result = await _unitOfWork.ServiceFeedback.UpdateAsync(serviceFeedbackTmp);
                    if (result > 0)
                    {
                        return new BusinessResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG, serviceFeedback);
                    }
                    else
                    {
                        return new BusinessResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);
                    }
                }
                else
                {
                    result = await _unitOfWork.ServiceFeedback.CreateAsync(serviceFeedback);
                    if (result > 0)
                    {
                        return new BusinessResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG, serviceFeedback);
                    }
                    else
                    {
                        return new BusinessResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG, serviceFeedback);
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
                var serviceFeedback = await _unitOfWork.ServiceFeedback.GetByIdAsync(id);
                if (serviceFeedback == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new Consultation());
                }
                else
                {
                    var result = await _unitOfWork.ServiceFeedback.RemoveAsync(serviceFeedback);
                    if (result)
                    {
                        return new BusinessResult(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG, serviceFeedback);
                    }
                    else
                    {
                        return new BusinessResult(Const.FAIL_DELETE_CODE, Const.FAIL_DELETE_MSG, serviceFeedback);
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
