using KPCOS.Common;
using KPCOS.Data.Models;
using KPCOS.Data;
using KPCOS.Service.Base;
using KPCOS.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KPCOS.Service.DTOs;

namespace KPCOS.Service.Service
{
    public class ServiceExecutionService : IServiceExecutionService
    {
        private readonly UnitOfWork _unitOfWork;
        public ServiceExecutionService()
        {
            _unitOfWork ??= new UnitOfWork();
        }
        public async Task<IBusinessResult> GetAll()
        {
            #region Business rule
            #endregion

            var serviceExecutions = await _unitOfWork.ServiceExecution.GetServiceExecutionsAsync();
            if (serviceExecutions == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new List<ServiceExecution>());
            }
            else
            {
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, serviceExecutions);
            }
        }
        public async Task<IBusinessResult> GetById(string id)
        {
            var serviceExecution = await _unitOfWork.ServiceExecution.GetAServiceExecutionByIdAsync(id);
            if (serviceExecution == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new ServiceExecution());
            }
            else
            {
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, serviceExecution);
            }
        }
        //public async Task<IBusinessResult> Save(ServiceExecution serviceExecution)
        //{
        //    try
        //    {
        //        int result = -1;
        //        var serviceExecutionTmp = await _unitOfWork.ServiceExecution.GetByIdAsync(serviceExecution.Id);
        //        if (serviceExecutionTmp != null)
        //        {
        //            serviceExecutionTmp.ServiceBookingId = serviceExecution.ServiceBookingId;
        //            serviceExecutionTmp.EmployeeId = serviceExecution.EmployeeId;
        //            serviceExecutionTmp.ExecutionDate = serviceExecution.ExecutionDate;
        //            serviceExecutionTmp.Status = serviceExecution.Status;
        //            serviceExecutionTmp.Result = serviceExecution.Result;

        //            result = await _unitOfWork.ServiceExecution.UpdateAsync(serviceExecutionTmp);
        //            if (result > 0)
        //            {
        //                return new BusinessResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG, serviceExecution);
        //            }
        //            else
        //            {
        //                return new BusinessResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);
        //            }
        //        }
        //        else
        //        {
        //            result = await _unitOfWork.ServiceExecution.CreateAsync(serviceExecution);
        //            if (result > 0)
        //            {
        //                return new BusinessResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG, serviceExecution);
        //            }
        //            else
        //            {
        //                return new BusinessResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG, serviceExecution);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return new BusinessResult(Const.ERROR_EXCEPTION, ex.ToString());
        //    }
        //}

        public async Task<IBusinessResult> Create(ServiceExecutionDTO request)
        {
            if (request is null)
            {
                return new BusinessResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG);
            }
            try
            {
                var serviceExecution = new ServiceExecution
                {
                    Id = Guid.NewGuid().ToString(),
                    ServiceBookingId = request.ServiceBookingId,
                    EmployeeId = request.EmployeeId,
                    ExecutionDate = request.ExecutionDate,
                    Result = request.Result,
                    Status = request.Status
                };
                var result = await _unitOfWork.ServiceExecution.CreateAsync(serviceExecution);
                if (result > 0)
                {
                    return new BusinessResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG, serviceExecution);
                }
                else
                {
                    return new BusinessResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG, serviceExecution);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.ToString());
            }
        }

        public async Task<IBusinessResult> Update(string id, ServiceExecutionDTO request)
        {
            try
            {
                var serviceExecution = await _unitOfWork.ServiceExecution.GetByIdAsync(id);
                if (serviceExecution == null)
                {
                    return new BusinessResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);
                }

                serviceExecution.ServiceBookingId = request.ServiceBookingId;
                serviceExecution.EmployeeId = request.EmployeeId;
                serviceExecution.ExecutionDate = request.ExecutionDate;
                serviceExecution.Result = request.Result;
                serviceExecution.Status = request.Status;

                var result = await _unitOfWork.ServiceExecution.UpdateAsync(serviceExecution);
                if (result > 0)
                {
                    return new BusinessResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG, serviceExecution);
                }
                else
                {
                    return new BusinessResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG, serviceExecution);
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
                var serviceExecution = await _unitOfWork.ServiceExecution.GetByIdAsync(id);
                if (serviceExecution == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new ServiceExecution());
                }
                else
                {
                    serviceExecution.Status = "Canceled";
                    var result = await _unitOfWork.ServiceExecution.UpdateAsync(serviceExecution);
                    if (result > 0)
                    {
                        return new BusinessResult(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG, serviceExecution);
                    }
                    else
                    {
                        return new BusinessResult(Const.FAIL_DELETE_CODE, Const.FAIL_DELETE_MSG, serviceExecution);
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
