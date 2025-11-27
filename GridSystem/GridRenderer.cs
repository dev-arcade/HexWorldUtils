using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

namespace MoonPlotsCartographerShared.GridSystem
{
    public class GridRenderer : MonoBehaviour
    {
        [SerializeField] private GridHandler squareGridHandler;

        [Space, Space] [SerializeField] private bool enable;
        [SerializeField] private bool drawBoundSpheresOnly;
        [SerializeField] private float sphereRadius = 0.5f;

        [Space] [SerializeField] private bool enableAddressLabels;
        [SerializeField] private Color gridColor = Color.white;

        private Vector3 _cachedBottomLeft;
        private Vector3 _cachedBottomRight;
        private Vector3 _cachedTopLeft;
        private Vector3 _cachedTopRight;
        private Vector3 _cachedCenter;
        private bool _areBoundsCalculated;

        private void Awake()
        {
            if (squareGridHandler)
                squareGridHandler.OnGenerated += UpdateGridBounds;
        }

        private void UpdateGridBounds(float3[,] grid)
        {
            float minX = float.MaxValue, maxX = float.MinValue;
            float minY = float.MaxValue, maxY = float.MinValue;
            foreach (var cell in grid)
            {
                if (cell.x < minX)
                    minX = cell.x;
                if (cell.x > maxX)
                    maxX = cell.x;
                if (cell.y < minY)
                    minY = cell.y;
                if (cell.y > maxY)
                    maxY = cell.y;
            }

            _cachedBottomLeft = new Vector3(minX, minY, 0);
            _cachedBottomRight = new Vector3(maxX, minY, 0);
            _cachedTopLeft = new Vector3(minX, maxY, 0);
            _cachedTopRight = new Vector3(maxX, maxY, 0);
            _cachedCenter = new Vector3((minX + maxX) / 2, (minY + maxY) / 2, 0);
            _areBoundsCalculated = true;
        }

        private void OnDrawGizmos()
        {
            if (!enable || !squareGridHandler || squareGridHandler.Grid == null)
            {
                _areBoundsCalculated = false;
                return;
            }

            var grid = squareGridHandler.Grid.GridData;
            var cellSize = squareGridHandler.Grid.CellSizeX;
            Gizmos.color = gridColor;

            if (drawBoundSpheresOnly)
            {
                DrawBoundSpheres(grid);
                return;
            }

            foreach (var cell in grid)
            {
                var cuberSize = new Vector3(cellSize, cellSize, 0);
                Gizmos.DrawWireCube(cell, cuberSize);
                if (enableAddressLabels)
                {
                    Handles.Label(cell, cell.ToString(),
                        new GUIStyle
                        {
                            normal = new GUIStyleState
                            {
                                textColor = gridColor
                            }
                        });
                }
            }
        }

        private void DrawBoundSpheres(float3[,] grid)
        {
            if (grid.Length == 0)
                return;
            if (!_areBoundsCalculated)
                UpdateGridBounds(grid);

            Gizmos.DrawSphere(_cachedBottomLeft, sphereRadius);
            Gizmos.DrawSphere(_cachedBottomRight, sphereRadius);
            Gizmos.DrawSphere(_cachedTopLeft, sphereRadius);
            Gizmos.DrawSphere(_cachedTopRight, sphereRadius);
            Gizmos.DrawSphere(_cachedCenter, sphereRadius);
        }
    }
}
