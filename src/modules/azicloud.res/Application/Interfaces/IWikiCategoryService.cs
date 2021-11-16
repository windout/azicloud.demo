using azicloud.res.Application.Models;
using azicloud.res.bo.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace azicloud.res.Application.Interfaces
{
    public interface IWikiCategoryService
    {
        Task<List<Category>> Filter_Rows1();
        Task<string> Delete1(int id);
        Task<Result<dynamic>> Create1(Category category);
    }
}
