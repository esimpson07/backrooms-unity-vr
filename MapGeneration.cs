using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MapGeneration : MonoBehaviour
{
    [SerializeField] private int RenderDistance;

    public GameObject XROrigin;
    public GameObject Ground;
    public GameObject Ceiling;
    public GameObject Wall1;
    public GameObject Pillar1;

    System.Random rand = new System.Random();

    private int Wall1Prob;
    private int Wall2Prob;
    private int Wall3Prob;
    private int Wall4Prob;

    private int i;
    private int b;
    private int n;
    private int a;

    private float wd = 4.0f; // Wall distance
    private float wh = 1.25f; // Wall height

    void Start()
    {

    }

    void CreateWall(float x, float y, float z, float r)
    {
        GameObject Wall1Clone = Instantiate(Wall1, new Vector3(x,(y + wh),z), Quaternion.Euler(new Vector3(0.0f, r, 0.0f)));
    }

    void CreatePillar(float x, float y, float z)
    {
        GameObject Pillar1Clone = Instantiate(Pillar1, new Vector3(x,(y + wh),z), Quaternion.identity);
    }

    void SpawnChunks(int x, int y, int z)
    {
        //Debug.Log("X = " + x + " and Z = " + z);
        if(!Physics.Raycast(new Vector3(x,(y + 1.9f),z),Vector3.down,2.0f))
        {
            GameObject GroundClone = Instantiate(Ground, new Vector3(x,y,z), Ground.transform.rotation);
            GameObject CeilingClone = Instantiate(Ceiling, new Vector3(x,(y + 3.0f),z), Ceiling.transform.rotation);
            Wall1Prob = rand.Next(0,2) * rand.Next(0,2);
            Wall2Prob = rand.Next(0,2) * rand.Next(0,2);
            Wall3Prob = rand.Next(0,2) * rand.Next(0,2);
            Wall4Prob = rand.Next(0,2) * rand.Next(0,2);
            if(Wall1Prob != 1)
            {
                CreateWall(x + wd,y,z,0);
            }
            if(Wall2Prob != 1)
            {
                CreateWall(x,y,z + wd,90);
            }
            if(Wall3Prob != 1)
            {
                CreateWall(x - wd,y,z,0);
            }
            if(Wall4Prob != 1)
            {
                CreateWall(x,y,z - wd,90);
            }
            if(Wall1Prob + Wall2Prob + Wall3Prob + Wall4Prob <= 3)
            {
                CreatePillar(x,y,z);
            }
        }
    }

    void Update()
    {
        for(int i = -RenderDistance; i <= RenderDistance; i ++)
        {
            for(int b = -RenderDistance; b <= RenderDistance; b ++)
            {
                SpawnChunks(((i + (int)(XROrigin.transform.position.x / 15)) * 15), 0, ((b + (int)(XROrigin.transform.position.z / 15)) * 15));
            }
        }
    }
}
