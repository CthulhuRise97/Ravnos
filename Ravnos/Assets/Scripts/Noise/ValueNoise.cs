using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ValueNoise{
    public static float[,] ValueNoiseMapGenerator(int width, int height, int seed, float noiseScale){
        float[,] noiseMap = new float[width,height];

        ValueNoise noise = new ValueNoise(seed);
        for(int i = 0; i < width; i++){
            for(int j = 0; j < height; j++){
                float xCoord = (float)i / width;
                float yCoord = (float)j / height;
                float valor = noise.GetNoise(xCoord, yCoord, noiseScale);
                noiseMap[i,j] = valor;
            }
        }

        return noiseMap;
    }

    public enum Interpolation { Linear, SmoothStep, KenPerlin }
    private Interpolation interp = Interpolation.SmoothStep;

    private const int GridWidth = 256;
    private const int GridHeight = 256;
    private float[,] grid = new float[GridWidth, GridHeight];

    public ValueNoise() => FillGrid();
    public ValueNoise(int seed) => FillGrid(seed);
    public ValueNoise(Interpolation interp)
    {
        FillGrid();
        this.interp = interp;
    }
    public ValueNoise(int seed, Interpolation interp)
    {
        FillGrid(seed);
        this.interp = interp;
    }

    public float GetNoise(float x, float y, float scale = 10f) =>
        CalculateNoise(x * scale, y * scale);

    private void FillGrid()
    {
        for (int x = 0; x < GridWidth; x++)
            for (int y = 0; y < GridHeight; y++)
                grid[x, y] = UnityEngine.Random.value;
    }

    private void FillGrid(int seed)
    {
        UnityEngine.Random.State originalState = UnityEngine.Random.state;
        UnityEngine.Random.InitState(seed);
        FillGrid();
        UnityEngine.Random.state = originalState;
    }

    private float CalculateNoise(float x, float y)
    {
        float ax = Mathf.Abs(x);
        float ay = Mathf.Abs(y);

        int xMin = Mathf.FloorToInt(ax) % GridWidth;
        int yMin = Mathf.FloorToInt(ay) % GridHeight;
        int xMax = xMin == GridWidth - 1 ? 0 : xMin + 1;
        int yMax = yMin == GridHeight - 1 ? 0 : yMin + 1;

        float c00 = grid[xMin, yMin];
        float c10 = grid[xMax, yMin];
        float c01 = grid[xMin, yMax];
        float c11 = grid[xMax, yMax];

        float tx = ax - xMin;
        float ty = ay - yMin;

        float point0 = Interpolate(c00, c10, tx);
        float point1 = Interpolate(c01, c11, tx);
        float point = Interpolate(point0, point1, ty);

        return point;
    }

    private float Interpolate(float a, float b, float t)
    {
        switch (interp)
        {
            case Interpolation.Linear:
                return Mathf.Lerp(a, b, t);
            case Interpolation.SmoothStep:
                return Mathf.SmoothStep(a, b, t);
            case Interpolation.KenPerlin:
                return KenPerlin(a, b, t);
            default:
                return 0f;
        }
    }

    private float KenPerlin(float a, float b, float t)
    {
        float t1 = 6 * Mathf.Pow(t, 5f);
        float t2 = 15 * Mathf.Pow(t, 4f);
        float t3 = 10 * Mathf.Pow(t, 3f);

        return Mathf.Lerp(a, b, t1 - t2 + t3);
    }


}
