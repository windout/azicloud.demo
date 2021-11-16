using Dapper;
using System.Threading.Tasks;

namespace azicloud.res.bo.Interfaces
{
    public interface IDapperService
    {
        Task<SqlMapper.GridReader> ExecuteSpGridReader(string sql);
        Task<int> ExecuteSp(string sql, DynamicParameters parameters);
        Task<dynamic> ExecuteGetById<T>(string sql, int id);
    }
}
