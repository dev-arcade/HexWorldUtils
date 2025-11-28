using HexWorldUtils.DataParsers;

namespace HexWorldUtils.Models
{
    public struct ItemData
    {
        public readonly char Category;
        public readonly char Type;
        public readonly char Subtype;
        public readonly int ResourceIndex;
        public readonly int Position;
        public readonly int Scale;

        public ItemData(char category, char type, char subtype, int resourceIndex, int position, int scale)
        {
            Category = category;
            Type = type;
            Subtype = subtype;
            ResourceIndex = resourceIndex;
            Position = position;
            Scale = scale;
        }

        public string GetStorageID() =>
            ItemDataParser.GetItemIDForStorage(this);

        public string GetID() =>
            ItemDataParser.GetItemID(this);
    }
}
