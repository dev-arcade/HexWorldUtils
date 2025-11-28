using UnityEngine;

namespace HexWorldUtils.Resource.Models
{
    public class ResourceInfo
    {
        public readonly Sprite Sprite;
        public readonly ResourceKey Key;

        public ResourceInfo(Sprite sprite, ResourceKey key)
        {
            Sprite = sprite;
            Key = key;
        }
    }
}
