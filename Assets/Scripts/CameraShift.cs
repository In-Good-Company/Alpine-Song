using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShift : MonoBehaviour
{
    public GameObject obj_CamPosObject;

    private void Update()
    {
        this.transform.position = obj_CamPosObject.transform.position;
        this.transform.rotation = obj_CamPosObject.transform.rotation;
    }
};
