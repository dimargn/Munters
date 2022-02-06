using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Munters.Models;
using Munters.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Munters.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private  IMemoryCache _cache;
        private GiphyService _giphyService;

        [BindProperty]
        public string searchString { get; set; }

        public IList<string> images = new List<string>();
        public IndexModel(ILogger<IndexModel> logger, IMemoryCache memoryCache)
        {
            _logger = logger;
            _cache = memoryCache;
            _giphyService = GiphyService.GetInstance();
        }

        public void OnGet()
        {

        }

        public void OnPostDaily()
        {
            images = _giphyService.TrendingGifs().ToList();
        }
        public void OnPostSearch()
        {
            if (string.IsNullOrEmpty(searchString))
            {
                images = new List<string> { "Please enter a search string for images" };
            }
            else
            {
                images = _giphyService.GifSearch(searchString).ToList();
                if(images.Count==0)
                {
                    images = new List<string> { "Didn't find any image containing " + searchString };
                }
            }
        }
    }
}
