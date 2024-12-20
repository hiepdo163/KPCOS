﻿using KPCOS.Common;
using KPCOS.Data;
using KPCOS.Data.Models;
using KPCOS.Service.Base;
using KPCOS.Service.DTOs;
using KPCOS.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPCOS.Service.Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly UnitOfWork _unitOfWork;
        public EmployeeService()
        {
            _unitOfWork ??= new UnitOfWork();
        }
        public async Task<IBusinessResult> GetByPage(QueryPagedEmployee query)
        {
            #region Business rule
            #endregion

            var employees = await _unitOfWork.Employee.GetEmployeesAsync();
            if (!string.IsNullOrEmpty(query.Name))
            {
                employees = employees.Where(e => e.User.Fullname.ToLower().Contains(query.Name.ToLower())).ToList();
            }
            if (query.FromSalary > 0)
            {
                employees = employees.Where(e => e.Salary > query.FromSalary).ToList();
            }
            if (query.ToSalary > 0)
            {
                employees = employees.Where(e => e.Salary < query.ToSalary).ToList();
            }

            if (employees == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new List<Employee>());
            }
            else
            {
                //employees = employees.Skip((query.PageNumber - 1) * 10).Take(10).ToList();
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, employees);
            }
        }
        public async Task<IBusinessResult> GetAll()
        {
            #region Business rule
            #endregion

            var employees = await _unitOfWork.Employee.GetEmployeesAsync();

            if (employees == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new List<Employee>());
            }
            else
            {
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, employees);
            }
        }
        public async Task<IBusinessResult> GetById(string id)
        {
            var employee = await _unitOfWork.Employee.GetAnEmployeeByIdAsync(id);
            if (employee == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new Employee());
            }
            else
            {
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, employee);
            }
        }
        //public async Task<IBusinessResult> Save(Employee employee)
        //{
        //    try
        //    {
        //        int result = -1;
        //        var employeeTmp = await _unitOfWork.Employee.GetByIdAsync(employee.Id);
        //        if (employeeTmp != null)
        //        {
        //            employeeTmp.Salary = employee.Salary;
        //            employeeTmp.SupervisorId = employee.SupervisorId;
        //            employeeTmp.UserId = employee.UserId;

        //            result = await _unitOfWork.Employee.UpdateAsync(employeeTmp);
        //            if (result > 0)
        //            {
        //                return new BusinessResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG, employee);
        //            }
        //            else
        //            {
        //                return new BusinessResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);
        //            }
        //        }
        //        else
        //        {
        //            result = await _unitOfWork.Employee.CreateAsync(employee);
        //            if (result > 0)
        //            {
        //                return new BusinessResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG, employee);
        //            }
        //            else
        //            {
        //                return new BusinessResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG, employee);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return new BusinessResult(Const.ERROR_EXCEPTION, ex.ToString());
        //    }
        //}

        public async Task<IBusinessResult> Create(EmployeeDTO request)
        {
            if (request is null)
            {
                return new BusinessResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG);
            }
            try
            {
                var employee = new Employee
                {
                    Id = Guid.NewGuid().ToString(),
                    Salary = request.Salary,
                    SupervisorId = request.SupervisorId,
                    UserId = request.UserId
                };

                var result = await _unitOfWork.Employee.CreateAsync(employee);
                if (result > 0)
                {
                    return new BusinessResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG, employee);
                }
                else
                {
                    return new BusinessResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG, employee);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.ToString());
            }
        }

            public async Task<IBusinessResult> Update(string id, EmployeeDTO request)
        {
            try
            {
                var employee = await _unitOfWork.Employee.GetByIdAsync(id);
                if (employee == null)
                {
                    return new BusinessResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);
                }

                employee.Salary = request.Salary;
                employee.SupervisorId = request.SupervisorId;
                employee.UserId = request.UserId;

                var result = await _unitOfWork.Employee.UpdateAsync(employee);
                if (result > 0)
                {
                    return new BusinessResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG, employee);
                }
                else
                {
                    return new BusinessResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG, employee);
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
                var employee = await _unitOfWork.Employee.GetByIdAsync(id);
                if (employee == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new Employee());
                }
                else
                {
                    var asignments = await _unitOfWork.ServiceAssignment.GetAssignmentsByEmployeeIdAsync(id);
                    var executions = await _unitOfWork.ServiceExecution.GetExecutionsByEmployeeIdAsync(id);
                    foreach (var assignment in asignments)
                    {
                        assignment.Status = "Canceled";
                        await _unitOfWork.ServiceAssignment.UpdateAsync(assignment);
                    }
                    foreach (var execution in executions)
                    {
                        execution.Status = "Canceled";
                        await _unitOfWork.ServiceExecution.UpdateAsync(execution);
                    }
                    var result = await _unitOfWork.Employee.RemoveAsync(employee);
                    if (result)
                    {
                        return new BusinessResult(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG, employee);
                    }
                    else
                    {
                        return new BusinessResult(Const.FAIL_DELETE_CODE, Const.FAIL_DELETE_MSG, employee);
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
