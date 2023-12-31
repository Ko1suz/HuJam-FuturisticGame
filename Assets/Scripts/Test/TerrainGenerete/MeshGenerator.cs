using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour
{
    Mesh mesh;

    Vector3[] vertices;
    int[] triangels;
    Vector2[] uvs;

    public int xSize = 20;
    public int zSize = 20;
    public float multiplyY = 2;
    public float multiplyZ = 2;
    public int XbuferIndex = 1;
    public int YbuferIndex = 5;

    public bool Ters = false;
    public bool randomBuffer = false;
    int randomXbuffer;
    int randomYbuffer;
    public int _z_Axis_Ofsset;
    Vector3 objectStartPos;
    Material material;
    // Start is called before the first frame update
    void Start()
    {
        randomXbuffer= Random.Range(0, 8);
        randomYbuffer= Random.Range(0, 8);
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        CreateShape();
        objectStartPos = transform.position;
        Debug.Log("VErtices ->"+vertices.Length);
         material = GetComponent<MeshRenderer>().materials[0];
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateMesh();
    }

    void CreateShape()
    {
        vertices = new Vector3[(xSize + 1 ) * (zSize + 1)];

        
        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                float y = Mathf.PerlinNoise(x * .3f, z * .3f) * 2f;
                vertices[i] = new Vector3(x, y, z);
                i++;
            }
        }

        int vert = 0;
        int tris = 0;
        triangels = new int[xSize * zSize * 6];
        for (int i = 0; i < zSize; i++)
        {
            for (int x = 0; x < xSize; x++)
            {
                triangels[tris + 0] = vert + 0;
                triangels[tris + 1] = vert + xSize + 1;
                triangels[tris + 2] = vert + 1;
                triangels[tris + 3] = vert + 1;
                triangels[tris + 4] = vert + xSize + 1;
                triangels[tris + 5] = vert + xSize + 2;

                vert++;
                tris += 6;
            }
            vert++;
        }

        uvs = new Vector2[vertices.Length];
        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                uvs[i] = new Vector2((float)x / xSize, (float)z / zSize);
                i++;
            }
        }

    }

    // 0->100 100->200   0-1-0  zindex zsize zindex > zsize/2  -> zindex - zsize/2
    void UpdateMesh()
    {
        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                if (Ters)
                {
                    if (randomBuffer)
                    {
                        if (multiplyZ == 0)
                        {
                            float y = Mathf.PerlinNoise(x * (AudioPeer.audioBandBuffer[randomXbuffer] * multiplyY), z * (AudioPeer.audioBandBuffer[randomYbuffer] * multiplyY)) * 2f;
                            vertices[i] = new Vector3(x, 0.05f * (xSize - x) * y, z);
                        }
                        else
                        {
                            float y = Mathf.PerlinNoise(x * (AudioPeer.audioBandBuffer[randomXbuffer] * multiplyY), z * (AudioPeer.audioBandBuffer[randomYbuffer] * multiplyY)) * 2f;

                            if (z > zSize / 2)
                            {
                                vertices[i] = new Vector3(x, 0.05f * (xSize - x) * y * (-(z - zSize) * multiplyZ), z);
                            }
                            else
                            {
                                vertices[i] = new Vector3(x, 0.05f * (xSize - x) * y * (z * multiplyZ), z);
                            }

                            float intensity = 10 * (AudioPeer.audioBandBuffer[1] * 5);
                            material.SetFloat("_Intesity", intensity);
                            //vertices[i] = new Vector3(x, 0.05f * (xSize - x) * y * (-(z - zSize)*multiplyZ), z);

                        }
                    }
                    else
                    {
                        if (multiplyZ == 0)
                        {
                            float y = Mathf.PerlinNoise(x * (AudioPeer.audioBandBuffer[XbuferIndex] * multiplyY), z * (AudioPeer.audioBandBuffer[YbuferIndex] * multiplyY)) * 2f;
                            vertices[i] = new Vector3(x, 0.05f * (xSize - x) * y, z);
                        }
                        else
                        {
                            float y = Mathf.PerlinNoise(x * (AudioPeer.audioBandBuffer[XbuferIndex] * multiplyY), z * (AudioPeer.audioBandBuffer[YbuferIndex] * multiplyY)) * 2f;

                            if (z > zSize / 2)
                            {
                                vertices[i] = new Vector3(x, 0.05f * (xSize - x) * y * (-(z - zSize) * multiplyZ), z);
                            }
                            else
                            {
                                vertices[i] = new Vector3(x, 0.05f * (xSize - x) * y * (z * multiplyZ), z);
                            }

                            //vertices[i] = new Vector3(x, 0.05f * (xSize - x) * y * (-(z - zSize)*multiplyZ), z);

                        }
                    }
                   
                }
                else
                {
                    if (randomBuffer)
                    {
                        if (multiplyZ == 0)
                        {
                            float y = Mathf.PerlinNoise(x * (AudioPeer.audioBandBuffer[randomXbuffer] * multiplyY), z * (AudioPeer.audioBandBuffer[randomYbuffer] * multiplyY)) * 2f;
                            vertices[i] = new Vector3(x, 0.05f * x * y, z);
                        }
                        else
                        {
                            float y = Mathf.PerlinNoise(x * (AudioPeer.audioBandBuffer[randomXbuffer] * multiplyY), z * (AudioPeer.audioBandBuffer[randomYbuffer] * multiplyY)) * 2f;
                            if (z > zSize / 2)
                            {
                                vertices[i] = new Vector3(x, 0.05f * x * y * (-(z - zSize) * multiplyZ), z);
                            }
                            else
                            {
                                vertices[i] = new Vector3(x, 0.05f * x * y * (z * multiplyZ), z);
                            }
                            float intensity = 10 * (AudioPeer.audioBandBuffer[1] * 5);
                            material.SetFloat("_Intesity", intensity);
                        }
                    }
                    else
                    {
                        if (multiplyZ == 0)
                        {
                            float y = Mathf.PerlinNoise(x * (AudioPeer.audioBandBuffer[XbuferIndex] * multiplyY), z * (AudioPeer.audioBandBuffer[YbuferIndex] * multiplyY)) * 2f;
                            vertices[i] = new Vector3(x, 0.05f * x * y, z);
                        }
                        else
                        {
                            float y = Mathf.PerlinNoise(x * (AudioPeer.audioBandBuffer[XbuferIndex] * multiplyY), z * (AudioPeer.audioBandBuffer[YbuferIndex] * multiplyY)) * 2f;
                            if (z > zSize / 2)
                            {
                                vertices[i] = new Vector3(x, 0.05f * x * y * (-(z - zSize) * multiplyZ), z);
                            }
                            else
                            {
                                vertices[i] = new Vector3(x, 0.05f * x * y * (z * multiplyZ), z);
                            }

                        }
                    }
                }

               
                i++;
            }
        }
      
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangels;
        mesh.uv = uvs;
        mesh.RecalculateNormals();
    }

    //private void OnDrawGizmos()
    //{
    //    if (vertices == null) return;

    //    for (int i = 0; i < vertices.Length; i++)
    //    {
    //        Gizmos.DrawSphere(vertices[i], .1f);
    //    }
    //}
}
