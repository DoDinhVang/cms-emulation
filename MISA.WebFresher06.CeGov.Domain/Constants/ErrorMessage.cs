using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher06.CeGov.Domain.Constants
{
    public class ErrorMessage
    {
        public const string ExistedCode = "Mã danh hiệu đã tồn tại";
        public const string NonExistingIds = "Ids không có trong hệ thống";
        public const string BadRequest = "Dữ liệu gửi lên không hợp lệ";
        public const string UnknowError = "Lỗi không khác định. Vui lòng liên hệ tới MISA";
        public const string NotFound = "Không tìm thấy dữ liệu";
    }
}
