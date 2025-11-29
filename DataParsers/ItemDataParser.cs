using System;
using HexWorldUtils.Models;
using UnityEngine;

namespace HexWorldUtils.DataParsers
{
    public static class ItemDataParser
    {
        public static string GetItemID(ItemData item) =>
            $"{item.Category}{item.Type}{item.Subtype}{item.ResourceIndex}";

        public static string GetItemIDForStorage(ItemData item) =>
            $"{item.Category}{item.Type}{item.Subtype}{item.ResourceIndex}:{item.Position}:{item.Scale}";

        public static bool TryParseItem(string id, out ItemData item)
        {
            var payload = id.Split(':');
            if (payload.Length < 3)
                throw new ArgumentException($"load failed, expected format: 'charCharChar:int:int', got: {id}");

            if (payload.Length > 3)
            {
                Debug.Log($"load waring for id format, expected format: 'charCharChar:int:int', got: {id}");
            }

            var idDataStr = payload[0];
            var positionStr = payload[1];
            var scaleStr = payload[2];

            if (idDataStr.Length < 3)
                throw new ArgumentException($"load failed, expected format: 'charCharChar', got: {idDataStr}");

            var category = idDataStr[0];
            var type = idDataStr[1];
            var subtype = idDataStr[2];
            var resourceIndexStr = idDataStr[3..];

            if (!int.TryParse(positionStr, out var position))
                throw new ArgumentException($"load failed, expected format: 'int', got: {positionStr}");

            if (!int.TryParse(scaleStr, out var scale))
                throw new ArgumentException($"load failed, expected format: 'int', got: {scaleStr}");

            if (!int.TryParse(resourceIndexStr, out var resourceIndex))
                throw new ArgumentException($"load failed, expected format: 'int', got: {resourceIndexStr}");

            item = new ItemData(category, type, subtype, resourceIndex, position, scale);
            return true;
            
            // test
        }
    }
}
