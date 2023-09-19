using Dapper;
using MISA.WebFresher06.CeGov.Domain;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher06.CeGov.Infrastructure;

public abstract class BaseReadOnlyRepository<TEntity, TKey> : IReadOnlyRepository<TEntity, TKey> where TEntity : IEntity<TKey>
{
    protected readonly string ConnectionString;
    protected virtual string TableName { get; set; } = typeof (TEntity).Name;

    public BaseReadOnlyRepository(string connectionString)
    {
        ConnectionString = connectionString;
    }


    public async Task<TEntity> GetAsync(TKey id)
    {
        var entity = await FindAsync(id);
        if (entity == null)
        {
            throw new NotFoundException();
        }
        return entity;
    }

    public async Task<List<TEntity>> GetListAsync()
    {
        var connection = new MySqlConnection(ConnectionString);
        var sql = $"SELECT * FROM {TableName} ORDER BY CreatedDate DESC;";
        var result = await connection.QueryAsync<TEntity>(sql);
        return result.ToList();
    }
    public async Task<TEntity?> FindAsync(TKey id)
    {
        var connection = new MySqlConnection(ConnectionString);
        var sql = $"SELECT * FROM  {TableName} e WHERE {TableName}Id=@id";
        var param = new DynamicParameters();
        param.Add(name: "id", value: id);
        var result = await connection.QuerySingleOrDefaultAsync<TEntity>(sql, param);
        return result;
    }

    public async Task<(IEnumerable<TEntity>, int, int, int)> GetPageDataAsync(int pageIndex, int pageSize, string? keyWord)
    {
        var connection = new MySqlConnection(ConnectionString);
        string query = $"SELECT * FROM {TableName} WHERE (@KeyWord IS NULL OR {TableName}Name LIKE CONCAT('%', @keyWord, '%') OR {TableName}Code LIKE CONCAT('%', @keyWord, '%')) ORDER BY CreatedDate DESC LIMIT @Offset, @PageSize;";
        string countQuery = $"SELECT COUNT(*) FROM {TableName} WHERE (@KeyWord IS NULL OR {TableName}Name LIKE CONCAT('%', @keyWord, '%'));";

        int offset = (pageIndex - 1) * pageSize;

        var result = await connection.QueryAsync<TEntity>(query, new { Offset = offset, PageSize = pageSize, KeyWord = keyWord });
        var totalItems = await connection.QuerySingleOrDefaultAsync<int>(countQuery, new { KeyWord = keyWord });
        return (result, pageIndex, pageSize, totalItems);
    }

    public async Task<List<TKey>> GetExistingIdsAsync(List<TKey> ids)
    {
        var connection = new MySqlConnection(ConnectionString);
        var query = $"SELECT {TableName}Id FROM {TableName} WHERE {TableName}Id IN @ids";
        var param = new DynamicParameters();
        param.Add(name: "ids", value: ids);
        var result = await connection.QueryAsync<TKey>(query, param);
        return  result.ToList();
    }
}
