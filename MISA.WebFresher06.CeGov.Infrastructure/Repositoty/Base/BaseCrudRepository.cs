using Dapper;
using MISA.WebFresher06.CeGov.Domain;
using MISA.WebFresher06.CeGov.Infrastructure;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher06.CeGov.Infrastructure
{
    public abstract class BaseCrudRepository<TEntity, TKey> : BaseReadOnlyRepository<TEntity, TKey>, ICrudRepository<TEntity, TKey> where TEntity : IEntity<TKey>
    {
        protected BaseCrudRepository(string connectionString) : base(connectionString)
        {
        }

        public async Task<int> AddAsync(TEntity entity)
        {

            var properties = entity.GetType().GetProperties();  // Lấy danh sách PropertyInfo. Đây là một class chứa thông tin về tên thuộc tính, kiểu dữ liệu.
            var param = new DynamicParameters();
            var columns = new List<string>();
            var values = new List<string>();
            foreach (var property in properties)
            {
               
                var name = property.Name;
                param.Add($"@{name}", property.GetValue(entity));
                columns.Add(name);
                values.Add($"@{name}");
            }
            var columnsString = string.Join(", ", columns);
            var valuesString = string.Join(", ", values);
            var addQuery = $"INSERT INTO {TableName} ({columnsString}) VALUES ({valuesString})";
            var connection = new MySqlConnection(ConnectionString);
            var result = await connection.ExecuteAsync(addQuery, param);
            return result;
        }

        public async Task<int> DeleteAsync(TKey id)
        {
            var connection = new MySqlConnection(ConnectionString);
            var deleteQuery = $"DELETE FROM {TableName} WHERE {TableName}Id = @id";
            var param = new DynamicParameters();
            param.Add(name: "id", value: id);
            var result = await connection.ExecuteAsync(deleteQuery, param);
            return result;
        }

        public async Task<int> DeleteManyAsync(List<TKey> ids)
        {
            var connection = new MySqlConnection(ConnectionString);
            var deleteQuery = $"DELETE FROM {TableName} WHERE {TableName}Id IN @ids;";
            var param = new DynamicParameters();
            param.Add(name: "ids", value: ids);
            var result = await connection.ExecuteAsync(deleteQuery, param);
            return result;
        }

        public async Task<int> UpdateAsync(TKey id, TEntity entity)
        {
            var connection = new MySqlConnection(ConnectionString);
            var en = entity;
            var properties = entity.GetType().GetProperties().Where(p => p.Name != "CreatedDate" & p.Name != $"{TableName}Id").ToList();  // Lấy danh sách PropertyInfo. Đây là một class chứa thông tin về tên thuộc tính, kiểu dữ liệu.
            var param = new DynamicParameters();
            var values = new List<string>();
            
            foreach (var property in properties)
            {
                var name = property.Name;
                param.Add($"{name}", property.GetValue(entity));
                values.Add($"{name} = @{name}");
            }
            param.Add("Id", id);
            var valuesString = string.Join(", ", values);
            var updateQuery = $"UPDATE {TableName} SET {valuesString} WHERE {TableName}Id = @Id";
            var result = await connection.ExecuteAsync(updateQuery, param);
            return result;
        }
    }
}
