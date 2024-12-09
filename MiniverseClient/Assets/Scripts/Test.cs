using System;
using Cysharp.Threading.Tasks;
using Miniverse.WebAPI;
using MiniverseShared.MessagePackObjects;
using MiniverseShared.WebAPI;
using R3;
using StreamingHubs;
using UnityEngine;

public class Test : MonoBehaviour
{
    private async UniTask Start()
    {
        var hub = await CreateRoom();
        var createRoomResult = await hub.OnJoinSelf.FirstAsync();
        var guestHub = await JoinRoom(createRoomResult.Ulid);
        
        // var response = await MessagePackWebAPI.GetAsync<MagicOnionURLRequest, MagicOnionURLResponse>();
        // Debug.Log(response.URL);
    }

    async UniTask<MatchingHub> CreateRoom()
    {
        var player = new Player{ Ulid = Ulid.NewUlid(), Name = "Host" };
        var hub = await MatchingHub.CreateAsync(player);
        await hub.CreateRoomAsync();
        return hub;
    }
    
    async UniTask<MatchingHub> JoinRoom(Ulid roomUlid)
    {
        var player = new Player{ Ulid = Ulid.NewUlid(), Name = "Guest" };
        var hub = await MatchingHub.CreateAsync(player);
        await hub.JoinRoomAsync(roomUlid);
        return hub;
    }
}