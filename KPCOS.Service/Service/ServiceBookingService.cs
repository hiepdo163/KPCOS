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
using KPCOS.Service.DTOs;

namespace KPCOS.Service.Service
{
    public class ServiceBookingService : IServiceBookingService
    {
        private readonly UnitOfWork _unitOfWork;
        public ServiceBookingService()
        {
            _unitOfWork ??= new UnitOfWork();
        }
        public async Task<IBusinessResult> GetAll()
        {
            #region Business rule
            #endregion

            var serviceBookings = await _unitOfWork.ServiceBooking.GetServiceBookingsAsync();
            if (serviceBookings == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new List<ServiceBooking>());
            }
            else
            {
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, serviceBookings);
            }
        }
        public async Task<IBusinessResult> GetById(string id)
        {
            var serviceBooking = await _unitOfWork.ServiceBooking.GetAServiceBookingByIdAsync(id);
            if (serviceBooking == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new ServiceBooking());
            }
            else
            {
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, serviceBooking);
            }
        }
        //public async Task<IBusinessResult> Save(ServiceBooking serviceBooking)
        //{
        //    try
        //    {
        //        int result = -1;
        //        var serviceBookingTmp = await _unitOfWork.ServiceBooking.GetByIdAsync(serviceBooking.Id);
        //        if (serviceBookingTmp != null)
        //        {
        //            serviceBookingTmp.CustomerId = serviceBookingTmp.CustomerId;
        //            serviceBookingTmp.ServiceType = serviceBookingTmp.ServiceType;
        //            serviceBookingTmp.BookingDate = serviceBookingTmp.BookingDate;
        //            serviceBookingTmp.ScheduleType = serviceBookingTmp.ScheduleType;
        //            serviceBookingTmp.Status = serviceBooking.Status;
        //            serviceBookingTmp.StartDate = serviceBookingTmp.StartDate;
        //            serviceBookingTmp.EndDate = serviceBookingTmp.EndDate;
        //            serviceBookingTmp.Note = serviceBookingTmp.Note;

        //            result = await _unitOfWork.ServiceBooking.UpdateAsync(serviceBookingTmp);
        //            if (result > 0)
        //            {
        //                return new BusinessResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG, serviceBooking);
        //            }
        //            else
        //            {
        //                return new BusinessResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);
        //            }
        //        }
        //        else
        //        {
        //            result = await _unitOfWork.ServiceBooking.CreateAsync(serviceBooking);
        //            if (result > 0)
        //            {
        //                return new BusinessResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG, serviceBooking);
        //            }
        //            else
        //            {
        //                return new BusinessResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG, serviceBooking);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return new BusinessResult(Const.ERROR_EXCEPTION, ex.ToString());
        //    }
        //}

        public async Task<IBusinessResult> Create(ServiceBookingDTO request)
        {
            if (request is null)
            {
                return new BusinessResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG);
            }
            try
            {
                var serviceBooking = new ServiceBooking
                {
                    Id = Guid.NewGuid().ToString(),
                    CustomerId = request.CustomerId,
                    ServiceType = request.ServiceType,
                    BookingDate = DateTime.Now,
                    ScheduleType = request.ScheduleType,
                    StartDate = request.StartDate,
                    EndDate = request.EndDate,
                    Status = request.Status,
                    Note = request.Note,
                };
                var result = await _unitOfWork.ServiceBooking.CreateAsync(serviceBooking);
                if (result > 0)
                {
                    return new BusinessResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG, serviceBooking);
                }
                else
                {
                    return new BusinessResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG, serviceBooking);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.ToString());
            }
        }

        public async Task<IBusinessResult> Update(string id, ServiceBookingDTO request)
        {
            try
            {
                var serviceBooking = await _unitOfWork.ServiceBooking.GetByIdAsync(id);
                if (serviceBooking == null)
                {
                    return new BusinessResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);
                }

                serviceBooking.CustomerId = request.CustomerId;
                serviceBooking.ServiceType = request.ServiceType;
                serviceBooking.StartDate = request.StartDate;
                serviceBooking.EndDate = request.EndDate;
                serviceBooking.ScheduleType = request.ScheduleType;
                serviceBooking.Status = request.Status;
                serviceBooking.Note = request.Note;

                var result = await _unitOfWork.ServiceBooking.UpdateAsync(serviceBooking);
                if (result > 0)
                {
                    return new BusinessResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG, serviceBooking);
                }
                else
                {
                    return new BusinessResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG, serviceBooking);
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
                var serviceBooking = await _unitOfWork.ServiceBooking.GetByIdAsync(id);
                if (serviceBooking == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new ServiceBooking());
                }
                else
                {
                    var assignments = await _unitOfWork.ServiceAssignment.GetAssignmentsByServiceBookingIdAsync(id);
                    var executions = await _unitOfWork.ServiceExecution.GetExecutionsByServiceBookingIdAsync(id);
                    foreach (var assignment in assignments)
                    {
                        assignment.Status = "Canceled";
                        await _unitOfWork.ServiceAssignment.UpdateAsync(assignment);
                    }
                    foreach (var execution in executions)
                    {
                        execution.Status = "Canceled";
                        await _unitOfWork.ServiceExecution.UpdateAsync(execution);
                    }
                    serviceBooking.Status = "Canceled";
                    var result = await _unitOfWork.ServiceBooking.UpdateAsync(serviceBooking);
                    if (result > 0)
                    {
                        return new BusinessResult(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG, serviceBooking);
                    }
                    else
                    {
                        return new BusinessResult(Const.FAIL_DELETE_CODE, Const.FAIL_DELETE_MSG, serviceBooking);
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
