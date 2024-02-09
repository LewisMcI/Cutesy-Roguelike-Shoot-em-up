using UnityEngine;

public class Cell : MonoBehaviour
{
    public bool collapsed;
    public Tile[] possibleTiles;

    public void CreateCell(Tile[] possibleTiles)
    {
        collapsed = false;
        this.possibleTiles = possibleTiles;
    }
    
    public void UpdateTileOptions(Tile[] newPossibleTiles)
    {
        possibleTiles = newPossibleTiles;
    }
}
