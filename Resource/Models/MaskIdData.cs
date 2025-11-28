using System;

namespace HexWorldUtils.Resource.Models
{
    [Serializable]
    public class MaskIdData
    {
        public TerrainMask mask;
        public string id;
        public string[] subTypes;
        public string[] secondarySubTypes;
    }
}
