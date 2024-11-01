using KPCOS.Common;
using KPCOS.Data;
using KPCOS.Data.Models;
using KPCOS.Data.Repository;
using KPCOS.Service.Base;
using KPCOS.Service.DTOs;
using KPCOS.Service.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace KPCOS.Service.Service
{
    public class UserService: IUserService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        public UserService(IConfiguration configuration)
        {
            _unitOfWork ??= new UnitOfWork();
            _configuration = configuration;
        }
        public async Task<IBusinessResult> GetAll()
        {
            #region Business rule
            #endregion

            var users = await _unitOfWork.User.GetAllAsync();
            if (users == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new List<User>());
            }
            else
            {
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, users);
            }
        }
        public async Task<IBusinessResult> GetById(string id)
        {
            var user = await _unitOfWork.User.GetByIdAsync(id);
            if (user == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new User());
            }
            else
            {
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, user);
            }
        }
        public async Task<IBusinessResult> Save(User user)
        {
            try
            {
                int result = -1;
                var userTmp = await _unitOfWork.User.GetByIdAsync(user.Id);
                if (userTmp != null)
                {
                    userTmp.Address = user.Address;
                    userTmp.Password = user.Password;
                    userTmp.Fullname = user.Fullname;
                    userTmp.Phone = user.Phone;
                    userTmp.Role = user.Role;
                    userTmp.Status = user.Status;
                    userTmp.UpdateDate = DateTime.Now;
                    userTmp.Username = user.Username;

                    result = await _unitOfWork.User.UpdateAsync(user);
                    if (result > 0)
                    {
                        return new BusinessResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG, user);
                    }
                    else
                    {
                        return new BusinessResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);
                    }
                }
                else
                {
                    result = await _unitOfWork.User.CreateAsync(user);
                    if (result > 0)
                    {
                        return new BusinessResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG, user);
                    }
                    else
                    {
                        return new BusinessResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG, user);
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
                var user = await _unitOfWork.User.GetByIdAsync(id);
                if (user == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new User());
                }
                else
                {
                    var employees = await _unitOfWork.Employee.GetEmployeesByUserIdAsync(id);
                    var customers = await _unitOfWork.Customer.GetCustomersByUserIdAsync(id);
                    foreach (var employee in employees)
                    {
                        var asignments = await _unitOfWork.ServiceAssignment.GetAssignmentsByEmployeeIdAsync(employee.Id);
                        var executions = await _unitOfWork.ServiceExecution.GetExecutionsByEmployeeIdAsync(employee.Id);
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
                        await _unitOfWork.Employee.RemoveAsync(employee);
                    }
                    foreach (var customer in customers)
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
                        await _unitOfWork.Customer.RemoveAsync(customer);
                    }
                    var result = await _unitOfWork.User.RemoveAsync(user);
                    if (result)
                    {
                        return new BusinessResult(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG, user);
                    }
                    else
                    {
                        return new BusinessResult(Const.FAIL_DELETE_CODE, Const.FAIL_DELETE_MSG, user);
                    }

                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.ToString());
            }
        }

        public async Task<IBusinessResult> LoginAsync(LoginDTO loginDTO)
        {
            var user = await _unitOfWork.User.GetByUsernameAndPasswordAsync(loginDTO.Username, loginDTO.Password);

            if (user == null)
            {
                return new BusinessResult(Const.FAIL_READ_CODE, "Invalid username or password.");
            }

            var token = GenerateJwtToken(user);

            return new BusinessResult(Const.SUCCESS_READ_CODE, "Login successful.", new { token });
        }
        public string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JwtConfig:Secret"]);
            var secretkey = _configuration["JwtConfig:Secret"];
            Console.WriteLine($"SecretKey:{secretkey}");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddHours(2), 
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<IBusinessResult> GetAllByRole(string role)
        {
            #region Business rule
            #endregion

            var users = await _unitOfWork.User.GetCustomersByRoleAsync(role);
            if (users == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new List<User>());
            }
            else
            {
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, users);
            }
        }
    }
}
