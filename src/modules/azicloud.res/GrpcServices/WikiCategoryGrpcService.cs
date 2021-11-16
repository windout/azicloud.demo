using azicloud.res.Application.Models;
using azicloud.res.bo.Interfaces;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using System.Collections.Generic;
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
    }
}
