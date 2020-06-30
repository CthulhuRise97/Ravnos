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
    public enum DrawMode { NoiseMap, ColourMap };
    public DrawMode drawMode;

    public int mapWidth;
    public int mapHeigth;
    public float noiseScale;
    public bool autoUpdate;

    public int octaves;
    [Range(0, 1)]
    public float persistance;
    public float lacunarity;
    public Vector2 offset;

    //tipos de ruido
    public enum NoiseSelector { Perlin, Simplex, Value, Voronoi, Worley };
    public NoiseSelector noiseSelector;

    public TerrainType[] regions;

    public void GenerateMap()
    {
        System.Random random = new System.Random();
        int RandomSeed = random.Next(0, 100000);

        System.Random local_seed = new System.Random(RandomSeed);
        int int_seed = local_seed.Next(-100000, 100000);
        float[,] NoiseMap;

        if (this.noiseSelector == NoiseSelector.Perlin)
        {
            NoiseMap = PerlinNoise.PerlinNoiseMapGenerator(this.mapWidth, this.mapHeigth, int_seed, this.noiseScale, octaves, persistance, lacunarity, offset);
        }
        else if (this.noiseSelector == NoiseSelector.Simplex)
        {
            NoiseMap = SimplexNoise.SimplexNoiseMapGenerator(this.mapWidth,this.mapHeigth, int_seed, octaves, offset);
        }else if(this.noiseSelector == NoiseSelector.Value)
        {
            NoiseMap = ValueNoise.ValueNoiseMapGenerator(this.mapWidth,this.mapHeigth, int_seed, this.noiseScale);
        }/*else if(this.noiseSelector == NoiseSelector.Voronoi){

        }else if(this.noiseSelector == NoiseSelector.Worley){

        }*/
        else
        {
            //default: perlin noise
            NoiseMap = PerlinNoise.PerlinNoiseMapGenerator(this.mapWidth, this.mapHeigth, int_seed, this.noiseScale, octaves, persistance, lacunarity, offset);
        }

        Color[] colourMap = new Color[mapWidth * mapHeigth];
        for (int y = 0; y < mapHeigth; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                float currentHeigth = NoiseMap[x, y];
                for (int i = 0; i < regions.Length; i++)
                {
                    if (currentHeigth <= regions[i].heigth)
                    {
                        colourMap[y * mapWidth + x] = regions[i].colour;
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
        else if (drawMode == DrawMode.ColourMap)
        {
            display.DrawTexture(TextureGenerator.TextureFromColourMap(colourMap, mapWidth, mapHeigth));
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
