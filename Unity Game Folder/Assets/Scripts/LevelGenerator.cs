using System;
using System.Data;
using UnityEditor.EditorTools;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public int width = 10;
    public int height = 10;
    bool showDebugUI = true;
    public Cell[] cells;

    private Cell[,] level;
    private void OnGUI()
    {
        if (showDebugUI)
        {
            GUILayout.BeginArea(new Rect(10, 10, 200, 200));
            GUILayout.Label("Debug UI");
            GUILayout.Space(10);

            // Width input field
            GUILayout.BeginHorizontal();
            GUILayout.Label("Width:");
            width = int.Parse(GUILayout.TextField(width.ToString()));
            GUILayout.EndHorizontal();

            // Height input field
            GUILayout.BeginHorizontal();
            GUILayout.Label("Height:");
            height = int.Parse(GUILayout.TextField(height.ToString()));
            GUILayout.EndHorizontal();

            GUILayout.Space(10);

            // Generate Level button
            if (GUILayout.Button("Generate Level"))
            {
                GenerateLevel();
            }

            GUILayout.EndArea();
        }
    }
    void Start()
    {
        GenerateLevel();
    }

    void GenerateLevel()
    {
        DeleteLevel();
        // Generate Grid
        level = new Cell[width, height];

        // Calculate starting position relative to the generator's transform
        Vector3 startPosition = transform.position - new Vector3((width - 1) * 0.5f, (height - 1) * 0.5f, 0);

        // Loop through each position within
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                // Find Cell
                // Check if surrounding cell has side
                // Left
                bool canLeft = CheckCell(new Vector2(x - 1, y), Vector3.right);
                bool canRight = CheckCell(new Vector2(x + 1, y), Vector3.left);
                bool canUp = CheckCell(new Vector2(x, y + 1), Vector3.down);
                bool canDown = CheckCell(new Vector2(x, y - 1), Vector3.up);
                TerrainType terrainType = TerrainType.Grass;
                Cell newCell = GetCell(terrainType, canLeft, canRight, canUp, canDown);

                level[x, y] = newCell;

                // Calculate spawn position relative to starting position
                Vector3 spawnPos = startPosition + new Vector3(x, y, 0);

                // Instantiate cell prefab at spawn position
                Instantiate(level[x, y].prefab, spawnPos, Quaternion.identity);
            }
        }
    }

    private void DeleteLevel()
    {
        if (level != null)
        {
            foreach (var cell in level)
            {
                Destroy(cell);
            }
        }
    }
    private Cell GetCell(TerrainType terrainType, bool canLeft, bool canRight, bool canUp, bool canDown)
    {
        if (canLeft && canRight && canUp && canDown)
        {
            // Normal Grass
        }
        else if (!canLeft && canRight && canUp && canDown)
        {
            return cells[1];
        }
        else if (canLeft && !canRight && canUp && canDown)
        {
            return cells[2];
        }
        else if (canLeft && canRight && !canUp && canDown)
        {
            return cells[3];
        }
        else if (canLeft && canRight && canUp && !canDown)
        {
            return cells[4];
        }
        return cells[0];
    }

    private bool CheckCell(Vector2 cellPos, Vector3 side)
    {
        // Check if cellPos is within the bounds of the level array
        if (cellPos.x < 0 || cellPos.y < 0 || cellPos.x > width-1 || cellPos.y > height-1)
            return false;
        // Check if the cell at cellPos is null
        if (level[(int)cellPos.x, (int)cellPos.y] == null)
            return true;

        // Right of cellPos
        if (side == Vector3.right)
            return !(level[(int)cellPos.x, (int)cellPos.y].hasRightSide);
        // Left of cellPos
        if (side == Vector3.left)
            return !(level[(int)cellPos.x, (int)cellPos.y].hasLeftSide);
        // Up of cellPos
        if (side == Vector3.up)
            return !(level[(int)cellPos.x, (int)cellPos.y].hasUpSide);
        // Down of cellPos
        if (side == Vector3.down)
            return !(level[(int)cellPos.x, (int)cellPos.y].hasDownSide);

        else
            return false;
    }
}
