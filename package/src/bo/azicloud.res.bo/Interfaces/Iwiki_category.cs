using azicloud.res.bo.Common;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace azicloud.res.bo.Interfaces
{
    public interface Iwiki_category
    {
        Task<SqlMapper.GridReader> wiki_category_Filter_Rows1();
        Task<Result<dynamic>> wiki_category_Create1<T>(int id,string name, string description);
        Task<bool> wiki_category_Delete1(int Id);
    }
}
