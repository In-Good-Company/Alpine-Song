using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShiftTrigger : MonoBehaviour
{
    public Camera mainCamPosSwitch;
    public GameObject transition_InPos;
    public GameObject transition_OutPos;


    private void Start()
    {
        if(mainCamPosSwitch == null)
        {
            mainCamPosSwitch = Camera.main;
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            mainCamPosSwitch.GetComponent<CameraShift>().obj_CamPosObject = transition_InPos;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            mainCamPosSwitch.GetComponent<CameraShift>().obj_CamPosObject = transition_OutPos;
        }
    }
}
