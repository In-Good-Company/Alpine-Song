using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShift : MonoBehaviour
{
    public float closeCamPos_X;
    public float closeCamPos_Y;
    public float closeCamPos_Z;

    public float closeCamRot_X;
    private Vector3 closeCamPositioner;
    private Vector3 closeCamRot;

    public float farCamPos_X;
    public float farCamPos_Y;
    public float farCamPos_Z;

    public float lerpSpeed;

    public float farCamRot_X;
    private Vector3 farCamPositioner;
    private Vector3 farCamRot;

    public int camCase;

    public GameObject camera_Main;

    public bool triggerExit;

    public Vector3 farLerpPos;
    public Vector3 farLerpRot;
    public Vector3 closeLerpPos;
    public Vector3 closeLerpRot;

    void Start()
    {
        closeCamPos_X = 0.0f;
        closeCamPos_Y = 16.0f;
        closeCamPos_Z = -15.0f;
        closeCamRot_X = 40.0f;

        farCamPos_X = 0.0f;
        farCamPos_Y = 60.0f;
        farCamPos_Z = -21.0f;
        farCamRot_X = 60.0f;

        lerpSpeed = 0.01f;//* Time.deltaTime;

        closeCamRot = new Vector3(closeCamRot_X, 0, 0);
        farCamRot = new Vector3(farCamRot_X, 0, 0);

        closeCamPositioner = new Vector3(closeCamPos_X, closeCamPos_Y, closeCamPos_Z);
        farCamPositioner = new Vector3(farCamPos_X, farCamPos_Y, farCamPos_Z);

        ///////


        farLerpPos = Vector3.Lerp(closeCamPositioner, farCamPositioner, lerpSpeed * Time.deltaTime);
        farLerpRot = Vector3.Lerp(closeCamRot, farCamRot, lerpSpeed * Time.deltaTime);
        closeLerpPos = Vector3.Lerp(farCamPositioner, closeCamPositioner, lerpSpeed * Time.deltaTime);
        closeLerpRot = Vector3.Lerp(farCamRot, closeCamRot, lerpSpeed * Time.deltaTime);
    }

    private void Awake()
    {
       


    }
    public void camSwitch(int camCase)
    {
        

    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "Player")
        {
            camCase = 2;
        }

    }

    private void OnTriggerExit(Collider col)
    {
        print("exit");
        if (col.gameObject.name == "Player" && triggerExit == true)
        {
            camCase = 1;
        }
    }

    private void Update()
    {
        
        camSwitch(camCase);

        switch (camCase)
        {
            case 1:

                camera_Main.transform.localEulerAngles = Vector3.Lerp(closeCamRot, farCamRot, lerpSpeed / Time.deltaTime); ;
                camera_Main.transform.localPosition = Vector3.Lerp(closeCamPositioner, farCamPositioner, lerpSpeed / Time.deltaTime); ;




                break;
            case 2:

                camera_Main.transform.localPosition = Vector3.Lerp(farCamRot, closeCamRot, lerpSpeed / Time.deltaTime);
                camera_Main.transform.localEulerAngles = Vector3.Lerp(farCamPositioner, closeCamPositioner, lerpSpeed / Time.deltaTime);




                break;

        }
    }
};
