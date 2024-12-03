using Cysharp.Net.Http;
using Cysharp.Serialization.MessagePack;
using Grpc.Net.Client;
using MagicOnion.Unity;
using MessagePack;
using MessagePack.Resolvers;
using MessagePack.Unity.Extension;
using UnityEngine;

namespace DefaultNamespace
{
    public class MagicOnionInitializer
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void OnRuntimeInitialize()
        {
            StaticCompositeResolver.Instance.Register(
                MagicOnionGeneratedClientInitializer.Resolver,
                StandardResolver.Instance,
                GeneratedResolver.Instance,
                MessagePack.Unity.UnityResolver.Instance,
                BuiltinResolver.Instance,
                UnityBlitWithPrimitiveArrayResolver.Instance,
                PrimitiveObjectResolver.Instance,
                UlidMessagePackResolver.Instance);

            var options = MessagePackSerializerOptions.Standard.WithResolver(StaticCompositeResolver.Instance);
            MessagePackSerializer.DefaultOptions = options;
            
            // Initialize gRPC channel provider when the application is loaded.
            GrpcChannelProviderHost.Initialize(new DefaultGrpcChannelProvider(() => new GrpcChannelOptions()
            {
                HttpHandler = new YetAnotherHttpHandler()
                {
                    Http2Only = true,
                },
                DisposeHttpClient = true,
            }));
        }
    }
}
