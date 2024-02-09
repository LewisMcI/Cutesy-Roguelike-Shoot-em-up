using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

enum Direction
{
    left,
    right,
    up,
    down
}
public class WaveFunctionCollapse : MonoBehaviour
{
    [SerializeField] int xWidth;
    [SerializeField] int yHeight;
    [SerializeField] Tile[] possibleTiles;
    [SerializeField] Cell cellPrefab;

    private Cell[,] grid;
    private void Awake()
    {
        nextCellList = new List<Vector2Int>();
        InitializeGrid();
    }

    private void InitializeGrid()
    {
        grid = new Cell[xWidth, yHeight];
        for (int x = 0; x < xWidth; x++)
        {
            for (int y = 0; y < yHeight; y++)
            {
                grid[x,y] = Instantiate(cellPrefab, new Vector2(x, y), Quaternion.identity, transform).GetComponent<Cell>();
                grid[x,y].CreateCell(possibleTiles);
            }
        }

        PopulateGrid();
    }

    private List<Vector2Int> nextCellList;
    private void PopulateGrid()
    {
        nextCellList.Add(new Vector2Int(0, 0));
        while (nextCellList.Count > 0)
        {
            // Get Next Cell
            Vector2Int nextCellPos = nextCellList.First();
            nextCellList.Remove(nextCellPos);

            // Update Cell
            UpdateCell(nextCellPos);
            // Update Surrounding Cells Tiles
            UpdateSurroundingCells(nextCellPos);
        }
    }

    private void UpdateCell(Vector2Int cellPos)
    {
        Cell cell = grid[cellPos.x, cellPos.y];
        int tileCount = cell.possibleTiles.Count();
        cell.collapsed = true;

        if (tileCount == 0)
        {
            Debug.Log("Could not find possible cell");
            return;
        }
        Tile newTile = cell.possibleTiles[UnityEngine.Random.Range(0, tileCount)];
        Instantiate(newTile, new Vector2(cellPos.x, cellPos.y), Quaternion.identity, transform);

        cell.possibleTiles = new Tile[] { newTile };
    }

    private void UpdateSurroundingCells(Vector2Int centerCellPos)
    {
        // Left
        Vector2Int leftCellPos = new Vector2Int(centerCellPos.x - 1, centerCellPos.y);
        if (leftCellPos.x > 0)
        {
            UpdatePossibleTiles(centerCellPos, leftCellPos, Direction.left);
            AddCellForUpdating(leftCellPos);
        }
        // Right
        Vector2Int rightCellPos = new Vector2Int(centerCellPos.x + 1, centerCellPos.y);
        if (rightCellPos.x < xWidth)
        {
            UpdatePossibleTiles(centerCellPos, rightCellPos, Direction.right);
            AddCellForUpdating(rightCellPos);
        }
        // Up
        Vector2Int upCellPos = new Vector2Int(centerCellPos.x, centerCellPos.y + 1);
        if (upCellPos.y < yHeight)
        {
            UpdatePossibleTiles(centerCellPos, upCellPos, Direction.up);
            AddCellForUpdating(upCellPos);
        }
        // Down
        Vector2Int downCellPos = new Vector2Int(centerCellPos.x, centerCellPos.y - 1);
        if (downCellPos.y > 0)
        {
            UpdatePossibleTiles(centerCellPos, downCellPos, Direction.down);
            AddCellForUpdating(downCellPos);
        }
    }

    private void UpdatePossibleTiles(Vector2Int centerCellPos, Vector2Int dirCellPos, Direction dir)
    {
        // If Cell has already been collapsed
        if (grid[dirCellPos.x, dirCellPos.y].collapsed == true)
            return;

        Tile[] possibleTiles = grid[dirCellPos.x, dirCellPos.y].possibleTiles;
        Tile cTiile = grid[centerCellPos.x, centerCellPos.y].possibleTiles[0];

        List<Tile> tempTileList = new List<Tile>();
        foreach (var tile in possibleTiles)
        {
            Tile[] dirTileList;
            switch (dir)
            {
                case Direction.left:
                    dirTileList = tile.rightNeighbours;
                    break;
                case Direction.right:
                    dirTileList = tile.leftNeighbours;
                    break;
                case Direction.up:
                    dirTileList = tile.downNeighbours;
                    break;
                case Direction.down:
                    dirTileList = tile.upNeighbours;
                    break;
                default:
                    dirTileList = tile.rightNeighbours;
                    break;
            }
            foreach (var dirTile in dirTileList)
            {
                Tile[] centerTileList;
                switch (dir)
                {
                    case Direction.left:
                        centerTileList = cTiile.leftNeighbours;
                        break;
                    case Direction.right:
                        centerTileList = cTiile.rightNeighbours;
                        break;
                    case Direction.up:
                        centerTileList = cTiile.upNeighbours;
                        break;
                    case Direction.down:
                        centerTileList = cTiile.downNeighbours;
                        break;
                    default:
                        centerTileList = cTiile.rightNeighbours;
                        break;
                }
                foreach (var centerTile in centerTileList)
                {
                    if (centerTile.Equals(dirTile))
                    {
                        Debug.Log("Has Same Neighbours");
                        tempTileList.Add(centerTile);
                    }
                }
            }
        }
        grid[dirCellPos.x, dirCellPos.y].UpdateTileOptions(tempTileList.ToArray());
    }

    private void AddCellForUpdating(Vector2Int cellPos)
    {
        // If Cell has already been collapsed
        if (grid[cellPos.x, cellPos.y].collapsed == true)
            return;

        // If Cell is already ready to be updated
        if (nextCellList.Contains(cellPos))
            return;

        nextCellList.Add(cellPos);
    }

}
