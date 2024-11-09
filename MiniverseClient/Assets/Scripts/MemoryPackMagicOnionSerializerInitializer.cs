using MagicOnion.Serialization;
using MagicOnion.Serialization.MemoryPack;
using UnityEngine;

namespace Miniverse
{
    public class MemoryPackMagicOnionSerializerInitializer
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        public static void Initialize()
        {
            MagicOnionSerializerProvider.Default = MemoryPackMagicOnionSerializerProvider.Instance;
        }
    }
}
