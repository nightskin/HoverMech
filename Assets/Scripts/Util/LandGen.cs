using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LandGen : MonoBehaviour
{
    public float perlin = 0.3f;
    public int gridX = 20;
    public int gridZ = 20;
    public GameObject ocean;

    Mesh land;
    Vector3[] vertices;
    int[] triangles;

    void Start()
    {
        transform.position = new Vector3(-gridX/2, 0, -gridZ/2);
        ocean.transform.localScale = new Vector3(gridX/2, 1, gridZ/2);

        land = new Mesh();
        GetComponent<MeshFilter>().mesh = land;
        CreateLand();
        DrawLand();
    }

    void CreateLand()
    {
        vertices = new Vector3[(gridX + 1) * (gridZ + 1)];
       
        int i = 0;
        for (int z = 0; z <= gridZ; z++)
        {
            for (int x = 0; x <= gridX; x++)
            {
                float y = Mathf.PerlinNoise(x * perlin, z * perlin) * 2;
                vertices[i] = new Vector3(x, y, z);
                i++;
            }
        }

        triangles = new int[gridX * gridZ * 6];

        int v = 0;
        int t = 0;

        for (int z = 0; z < gridZ; z++)
        {
            for (int x = 0; x < gridX; x++)
            {

                triangles[t + 0] = v + 0;
                triangles[t + 1] = v + gridX + 1;
                triangles[t + 2] = v + 1;
                triangles[t + 3] = v + 1;
                triangles[t + 4] = v + gridX + 1;
                triangles[t + 5] = v + gridX + 2;

                v++;
                t += 6;
            }
            v++;
        }
        
    }

    void DrawLand()
    {
        land.Clear();
        land.vertices = vertices;
        land.triangles = triangles;
        land.RecalculateNormals();
        GetComponent<MeshCollider>().sharedMesh = land;
    }

}
