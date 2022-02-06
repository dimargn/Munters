using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Munters.Results
{
    public class GiphySearchResult
    {
        [JsonProperty("data")]
        public Data[] Data { get; set; }
    }
}
