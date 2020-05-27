using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainSwitchCollider : MonoBehaviour
{
    public Collider col;
    public Terrain targetTerrain;
    private void Start()
    {
        if (col == null)
        {
            col = GetComponent<BoxCollider>();
        }
    }
}
