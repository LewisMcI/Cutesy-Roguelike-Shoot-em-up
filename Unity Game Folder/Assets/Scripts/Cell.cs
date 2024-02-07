using UnityEngine;

public enum TerrainType
{
    Grass,
    Sand,
    Water
}

[CreateAssetMenu(fileName = "Cell", menuName = "ScriptableObjects/Cell")]
public class Cell : ScriptableObject
{
    public GameObject prefab;
    public TerrainType terrainType;
    public bool hasUpSide;
    public bool hasDownSide;
    public bool hasLeftSide;
    public bool hasRightSide;
}
