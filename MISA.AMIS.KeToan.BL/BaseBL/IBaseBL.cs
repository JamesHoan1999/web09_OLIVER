using MISA.AMIS.KeToan.Common.Entities;
using MISA.AMIS.KeToan.Common.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.KeToan.BL
{
    public interface IBaseBL<T>
    {
        /// <summary>
        /// Lấy danh sách tất cả bản ghi
        /// </summary>
        /// <returns>Danh sách tất cả bản ghi</returns>
        /// Author:HoanOliver(15/11/2022)
        public ResponseData GetAllRecords();

        /// <summary>
        /// Lấy thông tin 1 bản ghi theo ID
        /// </summary>
        /// <param name="recordID">ID của bản ghi </param>
        /// <returns>Thông tin chi tiết của bản ghi</returns>
        /// Author:HoanOliver(15/11/2022)
        public ResponseData GetRecordByID(Guid recordID);

        /// <summary>
        /// Xóa thông tin 1 bản ghi theo ID
        /// </summary>
        /// <param name="recordID">ID của bản ghi </param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        /// Author:HoanOliver(15/11/2022)
        public ResponseData DeleteRecordByID(Guid recordID);

        /// <summary>
        /// Thêm mới một bản ghi
        /// </summary>
        /// <param name="record"></param>Thông tin bản ghi muốn thêm
        /// <returns>Trả về lỗi nếu validate false hoặc trả về số bản ghi bị ảnh hưởng trong database</returns>
        /// Author:HoanOliver(17/11/2022)
        public ResponseData InsertOneRecord(T record);

        /// <summary>
        /// Sửa thông thin một bản ghi theo ID
        /// </summary>
        /// <param name="recordID"></param> ID của bản ghi
        /// <param name="record"></param> Thông tin bản ghi
        /// <returns>Trả về lỗi nếu validate false hoặc trả về số bản ghi bị ảnh hưởng trong database</returns>
        /// HoanOliver(18/11/2022)
        public ResponseData UpdateOneRecord(Guid recordID, T record);
    }
}
