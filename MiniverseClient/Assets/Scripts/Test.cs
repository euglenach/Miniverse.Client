using System;
using Cysharp.Threading.Tasks;
using Miniverse.WebAPI;
using MiniverseShared.MessagePackObjects;
using MiniverseShared.WebAPI;
using StreamingHubs;
using UnityEngine;

public class Test : MonoBehaviour
{
    private async UniTask Start()
    {
        await CreateRoom();
        await JoinRoom(default);
        
        // var response = await MessagePackWebAPI.GetAsync<MagicOnionURLRequest, MagicOnionURLResponse>();
        // Debug.Log(response.URL);
    }

    async UniTask CreateRoom()
    {
        var player = new Player{ Ulid = Ulid.NewUlid(), Name = "Host" };
        var hub = await MatchingHub.CreateAsync(player);
        await hub.CreateRoomAsync();
    }
    
    async UniTask JoinRoom(Ulid roomUlid)
    {
        var player = new Player{ Ulid = Ulid.NewUlid(), Name = "Guest" };
        var hub = await MatchingHub.CreateAsync(player);
        await hub.JoinRoomAsync(roomUlid);
    }
}