using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using Cysharp.Net.Http;
using Cysharp.Threading.Tasks;
using MessagePack;
using MiniverseShared.WebAPI;
using UnityEngine;

namespace Miniverse.WebAPI
{
    public static class MessagePackWebAPI
    {
        private static readonly string baseUrl = "http://localhost:5277/api/";
        private static readonly HttpClient httpClient = new(new HttpClientHandler());
        
        public static async UniTask<TResponse> GetAsync<TRequest, TResponse>(CancellationToken cancellationToken = default)
        {
            var response = await httpClient.GetAsync(baseUrl + typeof(TRequest).Name, cancellationToken);
            var responseBytes = await response.Content.ReadAsByteArrayAsync();
            
            return MessagePackSerializer.Deserialize<TResponse>(responseBytes);
        }
        
        public static async UniTask<TResponse> PostAsync<TResponse>(object request, CancellationToken cancellationToken = default)
        {
            var bytes = MessagePackSerializer.Serialize(request);
            var content = new ByteArrayContent(bytes);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

            var response = await httpClient.GetAsync(baseUrl + request.GetType().Name, cancellationToken);
            var responseBytes = await response.Content.ReadAsByteArrayAsync();
            
            return MessagePackSerializer.Deserialize<TResponse>(responseBytes);
        }
    }
}
