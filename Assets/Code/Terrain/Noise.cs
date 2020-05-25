using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// referencia https://github.com/SebLague/Procedural-Landmass-Generation

public static class Noise{
 // Responsavel em devolver um grid onde teremos valores entre 0 e 1
 // persistence - mexe na amplitude
 // lacunarity - na frequencia
 public static float [,] GererateNoiseMap(int mapWidth, int mapHeight, float scale, int octaves, float persistance , float lacunarity,int seed , Vector2 offset)
    {
        float[,] noiseMap = new float[mapWidth, mapHeight];

        // garantir diferentes octaves com diferentes seeds
        System.Random prng = new System.Random(seed);
        Vector2[] octaveOffsets = new Vector2[octaves];
        for (int i = 0; i < octaves; i++)
        {
            float offsetX = prng.Next(-100000, 100000)+offset.x;
            float offsetY = prng.Next(-100000, 100000)+offset.y;
            octaveOffsets[i] = new Vector2(offsetX, offsetY);
        }

        // para a scala não ficar 0 definiremos uma scala minima.
        if (scale <= 0)
        {
            scale = 0.00001f;
        }
        //normalizar o noisemap
        float maxNoiseHeight = float.MinValue;
        float minNoiseHeight = float.MaxValue;

        float halfWidth = mapWidth / 2f;
        float halfHeight = mapHeight / 2f;

        for(int y = 0; y < mapHeight; y++)
        {
            for(int x = 0; x < mapWidth; x++)
            {
                float amplitude = 1;
                float frequency = 1;
                float noiseHeight = 0;

                for (int i = 0; i < octaves; i++)
                {
                     
                    float sampleX = (x-halfWidth) / scale * frequency + octaveOffsets[i].x;
                    float sampleY = (y-halfHeight) / scale * frequency + octaveOffsets[i].y;
                    //valor de -1 a 1
                    float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;
                    noiseHeight += perlinValue * amplitude;
                    amplitude *= persistance;
                    frequency *= lacunarity;
                }
                if (noiseHeight > maxNoiseHeight)
                {
                    maxNoiseHeight = noiseHeight;
                }
                else if (noiseHeight < minNoiseHeight)
                {
                    minNoiseHeight = noiseHeight;
                }
                noiseMap[x, y] = noiseHeight;
            }
        }
        //normalizado o noiseMap
        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                noiseMap[x, y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, noiseMap[x, y]);
            }

        }
        return noiseMap;
    }
}
