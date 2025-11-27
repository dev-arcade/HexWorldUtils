using HexWorldUtils.Models;
using UnityEngine;

namespace HexWorldUtils.DataParsers
{
    public static class ItemDataParser
    {
        public static string GetItemID(ItemData item) =>
            $"{item.Category}{item.Type}{item.Subtype}:{item.Position}:{item.Scale}";

        public static bool TryParseItem(string id, out ItemData item)
        {
            var payload = id.Split(':');
            if (payload.Length < 3)
            {
                Debug.Log($"load failed, expected format: 'charCharChar:int:int', got: {id}");
                item = default;
                return false;
            }

            if (payload.Length > 3)
            {
                Debug.Log($"load waring for id format, expected format: 'charCharChar:int:int', got: {id}");
            }

            var idDataStr = payload[0];
            var positionStr = payload[1];
            var scaleStr = payload[2];

            if (idDataStr.Length < 3)
            {
                Debug.Log($"load failed, expected format: 'charCharChar', got: {idDataStr}");
                item = default;
                return false;
            }

            var category = idDataStr[0];
            var type = idDataStr[1];
            var subtype = idDataStr[2];

            if (!int.TryParse(positionStr, out var position))
            {
                Debug.Log($"load failed, expected format: 'int', got: {positionStr}");
                item = default;
                return false;
            }

            if (!int.TryParse(scaleStr, out var scale))
            {
                Debug.Log($"load failed, expected format: 'int', got: {scaleStr}");
                item = default;
                return false;
            }

            item = new ItemData(category, type, subtype, position, scale);
            return true;
        }
    }
}
