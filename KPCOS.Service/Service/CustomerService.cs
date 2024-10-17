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
    public class CustomerService : ICustomerService
    {
        private readonly UnitOfWork _unitOfWork;
        public CustomerService()
        {
            _unitOfWork ??= new UnitOfWork();
        }
        public async Task<IBusinessResult> GetAll()
        {

            var customers = await _unitOfWork.Customer.GetCustomersAsync();
            if (customers == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new List<Customer>());
            }
            else
            {
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, customers);
            }


        }
        public async Task<IBusinessResult> GetById(string id)
        {
            var customer = await _unitOfWork.Customer.GetACustomerByIdAsync(id);
            if (customer == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new Customer());
            }
            else
            {
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, customer);
            }
        }
        public async Task<IBusinessResult> Save(Customer customer)
        {
            try
            {
                int result = -1;
                var customerTmp = await _unitOfWork.Customer.GetByIdAsync(customer.Id);
                if (customerTmp != null)
                {
                    customerTmp.LoyaltyPoint = customer.LoyaltyPoint;
                    customerTmp.MembershipStatus = customer.MembershipStatus;
                    customerTmp.UserId = customer.UserId;
                    customerTmp.PackageId = customer.PackageId;
                    
                    result = await _unitOfWork.Customer.UpdateAsync(customerTmp);
                    if (result > 0)
                    {
                        return new BusinessResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG, customer);
                    }
                    else
                    {
                        return new BusinessResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);
                    }
                }
                else
                {
                    result = await _unitOfWork.Customer.CreateAsync(customer);
                    if (result > 0)
                    {
                        return new BusinessResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG, customer);
                    }
                    else
                    {
                        return new BusinessResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG, customer);
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
                var customer = await _unitOfWork.Customer.GetByIdAsync(id);
                if (customer == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new Customer());
                }
                else
                {
                    var serviceBookings = await _unitOfWork.ServiceBooking.GetBookingsByCustomerIdAsync(id);
                    foreach (var booking in serviceBookings)
                    {
                        var assignments = await _unitOfWork.ServiceAssignment.GetAssignmentsByServiceBookingIdAsync(booking.Id);
                        var executions = await _unitOfWork.ServiceExecution.GetExecutionsByServiceBookingIdAsync(booking.Id);
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
                        booking.Status = "Canceled";
                        await _unitOfWork.ServiceBooking.UpdateAsync(booking);
                    }
                    var result = await _unitOfWork.Customer.RemoveAsync(customer);
                    if (result)
                    {
                        return new BusinessResult(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG, customer);
                    }
                    else
                    {
                        return new BusinessResult(Const.FAIL_DELETE_CODE, Const.FAIL_DELETE_MSG, customer);
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
