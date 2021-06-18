
using ECommerce.Api.Search.Interfaces;
using ECommerce.Api.Search.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Search.Controllers
{
    [ApiController]
    [Route("api/search")]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService searchservice;

        public SearchController(ISearchService searchService)
        {
            this.searchservice = searchService;
        }
        
        [HttpPost]
       public async Task<IActionResult> SearchAsync(SearchTerm term)
        {
            var result = await searchservice.SearchAsync(term.CustomerId);
            if (result.IsSuccess)
            {
                return Ok(result.SearchResults);
            }

            return NotFound();
        }



    }
}
