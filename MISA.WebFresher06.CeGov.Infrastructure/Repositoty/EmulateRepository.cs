using Dapper;
using ExcelDataReader;
using MISA.WebFresher06.CeGov.Domain;
using MySqlConnector;
using System;
using System.Data;
using System.Reflection.Metadata;
using System.Security.AccessControl;
using static Dapper.SqlMapper;

namespace MISA.WebFresher06.CeGov.Infrastructure
{
    public class EmulateRepository : BaseCrudRepository<Emulate, Guid>,IEmulateRepository
    {
        public EmulateRepository(string connectionString) : base(connectionString)
        {

        }

        public async Task<int> ChangeMultipleStatus(List<Guid> ids, EmulateStatus status)
        {
            var connection = new MySqlConnection(ConnectionString);
            var sql = $"UPDATE emulate e SET e.Status = @status WHERE e.EmulateId IN @ids;";
            var result = await connection.ExecuteAsync(sql, new
            {
                ids = ids,
                status = status
            });
            return result;
        }

        public async Task<int> ChangeStatus(Guid id, EmulateStatus status)
        {
            var connection = new MySqlConnection(ConnectionString);
            var sql = $"UPDATE emulate e SET e.Status = @status WHERE e.EmulateId = @id;";
            var result = await connection.ExecuteAsync(sql, new
            { 
                id = id,
                status = status
            });
            return result;
        }

        public async Task<Emulate?> FindByEmulateCodeAsync(string code)
        {
            var connection = new MySqlConnection(ConnectionString);
            var sql = $"SELECT * FROM  Emulate e WHERE EmulateCode=@code";
            var param = new DynamicParameters();
            param.Add(name: "code", value: code);
            var result = await connection.QuerySingleOrDefaultAsync<Emulate>(sql, param);
            return result;
        }
        public async Task<List<EmulateDetail>> GetListEmulate()
        {
            var connection = new MySqlConnection(ConnectionString);
            var sql = $"SELECT EmulateId,EmulateName,EmulateCode,emulate.RewarderId,RewardType,RewardObj,Description,Status,emulate.CreatedDate,emulate.CreatedBy,emulate.ModifiedDate,emulate.ModifiedBy,RewarderName FROM emulate LEFT JOIN rewarder ON emulate.RewarderId = rewarder.RewarderId ORDER BY emulate.CreatedDate DESC;";
            var result = await connection.QueryAsync<EmulateDetail>(sql);
            return result.ToList();
        }

        public async Task<(IEnumerable<EmulateDetail>, int, int, int)> GetPageEmulateAsync(int pageIndex, int pageSize, string? keyWord,string? rewardObj, string? rewardType, Guid? rewarderId, int? status)
        {
            var connection = new MySqlConnection(ConnectionString);
            var query = $"SELECT EmulateId,EmulateName,EmulateCode,emulate.RewarderId,RewardType,RewardObj,Description,Status,emulate.CreatedDate,emulate.CreatedBy,emulate.ModifiedDate,emulate.ModifiedBy,RewarderName FROM emulate LEFT JOIN rewarder ON emulate.RewarderId = rewarder.RewarderId WHERE (@KeyWord IS NULL OR EmulateName LIKE CONCAT('%', @keyWord, '%') OR EmulateCode LIKE CONCAT('%', @keyWord, '%'))";

            // Thêm điều kiện lọc cho rewardObj (nếu có)
            if (!string.IsNullOrEmpty(rewardObj))
            {
                query += " AND RewardObj = @RewardObj";
            }

            // Thêm điều kiện lọc cho rewardType (nếu có)
            if (!string.IsNullOrEmpty(rewardType))
            {
                query += " AND RewardType = @RewardType";
            }

            // Thêm điều kiện lọc cho rewarderId (nếu có)
            if (rewarderId.HasValue)
            {
                query += " AND emulate.RewarderId = @RewarderId";
            }

            // Thêm điều kiện lọc cho status (nếu có)
            if (status.HasValue)
            {
                query += " AND Status = @Status";
            }

            query += " ORDER BY CreatedDate DESC LIMIT @Offset, @PageSize;";

            string countQuery = $"SELECT COUNT(*) FROM {TableName} WHERE (@KeyWord IS NULL OR {TableName}Name LIKE CONCAT('%', @keyWord, '%'))";

            // Thêm điều kiện lọc cho rewardObj (nếu có)
            if (!string.IsNullOrEmpty(rewardObj))
            {
                countQuery += " AND RewardObj = @RewardObj";
            }

            // Thêm điều kiện lọc cho rewardType (nếu có)
            if (!string.IsNullOrEmpty(rewardType))
            {
                countQuery += " AND RewardType = @RewardType";
            }

            // Thêm điều kiện lọc cho rewarderId (nếu có)
            if (rewarderId.HasValue)
            {
                countQuery += " AND emulate.RewarderId = @RewarderId";
            }

            // Thêm điều kiện lọc cho status (nếu có)
            if (status.HasValue)
            {
                countQuery += " AND Status = @Status;";
            }
            var offset = (pageIndex - 1) * pageSize;

            var result = await connection.QueryAsync<EmulateDetail>(query, new
            {
                Offset = offset,
                PageSize = pageSize,
                KeyWord = keyWord,
                RewardObj = rewardObj,
                RewardType = rewardType,
                RewarderId = rewarderId,
                Status = status
            });
            var totalItems = await connection.QuerySingleOrDefaultAsync<int>(countQuery, new { KeyWord = keyWord,
                RewardObj = rewardObj,
                RewardType = rewardType,
                RewarderId = rewarderId,
                Status = status 
            });
            return (result, pageIndex, pageSize, totalItems);
        }

