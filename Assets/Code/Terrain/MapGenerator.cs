using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// referencia https://github.com/SebLague/Procedural-Landmass-Generation


public class MapGenerator : MonoBehaviour
{
    public enum DrawMode {NoiseMap,ColourMap};
    public DrawMode drawMode;
    [Range(10, 1024)]
    public int mapWidth;
    [Range(10, 1024)]
    public int mapHeight;
    public float noiseScale;

    public int octaves = 4;
    [Range(0, 1)]
    public float persistance = 0.5f;
    public float lacunarity = 2.0f ;

    public int seed;
    public Vector2 offset;
    public bool autoUpdate;

    public TerrainType[] regions;

    public void GenerateMap()
    {
        float[,] noiseMap = Noise.GererateNoiseMap(mapWidth, mapHeight, noiseScale, octaves, persistance, lacunarity,seed,offset);
        // colocando as cores
        Color[] colourMap = new Color[mapWidth * mapHeight];
        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                float currentHeight = noiseMap[x, y];
                for (int i = 0; i < regions.Length; i++)
                {
                    if(currentHeight<=regions[i].height)
                    {
                        colourMap[y * mapWidth + x] = regions[i].colour;
                        break;
                    }
                }
            }
        }
        MapDisplay display = FindObjectOfType<MapDisplay>();
        if(drawMode == DrawMode.NoiseMap)
        {
            display.DrawTexture(TextureGenerator.TextureFromHeightMap(noiseMap));
        }else if (drawMode == DrawMode.ColourMap)
        {
            display.DrawTexture(TextureGenerator.TextureFromColourMap(colourMap, mapWidth, mapHeight));
        }
       
    }
    private void OnValidate()
    {
        //validadores das variaveis
        if(mapWidth < 1)
        {
            mapWidth = 1;
        }
        if (mapHeight < 1)
        {
            mapHeight = 1;
        }
        if (lacunarity < 1)
        {
            lacunarity = 1;
        }
        if(octaves < 0)
        {
            octaves=0;
        }
    }
}
[System.Serializable]
public struct TerrainType
{
    public string name;
    public float height;
    public Color colour;

}
