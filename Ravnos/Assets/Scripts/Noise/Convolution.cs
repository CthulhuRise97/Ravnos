using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Convolution{
    public static float[,] ConvolutionMapGenerator(int width, int height, float[,] perlin, float[,] voronoi){
        float[,] noiseMap = new float[width,height];

        for(int x = 0; x < width; x++){
            for(int y = 0; y < height; y++){
                noiseMap[x,y] = perlin[x,y] * (voronoi[x,y] + 1);
            }
        }
        return noiseMap;
    }
}
