using UnityEngine;

namespace MoonPlotsCartographerShared.Models
{
    public struct ItemParsedData
    {
        public char Category;
        public char Type;
        public char Subtype;
        public int Position;
        public int Scale;

        public ItemParsedData(char category, char type, char subtype, int position, int scale)
        {
            Category = category;
            Type = type;
            Subtype = subtype;
            Position = position;
            Scale = scale;
        }

        public string GetID()
        {
            return $"{Category}{Type}{Subtype}:{Position}:{Scale}";
        }

        public bool LoadID(string id)
        {
            var payload = id.Split(':');
            if (payload.Length < 3)
            {
                Debug.Log($"load failed, expected format: 'charCharChar:int:int', got: {id}");
                return false;
            }

            var idDataStr = payload[0];
            var positionStr = payload[1];
            var scaleStr = payload[2];

            if (idDataStr.Length < 3)
            {
                Debug.Log($"load failed, expected format: 'charCharChar', got: {idDataStr}");
                return false;
            }

            Category = idDataStr[0];
            Type = idDataStr[1];
            Subtype = idDataStr[2];

            if (!int.TryParse(positionStr, out var position))
            {
                Debug.Log($"load failed, expected format: 'int', got: {positionStr}");
                return false;
            }

            Position = position;

            if (!int.TryParse(scaleStr, out var scale))
            {
                Debug.Log($"load failed, expected format: 'int', got: {scaleStr}");
                return false;
            }

            Scale = scale;
            return true;
        }
    }
}
