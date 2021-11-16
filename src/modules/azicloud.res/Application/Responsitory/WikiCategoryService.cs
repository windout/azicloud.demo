using azicloud.res.Application.Interfaces;
using azicloud.res.Application.Models;
using azicloud.res.bo.Common;
using azicloud.res.bo.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace azicloud.res.Application.Responsitory
{
    public class WikiCategoryService : IWikiCategoryService
    {
        private readonly Iwiki_category _Category;        
        public WikiCategoryService(Iwiki_category category)
        {
            _Category = category;
        }

        public async Task<Result<dynamic>> Create1(Category category)
        {
            var result = await _Category.wiki_category_Create1<Category>(category.Id, category.Name, category.Description);
            if(result.ResultObj is null)
            {
                result.IsSuccessed = false;
            }
            else
            {                
                result.IsSuccessed = true;
            }
            return result;
        }

        public async Task<string> Delete1(int id)
        {
            var result = await _Category.wiki_category_Delete1(id);
            if (result)
                return "Success";
            return "Fail";
        }

        public async Task<List<Category>> Filter_Rows1()
        {
            using var result = await _Category.wiki_category_Filter_Rows1();
            var list = result.Read<Category>().ToList();
            return list;
        }
    }
}
