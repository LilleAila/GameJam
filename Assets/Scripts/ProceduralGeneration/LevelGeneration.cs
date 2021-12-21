using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    public int levelWidthInTiles, levelDepthInTiles;

    public int scale = 1;

    public GameObject tilePrefab;
    public GameObject treeParent;

    // public float centerVertexZ, maxDistanceZ;

    public TreeGeneration treeGeneration;
    public RiverGeneration riverGeneration;

    private void Start()
    {
        GenerateMap();
    }

    void GenerateMap()
    {
        float centerVertZ = 11 * levelDepthInTiles;
        float maxDistZ = 11 * levelWidthInTiles;

        Vector3 tileSize = tilePrefab.GetComponent<MeshRenderer>().bounds.size;
        int tileWidth = (int)tileSize.x;
        int tileDepth = (int)tileSize.z;

        Vector3[] tileMeshVertices = tilePrefab.GetComponent<MeshFilter>().sharedMesh.vertices;
        int tileDepthInVertices = (int)Mathf.Sqrt(tileMeshVertices.Length);
        int tileWidthInVertices = tileDepthInVertices;

        float distanceBetweenVertices = (float)tileDepth / (float)tileDepthInVertices;

        LevelData levelData = new LevelData(tileDepthInVertices, tileWidthInVertices, this.levelDepthInTiles, this.levelWidthInTiles);

        for (int xTileIndex = 0; xTileIndex < levelWidthInTiles; xTileIndex++)
        {
            for (int zTileIndex = 0; zTileIndex < levelDepthInTiles; zTileIndex++)
            {
                Vector3 tilePosition = new Vector3(this.gameObject.transform.position.x + xTileIndex * tileWidth,
                    this.gameObject.transform.position.y,
                    this.gameObject.transform.position.z + zTileIndex * tileDepth);

                GameObject tile = Instantiate(tilePrefab, tilePosition, Quaternion.identity) as GameObject;
                TileData tileData = tile.GetComponent<TileGeneration>().GenerateTile(centerVertZ, maxDistZ);
                tile.transform.parent = this.transform;

                levelData.addTileData(tileData, zTileIndex, xTileIndex);
            }
        }

        // treeGeneration.GenerateTrees(this.levelDepthInTiles * tileDepthInVertices, this.levelWidthInTiles * tileWidthInVertices, distanceBetweenVertices, levelData);

        // riverGeneration.GenerateRivers(this.levelDepthInTiles * tileDepthInVertices, this.levelWidthInTiles * tileWidthInVertices, levelData);

        this.transform.localScale = new Vector3(this.scale, this.scale, this.scale);
        this.transform.position += new Vector3(0, -(10 * this.scale), 0);
    }
}

public class LevelData
{
    private int tileDepthInVertices, tileWidthInVertices;

    public TileData[,] tilesData;

    public LevelData(int tileDepthInVertices, int tileWidthInVertices, int levelDepthInTiles, int levelWidthInTiles)
    {
        tilesData = new TileData[tileDepthInVertices * levelDepthInTiles, tileWidthInVertices * levelWidthInTiles];

        this.tileDepthInVertices = tileDepthInVertices;
        this.tileWidthInVertices = tileWidthInVertices;
    }

    public void addTileData(TileData tileData, int tileZIndex, int tileXIndex)
    {
        tilesData[tileZIndex, tileXIndex] = tileData;
    }

    public TileCoordinate ConvertToTileCoordinate(int zIndex, int xIndex) {
        int tileZIndex = (int)Mathf.Floor((float)zIndex / (float)this.tileDepthInVertices);
        int tileXIndex = (int)Mathf.Floor((float)xIndex / (float)this.tileWidthInVertices);

        int coordinateZIndex = this.tileDepthInVertices - (zIndex % this.tileDepthInVertices) - 1;
        int coordinateXIndex = this.tileWidthInVertices - (xIndex % this.tileWidthInVertices) - 1;

        TileCoordinate tileCoordinate = new TileCoordinate(tileZIndex, tileXIndex, coordinateZIndex, coordinateXIndex);
        return tileCoordinate;
    }
}

public class TileCoordinate
{
    public int tileZIndex;
    public int tileXIndex;
    public int coordinateZIndex;
    public int coordinateXIndex;

    public TileCoordinate(int tileZIndex, int tileXIndex, int coordinateZIndex, int coordinateXIndex)
    {
        this.tileZIndex = tileZIndex;
        this.tileXIndex = tileXIndex;
        this.coordinateZIndex = coordinateZIndex;
        this.coordinateXIndex = coordinateXIndex;
    }
}