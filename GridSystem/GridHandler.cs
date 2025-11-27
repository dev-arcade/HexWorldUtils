using System;
using Unity.Mathematics;
using UnityEngine;

namespace HexWorldUtils.GridSystem
{
    public class GridHandler : MonoBehaviour
    {
        [SerializeField] private bool generateOnStart = true;
        [SerializeField] private GridSettings defaultSettings;

        private SquareGrid _grid;

        public event Action<float3[,]> OnGenerated;

        public SquareGrid Grid => _grid;

        private void Start()
        {
            if (generateOnStart)
                GenerateGrid(defaultSettings);
        }

        public void GenerateGrid(GridSettings settings)
        {
            _grid = new SquareGrid(settings.OriginPosition, settings.Width, settings.Height, settings.CellSizeX,
                settings.CellSizeY);
            OnGenerated?.Invoke(_grid.GridData);
        }
    }
}
