using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public struct TerrainType{
    public float heigth;
    public string name;
    public Color colour;
}


public class MapGenerator : MonoBehaviour{
    public enum DrawMode{NoiseMap, ColourMap};
    public DrawMode drawMode;

    public int mapWidth;
    public int mapHeigth;
    public float noiseScale;
    public bool autoUpdate;

    public int octaves;
    [Range(0,1)]
    public float persistance;
    public float lacunarity;

    //public int seed;
    public Vector2 offset;
    public bool SimplexNoiseVar;

    public TerrainType[] regions;

    public void GenerateMap(){
        System.Random random = new System.Random(); 
        Debug.Log("Generando mapa");
        int RandomSeed = random.Next(0,100000);
        System.Random local_seed = new System.Random(RandomSeed);
        int local_seed_ = local_seed.Next(-100000,100000);
        float[,] NoiseMap;
        if(this.SimplexNoiseVar == true){
            Debug.Log("Simplex activado");
            NoiseMap = SimplexNoise.Calc2D(this.mapWidth, this.mapHeigth, this.noiseScale);
        }else{
            Debug.Log("Perlin activado");
            NoiseMap = PerlinNoise.PerlinNoiseMapGenerator(this.mapWidth, this.mapHeigth, local_seed_, this.noiseScale, octaves, persistance,lacunarity, offset);
        }

        Color[] colourMap = new Color[mapWidth * mapHeigth];
        Debug.Log("Generando mapa de colores");
        for(int y = 0; y < mapHeigth; y++){
            for(int x = 0; x < mapWidth; x++){
                float currentHeigth = NoiseMap [x, y];
                for(int i = 0; i < regions.Length; i++){
                    if(currentHeigth <= regions[i].heigth){
                        colourMap[y * mapWidth + x] = regions[i].colour;
                        break;
                    }
                }
            }
        }

        MapDisplay display = FindObjectOfType<MapDisplay>();
        if(drawMode == DrawMode.NoiseMap){
            Debug.Log("Creando mapa de ruido");
            display.DrawTexture(TextureGenerator.TextureFromHeightMap(NoiseMap));
        }else if(drawMode == DrawMode.ColourMap){
            Debug.Log("Creando mapa de colores");
            display.DrawTexture(TextureGenerator.TextureFromColourMap(colourMap, mapWidth, mapHeigth));
        }
    }

    void OnValidate(){
        if(mapWidth < 1){
            mapWidth = 1;
        }
        if(mapHeigth < 1){
            mapHeigth = 1;
        }
        if(lacunarity < 1){
            lacunarity = 1;
        }
        if(octaves < 0){
            octaves = 0;
        }
    }
}
