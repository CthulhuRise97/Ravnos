using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public struct TerrainType
{
    public float heigth;
    public string name;
    public Color colour;
}


public class MapGenerator : MonoBehaviour
{
    public enum DrawMode { NoiseMap, HeighMap };
    public enum NoiseSelector { Perlin, Simplex, Value, Voronoi, PerlinVoronoi };

    //selectores
    public NoiseSelector noiseSelector;
    public DrawMode drawMode;

    //variables
    public int mapWidth = 200;
    public int mapHeigth = 120;
    public bool autoUpdate;
    public float lacunarity = 2;
    public Vector2 offset;
    public int octaves = 5;
    [Range(0, 1)]
    public float persistance = 0.5f;
    [Range(0, 50)]
    public float noiseScale = 45;
    public TerrainType[] regions;

    public void GenerateMap()
    {
        System.Random random = new System.Random();
        int RandomSeed = random.Next(0, 100000);

        System.Random local_seed = new System.Random(RandomSeed);
        int int_seed = local_seed.Next(-100000, 100000);
        float[,] NoiseMap;

        if (this.noiseSelector == NoiseSelector.Perlin) {
            NoiseMap = PerlinNoise.PerlinNoiseMapGenerator(this.mapWidth, this.mapHeigth, int_seed, this.noiseScale, octaves, persistance, lacunarity, offset);
        } else if (this.noiseSelector == NoiseSelector.Simplex) {
            NoiseMap = SimplexNoise.SimplexNoiseMapGenerator(this.mapWidth, this.mapHeigth, int_seed, octaves, offset);
        } else if (this.noiseSelector == NoiseSelector.Value) {
            NoiseMap = ValueNoise.ValueNoiseMapGenerator(this.mapWidth, this.mapHeigth, int_seed, this.noiseScale);
        } else if (this.noiseSelector == NoiseSelector.Voronoi) {
            NoiseMap = VoronoiNoise.ValueNoiseMapGenerator(this.mapWidth, this.mapHeigth, int_seed, this.noiseScale, octaves, persistance);
        } else if (this.noiseSelector == NoiseSelector.PerlinVoronoi) {
            float[,] perlinMap = PerlinNoise.PerlinNoiseMapGenerator(this.mapWidth, this.mapHeigth, int_seed, this.noiseScale, octaves, persistance, lacunarity, offset);
            float[,] voronoiMap = VoronoiNoise.ValueNoiseMapGenerator(this.mapWidth, this.mapHeigth, int_seed, this.noiseScale, octaves, persistance);
            NoiseMap = Convolution.ConvolutionMapGenerator(this.mapWidth, this.mapHeigth, perlinMap,voronoiMap);
        } else {
            //default: perlin noise
            NoiseMap = PerlinNoise.PerlinNoiseMapGenerator(this.mapWidth, this.mapHeigth, int_seed, this.noiseScale, octaves, persistance, lacunarity, offset);
        }

        Color[] HeighMap = new Color[mapWidth * mapHeigth];
        for (int y = 0; y < mapHeigth; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                float currentHeigth = NoiseMap[x, y];
                for (int i = 0; i < regions.Length; i++)
                {
                    if (currentHeigth <= regions[i].heigth)
                    {
                        HeighMap[y * mapWidth + x] = regions[i].colour;
                        break;
                    }
                }
            }
        }

        MapDisplay display = FindObjectOfType<MapDisplay>();
        if (drawMode == DrawMode.NoiseMap)
        {
            display.DrawTexture(TextureGenerator.TextureFromHeightMap(NoiseMap));
        }
        else if (drawMode == DrawMode.HeighMap)
        {
            display.DrawTexture(TextureGenerator.TextureFromHeighMap(HeighMap, mapWidth, mapHeigth));
        }
    }

    void OnValidate()
    {
        if (mapWidth < 1)
        {
            mapWidth = 1;
        }
        if (mapHeigth < 1)
        {
            mapHeigth = 1;
        }
        if (lacunarity < 1)
        {
            lacunarity = 1;
        }
        if (octaves < 0)
        {
            octaves = 0;
        }
    }
}
