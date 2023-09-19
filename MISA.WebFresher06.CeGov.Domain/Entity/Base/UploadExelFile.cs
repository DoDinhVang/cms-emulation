using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher06.CeGov.Domain
{
    public class UploadExelFileRequest
    {
        public IFormFile File { get; set; }
    }
    public class UploadExelFileResponse
    {
        public bool ISuccess { get; set; }
        public string Message { get; set; }
    }
}
