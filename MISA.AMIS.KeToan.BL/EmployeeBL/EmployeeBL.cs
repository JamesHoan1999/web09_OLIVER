using MISA.AMIS.KeToan.Common.Entities;
using MISA.AMIS.KeToan.Common.Entities.DTO;
using MISA.AMIS.KeToan.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.KeToan.BL
{
    public class EmployeeBL :BaseBL<Employee>, IEmployeeBL
    {
        #region Field

        private IEmployeeDL _employeeDL;

        #endregion

        #region Constructor

        public EmployeeBL( IEmployeeDL employeeDL) :base(employeeDL )
        {
            _employeeDL= employeeDL;
        }

       


        #endregion
        #region Methods

        /// <summary>
        /// Lấy mã nhân viên mới
        /// </summary>
        /// <returns>Mã nhân viên mới </returns>
        /// Author:HoanOliver(15/11/2022)
        public ResponseData GetNewEmployeeCode()
        {
            string result= _employeeDL.GetNewEmployeeCode();
            if( result != null)
            {
                return new ResponseData(true, result);
            }
            return new ResponseData(false, null);
        }


        /// <summary>
        /// Lấy danh sách nhân viên theo tìm kiếm và phân trang
        /// </summary>
        /// <param name="pageSize"></param> Số bản ghi trên 1 trang
        /// <param name="pageNumber"></param>Trang muốn lấy
        /// <param name="keySearch"></param> Từ khóa tìm kiếm
        /// <returns>Danh sách nhân viên</returns>
        public ResponseData GetEmployeeFilter(int pageSize, int pageNumber, string? keySearch)
        {
            var result= _employeeDL.GetEmployeeFilter(pageSize, pageNumber, keySearch);
            if (result != null)
            {
                return new ResponseData(true, result);
            }
            return new ResponseData(false, null);
        }


        /// <summary>
        /// Xóa nhiều nhân viên
        /// </summary>
        /// <param name="listEmployeeID"></param> Danh sách ID nhân viên
        /// <returns>Trả về 1 nếu thành công,0 nếu thất bại</returns>
        /// Author:HoanOliver(20/11/2022)
        public ResponseData DeleteMultipleEmployee(ListEmployeeID listEmployeeID)
        {
            int success= _employeeDL.DeleteMultipleEmployee(listEmployeeID);
            if (success == 1)
            {
                return new ResponseData(true, 1);
            }
            else 
                return new ResponseData(false, "Xóa nhiều nhân viên không thành công");
        }
        #endregion
    }
}
