using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher06.CeGov.Domain
{
    public class NotFoundException : Exception
    {
        #region Properties
        /// <summary>
        /// Mã exception
        /// </summary>
        /// Created by: ddVang (11/9/2023)
        public int ErrorCode { get; set; }
        #endregion

        #region Contructor
        /// <summary>
        /// Hàm khỏi tạo mặc định
        /// </summary>
        /// Created by: ddVang (11/9/2023)
        public NotFoundException()
        {
            ErrorCode = 404;
        }

        /// <summary>
        /// Hàm khởi tạo với mã code
        /// </summary>
        /// <param name="errorCode">Code của exception</param>
        /// Created by: ddVang (11/9/2023)
        public NotFoundException(int errorCode)
        {
            ErrorCode = errorCode;
        }

        /// <summary>
        /// Hàm khởi tạo với thông tin lỗi
        /// </summary>
        /// <param name="message">Thông tin lỗi</param>
        /// Created by: ddVang (11/9/2023)
        public NotFoundException(string message) : base(message)
        {
            ErrorCode = 404;
        }

        /// <summary>
        /// Hàm khởi tạo với mã code và thông tin lỗi
        /// </summary>
        /// <param name="errorCode">Mã của exception</param>
        /// <param name="message">Thông tin lỗi</param>
        /// Created by: ddVang (11/9/2023)
        public NotFoundException(int errorCode, string message) : base(message)
        {
            ErrorCode = errorCode;
        }
        #endregion
    }
}