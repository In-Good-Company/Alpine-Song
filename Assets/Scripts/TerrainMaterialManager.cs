using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainMaterialManager : MonoBehaviour
{
    public Transform playerPos;
    public TerrainData _terrainData;
    public int alphaHeight;
    public int alphaWidth;
    public float footStep_Pause;
    public float Timer;
    private float timerCountDown;
    public float[,,] splatmapData;
    public int numTextures;

    private PlayerMove playerMove;
    // Start is called before the first frame update
    void Start()
    {
        GetTerrain();
        playerMove = GetComponent<PlayerMove>();
        playerPos = this.transform;
        footStep_Pause = 1.5f;
        Timer = footStep_Pause;
        timerCountDown = 1 * Time.deltaTime;
        
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


    IEnumerator StepPause()
   {
        yield return new WaitForSeconds(footStep_Pause);
        
        print("step");
        
        yield return null;
        
    }
    
    
    private void PlayFootStepSound(int _terrainIDX)
    {
        
        if (playerMove.destinationReached == false && playerMove.markerPlaced == true)
        {
            switch (_terrainIDX)
            {
                case 0:
                    print("step0");

                    break;
                case 1:
                    print("step1");

                    break;
                case 2:
                    print("step2");
                    if (Timer <= 0.0f)
                    {
                        AkSoundEngine.PostEvent("Footsteps", gameObject);
                        Timer = footStep_Pause;
                    }

                    break;
                case 3:
                    print("step3");
                    //AkSoundEngine.PostEvent("Footsteps_Sand", gameObject);
                    break;
                case 4:
                    print("step4");
                    break;

                case 5:
                    print("step5");
                    break;

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        int terrainIdx = GetActiveTerrainTextureIdx(playerPos.transform.position);
        PlayFootStepSound(terrainIdx);
        Timer -= timerCountDown;
        
    }
}
