using MISA.AMIS.KeToan.Common.Entities;
using MISA.AMIS.KeToan.Common.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.KeToan.BL
{
    public interface IEmployeeBL : IBaseBL<Employee>
    {
        /// <summary>
        /// Lấy mã nhân viên mới
        /// </summary>
        /// <returns>Mã nhân viên mới </returns>
        /// Author:HoanOliver(15/11/2022)
        public ResponseData GetNewEmployeeCode();

        /// <summary>
        /// Lấy danh sách nhân viên theo tìm kiếm và phân trang
        /// </summary>
        /// <param name="pageSize"></param> Số bản ghi trên 1 trang
        /// <param name="pageNumber"></param>Trang muốn lấy
        /// <param name="keySearch"></param> Từ khóa tìm kiếm
        /// <returns>Danh sách nhân viên</returns>
        public ResponseData GetEmployeeFilter(int pageSize, int pageNumber, string? keySearch);

        /// <summary>
        /// Xóa nhiều nhân viên
        /// </summary>
        /// <param name="listEmployeeID"></param> Danh sách ID nhân viên
        /// <returns>Trả về 1 nếu thành công,0 nếu thất bại</returns>
        /// Author:HoanOliver(20/11/2022)
        public ResponseData DeleteMultipleEmployee(ListEmployeeID listEmployeeID);
    }
}
