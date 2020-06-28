using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinNoise{
    private static byte[] _perm;

    //perlin noise
    public static float[,] PerlinNoiseMapGenerator(int width, int height, int seed, float scale, int octaves, float persistance, float lacunarity, Vector2 offset){
        float[,] noiseMap = new float[width, height];
        System.Random prng = new System.Random(seed);
        Vector2[] octavesOffset = new Vector2[octaves];
        
        for(int i = 0; i < octaves; i++){
            float offsetX = prng.Next(-100000,100000) + offset.x;
            float offsetY = prng.Next(-100000,100000) + offset.y;
            octavesOffset[i] = new Vector2(offsetX,offsetY);
        }

        //setting minimum value
        if(scale <= 0){
            scale = 0.0001f;
        }

        float maxNoiseHeigth = float.MinValue;
        float minNoiseHeigth = float.MaxValue;

        float halfWidth = width / 2;
        float halfHeigth = height / 2;

        for(int y = 0; y < height; y++){
            for(int x = 0; x < width; x++){
                float amplitude = 1;
                float frequency = 1;
                float noiseHeigth = 0;

                for(int i = 0; i < octaves; i++){
                    float sampleX = (x - halfWidth) / scale * frequency + octavesOffset[i].x;
                    float sampleY = (y - halfHeigth) / scale * frequency + octavesOffset[i].y;

                    float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;
                    noiseMap[x,y] = perlinValue;
                    noiseHeigth += perlinValue * amplitude;
                    
                    amplitude *= persistance;
                    frequency *= lacunarity;
                }
                if(noiseHeigth > maxNoiseHeigth){
                    maxNoiseHeigth = noiseHeigth;
                }else if(noiseHeigth < minNoiseHeigth){
                    minNoiseHeigth = noiseHeigth;
                }
                noiseMap[x,y] = noiseHeigth;
            }
        }

        for(int y = 0; y < height; y++){
            for(int x = 0; x < width; x++){
                noiseMap[x,y] = Mathf.InverseLerp(minNoiseHeigth, maxNoiseHeigth, noiseMap[x,y]);
            }
        }
        return noiseMap;
    }
}
