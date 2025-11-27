using HexWorldUtils.DataParsers;

namespace HexWorldUtils.Models
{
    public struct ItemData
    {
        public char Category;
        public char Type;
        public char Subtype;
        public int Position;
        public int Scale;

        public ItemData(char category, char type, char subtype, int position, int scale)
        {
            Category = category;
            Type = type;
            Subtype = subtype;
            Position = position;
            Scale = scale;
        }

        public string GetID() =>
            ItemDataParser.GetItemID(this);
    }
}
