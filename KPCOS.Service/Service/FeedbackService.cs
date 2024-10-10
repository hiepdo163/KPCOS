using KPCOS.Common;
using KPCOS.Data;
using KPCOS.Data.Models;
using KPCOS.Service.Base;
using KPCOS.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KPCOS.Service.Service
{
    public class FeedbackService : IFeedbackService
    {
        private readonly UnitOfWork _unitOfWork;
        public FeedbackService()
        {
            _unitOfWork ??= new UnitOfWork();
        }
        public async Task<IBusinessResult> GetAll()
        {
            #region Business rule
            #endregion

            var feedbacks = await _unitOfWork.Feedback.GetAllAsync();
            Console.WriteLine($"Feedback count: {feedbacks.Count()}");
            if (feedbacks == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new List<Feedback>());
            }
            else
            {
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, feedbacks);
            }
        }
        public async Task<IBusinessResult> GetById(string id)
        {
            var feedback = await _unitOfWork.Feedback.GetByIdAsync(id);
            if (feedback == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new Feedback());
            }
            else
            {
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, feedback);
            }
        }   
        public async Task<IBusinessResult> DeleteById(string id)
        {
            try
            {
                var feedback = await _unitOfWork.Feedback.GetByIdAsync(id);
                if (feedback == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new Feedback());
                }
                else
                {
                    var result = await _unitOfWork.Feedback.RemoveAsync(feedback);
                    if (result)
                    {
                        return new BusinessResult(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG, feedback);
                    }
                    else
                    {
                        return new BusinessResult(Const.FAIL_DELETE_CODE, Const.FAIL_DELETE_MSG, feedback);
                    }

                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.ToString());
            }
        }

        public async Task<IBusinessResult> Create(Feedback feedback)
        {
            if (feedback == null)
            {
                return new BusinessResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG);
            }
            try
            {
                var result = await _unitOfWork.Feedback.CreateAsync(feedback);
                if (result > 0)
                {
                    return new BusinessResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG);
                }
                else
                {
                    return new BusinessResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG);
                }

            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }



        public async Task<IBusinessResult> Update(Feedback feedback)
        {
            if (feedback == null)
            {
                return new BusinessResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);
            }
            try
            {
                var result = await _unitOfWork.Feedback.UpdateAsync(feedback);
                if (result > 0)
                {
                    return new BusinessResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG);
                }
                else
                {
                    return new BusinessResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION,ex.Message);
            }
        }
    }
}
