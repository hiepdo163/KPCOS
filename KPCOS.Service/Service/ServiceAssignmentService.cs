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
    public class ServiceAssignmentService : IServiceAssignmentService
    {
        private readonly UnitOfWork _unitOfWork;
        public ServiceAssignmentService()
        {
            _unitOfWork ??= new UnitOfWork();
        }
        public async Task<IBusinessResult> GetAll(QueryPagedServiceAssignment query)
        {
            #region Business rule
            #endregion

            var serviceAssignments = await _unitOfWork.ServiceAssignment.GetServiceAssignmentsAsync();
            if (!string.IsNullOrEmpty(query.ServiceBookingId))
            {
                serviceAssignments = serviceAssignments.Where(e => e.ServiceBookingId == query.ServiceBookingId).ToList();
            }
            if (!string.IsNullOrEmpty(query.EmployeeId))
            {
                serviceAssignments = serviceAssignments.Where(e => e.EmployeeId == query.EmployeeId).ToList();
            }
            if (!string.IsNullOrEmpty(query.Status))
            {
                serviceAssignments = serviceAssignments.Where(e => e.Status == query.Status).ToList();
            }
            if (serviceAssignments == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new List<ServiceAssignment>());
            }
            else
            {
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, serviceAssignments);
            }
        }
        public async Task<IBusinessResult> GetById(string id)
        {
            var serviceAssignment = await _unitOfWork.ServiceAssignment.GetAServiceAssignmentByIdAsync(id);
            if (serviceAssignment == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new ServiceAssignment());
            }
            else
            {
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, serviceAssignment);
            }
        }
        //public async Task<IBusinessResult> Save(ServiceAssignment serviceAssignment)
        //{
        //    try
        //    {
        //        int result = -1;
        //        var serviceAssignmentTmp = await _unitOfWork.ServiceAssignment.GetByIdAsync(serviceAssignment.Id);
        //        if (serviceAssignmentTmp != null)
        //        {
        //            serviceAssignmentTmp.ServiceBookingId = serviceAssignment.ServiceBookingId;
        //            serviceAssignmentTmp.AssignDate = serviceAssignment.AssignDate;
        //            serviceAssignmentTmp.EmployeeId = serviceAssignment.EmployeeId;
        //            serviceAssignmentTmp.Status = serviceAssignment.Status;

        //            result = await _unitOfWork.ServiceAssignment.UpdateAsync(serviceAssignmentTmp);
        //            if (result > 0)
        //            {
        //                return new BusinessResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG, serviceAssignment);
        //            }
        //            else
        //            {
        //                return new BusinessResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);
        //            }
        //        }
        //        else
        //        {
        //            result = await _unitOfWork.ServiceAssignment.CreateAsync(serviceAssignment);
        //            if (result > 0)
        //            {
        //                return new BusinessResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG, serviceAssignment);
        //            }
        //            else
        //            {
        //                return new BusinessResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG, serviceAssignment);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return new BusinessResult(Const.ERROR_EXCEPTION, ex.ToString());
        //    }
        //}

        public async Task<IBusinessResult> Create(ServiceAssignmentDTO request)
        {
            if (request is null)
            {
                return new BusinessResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG);
            }
            try
            {
                var serviceAssignment = new ServiceAssignment
                {
                    Id = Guid.NewGuid().ToString(),
                    ServiceBookingId = request.ServiceBookingId,
                    EmployeeId = request.EmployeeId,
                    AssignDate = DateTime.Now,
                    Status = request.Status
                };
                var result = await _unitOfWork.ServiceAssignment.CreateAsync(serviceAssignment);
                if (result > 0)
                {
                    return new BusinessResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG, request);
                }
                else
                {
                    return new BusinessResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG, request);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.ToString());
            }
        }

        public async Task<IBusinessResult> Update(string id, ServiceAssignmentDTO request)
        {
            try
            {
                var serviceAssignment = await _unitOfWork.ServiceAssignment.GetByIdAsync(id);
                if (serviceAssignment == null)
                {
                    return new BusinessResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);
                }

                serviceAssignment.ServiceBookingId = request.ServiceBookingId;
                serviceAssignment.EmployeeId = request.EmployeeId;
                serviceAssignment.AssignDate = DateTime.Now;
                serviceAssignment.Status = request.Status;

                var result = await _unitOfWork.ServiceAssignment.UpdateAsync(serviceAssignment);
                if (result > 0)
                {
                    return new BusinessResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG, serviceAssignment);
                }
                else
                {
                    return new BusinessResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG, serviceAssignment);
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
                var serviceAssignment = await _unitOfWork.ServiceAssignment.GetByIdAsync(id);
                if (serviceAssignment == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new ServiceAssignment());
                }
                else
                {
                    serviceAssignment.Status = "Canceled";
                    var result = await _unitOfWork.ServiceAssignment.UpdateAsync(serviceAssignment);
                    if (result > 0)
                    {
                        return new BusinessResult(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG, serviceAssignment);
                    }
                    else
                    {
                        return new BusinessResult(Const.FAIL_DELETE_CODE, Const.FAIL_DELETE_MSG, serviceAssignment);
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
