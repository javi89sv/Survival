using UnityEngine;

public class ProceduralMapGenerator : MonoBehaviour
{
    public int mapWidth;
    public int mapHeight;
    public float noiseScale;
    public int octaves;
    public float persistence;
    public float lacunarity;
    public Vector2 offset;

    private void Start()
    {
        float[,] heightMap = GenerateHeightMap();
        Renderer meshRenderer = GetComponent<Renderer>();
        meshRenderer.material.mainTexture = GenerateTexture(heightMap);
    }

    private float[,] GenerateHeightMap()
    {
        float[,] heightMap = new float[mapWidth, mapHeight];
        float maxLocalNoiseHeight = float.MinValue;
        float minLocalNoiseHeight = float.MaxValue;
        float halfWidth = mapWidth / 2f;
        float halfHeight = mapHeight / 2f;

        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                float amplitude = 1;
                float frequency = 1;
                float noiseHeight = 0;

                for (int i = 0; i < octaves; i++)
                {
                    float sampleX = (x - halfWidth + offset.x) / noiseScale * frequency;
                    float sampleY = (y - halfHeight + offset.y) / noiseScale * frequency;

                    float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;
                    noiseHeight += perlinValue * amplitude;

                    amplitude *= persistence;
                    frequency *= lacunarity;
                }

                if (noiseHeight > maxLocalNoiseHeight)
                {
                    maxLocalNoiseHeight = noiseHeight;
                }
                else if (noiseHeight < minLocalNoiseHeight)
                {
                    minLocalNoiseHeight = noiseHeight;
                }

                heightMap[x, y] = noiseHeight;
            }
        }

        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                heightMap[x, y] = Mathf.InverseLerp(minLocalNoiseHeight, maxLocalNoiseHeight, heightMap[x, y]);
            }
        }

        return heightMap;
    }

    private Texture2D GenerateTexture(float[,] heightMap)
    {
        Texture2D texture = new Texture2D(mapWidth, mapHeight);

        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                texture.SetPixel(x, y, Color.Lerp(Color.black, Color.white, heightMap[x, y]));
            }
        }

        texture.Apply();
        return texture;
    }
}







//void AddTrees()
//{
//    for (int i = 0; i < treeCount; i++)
//    {
//        Vector3 treePosition = new Vector3(Random.Range(-mapWidth / 2, mapWidth / 2), 0, Random.Range(-mapHeight / 2, mapHeight / 2));
//        Instantiate(treePrefab, treePosition, Quaternion.identity);
//    }
//}

//void AddRocks()
//{
//    for (int i = 0; i < rockCount; i++)
//    {
//        Vector3 rockPosition = new Vector3(Random.Range(-mapWidth / 2, mapWidth / 2), 0, Random.Range(-mapHeight / 2, mapHeight / 2));
//        Instantiate(rockPrefab, rockPosition, Quaternion.identity);
//    }
//}


//void AddRivers()
//{
//    // Code for adding rivers goes here
//}



