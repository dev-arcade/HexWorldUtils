using HexWorldUtils.Resource.Models;

namespace HexWorldUtils.Resource
{
    public static class SharedResourceHelper
    {
        public static ResourceKey GetResourceKeyForAssetInit(string rawId)
        {
            var categoryChar = rawId[0];
            var typeChar = rawId[1];
            char subTypeChar;
            string index;
            string fullId;
            if (rawId.Length >= 4)
            {
                subTypeChar = rawId[2];
                index = rawId[3..];
                fullId = categoryChar.ToString() + typeChar.ToString() + subTypeChar.ToString() + index.ToString();
                return new ResourceKey(categoryChar, typeChar, subTypeChar, fullId);
            }

            if (rawId.Length == 3)
            {
                subTypeChar = '0';
                index = rawId[2].ToString();
            }

            else if (rawId.Length == 2)
            {
                subTypeChar = '0';
                index = "0";
            }

            else
                throw new System.Exception($"invalid resource id, expected at least 2 chars, received; {rawId}");

            fullId = categoryChar.ToString() + typeChar.ToString() + subTypeChar.ToString() + index.ToString();
            return new ResourceKey(categoryChar, typeChar, subTypeChar, fullId);

        }
    }
}
