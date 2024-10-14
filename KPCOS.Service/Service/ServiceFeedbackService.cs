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
using KPCOS.Data.Repository;
using System.Runtime.CompilerServices;
using KPCOS.Service.DTOs;

namespace KPCOS.Service.Service
{
    public class ServiceFeedbackService : IServiceFeedbackService
    {
        private readonly ServiceBookingRepository _serviceBookingRepository;
        private readonly CustomerRepository _customerRepository;

        private readonly UnitOfWork _unitOfWork;
        public ServiceFeedbackService()
        {
            _unitOfWork ??= new UnitOfWork();
            _serviceBookingRepository = new ServiceBookingRepository();
            _customerRepository = new CustomerRepository();
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

        public async Task<IBusinessResult> Create(ServiceFeedbackDTO serviceFeedbackDto)
        {
            if (serviceFeedbackDto == null)
            {
                return new BusinessResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG);
            }
            var serviceBookingExist = await _serviceBookingRepository.GetByIdAsync(serviceFeedbackDto.ServiceBookingId);
            if(serviceBookingExist == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, "ServiceBooking does not exist");
            }
            var customerExist = await _customerRepository.GetByIdAsync(serviceFeedbackDto.CustomerId);
            if (customerExist == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, "Customer does not exist");
            }
            try
            {
                var serviceFeedback = new ServiceFeedback
                {
                    Id = Guid.NewGuid().ToString(),
                    Feedback = serviceFeedbackDto.Feedback,
                    ServiceBookingId = serviceFeedbackDto.ServiceBookingId,
                    CustomerId = serviceFeedbackDto.CustomerId,
                    Rating = serviceFeedbackDto.Rating,
                    CreateDate = DateTime.Now,
                };

                var result = await _unitOfWork.ServiceFeedback.CreateAsync(serviceFeedback);
                if (result > 0)
                {
                    return new BusinessResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG, serviceFeedback);
                   
                }
                return new BusinessResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG);
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);

            }
        }

        public async Task<IBusinessResult> Update(ServiceFeedbackDTO serviceFeedbackDto)
        {
            if (serviceFeedbackDto == null)
            {
                return new BusinessResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);
            }
            try
            {
                var existingServiceFeedback = await _unitOfWork.ServiceFeedback.GetByIdAsync(serviceFeedbackDto.ID);
                if (existingServiceFeedback == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
                }
                existingServiceFeedback.Feedback = serviceFeedbackDto.Feedback;
                existingServiceFeedback.Rating = serviceFeedbackDto.Rating;
                existingServiceFeedback.ServiceBookingId = serviceFeedbackDto.ServiceBookingId;
                existingServiceFeedback.CustomerId = serviceFeedbackDto.CustomerId; 
                var result = await _unitOfWork.ServiceFeedback.UpdateAsync(existingServiceFeedback);

                if (result > 0)
                {
                    return new BusinessResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG, existingServiceFeedback);
                }

                return new BusinessResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);

            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }
    }
}


