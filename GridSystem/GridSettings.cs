using System;
using UnityEngine;

namespace HexWorldUtils.GridSystem
{
    [Serializable]
    public class GridSettings
    {
        [SerializeField] private Vector3 originPosition;
        [SerializeField] private int width;
        [SerializeField] private int height;
        [SerializeField] private float cellSizeX;
        [SerializeField] private float cellSizeY;

        public Vector3 OriginPosition => originPosition;
        public int Width => width;
        public int Height => height;
        public float CellSizeX => cellSizeX;
        public float CellSizeY => cellSizeY;

        public GridSettings(Vector3 originPosition, int width, int height, float cellSizeX, float cellSizeY)
        {
            this.originPosition = originPosition;
            this.width = width % 2 > 0 ? width + 1 : width;
            this.height = height % 2 > 0 ? height + 1 : height;
            this.cellSizeX = cellSizeX;
            this.cellSizeY = cellSizeY;
        }
    }
}
