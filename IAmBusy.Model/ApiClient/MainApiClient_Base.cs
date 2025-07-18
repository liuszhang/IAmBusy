using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAmBusy.Model.ApiClient
{
    public partial class MainApiClient
    {
        private HttpClient httpClient = new();

        public MainApiClient(HttpClient _httpClient)
        {
            httpClient= _httpClient;
            //httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            //httpClient.DefaultRequestHeaders.Add("User-Agent", "IAmBusy.DB.ApiClient");
        }

    }
}
