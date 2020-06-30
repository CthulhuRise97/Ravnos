using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ConvolutionSystem{

    public ConvolutionSystem(){
        
    }
    public static float[,] convolution(int width, int height, float[,] mapP, float[,] mapV){
        float[,] noiseMap = new float[width, height];

        for(int x = 0; x < width; x++){
            for(int y = 0; y < height; y++){
                float va1 = mapP[x,y];
                float va2 = mapV[x,y];

                float newVal = va1 + va2;
                noiseMap[x,y] = newVal;
            }
        }

        return noiseMap;
    }
}
