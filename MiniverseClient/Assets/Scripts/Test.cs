using System;
using Cysharp.Threading.Tasks;
using MiniverseShared.MessagePackObjects;
using StreamingHubs;
using UnityEngine;

public class Test : MonoBehaviour
{
    private async UniTask Start()
    {
        var player = new Player{ Ulid = Ulid.NewUlid(), Name = "Test" };
        var hub = await MatchingHub.CreateAsync(player);
        await hub.JoinAsync(Ulid.NewUlid());
    }
}