using System;

namespace HexWorldUtils.GridSystem
{
    public static class GridValidator
    {
        public static bool ValidateDimensions(int width, int height)
        {
            if (width <= 0)
                throw new ArgumentException("Width must be positive", nameof(width));
            if (height <= 0)
                throw new ArgumentException("Height must be positive", nameof(height));

            return true;
        }

        public static bool ValidateCellSize(float cellSizeX, float cellSizeY)
        {
            if (cellSizeX <= 0)
                throw new ArgumentException("Cell size X must be positive", nameof(cellSizeX));
            if (cellSizeY <= 0)
                throw new ArgumentException("Cell size Y must be positive", nameof(cellSizeY));

            return true;
        }
    }
}
