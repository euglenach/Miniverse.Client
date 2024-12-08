using System;
using System.Threading.Tasks;
using Cysharp.Net.Http;
using Cysharp.Threading.Tasks;
using Grpc.Core;
using Grpc.Net.Client;
using MagicOnion;
using MagicOnion.Client;
using MagicOnion.Unity;
using MiniverseShared.MessagePackObjects;
using MiniverseShared.StreamingHubs;
using UnityEngine;

namespace StreamingHubs
{
    public class MatchingHub : IDisposable
    {
        private readonly Player player;
        private readonly GrpcChannelx channel;
        private readonly IMatchingHub matchingHub;
        private readonly MatchingReceiver receiver;

        private MatchingHub(Player player, GrpcChannelx channel, IMatchingHub matchingHub, MatchingReceiver receiver)
        {
            this.player = player;
            this.channel = channel;
            this.matchingHub = matchingHub;
            this.receiver = receiver;
        }

        public static async ValueTask<MatchingHub> CreateAsync(Player player)
        {
            // Connect to the server using gRPC channel.
            var channel = GrpcChannelx.ForAddress(Constant.URL);

            var receiver = new MatchingReceiver();
            // Create a proxy to call the server transparently.
            var hubClient = await StreamingHubClient.ConnectAsync<IMatchingHub, IMatchingReceiver>(channel, receiver);
            
            return new(player, channel, hubClient, receiver);
        }

        public async UniTask CreateRoomAsync()
        {
            await matchingHub.CreateRoomAsync(player);
        }
        
        public async UniTask JoinRoomAsync(Ulid roomUlid)
        {
            await matchingHub.JoinRoomAsync(roomUlid, player);
        }
        
        public async void Dispose()
        {
            await channel.ShutdownAsync();
            await matchingHub.DisposeAsync();
        }

        private class MatchingReceiver : IMatchingReceiver
        {
            public void OnJoin(Player player)
            {
                Debug.Log("OnJoin");
            }

            public void OnLeave(Player player)
            {
                
            }
        }
    }
}
