using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGeneration : MonoBehaviour
{
    public NoiseMapGeneration noiseMapGeneration;
    public MeshRenderer tileRenderer;
    public MeshFilter meshFilter;
    public MeshCollider meshCollider;

    public float levelScale = 3;
    public float heightMultiplier = 3;
    // public int heightMultiplierMultiplier = 1;

    public AnimationCurve heightCurve;
    public AnimationCurve heatCurve;
    public AnimationCurve moistureCurve;

    public Wave[] heightWaves;
    public Wave[] heatWaves;
    public Wave[] moistureWaves;

    public BiomeRow[] biomes;

    public Color waterColor;

    public TerrainType[] heightTerrainTypes;
    public TerrainType[] heatTerrainTypes;
    public TerrainType[] moistureTerrainTypes;

    [SerializeField] private VisualizationMode visualizationMode;

    public TileData GenerateTile(float centerVertexZ, float maxDistanceZ)
    {
        // heightMultiplier *= heightMultiplierMultiplier;

        Vector3[] meshVertices = this.meshFilter.mesh.vertices;
        int tileDepth = (int)Mathf.Sqrt(meshVertices.Length);
        int tileWidth = tileDepth;

        float offsetX = -this.gameObject.transform.position.x;
        float offsetZ = -this.gameObject.transform.position.z;

        float[,] heightMap = this.noiseMapGeneration.GeneratePerlinNoiseMap(tileDepth, tileWidth, this.levelScale, offsetX, offsetZ, this.heightWaves);

        Vector3 tileDimensions = this.meshFilter.mesh.bounds.size;
        float distanceBetweenVertices = tileDimensions.z / (float)tileDepth;
        float vertexOffsetZ = this.gameObject.transform.position.z / distanceBetweenVertices;

        float[,] uniformHeatMap = this.noiseMapGeneration.GenerateUniformNoiseMap(tileDepth, tileWidth, centerVertexZ, maxDistanceZ, vertexOffsetZ);
        float[,] randomHeatMap = this.noiseMapGeneration.GeneratePerlinNoiseMap(tileDepth, tileWidth, this.levelScale, offsetX, offsetZ, this.heatWaves);
        float[,] heatMap = new float[tileDepth, tileWidth];

        for (int zIndex = 0; zIndex < tileDepth; zIndex++)
        {
            for (int xIndex = 0; xIndex < tileWidth; xIndex++)
            {
                heatMap[zIndex, xIndex] = uniformHeatMap[zIndex, xIndex] * randomHeatMap[zIndex, xIndex];
                heatMap[zIndex, xIndex] += this.heatCurve.Evaluate(heightMap[zIndex, xIndex]) * heightMap[zIndex, xIndex];
            }
        }

        float[,] moistureMap = this.noiseMapGeneration.GeneratePerlinNoiseMap(tileDepth, tileWidth, this.levelScale, offsetX, offsetZ, this.moistureWaves);
        for (int zIndex = 0; zIndex < tileDepth; zIndex++)
        {
            for (int xIndex = 0; xIndex < tileWidth; xIndex++)
            {
                moistureMap[zIndex, xIndex] -= this.moistureCurve.Evaluate(heightMap[zIndex, xIndex]) * heightMap[zIndex, xIndex];
            }
        }

        TerrainType[,] chosenHeightTerrainTypes = new TerrainType[tileDepth, tileWidth];
        Texture2D heightTexture = BuildTexture(heightMap, this.heightTerrainTypes, chosenHeightTerrainTypes);

        TerrainType[,] chosenHeatTerrainTypes = new TerrainType[tileDepth, tileWidth];
        Texture2D heatTexture = BuildTexture(heatMap, this.heatTerrainTypes, chosenHeatTerrainTypes);

        TerrainType[,] chosenMoistureTerrainTypes = new TerrainType[tileDepth, tileWidth];
        Texture2D moistureTexture = BuildTexture(moistureMap, this.moistureTerrainTypes, chosenMoistureTerrainTypes);

        //Texture2D biomeTexture = BuildBiomeTexture(chosenHeightTerrainTypes, chosenHeatTerrainTypes, chosenMoistureTerrainTypes);
        Biome[,] chosenBiomes = new Biome[tileDepth, tileWidth];
        Texture2D biomeTexture = BuildBiomeTexture(chosenHeightTerrainTypes, chosenHeatTerrainTypes, chosenMoistureTerrainTypes, chosenBiomes);

        switch (this.visualizationMode)
        {
            case VisualizationMode.Height:
                this.tileRenderer.material.mainTexture = heightTexture;
                break;
            case VisualizationMode.Heat:
                this.tileRenderer.material.mainTexture = heatTexture;
                break;
            case VisualizationMode.Moisture:
                this.tileRenderer.material.mainTexture = moistureTexture;
                break;
            case VisualizationMode.Biome:
                this.tileRenderer.material.mainTexture = biomeTexture;
                break;
        }

        UpdateMeshVertices(heightMap);

        TileData tileData = new TileData(heightMap, heatMap, moistureMap,
            chosenHeightTerrainTypes, chosenHeatTerrainTypes, chosenMoistureTerrainTypes, chosenBiomes,
            this.meshFilter.mesh, (Texture2D)this.tileRenderer.material.mainTexture);

        return tileData;
    }

    private Texture2D BuildTexture(float[,] heightMap, TerrainType[] terrainTypes, TerrainType[,] chosenTerrainTypes)
    {
        int tileDepth = heightMap.GetLength(0);
        int tileWidth = heightMap.GetLength(1);

        Color[] colorMap = new Color[tileDepth * tileWidth];
        for (int zIndex = 0; zIndex < tileDepth; zIndex++)
        {
            for (int xIndex = 0; xIndex < tileWidth; xIndex++)
            {
                int colorIndex = zIndex * tileWidth + xIndex;
                float height = heightMap[zIndex, xIndex];

                TerrainType terrainType = ChooseTerrainType(height, terrainTypes);

                colorMap[colorIndex] = terrainType.color;

                chosenTerrainTypes[zIndex, xIndex] = terrainType;
            }
        }

        Texture2D tileTexture = new Texture2D(tileWidth, tileDepth);
        tileTexture.wrapMode = TextureWrapMode.Clamp;
        tileTexture.SetPixels(colorMap);
        tileTexture.Apply();

        return tileTexture;
    }

    TerrainType ChooseTerrainType(float noise, TerrainType[] terrainTypes)
    {
        foreach (TerrainType terrainType in terrainTypes)
        {
            if(noise < terrainType.threshold)
            {
                return terrainType;
            }
        }
        return terrainTypes[terrainTypes.Length - 1];
    }

    private void UpdateMeshVertices(float[,] heightMap)
    {
        int tileDepth = heightMap.GetLength(0);
        int tileWidth = heightMap.GetLength(1);

        Vector3[] meshVertices = this.meshFilter.mesh.vertices;

        int vertexIndex = 0;
        for (int zIndex = 0; zIndex < tileDepth; zIndex++)
        {
            for (int xIndex = 0; xIndex < tileWidth; xIndex++)
            {
                float height = heightMap[zIndex, xIndex];

                Vector3 vertex = meshVertices[vertexIndex];

                meshVertices[vertexIndex] = new Vector3(vertex.x, this.heightCurve.Evaluate(height) * this.heightMultiplier, vertex.z);

                vertexIndex++;
            }
        }

        this.meshFilter.mesh.vertices = meshVertices;
        this.meshFilter.mesh.RecalculateBounds();
        this.meshFilter.mesh.RecalculateNormals();

        this.meshCollider.sharedMesh = this.meshFilter.mesh;
    }

    private Texture2D BuildBiomeTexture(TerrainType[,] heightTerrainTypes, TerrainType[,] heatTerrainTypes, TerrainType[,] moistureTerrainTypes, Biome[,] chosenBiomes)
    {
        int tileDepth = heatTerrainTypes.GetLength(0);
        int tileWidth = heatTerrainTypes.GetLength(1);

        Color[] colorMap = new Color[tileDepth * tileWidth];
        for (int zIndex = 0; zIndex < tileDepth; zIndex++)
        {
            for (int xIndex = 0; xIndex < tileWidth; xIndex++)
            {
                int colorIndex = zIndex * tileWidth + xIndex;

                TerrainType heightTerrainType = heightTerrainTypes[zIndex, xIndex];

                if(heightTerrainType.name != "water")
                {
                    TerrainType heatTerrainType = heatTerrainTypes[zIndex, xIndex];
                    TerrainType moistureTerrainType = moistureTerrainTypes[zIndex, xIndex];

                    Biome biome = this.biomes[moistureTerrainType.index].biomes[heatTerrainType.index];

                    colorMap[colorIndex] = biome.color;

                    chosenBiomes[zIndex, xIndex] = biome;
                } else
                {
                    colorMap[colorIndex] = this.waterColor;
                }
            }
        }

        Texture2D tileTexture = new Texture2D(tileWidth, tileDepth);
        tileTexture.wrapMode = TextureWrapMode.Clamp;
        tileTexture.SetPixels(colorMap);
        tileTexture.Apply();

        return tileTexture;
    }
}

