using Dapper;
using MISA.AMIS.KeToan.Common;
using MISA.AMIS.KeToan.Common.Constants;
using MISA.AMIS.KeToan.Common.Entities;
using MISA.AMIS.KeToan.Common.Entities.DTO;
using MISA.AMIS.KeToan.DL;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MISA.AMIS.KeToan.BL
{
    public class BaseBL<T> : IBaseBL<T>
    {
        #region Field

        private IBaseDL<T> _baseDL;

        #endregion

        #region Constructor       
        public BaseBL(IBaseDL<T> baseDL)
        {
            _baseDL = baseDL;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Lấy danh sách tất cả bản ghi
        /// </summary>
        /// <returns>Danh sách tất cả bản ghi</returns>
        /// Author:HoanOliver(15/11/2022)
        public ResponseData GetAllRecords()
        {
            var result = _baseDL.GetAllRecords();
            if (result != null)
            {
                return new ResponseData(true, result);
            }
            return new ResponseData(false, null);
        }

        /// <summary>
        /// Lấy thông tin 1 bản ghi theo ID
        /// </summary>
        /// <param name="recordID">ID của bản ghi </param>
        /// <returns>Thông tin chi tiết của bản ghi</returns>
        /// Author:HoanOliver(15/11/2022)
        public ResponseData GetRecordByID(Guid recordID)
        {
            T result = _baseDL.GetRecordByID(recordID);
            if (result != null)
            {
                return new ResponseData(true, result);
            }
            else return new ResponseData(false, null);

        }

        /// <summary>
        /// Xóa thông tin 1 bản ghi theo ID
        /// </summary>
        /// <param name="recordID">ID của bản ghi </param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        /// Author:HoanOliver(15/11/2022)
        public ResponseData DeleteRecordByID(Guid recordID)
        {
            int result = _baseDL.DeleteRecordByID(recordID);
            if (result > 0)
            {
                return new ResponseData(true, 1);
            }
            return new ResponseData(false, 0);

        }




        /// <summary>
        /// Sửa thông tin một bản ghi
        /// </summary>
        /// <param name="recordID"></param>ID bản ghi muốn sửa
        /// <param name="record"></param>Thông tin bản ghi muốn sửa
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        /// Author:HoanOliver(17/11/2022)
        public ResponseData UpdateOneRecord(Guid recordID, T record)
        {

            var id = recordID;

            var result = ValidateData(recordID, record);
            if (result.Success == false)
            {
                return new ResponseData(false, result.Data);
            }
            return new ResponseData(true, _baseDL.UpdateOneRecord(recordID, record));

        }
        #endregion





        /// <summary>
        ///Thêm mới  một bản ghi
        /// </summary>
        /// <param name="record"></param>Thông tin bản ghi muốn thêm
        /// <returns>Trả về lỗi nếu validate false hoặc trả về số bản ghi bị ảnh hưởng trong database</returns>
        /// Author:HoanOliver(17/11/2022)
        public ResponseData InsertOneRecord(T record)
        {
            var result = ValidateData(null, record);
            if (result.Success == false)
            {
                return new ResponseData(false, result.Data);
            }
            return new ResponseData(true, _baseDL.InsertOneRecord(record));
        }

        /// <summary>
        /// Validate thông tin khi thêm mới hoặc sửa bản ghi
        /// </summary>
        /// <param name="recordID"></param> Nếu sửa thì truyền vào ID bản ghi
        /// <param name="record"></param>Thông tin bản ghi
        /// <returns>Trả về true nếu validate thành công, ngược lại trả về false và lỗi gặp phải</returns>
        public ResponseData ValidateData(Guid? recordID, T record)
        {

            var properties = record.GetType().GetProperties();
            foreach (var prop in properties)
            {
                var propName = prop.Name;
                // lấy giá trị của property truyền lên
                var propValue = prop.GetValue(record);

                bool isExist = true;
                if (propValue == null || propValue.ToString().Trim() == "")
                {
                    isExist = false;
                }

                // Kiểm tra xem property có attribute là endWithNumber không
                //var isEndWithNumber = Attribute.IsDefined(prop, typeof(EndWithNumberAttribute));

                // Kiểm tra xem property có attribute là isNotNullOrEmpty không
                var IsNotNullOrEmpty = Attribute.IsDefined(prop, typeof(IsNotNullOrEmptyAttribute));

                // Kiểm tra xem property có attribute là isEmail không
                var validateEmail = Attribute.IsDefined(prop, typeof(ValidateEmailAttribute));

                // Kiểm tra xem property có attribute là BirhOfDate không
                //var BirhOfDate = Attribute.IsDefined(prop, typeof(BirhOfDateAttribute));

                //Kiểm tra xem property có attribute là OnlyNumber không
                var isOnlyNumber = Attribute.IsDefined(prop, typeof(IsNumberAttribute));

                //Validate nếu có atrribute bắt buộc nhập
                if (IsNotNullOrEmpty == true)
                {
                    var attribute = prop.GetCustomAttributes(typeof(IsNotNullOrEmptyAttribute), true).FirstOrDefault();


                    var errorMessage = (attribute as IsNotNullOrEmptyAttribute).ErrorMessage;
                    if (propValue == null || propValue.ToString().Trim() == "")
                    {
                        return new ResponseData(false, errorMessage);
                    }
                }

                //Validate nếu có attribute email
                if (validateEmail == true && isExist == true)
                {
                    var attribute = prop.GetCustomAttributes(typeof(ValidateEmailAttribute), true).FirstOrDefault();
                    var errorMessage = (attribute as ValidateEmailAttribute).ErrorMessage;
                    bool checkEmail = IsValidEmail(propValue?.ToString());
                    if (!checkEmail)
                    {
                        return new ResponseData(false, errorMessage);
                    }
                }


                //Validate nếu có attribute  là các trường nhập số
                if (isOnlyNumber == true && isExist == true)
                {
                    //lấy ra attribute
                    var attribute = prop.GetCustomAttributes(typeof(IsNumberAttribute), true).FirstOrDefault();

                    // lấy ra regex 
                    var regex = new Regex((attribute as IsNumberAttribute).Format);

                    var errorMessage = (attribute as IsNumberAttribute).ErrorMessage;

                    if (!regex.IsMatch(propValue.ToString()))
                    {
                        return new ResponseData(false, errorMessage);
                    }
                }
            }


            //Validate trùng mã
            foreach (var prop in properties)
            {
                var propName = prop.Name;
                // lấy giá trị của property truyền lên
                var propValue = prop.GetValue(record);

                //Kiểm tra xem property có attribute là không được phép trùng 
                var isUnique = Attribute.IsDefined(prop, typeof(UniqueAttribute));

                //Validate nếu có atrribute không được phép trùng
                if (isUnique == true)
                {
                    var attribute = prop.GetCustomAttributes(typeof(UniqueAttribute), true).FirstOrDefault();


                    var errorMessage = (attribute as UniqueAttribute).ErrorMessage;


                    if (recordID == null)
                    {
                        int isDuplicate = _baseDL.CheckDuplicateCodeInsert((string)propValue);
                        if (isDuplicate == 0)
                        {
                            return new ResponseData(false, errorMessage);
                        }
                    }
                    else
                    {
                        int isDuplicate = _baseDL.CheckDuplicateCodeUpdate((Guid)recordID, (string)propValue);
                        if (isDuplicate == 0)
                        {
                            return new ResponseData(false, errorMessage);

                        }
                    }
                    break;
                }
            }


            return new ResponseData(true, null);
        }

        public bool IsValidEmail(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false;
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }




    }
}
