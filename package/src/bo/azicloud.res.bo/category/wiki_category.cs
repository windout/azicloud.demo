using azicloud.res.bo.Common;
using azicloud.res.bo.Interfaces;
using Dapper;
using System.Threading.Tasks;

namespace azicloud.res.bo.category
{
    public class wiki_category:Iwiki_category
    {
        private readonly IDapperService _dapper;

        public wiki_category(IDapperService dapper)
        {
            _dapper = dapper;
        }

        public async Task<Result<dynamic>> wiki_category_Create1<T>(int id, string name,string description)
        {
            var parameters = new DynamicParameters();
            parameters.Add("Id", id);
            parameters.Add("uId", 4);
            parameters.Add("parent_id", null);
            parameters.Add("image_id", null);
            parameters.Add("name",name);
            parameters.Add("description",description );
            var result = await _dapper.ExecuteSp("wiki_category_Create1", parameters);
            var check = await _dapper.ExecuteGetById<T>("SELECT C.*, CD.name, CD.description " +

                                                     "FROM wiki_category C "+

                                                     "inner join wiki_category_description CD on C.id = CD.category_id "+

                                                     "where C.deleted = 'false' and C.id = @id ", result);   
            return new Result<dynamic>()
            {                
                ResultObj = check
            }; 
        }
      
        public async Task<bool> wiki_category_Delete1(int Id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("intId",Id);
            parameters.Add("uId", 4);
            var result = await _dapper.ExecuteSp("wiki_category_Delete1",parameters);
            var check = await _dapper.ExecuteGetById<bool>("select deleted from wiki_category where id=@id", result);                        
            return check;
        }

        public async Task<SqlMapper.GridReader> wiki_category_Filter_Rows1()
        {
            var result = await _dapper.ExecuteSpGridReader("wiki_category_Filter_Rows1");
            return result;
        }
       
    }
}