        public async Task<UploadExelFileResponse> UploadExcelFile(UploadExelFileRequest request, string path)
        {
            UploadExelFileResponse response = new UploadExelFileResponse();
            List<ExcelUploadParameters> Parameters = new List<ExcelUploadParameters>();
            response.ISuccess = true;
            response.Message = "SuccessFully";
            try
            {
                var connection = new MySqlConnection(ConnectionString);
                if (request.File.FileName.ToLower().Contains(".xlsx")){
                    FileStream stream = new FileStream(path, FileMode.Open,FileAccess.Read, FileShare.ReadWrite);
                    System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                    IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream);
                    DataSet dataset = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        UseColumnDataType = false,
                        ConfigureDataTable = (TableReader)=>new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = true
                        }
                    });
                    for(int i = 0; i < dataset.Tables[0].Rows.Count; i++)
                    {
                        Guid resultGuid;
                        EmulateStatus status;
                        ExcelUploadParameters rows = new ExcelUploadParameters();
                        rows.EmulateName = dataset.Tables[0].Rows[i].ItemArray[0] != null ? Convert.ToString(dataset.Tables[0].Rows[i].ItemArray[0]) : null;

                        rows.EmulateCode = dataset.Tables[0].Rows[i].ItemArray[1] != null ? Convert.ToString(dataset.Tables[0].Rows[i].ItemArray[1]) : null;

                        rows.RewardObj = dataset.Tables[0].Rows[i].ItemArray[2] != null ? Convert.ToString(dataset.Tables[0].Rows[i].ItemArray[2]) : null;

                        var rewarderId = dataset.Tables[0].Rows[i].ItemArray[3] != null ? Convert.ToString(dataset.Tables[0].Rows[i].ItemArray[3]) : null;

                        if (Guid.TryParse(rewarderId, out resultGuid))
                        {
                            // Chuyển đổi thành công, resultGuid chứa giá trị Guid hợp lệ.
                            rows.RewarderId = resultGuid;
                        }
                        else
                        {
                            // Không thể chuyển đổi
                            response.ISuccess = false;
                            response.Message = "Không thể chuyển đổi string sang Guid";
                            return response;
                        }

                        rows.RewardType = dataset.Tables[0].Rows[i].ItemArray[4] != null ? Convert.ToString(dataset.Tables[0].Rows[i].ItemArray[4]) : null;

                        var statusString = dataset.Tables[0].Rows[i].ItemArray[5] != null ? Convert.ToString( dataset.Tables[0].Rows[i].ItemArray[5]) : null;

                        if (Enum.TryParse(statusString, out status))
                        {
                            rows.Status = status;
                        }
                        else
                        {
                            response.ISuccess = false;
                            response.Message = "Không thể chuyển đổi string sang                                      EmulateStatus";
                            return response;
                        }

                        Parameters.Add(rows);
                    }
                    stream.Close();
                    if(Parameters.Count > 0)
                    {
                        var query = $"INSERT INTO Emulate (EmulateId, EmulateName, EmulateCode, RewardObj, RewarderId, RewardType,Status, CreatedDate)VALUES (@EmulateId, @EmulateName, @EmulateCode, @RewardObj, @RewarderId, @RewardType, @Status, @CreatedDate);";
                        foreach(ExcelUploadParameters row in Parameters)
                        {
                            var param = new DynamicParameters();
                            param.Add(name: "@EmulateId", value: Guid.NewGuid());
                            param.Add(name: "@EmulateName", value: row.EmulateName);
                            param.Add(name: "@EmulateCode", value: row.EmulateCode);
                            param.Add(name: "@RewardObj", value: row.RewardObj);
                            param.Add(name: "@RewarderId", value: row.RewarderId);
                            param.Add(name: "@RewardType", value: row.RewardType);
                            param.Add(name: "@Status", value: row.Status);
                            param.Add(name: "@CreatedDate", value: DateTime.Now);
                            var result = await connection.ExecuteAsync(query, param);
                            if(result <= 0)
                            {
                                response.ISuccess = false;
                                response.Message = "Query not executed";
                                return response;
                            }

                        }
                    }
                }
                else
                {
                    response.ISuccess = false;
                    response.Message = "file không hợp lệ";
                    return response;
                }
               
            }
            catch (Exception ex)
            {
                response.ISuccess = false;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}