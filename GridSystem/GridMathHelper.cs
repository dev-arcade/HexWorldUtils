using System;
using Unity.Mathematics;
using UnityEngine;

namespace HexWorldUtils.GridSystem
{
    public class GridMathHelper
    {
        public void CalculateSpriteGridFromTexture(SpriteRenderer resourceRenderer,
            out Vector3 origin, out float cellSize, out int gridSize)
        {
            if (!resourceRenderer)
                throw new ArgumentNullException(nameof(resourceRenderer));
            if (!resourceRenderer.sprite)
                throw new ArgumentException("SpriteRenderer has no sprite");

            origin = GetGridOrigin(resourceRenderer);
            var outerBounds = GetSpriteOuterBounds(resourceRenderer);
            var worldBounds = outerBounds.x > outerBounds.y ? outerBounds.x : outerBounds.y;

            var texWidth = resourceRenderer.sprite.texture.width;
            var texHeight = resourceRenderer.sprite.texture.height;
            gridSize = math.max(texWidth, texHeight);
            if (gridSize % 2 > 0)
                gridSize++;

            cellSize = worldBounds / gridSize;
        }

        public void CalculateSpriteGrid(SpriteRenderer resourceRenderer, float resolution,
            out Vector3 origin, out float cellSize, out int gridSize, out float worldBounds)
        {
            if (!resourceRenderer)
                throw new ArgumentNullException(nameof(resourceRenderer));
            if (!resourceRenderer.sprite)
                throw new ArgumentException("SpriteRenderer has no sprite");

            origin = GetGridOrigin(resourceRenderer);
            var outerBounds = GetSpriteOuterBounds(resourceRenderer);

            worldBounds = outerBounds.x > outerBounds.y ? outerBounds.x : outerBounds.y;
            cellSize = worldBounds / resolution;

            gridSize = Mathf.CeilToInt(resolution);
            if (gridSize % 2 > 0)
                gridSize++;
        }

        public Vector3 GetGridOrigin(SpriteRenderer spriteRenderer) => spriteRenderer.bounds.min;

        public Vector3 GetSpriteOuterBounds(SpriteRenderer spriteRenderer)
        {
            var bounds = spriteRenderer.bounds;
            return new Vector3(bounds.size.x, bounds.size.y, bounds.size.z);
        }

        public int2 MapTexturePixelToGrid(int2 texturePoint, int gridWidth, int gridHeight,
            int textureWidth, int textureHeight)
        {
            var scaleX = (float)gridWidth / textureWidth;
            var scaleY = (float)gridHeight / textureHeight;

            var gridX = texturePoint.x * scaleX;
            var gridY = texturePoint.y * scaleY;

            var gridCol = Mathf.RoundToInt(gridX);
            var gridRow = Mathf.RoundToInt(gridY);

            var x = Mathf.Clamp(gridCol, 0, gridWidth - 1);
            var y = Mathf.Clamp(gridRow, 0, gridHeight - 1);
            return new int2(x, y);
        }
    }
}
