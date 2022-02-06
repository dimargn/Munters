using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Munters.Results;
using Munters.Tools;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Munters.Services
{
    public sealed class GiphyService 
    {
        private readonly WebManager _webManager;
        private readonly string _authKey;
        private static readonly object _lock_obj = new object();

        private static GiphyService instance = null;
        private GiphyService(string authKey)
        {
            _authKey = authKey;
            _webManager = new WebManager();
        }

        public static GiphyService GetInstance(string authKey = "5ZUSA0FR7LL879HasfVcv9W8b747l6uA")
        {
            if (instance == null)
            {
                lock (_lock_obj)
                {
                    if (instance == null)
                    {
                        instance = new GiphyService(authKey);
                    }
                }
            }
            return instance;
        }


        public IEnumerable<string> TrendingGifs()
        {
            IEnumerable<string> results;
            lock (_lock_obj)
            {
                if(CacheManager.Exist(DateTime.Today))
                {
                    return (IEnumerable<string>)CacheManager.GetCache(DateTime.Today);
                }
                else
                {
                    var result = _webManager.GetData(new Uri($"http://api.giphy.com/v1/gifs/trending?api_key=" + _authKey));
                    if (!result.IsSuccess)
                    {
                        throw new WebException($"Failed to get GIF: {result.ResultJson}");
                    }

                    results = JsonConvert.DeserializeObject<GiphySearchResult>(result.ResultJson).Data.Select(x => x.Url);
                    CacheManager.SetCache(DateTime.Today, results);
                }
            }

            return results;
        }


        public IEnumerable<string> GifSearch(string searchString)
        {
            if (string.IsNullOrEmpty(searchString))
            {
                throw new FormatException("Must set query in order to search.");
            }

            IEnumerable<string> results;
            if (CacheManager.Exist(searchString))
            {
                return (IEnumerable<string>)CacheManager.GetCache(searchString);
            }
            else
            {

                var result = _webManager.GetData(new Uri($"http://api.giphy.com/v1/gifs/search?api_key=" + _authKey + "&q=" + searchString));
                if (!result.IsSuccess)
                {
                    throw new WebException($"Failed to get GIFs: {result.ResultJson}");
                }

                results = JsonConvert.DeserializeObject<GiphySearchResult>(result.ResultJson).Data.Select(x => x.Url);
                CacheManager.SetCache(searchString, results);
            }
            return results;
        }

    }

  /*  public sealed class GiphyServiceSingleton
    {
        private static GiphyService m_giphyService;
        private static readonly object initSingletonLock = new object();

        /// <summary>
        /// Get the acquisition object's instance, or initialize it if it doesn't exist.
        /// </summary>
        /// <returns>The acquisition object's instance. </returns>
        public static GiphyService GetGiphyService()
        {
            if (m_giphyService == null)
            {
                lock (initSingletonLock)
                {
                    if (m_giphyService == null)
                    {
                        m_giphyService = new GiphyService();
                    }
                }
            }

            return m_giphyService;
        }
    } */
}
