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
        CreateRoom().Forget();
        CreateRoom().Forget();
        CreateRoom().Forget();
        
        // var response = await MessagePackWebAPI.GetAsync<MagicOnionURLRequest, MagicOnionURLResponse>();
        // Debug.Log(response.URL);
    }

    async UniTaskVoid CreateRoom()
    {
        var player = new Player{ Ulid = Ulid.NewUlid(), Name = "Test" };
        var hub = await MatchingHub.CreateAsync(player);
        await hub.CreateRoomAsync();
    }
}