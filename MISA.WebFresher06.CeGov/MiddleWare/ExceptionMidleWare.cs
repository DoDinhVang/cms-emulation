using System.Resources;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using System;
using MISA.WebFresher06.CeGov.Domain;
using MISA.WebFresher06.CeGov.Domain.Constants;

namespace MISA.Web06.RESTful.Middleware
{
    public class ExceptionMiddleware
    {
        #region Properties
        private readonly RequestDelegate _requestDelegate;
        private readonly ILogger<ExceptionMiddleware> _logger;
        #endregion

        #region Contructor
        public ExceptionMiddleware(RequestDelegate requestDelegate, ILogger<ExceptionMiddleware> logger)
        {
            _requestDelegate = requestDelegate;
            _logger = logger;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Middleware xử lý lỗi
        /// </summary>
        /// <param name="context">Đối tượng HttpContext</param>
        /// <returns></returns>
        /// Created by: ddVang (11/9/2023)
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _requestDelegate(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="ex"></param>
        /// <returns></returns>
        /// Created by: ddVang (11/9/2023)
        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            context.Response.ContentType = "application/json";

            switch (ex)
            {
                // Trường hợp lỗi không tìm thấy dữ liệu
                case NotFoundException notFoundException:
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    await context.Response.WriteAsync(
                        text: new BaseException()
                        {
                            ErrorCode = ErrorCode.NotFound,
                            UserMessage = ErrorMessage.NotFound,
                            DevMessage = ex.Message,
                            TraceId = context.TraceIdentifier,
                            MoreInfo = ex.HelpLink
                        }.ToString() ?? ""
                    );
                    break;
                // Trường hợp lỗi xung đột request
                case ConflictException conflictException:
                    context.Response.StatusCode = StatusCodes.Status409Conflict;
                    await context.Response.WriteAsync(
                        text: new BaseException()
                        {
                            ErrorCode = ErrorCode.Conflict,
                            UserMessage = conflictException.Message ?? "",
                            DevMessage = ex.Message,
                            TraceId = context.TraceIdentifier,
                            MoreInfo = ex.HelpLink
                        }.ToString() ?? ""
                    );
                    break;
                // Lỗi nhập liệu từ người dùng
                case BadRequestException badRequestException:
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await context.Response.WriteAsync(
                        text: new BaseException()
                        {
                            ErrorCode = ErrorCode.BadRequest,
                            UserMessage = badRequestException.UserMsg ?? "",
                            DevMessage = ex.Message,
                            TraceId = context.TraceIdentifier,
                            MoreInfo = ex.HelpLink
                        }.ToString() ?? ""
                    );
                    break;
                // Lỗi mặc định
                default:
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    await context.Response.WriteAsync(
                       text: new BaseException()
                       {
                           ErrorCode = ErrorCode.UnknowError,
                           UserMessage = ErrorMessage.UnknowError,
                           DevMessage = ex.Message,
                           TraceId = context.TraceIdentifier,
                           MoreInfo = ex.HelpLink
                       }.ToString() ?? ""
                   );
                    break;
            }
        }
        #endregion
    }
}