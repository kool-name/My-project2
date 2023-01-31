using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public static class Noise
{
    public static float[,] GenerateNoiseRand(int width, int length)
    {
        float[,] noiseMap = new float[width, length];
        for (int x =0; x<width; ++x)
        {
            for (int y = 0; y < width; ++y)
            {
                noiseMap[x, y] = Random.Range(-1f, 1f);
            }
        }
        return noiseMap;
    }

    public static float[,,] GenerateNoiseRand(int width, int length, int height)
    {
        float[,,] noiseMap = new float[width,length, height];
        for (int x = 0; x < width; ++x)
        {
            for (int y = 0; y < length; ++y)
            {
                for (int z = 0; z < height; ++z)
                {
                    noiseMap[x, y, z] = Random.Range(-1f, 1f);
                }
            }
        }
        return noiseMap;
    }

    public static float[,] GenerateNoisePerl(int width, int height, float scale, float lacunarity, float persistanse, int octaves)
    {
        float[,] noiseMap = new float[width, height];

        if (scale <= 0)
        {
            scale = 0.0001f;
        }

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                float sampleX = x / scale;
                float sampleY = y / scale;

                float perlinValue = Perlin.Noise(sampleX, sampleY);
                noiseMap[x, y] = perlinValue;
            }
        }

        return noiseMap;
    }

    public static float[,,] GenerateNoisePerl(int width, int length, int height, float scale, float lacunarity, float persistanse, int octaves, Vector3 Offset)
    {
        float[,,] noiseMap = new float[width, length, height];

        if (scale <= 0)
        {
            scale = 0.0001f;
        }

        Vector3[] octavesOffset = new Vector3[octaves];

        System.Random rand = new System.Random();

        for (int i =0; i<octavesOffset.Length; ++i)
        {
            octavesOffset[i].x = rand.Next(-5000, 5000);
            octavesOffset[i].y = rand.Next(-5000, 5000);
            octavesOffset[i].z = rand.Next(-5000, 5000);
        }
        float maxNoiseValue = float.MinValue;
        float minNoiseValue = float.MaxValue;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < length; y++)
            {
                for (int z = 0; z < height; z++)
                {

                    float noiseValue = 0;
                    float frequency = 1;
                    float amplitude = 1;

                    for(int n = 0; n < octaves; ++n)
                    {
                        float sampleX = (x + Offset.x) / scale*frequency*octavesOffset[n].x;
                        float sampleY = (y + Offset.y) / scale * frequency * octavesOffset[n].y;
                        float sampleZ = (z + Offset.z) / scale * frequency * octavesOffset[n].z;

                        noiseValue += (Perlin.Noise(sampleX, sampleY, sampleZ)*2-1)*amplitude;

                        amplitude *= persistanse;
                        frequency *= lacunarity;
                    }

                    if (noiseValue < minNoiseValue) minNoiseValue = noiseValue;
                    else if (noiseValue > maxNoiseValue) maxNoiseValue = noiseValue;
                    noiseMap[x, y, z] = noiseValue;

                }
            }
        }
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < length; y++)
            {
                for (int z = 0; z < height; z++)
                {

                    noiseMap[x, y, z] = Mathf.InverseLerp(minNoiseValue, maxNoiseValue, noiseMap[x, y, z]);

                }
            }
        }
        
        return noiseMap;
    }

    
    public static float[,,] GenerateNoisePerl(int width, int length, int height, float scale)
    {
        float[,,] noiseMap = new float[width, length, height];

        if (scale <= 0)
        {
            scale = 0.0001f;
        }

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < length; y++)
            {
                for (int z = 0; z < height; z++)
                {
                    float sampleX = x / scale;
                    float sampleY = y / scale;
                    float sampleZ = z / scale;
                    noiseMap[x, y, z] = Perlin.Noise(sampleX, sampleY, sampleZ);
                }
            }
        }

        return noiseMap;
    }

    public static float GenerateNoisePerl_single(float x, float y, float z, float scale, float lacunarity, float persistanse, int octaves, Vector3[] octavesOffset, Vector3 Offset)
    {
        if (scale <= 0)
        {
            scale = 0.0001f;
        }

        /*Vector3[] octavesOffset = new Vector3[octaves];

        System.Random rand = new System.Random();

        for (int i = 0; i < octavesOffset.Length; ++i)
        {
            octavesOffset[i].x = rand.Next(-5000, 5000);
            octavesOffset[i].y = rand.Next(-5000, 5000);
            octavesOffset[i].z = rand.Next(-5000, 5000);
        }
        */
        float maxNoiseValue = float.MinValue;
        float minNoiseValue = float.MaxValue;

        float noiseValue = 0;
        float frequency = 1;
        float amplitude = 1;

        for (int n = 0; n < octavesOffset.Length; ++n)
        {
            float sampleX = (x + Offset.x) / scale * frequency * octavesOffset[n].x;
            float sampleY = (y + Offset.y) / scale * frequency * octavesOffset[n].y;
            float sampleZ = (z + Offset.z) / scale * frequency * octavesOffset[n].z;

            noiseValue += (Perlin.Noise(sampleX, sampleY, sampleZ) * 2 - 1) * amplitude;

            amplitude *= persistanse;
            frequency *= lacunarity;
        }
        Mathf.InverseLerp(minNoiseValue, maxNoiseValue, noiseValue);

        if (noiseValue < minNoiseValue) minNoiseValue = noiseValue;
        else if (noiseValue > maxNoiseValue) maxNoiseValue = noiseValue;

        return noiseValue;
    }
}
