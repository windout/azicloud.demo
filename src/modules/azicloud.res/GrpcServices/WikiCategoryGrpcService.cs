using azicloud.res.bo.Interfaces;
using Grpc.Core;
using System.Linq;
using System.Threading.Tasks;

namespace azicloud.res.GrpcServices
{
    public class WikiCategoryGrpcService : WikiCategory.WikiCategoryBase
    {
        private readonly Iwiki_category _Category;
        public WikiCategoryGrpcService(Iwiki_category category)
        {
            _Category = category;
        }

        public override async Task Filter_Rows1(
            NullRequest request,
            IServerStreamWriter<CategoryModel> responseStream,
            ServerCallContext context)
        {
            using var result = await _Category.wiki_category_Filter_Rows1();
            var list = result.Read<CategoryModel>().ToList();
            foreach (var item in list)
            {
                await responseStream.WriteAsync(item);
            }
        }
        public override async Task<CategoryRespone> Delete1(
            CategoryModel request, 
            ServerCallContext context)
        {
            var result = await _Category.wiki_category_Delete1(request.Id);
            if (result)
            {
                return new CategoryRespone() { Message = "Success!" };
            }
            return new CategoryRespone() { Message = "Fail!" };
        }

        public override async Task<CategoryRespone> Create1(
            CategoryModel request,
            ServerCallContext context)
        {
            if (request.Id>0)
            {
                var result = await _Category.wiki_category_Create1<CategoryModel>(request.Id,request.Name,request.Description);
                if (result.IsSuccessed)
                {
                    return new CategoryRespone() { Message = "Update Success!" };
                }
                return new CategoryRespone() { Message = "Update Fail!" };
            }
            else
            {
                request.Id = -1;
                var result = await _Category.wiki_category_Create1<CategoryModel>(request.Id, request.Name, request.Description);
                if (result.IsSuccessed)
                {
                    return new CategoryRespone() { Message = "Create Success!" };
                }
                return new CategoryRespone() { Message = "Create Fail!" };
            }            
        }
    }
}
