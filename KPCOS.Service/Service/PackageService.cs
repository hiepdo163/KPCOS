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
    public class PackageService : IPackageService
    {
        private readonly UnitOfWork _unitOfWork;
        public PackageService()
        {
            _unitOfWork ??= new UnitOfWork();
        }
        public async Task<IBusinessResult> GetAll()
        {
            #region Business rule
            #endregion

            var packages = await _unitOfWork.Package.GetAllAsync();
            if (packages == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new List<Package>());
            }
            else
            {
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, packages);
            }
        }
        public async Task<IBusinessResult> GetById(string id)
        {
            var package = await _unitOfWork.Package.GetByIdAsync(id);
            if (package == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new Package());
            }
            else
            {
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, package);
            }
        }
        public async Task<IBusinessResult> Save(Package package)
        {
            try
            {
                int result = -1;
                var packageTmp = await _unitOfWork.Package.GetByIdAsync(package.Id);
                if (packageTmp != null)
                {
                    packageTmp.Description = package.Description;
                    packageTmp.Name = package.Name;
                    packageTmp.DiscountPercentage = package.DiscountPercentage;
                    packageTmp.Duration = package.Duration;
                    packageTmp.FeatureInclude = package.FeatureInclude;
                    packageTmp.Price = package.Price;

                    result = await _unitOfWork.Package.UpdateAsync(packageTmp);
                    if (result > 0)
                    {
                        return new BusinessResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG, package);
                    }
                    else
                    {
                        return new BusinessResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);
                    }
                }
                else
                {
                    result = await _unitOfWork.Package.CreateAsync(package);
                    if (result > 0)
                    {
                        return new BusinessResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG, package);
                    }
                    else
                    {
                        return new BusinessResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG, package);
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
                var package = await _unitOfWork.Package.GetByIdAsync(id);
                if (package == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new Package());
                }
                else
                {
                    var result = await _unitOfWork.Package.RemoveAsync(package);
                    if (result)
                    {
                        return new BusinessResult(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG, package);
                    }
                    else
                    {
                        return new BusinessResult(Const.FAIL_DELETE_CODE, Const.FAIL_DELETE_MSG, package);
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
