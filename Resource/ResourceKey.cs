namespace HexWorldUtils.Resource
{
    public enum ResourceKeyType
    {
        FullID,
        Category,
        Type,
        SubType,
    }

    public class ResourceKey
    {
        public readonly string FullID;
        public readonly char? Category;
        public readonly char? Type;
        public readonly char? SubType;

        /// <summary>
        /// optional
        /// </summary>
        public string CategoryDisplayName;

        public ResourceKey(char? category, char? type, char? subType, string fullId)
        {
            Category = category;
            Type = type;
            SubType = subType;
            FullID = fullId;
        }

        public bool TryGetKeyType(out ResourceKeyType keyType)
        {
            if (!string.IsNullOrWhiteSpace(FullID))
            {
                keyType = ResourceKeyType.FullID;
                return true;
            }

            keyType = default;
            if (Category == null)
                return false;

            if (Type == null)
            {
                keyType = ResourceKeyType.Category;
                return true;
            }

            if (SubType == null)
            {
                keyType = ResourceKeyType.Type;
                return true;
            }

            keyType = ResourceKeyType.SubType;
            return true;
        }

        public override string ToString()
        {
            return $"Full ID: {FullID}, Category: {Category}, Type: {Type}, SubType: {SubType}";
        }
    }
}
