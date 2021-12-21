using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeGeneration : MonoBehaviour
{
    public NoiseMapGeneration noiseMapGeneration;

    public Wave[] waves;
    public float levelScale;
    public float[] neighborRadius;
    public GameObject[] treePrefab;

    public void GenerateTrees(int levelDepth, int levelWidth, float distanceBetweenVertices, LevelData levelData)
    {
        float[,] treeMap = this.noiseMapGeneration.GeneratePerlinNoiseMap(levelDepth, levelWidth, this.levelScale, 0, 0, this.waves);

        float levelSizeX = levelWidth * distanceBetweenVertices;
        float levelSizeZ = levelDepth * distanceBetweenVertices;

        for (int zIndex = 0; zIndex < levelDepth; zIndex++)
        {
            for (int xIndex = 0; xIndex < levelWidth; xIndex++)
            {
                TileCoordinate tileCoordinate = levelData.ConvertToTileCoordinate(zIndex, xIndex);
                TileData tileData = levelData.tilesData[tileCoordinate.tileZIndex, tileCoordinate.tileXIndex];
                int tileWidth = tileData.heightMap.GetLength(1);

                Vector3[] meshVertices = tileData.mesh.vertices;
                int vertexIndex = tileCoordinate.coordinateZIndex * tileWidth + tileCoordinate.coordinateXIndex;

                TerrainType terrainType = tileData.chosenHeightTerrainTypes[tileCoordinate.coordinateZIndex, tileCoordinate.coordinateXIndex];

                Biome biome = tileData.chosenBiomes[tileCoordinate.coordinateZIndex, tileCoordinate.coordinateXIndex];

                if(terrainType.name != "water")
                {
                    float treeValue = treeMap[zIndex, xIndex];

                    int terrainTypeIndex = terrainType.index;

                    int neighbotZBegin = (int)Mathf.Max(0, zIndex - this.neighborRadius[biome.index]);
                    int neighborZEnd = (int)Mathf.Min(levelDepth - 1, zIndex + this.neighborRadius[biome.index]);
                    int neighborXBegin = (int)Mathf.Max(0, xIndex, this.neighborRadius[biome.index]);
                    int neighborXEnd = (int)Mathf.Min(levelWidth - 1, xIndex + this.neighborRadius[biome.index]);

                    float maxValue = 0f;
                    for (int neighborZ = 0; neighborZ <= neighborZEnd; neighborZ++)
                    {
                        for (int neighborX = 0; neighborX <= neighborXEnd; neighborX++)
                        {
                            float neighborValue = treeMap[neighborZ, neighborX];

                            if(neighborValue >= maxValue)
                            {
                                maxValue = neighborValue;
                            }
                        }
                    }

                    if(treeValue == maxValue)
                    {
                        Vector3 treePosition = new Vector3(xIndex * distanceBetweenVertices, meshVertices[vertexIndex].y, zIndex * distanceBetweenVertices);
                        GameObject tree = Instantiate(this.treePrefab[biome.index], treePosition, Quaternion.identity) as GameObject;
                        // tree.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
                        // tree.transform.parent = this.parent.transform;
                    }
                }
            }
        }
    }
}
