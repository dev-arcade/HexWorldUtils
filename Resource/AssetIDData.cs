using System.Collections.Generic;
using UnityEngine;
using Random = Unity.Mathematics.Random;

namespace HexWorldUtils.Resource
{
    [CreateAssetMenu(menuName = "Shared Data/Asset ID Data")]
    public class AssetIDData : ScriptableObject
    {
        private struct MaskIdRef
        {
            public string[] MainIds;
            public string[] SecondaryIds;
        }

        [SerializeField] private MaskIdData[] maskIds;
        [SerializeField] private ItemIdData[] itemIds;
        [SerializeField] private string[] debrisIds;
        [SerializeField] private string[] caveItemIndexes;

        private Random _random;
        private Dictionary<TerrainMask, MaskIdRef> _maskIdData;

        public MaskIdData[] MaskIds => maskIds;
        public ItemIdData[] ItemIds => itemIds;
        public string[] DebrisIds => debrisIds;

        public void SetupMaskIdDictionary(uint seed)
        {
            _random = new Random(seed);
            _maskIdData = new Dictionary<TerrainMask, MaskIdRef>();
            for (int i = 0; i < maskIds.Length; i++)
            {
                var id = maskIds[i].id;
                var idRef = new MaskIdRef();
                idRef.MainIds = new string[maskIds[i].subTypes.Length];
                idRef.SecondaryIds = new string[maskIds[i].secondarySubTypes.Length];

                for (int j = 0; j < idRef.MainIds.Length; j++)
                {
                    var idVal = id + maskIds[i].subTypes[j];
                    idRef.MainIds[j] = idVal;
                }

                for (int j = 0; j < idRef.SecondaryIds.Length; j++)
                {
                    var idVal = id + maskIds[i].secondarySubTypes[j];
                    idRef.SecondaryIds[j] = idVal;
                }

                _maskIdData.Add(maskIds[i].mask, idRef);
            }
        }

        public string GetMaskId(TerrainMask mask, int subTypeIndex) => 
            _maskIdData[mask].MainIds[subTypeIndex];

        public string GetRandomMaskId(TerrainMask mask) =>
            _maskIdData[mask].MainIds[_random.NextInt(_maskIdData[mask].MainIds.Length)];

        public string GetSecondaryMaskId(TerrainMask mask, int subTypeIndex) => 
            _maskIdData[mask].SecondaryIds[subTypeIndex];

        public string GetRandomSecondaryMaskId(TerrainMask mask) =>
            _maskIdData[mask].SecondaryIds[_random.NextInt(_maskIdData[mask].SecondaryIds.Length)];

        public ItemIdData[] GetCaveIds()
        {
            var ids = new ItemIdData[caveItemIndexes.Length];
            for (int i = 0; i < ids.Length; i++)
                ids[i] = itemIds[i];

            return ids;
        }
    }
}
