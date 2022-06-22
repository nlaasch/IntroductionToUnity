using System;
using UnityEngine;
using System.Collections;

public class MapGenerator : MonoBehaviour {

    public enum DrawMode {NoiseMap, ColourMap, Mesh, FalloffMap, CustomFallOff};
    public DrawMode drawMode;



    const int mapChunkSize = 241;

    [Range(0,6)]
    public int levelOfDetail;
    
    public float noiseScale;

    public int octaves;
    [Range(0,1)]
    public float persistance;
    public float lacunarity;

    public int seed;
    public Vector2 offset;
    

    public Vector2 falloffVariables = new Vector2(3,2.2f);
    public bool useFalloff;

    public float meshHeightMultiplier;
    public AnimationCurve meshHeightCurve;

    public bool autoUpdate;

    public bool useRegions;
    public TerrainType[] regions;
    public Gradient regionGradient;
    

    private float[,] falloffMap;

    private void Awake()
    {
        falloffMap = FalloffGenerator.GenerateFalloffMap(mapChunkSize, falloffVariables.x, falloffVariables.y);
        seed = SeedHolder.seed;
        GenerateMap();
    }
    


    public void GenerateMap() {
        float[,] noiseMap = Noise.GenerateNoiseMap (mapChunkSize, mapChunkSize, seed, noiseScale, octaves, persistance, lacunarity, offset);

        Color[] colourMap = new Color[mapChunkSize * mapChunkSize];
        for (int y = 0; y < mapChunkSize; y++) {
            for (int x = 0; x < mapChunkSize; x++) {
                if (useFalloff)
                {
                    noiseMap[x,y] = Mathf.Clamp01(noiseMap[x,y] - falloffMap[x,y]);
                }
                float currentHeight = noiseMap [x, y];
                if (useRegions)
                {
                    for (int i = 0; i < regions.Length; i++) {
                        if (currentHeight <= regions [i].height) {
                            colourMap [y * mapChunkSize + x] = regions [i].colour;
                            break;
                        }
                    }
                }
                else
                {
                    colourMap[y * mapChunkSize + x] = regionGradient.Evaluate(currentHeight);
                }
                
            }
        }

        MapDisplay display = FindObjectOfType<MapDisplay> ();
        if (drawMode == DrawMode.NoiseMap) {
            display.DrawTexture (TextureGenerator.TextureFromHeightMap (noiseMap));
        } else if (drawMode == DrawMode.ColourMap) {
            display.DrawTexture (TextureGenerator.TextureFromColourMap (colourMap, mapChunkSize, mapChunkSize));
        } else if (drawMode == DrawMode.Mesh) {
            display.DrawMesh (MeshGenerator.GenerateTerrainMesh (noiseMap, meshHeightMultiplier, meshHeightCurve, levelOfDetail), TextureGenerator.TextureFromColourMap (colourMap, mapChunkSize, mapChunkSize));
        } else if (drawMode == DrawMode.FalloffMap)
        {
            display.DrawTexture(TextureGenerator.TextureFromHeightMap(FalloffGenerator.GenerateFalloffMap(mapChunkSize, falloffVariables.x, falloffVariables.y)));
        }
    }

    void OnValidate() {
        if (falloffVariables.x <= 0)
        {
            falloffVariables.x = 0.001f;
        }
        if (falloffVariables.y <= 0)
        {
            falloffVariables.y = 0.001f;
        }
        
        if (lacunarity < 1) {
            lacunarity = 1;
        }
        if (octaves < 0) {
            octaves = 0;
        }
        falloffMap = FalloffGenerator.GenerateFalloffMap(mapChunkSize, falloffVariables.x, falloffVariables.y);
    }
}

[System.Serializable]
public struct TerrainType {
    public string name;
    public float height;
    public Color colour;
}