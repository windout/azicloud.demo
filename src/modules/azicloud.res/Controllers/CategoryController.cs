using azicloud.res.Application.Interfaces;
using azicloud.res.Application.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace azicloud.res.Controllers
{
    [ApiController]
    [Route("api/v1/wiki/wikicategory")]
    public class CategoryController : ControllerBase
    {
        IWikiCategoryService _wikiCategoryService;

        public CategoryController(IWikiCategoryService wikiCategoryService)
        {
            _wikiCategoryService = wikiCategoryService;
        }

        [HttpGet("list")]
        public async Task<IActionResult> Filter_Rows1()
        {
            return Ok(await _wikiCategoryService.Filter_Rows1());
        }
        [HttpDelete("{id}/delete")]
        public async Task<IActionResult> Delete1(int id)
        {
            return Ok(await _wikiCategoryService.Delete1(id));
        }
        [HttpPost("create")]
        public async Task<IActionResult> Create1([FromForm]Category category)
        {
            if(category.Id <= 0)
            {
                category.Id = -1;
            }                
            return Ok(await _wikiCategoryService.Create1(category));
        }      
    }
}
