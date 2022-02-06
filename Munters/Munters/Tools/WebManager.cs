using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Munters.Tools
{
    public class WebManager 
    {
        public Result GetData(Uri uri)
        {
            using (var httpClient = new HttpClient())
            {
                try
                {
                    Result result = new Result(false, "");
                    var response = httpClient.GetAsync(uri).Result;
                    var responseContent = response.Content.ReadAsStringAsync();
                    result.IsSuccess = response.IsSuccessStatusCode;
                    result.ResultJson = responseContent.Result;
                    return result;
                }
                catch (Exception ex)
                {
                    return new Result(false, ex.Message);
                }
            }
        }
    }
}
