using azicloud.res.bo.Interfaces;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace azicloud.res.bo.DbContext
{
    public class DapperService : IDapperService
    {       
        private readonly IDbConnection db;
        public DapperService(string conStr)
        {           
            db = new SqlConnection(conStr);
        }

        public async Task<dynamic> ExecuteGetById<T>(string sql, int id)
        {
            var result = await db.QueryFirstOrDefaultAsync<T>(sql, new { id });
            return result;
        }
        public async Task<int> ExecuteSp(string sql, DynamicParameters parameters)
        {
            var result = await db.ExecuteScalarAsync<int>(sql,parameters,null,null, CommandType.StoredProcedure);
            return result;
        }
        
        public async Task<SqlMapper.GridReader> ExecuteSpGridReader(string sql)
        {
            return await db.QueryMultipleAsync(sql, CommandType.StoredProcedure);
        }       
    }
}
