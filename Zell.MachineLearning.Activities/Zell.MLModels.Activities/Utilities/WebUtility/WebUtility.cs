using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class WebUtility
    {
        readonly string _url;

        public WebUtility(string url)
        {
            _url = url;
        }

        public string Get(string accept = null, string token = null)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(_url);
            if (accept != null)
                httpWebRequest.Accept = accept;
            if (token != null)
                httpWebRequest.Headers.Add("Authorization", "Bearer " + token);
            using (HttpWebResponse response = (HttpWebResponse)httpWebRequest.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream ?? throw new InvalidOperationException()))
            {
                return reader.ReadToEnd();
            }
        }

        public string Post(string bodyAsJsonString, Dictionary<string, string> headerList = null, string token = null)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(_url);
            if (headerList != null)
            {
                string contentType = headerList["contentType"];
                httpWebRequest.ContentType = contentType;
            }
            httpWebRequest.Method = "POST";
            if (token != null)
                httpWebRequest.Headers.Add("Authorization", "Bearer " + token);
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string jsonPayload = bodyAsJsonString;
                streamWriter.Write(jsonPayload);
            }
            using (HttpWebResponse response = (HttpWebResponse)httpWebRequest.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream ?? throw new InvalidOperationException()))
            {
                return reader.ReadToEnd();
            }
        }

        public async Task<string> GetAsync(string accept = null, string token = null)
        {
            using (var httpClient = new HttpClient())
            {
                if (accept != null)
                    httpClient.DefaultRequestHeaders.Add("Accept", accept);
                if (token != null)
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await httpClient.GetAsync(_url);

                //will throw an exception if not successful
                response.EnsureSuccessStatusCode();

                string content = await response.Content.ReadAsStringAsync();
                return content;
            }
        }

        public async Task<string> PostAsync(string bodyAsJsonString, Dictionary<string, string> headerList = null, string token = null)
        {
            using (var httpClient = new HttpClient())
            {
                HttpContent content;
                if (headerList != null)
                {
                    string contentType = headerList["contentType"];
                    content = new StringContent(bodyAsJsonString, Encoding.UTF8, contentType);
                }
                else
                    content = new StringContent(bodyAsJsonString);
                if (token != null)
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await httpClient.PostAsync(_url, content);

                //will throw an exception if not successful
                response.EnsureSuccessStatusCode();

                string responseContent = await response.Content.ReadAsStringAsync();
                return responseContent;
            }
        }

        public async Task<string> PostAsJsonAsync(object body, Dictionary<string, string> headerList = null, string token = null)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                httpClient.BaseAddress = new Uri(_url);

                HttpResponseMessage response = await httpClient.PostAsJsonAsync("", body);

                //will throw an exception if not successful
                response.EnsureSuccessStatusCode();

                string responseContent = await response.Content.ReadAsStringAsync();
                return responseContent;
            }
        }

        public async Task<string> PutAsync(string bodyAsJsonString, Dictionary<string, string> headerList = null, string token = null)
        {
            using (var httpClient = new HttpClient())
            {
                HttpContent content;
                if (headerList != null)
                {
                    string contentType = headerList["contentType"];
                    content = new StringContent(bodyAsJsonString, Encoding.UTF8, contentType);
                }
                else
                    content = new StringContent(bodyAsJsonString);
                if (token != null)
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Put,
                    RequestUri = new Uri(_url),
                    Content = content
                };
                var response = await httpClient.SendAsync(request);

                //will throw an exception if not successful
                response.EnsureSuccessStatusCode();

                var responseContent = await response.Content.ReadAsStringAsync();
                return responseContent;
            }
        }
    }
}