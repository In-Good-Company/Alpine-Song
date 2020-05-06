using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainMaterialManager : MonoBehaviour
{
    public Transform playerPos;
    public TerrainData _terrainData;
    public int alphaHeight;
    public int alphaWidth;

    public float[,,] splatmapData;
    public int numTextures;

    private PlayerMove playerMove;
    // Start is called before the first frame update
    void Start()
    {
        GetTerrain();
        playerMove = GetComponent<PlayerMove>();
        playerPos = this.transform;
    }

    void GetTerrain()
    {
        _terrainData = Terrain.activeTerrain.terrainData;
        alphaHeight = _terrainData.alphamapHeight;
        alphaWidth = _terrainData.alphamapWidth;
        splatmapData = _terrainData.GetAlphamaps(0, 0, alphaWidth, alphaHeight);
        numTextures = splatmapData.Length / (alphaWidth * alphaHeight);
    }

     private Vector3 GetSplatCoordinate(Vector3 playerPos)
    {
        Vector3 coordinate = new Vector3();
        Terrain _terrain = Terrain.activeTerrain;
        Vector3 terrainPosition = _terrain.transform.position;
        coordinate.x = ((playerPos.x - terrainPosition.x) / _terrain.terrainData.size.x) * _terrain.terrainData.alphamapWidth;
        coordinate.z = ((playerPos.z - terrainPosition.z) / _terrain.terrainData.size.z) * _terrain.terrainData.alphamapHeight;
        return coordinate;
    }

    private int GetActiveTerrainTextureIdx(Vector3 playerPos)
    {
        Vector3 TerrainCord = GetSplatCoordinate(playerPos);
        int textureID = 0;
        float comp = 0f;
        for (int i = 0; i < numTextures; i++)
        {
            if (comp < splatmapData[(int)TerrainCord.z, (int)TerrainCord.x, i])
                textureID = i;
        }
        return textureID;
    }

    public int GetTerrainAtPosition(Vector3 pos)
    {
        int terrainIdx = GetActiveTerrainTextureIdx(pos);
        return terrainIdx;
    }

    private void PlayFootStepSound(int _terrainIDX)
    {
        if (playerMove.destinationReached == false && playerMove.markerPlaced == true)
        {
            switch (_terrainIDX)
            {
                case 0:
                    AkSoundEngine.PostEvent("Footsteps", gameObject);
                    break;
                case 1:
                    //AkSoundEngine.PostEvent("Footsteps_Dirt", gameObject);
                    break;
                case 3:
                    //AkSoundEngine.PostEvent("Footsteps_Sand", gameObject);
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        int terrainIdx = GetActiveTerrainTextureIdx(playerPos.transform.position);
        PlayFootStepSound(terrainIdx);
    }
}
