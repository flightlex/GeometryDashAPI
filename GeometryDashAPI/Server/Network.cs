﻿using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GeometryDashAPI.Server
{
    internal class Network
    {
        public Encoding DataEncoding { get; }

        public Network() : this(Encoding.ASCII)
        {
        }

        public Network(Encoding encoding)
        {
            DataEncoding = encoding;
        }
        
        public async Task<string> GetAsync(string path, IQuery query)
        {
            return await GetUseWebClient(path, query.BuildQuery());
        }

        private async Task<string> GetUseWebClient(string path, Parameters properties)
        {
            var client = WebRequest.Create($"http://boomlings.com{path}");
            client.ContentType = "application/x-www-form-urlencoded";
            client.Headers.Add("20", "*/*");
            client.Method = "POST";
            var data = DataEncoding.GetBytes(properties.ToString());
            var requestStream = await client.GetRequestStreamAsync();
            await requestStream.WriteAsync(data, 0, data.Length);
            var response = await client.GetResponseAsync();
            return await new StreamReader(response.GetResponseStream()).ReadToEndAsync();
        }
    }
}