[System.Serializable]
public class TerrainType
{
    public string name;
    public float threshold;
    public Color color;
    public int index;
}

[System.Serializable]
public class Biome
{
    public string name;
    public Color color;
    public int index;
}

[System.Serializable]
public class BiomeRow
{
    public Biome[] biomes;
}

public class TileData
{
    public float[,] heightMap;
    public float[,] heatMap;
    public float[,] moistureMap;

    public TerrainType[,] chosenHeightTerrainTypes;
    public TerrainType[,] chosenHeatTerrainTypes;
    public TerrainType[,] chosenMoistureTerrainTypes;
    public Biome[,] chosenBiomes;

    public Mesh mesh;
    public Texture2D texture;

    public TileData(float[,] heightMap, float[,] heatMap, float[,] moistureMap,
        TerrainType[,] chosenHeightTerrainTypes, TerrainType[,] chosenHeatTerrainTypes, TerrainType[,] chosenMoistureTerrainTypes,
        Biome[,] chosenBiomes, Mesh mesh, Texture2D texture)
    {
        this.heightMap = heightMap;
        this.heatMap = heatMap;
        this.moistureMap = moistureMap;
        this.chosenHeightTerrainTypes = chosenHeightTerrainTypes;
        this.chosenHeatTerrainTypes = chosenHeatTerrainTypes;
        this.chosenMoistureTerrainTypes = chosenMoistureTerrainTypes;
        this.chosenBiomes = chosenBiomes;
        this.mesh = mesh;
        this.texture = texture;
    }
}

enum VisualizationMode {
    Height,
    Heat,
    Moisture,
    Biome
}